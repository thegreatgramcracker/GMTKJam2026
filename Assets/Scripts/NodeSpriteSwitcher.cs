using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteAlways]
public class NodeSpriteSwitcher : MonoBehaviour
{
    public NodeSprite[] sprites;
    public MapNode mapNode;
    public SpriteRenderer renderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        NodeSprite spr = sprites.FirstOrDefault(s =>
        s.traverseNorth == mapNode.DirectionTraversable("North") &&
        s.traverseEast == mapNode.DirectionTraversable("East") &&
        s.traverseWest == mapNode.DirectionTraversable("West") &&
        s.traverseSouth == mapNode.DirectionTraversable("South"));

        if (spr != null)
        {
            renderer.sprite = spr.sprite;
        }
    }
}
[System.Serializable]
public class NodeSprite
{
    public bool traverseNorth;
    public bool traverseWest;
    public bool traverseEast;
    public bool traverseSouth;

    public Sprite sprite;
}
