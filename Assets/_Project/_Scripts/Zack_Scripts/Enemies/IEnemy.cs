using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Base class that all enemies inherit from
/// </summary>
public class IEnemy : MonoBehaviour
{
    [SerializeField] private EnemyData data;
    [SerializeField] private int id;
    [SerializeField] private float offset; // used for spawning at an offset to original enemy
    
    public EnemyData Data => data;
    public int ID { get => id; set => id = value; }
    public float Offset { get => offset; set => offset = value; }
}
