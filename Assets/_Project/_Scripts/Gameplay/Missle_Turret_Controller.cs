using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Timers;
using Random = UnityEngine.Random;

public class Missle_Turret_Controller : IEnemy
{
    public GameObject missle_prefab = null;
    public int score_value = 200;
    public GameObject player = null;
    public float missle_speed = 10f;
    public GameObject playObjects;

    //private Game_Controller game_controller = null;
    private GameObject missle = null;

    //private bool can_shoot = true;
    private float randomTimer = 0.0F;
    private float shotTimer = 0.0F;

    // Start is called before the first frame update
    void Start()
    {
        //game_controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game_Controller>();
        player = GameObject.FindGameObjectWithTag("Player");
        newPosition = transform.position;
        randomTimer = Random.Range(1.0F, 2.0F);
    }

    // Update is called once per framep
    void Update()
    {
        lookAtPlayer();
        moveWithBackground();

        if (shotTimer >= randomTimer)
        {
            tryShoot();
            shotTimer = 0.0F;
            randomTimer = Random.Range(1.0F, 2.0F);
        }

        shotTimer += Time.deltaTime;
    }


    void moveWithBackground()
    {
        newPosition.y -= Data.speed * Time.deltaTime;
        transform.position = newPosition;
    }
    
    void tryShoot() {if(!missle) shootMissle();}

    void shootMissle()
    {
        missle = Instantiate(missle_prefab, transform.position + (transform.forward * 1f), Quaternion.identity);
        Rigidbody rb = missle.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * missle_speed;
    }

    void lookAtPlayer()
    {
        // float x = player.transform.position.x - transform.position.x;
        // float y = player.transform.position.y - player.transform.position.y;
        // float angle = (float)Math.Atan2(y, x);
        // angle *= Mathf.Rad2Deg;
        // transform.Rotate(0, angle, 0);
        // transform.eulerAngles = new Vector3((float)angle, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        transform.LookAt(player.transform, Vector3.back);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet") || other.CompareTag("KillBox"))
        {
            // destroy turret and add score
            //game_controller.AddPlayerScore(score_value);
            gameObject.SetActive(false);
        }
    }
}