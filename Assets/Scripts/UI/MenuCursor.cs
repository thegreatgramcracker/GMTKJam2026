using UnityEngine;
using UnityEngine.UI;

public class MenuCursor : MonoBehaviour
{

    public MenuNavigator menu;
    public Vector2 offset;
    public float speed;
    RectTransform rectTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (menu != null && menu.currentButton != null)
        {
            rectTransform.anchoredPosition = Vector2.MoveTowards(
                rectTransform.anchoredPosition,
                menu.currentButton.GetComponent<RectTransform>().anchoredPosition + offset,
                speed * Time.deltaTime);
        }
    }
}
