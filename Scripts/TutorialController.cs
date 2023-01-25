using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour {

    public static TutorialController Instance;

    public GameObject tutorialScreen;
    //public Sprite[] storyPics;
    //public GameObject storyImage;
    //private GameObject textPanel;
    public Text tutorialText;
    //private Text taglineText;
    public GameObject[] tutorialPanels;
    /*public GameObject storyPanelIntro;
    public GameObject storyPanel1Intro;
    public GameObject storyPanel2;
    public GameObject storyPanel3;
    public GameObject storyPanel4;
    public GameObject storyPanel5;
    public GameObject storyPanel6;
    public GameObject storyPanel7;
    public GameObject storyPanel8;*/
    private double tutorial = 0;
    public bool tutorialShown = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            DestroyImmediate(gameObject);
            return;
        }

        Instance = this;

    }

    // Use this for initialization
    void Start()
    {
        tutorialScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyUp(KeyCode.Space))
        {
            MoveTutorial();
        }
         if (Input.GetKeyUp(KeyCode.Escape))
         {
             CloseStory();
         }*/
    }

    public void ShowTutorial(int tutorialNum)
    {
        tutorialScreen.SetActive(true);
        BattleController.Instance.PauseBattle();
        GameController.Instance.ArmyPause();
        switch (tutorialNum)
        {
            case 0:
                BattleTutorial();
                break;
            case 1:
                GlobalMapTutorial();
                break;
            case 2:
                LevelMapTutorial();
                break;
            default:
                break;
        }

    }

    public void CloseStory()
    {
        BattleController.Instance.UnpauseBattle();
        GameController.Instance.ArmyUnpause();
        ClosePanels();
        tutorialScreen.SetActive(false);
    }

    public void ClosePanels()
    {
        foreach (GameObject panel in tutorialPanels)
        {
            panel.SetActive(false);
        }
    }

    public void SetBackground(int backNum)
    {
        //storyImage.GetComponent<Image> ().sprite = storyPics [backNum];
        //string panelName = "storyPanel" + backNum + 1;

    }

    public void MoveTutorial()
    {

        Debug.Log("Skip tutorial " + tutorial);
        if (tutorial == 0.0)
        {
            BattleTutorialB();
        }
        else if (tutorial == 0.1)
        {
            BattleTutorialC();
        }
        else if (tutorial == 0.2)
        {
            BattleTutorialD();
        }
        else if (tutorial == 0.3)
        {
            BattleTutorialE();
        }
        else if (tutorial == 0.4)
        {
            BattleTutorialF();
        }
        else if (tutorial == 1.0)
        {
            GlobalMapTutorialB();
        }
        else if (tutorial == 1.1)
        {
            GlobalMapTutorialC();
        }
        else if (tutorial == 1.2)
        {
            GlobalMapTutorialD();
        }
        else if (tutorial == 1.3)
        {
            GlobalMapTutorialE();
        }
        else if (tutorial == 1.4)
        {
            GlobalMapTutorialF();
        }
        else if (tutorial == 1.5)
        {
            GlobalMapTutorialG();
        }
        else if (tutorial == 2.0)
        {
            LevelMapTutorialB();
        }
        else if (tutorial == 2.1)
        {
            LevelMapTutorialC();
        }
        else if (tutorial == 2.2)
        {
            LevelMapTutorialD();
        }
        else if (tutorial == 2.3)
        {
            LevelMapTutorialE();
        }
        else
        {
            CloseStory();
        }
    }

    public void BattleTutorial()
    {
        tutorialText.text = "Lieutenant Vafer: I know you've trained for battle, but now that it's here I want to remind you of how to survey your troops. Remember, once the fight starts" +
            " your units will do what you have taught them to do. You can send a signal for retreat but it would be impossible to micromanage every unit in your army.";
        tutorial = 0.0;
        tutorialPanels[0].SetActive(false);
        tutorialPanels[1].SetActive(true);
    }

    public void BattleTutorialB()
    {
        tutorialText.text = "Lieutenant Vafer: Above each unit you can keep track of their health, experience, and turn meter. The Green and red bar keeps track of how much health your unit has" +
            "remaining. The yellow bar in the middle will let you know how much your units are learning. A good commander makes sure units are given jobs in which they can still learn. " +
            "Finally, the blue bar at the bottom lets you know how close each unit is to moving. Faster units will get to take more turns so speed often wins or loses battles.";
        tutorial = 0.1;
        tutorialPanels[1].SetActive(false);
        tutorialPanels[2].SetActive(true);
    }

    public void BattleTutorialC()
    {
        tutorialText.text = "Lieutenant Vafer: It is also important to pay attention to the details. If you look at the top of the screen, you can keep track of key information. In the top left you" +
            " can see the name, class level and total level of a unit. In the top right, you can see what skill that unit is using.";
        tutorial = 0.2;
        tutorialPanels[2].SetActive(false);
        tutorialPanels[3].SetActive(true);
    }

    public void BattleTutorialD()
    {
        tutorialText.text = "Lieutenant Vafer: In the middle, you can see who was the target of the skill and how much health they lost. On the bottom, you can see if that unit also" +
            " received a status effect.";
        tutorial = 0.3;
        tutorialPanels[3].SetActive(false);
        tutorialPanels[4].SetActive(true);
    }

    public void BattleTutorialE()
    {
        tutorialText.text = "Lieutenant Vafer: If you ever forget what status effects do you can look them up in the battle menu or the level menu.";
        tutorial = 0.4;
        tutorialPanels[4].SetActive(false);
        tutorialPanels[5].SetActive(true);
    }

    public void BattleTutorialF()
    {
        tutorialText.text = "Lieutenant Vafer: The Battle Menu also lets you do things like pause the battle to catch your breath, pause the level map when the battle ends, adjust your audio levels " +
            "or retreat if you need to adjust your strategy. Now that we've reviewed how battles work let's watch our platoon protect our home base.";
        tutorial = 0.5;
        tutorialPanels[5].SetActive(false);
        tutorialPanels[6].SetActive(true);
    }

    public void GlobalMapTutorial()
    {
        tutorialText.text = "Lieutenant Vafer: Now that we are on our own you need to know how to manage your army. This can be done through the global map menu which is under the button in the top " +
            "left of your screen.";
        tutorial = 1.0;
        tutorialPanels[6].SetActive(false);
        tutorialPanels[7].SetActive(true);
    }

    public void GlobalMapTutorialB()
    {
        tutorialText.text = "Lieutenant Vafer: You have the option of saving your army's progress or adjusting the sound. You also have the ability to interact with your units and platoons.";
        tutorial = 1.1;
        tutorialPanels[7].SetActive(false);
        tutorialPanels[8].SetActive(true);
    }

    public void GlobalMapTutorialC()
    {
        tutorialText.text = "Lieutenant Vafer: In Platoon View you can see the leaders and total levels of your platoons. Click on any platoon to inspect it.";
        tutorial = 1.2;
        tutorialPanels[8].SetActive(false);
        tutorialPanels[9].SetActive(true);
    }

    public void GlobalMapTutorialD()
    {
        tutorialText.text = "Lieutenant Vafer: In Platoon Inspect you can see and change your platoon's formation. The right of the formation is the side facing the enemy platoons while the left of the formation" +
            " is the back lines. You can click on units to inspect or remove them and click on any add unit spaces to fill the platoon. Platoons have a maximum capacity of 13. The change leader button allows" +
            "you to determine who represents a unit in the field of battle.";
        tutorial = 1.3;
        tutorialPanels[9].SetActive(false);
        tutorialPanels[10].SetActive(true);
    }

    public void GlobalMapTutorialE()
    {
        tutorialText.text = "Lieutenant Vafer: In Unit View you can see all of your units outside of their platoons. You can organize the units by platoon or level number in ascending or" +
            " descending order. You can also hide all units that are currently assigned to platoons. There are small, medium, and large units and units can ONLY have their size changed after being removed" +
            " from a platoon in the Unit View screen.";
        tutorial = 1.4;
        tutorialPanels[10].SetActive(false);
        tutorialPanels[11].SetActive(true);
    }

    public void GlobalMapTutorialF()
    {
        tutorialText.text = "Lieutenant Vafer: If you click on a unit in Unit View or in Platoon Inspect you can inspect the unit. Unit Inspect allows you to see the level, class, name and statistics of a unit." +
            " If you don't like a unit's name you can change that by clicking on the icon in the top right. If you click on change class you can see the classes a specific unit has unlocked. Human units can only " +
            "learn from a class until they reach level 10 in that class. Other units can learn from a class until level 30. Any experience gained after hitting the maximum level of a class is wasted. Classes can also" +
            " be changed within levels through the level menu.";
        tutorial = 1.5;
        tutorialPanels[11].SetActive(false);
        tutorialPanels[12].SetActive(true);
    }

    public void GlobalMapTutorialG()
    {
        tutorialText.text = "Lieutenant Vafer: You can also hover over a class on either the class change menu or the unit inspect menu to get details on a class. " +
            "The details panel will tell you how your unit's stastics will improve by gaining a level in that class and the skills available to that class. Units will always use the most " +
            "advanced skill available to them that is not on cooldown. First level skills have no cooldowns. Second level skills have a two turn cooldown and third level skills have a five turn" +
            " cooldown. That is all the basics of the global map. If you have any other questions I'm always here!";
        tutorial = 1.6;
        tutorialPanels[12].SetActive(false);
        tutorialPanels[13].SetActive(true);
    }

    public void LevelMapTutorial()
    {
        tutorialText.text = "Lieutenant Vafer: Now that you are in charge of the entire battle we should go over how you can win, lose, and control your platoons. In combat, if a normal unit is reduced to zero health" +
            " it is rushed back to the medical tents and unavailable for the rest of the battle. It will also lose any progress it made towards its next level." +
            " If you should ever be reduced to zero health and need to leave the field our entire army will retreat and the battle" +
            " will be lost. Also, if our base of operations is ever captured by the enemy we will lose the battle. We can win by either capturing the enemy base or eliminating all enemy platoons from the field.";
        tutorial = 2.0;
        tutorialPanels[13].SetActive(false);
        tutorialPanels[14].SetActive(true);
    }

    public void LevelMapTutorialB()
    {
        tutorialText.text = "Lieutenant Vafer: To capture bases and engage in combat you can need to control your platoons. You can select a platoon by either clicking on that platoon or by hitting the tab button until " +
            "you have the correct platoon leader's name in the active platoon box. You can right click on destinations on the map to send the selected platoon to that location.";
        tutorial = 2.1;
        tutorialPanels[14].SetActive(false);
        tutorialPanels[15].SetActive(true);
    }

    public void LevelMapTutorialC()
    {
        tutorialText.text = "Lieutenant Vafer: For other tasks you can select the menu button. On this screen you can adjust your audio, review the impacts of status effects, pause the global map and inspect your platoons." +
            " While within a battle, you are also unable to add or remove units. You are still able to adjust the classes of your units. " +
            "If you want time to evaluate your battlefield you can close the menu while leaving the game paused. If you are ready to go you can close the menu with the unpause button.";
        tutorial = 2.2;
        tutorialPanels[15].SetActive(false);
        tutorialPanels[16].SetActive(true);
    }

    public void LevelMapTutorialD()
    {
        tutorialText.text = "Lieutenant Vafer: The active platoons button does not show you any of your platoons that have left the field of battle. " +
            "If you click on enemy platoons you can see your opposition. Even platoons that are currently hiding on the map are visible through the enemy platoon view so you might want to check here before" +
            " leaving your base undefended.";
        tutorial = 2.3;
        tutorialPanels[16].SetActive(false);
        tutorialPanels[17].SetActive(true);
    }

    public void LevelMapTutorialE()
    {
        tutorialText.text = "Lieutenant Vafer: If you inspect an enemy platoon it looks almost the same as inspecting one of your own platoons. The only real difference is that the front units in an enemy's platoons are to the" +
            " left of the platoon inspection screen and the back line is on the right. I think that should be enough of a refresher for now, let's get out there and take back our farms!";
        tutorial = 2.4;
        tutorialPanels[17].SetActive(false);
        tutorialPanels[18].SetActive(true);
    }
}
