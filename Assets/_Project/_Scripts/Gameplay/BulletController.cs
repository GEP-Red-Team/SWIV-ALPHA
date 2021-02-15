using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody rb;
    private Vector2 screenBounds;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    // Update is called once per frame
    private void Update()
    {
        // If the bullet goes offscreen, deactivate it
        if (transform.position.x > screenBounds.x || transform.position.x < -screenBounds.x ||
            transform.position.y > screenBounds.y || transform.position.y < -screenBounds.y)
        {
            gameObject.SetActive(false);
        }
    }
}
