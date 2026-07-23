using UnityEngine;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

public class MapNode : MonoBehaviour
{
    public FeatureType featureNorth;
    public FeatureType featureWest;
    public FeatureType featureEast;
    public FeatureType featureSouth;

    public List<Enemy> enemies = new List<Enemy>();

    public bool explored = false;
    public bool discovered = false;
    

    MapManager mapManager;
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
