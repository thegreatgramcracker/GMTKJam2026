using UnityEngine;

public class MoveMenu : MonoBehaviour
{
    public MapManager mapMan;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnEnable()
    {
        transform.GetChild(0).gameObject.SetActive(mapMan.player.CanTraverse("Forward"));
        transform.GetChild(1).gameObject.SetActive(mapMan.player.CanTraverse("Left"));
        transform.GetChild(2).gameObject.SetActive(mapMan.player.CanTraverse("Right"));
        transform.GetChild(3).gameObject.SetActive(mapMan.player.CanTraverse("Back"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
