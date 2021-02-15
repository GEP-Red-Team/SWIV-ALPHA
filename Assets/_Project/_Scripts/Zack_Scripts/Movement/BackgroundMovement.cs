using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    public float scrollSpeed = 10f;

    private void Update()
    {
        transform.Translate(Vector3.back * (scrollSpeed * Time.deltaTime));
    }
}
