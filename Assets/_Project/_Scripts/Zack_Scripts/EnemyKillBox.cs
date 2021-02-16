using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKillBox : MonoBehaviour
{
    // doesn't actually need to be in a separate script just did it for convenience of placing a separate trigger volume
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
        }
    }
}