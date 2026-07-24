using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EnemyObject : MonoBehaviour
{
    public Enemy myEnemy;
    public TextMeshProUGUI heatIndicator;
    public Image img;
    public RectTransform targetIndicator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        img.sprite = myEnemy.sprite;
        img.color = myEnemy.color;
        heatIndicator.text = myEnemy.currentHeat.ToString();
        heatIndicator.rectTransform.localPosition = myEnemy.enemyBase.divHeatOffsets[myEnemy.divNumber];
        targetIndicator.localPosition = myEnemy.enemyBase.divTargetOffsets[myEnemy.divNumber];
        targetIndicator.sizeDelta = myEnemy.enemyBase.divTargetDimensions[myEnemy.divNumber];
    }

    public void SetTargetIndex()
    {
        BattleManager.instance.selectedTargetIndex = BattleManager.instance.CurrentEnemies.IndexOf(myEnemy);
        BattleManager.instance.StartTurn();
    }
}
