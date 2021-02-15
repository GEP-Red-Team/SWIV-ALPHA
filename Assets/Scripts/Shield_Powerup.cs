using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield_Powerup : MonoBehaviour
{
    public int hits = 4;
    public Vector3 active_scale = new Vector3(2.5f, 2.5f, 2.5f);
    public float duration = 25f;

    private int hits_remaining;
    private bool active = false;
    private GameObject player = null;
    private float active_time;

    // Start is called before the first frame update
    void Start()
    {
        hits_remaining = hits;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(active)
        {
            if((Time.time - active_time) >= duration)
            {
                Destroy(gameObject);
            }
        }
    }

    void LateUpdate()
    {
        if (active)
        {
            transform.position = player.transform.position;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!active)
            {
                OnPickedUp();
            }
        }
        else if (other.CompareTag("Bullet"))
        {
            if (!other.GetComponent<Bullet_Controller>().IsFiredByPlayer())
            {
                Destroy(other);
                hits_remaining--;
                if (hits_remaining == 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    public void OnPickedUp()
    {
        active = true;
        active_time = Time.time;
        transform.localScale = active_scale;
    }
}
