using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Encounter", menuName = "Scriptable Objects/Encounter")]
public class Encounter : ScriptableObject
{
    public List<EnemyBase> enemies;
}
