using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class MapNode : MonoBehaviour
{
    public FeatureType featureNorth;
    public FeatureType featureWest;
    public FeatureType featureEast;
    public FeatureType featureSouth;

    public EncounterSet encounterSet;
    //[NonSerialized]
    public List<Enemy> currentEnemies = new List<Enemy>();

    public bool explored = false;
    public bool discovered = false;
    public bool encountered = false;
    

    public Vector2Int gridPos;

    private void Start()
    {

        gridPos = MapManager.instance.WorldToMapPos(transform.position);
        if (gridPos.x < 0 || gridPos.x > MapManager.instance.map.nodes.GetLength(0) ||
            gridPos.y < 0 || gridPos.y > MapManager.instance.map.nodes.GetLength(1))
        {
            Debug.Log("Tile out of bounds at " + transform.position);
        }
        else
        {
            MapManager.instance.map.nodes[gridPos.x, gridPos.y] = this;
        }
            
    }

    private void Update()
    {
        currentEnemies = currentEnemies.Where(e => e.currentHeat < e.maxHeat).ToList();

        
    }

    public void OnEnter(int dir)
    {
        if (encounterSet != null)
        {
            Encounter encounter = encounterSet.GetRandomEncounter();
            if (encounter != null && !encountered)
            {
                int currSlot = 0;
                foreach (EnemyBase enemy in encounter.enemies)
                {
                    currentEnemies.Add(new Enemy(enemy, currSlot));
                    currSlot++;
                }
                encountered = true;
            }
        }
        BattleManager.instance.InitiateEncounter();
    }



    public FeatureType GetFeature(int dir)
    {
        switch (dir)
        {
            case 0:
                return featureNorth;
            case 1:
                return featureEast;
            case 2:
                return featureSouth;
            case 3:
                return featureWest;
            default:
                return FeatureType.Wall;
        }
    }

    public bool DirectionTraversable(int dir)
    {
        return GetFeature(dir) == FeatureType.Exit;
    }


}
