using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle_Controller : MonoBehaviour
{
    private Vector2 screen_bounds;
    public GameObject player = null;
    private Rigidbody rb = null;
    public float missle_speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        screen_bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        player = GameObject.FindGameObjectWithTag("Player");
        rb = transform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (transform.position.x > screen_bounds.x || transform.position.x < -screen_bounds.x ||
            transform.position.y > screen_bounds.y || transform.position.y < -screen_bounds.y)
        {
            Destroy(gameObject);
        }
    }
    private void Move()
    {
        transform.LookAt(player.transform, Vector3.forward);
        rb.velocity = transform.forward * missle_speed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet"))
        {
            // destroy missle
            Destroy(gameObject);
        }
    }
}
