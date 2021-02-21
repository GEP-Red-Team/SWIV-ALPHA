using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{
    public float fallSpeed = 3f;

    private Rigidbody rb = null;
    private Vector2 screenBounds;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > screenBounds.x || transform.position.x < -screenBounds.x ||
            transform.position.y > screenBounds.y || transform.position.y < -screenBounds.y)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(new Vector2(transform.position.x, transform.position.y) + (new Vector2(0f, -1f) * fallSpeed * Time.deltaTime));
    }
}
