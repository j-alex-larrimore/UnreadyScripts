using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitManagementPrefab : MonoBehaviour {

    public Text unitName;
    public Text unitClass;
    public Text totalLevels;
    public Text classLevels;
    public Text platoonNumber;
    private UnitData unit;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetText(UnitData u)
    {
        unit = u;
        unitClass.text = "" + unit.GetCharClassText();
        totalLevels.text = "Total Levels: " + unit.totalLevels;
        unitName.text = "" + unit.unitName;
        if(unit.platoonNum == -1)
        {
            platoonNumber.text = "UNASSIGNED";
        }
        else
        {
            platoonNumber.text = "Platoon " + unit.platoonNum;
        }        
        classLevels.text = "" + unit.GetCurrentLevel();
    }

    public void MouseClick()
    {
        if (CanvasController.Instance.addingUnit)
        {
            if(CanvasController.Instance.selectedPlatoon.AddUnit(unit, CanvasController.Instance.addingPosition))
            {
                unit.SetPlatoonPosition(CanvasController.Instance.selectedPlatoon.platoonNum, CanvasController.Instance.addingPosition);
                CanvasController.Instance.CloseUnitManagement();

            }
            //CanvasController.Instance.OpenPlatoonInspect(CanvasController.Instance.selectedPlatoon.platoonNum);
        }
        else
        {
            CanvasController.Instance.OpenUnitInspect(unit);
        }

    }
}
