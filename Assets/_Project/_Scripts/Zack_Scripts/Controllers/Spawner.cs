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
        public string[] enemyType; // specify the enemy type/s for this wave
        public int[] enemyAmount; // specify the amount of enemies you want of a given type
        public int spawnRate; // control the speed at which the enemies spawn for wave
    }

    [SerializeField]
    public enum SpawnState
    {
        Spawning,
        Countdown,
        Wait
    } // used for stopping spawning of multiple coroutines 

    public Wave[] waves;
    public Transform[] spawnPoints;
    public float waveTime = 5f; // used to choose the time to countdown to the next wave

    [SerializeField] private int nextWave = 0;
    [SerializeField] private float waveCountdown;
    [SerializeField] private SpawnState state = SpawnState.Countdown;

    private void Start() { waveCountdown = waveTime; }

    public void Spawn(ref Dictionary<string, List<GameObject>> enemies)
    {
        if (waveCountdown <= 0)
        {
            if (state != SpawnState.Spawning) { StartCoroutine(SpawnWave(enemies, waves[nextWave])); }
        }
        else { waveCountdown -= Time.deltaTime; }
    }


    private IEnumerator SpawnWave(IReadOnlyDictionary<string, List<GameObject>> enemies, Wave wave)
    {
        state = SpawnState.Spawning; // stops any attempts to spawn anymore waves 
        for (var i = 0; i < wave.enemyType.Length; i++)
        {
            var amount = wave.enemyAmount[i];
            var type = wave.enemyType[i];
            var enemyList = enemies[type];
            SpawnEnemies(enemyList, amount);

            yield return new WaitForSeconds(wave.spawnRate);
        }

        state = SpawnState.Wait;
        yield break;
    }

    private void SpawnEnemies(IReadOnlyList<GameObject> enemyList, int counter)
    {
        var spawn = Random.Range(0, spawnPoints.Length - 1);
        for (var j = 0; j < counter; j++)
        {
            enemyList[j].transform.position = spawnPoints[spawn].position;
            enemyList[j].SetActive(true);
        }
    }


    public SpawnState State { get => state; set => state = value; }
}