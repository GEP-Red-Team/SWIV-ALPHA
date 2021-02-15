using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBox : MonoBehaviour
{
    public GameObject controller;
    [SerializeField] private BackgroundController _bC;

    private void Start()
    {
        _bC = controller.GetComponent<BackgroundController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Background"))
        {
            _bC.CollisionDetected(other.gameObject);
        }
        else if (other.CompareTag("SBackground"))
        {
            _bC.ActivateNewPanel();
            Destroy(other.gameObject);
        }
    }
}