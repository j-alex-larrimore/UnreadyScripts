using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatoonInfoPanel : MonoBehaviour {

    public Text leaderName;
    public Text leaderClass;
    public Text totalLevels;
    public Text availableSpace;
    public Image leaderImage;
    private int platoonNumber;
    
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetText(int platoonNum)
    {
        if (CanvasController.Instance.viewingEnemyPlatoons)
        {
            leaderClass.text = "" + LevelController.Instance.enemyPlatoons[platoonNum].GetLeader().GetCharClassText();
            totalLevels.text = "" + LevelController.Instance.enemyPlatoons[platoonNum].TotalLevels();
            leaderName.text = "" + LevelController.Instance.enemyPlatoons[platoonNum].GetLeader().unitName;
            platoonNumber = platoonNum;
            if(LevelController.Instance.enemyPlatoons[platoonNum].availableSpace > 0)
            {
                availableSpace.text = "FREE SPACE: " + LevelController.Instance.enemyPlatoons[platoonNum].availableSpace;
            }
            else
            {
                availableSpace.text = "";
            }
        }
        else
        {
            leaderClass.text = "" + ArmyData.Instance.platoons[platoonNum].GetLeader().GetCharClassText();
            totalLevels.text = "" + ArmyData.Instance.platoons[platoonNum].TotalLevels();
            leaderName.text = "" + ArmyData.Instance.platoons[platoonNum].GetLeader().unitName;
            platoonNumber = platoonNum;
            if (ArmyData.Instance.platoons[platoonNum].availableSpace > 0)
            {
                availableSpace.text = "FREE SPACE: " + ArmyData.Instance.platoons[platoonNum].availableSpace;
            }
            else
            {
                availableSpace.text = "";
            }
        }
        //leaderImage = CanvasController.Instance.profilePics[ArmyData.Instance.platoons[platoonNum].GetLeader().GetCharClass()];
    }

    private void OnMouseDown()
    {
        CanvasController.Instance.OpenPlatoonInspect(platoonNumber);
    }

    public void MouseClick()
    {
        CanvasController.Instance.OpenPlatoonInspect(platoonNumber);
    }
}
