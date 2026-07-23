using UnityEngine;

public class Player : MonoBehaviour
{
    public MapManager mapManager;
    public Vector2Int gridPos;
    public string facingDir;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gridPos = mapManager.WorldToMapPos(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
