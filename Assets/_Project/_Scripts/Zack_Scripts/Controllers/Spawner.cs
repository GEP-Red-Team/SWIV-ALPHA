using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string[] enemyType;  // specify the enemy type/s for this wave
        public int[] enemyAmount;   // specify the amount of enemies you want of a given type
        public float spawnRate;     // control the speed at which the enemies spawn for wave
    }

    [SerializeField]
    public enum SpawnState { Spawning, Countdown, Wait } // used for stopping spawning of multiple coroutines 

    public Wave[] waves;                                // create amount of waves wanted
    public Transform[] spawnPoints = new Transform[3];  // set to 3 as only 3 spawn points at the moment
    public float waveTime = 5f;                         // used to choose the time to countdown to the next wave

    [SerializeField] private int nextWave;              // currently unused
    [SerializeField] private float waveCountdown;
    [SerializeField] private SpawnState state = SpawnState.Countdown;

    private void Start() { waveCountdown = waveTime; }

    public void Spawn(ref Dictionary<string, List<GameObject>> enemies)
    {
        if (waveCountdown <= 0)
        {
            if (state == SpawnState.Spawning) return;
            StartCoroutine(SpawnWave(enemies, waves[nextWave]));
            waveCountdown = waveTime;
            nextWave++;
        }
        else { waveCountdown -= Time.deltaTime; }
    }


    private IEnumerator SpawnWave(IReadOnlyDictionary<string, List<GameObject>> enemies, Wave wave)
    {
        state = SpawnState.Spawning;  // stops any attempts to spawn waves
        
        for (var i = 0; i < wave.enemyType.Length; i++)
        {
            var spawn = Random.Range(0, spawnPoints.Length - 1);
            var amount = wave.enemyAmount[i];
            var type = wave.enemyType[i];
            var enemyList = enemies[type];

            for (var j = 0; j < amount; j++)
            {
                SpawnEnemy(enemyList[j], spawn);
                yield return new WaitForSeconds(wave.spawnRate);
            }
        }

        state = SpawnState.Wait;    // used to stop unnecessary invocation of spawner
    }

    private void SpawnEnemy(GameObject enemy, int spawn)
    {
        enemy.transform.position = spawnPoints[spawn].position;
        enemy.SetActive(true);
    }


    public SpawnState State { get => state; set => state = value; } // used in EnemyController to stop spawning of waves
}