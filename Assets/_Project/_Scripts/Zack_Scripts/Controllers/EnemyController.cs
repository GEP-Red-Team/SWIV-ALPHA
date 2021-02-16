using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    
    public GameObject[] enemyPrefabs;
    public int[] amountOfEnemyType;
    public Spawner spawner;

    [SerializeField] private Dictionary<string, List<GameObject>> enemies;

    private void Start()
    {
        InitEnemies();
    }

    private void Update()
    {
        spawner.Spawn(enemies);
    }

    private void InitEnemies()
    {
        enemies = new Dictionary<string, List<GameObject>>();
        var newID = 0;

        for (var i = 0; i < enemyPrefabs.Length; i++)
        {
            var amount = amountOfEnemyType[i];
            var tempList = new List<GameObject>();
            var offset = 0f;

            for (var j = 0; j < amount; j++)
            {
                var temp = Instantiate(enemyPrefabs[i]);
                temp.SetActive(false);
                temp.GetComponent<IEnemy>().ID = newID;
                temp.GetComponent<IEnemy>().Offset = offset;
                tempList.Add(temp);
                newID++;
                offset += 1f;

                if (j < amount - 1) continue;
                var type = temp.GetComponent<IEnemy>().Data.type;
                enemies.Add(type, tempList);
                spawner.HaveSpawned.Add(false);
            }
        }
    }
}