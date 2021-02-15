using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;

    public string fireInput = "shoot";
    public float fireRate = 1f;

    public float bulletSpeed = 30f;
    public GameObject bulletPrefab = null;

    public List<GameObject> bulletPool = new List<GameObject>();
    public int bulletPoolSize = 10;

    private Rigidbody rb;
    private Vector2 direction;

    private Vector2 screenBounds;

    private float lastFireTime = 0f;

    private float objectWidth;
    private float objectHeight;

    private int currentBulletIndex = 0;

    public delegate void OnPlayerHitDelegate();
    public event OnPlayerHitDelegate OnPlayerHitCallback;

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
            bulletPool[i].SetActive(false);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        // Fire if cooldown reached
        if (CustomInput.InputManager.IsKeyPressed(fireInput))
        {
            if (CanFire())
            {
                Fire();
            }
        }
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

        // Put the bullet in front of the player.
        bullet.transform.position = transform.position + (transform.forward * 1.5f);

        // Set the bullet's velocity.
        bullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;

        // Update current bullet index.
        currentBulletIndex = (currentBulletIndex + 1) % bulletPoolSize;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet"))
        {
            OnPlayerHitCallback();
        }
    }
}
