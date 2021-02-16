using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    public BackgroundData data;
    private void Update()
    {
        transform.Translate(Vector3.back * (data.speed * Time.deltaTime));
    }
}
