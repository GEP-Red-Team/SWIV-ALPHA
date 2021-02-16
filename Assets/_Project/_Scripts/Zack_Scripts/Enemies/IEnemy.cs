using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IEnemy : MonoBehaviour
{
    [SerializeField] private EnemyData data;
    [SerializeField] private int id;
    [SerializeField] private float offset;


    public EnemyData Data => data;
    public int ID { get => id; set => id = value; }
    public float Offset { get => offset; set => offset = value; }
}
