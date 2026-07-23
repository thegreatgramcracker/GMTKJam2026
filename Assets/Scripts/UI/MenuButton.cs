using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class MenuButton : MonoBehaviour
{
    public bool highlighted;
    public UnityEvent onSubmit, onCursorEnter, onCursorExit;
    public MaskableGraphic graphic;
    public Color normalColor = Color.white, highlightedColor;
    public MenuNavigator navigator;

    private void Start()
    {
        if (navigator == null) navigator = GetComponentInParent<MenuNavigator>();
    }

    private void Update()
    {
        if (!highlighted && navigator.currentButton == this)
        {
            highlighted = true;
            onCursorEnter.Invoke();
        }
        if (highlighted && navigator.currentButton != this)
        {
            highlighted = false;
            onCursorExit.Invoke();
        }

        if (highlighted)
        {
            graphic.color = highlightedColor;
        }
        else
        {
            graphic.color = normalColor;
        }
    }

}
