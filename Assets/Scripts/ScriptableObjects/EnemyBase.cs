using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable Objects/Enemy")]
public class EnemyBase : ScriptableObject
{
    public string enemyName;
    public Sprite[] divSprites;
    public Vector2[] divHeatOffsets;
    public Vector2[] divTargetOffsets;
    public Vector2[] divTargetDimensions; 
    public Color color = Color.white;

    public int maxHeat;

    public int baseAttack;
    public int baseArmor;
}
