using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitInspectionPanel : MonoBehaviour {

    public GameObject changeNamePanel;
    public GameObject changeNameInputField;
    public GameObject statsDescriptionPanel;

    public Text unitName;
    public Text totalLevels;
    public Text className;
    public Text classLevel;
    public Text platoon;

    public Text agility;
    public Text constitution;
    public Text dexterity;
    public Text intelligence;
    public Text speed;
    public Text strength;
    public Text wisdom;

    public UnitData unit;
    private float hoverTimer = 1.5f;
    private float hoverTime;
    private bool hovering = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (CanvasController.Instance.changingName && Input.GetKeyDown(KeyCode.Return))
        {
            SubmitNameChange();
        }

        if (hovering)
        {
            hoverTime += Time.deltaTime;
        }

        if(hovering && hoverTimer < hoverTime)
        {
            ShowStatDescriptions();
        }
	}

    public void SetText(UnitData u)
    {
        unit = u;
        unitName.text = "" + u.unitName;
        className.text = "" + u.GetCharClassText();
        totalLevels.text = "Total Levels: " + u.totalLevels;
        classLevel.text = "Level " + u.GetCurrentLevel();
        platoon.text = "Platoon: " + u.platoonNum;
        agility.text = "Agility: " + u.stats[3];
        constitution.text = "Constitution: " + u.stats[4];
        dexterity.text = "Dexterity: " + u.stats[2];
        intelligence.text = "Intelligence: " + u.stats[6];
        speed.text = "Speed: " + u.stats[1];
        strength.text = "Strength: " + u.stats[0];
        wisdom.text = "Wisdom: " + u.stats[5];
    }

    public void ChangeName()
    {
        changeNamePanel.SetActive(true);
        Debug.Log("Changing name for " + CanvasController.Instance.selectedUnit.unitName);
        changeNameInputField.GetComponent<InputField>().placeholder.GetComponent<Text>().text = CanvasController.Instance.selectedUnit.unitName;
        CanvasController.Instance.changingName = true;
    }

    public void CloseChangeNamePanel()
    {
        CanvasController.Instance.changingName = false;
        changeNamePanel.SetActive(false);
    }

    public void SubmitNameChange()
    {
        if (changeNameInputField.GetComponent<InputField>().text.Length >= 3 && changeNameInputField.GetComponent<InputField>().text.Length <= 13)
        {
            unit.SetName(changeNameInputField.GetComponent<InputField>().text);
            CloseChangeNamePanel();
            unitName.text = "" + unit.unitName;
        }

    }

    public void StatsHover()
    {
        hovering = true;
        Debug.Log("Hovering on unit inspect!");
    }

    public void StatsNotHover()
    {
        statsDescriptionPanel.SetActive(false);
        hovering = false;
        hoverTime = 0f;
    }

    public void StartHover()
    {
        //hovering = true;
        ClassDescriptionPanel.Instance.CurrentClassHover();
    }

    public void StopHover()
    {
        //hovering = false;
        ClassDescriptionPanel.Instance.StopCurrentClassHover();
        //Debug.Log("Mouse Exit: " + classNum);
    }

    public void ShowStatDescriptions()
    {
        statsDescriptionPanel.SetActive(true);
    }

    public void ClassChange()
    {
        if(unit.GetCharType() == 1)
        {
            CanvasController.Instance.OpenHumanChange();
        }
        else if (unit.GetCharType() == 2)
        {
            CanvasController.Instance.OpenAnimalChange();
        }
        else if (unit.GetCharType() == 3)
        {
            CanvasController.Instance.OpenMythChange();
        }
    }
}
