using UnityEngine;

public class Player : MonoBehaviour
{
    public MapManager mapManager;
    public MapNode currentNode;
    public Vector2Int gridPos;
    public int facingAngle; //0 = north, 3 = west, goes clockwise
    public int wax;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gridPos = mapManager.WorldToMapPos(transform.position);
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, (facingAngle % 4) * -90f));
        currentNode = mapManager.map.nodes[gridPos.x, gridPos.y];
    }

    public bool CanTraverse(string dir)
    {
        if (currentNode == null) return false;
        switch (dir.ToLower())
        {
            case "forward":
                return currentNode.DirectionTraversable(facingAngle);
            case "back":
                return currentNode.DirectionTraversable((facingAngle + 2) % 4);
            case "left":
                return currentNode.DirectionTraversable((facingAngle + 3) % 4);
            case "right":
                return currentNode.DirectionTraversable((facingAngle + 1) % 4);
        }
        return false;
    }

    public void MoveRelative(string dir)
    {
        switch (dir.ToLower())
        {
            case "forward":
                Move(facingAngle);
                break;
            case "back":
                Move((facingAngle + 2) % 4);
                break;
            case "left":
                Move((facingAngle + 3) % 4);
                break;
            case "right":
                Move((facingAngle + 1) % 4);
                break;
        }
    }

    public void Move(int dir)
    {
        switch (dir)
        {
            case 0:
                gridPos += Vector2Int.up;
                break;
            case 1:
                gridPos += Vector2Int.right;
                break;
            case 2:
                gridPos += Vector2Int.down;
                break;
            case 3:
                gridPos += Vector2Int.left;
                break;
        }
        wax--;
        facingAngle = dir;
        currentNode = mapManager.map.nodes[gridPos.x, gridPos.y];
        transform.position = currentNode.transform.position;
    }

}
