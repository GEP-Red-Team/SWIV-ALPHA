using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public float speed = 10f;
    public string fire_input = "Fire1";
    public float fire_rate = 1f;
    public float bullet_speed = 30f;
    public GameObject bullet_prefab = null;
    public Game_Controller game_controller = null;

    private Rigidbody rb;
    private Vector2 direction;
    private float last_fire_time = 0f;
    private Vector2 screen_bounds;
    private float object_width;
    private float object_height;
    private int lives = 3;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        screen_bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        Mesh mesh = transform.GetComponent<MeshFilter>().mesh;
        object_width = mesh.bounds.extents.x;
        object_height = mesh.bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        // Fire if cooldown reached
        if (Input.GetButton(fire_input))
        {
            if (CanFire())
            {
                last_fire_time = Time.time;
                Fire();
            }
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(new Vector2(transform.position.x, transform.position.y) + (direction * speed * Time.deltaTime));
    }

    void LateUpdate()
    {
        // Keep the player within the screen bounds
        Vector3 view_pos = transform.position;
        view_pos.x = Mathf.Clamp(view_pos.x, -screen_bounds.x + object_width, screen_bounds.x - object_width);
        view_pos.y = Mathf.Clamp(view_pos.y, -screen_bounds.y + object_height, screen_bounds.y - object_height);
        transform.position = view_pos;
    }

    bool CanFire()
    {
        return (Time.time - last_fire_time) >= fire_rate;
    }

    void Fire()
    {
        if(!bullet_prefab)
        {
            Debug.LogError("A bullet game object needs to be assigned in the player controller.");
            return;
        }

        GameObject bullet = Instantiate(bullet_prefab, transform.position + (transform.forward * 1f), Quaternion.identity);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        Bullet_Controller bc = bullet.GetComponent<Bullet_Controller>();
        bc.SetFiredByPlayer(true);
        rb.velocity = transform.forward * bullet_speed;
        bullet.transform.position = transform.position + (transform.forward * 1f);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet"))
        {
            if(!other.GetComponent<Bullet_Controller>().IsFiredByPlayer())
            {
               OnHitByBullet();
            }
        }
    }

    void OnHitByBullet()
    {
        gameObject.SetActive(false);
        lives--;
        if (lives >= 0)
        {
            game_controller.OnPlayerDead();
        }
    }

    public int GetLives()
    {
        return lives;
    }
}
