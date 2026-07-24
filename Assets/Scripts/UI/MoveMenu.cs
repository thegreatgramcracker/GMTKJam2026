using UnityEngine;

public class MoveMenu : MonoBehaviour
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnEnable()
    {
        transform.GetChild(0).gameObject.SetActive(MapManager.instance.player.CanTraverse("Forward"));
        transform.GetChild(1).gameObject.SetActive(MapManager.instance.player.CanTraverse("Left"));
        transform.GetChild(2).gameObject.SetActive(MapManager.instance.player.CanTraverse("Right"));
        transform.GetChild(3).gameObject.SetActive(MapManager.instance.player.CanTraverse("Back"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
