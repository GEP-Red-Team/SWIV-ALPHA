using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class SineWaveEnemy : IEnemy
{
    [SerializeField] private float frequency = 3.0f;
    [SerializeField] private float magnitude = 1.0f;
    [SerializeField] private float sineSpeed = 1.0f;

    private void OnEnable()
    {
        startPosition = this.transform.position;
        newPosition = startPosition;
    }

    private void Update()
    {
        newPosition.y -= Data.speed * Time.deltaTime;
        newPosition.x = startPosition.x + sineSpeed * Mathf.Sin(Time.time * frequency + Offset) * magnitude;
        transform.position = newPosition;
    }
}