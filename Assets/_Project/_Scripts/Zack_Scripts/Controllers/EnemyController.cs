using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // enemy types you want (set manually in inspector)
    public int[] amountOfEnemyType; // amount of enemy type (set manually in inspector) 
    public Spawner spawner;
    public GameObject playObjects;

    [SerializeField] private Dictionary<string, List<GameObject>> enemies;
    [SerializeField] private float enemyCheckTimer = 2f;

    private void Start() { InitEnemies(); }

    private void Update()
    {
        if (spawner.State == Spawner.SpawnState.Wait)
        {
            enemyCheckTimer -= Time.deltaTime;
            if (!EnemiesAlive())
            {
                enemyCheckTimer = 2f;
                spawner.State = Spawner.SpawnState.Countdown;
            }
            else
            {
                return;
            }
        }

        spawner.Spawn(ref enemies); // expensive invocation
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
                var temp = Instantiate(enemyPrefabs[i], playObjects.transform, true);
                temp.SetActive(false);
                temp.GetComponent<IEnemy>().ID = newID;
                temp.GetComponent<IEnemy>().Offset = -offset;
                tempList.Add(temp);
                newID++;
                offset += 1f;

                if (j < amount - 1) continue;
                var type = temp.GetComponent<IEnemy>()
                    .Data
                    .type; // name set in data container for enemy -> zack_scrips/data/instances
                enemies.Add(type, tempList);
            }
        }
    }

    // returns
    private bool EnemiesAlive()
    {
        return !(enemyCheckTimer <= 0f) || enemies.Any(type => type.Value.Any(obj => obj.activeSelf));
    }
}