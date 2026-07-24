using System;
using UnityEngine;

[Serializable]
public class Enemy
{
    public string name;
    public Sprite sprite;
    public Color color;
    public int maxHeat;
    public int attack;
    public int armor;
    public int currentHeat;
    public int slot;
    public int divNumber;

    public EnemyBase enemyBase;

    public Enemy(EnemyBase enemy, int _slot)
    {
        enemyBase = enemy;
        name = enemy.name;
        sprite = enemy.divSprites[0];
        color = enemy.color;
        maxHeat = enemy.maxHeat;
        attack = enemy.baseAttack;
        armor = enemy.baseArmor;
        currentHeat = 0;
        divNumber = 0;
        slot = _slot;
    }

    public Enemy(Enemy enemy, int div, int spriteIndex)
    {
        enemyBase = enemy.enemyBase;
        name = enemy.name;
        sprite = enemy.enemyBase.divSprites[spriteIndex];
        divNumber = spriteIndex;
        color = enemy.color;
        maxHeat = enemy.maxHeat / div;
        attack = enemy.enemyBase.baseAttack / div;
        armor = enemy.armor / div;
        currentHeat = enemy.currentHeat;
        slot = enemy.slot;
    }
}
