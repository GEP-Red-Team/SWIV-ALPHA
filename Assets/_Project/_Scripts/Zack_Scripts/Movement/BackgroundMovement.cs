using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Start()
    {
        speed = GameObject.FindWithTag("BackgroundController").GetComponent<BackgroundData>().speed;
    }

    private void Update()
    {
        transform.Translate(Vector3.back * (speed * Time.deltaTime));
    }
}
