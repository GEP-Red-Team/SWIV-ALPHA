using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BackgroundData", menuName = "Data/BackgroundData", order = 0)]
public class BackgroundData : ScriptableObject
{
    public Material[] materials;
    public Vector3 panelDimensions;
    public float speed;
}