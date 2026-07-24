using System;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;
    public Player player;
    [NonSerialized]
    public DungeonMap map;
    public Vector2Int gridSize;
    public Animator fadeAnimator;


    private void Awake()
    {
        instance = this;
        map = new DungeonMap(gridSize);
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector2Int WorldToMapPos(Vector2 worldPos)
    {
        return new Vector2Int(gridSize.x / 2 + Mathf.FloorToInt(worldPos.x), gridSize.y / 2 + Mathf.FloorToInt(worldPos.y));
    }
}

public class DungeonMap
{
    public MapNode[,] nodes;
    
    public DungeonMap(Vector2Int mapSize)
    {
        nodes = new MapNode[mapSize.x, mapSize.y];
    }

}
