using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable Objects/Enemy")]
public class Enemy : ScriptableObject
{
    public string enemyName;
    public Sprite sprite;

    public int maxHeat;

    public int baseAttack;
    public int baseArmor;
}
