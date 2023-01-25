using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {

    public static CanvasController Instance;

    public GameObject globalMap;
    public GameObject map;
    public GameObject difficultyScreen;
    public GameObject continueScreen;
    public GameObject saveRetryScreen;
    public GameObject gameLostScreen;
    public GameObject hardGameLostScreen;
    public GameObject loginScreen;
    public GameObject settingsScreen;

    public GameObject scrollView;
    public GameObject player;

    //Buttons to hide when going through story
    public GameObject globalMenuButton;
    public GameObject battleReadyButton;
    public GameObject battleMenuButton; 

    public GameObject armyOptionsMenu;
    public GameObject levelOptionsMenu;
    public GameObject activePlatoonsView;
    public GameObject allyPlatoonButton;
    public GameObject enemyPlatoonButton;
    public GameObject removeUnitButton;
    public GameObject platoonManagementPanel;
    public GameObject unitManagementPanel;
    public GameObject platoonInspectionPanel;
    public GameObject unitInspectionPanel;
    public GameObject sortLevelsHighLowButton;
    public GameObject sortLevelsLowHighButton;
    public GameObject sortPlatoonLowHighButton;
    public GameObject sortPlatoonHighLowButton;
    public GameObject hideAssignedUnitsButton;
    public GameObject showAssignedUnitsButton;

    public GameObject statusCanvas;
    
    /*public GameObject femaleHumanClassChangePanel1;
    public GameObject maleHumanClassChangePanel1;
    public GameObject femaleHumanClassChangePanel2;
    public GameObject maleHumanClassChangePanel2;
    public GameObject femaleHumanClassChangePanel3;
    public GameObject maleHumanClassChangePanel3;*/
    public GameObject humanClassChangePanel1;
    public GameObject humanClassChangePanel2;
    public GameObject humanClassChangePanel3;
    public GameObject mythClassChangePanel;
    public GameObject mythClassChangePanel2;
    public GameObject animalClassChangePanel;

    public GameObject femaleHumanClassChangeMenu1;
    public GameObject maleHumanClassChangeMenu1;
    public GameObject femaleHumanClassChangeMenu2;
    public GameObject maleHumanClassChangeMenu2;
    public GameObject femaleHumanClassChangeMenu3;
    public GameObject maleHumanClassChangeMenu3;
    public GameObject mythClassChangeMenu;
    public GameObject mythClassChangeMenu2;
    public GameObject animalClassChangeMenu;

    public GameObject changeLeaderButton;
    public GameObject platoonUnitOptionsPanel;
    public GameObject classChangeCamera;

    public GameObject platoonPanelPrefab;
    public GameObject platoonInspectUnitInfoBoxPrefab;
    public GameObject platoonInspectAddUnitPrefab;
    public GameObject unitManagementPrefab;

    public GameObject[] humanClassChangeButtons = new GameObject[59];
    public GameObject[] animalClassChangeButtons = new GameObject[10];
    public GameObject[] mythClassChangeButtons = new GameObject[14];

    public GameObject[] maleHumanClassChangeObjects = new GameObject[59];
    public GameObject[] femaleHumanClassChangeObjects = new GameObject[59];
    public GameObject[] animalClassChangeObjects = new GameObject[10];
    public GameObject[] mythClassChangeObjects = new GameObject[14];

    public GameObject[] humanClassChangeReqs = new GameObject[59];
    public GameObject[] animalClassChangeReqs = new GameObject[10];
    public GameObject[] mythClassChangeReqs = new GameObject[14];

    public GameObject[] humanText = new GameObject[59];
    public GameObject[] animalText = new GameObject[10];
    public GameObject[] mythText = new GameObject[14];
    public GameObject classDescriptionCanvas;
    public GameObject classChangeErrorMessage;

    public GameObject heroWarningText;
    public GameObject saveText;

    public GameObject pauseText;

    public GameObject classDescriptionPanel;
    public GameObject[] classDescriptionPanels;

    public GameObject[] platoonUnitPanels = new GameObject[25];
    public List<GameObject> unitManagementPrefabs = new List<GameObject>();

    public Platoon selectedPlatoon;
    public UnitData selectedUnit;
    public Text selectedPlatoonText;

    public Transform unitScrollViewContent;

    public bool selectingLeader = false;
    public bool addingUnit = false;
    public bool changingName = false;
    public int addingPosition = -1;
    public bool inGlobalMenu = true;
    public GameObject levelGUI;

    public bool viewingEnemyPlatoons = false;
    public bool viewingActivePlatoons = false;
    public bool inspectingThroughHover = false;
    public bool viewingAllUnits = false;
    public GameObject classChangeButton;

    public GameObject battleGUI;
    public Text battleUnitText;
    public Text battleSkillText;
    public Text battleBattleText;
    public Text battleStatusText;
    public GameObject battleOptionsMenu;
    public bool battlePaused = false;

    public Text armiesPausedText;
    public Text armiesPausedTextBattle;

    private bool hidingAssignedUnits = false;
    private int unitSortingMethod = 0;

    //Version 2.0
    public GameObject skillButton1;
    public GameObject skillButton2;
    public GameObject skillButton3;
    public GameObject readyButton;

    public Text restartCount;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            DestroyImmediate(this);
            return;
        }

        Instance = this;
        //DontDestroyOnLoad(this);
    }

    public void GlobalPlayerAnimate(int buttonNum)
    {        

        string triggerString = "";
        switch (GlobalMapController.Instance.clickedButtonNum)
        {
            case 1:
                triggerString = "Level1";
                break;
            case 3:
                triggerString = "Level3";
                break;
            case 4:
                triggerString = "Level4";
                break;
            case 5:
                triggerString = "Level5";
                break;
            case 7:
                triggerString = "Level7";
                break;
            case 8:
                triggerString = "Level8";
                break;
            case 9:
                triggerString = "Level9";
                break;
            case 10:
                triggerString = "Level10";
                break;
            case 11:
                triggerString = "Level11";
                break;
            /*case 11:
                triggerString = "Level11B";
                break;*/
            case 12:
                triggerString = "Level12";
                break;
            case 13:
                triggerString = "Level13";
                break;
            case 14:
                triggerString = "Level14";
                break;
            case 15:
                triggerString = "Level15";
                break;
            /*case 16:
                triggerString = "Level15B";
                break;*/
            case 16:
                triggerString = "Level16";
                break;
            case 17:
                triggerString = "Level17";
                break;
            case 18:
                triggerString = "Level18";
                break;
            case 19:
                triggerString = "Level19";
                break;
            case 20:
                triggerString = "Level20";
                break;
            case 21:
                triggerString = "Level21";
                break;
            case 22:
                triggerString = "Level22";
                break;
            case 23:
                triggerString = "Level23";
                break;
            case 24:
                triggerString = "Level24";
                break;
            case 25:
                triggerString = "Level25";
                break;
            case 27:
                triggerString = "Level27";
                break;
            case 28:
                triggerString = "Level28";
                break;
            case 30:
                triggerString = "Level30";
                break;
            default:
                break;
        }
        player.GetComponent<Animator>().SetTrigger(triggerString);

        //FOR GLOBAL MAP TESTING ONLY
        //GameController.Instance.levelsCompleted++;
    }

    /*public void LoggedIn(bool continuePossible)
    {
        loginScreen.SetActive(false);
        if (continuePossible)
        {
            NewOrContinue();
        }
        else
        {
            PickDifficulty();
        }
    }*/

    public void DisableLogin()
    {
        loginScreen.SetActive(false);
        continueScreen.SetActive(true);
        //globalMap.SetActive(true);
        map.GetComponent<RectTransform>().localPosition = new Vector2(0f, 0f);
    }
   
    public void NewOrContinue()
    {
        continueScreen.SetActive(true);
    }

    public void PickDifficulty()
    {
        continueScreen.SetActive(false);
        difficultyScreen.SetActive(true);
    }

    public void NewGame()
    {
        PickDifficulty();
    }

    public void ContinueGame()
    {
        continueScreen.SetActive(false);
        //DataManagement.Instance.Continue();

        LocalSave.Instance.Continue();
        /*if (GameController.Instance.levelsCompleted == 0)
        {
            PickDifficulty();
        }
        else
        {
            globalMap.SetActive(true);
        }*/
    }

    public void EasyDifficulty()
    {
        GameController.Instance.EasyDifficulty();
        difficultyScreen.SetActive(false);
        globalMap.SetActive(true);
        BattleController.Instance.EasyMode();
    }

    public void HardDifficulty()
    {
        GameController.Instance.HardDifficulty();
        difficultyScreen.SetActive(false);
        globalMap.SetActive(true);
    }

    public void LevelWon()
    {

        GameController.Instance.towns.Clear();
        GameController.Instance.ambush.Clear();

        /*if (GameController.Instance.difficulty)
        {
            saveRetryScreen.SetActive(true);
        }
        else
        {*/
            SaveGameEndLevel();
        //}
        SoundController.Instance.LevelWon();
    }

    public void SaveGame()
    {
        //DataManagement.Instance.SaveProgress();
        LocalSave.Instance.SaveProgress();
    }

    public void MidLevelSaveGame()
    {
        LocalSave.Instance.MidLevelSaveProgress();
    }

    public void SaveGameEndLevel()
    {
        if (GameController.Instance.levelsCompleted < LevelController.Instance.levelNum)
        {
            LevelController.Instance.SaveGameLevelWon();
            GameController.Instance.levelsCompleted++;
        }else if (GameController.Instance.levelsCompleted >= 30 && GameController.Instance.difficulty)
        {
            GameController.Instance.levelsCompleted++;
        }
        saveRetryScreen.SetActive(false);
        //DataManagement.Instance.SaveProgress();
        LocalSave.Instance.SaveProgress();
        GameController.Instance.LevelOver();
    }

    public void SaveGameHardLoss()
    {
        //GameController.Instance.levelsCompleted = 1;
        GameController.Instance.restarts++;
        Debug.Log("Savegamehardloss restarts " + GameController.Instance.restarts);
        hardGameLostScreen.SetActive(false);
        LocalSave.Instance.SaveProgress();
        GameController.Instance.LevelOver();
    }

    public void Retry()
    {
        saveRetryScreen.SetActive(false);
        gameLostScreen.SetActive(false);
        hardGameLostScreen.SetActive(false);
        //GameController.Instance.inBattle = false;
        //BattleController.Instance.ClearHealthBars();
        //CanvasController.Instance.CloseBattleGUI();


        //Reload level data
        //Reload scene
        if (GameController.Instance.levelsCompleted == 0)
        {
            ArmyData.Instance.ClearArmy();
            GameController.Instance.Level1Lost();
        }
        else if(GameController.Instance.difficulty)
        {
            //DataManagement.Instance.GetUserDataRetry();
            LocalSave.Instance.GetUserDataRetry();
        }
        else
        {
            GameController.Instance.LevelOver();
        }
        
    }

    public void LevelLost()
    {

        GameController.Instance.towns.Clear();
        GameController.Instance.ambush.Clear();
        if (!GameController.Instance.difficulty)
        {
            //DataManagement.Instance.SaveProgress();
            LocalSave.Instance.SaveProgress();
        }

        Invoke("DelayGameOver", 1);        
    }

    private void DelayGameOver()
    {
        //Let player retry or save the loss and have to restart. Keep count of total restarts.
        if (GameController.Instance.difficulty)
        {
            hardGameLostScreen.SetActive(true);
            restartCount.text = "" + GameController.Instance.restarts;
        }
        else
        {
            gameLostScreen.SetActive(true);
        }        
        SoundController.Instance.GameOver();
    }



    public void HideMap()
    {
        globalMap.SetActive(false);
    }

    public void ShowMap()
    {
        globalMap.SetActive(true);
    }

    public void MapOptionsOpen()
    {
        if(GameController.Instance.levelsCompleted > 0)
        {
            GlobalMapController.Instance.paused = true;
            armyOptionsMenu.SetActive(true);
            SoundController.Instance.ButtonClick();
        }
    }

    public void MapOptionsClosed()
    {
        GlobalMapController.Instance.paused = false;
        armyOptionsMenu.SetActive(false);
        SoundController.Instance.ButtonClick();
    }

    public void ArmyPauseButton()
    {
       
        if (GameController.Instance.armyPause)
        {
            GameController.Instance.ArmyUnpause();
            UnpausedText();
        }
        else{
            GameController.Instance.ArmyPause();
            PausedText();
        }
    }

    public void BattleArmyPause()
    {
        if (GameController.Instance.battleArmyPause)
        {
            GameController.Instance.battleArmyPause = false;
            UnpausedText();
        }
        else
        {
            GameController.Instance.battleArmyPause = true;
            PausedText();
        }
    }

    public void LevelOptionsOpen()
    {
        GameController.Instance.ArmyPause();
        
        SoundController.Instance.ButtonClick();
        if (GameController.Instance.inBattle)
        {
            battleOptionsMenu.SetActive(true);
            UnpausedText();
        }
        else
        {
            levelOptionsMenu.SetActive(true);
            PausedText();
        }
    }

    public void LevelOptionsClosed()
    {
        //GameController.Instance.ArmyUnpause();
        SoundController.Instance.ButtonClick();
        levelOptionsMenu.SetActive(false);
        battleOptionsMenu.SetActive(false);
    }

    public void LevelOptionsClosedUnpaused()
    {
        GameController.Instance.ArmyUnpause();
        SoundController.Instance.ButtonClick();
        levelOptionsMenu.SetActive(false);
        battleOptionsMenu.SetActive(false);
    }

    public void OpenActivePlatoonsMenu()
    {
        activePlatoonsView.SetActive(true);
        SoundController.Instance.ButtonClick();
        viewingActivePlatoons = true;
        ViewAllyPlatoons();
    }

    public void CloseActivePlatoonsMenu()
    {
        activePlatoonsView.SetActive(false);
        SoundController.Instance.ButtonClick();
        ClearUnitManagementPrefabs();
        viewingActivePlatoons = false;
        viewingEnemyPlatoons = false;
    }

    public void ViewEnemyPlatoons()
    {
        allyPlatoonButton.SetActive(true);
        enemyPlatoonButton.SetActive(false);
        ClearUnitManagementPrefabs();
        viewingEnemyPlatoons = true;
        for (int i = 0; i < LevelController.Instance.enemyPlatoons.Length; i++)
        {
            if(LevelController.Instance.enemyPlatoons[i] != null)
            {
                GameObject g = (GameObject)Instantiate(platoonPanelPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                g.GetComponent<RectTransform>().SetParent(GameObject.Find("ActivePlatoonsPanel").transform);
                g.GetComponent<RectTransform>().SetPositionAndRotation(g.GetComponent<RectTransform>().position, new Quaternion(0, 0, 0, 0));
                g.GetComponent<RectTransform>().localPosition = new Vector3(g.GetComponent<RectTransform>().position.x, g.GetComponent<RectTransform>().position.x, 0);
                g.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                g.GetComponent<RectTransform>().anchoredPosition = new Vector2(((i / 5) * 160) - 240, ((i % 5) * -95) + 190);

                g.GetComponent<PlatoonInfoPanel>().SetText(i);

                unitManagementPrefabs.Add(g);
            }
            
        }
    }

    public void ViewAllyPlatoons()
    {

        allyPlatoonButton.SetActive(false);
        enemyPlatoonButton.SetActive(true);
        ClearUnitManagementPrefabs();
        viewingEnemyPlatoons = false;
        //LOOKS LIKE THIS IS A BUG, SHOULD BE ALLY?
        for (int i = 0; i < LevelController.Instance.enemyPlatoons.Length; i++)
        {
            if (LevelController.Instance.playerPlatoons[i] != null)
            {
                GameObject g = (GameObject)Instantiate(platoonPanelPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                g.GetComponent<RectTransform>().SetParent(GameObject.Find("ActivePlatoonsPanel").transform);
                g.GetComponent<RectTransform>().SetPositionAndRotation(g.GetComponent<RectTransform>().position, new Quaternion(0, 0, 0, 0));
                g.GetComponent<RectTransform>().localPosition = new Vector3(g.GetComponent<RectTransform>().position.x, g.GetComponent<RectTransform>().position.x, 0);
                g.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                g.GetComponent<RectTransform>().anchoredPosition = new Vector2(((i / 5) * 160) - 240, ((i % 5) * -95) + 190);
                g.GetComponent<PlatoonInfoPanel>().SetText(i);
                unitManagementPrefabs.Add(g);
            }
        }
    }

    public void OpenAudioMenu()
    {
        settingsScreen.SetActive(true);
        SoundController.Instance.SetSliders();
        SoundController.Instance.ButtonClick();
    }

    public void CloseAudioMenu()
    {
        settingsScreen.SetActive(false);
        SoundController.Instance.ButtonClick();
    }

    public void OpenPlatoonManagement()
    {
        platoonManagementPanel.SetActive(true);
        //SoundController.Instance.ButtonClick();

        for (int i = 0; i < ArmyData.Instance.platoons.Count; i++)
        {
            GameObject g = (GameObject)Instantiate(platoonPanelPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            g.GetComponent<RectTransform>().SetParent(GameObject.Find("PlatoonManagementPanel").transform);
            g.GetComponent<RectTransform>().SetPositionAndRotation(g.GetComponent<RectTransform>().position, new Quaternion(0, 0, 0, 0));
            g.GetComponent<RectTransform>().localPosition = new Vector3(g.GetComponent<RectTransform>().position.x, g.GetComponent<RectTransform>().position.x, 0);
            g.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            g.GetComponent<RectTransform>().anchoredPosition = new Vector2(((i / 5) * 160) - 240, ((i % 5) * -95) + 190);
            g.GetComponent<PlatoonInfoPanel>().SetText(i);
            unitManagementPrefabs.Add(g);
        }
    }

    public void ClosePlatoonManagement()
    {
        ClearUnitManagementPrefabs();
        //SoundController.Instance.ButtonClick();
        platoonManagementPanel.SetActive(false);
    }

    public void BattleMenuPlatoonInspect()
    {
        OpenPlatoonInspect(GameController.Instance.playerFighting.GetPlatoonNum());
    }

    public void OpenPlatoonInspect(int pNum)
    {
        platoonInspectionPanel.SetActive(true);
        if (!viewingEnemyPlatoons)
        {
            selectedPlatoon = ArmyData.Instance.platoons[pNum];
            changeLeaderButton.SetActive(true);
            enemyPlatoonButton.SetActive(false);
            OpenPlayerPlatoonInspect(pNum);
        }
        else
        {
            selectedPlatoon = LevelController.Instance.enemyPlatoons[pNum];
            changeLeaderButton.SetActive(false);
            allyPlatoonButton.SetActive(false);
            OpenEnemyPlatoonInspect(pNum);
        }
        
        if (inGlobalMenu)
        {
            removeUnitButton.SetActive(true);
        }
        else
        {
            removeUnitButton.SetActive(false);
        }           
    }

    private void OpenPlayerPlatoonInspect(int pNum)
    {
        for (int i = 0; i < 25; i++)
        {
            if (selectedPlatoon.units[i] != null)
            {
                GameObject g = (GameObject)Instantiate(platoonInspectUnitInfoBoxPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                g.GetComponent<RectTransform>().SetParent(GameObject.Find("PlatoonInspectionPanel").transform);
                g.GetComponent<RectTransform>().SetPositionAndRotation(g.GetComponent<RectTransform>().localPosition, new Quaternion(0, 0, 0, 0));
                g.GetComponent<RectTransform>().localPosition = new Vector3(g.GetComponent<RectTransform>().localPosition.x, g.GetComponent<RectTransform>().localPosition.x, 0);
                g.GetComponent<PlatoonInspectInfoPanel>().SetText(pNum, i);
                platoonUnitPanels[i] = g;
                unitManagementPrefabs.Add(g);

                if (selectedPlatoon.units[i].GetClassSize() == 1)
                {
                    g.GetComponent<RectTransform>().anchoredPosition = new Vector2(250 - ((i / 5) * 125), ((i % 5) * 90) - 190);
                    g.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                }
                else if (selectedPlatoon.units[i].GetClassSize() == 2)
                {
                    g.GetComponent<PlatoonInspectInfoPanel>().SetText(pNum, i);

                    float mediumXPos = ((250 - ((i / 5) * 125)) + (250 - (((i - 5) / 5) * 125))) / 2;

                    float mediumYPos = ((((i % 5) * 90) - 170) + ((((i + 1) % 5) * 90) - 190)) / 2 - 10;

                    g.GetComponent<RectTransform>().anchoredPosition = new Vector2(mediumXPos, mediumYPos);
                    g.GetComponent<RectTransform>().localScale = new Vector3(2, 2, 2);
                }
                else
                {
                    //g.GetComponent<RectTransform>().anchoredPosition = new Vector2(250 - (((i - 4) * 5) * 125), (((i - 4) / 5) % 90) - 190);
                    g.GetComponent<RectTransform>().anchoredPosition = new Vector2(250 - (((i - 4) / 5) * 125), (((i - 4) % 5) * 90) - 190);
                    g.GetComponent<RectTransform>().localScale = new Vector3(3, 3, 3);
                }

                if (selectedPlatoon.GetLeader().positionNum == i)
                {
                    g.GetComponent<PlatoonInspectInfoPanel>().SetLeaderText();
                }
            }

            if (!selectedPlatoon.blockedSpaces[i] && selectedPlatoon.availableSpace >= 1 && inGlobalMenu)
            {
                AddAddUnitBox(i);
            }
        }
    }

    private void OpenEnemyPlatoonInspect(int pNum)
    {
        for (int i = 0; i < 25; i++)
        {
            if (selectedPlatoon.units[i] != null)
            {
                GameObject g = (GameObject)Instantiate(platoonInspectUnitInfoBoxPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                g.GetComponent<RectTransform>().SetParent(GameObject.Find("PlatoonInspectionPanel").transform);
                g.GetComponent<RectTransform>().SetPositionAndRotation(g.GetComponent<RectTransform>().localPosition, new Quaternion(0, 0, 0, 0));
                g.GetComponent<RectTransform>().localPosition = new Vector3(g.GetComponent<RectTransform>().localPosition.x, g.GetComponent<RectTransform>().localPosition.x, 0);
                g.GetComponent<PlatoonInspectInfoPanel>().SetText(pNum, i);
                platoonUnitPanels[i] = g;
                unitManagementPrefabs.Add(g);

                if (selectedPlatoon.units[i].GetClassSize() == 1)
                {
                    g.GetComponent<RectTransform>().anchoredPosition = new Vector2(((i / 5) * 125) - 250, ((i % 5) * 90) - 190);
                    g.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                }
                else if (selectedPlatoon.units[i].GetClassSize() == 2)
                {
                    g.GetComponent<PlatoonInspectInfoPanel>().SetText(pNum, i);

                    float mediumXPos = ((((i / 5) * 125) - 250) + ((((i - 5) / 5) * 125) - 250)) / 2;

                    float mediumYPos = ((((i % 5) * 90) - 170) + ((((i + 1) % 5) * 90) - 190)) / 2;

                    g.GetComponent<RectTransform>().anchoredPosition = new Vector2(mediumXPos, mediumYPos);
                    g.GetComponent<RectTransform>().localScale = new Vector3(2, 2, 2);
                }
                else
                {
                    g.GetComponent<RectTransform>().anchoredPosition = new Vector2((((i - 4) * 5) * 125) - 250, (((i - 4) / 5) % 90) - 190);
                    g.GetComponent<RectTransform>().localScale = new Vector3(3, 3, 3);
                }

                if (selectedPlatoon.GetLeader().positionNum == i)
                {
                    g.GetComponent<PlatoonInspectInfoPanel>().SetLeaderText();
                }
            }

            if (!selectedPlatoon.blockedSpaces[i] && selectedPlatoon.availableSpace >= 1 && inGlobalMenu)
            {
                AddAddUnitBox(i);
            }
        }
    }

    private void AddAddUnitBox(int i)
    {       

        GameObject g = (GameObject)Instantiate(platoonInspectAddUnitPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        g.GetComponent<RectTransform>().SetParent(GameObject.Find("PlatoonInspectionPanel").transform);
        g.GetComponent<RectTransform>().SetPositionAndRotation(g.GetComponent<RectTransform>().localPosition, new Quaternion(0, 0, 0, 0));
        g.GetComponent<RectTransform>().localPosition = new Vector3(g.GetComponent<RectTransform>().localPosition.x, g.GetComponent<RectTransform>().localPosition.x, 0);
        g.GetComponent<RectTransform>().anchoredPosition = new Vector2(250 - ((i / 5) * 125), ((i % 5) * 90) - 190);
        g.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        //g.GetComponent<RectTransform>().anchoredPosition = new Vector2(((i % 5) * 125) - 250, ((i / 5) * 90) - 190);
        //g.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        g.GetComponent<PlatoonAddUnitBox>().SetLocation(i);
        unitManagementPrefabs.Add(g);
    }

    private void AddAddUnitBoxes()
    {
        for (int i = 0; i < 25; i++)
        {
            if (!selectedPlatoon.blockedSpaces[i])
            {
                AddAddUnitBox(i);
            }
        }
    }

    public void ChangeLeaderButton()
    {
        if (!selectingLeader)
        {
            HideChangeLeaderButton();
            SoundController.Instance.ButtonClick();
        }
        else
        {
            SoundController.Instance.ButtonClick();
        }
    }

    public void HideChangeLeaderButton()
    {
        changeLeaderButton.SetActive(false);
        selectingLeader = true;
    }

    public void ShowChangeLeaderButton()
    {
        changeLeaderButton.SetActive(true);
        selectingLeader = false;
    }

    public void HidePlatoonUnitOptionsPanel()
    {
        platoonUnitOptionsPanel.SetActive(false);
        //SoundController.Instance.ButtonClick();
    }

    public void ShowPlatoonUnitOptionsPanel()
    {
        platoonUnitOptionsPanel.SetActive(true);
        //SoundController.Instance.ButtonClick();
    }

    public void ClosePlatoonInspection()
    {
        ClearUnitManagementPrefabs();
        selectedUnit = null;
        platoonInspectionPanel.SetActive(false);

        if (inspectingThroughHover)
        {
            viewingEnemyPlatoons = false;
            CanvasController.Instance.inspectingThroughHover = false;
        }
        else if (!viewingActivePlatoons)
        {
            OpenPlatoonManagement();
        }
        else if (viewingEnemyPlatoons)
        {
            allyPlatoonButton.SetActive(true);
            ViewEnemyPlatoons();
        }
        else
        {
            enemyPlatoonButton.SetActive(true);
            ViewAllyPlatoons();
        }        
    }    

    public void OpenHumanChange()
    {
        this.gameObject.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
        classChangeCamera.SetActive(true);
        CloseClassHoverPanels();
        if (selectedUnit.IsMale())
        {
            OpenMHumanChange();
            for (int i = 0; i < selectedUnit.GetUnlockedClasses().Length; i++)
            {
                //This is a bad implementation! need to change text colors a better way.
                humanClassChangeButtons[i].SetActive(true);
                if (humanClassChangeButtons[i].GetComponent<ClassChangeButton>())
                {
                    humanClassChangeButtons[i].GetComponent<ClassChangeButton>().SetNormalText();
                }
                humanClassChangeButtons[i].SetActive(false);

                if (selectedUnit.GetUnlockedClasses()[i])
                {
                    humanClassChangeButtons[i].SetActive(true);
                    maleHumanClassChangeObjects[i].SetActive(true);
                    humanText[i].SetActive(true);
                    humanClassChangeReqs[i].SetActive(false);
                }
                else if (i != 53 && i != 57)
                {
                    humanClassChangeButtons[i].SetActive(false);
                    maleHumanClassChangeObjects[i].SetActive(false);
                    //humanText[i].SetActive(false);
                    humanClassChangeReqs[i].SetActive(true);
                }
            }
        }
        else
        {
            OpenFHumanChange();
            for (int i = 0; i < selectedUnit.GetUnlockedClasses().Length; i++)
            {
                humanClassChangeButtons[i].SetActive(true);
                if (humanClassChangeButtons[i].GetComponent<ClassChangeButton>())
                {
                    humanClassChangeButtons[i].GetComponent<ClassChangeButton>().SetNormalText();
                }
                humanClassChangeButtons[i].SetActive(false);

                if (selectedUnit.GetUnlockedClasses()[i])
                {
                    humanClassChangeButtons[i].SetActive(true);
                    femaleHumanClassChangeObjects[i].SetActive(true);
                    humanText[i].SetActive(true);
                    humanClassChangeReqs[i].SetActive(false);
                }
                else if(i != 53 && i != 57)
                {
                    humanClassChangeButtons[i].SetActive(false);
                    femaleHumanClassChangeObjects[i].SetActive(false);
                    //humanText[i].SetActive(false);
                    humanClassChangeReqs[i].SetActive(true);
                }
            }
        }
    }

    public void OpenHumanChange2()
    {
        CloseClassHoverPanels();
        if (selectedUnit.IsMale())
        {
            MaleHumanPage2();
        }
        else
        {
            FemaleHumanPage2();
        }
        SoundController.Instance.ButtonClick();
    }

    public void OpenHumanChange3()
    {
        CloseClassHoverPanels();
        if (selectedUnit.IsMale())
        {
            MaleHumanPage3();
        }
        else
        {
            FemaleHumanPage3();
        }
        SoundController.Instance.ButtonClick();
    }

    public void OpenFHumanChange()
    {
        femaleHumanClassChangeMenu1.SetActive(true);
        humanClassChangePanel1.SetActive(true);
        femaleHumanClassChangeMenu2.SetActive(false);
        humanClassChangePanel2.SetActive(false);
        femaleHumanClassChangeMenu3.SetActive(false);
        humanClassChangePanel3.SetActive(false);
    }

    public void OpenMHumanChange()
    {
        maleHumanClassChangeMenu1.SetActive(true);
        humanClassChangePanel1.SetActive(true);
        maleHumanClassChangeMenu2.SetActive(false);
        humanClassChangePanel2.SetActive(false);
        maleHumanClassChangeMenu3.SetActive(false);
        humanClassChangePanel3.SetActive(false);
    }

    public void CloseHumanChange()
    {
        this.gameObject.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        classChangeCamera.SetActive(false);
        CloseClassHoverPanels();

        if (selectedUnit.IsMale())
        {
            CloseMHumanChange();
        }
        else
        {
            CloseFHumanChange();
        }
        SoundController.Instance.ButtonClick();
    }

    public void CloseFHumanChange()
    {
        femaleHumanClassChangeMenu1.SetActive(false);
        humanClassChangePanel1.SetActive(false);
        femaleHumanClassChangeMenu2.SetActive(false);
        humanClassChangePanel2.SetActive(false);
        femaleHumanClassChangeMenu3.SetActive(false);
        humanClassChangePanel3.SetActive(false);
    }

    public void CloseMHumanChange()
    {
        maleHumanClassChangeMenu1.SetActive(false);
        humanClassChangePanel1.SetActive(false);
        maleHumanClassChangeMenu2.SetActive(false);
        humanClassChangePanel2.SetActive(false);
        maleHumanClassChangeMenu3.SetActive(false);
        humanClassChangePanel3.SetActive(false);
    }

    public void FemaleHumanPage2()
    {
        femaleHumanClassChangeMenu1.SetActive(false);
        humanClassChangePanel1.SetActive(false);
        femaleHumanClassChangeMenu2.SetActive(true);
        humanClassChangePanel2.SetActive(true);
        femaleHumanClassChangeMenu3.SetActive(false);
        humanClassChangePanel3.SetActive(false);
    }

    public void FemaleHumanPage3()
    {
        femaleHumanClassChangeMenu1.SetActive(false);
        humanClassChangePanel1.SetActive(false);
        femaleHumanClassChangeMenu2.SetActive(false);
        humanClassChangePanel2.SetActive(false);
        femaleHumanClassChangeMenu3.SetActive(true);
        humanClassChangePanel3.SetActive(true);
    }

    public void MaleHumanPage2()
    {
        maleHumanClassChangeMenu1.SetActive(false);
        humanClassChangePanel1.SetActive(false);
        maleHumanClassChangeMenu2.SetActive(true);
        humanClassChangePanel2.SetActive(true);
        maleHumanClassChangeMenu3.SetActive(false);
        humanClassChangePanel3.SetActive(false);
    }

    public void MaleHumanPage3()
    {
        maleHumanClassChangeMenu1.SetActive(false);
        humanClassChangePanel1.SetActive(false);
        maleHumanClassChangeMenu2.SetActive(false);
        humanClassChangePanel2.SetActive(false);
        maleHumanClassChangeMenu3.SetActive(true);
        humanClassChangePanel3.SetActive(true);
    }

    public void OpenAnimalChange()
    {
        animalClassChangeMenu.SetActive(true);
        animalClassChangePanel.SetActive(true);
        CloseClassHoverPanels();
        this.gameObject.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
        classChangeCamera.SetActive(true);
        SoundController.Instance.ButtonClick();
        for (int i = 0; i < selectedUnit.GetUnlockedClasses().Length; i ++)
        {
            //Yuck!
            animalClassChangeButtons[i].SetActive(true);
            animalClassChangeButtons[i].SetActive(false);

            if (selectedUnit.GetUnlockedClasses()[i])
            {
                animalClassChangeButtons[i].SetActive(true);
                animalClassChangeObjects[i].SetActive(true);
                animalClassChangeReqs[i].SetActive(false);
                animalText[i].SetActive(true);
            }
            else
            {
                animalClassChangeButtons[i].SetActive(false);
                animalClassChangeObjects[i].SetActive(false);
                animalClassChangeReqs[i].SetActive(true);
                //animalText[i].SetActive(false);
            }
        }
    }

    public void CloseAnimalChange()
    {
        this.gameObject.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        classChangeCamera.SetActive(false);
        SoundController.Instance.ButtonClick();
        animalClassChangeMenu.SetActive(false);
        animalClassChangePanel.SetActive(false);
        CloseClassHoverPanels();
    }

    public void OpenMythChange()
    {
        Debug.Log("Open myth change");
        CloseClassHoverPanels();
        this.gameObject.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
        classChangeCamera.SetActive(true);
        SoundController.Instance.ButtonClick();
        mythClassChangeMenu.SetActive(true);
        mythClassChangePanel.SetActive(true);
        mythClassChangeMenu2.SetActive(false);
        mythClassChangePanel2.SetActive(false);
        for (int i = 0; i < selectedUnit.GetUnlockedClasses().Length; i++)
        {
            mythClassChangeButtons[i].SetActive(true);
            mythClassChangeButtons[i].SetActive(false);

            if (selectedUnit.GetUnlockedClasses()[i])
            {
                mythClassChangeButtons[i].SetActive(true);
                mythClassChangeObjects[i].SetActive(true);
                mythText[i].SetActive(true);
                mythClassChangeReqs[i].SetActive(false);
            }
            else
            {
                mythClassChangeButtons[i].SetActive(false);
                mythClassChangeObjects[i].SetActive(false);
                mythClassChangeReqs[i].SetActive(true);
                //mythText[i].SetActive(false);
            }
        }
    }

    public void OpenMythChange2()
    {
        mythClassChangeMenu2.SetActive(true);
        mythClassChangePanel2.SetActive(true);
        mythClassChangeMenu.SetActive(false);
        mythClassChangePanel.SetActive(false);
        SoundController.Instance.ButtonClick();
    }

    public void CloseMythChange()
    {
        this.gameObject.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        classChangeCamera.SetActive(false);
        SoundController.Instance.ButtonClick();
        mythClassChangeMenu2.SetActive(false);
        mythClassChangePanel2.SetActive(false);
        mythClassChangeMenu.SetActive(false);
        mythClassChangePanel.SetActive(false);
        CloseClassHoverPanels();
    }

    

    public void ClassChange(int classNum)
    {
        selectedUnit.SetClass(classNum, selectedUnit.platoonNum);
        CloseAnimalChange();
        CloseMythChange();
        CloseHumanChange();
        CloseClassHoverPanels();
        OpenUnitInspect(selectedUnit);
        //SoundController.Instance.ButtonClick();
    }

    public void PlatoonRemoveUnit()
    {
        if(selectedUnit.positionNum != selectedPlatoon.GetLeader().positionNum)
        {
           /* if (selectedPlatoon.availableSpace == 0)
            {
                AddAddUnitBoxes();
            }*/

            //selectedPlatoon.units[selectedUnit.positionNum] = null;
            //selectedPlatoon.blockedSpaces[selectedUnit.positionNum] = false;
            selectedPlatoon.RemoveUnit(selectedUnit, selectedUnit.positionNum);

            HidePlatoonUnitOptionsPanel();
            Destroy(platoonUnitPanels[selectedUnit.positionNum]);

            //AddAddUnitBox(selectedUnit.positionNum);
            AddAddUnitBoxes();

            selectedUnit.platoonNum = -1;
            selectedUnit.positionNum = -1;

            //SoundController.Instance.ButtonClick();

            selectedUnit = null;
        }        
    }

    public void SelectUnitToAdd(int positionNum)
    {
        addingUnit = true;
        ClosePlatoonInspection();
        OpenUnitManagementForAdding();        
        addingPosition = positionNum;
        SoundController.Instance.ButtonClick();
    }

    public void OpenUnitManagement()
    {
        unitManagementPanel.SetActive(true);
        viewingAllUnits = true;
        if (hidingAssignedUnits)
        {
            HideAssignedUnits();
        }
        else
        {
            ShowAssignedUnits();
        }
        SoundController.Instance.ButtonClick();
    }

    public void OpenUnitManagementForAdding()
    {
        unitManagementPanel.SetActive(true);
        SortUnitByLevelsHighToLow();
    }

    public void CloseUnitManagementButton()
    {
        viewingAllUnits = false;
        CloseUnitManagement();
    }

    public void CloseUnitManagement()
    {
        unitManagementPanel.SetActive(false);
        ClearUnitManagementPrefabs();
        SoundController.Instance.ButtonClick();
        if (addingUnit)
        {
            addingUnit = false;
            OpenPlatoonInspect(selectedPlatoon.platoonNum);
        }
    }

    public void ShowAssignedUnits()
    {
        hidingAssignedUnits = false;
        showAssignedUnitsButton.SetActive(false);
        hideAssignedUnitsButton.SetActive(true);
        if (unitSortingMethod == 0)
        {
            SortUnitByLevelsHighToLow();
        }else if(unitSortingMethod == 1)
        {
            SortUnitByLevelsLowToHigh();
        }
        else if (unitSortingMethod == 2)
        {
            SortUnitByPlatoonsHighToLow();
        }
        else if (unitSortingMethod == 3)
        {
            SortUnitByPlatoonsLowToHigh();
        }
    }

    public void HideAssignedUnits()
    {
        hidingAssignedUnits = true;
        showAssignedUnitsButton.SetActive(true);
        hideAssignedUnitsButton.SetActive(false);
        if (unitSortingMethod == 0)
        {
            SortUnitByLevelsHighToLow();
        }
        else if (unitSortingMethod == 1)
        {
            SortUnitByLevelsLowToHigh();
        }
        else if (unitSortingMethod == 2)
        {
            SortUnitByPlatoonsHighToLow();
        }
        else if (unitSortingMethod == 3)
        {
            SortUnitByPlatoonsLowToHigh();
        }
    }

    public void SortUnitByLevelsHighToLow()
    {
        sortLevelsHighLowButton.SetActive(false);
        sortLevelsLowHighButton.SetActive(true);
        ClearUnitManagementPrefabs();
        //SoundController.Instance.ButtonClick();
        unitSortingMethod = 0;
        if (addingUnit || hidingAssignedUnits)
        {
            List<UnitData> tempList = ArmyData.Instance.GetUnassignedUnits();
            tempList.Sort((x, y) => y.totalLevels.CompareTo(x.totalLevels));
            CreateUnitManagementPrefabs(tempList);
        }
        else
        {
            ArmyData.Instance.units.Sort((x, y) => y.totalLevels.CompareTo(x.totalLevels));
            CreateUnitManagementPrefabs();
        }
        
    }

    public void SortUnitByLevelsLowToHigh()
    {
        //SoundController.Instance.ButtonClick();
        sortLevelsHighLowButton.SetActive(true);
        sortLevelsLowHighButton.SetActive(false);
        ClearUnitManagementPrefabs();
        unitSortingMethod = 1;
        if (addingUnit || hidingAssignedUnits)
        {
            List<UnitData> tempList = ArmyData.Instance.GetUnassignedUnits();
            tempList.Sort((x, y) => x.totalLevels.CompareTo(y.totalLevels));
            CreateUnitManagementPrefabs(tempList);
        }
        else
        {
            ArmyData.Instance.units.Sort((x, y) => x.totalLevels.CompareTo(y.totalLevels));
            CreateUnitManagementPrefabs();
        }
    }

    public void SortUnitByPlatoonsHighToLow()
    {
        //SoundController.Instance.ButtonClick();
        sortPlatoonHighLowButton.SetActive(false);
        sortPlatoonLowHighButton.SetActive(true);
        ClearUnitManagementPrefabs();
        unitSortingMethod = 2;
        if (addingUnit || hidingAssignedUnits)
        {
            List<UnitData> tempList = ArmyData.Instance.GetUnassignedUnits();
            tempList.Sort((x, y) => y.platoonNum.CompareTo(x.platoonNum));
            CreateUnitManagementPrefabs(tempList);
        }
        else
        {
            ArmyData.Instance.units.Sort((x, y) => y.platoonNum.CompareTo(x.platoonNum));
            CreateUnitManagementPrefabs();
        }
    }

    public void SortUnitByPlatoonsLowToHigh()
    {
        //SoundController.Instance.ButtonClick();
        sortPlatoonHighLowButton.SetActive(true);
        sortPlatoonLowHighButton.SetActive(false);
        ClearUnitManagementPrefabs();
        unitSortingMethod = 3;
        if (addingUnit || hidingAssignedUnits)
        {
            List<UnitData> tempList = ArmyData.Instance.GetUnassignedUnits();
            tempList.Sort((x, y) => x.platoonNum.CompareTo(y.platoonNum));
            CreateUnitManagementPrefabs(tempList);
        }
        else
        {
            ArmyData.Instance.units.Sort((x, y) => x.platoonNum.CompareTo(y.platoonNum));
            CreateUnitManagementPrefabs();
        }
    }

    private void ClearUnitManagementPrefabs()
    { 
        if(unitManagementPrefabs.Count > 0)
        {
            for (int i = unitManagementPrefabs.Count; i > 0; i--)
            {
                Destroy(unitManagementPrefabs[i-1]);
            }
            unitManagementPrefabs.Clear();
        }
            
    }

    private void CreateUnitManagementPrefabs()
    {
        for (int i = 0; i < ArmyData.Instance.units.Count; i++)
        {
            int horizontalAdjustment = (int)(ArmyData.Instance.units.Count * 140 / 5);
            unitScrollViewContent.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, horizontalAdjustment);

            GameObject g = (GameObject)Instantiate(unitManagementPrefab, unitScrollViewContent);
            g.GetComponent<RectTransform>().SetPositionAndRotation(g.GetComponent<RectTransform>().localPosition, new Quaternion(0, 0, 0, 0));
            g.GetComponent<RectTransform>().localPosition = new Vector3(g.GetComponent<RectTransform>().localPosition.x, g.GetComponent<RectTransform>().localPosition.x, 0);
            g.GetComponent<RectTransform>().anchoredPosition = new Vector2((((i / 5) * 140) + 60) - horizontalAdjustment/2, (175 - (i % 5) * 85));
            g.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            g.GetComponent<UnitManagementPrefab>().SetText(ArmyData.Instance.units[i]);
            
            unitManagementPrefabs.Add(g);
            //g.GetComponent<RectTransform>().anchoredPosition = new Vector2(((i / 5) * 160) - 240, ((i % 5) * -95) + 190);
        }
    }

    private void CreateUnitManagementPrefabs(List<UnitData> uaList)
    {
        for (int i = 0; i < uaList.Count; i++)
        {
            int horizontalAdjustment = (int)(uaList.Count * 140 / 5);
            unitScrollViewContent.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, horizontalAdjustment);

            GameObject g = (GameObject)Instantiate(unitManagementPrefab, unitScrollViewContent);
            g.GetComponent<RectTransform>().SetPositionAndRotation(g.GetComponent<RectTransform>().localPosition, new Quaternion(0, 0, 0, 0));
            g.GetComponent<RectTransform>().localPosition = new Vector3(g.GetComponent<RectTransform>().localPosition.x, g.GetComponent<RectTransform>().localPosition.x, 0);
            g.GetComponent<RectTransform>().anchoredPosition = new Vector2((((i / 5) * 140) + 60) - horizontalAdjustment / 2, (175 - (i % 5) * 85));
            g.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            g.GetComponent<UnitManagementPrefab>().SetText(uaList[i]);

            unitManagementPrefabs.Add(g);
            //g.GetComponent<RectTransform>().anchoredPosition = new Vector2(((i / 5) * 160) - 240, ((i % 5) * -95) + 190);
        }
    }

    public void PlatoonInspectUnit()
    {
        OpenUnitInspect(selectedUnit);
        //SoundController.Instance.ButtonClick();
    }

    public void OpenUnitInspect(UnitData d)
    {
        if(d != null)
        {
            unitInspectionPanel.SetActive(true);
            selectedUnit = d;
            selectedUnit.UpdateClassUnlocks();
            CloseUnitManagement();
            unitInspectionPanel.GetComponent<UnitInspectionPanel>().SetText(d);

            classDescriptionCanvas.SetActive(true);
            classDescriptionPanel.SetActive(true);
            if (viewingEnemyPlatoons)
            {
                classChangeButton.SetActive(false);
            }
            else
            {
                classChangeButton.SetActive(true);
            }
        }      

    }

    public void CloseUnitInspection()
    {
        unitInspectionPanel.SetActive(false);
        CloseClassHoverPanels();
        if (viewingAllUnits)
        {
            OpenUnitManagement();
        }
        else
        {
            OpenPlatoonInspect(selectedPlatoon.platoonNum);
        }
        //SoundController.Instance.ButtonClick();
    }

    public void CloseClassHoverPanels()
    {
        foreach(GameObject g in classDescriptionPanels)
        {
            if(g != null)
            {
                g.SetActive(false);
            }
        }
    }

    public void OpenBattleGUI()
    {
        battleGUI.SetActive(true);
    }

    public void CloseBattleGUI()
    {
        battleBattleText.text = "";
        battleUnitText.text = "";
        battleGUI.SetActive(false);
    }

    public void PauseBattle(bool autoPause)
    {
        if (battlePaused && !autoPause)
        {
            BattleController.Instance.UnpauseBattle();
            battlePaused = false;
        }
        else
        {
            BattleController.Instance.PauseBattle();
            battlePaused = true;
        }
        SoundController.Instance.ButtonClick();
    }

    public void RetreatButton()
    {
        BattleController.Instance.Retreat();
        battlePaused = false;
        GameController.Instance.ArmyPause();
        PausedText();
    }

    public void PausedText()
    {
        armiesPausedText.text = "Armies Paused";
        armiesPausedText.color = Color.green;
        armiesPausedTextBattle.text = "Armies Paused";
        armiesPausedTextBattle.color = Color.green;
    }

    public void UnpausedText()
    {
        armiesPausedText.text = "Armies Unpaused";
        armiesPausedText.color = Color.red;
        armiesPausedTextBattle.text = "Armies Unpaused";
        armiesPausedTextBattle.color = Color.red;
        pauseText.SetActive(false);
    }

    public void ArmiesPausedBigText(bool showing)
    {
        pauseText.SetActive(showing);
    }

    public void ClassChangeErrorMessage()
    {
        classChangeErrorMessage.SetActive(true);
    }

    public void ShowStatusDescriptions()
    {
        statusCanvas.SetActive(true);
    }

    public void HideStatusDescriptions()
    {
        statusCanvas.SetActive(false);
    }

    public void LevelStart()
    {
        selectedPlatoonText.text = "";
    }

    public void ClearTexts()
    {
        battleBattleText.text = "";
        battleSkillText.text = "";
        battleStatusText.text = "";
        battleUnitText.text = "";
    }

    public void SetBattleButtonOneText(UnitData movingUnit)
    {
        if (movingUnit.charType == 1)
        {
            ButtonOneHuman(movingUnit);
        }
        else if (movingUnit.charType == 2)
        {
            ButtonOneAnimal(movingUnit);
        }
        else if (movingUnit.charType == 3)
        {
            ButtonOneMyth(movingUnit);
        }
    }

    public void SetBattleButtonTwoText(UnitData movingUnit)
    {
        if (movingUnit.charType == 1)
        {
            ButtonTwoHuman(movingUnit);
        }
        else if (movingUnit.charType == 2)
        {
            ButtonTwoAnimal(movingUnit);
        }
        else if (movingUnit.charType == 3)
        {
            ButtonTwoMyth(movingUnit);
        }
    }

    public void SetBattleButtonThreeText(UnitData movingUnit)
    {
        if (movingUnit.charType == 1)
        {
            ButtonThreeHuman(movingUnit);
        }
        else if (movingUnit.charType == 2)
        {
            ButtonThreeAnimal(movingUnit);
        }
        else if (movingUnit.charType == 3)
        {
            ButtonThreeMyth(movingUnit);
        }
    }

    public void ButtonOneHuman(UnitData movingUnit)
    {
        switch (movingUnit.GetCharClass())
        {
            case 0:
            case 3:
            case 7:
            case 9:
            case 10:
            case 11:
            case 12:
            case 13:
            case 14:
            case 18:
            case 25:
            case 37:
            case 38:
            case 39:
            case 40:
            case 41:
            case 42:
            case 48:
            case 49:
            case 50:
                skillButton1.GetComponentInChildren<Text>().text = "Slash";
                break;
            case 1:
            case 4:
            case 19:
            case 27:
            case 45:
                skillButton1.GetComponentInChildren<Text>().text = "Magic Missle";
                break;
            case 2:
            case 17:
            case 36:
            case 54:
                skillButton1.GetComponentInChildren<Text>().text = "Stab";
                break;
            case 5:
            case 21:
            case 32:
            case 46:
                skillButton1.GetComponentInChildren<Text>().text = "Bash";
                break;
            case 6:
            case 15:
            case 35:
            case 47:
                skillButton1.GetComponentInChildren<Text>().text = "Shoot";
                break;
            case 8:
                skillButton1.GetComponentInChildren<Text>().text = "Punch";
                break;
            case 24:
                skillButton1.GetComponentInChildren<Text>().text = "Knockout";
                break;
            case 16:
            case 43:
            case 56:
                skillButton1.GetComponentInChildren<Text>().text = "Snare";
                break;
            case 20:
            case 26:
            case 58:
                skillButton1.GetComponentInChildren<Text>().text = "Pain";
                break;
            case 22:
            case 31:
                skillButton1.GetComponentInChildren<Text>().text = "Flash";
                break;
            case 23:
            case 33:
                skillButton1.GetComponentInChildren<Text>().text = "Slow";
                break;
            case 28:
                skillButton1.GetComponentInChildren<Text>().text = "Earthspike";
                break;
            case 29:
                skillButton1.GetComponentInChildren<Text>().text = "Fireball";
                break;
            case 30:
                skillButton1.GetComponentInChildren<Text>().text = "Refresh";
                break;
            case 34:
                skillButton1.GetComponentInChildren<Text>().text = "Tangle";
                break;
            case 55:
                skillButton1.GetComponentInChildren<Text>().text = "Corrupt";
                break;
            case 44:
                skillButton1.GetComponentInChildren<Text>().text = "Trip";
                break;
            case 51:
            case 52:
                skillButton1.GetComponentInChildren<Text>().text = "Hack";
                break;
            default:
                break;
        }
    }

    public void ButtonTwoHuman(UnitData movingUnit)
    {
        switch (movingUnit.GetCharClass())
        {
            case 4:
                skillButton2.GetComponentInChildren<Text>().text = "Magic Bolt";
                break;
            case 5:
            case 21:
            case 24:
            case 32:
            case 34:
            case 41:
            case 46:
            case 49:
                skillButton2.GetComponentInChildren<Text>().text = "Heal";
                break;
            case 6:
            case 16:
            case 43:
            case 56:
                skillButton2.GetComponentInChildren<Text>().text = "Spike";
                break;
            case 7:
            case 17:
            case 25:
            case 36:
            case 37:
            case 44:
            case 48:
            case 54:
                skillButton2.GetComponentInChildren<Text>().text = "Backstab";
                break;
            case 8:
                skillButton2.GetComponentInChildren<Text>().text = "Haymaker";
                break;
            case 9:
            case 14:
                skillButton2.GetComponentInChildren<Text>().text = "Chop";
                break;
            case 10:
            case 40:
            case 55:
                skillButton2.GetComponentInChildren<Text>().text = "Trip";
                break;
            case 11:
            case 38:
            case 50:
                skillButton2.GetComponentInChildren<Text>().text = "Hack";
                break;
            case 12:
            case 51:
                skillButton2.GetComponentInChildren<Text>().text = "Guard";
                break;
            case 13:
            case 39:
                skillButton2.GetComponentInChildren<Text>().text = "Disarm";
                break;
            case 15:
            case 35:
            case 47:
                skillButton2.GetComponentInChildren<Text>().text = "Snipe";
                break;
            case 18:
                skillButton2.GetComponentInChildren<Text>().text = "Slice";
                break;
            case 19:
            case 27:
            case 45:
            case 52:
                skillButton2.GetComponentInChildren<Text>().text = "Magic Storm";
                break;
            case 20:
            case 26:
            case 42:
            case 58:
                skillButton2.GetComponentInChildren<Text>().text = "Corrupt";
                break;
            case 22:
            case 29:
                skillButton2.GetComponentInChildren<Text>().text = "Incinerate";
                break;
            case 23:
            case 33:
                skillButton2.GetComponentInChildren<Text>().text = "Sleep";
                break;
            case 28:
                skillButton2.GetComponentInChildren<Text>().text = "Rockslide";
                break;
            case 30:
                skillButton2.GetComponentInChildren<Text>().text = "Flood";
                break;
            case 31:
                skillButton2.GetComponentInChildren<Text>().text = "Wind Armor";
                break;
            default:
                break;
        }
    }

    public void ButtonThreeHuman(UnitData movingUnit)
    {
        switch (movingUnit.GetCharClass())
        {
            case 26:
                skillButton3.GetComponentInChildren<Text>().text = "Psychosis";
                break;
            case 27:
                skillButton3.GetComponentInChildren<Text>().text = "Magic Assault";
                break;
            case 28:
                skillButton3.GetComponentInChildren<Text>().text = "Earthquake";
                break;
            case 29:
                skillButton3.GetComponentInChildren<Text>().text = "Inferno";
                break;
            case 30:
                skillButton3.GetComponentInChildren<Text>().text = "Tsunami";
                break;
            case 31:
                skillButton3.GetComponentInChildren<Text>().text = "Hurricane";
                break;
            case 32:
            case 33:
                skillButton3.GetComponentInChildren<Text>().text = "Mass Heal";
                break;
            case 34:
                skillButton3.GetComponentInChildren<Text>().text = "Nature's Bounty";
                break;
            case 35:
                skillButton3.GetComponentInChildren<Text>().text = "Poison Arrow";
                break;
            case 36:
                skillButton3.GetComponentInChildren<Text>().text = "Keelhaul";
                break;
            case 37:
                skillButton3.GetComponentInChildren<Text>().text = "Skewer";
                break;
            case 38:
                skillButton3.GetComponentInChildren<Text>().text = "Destroy";
                break;
            case 39:
                skillButton3.GetComponentInChildren<Text>().text = "Critical Strike";
                break;
            case 40:
                skillButton3.GetComponentInChildren<Text>().text = "Work the Crowd";
                break;
            case 41:
                skillButton3.GetComponentInChildren<Text>().text = "Fortify";
                break;
            case 42:
                skillButton3.GetComponentInChildren<Text>().text = "Dark Strike";
                break;
            case 43:
                skillButton3.GetComponentInChildren<Text>().text = "Animal Assault";
                break;
            case 44:
                skillButton3.GetComponentInChildren<Text>().text = "Cheap Shot";
                break;
            case 45:
                skillButton3.GetComponentInChildren<Text>().text = "Magical Onslaught";
                break;
            case 46:
                skillButton3.GetComponentInChildren<Text>().text = "Revive";
                break;
            case 47:
                skillButton3.GetComponentInChildren<Text>().text = "Bullseye";
                break;
            case 48:
            case 55:
                skillButton3.GetComponentInChildren<Text>().text = "Assassinate";
                break;
            case 49:
                skillButton3.GetComponentInChildren<Text>().text = "Guardian Angel";
                break;
            case 50:
                skillButton3.GetComponentInChildren<Text>().text = "Execute";
                break;
            case 51:
                skillButton3.GetComponentInChildren<Text>().text = "Dragonfire";
                break;
            case 52:
                skillButton3.GetComponentInChildren<Text>().text = "Hellstrike";
                break;
            case 54:
                skillButton3.GetComponentInChildren<Text>().text = "Pilfer";
                break;
            case 56:
                skillButton3.GetComponentInChildren<Text>().text = "Mythic Assault";
                break;
            case 58:
                skillButton3.GetComponentInChildren<Text>().text = "Mass Hysteria";
                break;
            default:
                break;
        }
    }

    public void ButtonOneAnimal(UnitData movingUnit)
    {
        switch (movingUnit.GetCharClass())
        {
            case 0:
                skillButton1.GetComponentInChildren<Text>().text = "Nip";
                break;
            case 1:
            case 3:
            case 5:
            case 8:
            case 9:
                skillButton1.GetComponentInChildren<Text>().text = "Bite";
                break;
            case 2:
            case 4:
            case 7:
                skillButton1.GetComponentInChildren<Text>().text = "Gore";
                break;
            case 6:
                skillButton1.GetComponentInChildren<Text>().text = "Claw";
                break;
            default:
                break;
        }
    }

    public void ButtonTwoAnimal(UnitData movingUnit)
    {
        switch (movingUnit.GetCharClass())
        {
            case 3:
            case 5:
                skillButton2.GetComponentInChildren<Text>().text = "Savage";
                break;
            case 4:
            case 7:
            case 8:
                skillButton2.GetComponentInChildren<Text>().text = "Trample";
                break;
            case 6:
                skillButton2.GetComponentInChildren<Text>().text = "Peck";
                break;
            case 9:
                skillButton2.GetComponentInChildren<Text>().text = "Nip";
                break;
            default:
                break;
        }
    }

    public void ButtonThreeAnimal(UnitData movingUnit)
    {
        switch (movingUnit.GetCharClass())
        {
            case 7:
                skillButton3.GetComponentInChildren<Text>().text = "Stomp";
                break;
            case 8:
            case 9:
                skillButton3.GetComponentInChildren<Text>().text = "Drown";
                break;
            default:
                break;
        }
    }

    public void ButtonOneMyth(UnitData movingUnit)
    {
        switch (movingUnit.GetCharClass())
        {
            case 0:
            case 1:
            case 7:
            case 9:
            case 13:
                skillButton1.GetComponentInChildren<Text>().text = "Claw";
                break;
            case 2:
            case 6:
                skillButton1.GetComponentInChildren<Text>().text = "Bash";
                break;
            case 3:
            case 5:
                skillButton1.GetComponentInChildren<Text>().text = "Slash";
                break;
            case 4:
            case 11:
            case 12:
                skillButton1.GetComponentInChildren<Text>().text = "Bite";
                break;
            case 8:
            case 10:
                skillButton1.GetComponentInChildren<Text>().text = "Nip";
                break;
            default:
                break;
        }
    }

    public void ButtonTwoMyth(UnitData movingUnit)
    {
        switch (movingUnit.GetCharClass())
        {
            case 0:
                skillButton2.GetComponentInChildren<Text>().text = "Slice";
                break;
            case 1:
                skillButton2.GetComponentInChildren<Text>().text = "Nip";
                break;
            case 2:
                skillButton2.GetComponentInChildren<Text>().text = "Hack";
                break;
            case 3:
            case 10:
                skillButton2.GetComponentInChildren<Text>().text = "Snipe";
                break;
            case 4:
                skillButton2.GetComponentInChildren<Text>().text = "Sting";
                break;
            case 5:
                skillButton2.GetComponentInChildren<Text>().text = "Poison";
                break;
            case 6:
                skillButton2.GetComponentInChildren<Text>().text = "Knockout";
                break;
            case 7:
            case 11:
            case 13:
                skillButton2.GetComponentInChildren<Text>().text = "Savage";
                break;
            case 8:
                skillButton2.GetComponentInChildren<Text>().text = "Sleep";
                break;
            case 9:
                skillButton2.GetComponentInChildren<Text>().text = "Peck";
                break;
            case 12:
                skillButton2.GetComponentInChildren<Text>().text = "Trample";
                break;
            default:
                break;
        }
    }

    public void ButtonThreeMyth(UnitData movingUnit)
    {
        switch (movingUnit.GetCharClass())
        {
            case 9:
                skillButton3.GetComponentInChildren<Text>().text = "Rebirth";
                break;
            case 10:
                skillButton3.GetComponentInChildren<Text>().text = "Petrify";
                break;
            case 11:
                skillButton3.GetComponentInChildren<Text>().text = "Hellfire";
                break;
            case 12:
                skillButton3.GetComponentInChildren<Text>().text = "Plague";
                break;
            case 13:
                skillButton3.GetComponentInChildren<Text>().text = "Dragonfire";
                break;
            default:
                break;
        }
    }

    public void ButtonThreeHighlight()
    {
        skillButton1.GetComponentInChildren<Text>().color = Color.cyan;
        skillButton1.GetComponentInChildren<Text>().fontSize = 14;
        skillButton2.GetComponentInChildren<Text>().color = Color.cyan;
        skillButton2.GetComponentInChildren<Text>().fontSize = 14;
        skillButton3.GetComponentInChildren<Text>().color = Color.green;
        skillButton3.GetComponentInChildren<Text>().fontSize = 18;
    }

    public void ButtonTwoHighlight()
    {
        skillButton1.GetComponentInChildren<Text>().color = Color.cyan;
        skillButton1.GetComponentInChildren<Text>().fontSize = 14;
        skillButton2.GetComponentInChildren<Text>().color = Color.green;
        skillButton2.GetComponentInChildren<Text>().fontSize = 18;
        skillButton3.GetComponentInChildren<Text>().color = Color.cyan;
        skillButton3.GetComponentInChildren<Text>().fontSize = 14;
    }

    public void ButtonOneHighlight()
    {
        skillButton1.GetComponentInChildren<Text>().color = Color.green;
        skillButton1.GetComponentInChildren<Text>().fontSize = 18;
        skillButton2.GetComponentInChildren<Text>().color = Color.cyan;
        skillButton2.GetComponentInChildren<Text>().fontSize = 14;
        skillButton3.GetComponentInChildren<Text>().color = Color.cyan;
        skillButton3.GetComponentInChildren<Text>().fontSize = 14;
    }

    public void SetSaveText(string result)
    {
        saveText.SetActive(true);
        saveText.GetComponent<Text>().text = result;
        Invoke("HideSaveText", 2f);
    }

    public void HideSaveText()
    {
        saveText.SetActive(false);
    }

}
