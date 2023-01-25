using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatoonInspectInfoPanel : MonoBehaviour {

    public Text unitName;
    public Text classLevel;
    public Text className;
    public Text totalLevels;
    public Text level;
    public RectTransform healthBar;

    private int platoonNumber;
    private int unitNumber;

    private bool leaderText = false;
    private bool selectedText = false;
    private bool isHero = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(isHero)
        {
            SetHeroText();
        }
        else if (CanvasController.Instance.selectedPlatoon.GetLeader()!= null && CanvasController.Instance.selectedPlatoon.GetLeader().positionNum != unitNumber &&
            CanvasController.Instance.selectedUnit != null && CanvasController.Instance.selectedUnit.positionNum != unitNumber)
        {
            
            SetNormalText();
            
        }else if (leaderText && CanvasController.Instance.selectedPlatoon.GetLeader().positionNum != unitNumber)
        {
            SetSelectedText();
        }else if(selectedText && CanvasController.Instance.selectedUnit.positionNum != unitNumber && leaderText)
        {
            SetLeaderText();
            
        }

       
    }

    public void SetText(int platoonNum, int unitNum)
    {
        int health = 0;
        int maxHealth = 0;

        if (CanvasController.Instance.viewingEnemyPlatoons)
        {
            unitName.text = "" + LevelController.Instance.enemyPlatoons[platoonNum].units[unitNum].unitName;
            className.text = "" + LevelController.Instance.enemyPlatoons[platoonNum].units[unitNum].GetCharClassText();
            totalLevels.text = "" + LevelController.Instance.enemyPlatoons[platoonNum].units[unitNum].totalLevels;
            classLevel.text = "" + LevelController.Instance.enemyPlatoons[platoonNum].units[unitNum].GetCurrentLevel();
            health = LevelController.Instance.enemyPlatoons[platoonNum].units[unitNum].GetCurrentHealth();
            maxHealth = LevelController.Instance.enemyPlatoons[platoonNum].units[unitNum].GetMaxHealth();
            platoonNumber = platoonNum;
            unitNumber = unitNum;
        }
        else
        {
            unitName.text = "" + ArmyData.Instance.platoons[platoonNum].units[unitNum].unitName;
            className.text = "" + ArmyData.Instance.platoons[platoonNum].units[unitNum].GetCharClassText();
            totalLevels.text = "" + ArmyData.Instance.platoons[platoonNum].units[unitNum].totalLevels;
            classLevel.text = "" + ArmyData.Instance.platoons[platoonNum].units[unitNum].GetCurrentLevel();
            health = ArmyData.Instance.platoons[platoonNum].units[unitNum].GetCurrentHealth();
            maxHealth = ArmyData.Instance.platoons[platoonNum].units[unitNum].GetMaxHealth();
            platoonNumber = platoonNum;
            unitNumber = unitNum;
            if (ArmyData.Instance.platoons[platoonNum].units[unitNum].specialCharNum == 1)
            {
                isHero = true;
            }
        }

        float healthPercent = ((float)health / maxHealth) * 100;
        healthBar.sizeDelta = new Vector2(healthPercent, healthBar.sizeDelta.y);

    }

    public void SetLeaderText()
    {
        if (!isHero)
        {
            SetColors(Color.cyan);
        }
        else
        {
            SetHeroText();
        }

        
        leaderText = true;
        selectedText = false;
    }

    public void SetSelectedText()
    {
        SetColors(Color.magenta);
        selectedText = true;
    }

    public void SetNormalText()
    {
        if (!isHero)
        {
            SetColors(Color.white);
        }
        else
        {
            SetHeroText();
        }
        
        leaderText = false;
        selectedText = false;
    }

    public void SetHeroText()
    {
        if(CanvasController.Instance.selectedUnit != null && CanvasController.Instance.selectedUnit.positionNum == unitNumber)
        {
            SetSelectedText();
        }
        else
        {
            SetColors(Color.green);
            selectedText = false;
        }



    }

    public void SetColors(Color c)
    {
        unitName.color = new Color32(255, 135, 0, 255);
        className.color = c;
        totalLevels.color = c;
        classLevel.color = c;
        level.color = c;
    }

    public void MouseClick()
    {
        //Debug.Log("Info box clicked");
         //CanvasController.Instance.OpenPlatoonInspect(platoonNumber);
         if (CanvasController.Instance.viewingEnemyPlatoons)
         {
             CanvasController.Instance.selectedUnit = LevelController.Instance.enemyPlatoons[platoonNumber].units[unitNumber];
             CanvasController.Instance.ShowPlatoonUnitOptionsPanel();
             SetSelectedText();
         }
         else
         {
             if (CanvasController.Instance.selectingLeader && ArmyData.Instance.platoons[platoonNumber].units[unitNumber].GetClassSize() == 1)
             {
                 SetLeaderText();
                 ArmyData.Instance.platoons[platoonNumber].SetLeader(ArmyData.Instance.platoons[platoonNumber].units[unitNumber]);
                 CanvasController.Instance.ShowChangeLeaderButton();
             }
             else if (!CanvasController.Instance.selectingLeader)
             {
                 CanvasController.Instance.selectedUnit = ArmyData.Instance.platoons[platoonNumber].units[unitNumber];
                 CanvasController.Instance.ShowPlatoonUnitOptionsPanel();
                 SetSelectedText();
             }
         }
    }
}
