using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public static GameController Instance;

    public int levelsCompleted;
    public int restarts;

    //For data saving
    public List<TownScript> towns = new List<TownScript>();
    public List<AmbushTrigger> ambush = new List<AmbushTrigger>();

    //public int currentStage = 0;

    public GameObject[] maleArmyLeads;
    public GameObject[] maleUnitPrefabs;
    public GameObject[] femaleArmyLeads;
    public GameObject[] femaleUnitPrefabs;
    public GameObject[] projectiles;

    private Vector3[] smallPlayerPositions = new Vector3[25];
    private Vector3[] mediumPlayerPositions = new Vector3[25];
    private Vector3[] largePlayerPositions = new Vector3[25];

    private Vector3[] smallEnemyPositions = new Vector3[25];
    private Vector3[] mediumEnemyPositions = new Vector3[25];
    private Vector3[] largeEnemyPositions = new Vector3[25];

    //public Platoon[] playerPlatoons;
    //public Platoon[] enemyPlatoons;
    //public List<PlayerPlatoonController> playerPlatoons = new List<PlayerPlatoonController>();
    //public List<EnemyPlatoonController> enemyPlatoons = new List<EnemyPlatoonController>();

    public bool isPlatoonSelected = false;
    public PlayerPlatoonController selectedPlatoon;

    public GameObject selectedMarker;

    public bool armyPause = false;
    public bool battleArmyPause = false;
    public bool inBattle = false;
    public bool autoBattle = false;

    public PlayerPlatoonController playerFighting;
    private EnemyPlatoonController enemyFighting;

    public bool difficulty = false;

    public GameObject healthbarPrefab;
    //public GameObject maxLevelImage;

    public bool heroInPlatoon = true;
    public int heroPlatoonNum = 0;

    public bool gameLoadedMidLevel = false;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += BackToMenu;
    }

    void Awake() {
        if (Instance != null && Instance != this) {
            DestroyImmediate(this);
            return;
        }

        //Screen.SetResolution(800, 600, false);

        Instance = this;
        DontDestroyOnLoad(this);
    }

    // Use this for initialization
    void Start() {
        levelsCompleted = 0;
        //LevelController.Instance.battleCamera.SetActive(false);
       
        
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(1) && isPlatoonSelected)
        {
            MoveUnit();
        }
    }

    private void BackToMenu(Scene scene, LoadSceneMode mode)
    {
        GlobalMapController.Instance.HideFightButton();
        CanvasController.Instance.globalMenuButton.SetActive(true);

        Debug.Log("Back to Menu " + ArmyData.Instance.units.Count + " " + levelsCompleted + " " + scene.name);

        if (SceneManager.GetActiveScene().buildIndex == 0 && levelsCompleted > 0)
        {            
            CanvasController.Instance.ShowMap();
            CanvasController.Instance.inGlobalMenu = true;
            CanvasController.Instance.levelGUI.SetActive(false);
        }
        else if(SceneManager.GetActiveScene().buildIndex != 0)
        {
            CanvasController.Instance.inGlobalMenu = false;
            CanvasController.Instance.levelGUI.SetActive(true);
        }
        else
        {
            CanvasController.Instance.globalMenuButton.SetActive(false);
        }
    }

    public void GameContinued()
    {
        foreach (UnitData playerUnit in ArmyData.Instance.units)
        {
            playerUnit.ArmyStart();
        }
        Debug.Log("Game Continued " + ArmyData.Instance.units.Count + " " + levelsCompleted);
        if(levelsCompleted > 0)
        {
            CanvasController.Instance.globalMenuButton.SetActive(true);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void EasyDifficulty()
    {
        difficulty = false;
        ArmyData.Instance.StartingArmy();
    }

    public void HardDifficulty()
    {
        difficulty = true;
        ArmyData.Instance.StartingArmy();
    }    

    public void SetPlayerPlatoon(int platoonNum)
    {
        if(ArmyData.Instance.platoons[platoonNum] == null)
        {
            return;
        }

        GameObject g;
        UnitData leader = ArmyData.Instance.platoons[platoonNum].GetLeader();
        LevelController.Instance.playerPlatoons[platoonNum] = ArmyData.Instance.platoons[platoonNum];
        LevelController.Instance.playerPlatoonsAdded++;
        LevelController.Instance.playerPlatoonsRemaining++;
        LevelController.Instance.startingPlayerPlatoonCount++;

        int leaderNum = leader.GetCharClass();
        if (leader.IsMale())
        {
            g = (GameObject)Instantiate(maleArmyLeads[leaderNum], LevelController.Instance.playerStartingPoint, Quaternion.identity);            
        }
        else
        {
            g = (GameObject)Instantiate(femaleArmyLeads[leaderNum], LevelController.Instance.playerStartingPoint, Quaternion.identity);
        }
        g.gameObject.tag = "Player";
        g.transform.SetParent(GameObject.Find("PlayerLeaders").transform);
        g.transform.localPosition = LevelController.Instance.playerStartingPoint;


        g.AddComponent<PlayerPlatoonController>();
        g.GetComponent<PlayerPlatoonController>().moveTime = 15;
        g.GetComponent<PlayerPlatoonController>().platoonNum = platoonNum;
        LevelController.Instance.playerPlatoonControllers[platoonNum] = g.GetComponent<PlayerPlatoonController>();
       
        for (int i = 0; i < 25; i++)
        {
            if (ArmyData.Instance.platoons[platoonNum].units[i] != null)
            {
                ArmyData.Instance.platoons[platoonNum].units[i].ArmyStart();
            }
                //e.ArmyStart();
        }
        

    }

    public void MoveUnit()
    {
        //Vector3 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        if(Camera.main != null)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            selectedPlatoon.MoveUnit(mousePos);
        }
        
    }

    public void Fight(PlayerPlatoonController p, EnemyPlatoonController e)
    {
        LevelController.Instance.battleCamera.SetActive(true);
        LevelController.Instance.armyCamera.SetActive(false);
        ArmyPause();
        CanvasController.Instance.ArmiesPausedBigText(false);
        autoBattle = false;
        BattleController.Instance.NewBattle();
        StartBattle(p, e);
    }

    public void AutoFight(PlayerPlatoonController p, EnemyPlatoonController e)
    {
        LevelController.Instance.battleCamera.SetActive(true);
        LevelController.Instance.armyCamera.SetActive(false);
        ArmyPause();
        CanvasController.Instance.ArmiesPausedBigText(false);
        autoBattle = true;
        BattleController.Instance.NewBattle();
        StartBattle(p, e);
    }

    public void FightReturn(bool playerWon, bool unitDestroyed)
    {
        LevelController.Instance.battleCamera.SetActive(false);
        LevelController.Instance.armyCamera.SetActive(true);
        CanvasController.Instance.battleOptionsMenu.SetActive(false);
        //Platoon pPlatoon = LevelController.Instance.playerPlatoons[playerFighting.GetPlatoonNum()];
        //Platoon ePlatoon = LevelController.Instance.enemyPlatoons[enemyFighting.GetPlatoonNum()];

        if (unitDestroyed)
        {
            if (playerWon)
            {
                LevelController.Instance.enemyPlatoonsRemaining--;
                LevelController.Instance.enemyPlatoonControllers[enemyFighting.platoonNum] = null;
                LevelController.Instance.enemyPlatoons[enemyFighting.platoonNum] = null;
                Destroy(enemyFighting.gameObject);
            }
            else
            {
                LevelController.Instance.playerPlatoonsRemaining--;
                LevelController.Instance.playerPlatoonControllers[playerFighting.platoonNum] = null;
                LevelController.Instance.playerPlatoons[playerFighting.platoonNum] = null;
                Destroy(playerFighting.gameObject);
            }

            if (!battleArmyPause)
            {
                ArmyUnpause();
            }
            else
            {
                CanvasController.Instance.ArmiesPausedBigText(true);
            }
            battleArmyPause = false;
        }
        else
        {
            if (!playerWon)
            {
                Retreat(enemyFighting.gameObject, playerFighting.gameObject);
                playerFighting.StopMovement();
            }
            else
            {
                
                Retreat(playerFighting.gameObject, enemyFighting.gameObject);
            }
        }       

        inBattle = false;
        BattleController.Instance.ClearHealthBars();
        CanvasController.Instance.CloseBattleGUI();
    }

    private void Retreat(GameObject winningArmy, GameObject losingArmy)
    {
        float xDif = losingArmy.transform.localPosition.x - winningArmy.transform.localPosition.x;
        float zDif = losingArmy.transform.localPosition.z - winningArmy.transform.localPosition.z;

        //Testing purposes
        Vector3 startPos = losingArmy.transform.localPosition;


        if (Mathf.Abs(xDif) > Mathf.Abs(zDif))
        {
            if(xDif > 0)
            {
                losingArmy.transform.localPosition = new Vector3(losingArmy.transform.localPosition.x + 70, losingArmy.transform.localPosition.y, losingArmy.transform.localPosition.z);
                if (losingArmy.transform.localPosition.x > LevelController.Instance.maxX)
                {
                    losingArmy.transform.localPosition = new Vector3(LevelController.Instance.maxX, losingArmy.transform.localPosition.y, losingArmy.transform.localPosition.z);
                }
            }
            else
            {
                losingArmy.transform.localPosition = new Vector3(losingArmy.transform.localPosition.x - 70, losingArmy.transform.localPosition.y, losingArmy.transform.localPosition.z);
                if (losingArmy.transform.localPosition.x < LevelController.Instance.minX)
                {
                    losingArmy.transform.localPosition = new Vector3(LevelController.Instance.minX, losingArmy.transform.localPosition.y, losingArmy.transform.localPosition.z);
                }
            }
        }
        else
        {
            if (zDif > 0)
            {
                losingArmy.transform.localPosition = new Vector3(losingArmy.transform.localPosition.x, losingArmy.transform.localPosition.y, losingArmy.transform.localPosition.z + 70);
                if (losingArmy.transform.localPosition.z > LevelController.Instance.maxZ)
                {
                    losingArmy.transform.localPosition = new Vector3(losingArmy.transform.localPosition.x, losingArmy.transform.localPosition.y, LevelController.Instance.maxZ);
                }
            }
            else
            {
                losingArmy.transform.localPosition = new Vector3(losingArmy.transform.localPosition.x, losingArmy.transform.localPosition.y, losingArmy.transform.localPosition.z - 70);
                if (losingArmy.transform.localPosition.z < LevelController.Instance.minZ)
                {
                    losingArmy.transform.localPosition = new Vector3(losingArmy.transform.localPosition.x, losingArmy.transform.localPosition.y, LevelController.Instance.minZ);
                }
            }
        }

    }

    public void ArmyPause()
    {
        armyPause = true;
        CanvasController.Instance.ArmiesPausedBigText(true);
    }

    public void ArmyUnpause()
    {
        armyPause = false;
        CanvasController.Instance.ArmiesPausedBigText(false);
    }

    /*public void LevelLost()
    {
        GlobalMapController.Instance.MapButtonReset();
        //inBattle = false;
        //BattleController.Instance.ClearHealthBars();
        //CanvasController.Instance.CloseBattleGUI();
        foreach (UnitData playerUnit in ArmyData.Instance.units)
        {
            playerUnit.ArmyStart();
        }
        SceneManager.LoadScene(0);
        //CanvasController.Instance.ShowMap();
    }*/

    public void Level1Lost()
    {
        SceneManager.LoadScene(1);
    }

    /*public void LevelWon()
    {
        //CheckDeadUnitsForUndead();
        GlobalMapController.Instance.MapButtonReset();
        foreach (UnitData playerUnit in ArmyData.Instance.units)
        {
            playerUnit.ArmyStart();
        }
        SceneManager.LoadScene(0);
    }*/

    public void LevelOver()
    {
        GlobalMapController.Instance.MapButtonReset();
        gameLoadedMidLevel = false;
        foreach (UnitData playerUnit in ArmyData.Instance.units)
        {
            playerUnit.ArmyStart();
        }
        SceneManager.LoadScene(0);
    }

    private void CheckDeadUnitsForUndead()
    {

    }

    /**********************
     * BATTLE CODE
     * ***********************/

    public void StartBattle(PlayerPlatoonController p, EnemyPlatoonController e)
    {
        if (!inBattle)
        {
            playerFighting = p;
            enemyFighting = e;
            //Platoon pPlatoon = ArmyData.Instance.platoons[playerFighting.GetPlatoonNum()];
            Platoon pPlatoon = LevelController.Instance.playerPlatoons[playerFighting.GetPlatoonNum()];
            Platoon ePlatoon = LevelController.Instance.enemyPlatoons[enemyFighting.GetPlatoonNum()];
            
            CanvasController.Instance.OpenBattleGUI();
            for (int i = 0; i < pPlatoon.units.Length; i++)
            {
                if (pPlatoon.units[i] != null && !pPlatoon.units[i].isDead())
                {
                    //if(pPlatoon.units[i].GetClassSize() == 1)
                    //{
                        SetUnit(pPlatoon.units[i], true);
                   // }

                }
            }

            for (int i = 0; i < ePlatoon.units.Length; i++)
            {
                if (ePlatoon.units[i] != null && !ePlatoon.units[i].isDead())
                {
                    SetUnit(ePlatoon.units[i], false);
                }
            }
            inBattle = true;
        }        
    }


    public void SetUnit(UnitData unit, bool playerUnit)
    {
        GameObject g;
        if (unit.IsMale())
        {
            if (unit.GetCharType() == 1)
            {
                g = Instantiate(maleUnitPrefabs[unit.GetCharClass()], new Vector3(0f, 0f, 0f), Quaternion.identity);
            }
            else if (unit.GetCharType() == 2)
            {
                g = Instantiate(maleUnitPrefabs[unit.GetCharClass() + 63], new Vector3(0f, 0f, 0f), Quaternion.identity);
            }
            else if (unit.GetCharType() == 3)
            {
                g = Instantiate(maleUnitPrefabs[unit.GetCharClass() + 73], new Vector3(0f, 0f, 0f), Quaternion.identity);
            }
            else
            {
                g = Instantiate(maleUnitPrefabs[unit.GetCharClass() + 59], new Vector3(0f, 0f, 0f), Quaternion.identity);
            }
            
            
        }
        else
        {
            if (unit.GetCharType() == 1)
            {
                g = Instantiate(femaleUnitPrefabs[unit.GetCharClass()], new Vector3(0f, 0f, 0f), Quaternion.identity);
            }
            else if (unit.GetCharType() == 2)
            {
                g = Instantiate(femaleUnitPrefabs[unit.GetCharClass() + 63], new Vector3(0f, 0f, 0f), Quaternion.identity);
            }
            else if (unit.GetCharType() == 3)
            {
                g = Instantiate(femaleUnitPrefabs[unit.GetCharClass() + 73], new Vector3(0f, 0f, 0f), Quaternion.identity);
            }
            else
            {
                g = Instantiate(femaleUnitPrefabs[unit.GetCharClass() + 59], new Vector3(0f, 0f, 0f), Quaternion.identity);
            }
        }

        if(unit.GetClassSize() == 1)
        {
            SetSmallUnit(g, playerUnit, unit);
        }else if(unit.GetClassSize() == 2)
        {
            SetMediumUnit(g, playerUnit, unit);
        }
        else if(unit.GetClassSize() == 3)
        {
            SetLargeUnit(g, playerUnit, unit);
        }

        //unit.SetSpeedCount(0);
    }

    public void SetSmallUnit(GameObject g, bool playerUnit, UnitData unit)
    {
        GameObject hb;
        //GameObject max;
       
        if (playerUnit)
        {
            BattleController.Instance.AddPlayerUnit(unit, g);
            g.transform.SetParent(LevelController.Instance.playerUnitsHolder.transform);
            g.transform.SetPositionAndRotation(new Vector3(0, 0, 0), Quaternion.Euler(0, 150, 0));
            g.transform.localPosition = new Vector3(25 - 5 * (unit.positionNum / 5), 0, -2.5f + 10 * (unit.positionNum % 5));

            hb = Instantiate(healthbarPrefab, new Vector3(0, 0, 0), Quaternion.Euler(55, 0, 0));
            /*max = Instantiate(maxLevelImage, new Vector3(0, 0, 0), Quaternion.identity);
            max.transform.SetParent(hb.transform);
            max.transform.localScale = new Vector3(1, 1, 1);
            max.transform.localRotation = new Quaternion(0, 0, 0, 0);
            max.transform.localPosition = new Vector3(0, 15, 0);
            max.gameObject.SetActive(false);*/
            hb.transform.SetParent(LevelController.Instance.playerUnitsHolder.transform);
            BattleController.Instance.playerHealthBars[unit.positionNum] = hb;
            smallPlayerPositions[unit.positionNum] = g.transform.position;

            unit.AdjustXPBar();
        }
        else
        {
            BattleController.Instance.AddEnemyUnit(unit, g);
            g.transform.SetParent(LevelController.Instance.enemyUnitsHolder.transform);
            g.transform.SetPositionAndRotation(new Vector3(0, 0, 0), Quaternion.Euler(0, -30, 0));
            g.transform.localPosition = new Vector3(5 + 5 * (unit.positionNum / 5), 0, -2.5f + 10 * (unit.positionNum % 5));

            hb = Instantiate(healthbarPrefab, new Vector3(0, 0, 0), Quaternion.Euler(55, 0, 0));
            hb.transform.SetParent(LevelController.Instance.enemyUnitsHolder.transform);
            BattleController.Instance.enemyHealthBars[unit.positionNum] = hb;
            smallEnemyPositions[unit.positionNum] = g.transform.position;
        }
        
        hb.transform.localPosition = new Vector3(g.transform.localPosition.x, g.transform.localPosition.y + 6, g.transform.localPosition.z);
        unit.AdjustHealthBar();
        unit.UpdateSpeedBar();
    }

    public void SetMediumUnit(GameObject g, bool playerUnit, UnitData unit)
    {
        GameObject hb;

        if (playerUnit)
        {
            BattleController.Instance.AddPlayerUnit(unit, g);
            g.transform.SetParent(LevelController.Instance.playerUnitsHolder.transform);
            g.transform.SetPositionAndRotation(new Vector3(0, 0, 0), Quaternion.Euler(0, 150, 0));
            g.transform.localPosition = new Vector3(27.5f - 5 * (unit.positionNum / 5), 0, 10 * (unit.positionNum % 5));
            hb = Instantiate(healthbarPrefab, new Vector3(0, 0, 0), Quaternion.Euler(55, 0, 0));
            hb.transform.SetParent(LevelController.Instance.playerUnitsHolder.transform);
            BattleController.Instance.playerHealthBars[unit.positionNum] = hb;
            mediumPlayerPositions[unit.positionNum] = g.transform.position;
        }
        else
        {
            BattleController.Instance.AddEnemyUnit(unit, g);
            g.transform.SetParent(LevelController.Instance.enemyUnitsHolder.transform);
            g.transform.SetPositionAndRotation(new Vector3(0, 0, 0), Quaternion.Euler(0, -30, 0));
            g.transform.localPosition = new Vector3(2.5f + 5 * (unit.positionNum / 5), 0, 10 * (unit.positionNum % 5));
            hb = Instantiate(healthbarPrefab, new Vector3(0, 0, 0), Quaternion.Euler(55, 0, 0));
            hb.transform.SetParent(LevelController.Instance.enemyUnitsHolder.transform);
            BattleController.Instance.enemyHealthBars[unit.positionNum] = hb;
            mediumEnemyPositions[unit.positionNum] = g.transform.position;
        }
        hb.transform.localPosition = new Vector3(g.transform.localPosition.x, g.transform.localPosition.y + 12, g.transform.localPosition.z);
        unit.AdjustHealthBar();
    }

    public void SetLargeUnit(GameObject g, bool playerUnit, UnitData unit)
    {
        int adjustedPosition = unit.positionNum - 4;
        GameObject hb;

        if (playerUnit)
        {
            BattleController.Instance.AddPlayerUnit(unit, g);
            g.transform.SetParent(LevelController.Instance.playerUnitsHolder.transform);
            g.transform.SetPositionAndRotation(new Vector3(0, 0, 0), Quaternion.Euler(0, 150, 0));
            g.transform.localPosition = new Vector3(25 - 5 * (adjustedPosition / 5), 0, -2.5f + 10 * (adjustedPosition % 5));
            hb = Instantiate(healthbarPrefab, new Vector3(0, 0, 0), Quaternion.Euler(55, 0, 0));
            hb.transform.SetParent(LevelController.Instance.playerUnitsHolder.transform);
            BattleController.Instance.playerHealthBars[unit.positionNum] = hb;
            largePlayerPositions[unit.positionNum] = g.transform.position;
        }
        else
        {
            BattleController.Instance.AddEnemyUnit(unit, g);
            g.transform.SetParent(LevelController.Instance.enemyUnitsHolder.transform);
            g.transform.SetPositionAndRotation(new Vector3(0, 0, 0), Quaternion.Euler(0, -30, 0));
            g.transform.localPosition = new Vector3(5 + 5 * (adjustedPosition / 5), 0, -2.5f + 10 * (adjustedPosition % 5));
            hb = Instantiate(healthbarPrefab, new Vector3(0, 0, 0), Quaternion.Euler(55, 0, 0));
            hb.transform.SetParent(LevelController.Instance.enemyUnitsHolder.transform);
            BattleController.Instance.enemyHealthBars[unit.positionNum] = hb;
            largeEnemyPositions[unit.positionNum] = g.transform.position;
        }
        hb.transform.localPosition = new Vector3(g.transform.localPosition.x, g.transform.localPosition.y + 16, g.transform.localPosition.z);
        unit.AdjustHealthBar();
    }

    public Vector3 GetPlayerMiddlePosition()
    {
        return new Vector3(-907, 5, -945);
    }

    public Vector3 GetEnemyMiddlePosition()
    {
        return new Vector3(-876, 5, -945);
    }

    public Vector3 GetUnitPosition(bool playerTeam, int unitSize, int unitPosition)
    {
        if (playerTeam)
        {
            return GetPlayerUnitPosition(unitSize, unitPosition);
        }
        else
        {
            return GetEnemyUnitPosition(unitSize, unitPosition);
        }
    }

    public Vector3 GetPlayerUnitPosition(int unitSize, int unitPosition)
    {
        if (unitSize == 1)
        {
            return smallPlayerPositions[unitPosition];
        }else if(unitSize == 2)
        {
            return mediumPlayerPositions[unitPosition];
        }
        else
        {
            return largePlayerPositions[unitPosition];
        }
    }

    public Vector3 GetEnemyUnitPosition(int unitSize, int unitPosition)
    {
        if (unitSize == 1)
        {
            return smallEnemyPositions[unitPosition];
        }
        else if (unitSize == 2)
        {
            return mediumEnemyPositions[unitPosition];
        }
        else
        {
            return largeEnemyPositions[unitPosition];
        }
    }

    public void SetHeroPlatoonNum(int num)
    {
        Debug.Log("Setting hero in platoon: " + num);
        heroPlatoonNum = num;
    }
}
