using System.Collections;
using System.Collections.Generic;
using CustomInput;
using Sound;
using UnityEngine;

// Shields will be spawned into the scene under certain conditions:
// - Enemy defeated?
// - On timer?
// - Pickup locations?
public class ShieldPowerup
{
    private float activeLifetime = 15f;

    private bool active = false;
    private float madeActiveTime = 0f;
    private float remainingHits = 0;
    private GameObject shieldGameObject;
    private GameObject playerGameObject;

    public ShieldPowerup(GameObject shieldObject, float shieldObjectScale, GameObject playerObject)
    {
        playerGameObject = playerObject;

        shieldGameObject = shieldObject;
        shieldGameObject.SetActive(false);
        shieldGameObject.transform.localScale = new Vector3(shieldObjectScale, shieldObjectScale, shieldObjectScale);
    }

    public void Activate()
    {
        //play sound
        SoundManager.PlaySound(SoundManager.Sound.ShieldUp);

        //logic
        active = true;
        madeActiveTime = Time.time;
        remainingHits = 4;
        shieldGameObject.SetActive(true);
    }

    public void Deactivate()
    {
        //play sound
        SoundManager.PlaySound(SoundManager.Sound.ShieldUp);
        
        //logic
        active = false;
        shieldGameObject.SetActive(false);
    }

    public void Update()
    {
        if(active)
        {
            if(GetElapsedTimeSinceActivated() >= activeLifetime)
            {
                Deactivate();
            }
        }
    }

    public void LateUpdate()
    {
        shieldGameObject.transform.position = playerGameObject.transform.position;
    }

    public void OnHit()
    {
        remainingHits--;
        if (remainingHits == 0)
        {
            Deactivate();
        }
    }

    public bool IsActive()
    {
        return active;
    }

    private float GetElapsedTimeSinceActivated()
    {
        return Time.time - madeActiveTime;
    }
}

public class PlayerController : MonoBehaviour
{
    public GameObject playObjects;
    
    public float speed = 10f;

    public string fireInput = "shoot";
    public float fireRate = 1f;

    public float bulletSpeed = 30f;
    public GameObject bulletPrefab = null;

    private List<GameObject> bulletPool = new List<GameObject>();
    public int bulletPoolSize = 10;

    public delegate void OnPlayerHitDelegate();
    public event OnPlayerHitDelegate OnPlayerHitCallback;

    public float shieldPowerupObjectScaleOnPlayer = 2.5f;
    public float shieldPowerupObjectScaleInScene = 0.75f;

    private Rigidbody rb;
    private Vector2 direction;

    private Vector2 screenBounds;

    private float lastFireTime = 0f;

    private float objectWidth;
    private float objectHeight;

    private int currentBulletIndex = 0;

    // This is an example of one powerup, but to support multiple powerups "equipped" on the player should be achievable.
    // Use an array of powerup objects that can be added and removed as they are picked up in game.
    private ShieldPowerup shieldPowerup = null;
    public GameObject shieldPowerupGameObject = null;

    private GameStates.PlayState playState;

    private const float BULLET_SPAWN_DISTANCE_FROM_PLAYER = 1.5f;
    private const int ENEMY_SCORE_VALUE = 500;

    public void OnShotEnemy(Transform transform)
    {
        playState.AddPlayerScore(ENEMY_SCORE_VALUE);

        // Should a powerup be dropped?
        if (Random.Range(0,3) == 1)
        {
            // Which powerup should be dropped? There is only one implemented currently but a decision could be made here.
            GameObject powerup = Instantiate(shieldPowerupGameObject, transform.position, Quaternion.identity);
            powerup.transform.localScale = new Vector3(shieldPowerupObjectScaleInScene, shieldPowerupObjectScaleInScene, shieldPowerupObjectScaleInScene);
        }
    }

    public void SetPlayState(GameStates.PlayState state)
    {
        playState = state;
    }

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Get screen extents.
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        // Get mesh extents.
        Mesh mesh = transform.GetComponent<MeshFilter>().mesh;
        objectWidth = mesh.bounds.extents.x;
        objectHeight = mesh.bounds.extents.y;

        // Initialize bullet pool.
        for(int i = 0; i < bulletPoolSize; i++)
        {
            bulletPool.Add(Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity));
            //bulletPool[i].transform.localScale = transform.localScale; // Bullet scale is applied seperately in the bullet prefab.
            bulletPool[i].GetComponent<BulletController>().OnEnemyShot += OnShotEnemy;
            bulletPool[i].transform.SetParent(playObjects.transform);
            bulletPool[i].SetActive(false);
        }

        // Initialize powerups.
        shieldPowerup = new ShieldPowerup(Instantiate(shieldPowerupGameObject), transform.localScale.x * shieldPowerupObjectScaleOnPlayer, gameObject);
    }

    // Update is called once per frame
    private void Update()
    {
        //direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        direction = new Vector2(InputManager.GetAxis(1,"horizontal"), InputManager.GetAxis(1,"vertical"));

        // Fire if cooldown reached
        if (CustomInput.InputManager.GetKeyPressed(1, fireInput))
        {
            if (CanFire())
            {
                Fire();
            }
        }

        // Update powerups.
        shieldPowerup.Update();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(new Vector2(transform.position.x, transform.position.y) + (direction * speed * Time.deltaTime));
    }

    private void LateUpdate()
    {
        // Keep the player within the screen bounds
        Vector3 view_pos = transform.position;
        view_pos.x = Mathf.Clamp(view_pos.x, -screenBounds.x + objectWidth, screenBounds.x - objectWidth);
        view_pos.y = Mathf.Clamp(view_pos.y, -screenBounds.y + objectHeight, screenBounds.y - objectHeight);
        transform.position = view_pos;

        // Late update powerups.
        shieldPowerup.LateUpdate();
    }

    private bool CanFire()
    {
        return (Time.time - lastFireTime) >= fireRate;
    }

    private void Fire()
    {
        if(!bulletPrefab)
        {
            Debug.LogError("A bullet game object needs to be assigned in the player controller.");
            return;
        }

        lastFireTime = Time.time;

        GameObject bullet = bulletPool[currentBulletIndex];
        bullet.SetActive(true);
        SoundManager.PlaySound(SoundManager.Sound.Fire, SoundManager.ParentState.Play);

        // Put the bullet in front of the player.
        bullet.transform.position = transform.position + (transform.forward * BULLET_SPAWN_DISTANCE_FROM_PLAYER);

        // Set the bullet's velocity.
        bullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;

        // Update current bullet index.
        currentBulletIndex = (currentBulletIndex + 1) % bulletPoolSize;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet") || other.CompareTag("Enemy"))
        {
            if(!shieldPowerup.IsActive())
            {
                OnPlayerHitCallback();
            }
            else
            {
                // Deactivate other bullet. This will work with the enemy's or global bullet pool.
            }
        }
        else if(other.CompareTag("PowerupShield") && !shieldPowerup.IsActive())
        {
            shieldPowerup.Activate();
            Destroy(other.gameObject);
        }
    }
}
