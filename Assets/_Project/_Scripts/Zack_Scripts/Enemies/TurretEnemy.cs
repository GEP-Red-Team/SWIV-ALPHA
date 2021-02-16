using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TurretEnemy : IEnemy
{
    [SerializeField] private Vector3 newPosition;
    private void Start()
    {
        newPosition = transform.position;
    }

    private void Update()
    {
        newPosition.y -= Data.speed * Time.deltaTime;
        transform.position = newPosition;
    }
}
