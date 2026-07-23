using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System.Linq;

public class MenuNavigator : MonoBehaviour
{
    [System.Serializable]
    public class ButtonAction
    {
        public string actionName;
        public bool isNavigation;
        public int desiredNavValue;
        
        public UnityEvent response;
    }

    public InputActionAsset controls;

    public List<MenuButton> menuButtons;
    [HideInInspector]
    int currentSelectedIndex;
    public bool loopingX, loopingY;
    [HideInInspector]
    public MenuButton currentButton;
    public Vector2Int layoutDimensions;
    public Vector2Int selectedPosition = new Vector2Int();
    AudioSource audioSource;
    public AudioClip navigateSound;
    public List<ButtonAction> buttonActions;

    
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        //controls.Enable();
        controls.FindAction("Move").performed += Move_performed;
    }

    private void Move_performed(InputAction.CallbackContext obj)
    {
        if (!menuButtons.Any()) return;
        Vector2Int previousPos = selectedPosition;
        Vector2Int posChange = new Vector2Int(Mathf.RoundToInt(obj.ReadValue<Vector2>().x), Mathf.RoundToInt(obj.ReadValue<Vector2>().y));


        if (loopingX)
        {
            if (posChange.x + selectedPosition.x < 0)
            {
                selectedPosition.x = layoutDimensions.x;
            }
            else if (posChange.y + selectedPosition.x > layoutDimensions.x)
            {
                selectedPosition.x = 0;
            }
            else
            {
                selectedPosition.x += posChange.x;
            }
                
        }
        else
        {
            selectedPosition.x = Mathf.Clamp(selectedPosition.x + posChange.x, 0, layoutDimensions.x);
        }

        if (loopingY)
        {
            if (posChange.y + selectedPosition.y < 0)
            {
                selectedPosition.y = layoutDimensions.y;
            }
            else if (posChange.y + selectedPosition.y > layoutDimensions.y)
            {
                selectedPosition.y = 0;
            }
            else
            {
                selectedPosition.y += posChange.y;
            }
        }
        else
        {
            selectedPosition.y = Mathf.Clamp(selectedPosition.y + posChange.y, 0, layoutDimensions.y);
        }
        if (navigateSound != null && previousPos != selectedPosition)
        {
            audioSource.PlayOneShot(navigateSound);
        }
    }

    private void OnDisable()
    {
        currentButton = null;
        controls.FindAction("Move").performed -= Move_performed;
    }

    // Update is called once per frame
    void Update()
    {
        if (menuButtons.Any())
        {
            
            currentSelectedIndex = PositionToIndex(selectedPosition);
            //if the menu index is outside of the menu range, go to last menu item
            if (menuButtons.Count < currentSelectedIndex + 1)
            {
                currentSelectedIndex = menuButtons.Count - 1;
            }
            currentButton = menuButtons[currentSelectedIndex];
        }

        if (currentButton && controls.FindAction("Select").WasPressedThisFrame())
        {
            currentButton.onSubmit.Invoke();
        }

        foreach (ButtonAction action in buttonActions)
        {
            if (InputSystem.actions.FindAction(action.actionName).WasPressedThisFrame())
            {
                if (action.isNavigation)
                {
                    if (Mathf.RoundToInt(InputSystem.actions.FindAction(action.actionName).ReadValue<float>()) == action.desiredNavValue)
                    {
                        action.response.Invoke();
                        break;
                    }
                }
                action.response.Invoke();
                break;
            }
        }

    }

    public void ResetPosition()
    {
        selectedPosition = Vector2Int.zero;
        //currentSelectedIndex = 0;
    }

    public void SetPositionLast()
    {
        selectedPosition = layoutDimensions;
        //currentSelectedIndex = menuButtons.Count - 1;
    }

    Vector2Int GetIndexPosition(int index)
    {
        Vector2Int pos = new Vector2Int(index % (layoutDimensions.x + 1), index / (layoutDimensions.x + 1));

        return pos;
    }

    int PositionToIndex(Vector2Int pos)
    {
        return (pos.x + (pos.y * (layoutDimensions.x + 1)));
    }

    Vector2 CalculateBounds()
    {
        //cycle through each button, record the lower left and upper right position of the button, check if the 
        //lower left is lowest on x or y, record position if so, check upepr right if highest on x or y, record if so.
        //find the square with upper right at the highest x and y, and lower left at lowest x and y.
        Vector2 bounds = Vector2.zero;

        return bounds;
    }

    
    



}
