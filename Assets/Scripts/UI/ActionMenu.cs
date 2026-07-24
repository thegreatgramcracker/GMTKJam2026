using TMPro;
using UnityEngine;

public class ActionMenu : MonoBehaviour
{
    public MenuButton moveButton;
    public MenuButton checkButton;
    public MenuButton fightButton;
    MenuNavigator navigator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        navigator = GetComponent<MenuNavigator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (BattleManager.instance.CurrentEnemies.Count > 0)
        {
            checkButton.gameObject.SetActive(false);
            fightButton.gameObject.SetActive(true);
            navigator.menuButtons[1] = fightButton;
            moveButton.GetComponent<TextMeshProUGUI>().text = "Flee";
        }
        else
        {
            checkButton.gameObject.SetActive(true);
            fightButton.gameObject.SetActive(false);
            navigator.menuButtons[1] = checkButton;
            moveButton.GetComponent<TextMeshProUGUI>().text = "Move";
        }
    }
}
