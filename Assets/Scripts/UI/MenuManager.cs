using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    public MenuNavigator actionMenu;
    public RectTransform transition;
    public TextReader dialogueWindow;

    private void Awake()
    {
        instance = this;
    }

}
