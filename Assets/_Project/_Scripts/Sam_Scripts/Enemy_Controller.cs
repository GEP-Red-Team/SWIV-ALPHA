using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Controller : MonoBehaviour
{
    public float fire_rate = 1f;
    public float bullet_speed = 30f;
    public GameObject bullet_prefab = null;

    private Game_Controller game_controller = null;
    private float last_fire_time = 0f;
    private int score_value = 100;

    // Start is called before the first frame update
    void Start()
    {
        game_controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        // Fire if cooldown reached
        if (CanFire())
        {
            last_fire_time = Time.time;
            Fire();
        }
    }

    bool CanFire()
    {
        return (Time.time - last_fire_time) >= fire_rate;
    }

    void Fire()
    {
        if (!bullet_prefab)
        {
            Debug.LogError("A bullet game object needs to be assigned in the turret controller.");
            return;
        }

        GameObject bullet = Instantiate(bullet_prefab, transform.position + (transform.forward * 1f), Quaternion.identity);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * bullet_speed;
        bullet.transform.position = transform.position + (transform.forward * 1f);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet"))
        {
            game_controller.AddPlayerScore(score_value);
            game_controller.InstantiateShieldPowerup(transform);
            Destroy(gameObject);
        }
    }
}
