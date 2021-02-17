using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Data/EnemyData", order = 1)]
public class EnemyData : ScriptableObject 
{
    public string type;
    public float health;
    public float speed;
    public float attackDamage;
}
