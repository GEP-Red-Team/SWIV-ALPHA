using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Controller : MonoBehaviour
{
    private Rigidbody rb;
    private Vector2 screen_bounds;
    private bool fired_by_player = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        screen_bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        // If the bullet goes offscreen, destroy it
        if (transform.position.x > screen_bounds.x || transform.position.x < -screen_bounds.x ||
            transform.position.y > screen_bounds.y || transform.position.y < -screen_bounds.y)
        {
            Destroy(gameObject);
        }
    }

    public bool IsFiredByPlayer()
    {
        return fired_by_player;
    }

    public void SetFiredByPlayer(bool was_fired_by_player)
    {
        fired_by_player = was_fired_by_player;
    }
}
