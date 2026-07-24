using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TextReader : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public float textSpeed;

    public float currentProgress, maxProgress;
    public bool reading = false;
    GameObject nextMenu;
    private void OnEnable()
    {
        InputSystem.actions.FindAction("Select").performed += ConfirmPressed;
        InputSystem.actions.FindAction("Select").Enable();
    }

    private void OnDisable()
    {
        InputSystem.actions.FindAction("Select").performed -= ConfirmPressed;
        InputSystem.actions.FindAction("Select").Disable();
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (reading)
        {
            currentProgress += textSpeed * Time.deltaTime;
            if (currentProgress > maxProgress)
            {
                currentProgress = maxProgress;
                reading = false;
            }
            for (int i = 0; i < maxProgress; i++)
            {
                if (i < currentProgress)
                {
                    SetCharacterColor(i, new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, 1f));
                }
                else
                {
                    SetCharacterColor(i, new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, 0f));
                }
            }
            textMesh.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

        }
    }
    void SetCharacterColor(int chrIndex, Color color)
    {
        if (chrIndex >= textMesh.textInfo.characterInfo.Length || !textMesh.textInfo.characterInfo[chrIndex].isVisible) return;
        int meshIndex = textMesh.textInfo.characterInfo[chrIndex].materialReferenceIndex;
        int vertIndex = textMesh.textInfo.characterInfo[chrIndex].vertexIndex;
        textMesh.textInfo.meshInfo[meshIndex].colors32[vertIndex] = color;
        textMesh.textInfo.meshInfo[meshIndex].colors32[vertIndex + 1] = color;
        textMesh.textInfo.meshInfo[meshIndex].colors32[vertIndex + 2] = color;
        textMesh.textInfo.meshInfo[meshIndex].colors32[vertIndex + 3] = color;
    }

    void ConfirmPressed(InputAction.CallbackContext ctx)
    {
        if (!reading)
        {
            if (nextMenu != null)
            {
                gameObject.SetActive(false);
                nextMenu.SetActive(true);
            }
            //ReadText(text);
        }
    }

    public void ReadText(string text, GameObject _nextMenu = null)
    {
        gameObject.SetActive(true);
        currentProgress = 0f;
        textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, 0f);
        textMesh.text = text;
        maxProgress = text.Length;
        textMesh.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
        reading = true;
        nextMenu = _nextMenu;
    }
}
