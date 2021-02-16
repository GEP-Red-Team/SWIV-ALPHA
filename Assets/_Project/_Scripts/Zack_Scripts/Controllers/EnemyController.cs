using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Dictionary<string, List<GameObject>> enemies;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private GameObject[] amountOfEnemyType;

    private void Start()
    {
        enemies = new Dictionary<string, List<GameObject>>();
        
        GameObject temp;
        
        
    }
}
