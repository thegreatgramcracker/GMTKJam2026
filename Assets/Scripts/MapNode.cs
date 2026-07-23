using UnityEngine;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

public class MapNode : MonoBehaviour
{
    public FeatureType featureNorth;
    public FeatureType featureWest;
    public FeatureType featureEast;
    public FeatureType featureSouth;

    public bool explored = false;
    public bool discovered = false;

    MapManager mapManager;
    public SpriteRenderer indicator;
    public Vector2Int gridPos;

    private void Start()
    {
        mapManager = GetComponentInParent<MapManager>();

        gridPos = mapManager.WorldToMapPos(transform.position);
        if (gridPos.x < 0 || gridPos.x > mapManager.map.nodes.GetLength(0) ||
            gridPos.y < 0 || gridPos.y > mapManager.map.nodes.GetLength(1))
        {
            Debug.Log("Tile out of bounds at " + transform.position);
        }
        else
        {
            mapManager.map.nodes[gridPos.x, gridPos.y] = this;
        }
            
    }



    private void Update()
    {
        indicator.enabled = mapManager.player.gridPos == gridPos;
    }

    public FeatureType GetFeature(string dir)
    {
        switch (dir.ToLower())
        {
            case "north":
                return featureNorth;
            case "west":
                return featureWest;
            case "east":
                return featureEast;
            case "south":
                return featureSouth;
            default:
                return FeatureType.Wall;
        }
    }

    public bool DirectionTraversable(string dir)
    {
        return GetFeature(dir) == FeatureType.Exit;
    }


}
