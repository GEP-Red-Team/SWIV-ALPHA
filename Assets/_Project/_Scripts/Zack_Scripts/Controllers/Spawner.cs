using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using UnityEngine;
using UnityEngine.VFX;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private int enemySpawnBuffer;
    [SerializeField] private float waveTimer = 5f;
    [SerializeField] private List<bool> haveSpawned;

    public List<bool> HaveSpawned => haveSpawned;

    public void Spawn(Dictionary<string, List<GameObject>> objects)
    {
        
    }
}