using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    public Player player;
    public BGMManager bgmMan;
    public Transform actionMenu;
    public Transform[] slots;
    public GameObject enemyObject;
    public MenuNavigator selectMenuNavigator;
    public AttackType selectedAttackType;
    public int selectedTargetIndex;
    TextReader reader;
    public float dialogueWaitTime;

    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        reader = MenuManager.instance.dialogueWindow;
    }

    // Update is called once per frame
    void Update()
    {
        selectMenuNavigator.layoutDimensions = new Vector2Int(CurrentEnemies.Count - 1, 0);
    }

    public void InitiateEncounter()
    {
        foreach (Transform slot in slots)
        {
            foreach (Transform child in slot)
            {
                Destroy(child.gameObject);
            }
            slot.gameObject.SetActive(false);
            selectMenuNavigator.menuButtons = new List<MenuButton>();
        }
        foreach (Enemy enemy in CurrentEnemies)
        {
            EnemyObject currentEnemy = Instantiate(enemyObject, slots[enemy.slot]).GetComponent<EnemyObject>();
            slots[enemy.slot].gameObject.SetActive(true);
            currentEnemy.myEnemy = enemy;
            MenuButton currentButton = currentEnemy.GetComponent<MenuButton>();
            selectMenuNavigator.menuButtons.Add(currentButton);
        }
    }

    public void SetAttackType(int attackType) => selectedAttackType = (AttackType)attackType;

    public void StartTurn()
    {
        selectMenuNavigator.enabled = false;
        StartCoroutine(ExecuteTurn());
    }


    public IEnumerator ExecuteTurn()
    {
        Enemy target = CurrentEnemies[selectedTargetIndex];
        if (selectedAttackType == AttackType.Burn)
        {
            reader.ReadText("You used Burn!");
            yield return new WaitUntil( () => !reader.reading);
            yield return new WaitForSeconds(dialogueWaitTime);
            target.currentHeat += player.burnPower;
        }
        else if (selectedAttackType == AttackType.Slice)
        {
            reader.ReadText("You attacked!");
            yield return new WaitUntil(() => !reader.reading);
            yield return new WaitForSeconds(dialogueWaitTime);
            target.armor -= player.attack;
            if (target.armor < 0)
            {
                CurrentEnemies.Remove(target);
                CurrentEnemies.Insert(selectedTargetIndex, new Enemy(target, 2, 1));
                CurrentEnemies.Insert(selectedTargetIndex + 1, new Enemy(target, 2, 2));
                reader.ReadText("Enemy split into two!");
                yield return new WaitUntil(() => !reader.reading);
                yield return new WaitForSeconds(dialogueWaitTime);
            }
        }
        yield return null;


        InitiateEncounter();
        yield return null;


        foreach (Enemy enemy in CurrentEnemies)
        {
            reader.ReadText(enemy.name + " attacks!");
            yield return new WaitUntil(() => !reader.reading);
            yield return new WaitForSeconds(dialogueWaitTime);
            float guardRate = Random.Range(0.5f, 0.8f);
            int armorDamage = Mathf.RoundToInt(enemy.attack * guardRate);
            int healthDamage = Mathf.RoundToInt(enemy.attack * (1f - guardRate));
            player.armor -= armorDamage;
            if (player.armor < 0)
            {
                healthDamage -= player.armor; //adds the negative damage
                player.armor = 0;
                armorDamage = 0;
            }
            player.wax -= healthDamage;

            reader.ReadText("You took " + healthDamage + " and your armor took " + armorDamage + "!");
            yield return new WaitUntil(() => !reader.reading);
            yield return new WaitForSeconds(dialogueWaitTime);
        }
        reader.gameObject.SetActive(false);
        actionMenu.gameObject.SetActive(true);
        yield return null;
    }

    public List<Enemy> CurrentEnemies { get { return MapManager.instance.map.nodes[player.gridPos.x, player.gridPos.y].currentEnemies; } }

}
