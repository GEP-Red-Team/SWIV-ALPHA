using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingBackground : MonoBehaviour
{
    [SerializeField] private BackgroundData data;

    private void Start()
    {
        this.GetComponent<Renderer>().material = data.materials[0];
    }
}