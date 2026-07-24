using UnityEngine;
using TMPro;
using System.Collections;

public class Player : MonoBehaviour
{
    public MapNode currentNode;
    public Vector2Int gridPos;
    public int facingAngle; //0 = north, 3 = west, goes clockwise
    public int wax;
    public TextMeshProUGUI waxIndicator;

    public int attack;
    public int burnPower;
    public int armor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gridPos = MapManager.instance.WorldToMapPos(transform.position);
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, (facingAngle % 4) * -90f));
        currentNode = MapManager.instance.map.nodes[gridPos.x, gridPos.y];
        waxIndicator.text = wax.ToString();
        if (currentNode.currentEnemies.Count <= 0)
        {
            BattleManager.instance.bgmMan.Stop();
        }
        else
        {
            BattleManager.instance.bgmMan.PlaySoundByIndex(0);
        }
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
                MenuManager.instance.transition.rotation = Quaternion.Euler(0, 0, 0);
                StartCoroutine(Move(facingAngle));
                break;
            case "back":
                MenuManager.instance.transition.rotation = Quaternion.Euler(0, 0, 180);
                StartCoroutine(Move((facingAngle + 2) % 4));
                break;
            case "left":
                MenuManager.instance.transition.rotation = Quaternion.Euler(0, 0, 90);
                StartCoroutine(Move((facingAngle + 3) % 4));
                break;
            case "right":
                MenuManager.instance.transition.rotation = Quaternion.Euler(0, 0, 270);
                StartCoroutine(Move((facingAngle + 1) % 4));
                break;
        }
    }

    public IEnumerator Move(int dir)
    {
        MapManager.instance.fadeAnimator.Play("FadeIn");
        yield return new WaitForSeconds(0.45f);
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
        currentNode = MapManager.instance.map.nodes[gridPos.x, gridPos.y];
        currentNode.OnEnter(0);
        transform.position = currentNode.transform.position;
        yield return new WaitForSeconds(0.383f);
        MenuManager.instance.actionMenu.gameObject.SetActive(true);
    }

    public FeatureType GetFeatureRelative(string dir)
    {
        if (currentNode == null) return FeatureType.Wall;
        switch (dir.ToLower())
        {
            case "forward":
                return currentNode.GetFeature(facingAngle);
            case "back":
                return currentNode.GetFeature((facingAngle + 2) % 4);
            case "left":
                return currentNode.GetFeature((facingAngle + 3) % 4);
            case "right":
                return currentNode.GetFeature((facingAngle + 1) % 4);
        }
        return FeatureType.Wall;
    }

}
