using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelController : MonoBehaviour
{

    public static LevelController Instance;

    //public GameObject[] enemyUnits;
    //public GameObject[] playerUnits;


    public Vector3[] chokePoints;

    public int playerUnitCount;
    public int enemyUnitCount;

    public List<UnitData> enemyUnits = new List<UnitData>();

    public int levelNum;

    public GameObject armyCamera;
    public GameObject battleCamera;

    public GameObject playerUnitsHolder;
    public GameObject enemyUnitsHolder;

    //public List<Platoon> enemyPlatoons = new List<Platoon>();
    //public List<Platoon> playerPlatoons = new List<Platoon>();
    public Platoon[] enemyPlatoons = new Platoon[20];
    public Platoon[] playerPlatoons = new Platoon[20];

    public PlayerPlatoonController[] playerPlatoonControllers = new PlayerPlatoonController[20];
    public EnemyPlatoonController[] enemyPlatoonControllers = new EnemyPlatoonController[20];
    

    public int playerPlatoonsRemaining = 0;
    public int enemyPlatoonsRemaining = 0;
    public int startingPlayerPlatoonCount = 0;
    public int startingEnemyPlatoonCount = 0;
    public int playerPlatoonsAdded = 0;

    public float maxX;
    public float maxZ;
    public float minX;
    public float minZ;
    public Vector3 playerStartingPoint;

    public int playerControlledTowns = 0;

    public bool playerBaseCaptured = false;
    public bool enemyBaseCaptured = false;

    public bool heroDead = false;

    private bool levelWon = false;

    public bool levelLost = false;

    public int maxDeployedPlatoon = 0;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            DestroyImmediate(this);
            return;
        }

        Instance = this;
    }

    // Use this for initialization
    void Start()
    {
        if (GameController.Instance.gameLoadedMidLevel)
        {
            LoadMidLevelEnemyUnitData(LocalSave.Instance.GetEnemyArmy());
            SetTownOwnership(LocalSave.Instance.GetBaseOwnership());
            SetAmbushUsed(LocalSave.Instance.GetAmbush());

             for (int i = 0; i < ArmyData.Instance.platoons.Count; i++)
             {
                 GameController.Instance.SetPlayerPlatoon(i);
            }
            SetLeaderLocations(LocalSave.Instance.GetPlayerLeaderLocs(), LocalSave.Instance.GetEnemyLeaderLocs());
            BattleController.Instance.StartBattle();
            return;
        }

        playerPlatoonsRemaining = 0;
        enemyPlatoonsRemaining = 0;
        startingPlayerPlatoonCount = 0;
        startingEnemyPlatoonCount = 0;
        playerPlatoonsAdded = 0;
        Debug.Log("Level Controller START");
        //Sets speed counters
        BattleController.Instance.StartBattle();

        foreach (UnitData playerUnit in ArmyData.Instance.units)
        {
            playerUnit.ArmyStart();
        }

        CanvasController.Instance.LevelStart();

        /*foreach (Platoon p in ArmyData.Instance.platoons)
        {
            playerPlatoons.Add(p);
        }*/

        switch (levelNum)
        {
            case 1:
                Level1Setup();
                StoryController.Instance.ShowStory(1);
                break;
            case 2:
                Level2Setup();
                StoryController.Instance.ShowStory(2);
                break;
            case 3:
                Level3Setup();
                StoryController.Instance.ShowStory(3);
                break;
            case 4:
                Level4Setup();
                StoryController.Instance.ShowStory(4);
                break;
            case 5:
                Level5Setup();
                StoryController.Instance.ShowStory(5);
                break;
            case 6:
                Level6Setup();
                StoryController.Instance.ShowStory(6);
                break;
            case 7:
                Level7Setup();
                StoryController.Instance.ShowStory(7);
                break;
            case 8:
                Level8Setup();
                StoryController.Instance.ShowStory(8);
                break;
            case 9:
                Level9Setup();
                StoryController.Instance.ShowStory(9);
                break;
            case 10:
                Level10Setup();
                StoryController.Instance.ShowStory(10);
                break;
            case 11:
                Level11Setup();
                StoryController.Instance.ShowStory(11);
                break;
            case 12:
                Level12Setup();
                StoryController.Instance.ShowStory(12);
                break;
            case 13:
                Level13Setup();
                StoryController.Instance.ShowStory(13);
                break;
            case 14:
                Level14Setup();
                StoryController.Instance.ShowStory(14);
                break;
            case 15:
                Level15Setup();
                StoryController.Instance.ShowStory(15);
                break;
            case 16:
                Level16Setup();
                StoryController.Instance.ShowStory(16);
                break;
            case 17:
                Level17Setup();
                StoryController.Instance.ShowStory(17);
                break;
            case 18:
                Level18Setup();
                StoryController.Instance.ShowStory(18);
                break;
            case 19:
                Level19Setup();
                StoryController.Instance.ShowStory(19);
                break;
            case 20:
                Level20Setup();
                StoryController.Instance.ShowStory(20);
                break;
            case 21:
                Level21Setup();
                StoryController.Instance.ShowStory(21);
                break;
            case 22:
                Level22Setup();
                StoryController.Instance.ShowStory(22);
                break;
            case 23:
                Level23Setup();
                StoryController.Instance.ShowStory(23);
                break;
            case 24:
                Level24Setup();
                StoryController.Instance.ShowStory(24);
                break;
            case 25:
                Level25Setup();
                StoryController.Instance.ShowStory(25);
                break;
            case 26:
                Level26Setup();
                StoryController.Instance.ShowStory(26);
                break;
            case 27:
                Level27Setup();
                StoryController.Instance.ShowStory(27);
                break;
            case 28:
                Level28Setup();
                StoryController.Instance.ShowStory(28);
                break;
            case 29:
                Level29Setup();
                StoryController.Instance.ShowStory(29);
                break;
            case 30:
                Level30Setup();
                StoryController.Instance.ShowStory(30);
                break;
            default:
                break;
        }
        startingEnemyPlatoonCount = enemyPlatoonsRemaining;
        SoundController.Instance.PlayGameMusic(levelNum - 1);
        ArmyStart();
    }

    // Update is called once per frame
    void Update()
    {
        if(levelNum > 1)
        {
            LevelUpdate();
        }

        switch (levelNum)
        {
            case 1:
                Level1Update();
                break;
            /*case 2:
                Level2Update();
                break;
            case 3:
                Level3Update();
                break;*/
            default:
                break;
        }
    }

    public void SaveGameLevelWon()
    {
        switch (levelNum)
        {
            case 1:
                Level1Won();
                StoryController.Instance.ShowStory(35);                
                break;
            case 2:
                Level2Won();
                StoryController.Instance.ShowStory(36);
                break;
            case 3:
                Level3Won();
                StoryController.Instance.ShowStory(38);
                break;
            case 4:
                Level4Won();
                StoryController.Instance.ShowStory(39);
                break;
            case 5:
                Level5Won();
                StoryController.Instance.ShowStory(40);
                break;
            case 6:
                Level6Won();
                StoryController.Instance.ShowStory(41);
                break;
            case 7:
                Level7Won();
                StoryController.Instance.ShowStory(42);
                break;
            case 8:
                Level8Won();
                StoryController.Instance.ShowStory(43);
                break;
            case 9:
                Level9Won();
                StoryController.Instance.ShowStory(44);
                break;
            case 10:
                Level10Won();
                StoryController.Instance.ShowStory(45);
                break;
            case 11:
                Level11Won();
                StoryController.Instance.ShowStory(46);
                break;
            case 12:
                Level12Won();
                StoryController.Instance.ShowStory(47);
                break;
            case 13:
                Level13Won();
                StoryController.Instance.ShowStory(48);
                break;
            case 14:
                Level14Won();
                StoryController.Instance.ShowStory(49);
                break;
            case 15:
                Level15Won();
                StoryController.Instance.ShowStory(50);
                break;
            case 16:
                Level16Won();
                StoryController.Instance.ShowStory(51);
                break;
            case 17:
                Level17Won();
                StoryController.Instance.ShowStory(52);
                break;
            case 18:
                Level18Won();
                StoryController.Instance.ShowStory(53);
                break;
            case 19:
                Level19Won();
                StoryController.Instance.ShowStory(54);
                break;
            case 20:
                Level20Won();
                StoryController.Instance.ShowStory(55);
                break;
            case 21:
                Level21Won();
                StoryController.Instance.ShowStory(56);
                break;
            case 22:
                Level22Won();
                StoryController.Instance.ShowStory(57);
                break;
            case 23:
                Level23Won();
                StoryController.Instance.ShowStory(58);
                break;
            case 24:
                Level24Won();
                StoryController.Instance.ShowStory(59);
                break;
            case 25:
                Level25Won();
                StoryController.Instance.ShowStory(60);
                break;
            case 26:
                Level26Won();
                StoryController.Instance.ShowStory(61);
                break;
            case 27:
                Level27Won();
                StoryController.Instance.ShowStory(62);
                break;
            case 28:
                Level28Won();
                StoryController.Instance.ShowStory(63);
                break;
            case 29:
                Level29Won();
                StoryController.Instance.ShowStory(64);
                break;
            case 30:
                Level30Won();
                StoryController.Instance.ShowStory(65);
                break;
            default:
                break;
        }
    }

    private void AddEnemyUnit(int charType, int position, int charClass, bool leader, int platoon, int level)
    {

        if (charType == 1)
        {
            HumanData u = new HumanData(false);
            AddEnemyUnitData(u, position, charClass, leader, platoon, level, charType);
        }
        else if (charType == 2)
        {
            AnimalData u = new AnimalData(false);
            AddEnemyUnitData(u, position, charClass, leader, platoon, level, charType);
        }
        else
        {
            MythData u = new MythData(false);
            AddEnemyUnitData(u, position, charClass, leader, platoon, level, charType);
        }
    }

    //For leaders. Need to match gender with avatar
    private void AddEnemyUnit(int charType, int position, int charClass, bool leader, int platoon, int level, bool gender)
    {

        if (charType == 1)
        {
            HumanData u = new HumanData(gender, false);
            AddEnemyUnitData(u, position, charClass, leader, platoon, level, charType);
        }
        else if (charType == 2)
        {
            AnimalData u = new AnimalData(false);
            AddEnemyUnitData(u, position, charClass, leader, platoon, level, charType);
        }
        else
        {
            MythData u = new MythData(false);
            AddEnemyUnitData(u, position, charClass, leader, platoon, level, charType);
        }
    }

    private void AddEnemyUnitData(UnitData u, int position, int charClass, bool leader, int platoon, int level, int charType)
    {
        u.SetPlatoonPosition(platoon, position);
        int maxClassNum = 30;

        if(GameController.Instance.levelsCompleted >= 30)
        {
            int timesWon = GameController.Instance.levelsCompleted / 30;
            level += (timesWon * 100);
        }

        if(charType == 1)
        {
            maxClassNum = 10;
        }

        if (!GameController.Instance.difficulty)
        {
            level *= 2;
        }

        //Make sure enemy level stays at max
        if(charType == 1)
        {
            //Human

        }else if(charType == 2 && level >= 270)
        {
            //Animal
            level = 270;
        }
        else if(charType == 3 && level >= 420)
        {
            //Myth
            level = 420;
        }
        
        if (leader)
        {
            u.specialCharNum = 2;
            enemyPlatoons[platoon].SetLeader(u);
        }
        
        int tempClass = 0;

        for (int i = level; i > 0; i--)
        {
            

            if (i % maxClassNum == 0 && ((GameController.Instance.difficulty && i != level) || (!GameController.Instance.difficulty && i != 2*level)))
                //if (i % maxClassNum == 0 && i > 2 * maxClassNum)
            {
                tempClass++;
                //Debug.Log("TempClass ++ " + tempClass + " " + i);
               // u.SetEnemyClass(tempClass);
            }

            if (tempClass == charClass)
            {
                //Debug.Log("Temp = char " + tempClass + " i: " + i);

                tempClass++;
            }

            u.SetEnemyClass(tempClass);

            if (i <= maxClassNum)
            {
                u.SetEnemyClass(charClass);
            }
            u.LevelUp();

            
        }
        u.SetEnemyClass(charClass);
        enemyPlatoons[platoon].AddUnit(u, position);
        enemyUnits.Add(u);
    }
   

    private void Level1Update()
    {
        if (enemyPlatoonsRemaining <= 0 && !levelWon)
        {
            levelWon = true;
            //Level1Won();
            CanvasController.Instance.LevelWon();
        }

        if (playerPlatoonsRemaining <= 0)
        {
            CanvasController.Instance.LevelLost();
        }
    }   

    private void LevelUpdate()
    {
        if (!levelLost && (playerPlatoonsRemaining <= 0
            || playerBaseCaptured || heroDead)
            )
        {
            if (heroDead)
            {
                Invoke("DelayFightReturn", 1);
            }

            CanvasController.Instance.LevelLost();
            levelLost = true;
        }

        if ((enemyPlatoonsRemaining <= 0 || (GameController.Instance.difficulty && enemyBaseCaptured)) && !levelWon)
        {
            levelWon = true;
            LevelWon();
            //Level2Won();
            CanvasController.Instance.LevelWon();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {

            int nextPlatoonNum = -1;
            if (GameController.Instance.selectedPlatoon == null)
            {
                nextPlatoonNum = 0;
            }
            else
            {
                nextPlatoonNum = GameController.Instance.selectedPlatoon.platoonNum;
                do
                {
                    nextPlatoonNum++;
                    if (nextPlatoonNum == ArmyData.Instance.platoons.Count)
                    {
                        nextPlatoonNum = 0;
                    }
                }
                while (LevelController.Instance.playerPlatoonControllers[nextPlatoonNum] == null);

            }
            LevelController.Instance.playerPlatoonControllers[nextPlatoonNum].isSelected = true;
            if (GameController.Instance.isPlatoonSelected && GameController.Instance.selectedPlatoon != null)
            {
                GameController.Instance.selectedPlatoon.Deselect();
            }
            GameController.Instance.isPlatoonSelected = true;
            GameController.Instance.selectedPlatoon = LevelController.Instance.playerPlatoonControllers[nextPlatoonNum];
            CanvasController.Instance.selectedPlatoonText.text = ArmyData.Instance.platoons[nextPlatoonNum].GetLeader().unitName;
            if (LevelController.Instance.playerPlatoonControllers[nextPlatoonNum].selectedMarker != null)
            {
                Destroy(LevelController.Instance.playerPlatoonControllers[nextPlatoonNum].selectedMarker);
            }
            LevelController.Instance.playerPlatoonControllers[nextPlatoonNum].selectedMarker = Instantiate(GameController.Instance.selectedMarker, LevelController.Instance.playerPlatoonControllers[nextPlatoonNum].gameObject.transform);
            LevelController.Instance.playerPlatoonControllers[nextPlatoonNum].selectedMarker.gameObject.transform.position =
                new Vector3(LevelController.Instance.playerPlatoonControllers[nextPlatoonNum].selectedMarker.gameObject.transform.position.x,
                LevelController.Instance.playerPlatoonControllers[nextPlatoonNum].selectedMarker.gameObject.transform.position.y + 5,
                LevelController.Instance.playerPlatoonControllers[nextPlatoonNum].selectedMarker.gameObject.transform.position.z);
        }
    }

    private void DelayFightReturn()
    {
        GameController.Instance.FightReturn(false, false);
    }



    /*private void Level2Update()
    {
        if ((enemyPlatoonsRemaining <= 0 || enemyBaseCaptured) && !levelWon)
        {
            levelWon = true;
            LevelWon();
            //Level2Won();
            CanvasController.Instance.LevelWon();
        }        
    }

    private void Level3Update()
    {
        if ((enemyPlatoonsRemaining <= 0 || enemyBaseCaptured) && !levelWon)
        {
            levelWon = true;
            LevelWon();
           // Level3Won();
            CanvasController.Instance.LevelWon();
        }
    }*/

    private void LevelWon()
    {
        foreach (UnitData u in ArmyData.Instance.units)
        {
            for (int i = 0; i < playerControlledTowns; i++)
            {
                u.GainXP(4, u);
            }
        }
    }

    private void ArmyStart()
    {
        for (int p = 0; p < enemyPlatoonsRemaining; p++)
        {
            for (int i = 0; i < 25; i++)
            {
                if (enemyPlatoons[p].units[i] != null)
                {
                    enemyPlatoons[p].units[i].ArmyStart();
                }
                //e.ArmyStart();
            }
        }
    }

    private void Level1Setup()
    {
        Platoon p = new Platoon(0);
        enemyPlatoons[0] = p;
        enemyPlatoonsRemaining++;

        AddEnemyUnit(1, 10, 7, true, 0, 0, false);

        AddEnemyUnit(2, 4, 2, false, 0, 0);

        AddEnemyUnit(2, 12, 1, false, 0, 0);

        AddEnemyUnit(2, 18, 0, false, 0, 0);

        AddEnemyUnit(1, 0, 3, false, 0, 0);

        AddEnemyUnit(1, 20, 2, false, 0, 0);

        AddEnemyUnit(1, 24, 1, false, 0, 0);

        AddEnemyUnit(1, 6, 0, false, 0, 0);

        AddEnemyUnit(1, 8, 0, false, 0, 0);

        AddEnemyUnit(1, 22, 0, false, 0, 0);


        for (int i = 0; i < 25; i++)
        {
            if (enemyPlatoons[0].units[i] != null)
            {
                enemyPlatoons[0].units[i].ArmyStart();
            }
        }


        GameController.Instance.SetPlayerPlatoon(0);
        maxDeployedPlatoon = 0;
    }

    private void Level2Setup()
    {
        Debug.Log("Level 2 Setup");




        Platoon p0 = new Platoon(0);
        enemyPlatoons[0] = p0;

        Platoon p1 = new Platoon(1);
        enemyPlatoons[1] = p1;

        Platoon p2 = new Platoon(2);
        enemyPlatoons[2] = p2;

        Platoon p3 = new Platoon(3);
        enemyPlatoons[3] = p3;

        Platoon p4 = new Platoon(4);
        enemyPlatoons[4] = p4;
        enemyPlatoonsRemaining = 5;

        //AddEnemyUnit(int charType, int position, int charClass, bool leader, int platoon, int level)
        if (!GameController.Instance.difficulty)
        {
            AddEnemyUnit(1, 23, 6, true, 0, 1, true);
            AddEnemyUnit(2, 3, 2, false, 0, 0);
            AddEnemyUnit(1, 19, 0, false, 0, 0);
            AddEnemyUnit(2, 12, 1, false, 0, 0);
            AddEnemyUnit(2, 10, 0, false, 0, 0);
            AddEnemyUnit(1, 1, 0, false, 0, 0);
            AddEnemyUnit(1, 9, 0, false, 0, 0);
            AddEnemyUnit(1, 16, 0, false, 0, 0);
            AddEnemyUnit(1, 4, 0, false, 0, 0);

            AddEnemyUnit(1, 2, 9, true, 1, 1, true);
            AddEnemyUnit(2, 6, 2, false, 1, 0);
            AddEnemyUnit(2, 15, 1, false, 1, 0);
            AddEnemyUnit(2, 21, 0, false, 1, 0);
            AddEnemyUnit(1, 17, 0, false, 1, 0);
            AddEnemyUnit(1, 22, 0, false, 1, 0);
            AddEnemyUnit(1, 9, 0, false, 1, 0);
            AddEnemyUnit(1, 19, 0, false, 1, 0);
            AddEnemyUnit(1, 4, 0, false, 1, 0);

            AddEnemyUnit(1, 24, 4, true, 2, 1, true);
            AddEnemyUnit(1, 4, 0, false, 2, 0);
            AddEnemyUnit(2, 10, 2, false, 2, 0);
            AddEnemyUnit(2, 14, 1, false, 2, 0);
            AddEnemyUnit(2, 19, 0, false, 2, 0);
            AddEnemyUnit(1, 22, 0, false, 2, 0);
            AddEnemyUnit(1, 9, 0, false, 2, 0);
            AddEnemyUnit(1, 16, 0, false, 2, 0);
            AddEnemyUnit(1, 2, 0, false, 2, 0);

            AddEnemyUnit(1, 2, 10, true, 3, 1, false);
            AddEnemyUnit(1, 3, 0, false, 3, 0);
            AddEnemyUnit(1, 22, 0, false, 3, 0);
            AddEnemyUnit(2, 6, 0, false, 3, 0);
            AddEnemyUnit(2, 8, 1, false, 3, 0);
            AddEnemyUnit(2, 1, 2, false, 3, 0);
            AddEnemyUnit(1, 9, 0, false, 3, 0);
            AddEnemyUnit(1, 19, 0, false, 3, 0);
            AddEnemyUnit(1, 14, 0, false, 3, 0);

            AddEnemyUnit(1, 22, 5, true, 4, 2, false);
            AddEnemyUnit(1, 3, 3, false, 4, 1);
            AddEnemyUnit(2, 5, 5, false, 4, 1);
            AddEnemyUnit(2, 20, 6, false, 4, 1);
            AddEnemyUnit(1, 9, 0, false, 4, 0);
            AddEnemyUnit(1, 19, 0, false, 4, 0);
            AddEnemyUnit(1, 14, 0, false, 4, 0);
            AddEnemyUnit(1, 2, 0, false, 4, 0);
            AddEnemyUnit(1, 16, 0, false, 4, 0);
            AddEnemyUnit(2, 13, 0, false, 4, 1);
            AddEnemyUnit(2, 8, 1, false, 4, 1);
            AddEnemyUnit(2, 4, 2, false, 4, 1);
        }
        else
        {
            AddEnemyUnit(1, 23, 6, true, 0, 2, true);
            AddEnemyUnit(2, 3, 2, false, 0, 1);
            AddEnemyUnit(1, 19, 0, false, 0, 0);
            AddEnemyUnit(2, 12, 1, false, 0, 1);
            AddEnemyUnit(2, 10, 0, false, 0, 1);
            AddEnemyUnit(1, 1, 0, false, 0, 0);
            AddEnemyUnit(1, 9, 0, false, 0, 0);
            AddEnemyUnit(1, 16, 0, false, 0, 0);
            AddEnemyUnit(1, 4, 0, false, 0, 0);

            AddEnemyUnit(1, 2, 9, true, 1, 2, true);
            AddEnemyUnit(2, 6, 2, false, 1, 1);
            AddEnemyUnit(2, 15, 1, false, 1, 1);
            AddEnemyUnit(2, 21, 0, false, 1, 1);
            AddEnemyUnit(1, 17, 0, false, 1, 0);
            AddEnemyUnit(1, 22, 0, false, 1, 0);
            AddEnemyUnit(1, 9, 0, false, 1, 0);
            AddEnemyUnit(1, 19, 0, false, 1, 0);
            AddEnemyUnit(1, 4, 0, false, 1, 0);

            AddEnemyUnit(1, 24, 4, true, 2, 2, true);
            AddEnemyUnit(1, 4, 0, false, 2, 0);
            AddEnemyUnit(2, 10, 2, false, 2, 1);
            AddEnemyUnit(2, 14, 1, false, 2, 1);
            AddEnemyUnit(2, 19, 0, false, 2, 1);
            AddEnemyUnit(1, 22, 0, false, 2, 0);
            AddEnemyUnit(1, 9, 0, false, 2, 0);
            AddEnemyUnit(1, 16, 0, false, 2, 0);
            AddEnemyUnit(1, 2, 0, false, 2, 0);

            AddEnemyUnit(1, 2, 10, true, 3, 2, false);
            AddEnemyUnit(1, 3, 0, false, 3, 0);
            AddEnemyUnit(1, 22, 0, false, 3, 0);
            AddEnemyUnit(2, 6, 0, false, 3, 1);
            AddEnemyUnit(2, 8, 1, false, 3, 1);
            AddEnemyUnit(2, 1, 2, false, 3, 1);
            AddEnemyUnit(1, 9, 0, false, 3, 0);
            AddEnemyUnit(1, 19, 0, false, 3, 0);
            AddEnemyUnit(1, 14, 0, false, 3, 0);

            AddEnemyUnit(1, 22, 5, true, 4, 5, false);
            AddEnemyUnit(1, 3, 3, false, 4, 3);
            AddEnemyUnit(2, 5, 5, false, 4, 3);
            AddEnemyUnit(2, 20, 6, false, 4, 3);
            AddEnemyUnit(1, 9, 0, false, 4, 1);
            AddEnemyUnit(1, 19, 0, false, 4, 1);
            AddEnemyUnit(1, 14, 0, false, 4, 1);
            AddEnemyUnit(1, 2, 0, false, 4, 1);
            AddEnemyUnit(1, 16, 0, false, 4, 1);
            AddEnemyUnit(2, 13, 0, false, 4, 2);
            AddEnemyUnit(2, 8, 1, false, 4, 2);
            AddEnemyUnit(2, 4, 2, false, 4, 2);
        }
        

        // 2 7  10 11 12 15 17 18 21 23 24

        //0 1 3 4 5 6 7 8 9 10 11 12 13 14 15 16 17 18 19 20 21 22 23 24

        for (int i = 0; i < 4; i++)
        {
            GameController.Instance.SetPlayerPlatoon(i);
        }
        maxDeployedPlatoon = 3;
    }

    private void Level3Setup()
    {
        Debug.Log("Level 3 Setup");

        Platoon p0 = new Platoon(0);
        enemyPlatoons[0] = p0;

        Platoon p1 = new Platoon(1);
        enemyPlatoons[1] = p1;

        Platoon p2 = new Platoon(2);
        enemyPlatoons[2] = p2;

        Platoon p3 = new Platoon(3);
        enemyPlatoons[3] = p3;

        Platoon p4 = new Platoon(4);
        enemyPlatoons[4] = p4;

        Platoon p5 = new Platoon(5);
        enemyPlatoons[5] = p5;

        Platoon p6 = new Platoon(6);
        enemyPlatoons[6] = p6;

        Platoon p7 = new Platoon(7);
        enemyPlatoons[7] = p7;

        Platoon p8 = new Platoon(8);
        enemyPlatoons[8] = p8;


        enemyPlatoonsRemaining = 9;

        //AddEnemyUnit(int charType, int position, int charClass, bool leader, int platoon, int level)

        //23
        AddEnemyUnit(1, 17, 4, true, 0, 5, true);
        AddEnemyUnit(2, 9, 1, false, 0, 3);
        AddEnemyUnit(2, 2, 2, false, 0, 3);
        AddEnemyUnit(2, 10, 3, false, 0, 3);
        AddEnemyUnit(1, 22, 0, false, 0, 1);
        AddEnemyUnit(1, 7, 0, false, 0, 1);
        AddEnemyUnit(1, 8, 0, false, 0, 1);
        AddEnemyUnit(1, 6, 0, false, 0, 1);
        AddEnemyUnit(1, 5, 0, false, 0, 1);
        AddEnemyUnit(1, 0, 0, false, 0, 1);
        AddEnemyUnit(1, 4, 0, false, 0, 1);
        AddEnemyUnit(1, 1, 0, false, 0, 1);
        AddEnemyUnit(1, 3, 0, false, 0, 1);

        //23
        AddEnemyUnit(1, 19, 5, true, 1, 5, false);
        AddEnemyUnit(2, 18, 0, false, 1, 3);
        AddEnemyUnit(2, 13, 5, false, 1, 3);
        AddEnemyUnit(1, 0, 0, false, 1, 1);
        AddEnemyUnit(1, 1, 0, false, 1, 1);
        AddEnemyUnit(1, 7, 0, false, 1, 1);
        AddEnemyUnit(1, 5, 0, false, 1, 1);
        AddEnemyUnit(1, 6, 0, false, 1, 1);
        AddEnemyUnit(1, 15, 0, false, 1, 1);
        AddEnemyUnit(1, 16, 0, false, 1, 1);
        AddEnemyUnit(1, 20, 0, false, 1, 1);
        AddEnemyUnit(1, 21, 0, false, 1, 1);

        //23
        AddEnemyUnit(1, 17, 6, true, 2, 5, true);
        AddEnemyUnit(2, 4, 2, false, 2, 3);
        AddEnemyUnit(2, 5, 4, false, 2, 3);
        AddEnemyUnit(1, 21, 0, false, 2, 1);
        AddEnemyUnit(1, 23, 0, false, 2, 1);
        AddEnemyUnit(1, 2, 0, false, 2, 1);
        AddEnemyUnit(1, 3, 0, false, 2, 1);
        AddEnemyUnit(1, 22, 0, false, 2, 1);
        AddEnemyUnit(1, 10, 0, false, 2, 1);
        AddEnemyUnit(1, 11, 0, false, 2, 1);
        AddEnemyUnit(1, 20, 0, false, 2, 1);
        AddEnemyUnit(1, 24, 0, false, 2, 1);

        //22
        AddEnemyUnit(1, 22, 7, true, 3, 5, false);
        AddEnemyUnit(1, 3, 0, false, 3, 1);
        AddEnemyUnit(1, 1, 0, false, 3, 1);
        AddEnemyUnit(2, 13, 3, false, 3, 3);
        AddEnemyUnit(2, 11, 6, false, 3, 3);
        AddEnemyUnit(1, 23, 0, false, 3, 1);
        AddEnemyUnit(1, 21, 0, false, 3, 1);
        AddEnemyUnit(1, 2, 0, false, 3, 1);
        AddEnemyUnit(1, 8, 0, false, 3, 1);
        AddEnemyUnit(1, 6, 0, false, 3, 1);
        AddEnemyUnit(1, 18, 0, false, 3, 1);
        AddEnemyUnit(1, 16, 0, false, 3, 1);
        AddEnemyUnit(1, 4, 0, false, 3, 1);

        //51, high star units
        AddEnemyUnit(1, 22, 15, true, 4, 10, false);
        AddEnemyUnit(1, 16, 1, false, 4, 6);
        AddEnemyUnit(1, 18, 2, false, 4, 6);
        AddEnemyUnit(1, 0, 3, false, 4, 6);
        AddEnemyUnit(2, 7, 7, false, 4, 4);
        AddEnemyUnit(1, 1, 0, false, 4, 1);
        AddEnemyUnit(1, 4, 0, false, 4, 1);
        AddEnemyUnit(1, 6, 0, false, 4, 1);
        AddEnemyUnit(1, 9, 0, false, 4, 1);
        AddEnemyUnit(1, 5, 0, false, 4, 1);
        AddEnemyUnit(1, 12, 0, false, 4, 1);
        AddEnemyUnit(1, 13, 0, false, 4, 1);

        //35
        AddEnemyUnit(1, 0, 8, true, 5, 5, false);
        AddEnemyUnit(1, 2, 0, false, 5, 2);
        AddEnemyUnit(1, 5, 0, false, 5, 2);
        AddEnemyUnit(2, 9, 1, false, 5, 3);
        AddEnemyUnit(2, 19, 1, false, 5, 3);
        AddEnemyUnit(2, 24, 2, false, 5, 3);
        AddEnemyUnit(2, 4, 0, false, 5, 3);
        AddEnemyUnit(1, 10, 0, false, 5, 2);
        AddEnemyUnit(1, 15, 0, false, 5, 2);
        AddEnemyUnit(1, 20, 0, false, 5, 2);
        AddEnemyUnit(1, 1, 0, false, 5, 2);
        AddEnemyUnit(1, 3, 0, false, 5, 2);
        AddEnemyUnit(1, 7, 0, false, 5, 2);
        AddEnemyUnit(1, 22, 0, false, 5, 2);

        //33
        AddEnemyUnit(1, 3, 9, true, 6, 6, false);
        AddEnemyUnit(1, 1, 0, false, 6, 2);
        AddEnemyUnit(1, 0, 0, false, 6, 2);
        AddEnemyUnit(2, 23, 6, false, 6, 3);
        AddEnemyUnit(2, 2, 2, false, 6, 3);
        AddEnemyUnit(2, 11, 1, false, 6, 3);
        AddEnemyUnit(1, 24, 0, false, 6, 2);
        AddEnemyUnit(1, 22, 0, false, 6, 2);
        AddEnemyUnit(1, 15, 0, false, 6, 2);
        AddEnemyUnit(1, 19, 0, false, 6, 2);
        AddEnemyUnit(1, 17, 0, false, 6, 2);
        AddEnemyUnit(1, 18, 0, false, 6, 2);
        AddEnemyUnit(1, 4, 0, false, 6, 2);

        //33
        AddEnemyUnit(1, 9, 10, true, 7, 6, true);
        AddEnemyUnit(1, 4, 0, false, 7, 2);
        AddEnemyUnit(1, 0, 0, false, 7, 2);
        AddEnemyUnit(2, 20, 0, false, 7, 3);
        AddEnemyUnit(2, 5, 2, false, 7, 3);
        AddEnemyUnit(2, 15, 6, false, 7, 3);
        AddEnemyUnit(1, 13, 0, false, 7, 2);
        AddEnemyUnit(1, 12, 0, false, 7, 2);
        AddEnemyUnit(1, 11, 0, false, 7, 2);
        AddEnemyUnit(1, 2, 0, false, 7, 2);
        AddEnemyUnit(1, 7, 0, false, 7, 2);
        AddEnemyUnit(1, 3, 0, false, 7, 2);
        AddEnemyUnit(1, 1, 0, false, 7, 2);

        //33
        AddEnemyUnit(1, 3, 8, true, 8, 3, true);
        AddEnemyUnit(1, 1, 0, false, 8, 2);
        AddEnemyUnit(1, 2, 0, false, 8, 2);
        AddEnemyUnit(2, 14, 0, false, 8, 3);
        AddEnemyUnit(2, 10, 1, false, 8, 3);
        AddEnemyUnit(2, 12, 3, false, 8, 3);
        AddEnemyUnit(1, 23, 0, false, 8, 2);
        AddEnemyUnit(1, 22, 0, false, 8, 2);
        AddEnemyUnit(1, 21, 0, false, 8, 2);
        AddEnemyUnit(1, 5, 0, false, 8, 2);
        AddEnemyUnit(1, 16, 0, false, 8, 2);
        AddEnemyUnit(1, 24, 0, false, 8, 2);
        AddEnemyUnit(1, 0, 0, false, 8, 2);


        for (int i = 0; i < 7; i++)
        {
            GameController.Instance.SetPlayerPlatoon(i);
        }
        maxDeployedPlatoon = 6;
    }

    private void Level4Setup()
    {
        Debug.Log("Level 4 Setup");

        Platoon p0 = new Platoon(0);
        enemyPlatoons[0] = p0;

        Platoon p1 = new Platoon(1);
        enemyPlatoons[1] = p1;

        Platoon p2 = new Platoon(2);
        enemyPlatoons[2] = p2;

        Platoon p3 = new Platoon(3);
        enemyPlatoons[3] = p3;

        Platoon p4 = new Platoon(4);
        enemyPlatoons[4] = p4;

        Platoon p5 = new Platoon(5);
        enemyPlatoons[5] = p5;

        Platoon p6 = new Platoon(6);
        enemyPlatoons[6] = p6;

        Platoon p7 = new Platoon(7);
        enemyPlatoons[7] = p7;

        Platoon p8 = new Platoon(8);
        enemyPlatoons[8] = p8;

        Platoon p9 = new Platoon(9);
        enemyPlatoons[9] = p9;

        Platoon p10 = new Platoon(10);
        enemyPlatoons[10] = p10;

        //Platoon p11 = new Platoon(11);
        //enemyPlatoons[11] = p11;

        enemyPlatoonsRemaining = 11;

        //AddEnemyUnit(int charType, int position, int charClass, bool leader, int platoon, int level)

        //40
        AddEnemyUnit(1, 1, 9, true, 0, 7, true);
        AddEnemyUnit(2, 9, 1, false, 0, 4);
        AddEnemyUnit(2, 2, 2, false, 0, 4);
        AddEnemyUnit(2, 10, 3, false, 0, 4);
        AddEnemyUnit(1, 22, 2, false, 0, 3);
        AddEnemyUnit(1, 7, 1, false, 0, 3);
        AddEnemyUnit(1, 8, 3, false, 0, 3);
        AddEnemyUnit(1, 6, 0, false, 0, 2);
        AddEnemyUnit(1, 5, 0, false, 0, 2);
        AddEnemyUnit(1, 0, 0, false, 0, 2);
        AddEnemyUnit(1, 4, 0, false, 0, 2);
        AddEnemyUnit(1, 17, 0, false, 0, 2);
        AddEnemyUnit(1, 3, 0, false, 0, 2);

        //40
        AddEnemyUnit(1, 19, 4, true, 1, 7, false);
        AddEnemyUnit(2, 18, 0, false, 1, 4);
        AddEnemyUnit(2, 13, 5, false, 1, 4);
        AddEnemyUnit(1, 0, 3, false, 1, 3);
        AddEnemyUnit(1, 21, 2, false, 1, 3);
        AddEnemyUnit(1, 16, 1, false, 1, 3);
        AddEnemyUnit(1, 5, 0, false, 1, 2);
        AddEnemyUnit(1, 6, 0, false, 1, 2);
        AddEnemyUnit(1, 15, 0, false, 1, 2);
        AddEnemyUnit(1, 7, 0, false, 1, 2);
        AddEnemyUnit(1, 20, 0, false, 1, 2);
        AddEnemyUnit(1, 1, 0, false, 1, 2);

        //40
        AddEnemyUnit(1, 17, 6, true, 2, 7, true);
        AddEnemyUnit(2, 4, 2, false, 2, 4);
        AddEnemyUnit(2, 5, 4, false, 2, 4);
        AddEnemyUnit(1, 21, 1, false, 2, 3);
        AddEnemyUnit(1, 23, 2, false, 2, 3);
        AddEnemyUnit(1, 2, 3, false, 2, 3);
        AddEnemyUnit(1, 3, 0, false, 2, 2);
        AddEnemyUnit(1, 22, 0, false, 2, 2);
        AddEnemyUnit(1, 10, 0, false, 2, 2);
        AddEnemyUnit(1, 11, 0, false, 2, 2);
        AddEnemyUnit(1, 20, 0, false, 2, 2);
        AddEnemyUnit(1, 24, 0, false, 2, 2);

        //40
        AddEnemyUnit(1, 2, 10, true, 3, 7, false);
        AddEnemyUnit(2, 13, 3, false, 3, 4);
        AddEnemyUnit(2, 11, 6, false, 3, 4);
        AddEnemyUnit(1, 3, 3, false, 3, 3);
        AddEnemyUnit(1, 16, 1, false, 3, 3);
        AddEnemyUnit(1, 23, 2, false, 3, 3);
        AddEnemyUnit(1, 21, 0, false, 3, 2);
        AddEnemyUnit(1, 12, 0, false, 3, 2);
        AddEnemyUnit(1, 8, 0, false, 3, 2);
        AddEnemyUnit(1, 6, 0, false, 3, 2);
        AddEnemyUnit(1, 18, 0, false, 3, 2);
        AddEnemyUnit(1, 1, 0, false, 3, 2);
        AddEnemyUnit(1, 22, 0, false, 3, 2);

        //80
        AddEnemyUnit(1, 22, 16, true, 4, 15, false);
        AddEnemyUnit(2, 23, 9, false, 4, 7);
        AddEnemyUnit(2, 15, 0, false, 4, 5);
        AddEnemyUnit(2, 12, 1, false, 4, 5);
        AddEnemyUnit(2, 2, 2, false, 4, 5);
        AddEnemyUnit(2, 8, 4, false, 4, 5);
        AddEnemyUnit(2, 5, 5, false, 4, 5);
        AddEnemyUnit(2, 16, 6, false, 4, 5);
        AddEnemyUnit(2, 14, 3, false, 4, 5);
        AddEnemyUnit(1, 21, 2, false, 4, 6);
        

        //50
        AddEnemyUnit(1, 0, 10, true, 5, 7, false);
        AddEnemyUnit(2, 9, 1, false, 5, 3);
        AddEnemyUnit(2, 19, 1, false, 5, 4);
        AddEnemyUnit(2, 24, 2, false, 5, 5);
        AddEnemyUnit(2, 4, 0, false, 5, 4);
        AddEnemyUnit(1, 2, 0, false, 5, 3);
        AddEnemyUnit(1, 5, 0, false, 5, 3);
        AddEnemyUnit(1, 10, 0, false, 5, 3);
        AddEnemyUnit(1, 15, 0, false, 5, 3);
        AddEnemyUnit(1, 20, 0, false, 5, 3);
        AddEnemyUnit(1, 1, 0, false, 5, 3);
        AddEnemyUnit(1, 3, 0, false, 5, 3);
        AddEnemyUnit(1, 7, 0, false, 5, 3);
        AddEnemyUnit(1, 22, 0, false, 5, 3);

        //50
        AddEnemyUnit(1, 3, 9, true, 6, 7, false);
        AddEnemyUnit(2, 23, 6, false, 6, 5);
        AddEnemyUnit(2, 2, 2, false, 6, 5);
        AddEnemyUnit(2, 11, 1, false, 6, 5);
        AddEnemyUnit(1, 1, 3, false, 6, 4);
        AddEnemyUnit(1, 17, 2, false, 6, 4);
        AddEnemyUnit(1, 24, 1, false, 6, 4);
        AddEnemyUnit(1, 22, 0, false, 6, 3);
        AddEnemyUnit(1, 15, 0, false, 6, 3);
        AddEnemyUnit(1, 19, 0, false, 6, 2);
        AddEnemyUnit(1, 0, 0, false, 6, 2);
        AddEnemyUnit(1, 18, 0, false, 6, 3);
        AddEnemyUnit(1, 4, 0, false, 6, 3);

        //50
        AddEnemyUnit(1, 9, 8, true, 7, 7, true);
        AddEnemyUnit(2, 20, 0, false, 7, 5);
        AddEnemyUnit(2, 5, 2, false, 7, 5);
        AddEnemyUnit(2, 15, 6, false, 7, 5);
        AddEnemyUnit(1, 4, 3, false, 7, 4);
        AddEnemyUnit(1, 11, 2, false, 7, 4);
        AddEnemyUnit(1, 13, 1, false, 7, 4);
        AddEnemyUnit(1, 12, 0, false, 7, 3);
        AddEnemyUnit(1, 0, 0, false, 7, 3);
        AddEnemyUnit(1, 2, 0, false, 7, 2);
        AddEnemyUnit(1, 7, 0, false, 7, 2);
        AddEnemyUnit(1, 3, 0, false, 7, 3);
        AddEnemyUnit(1, 1, 0, false, 7, 3);

        //60
        AddEnemyUnit(1, 21, 5, true, 8, 10, true);
        AddEnemyUnit(2, 14, 0, false, 8, 7);
        AddEnemyUnit(2, 10, 1, false, 8, 7);
        AddEnemyUnit(2, 12, 3, false, 8, 6);
        AddEnemyUnit(1, 1, 3, false, 8, 4);
        AddEnemyUnit(1, 24, 1, false, 8, 4);
        AddEnemyUnit(1, 23, 2, false, 8, 4);
        AddEnemyUnit(1, 22, 0, false, 8, 3);
        AddEnemyUnit(1, 3, 0, false, 8, 3);
        AddEnemyUnit(1, 5, 0, false, 8, 3);
        AddEnemyUnit(1, 16, 0, false, 8, 3);
        AddEnemyUnit(1, 2, 0, false, 8, 3);
        AddEnemyUnit(1, 0, 0, false, 8, 3);

        //60
        AddEnemyUnit(1, 22, 7, true, 9, 10, false);
        AddEnemyUnit(2, 14, 2, false, 9, 7);
        AddEnemyUnit(2, 10, 1, false, 9, 7);
        AddEnemyUnit(2, 12, 6, false, 9, 6);
        AddEnemyUnit(1, 0, 3, false, 9, 4);
        AddEnemyUnit(1, 18, 1, false, 9, 4);
        AddEnemyUnit(1, 23, 2, false, 9, 4);
        AddEnemyUnit(1, 3, 0, false, 9, 3);
        AddEnemyUnit(1, 21, 0, false, 9, 3);
        AddEnemyUnit(1, 15, 0, false, 9, 3);
        AddEnemyUnit(1, 16, 0, false, 9, 3);
        AddEnemyUnit(1, 24, 0, false, 9, 3);
        AddEnemyUnit(1, 1, 0, false, 9, 3);

        //60
        /*AddEnemyUnit(1, 14, 4, true, 10, 10, false);
        AddEnemyUnit(2, 3, 2, false, 10, 8);
        AddEnemyUnit(2, 12, 5, false, 10, 6);
        AddEnemyUnit(1, 16, 2, false, 10, 4);
        AddEnemyUnit(1, 2, 3, false, 10, 4);
        AddEnemyUnit(1, 23, 1, false, 10, 4);
        AddEnemyUnit(1, 22, 0, false, 10, 3);
        AddEnemyUnit(1, 21, 0, false, 10, 3);
        AddEnemyUnit(1, 5, 0, false, 10, 3);
        AddEnemyUnit(1, 19, 0, false, 10, 3);
        AddEnemyUnit(1, 10, 0, false, 10, 3);
        AddEnemyUnit(1, 0, 0, false, 10, 3);*/

        //60
        AddEnemyUnit(1, 16, 6, true, 10, 10, true);
        AddEnemyUnit(2, 14, 0, false, 10, 8);
        AddEnemyUnit(2, 8, 4, false, 10, 6);
        AddEnemyUnit(1, 21, 1, false, 10, 4);
        AddEnemyUnit(1, 2, 3, false, 10, 4);
        AddEnemyUnit(1, 23, 2, false, 10, 4);
        AddEnemyUnit(1, 22, 0, false, 10, 3);
        AddEnemyUnit(1, 1, 0, false, 10, 3);
        AddEnemyUnit(1, 15, 0, false, 10, 3);
        AddEnemyUnit(1, 10, 0, false, 10, 3);
        AddEnemyUnit(1, 24, 0, false, 10, 3);
        AddEnemyUnit(1, 20, 0, false, 10, 3);

        for (int i = 0; i < 8; i++)
        {
            GameController.Instance.SetPlayerPlatoon(i);
        }
        maxDeployedPlatoon = 7;
    }

    private void Level5Setup()
    {
        Debug.Log("Level 5 Setup");

        Platoon p0 = new Platoon(0);
        enemyPlatoons[0] = p0;

        Platoon p1 = new Platoon(1);
        enemyPlatoons[1] = p1;

        Platoon p2 = new Platoon(2);
        enemyPlatoons[2] = p2;

        Platoon p3 = new Platoon(3);
        enemyPlatoons[3] = p3;

        Platoon p4 = new Platoon(4);
        enemyPlatoons[4] = p4;

        Platoon p5 = new Platoon(5);
        enemyPlatoons[5] = p5;

        Platoon p6 = new Platoon(6);
        enemyPlatoons[6] = p6;

        Platoon p7 = new Platoon(7);
        enemyPlatoons[7] = p7;

        Platoon p8 = new Platoon(8);
        enemyPlatoons[8] = p8;

        Platoon p9 = new Platoon(9);
        enemyPlatoons[9] = p9;

        Platoon p10 = new Platoon(10);
        enemyPlatoons[10] = p10;

        Platoon p11 = new Platoon(11);
        enemyPlatoons[11] = p11;

        Platoon p12 = new Platoon(12);
        enemyPlatoons[12] = p12;

        enemyPlatoonsRemaining = 13;

        //AddEnemyUnit(int charType, int position, int charClass, bool leader, int platoon, int level)

        //65
        AddEnemyUnit(1, 20, 6, true, 0, 12, false);
        AddEnemyUnit(2, 8, 1, false, 0, 5);
        AddEnemyUnit(2, 2, 2, false, 0, 5);
        AddEnemyUnit(2, 12, 0, false, 0, 5);
        AddEnemyUnit(2, 0, 2, false, 0, 5);
        AddEnemyUnit(2, 4, 1, false, 0, 5);
        AddEnemyUnit(2, 14, 0, false, 0, 5);
        AddEnemyUnit(1, 17, 1, false, 0, 4);
        AddEnemyUnit(1, 23, 2, false, 0, 4);
        AddEnemyUnit(1, 3, 3, false, 0, 4);
        AddEnemyUnit(1, 1, 0, false, 0, 4);
        AddEnemyUnit(1, 5, 0, false, 0, 4);
        AddEnemyUnit(1, 10, 0, false, 0, 4);

        //65
        AddEnemyUnit(1, 18, 7, true, 1, 12, false);
        AddEnemyUnit(2, 21, 0, false, 1, 5);
        AddEnemyUnit(2, 13, 1, false, 1, 5);
        AddEnemyUnit(2, 19, 0, false, 1, 5);
        AddEnemyUnit(2, 11, 1, false, 1, 5);
        AddEnemyUnit(2, 4, 2, false, 1, 5);
        AddEnemyUnit(2, 6, 2, false, 1, 5);
        AddEnemyUnit(1, 0, 3, false, 1, 4);
        AddEnemyUnit(1, 22, 2, false, 1, 4);
        AddEnemyUnit(1, 15, 1, false, 1, 4);
        AddEnemyUnit(1, 23, 0, false, 1, 4);
        AddEnemyUnit(1, 2, 0, false, 1, 4);
        AddEnemyUnit(1, 14, 0, false, 1, 4);

        //65
        AddEnemyUnit(1, 22, 4, true, 2, 12, true);
        AddEnemyUnit(2, 24, 0, false, 2, 5);
        AddEnemyUnit(2, 14, 1, false, 2, 5);
        AddEnemyUnit(2, 23, 0, false, 2, 5);
        AddEnemyUnit(2, 10, 1, false, 2, 5);
        AddEnemyUnit(2, 2, 2, false, 2, 5);
        AddEnemyUnit(2, 4, 2, false, 2, 5);
        AddEnemyUnit(1, 19, 1, false, 2, 4);
        AddEnemyUnit(1, 20, 2, false, 2, 4);
        AddEnemyUnit(1, 1, 3, false, 2, 4);
        AddEnemyUnit(1, 0, 0, false, 2, 4);
        AddEnemyUnit(1, 3, 0, false, 2, 4);
        AddEnemyUnit(1, 7, 0, false, 2, 4);

        //65
        AddEnemyUnit(1, 1, 10, true, 3, 12, true);
        AddEnemyUnit(2, 21, 0, false, 3, 5);
        AddEnemyUnit(2, 14, 1, false, 3, 5);
        AddEnemyUnit(2, 23, 0, false, 3, 5);
        AddEnemyUnit(2, 10, 1, false, 3, 5);
        AddEnemyUnit(2, 7, 2, false, 3, 5);
        AddEnemyUnit(2, 13, 2, false, 3, 5);
        AddEnemyUnit(1, 4, 3, false, 3, 4);
        AddEnemyUnit(1, 18, 1, false, 3, 4);
        AddEnemyUnit(1, 16, 2, false, 3, 4);
        AddEnemyUnit(1, 0, 0, false, 3, 4);
        AddEnemyUnit(1, 2, 0, false, 3, 4);
        AddEnemyUnit(1, 3, 0, false, 3, 4);

        //128
        AddEnemyUnit(1, 18, 25, true, 4, 20, true);
        AddEnemyUnit(1, 0, 9, false, 4, 9);
        AddEnemyUnit(1, 2, 9, false, 4, 9);
        AddEnemyUnit(1, 4, 9, false, 4, 9);
        AddEnemyUnit(1, 6, 10, false, 4, 9);
        AddEnemyUnit(1, 8, 10, false, 4, 9);
        AddEnemyUnit(1, 10, 3, false, 4, 9);
        AddEnemyUnit(1, 12, 3, false, 4, 9);
        AddEnemyUnit(1, 14, 3, false, 4, 9);
        AddEnemyUnit(1, 16, 3, false, 4, 9);
        AddEnemyUnit(1, 20, 1, false, 4, 9);
        AddEnemyUnit(1, 22, 6, false, 4, 9);
        AddEnemyUnit(1, 24, 5, false, 4, 9);


        //75
        AddEnemyUnit(1, 4, 8, true, 5, 10, false);
        AddEnemyUnit(2, 0, 2, false, 5, 6);
        AddEnemyUnit(2, 14, 1, false, 5, 6);
        AddEnemyUnit(2, 10, 0, false, 5, 6);
        AddEnemyUnit(2, 11, 5, false, 5, 6);
        AddEnemyUnit(2, 13, 3, false, 5, 6);
        AddEnemyUnit(1, 21, 1, false, 5, 4);
        AddEnemyUnit(1, 23, 1, false, 5, 4);
        AddEnemyUnit(1, 20, 2, false, 5, 4);
        AddEnemyUnit(1, 24, 2, false, 5, 4);
        AddEnemyUnit(1, 1, 3, false, 5, 4);
        AddEnemyUnit(1, 3, 3, false, 5, 4);

        //75
        AddEnemyUnit(1, 23, 5, true, 6, 10, false);
        AddEnemyUnit(2, 16, 0, false, 6, 6);
        AddEnemyUnit(2, 12, 1, false, 6, 6);
        AddEnemyUnit(2, 1, 2, false, 6, 6);
        AddEnemyUnit(2, 7, 4, false, 6, 6);
        AddEnemyUnit(2, 21, 6, false, 6, 6);
        AddEnemyUnit(1, 0, 3, false, 6, 4);
        AddEnemyUnit(1, 17, 2, false, 6, 4);
        AddEnemyUnit(1, 15, 1, false, 6, 4);
        AddEnemyUnit(1, 19, 1, false, 6, 4);
        AddEnemyUnit(1, 22, 2, false, 6, 4);
        AddEnemyUnit(1, 4, 3, false, 6, 4);

        //75
        AddEnemyUnit(1, 2, 9, true, 7, 10, false);
        AddEnemyUnit(2, 22, 0, false, 7, 6);
        AddEnemyUnit(2, 4, 1, false, 7, 6);
        AddEnemyUnit(2, 0, 2, false, 7, 6);
        AddEnemyUnit(2, 9, 3, false, 7, 6);
        AddEnemyUnit(2, 10, 4, false, 7, 6);
        AddEnemyUnit(1, 23, 1, false, 7, 4);
        AddEnemyUnit(1, 21, 2, false, 7, 4);
        AddEnemyUnit(1, 1, 3, false, 7, 4);
        AddEnemyUnit(1, 24, 1, false, 7, 4);
        AddEnemyUnit(1, 20, 2, false, 7, 4);
        AddEnemyUnit(1, 3, 3, false, 7, 4);

        //75
        AddEnemyUnit(1, 7, 18, true, 8, 10, true);
        AddEnemyUnit(2, 23, 0, false, 8, 6);
        AddEnemyUnit(2, 0, 1, false, 8, 6);
        AddEnemyUnit(2, 1, 2, false, 8, 6);
        AddEnemyUnit(2, 21, 6, false, 8, 6);
        AddEnemyUnit(2, 13, 5, false, 8, 6);
        AddEnemyUnit(1, 23, 2, false, 8, 4);
        AddEnemyUnit(1, 16, 1, false, 8, 4);
        AddEnemyUnit(1, 19, 1, false, 8, 4);
        AddEnemyUnit(1, 17, 2, false, 8, 4);
        AddEnemyUnit(1, 18, 2, false, 8, 4);
        AddEnemyUnit(1, 3, 3, false, 8, 4);
        AddEnemyUnit(1, 4, 3, false, 8, 4);

        //81
        AddEnemyUnit(1, 15, 16, true, 9, 10, true);
        AddEnemyUnit(2, 3, 3, false, 9, 7);
        AddEnemyUnit(2, 5, 4, false, 9, 7);
        AddEnemyUnit(2, 21, 5, false, 9, 7);
        AddEnemyUnit(2, 24, 6, false, 9, 7);
        AddEnemyUnit(1, 23, 0, false, 9, 4);
        AddEnemyUnit(1, 11, 0, false, 9, 4);
        AddEnemyUnit(1, 12, 0, false, 9, 4);
        AddEnemyUnit(1, 13, 0, false, 9, 4);
        AddEnemyUnit(1, 9, 0, false, 9, 4);
        AddEnemyUnit(1, 7, 0, false, 9, 4);

        //85
        AddEnemyUnit(1, 12, 17, true, 10, 12, true);
        AddEnemyUnit(2, 19, 3, false, 10, 8);
        AddEnemyUnit(2, 5, 4, false, 10, 7);
        AddEnemyUnit(2, 8, 5, false, 10, 7);
        AddEnemyUnit(2, 20, 6, false, 10, 8);
        AddEnemyUnit(1, 16, 0, false, 10, 4);
        AddEnemyUnit(1, 17, 0, false, 10, 4);
        AddEnemyUnit(1, 18, 0, false, 10, 4);
        AddEnemyUnit(1, 21, 0, false, 10, 4);
        AddEnemyUnit(1, 22, 0, false, 10, 4);
        AddEnemyUnit(1, 23, 0, false, 10, 4);

        //90
        AddEnemyUnit(1, 19, 15, true, 11, 12, true);
        AddEnemyUnit(2, 23, 0, false, 11, 7);
        AddEnemyUnit(2, 17, 1, false, 11, 7);
        AddEnemyUnit(2, 1, 2, false, 11, 7);
        AddEnemyUnit(2, 22, 3, false, 11, 7);
        AddEnemyUnit(2, 8, 4, false, 11, 7);
        AddEnemyUnit(2, 10, 5, false, 11, 7);
        AddEnemyUnit(2, 24, 6, false, 11, 7);
        AddEnemyUnit(1, 18, 0, false, 11, 5);
        AddEnemyUnit(1, 20, 0, false, 11, 5);
        AddEnemyUnit(1, 21, 0, false, 11, 5);

        //110
        AddEnemyUnit(1, 0, 11, true, 12, 14, false);
        AddEnemyUnit(1, 2, 0, false, 12, 8);
        AddEnemyUnit(1, 4, 0, false, 12, 8);
        AddEnemyUnit(1, 6, 0, false, 12, 8);
        AddEnemyUnit(1, 8, 0, false, 12, 8);
        AddEnemyUnit(1, 10, 0, false, 12, 8);
        AddEnemyUnit(1, 12, 0, false, 12, 8);
        AddEnemyUnit(1, 14, 0, false, 12, 8);
        AddEnemyUnit(1, 16, 0, false, 12, 8);
        AddEnemyUnit(1, 18, 0, false, 12, 8);
        AddEnemyUnit(1, 20, 0, false, 12, 8);
        AddEnemyUnit(1, 22, 0, false, 12, 8);
        AddEnemyUnit(1, 24, 0, false, 12, 8);

        for (int i = 0; i < 9; i++)
        {
            GameController.Instance.SetPlayerPlatoon(i);
        }
        maxDeployedPlatoon = 8;
    }

    private void Level6Setup()
    {
        Debug.Log("Level 6 Setup");

        Platoon p0 = new Platoon(0);
        enemyPlatoons[0] = p0;

        Platoon p1 = new Platoon(1);
        enemyPlatoons[1] = p1;

        Platoon p2 = new Platoon(2);
        enemyPlatoons[2] = p2;

        Platoon p3 = new Platoon(3);
        enemyPlatoons[3] = p3;

        Platoon p4 = new Platoon(4);
        enemyPlatoons[4] = p4;

        Platoon p5 = new Platoon(5);
        enemyPlatoons[5] = p5;

        Platoon p6 = new Platoon(6);
        enemyPlatoons[6] = p6;

        Platoon p7 = new Platoon(7);
        enemyPlatoons[7] = p7;

        Platoon p8 = new Platoon(8);
        enemyPlatoons[8] = p8;

        Platoon p9 = new Platoon(9);
        enemyPlatoons[9] = p9;

        Platoon p10 = new Platoon(10);
        enemyPlatoons[10] = p10;

        Platoon p11 = new Platoon(11);
        enemyPlatoons[11] = p11;

        Platoon p12 = new Platoon(12);
        enemyPlatoons[12] = p12;

        enemyPlatoonsRemaining = 13;

        //AddEnemyUnit(int charType, int position, int charClass, bool leader, int platoon, int level)

        //108
        AddEnemyUnit(1, 20, 6, true, 0, 10, false);
        AddEnemyUnit(2, 8, 1, false, 0, 8);
        AddEnemyUnit(2, 2, 2, false, 0, 8);
        AddEnemyUnit(2, 12, 0, false, 0, 8);
        AddEnemyUnit(2, 0, 2, false, 0, 8);
        AddEnemyUnit(2, 4, 1, false, 0, 8);
        AddEnemyUnit(2, 14, 0, false, 0, 8);
        AddEnemyUnit(1, 17, 1, false, 0, 8);
        AddEnemyUnit(1, 23, 2, false, 0, 8);
        AddEnemyUnit(1, 3, 3, false, 0, 8);
        AddEnemyUnit(1, 1, 0, false, 0, 8);
        AddEnemyUnit(1, 5, 0, false, 0, 8);
        AddEnemyUnit(1, 10, 0, false, 0, 8);

        //71
        AddEnemyUnit(1, 18, 7, true, 1, 10, false);
        AddEnemyUnit(2, 21, 0, false, 1, 8);
        AddEnemyUnit(2, 13, 1, false, 1, 8);
        AddEnemyUnit(2, 19, 0, false, 1, 8);
        AddEnemyUnit(2, 11, 1, false, 1, 8);
        AddEnemyUnit(2, 4, 2, false, 1, 8);
        AddEnemyUnit(2, 6, 2, false, 1, 8);
        AddEnemyUnit(1, 0, 3, false, 1, 8);
        AddEnemyUnit(1, 22, 2, false, 1, 8);
        AddEnemyUnit(1, 15, 1, false, 1, 8);
        AddEnemyUnit(1, 23, 0, false, 1, 8);
        AddEnemyUnit(1, 2, 0, false, 1, 8);
        AddEnemyUnit(1, 14, 0, false, 1, 8);

        //71
        AddEnemyUnit(1, 22, 4, true, 2, 10, true);
        AddEnemyUnit(2, 24, 0, false, 2, 8);
        AddEnemyUnit(2, 14, 1, false, 2, 8);
        AddEnemyUnit(2, 23, 0, false, 2, 8);
        AddEnemyUnit(2, 10, 1, false, 2, 8);
        AddEnemyUnit(2, 2, 2, false, 2, 8);
        AddEnemyUnit(2, 4, 2, false, 2, 8);
        AddEnemyUnit(1, 19, 1, false, 2, 8);
        AddEnemyUnit(1, 20, 2, false, 2, 8);
        AddEnemyUnit(1, 1, 3, false, 2, 8);
        AddEnemyUnit(1, 0, 0, false, 2, 8);
        AddEnemyUnit(1, 3, 0, false, 2, 8);
        AddEnemyUnit(1, 7, 0, false, 2, 8);

        //71
        AddEnemyUnit(1, 1, 10, true, 3, 10, true);
        AddEnemyUnit(2, 21, 0, false, 3, 8);
        AddEnemyUnit(2, 14, 1, false, 3, 8);
        AddEnemyUnit(2, 23, 0, false, 3, 8);
        AddEnemyUnit(2, 10, 1, false, 3, 8);
        AddEnemyUnit(2, 7, 2, false, 3, 8);
        AddEnemyUnit(2, 13, 2, false, 3, 8);
        AddEnemyUnit(1, 4, 3, false, 3, 8);
        AddEnemyUnit(1, 18, 1, false, 3, 8);
        AddEnemyUnit(1, 16, 2, false, 3, 8);
        AddEnemyUnit(1, 0, 0, false, 3, 8);
        AddEnemyUnit(1, 2, 0, false, 3, 8);
        AddEnemyUnit(1, 3, 0, false, 3, 8);

        //165
        AddEnemyUnit(1, 18, 25, true, 4, 30, true);
        AddEnemyUnit(1, 0, 9, false, 4, 20);
        AddEnemyUnit(1, 2, 9, false, 4, 20);
        AddEnemyUnit(1, 4, 9, false, 4, 20);
        AddEnemyUnit(1, 6, 10, false, 4, 20);
        AddEnemyUnit(1, 8, 10, false, 4, 20);
        AddEnemyUnit(1, 10, 3, false, 4, 20);
        AddEnemyUnit(1, 12, 3, false, 4, 20);
        AddEnemyUnit(1, 14, 3, false, 4, 20);
        AddEnemyUnit(1, 16, 3, false, 4, 20);
        AddEnemyUnit(1, 20, 1, false, 4, 20);
        AddEnemyUnit(1, 22, 6, false, 4, 20);
        AddEnemyUnit(1, 24, 5, false, 4, 20);

        //87
        AddEnemyUnit(1, 4, 8, true, 5, 15, false);
        AddEnemyUnit(2, 0, 2, false, 5, 10);
        AddEnemyUnit(2, 14, 1, false, 5, 10);
        AddEnemyUnit(2, 10, 0, false, 5, 10);
        AddEnemyUnit(2, 11, 5, false, 5, 10);
        AddEnemyUnit(2, 13, 3, false, 5, 10);
        AddEnemyUnit(1, 21, 1, false, 5, 10);
        AddEnemyUnit(1, 23, 1, false, 5, 10);
        AddEnemyUnit(1, 20, 2, false, 5, 10);
        AddEnemyUnit(1, 24, 2, false, 5, 10);
        AddEnemyUnit(1, 1, 3, false, 5, 10);
        AddEnemyUnit(1, 3, 3, false, 5, 10);

        //87
        AddEnemyUnit(1, 23, 5, true, 6, 15, false);
        AddEnemyUnit(2, 16, 0, false, 6, 10);
        AddEnemyUnit(2, 12, 1, false, 6, 10);
        AddEnemyUnit(2, 1, 2, false, 6, 10);
        AddEnemyUnit(2, 7, 4, false, 6, 10);
        AddEnemyUnit(2, 21, 6, false, 6, 10);
        AddEnemyUnit(1, 0, 3, false, 6, 10);
        AddEnemyUnit(1, 17, 2, false, 6, 10);
        AddEnemyUnit(1, 15, 1, false, 6, 10);
        AddEnemyUnit(1, 19, 1, false, 6, 10);
        AddEnemyUnit(1, 22, 2, false, 6, 10);
        AddEnemyUnit(1, 4, 3, false, 6, 10);

        //87
        AddEnemyUnit(1, 2, 9, true, 7, 15, false);
        AddEnemyUnit(2, 22, 0, false, 7, 10);
        AddEnemyUnit(2, 4, 1, false, 7, 10);
        AddEnemyUnit(2, 0, 2, false, 7, 10);
        AddEnemyUnit(2, 9, 3, false, 7, 10);
        AddEnemyUnit(2, 10, 4, false, 7, 10);
        AddEnemyUnit(1, 23, 1, false, 7, 10);
        AddEnemyUnit(1, 21, 2, false, 7, 10);
        AddEnemyUnit(1, 1, 3, false, 7, 10);
        AddEnemyUnit(1, 24, 1, false, 7, 10);
        AddEnemyUnit(1, 20, 2, false, 7, 10);
        AddEnemyUnit(1, 3, 3, false, 7, 10);

        //87
        AddEnemyUnit(1, 7, 18, true, 8, 15, true);
        AddEnemyUnit(2, 23, 0, false, 8, 10);
        AddEnemyUnit(2, 0, 1, false, 8, 10);
        AddEnemyUnit(2, 1, 2, false, 8, 10);
        AddEnemyUnit(2, 21, 6, false, 8, 10);
        AddEnemyUnit(2, 13, 5, false, 8, 10);
        AddEnemyUnit(1, 23, 2, false, 8, 10);
        AddEnemyUnit(1, 16, 1, false, 8, 10);
        AddEnemyUnit(1, 19, 1, false, 8, 10);
        AddEnemyUnit(1, 17, 2, false, 8, 10);
        AddEnemyUnit(1, 18, 2, false, 8, 10);
        AddEnemyUnit(1, 3, 3, false, 8, 10);
        AddEnemyUnit(1, 4, 3, false, 8, 10);

        //100
        AddEnemyUnit(1, 15, 16, true, 9, 20, true);
        AddEnemyUnit(2, 3, 3, false, 9, 10);
        AddEnemyUnit(2, 5, 4, false, 9, 10);
        AddEnemyUnit(2, 21, 5, false, 9, 10);
        AddEnemyUnit(2, 24, 6, false, 9, 10);
        AddEnemyUnit(1, 23, 0, false, 9, 10);
        AddEnemyUnit(1, 11, 0, false, 9, 10);
        AddEnemyUnit(1, 12, 0, false, 9, 10);
        AddEnemyUnit(1, 13, 0, false, 9, 10);
        AddEnemyUnit(1, 9, 0, false, 9, 10);
        AddEnemyUnit(1, 7, 0, false, 9, 10);

        //111
        AddEnemyUnit(1, 12, 17, true, 10, 20, true);
        AddEnemyUnit(2, 19, 3, false, 10, 11);
        AddEnemyUnit(2, 5, 4, false, 10, 11);
        AddEnemyUnit(2, 8, 5, false, 10, 11);
        AddEnemyUnit(2, 20, 6, false, 10, 11);
        AddEnemyUnit(1, 16, 0, false, 10, 10);
        AddEnemyUnit(1, 17, 0, false, 10, 10);
        AddEnemyUnit(1, 18, 0, false, 10, 10);
        AddEnemyUnit(1, 21, 0, false, 10, 10);
        AddEnemyUnit(1, 22, 0, false, 10, 10);
        AddEnemyUnit(1, 23, 0, false, 10, 10);

        //120
        AddEnemyUnit(1, 19, 15, true, 11, 22, true);
        AddEnemyUnit(2, 23, 0, false, 11, 11);
        AddEnemyUnit(2, 17, 1, false, 11, 11);
        AddEnemyUnit(2, 1, 2, false, 11, 11);
        AddEnemyUnit(2, 22, 3, false, 11, 11);
        AddEnemyUnit(2, 8, 4, false, 11, 11);
        AddEnemyUnit(2, 10, 5, false, 11, 11);
        AddEnemyUnit(2, 24, 6, false, 11, 11);
        AddEnemyUnit(1, 18, 0, false, 11, 10);
        AddEnemyUnit(1, 20, 0, false, 11, 10);
        AddEnemyUnit(1, 21, 0, false, 11, 10);

        //135
        AddEnemyUnit(1, 0, 11, true, 12, 30, false);
        AddEnemyUnit(1, 2, 0, false, 12, 10);
        AddEnemyUnit(1, 4, 0, false, 12, 10);
        AddEnemyUnit(1, 6, 0, false, 12, 10);
        AddEnemyUnit(1, 8, 0, false, 12, 10);
        AddEnemyUnit(1, 10, 0, false, 12, 10);
        AddEnemyUnit(1, 12, 0, false, 12, 10);
        AddEnemyUnit(1, 14, 0, false, 12, 10);
        AddEnemyUnit(1, 16, 0, false, 12, 10);
        AddEnemyUnit(1, 18, 0, false, 12, 10);
        AddEnemyUnit(1, 20, 0, false, 12, 10);
        AddEnemyUnit(1, 22, 0, false, 12, 10);
        AddEnemyUnit(1, 24, 0, false, 12, 10);

        for (int i = 0; i < 9; i++)
        {
            GameController.Instance.SetPlayerPlatoon(i);
        }
        maxDeployedPlatoon = 8;
    }

    private void Level7Setup()
    {
        Debug.Log("Level 7 Setup");

        Platoon p0 = new Platoon(0);
        enemyPlatoons[0] = p0;

        Platoon p1 = new Platoon(1);
        enemyPlatoons[1] = p1;

        Platoon p2 = new Platoon(2);
        enemyPlatoons[2] = p2;

        Platoon p3 = new Platoon(3);
        enemyPlatoons[3] = p3;

        Platoon p4 = new Platoon(4);
        enemyPlatoons[4] = p4;

        Platoon p5 = new Platoon(5);
        enemyPlatoons[5] = p5;

        Platoon p6 = new Platoon(6);
        enemyPlatoons[6] = p6;

        Platoon p7 = new Platoon(7);
        enemyPlatoons[7] = p7;

        Platoon p8 = new Platoon(8);
        enemyPlatoons[8] = p8;

        Platoon p9 = new Platoon(9);
        enemyPlatoons[9] = p9;

        Platoon p10 = new Platoon(10);
        enemyPlatoons[10] = p10;

        Platoon p11 = new Platoon(11);
        enemyPlatoons[11] = p11;

        Platoon p12 = new Platoon(12);
        enemyPlatoons[12] = p12;

        enemyPlatoonsRemaining = 13;

        //AddEnemyUnit(int charType, int position, int charClass, bool leader, int platoon, int level)

        //160
        AddEnemyUnit(1, 20, 23, true, 0, 20, false);
        AddEnemyUnit(2, 8, 0, false, 0, 10);
        AddEnemyUnit(1, 2, 3, false, 0, 10);
        AddEnemyUnit(1, 12, 2, false, 0, 10);
        AddEnemyUnit(1, 10, 2, false, 0, 10);
        AddEnemyUnit(1, 15, 1, false, 0, 10);
        AddEnemyUnit(1, 4, 3, false, 0, 10);
        AddEnemyUnit(1, 17, 1, false, 0, 10);
        AddEnemyUnit(1, 23, 2, false, 0, 10);
        AddEnemyUnit(1, 19, 4, false, 0, 15);
        AddEnemyUnit(1, 5, 6, false, 0, 15);
        AddEnemyUnit(1, 1, 8, false, 0, 15);
        AddEnemyUnit(1, 3, 10, false, 0, 15);

        //160
        AddEnemyUnit(1, 18, 19, true, 1, 20, false);
        AddEnemyUnit(2, 1, 2, false, 1, 10);
        AddEnemyUnit(1, 13, 1, false, 1, 10);
        AddEnemyUnit(1, 19, 2, false, 1, 10);
        AddEnemyUnit(1, 11, 1, false, 1, 10);
        AddEnemyUnit(1, 9, 2, false, 1, 10);
        AddEnemyUnit(1, 3, 3, false, 1, 10);
        AddEnemyUnit(1, 0, 3, false, 1, 10);
        AddEnemyUnit(1, 22, 2, false, 1, 10);
        AddEnemyUnit(1, 15, 10, false, 1, 15);
        AddEnemyUnit(1, 2, 9, false, 1, 15);
        AddEnemyUnit(1, 7, 7, false, 1, 15);
        AddEnemyUnit(1, 14, 5, false, 1, 15);

        //160
        AddEnemyUnit(1, 2, 12, true, 2, 20, true);
        AddEnemyUnit(2, 24, 1, false, 2, 10);
        AddEnemyUnit(1, 14, 2, false, 2, 10);
        AddEnemyUnit(1, 23, 2, false, 2, 10);
        AddEnemyUnit(1, 10, 1, false, 2, 10);
        AddEnemyUnit(1, 16, 3, false, 2, 10);
        AddEnemyUnit(1, 4, 3, false, 2, 10);
        AddEnemyUnit(1, 19, 1, false, 2, 10);
        AddEnemyUnit(1, 20, 1, false, 2, 10);
        AddEnemyUnit(1, 1, 8, false, 2, 15);
        AddEnemyUnit(1, 0, 9, false, 2, 15);
        AddEnemyUnit(1, 3, 7, false, 2, 15);
        AddEnemyUnit(1, 17, 4, false, 2, 15);

        //180
        AddEnemyUnit(1, 9, 16, true, 3, 25, true);
        AddEnemyUnit(2, 12, 3, false, 3, 15);
        AddEnemyUnit(1, 14, 1, false, 3, 14);
        AddEnemyUnit(1, 23, 2, false, 3, 14);
        AddEnemyUnit(1, 10, 1, false, 3, 14);
        AddEnemyUnit(1, 7, 2, false, 3, 14);
        AddEnemyUnit(1, 2, 3, false, 3, 14);
        AddEnemyUnit(1, 4, 3, false, 3, 14);
        AddEnemyUnit(1, 18, 1, false, 3, 10);
        AddEnemyUnit(1, 16, 5, false, 3, 14);
        AddEnemyUnit(1, 24, 6, false, 3, 14);
        AddEnemyUnit(1, 20, 7, false, 3, 14);
        AddEnemyUnit(1, 3, 8, false, 3, 14);

        //330
        AddEnemyUnit(1, 22, 31, true, 4, 30, false);
        AddEnemyUnit(1, 21, 4, false, 4, 25);
        AddEnemyUnit(1, 20, 5, false, 4, 25);
        AddEnemyUnit(1, 23, 6, false, 4, 25);
        AddEnemyUnit(1, 24, 7, false, 4, 25);
        AddEnemyUnit(1, 2, 9, false, 4, 25);
        AddEnemyUnit(1, 10, 5, false, 4, 25);
        AddEnemyUnit(1, 6, 8, false, 4, 25);
        AddEnemyUnit(1, 8, 10, false, 4, 25);
        AddEnemyUnit(1, 0, 9, false, 4, 25);
        AddEnemyUnit(1, 4, 9, false, 4, 25);
        AddEnemyUnit(1, 18, 4, false, 4, 25);
        AddEnemyUnit(1, 15, 7, false, 4, 25);

        //180
        AddEnemyUnit(1, 17, 21, true, 5, 25, false);
        AddEnemyUnit(2, 15, 6, false, 5, 14);
        AddEnemyUnit(1, 14, 1, false, 5, 14);
        AddEnemyUnit(1, 10, 2, false, 5, 14);
        AddEnemyUnit(1, 11, 1, false, 5, 14);
        AddEnemyUnit(1, 13, 2, false, 5, 14);
        AddEnemyUnit(1, 0, 3, false, 5, 14);
        AddEnemyUnit(1, 4, 3, false, 5, 14);
        AddEnemyUnit(1, 20, 2, false, 5, 10);
        AddEnemyUnit(1, 24, 5, false, 5, 14);
        AddEnemyUnit(1, 22, 6, false, 5, 14);
        AddEnemyUnit(1, 3, 10, false, 5, 14);
        AddEnemyUnit(1, 7, 4, false, 5, 14);

        //199
        AddEnemyUnit(1, 23, 22, true, 6, 25, false);
        AddEnemyUnit(2, 8, 4, false, 6, 14);
        AddEnemyUnit(1, 12, 1, false, 6, 15);
        AddEnemyUnit(1, 14, 2, false, 6, 15);
        AddEnemyUnit(1, 1, 3, false, 6, 15);
        AddEnemyUnit(1, 21, 1, false, 6, 15);
        AddEnemyUnit(1, 0, 3, false, 6, 15);
        AddEnemyUnit(1, 17, 2, false, 6, 15);
        AddEnemyUnit(1, 15, 8, false, 6, 14);
        AddEnemyUnit(1, 2, 9, false, 6, 14);
        AddEnemyUnit(1, 22, 7, false, 6, 14);
        AddEnemyUnit(1, 19, 4, false, 6, 14);

        //199
        AddEnemyUnit(1, 22, 20, true, 7, 25, false);
        AddEnemyUnit(2, 5, 5, false, 7, 14);
        AddEnemyUnit(1, 17, 1, false, 7, 15);
        AddEnemyUnit(1, 15, 2, false, 7, 15);
        AddEnemyUnit(1, 3, 3, false, 7, 15);
        AddEnemyUnit(1, 4, 3, false, 7, 15);
        AddEnemyUnit(1, 23, 1, false, 7, 15);
        AddEnemyUnit(1, 21, 2, false, 7, 15);
        AddEnemyUnit(1, 12, 4, false, 7, 14);
        AddEnemyUnit(1, 24, 5, false, 7, 14);
        AddEnemyUnit(1, 20, 6, false, 7, 14);
        AddEnemyUnit(1, 10, 7, false, 7, 14);

        //204
        AddEnemyUnit(1, 22, 15, true, 8, 25, true);
        AddEnemyUnit(2, 6, 7, false, 8, 10);
        AddEnemyUnit(1, 13, 1, false, 8, 16);
        AddEnemyUnit(1, 21, 2, false, 8, 16);
        AddEnemyUnit(1, 0, 3, false, 8, 16);
        AddEnemyUnit(1, 3, 3, false, 8, 16);
        AddEnemyUnit(1, 23, 2, false, 8, 16);
        AddEnemyUnit(1, 16, 1, false, 8, 16);
        AddEnemyUnit(1, 19, 4, false, 8, 16);
        AddEnemyUnit(1, 17, 6, false, 8, 16);
        AddEnemyUnit(1, 18, 5, false, 8, 16);
        AddEnemyUnit(1, 4, 10, false, 8, 16);

        //204
        AddEnemyUnit(1, 15, 18, true, 9, 25, true);
        AddEnemyUnit(2, 7, 8, false, 9, 10);
        AddEnemyUnit(1, 5, 1, false, 9, 16);
        AddEnemyUnit(1, 21, 2, false, 9, 16);
        AddEnemyUnit(1, 4, 3, false, 9, 16);
        AddEnemyUnit(1, 1, 3, false, 9, 16);
        AddEnemyUnit(1, 11, 2, false, 9, 16);
        AddEnemyUnit(1, 12, 1, false, 9, 16);
        AddEnemyUnit(1, 0, 9, false, 9, 16);
        AddEnemyUnit(1, 10, 8, false, 9, 16);
        AddEnemyUnit(1, 24, 7, false, 9, 16);
        AddEnemyUnit(1, 20, 5, false, 9, 16);

        //204
        AddEnemyUnit(1, 0, 11, true, 10, 25, true);
        AddEnemyUnit(2, 15, 9, false, 10, 10);
        AddEnemyUnit(1, 5, 1, false, 10, 16);
        AddEnemyUnit(1, 24, 2, false, 10, 16);
        AddEnemyUnit(1, 1, 3, false, 10, 16);
        AddEnemyUnit(1, 3, 3, false, 10, 16);
        AddEnemyUnit(1, 17, 2, false, 10, 16);
        AddEnemyUnit(1, 18, 1, false, 10, 16);
        AddEnemyUnit(1, 21, 6, false, 10, 16);
        AddEnemyUnit(1, 4, 8, false, 10, 16);
        AddEnemyUnit(1, 2, 9, false, 10, 16);
        AddEnemyUnit(1, 8, 10, false, 10, 16);

        //225
        AddEnemyUnit(1, 5, 13, true, 11, 25, true);
        AddEnemyUnit(1, 23, 2, false, 11, 16);
        AddEnemyUnit(1, 17, 1, false, 11, 16);
        AddEnemyUnit(1, 22, 2, false, 11, 16);
        AddEnemyUnit(1, 1, 3, false, 11, 16);
        AddEnemyUnit(1, 2, 3, false, 11, 16);
        AddEnemyUnit(1, 10, 1, false, 11, 16);
        AddEnemyUnit(1, 24, 4, false, 11, 16);
        AddEnemyUnit(1, 18, 5, false, 11, 16);
        AddEnemyUnit(1, 20, 6, false, 11, 16);
        AddEnemyUnit(1, 0, 8, false, 11, 16);
        AddEnemyUnit(1, 3, 9, false, 11, 20);
        AddEnemyUnit(1, 4, 10, false, 11, 20);

        //225
        AddEnemyUnit(1, 7, 14, true, 12, 25, false);
        AddEnemyUnit(1, 8, 1, false, 12, 16);
        AddEnemyUnit(1, 6, 2, false, 12, 16);
        AddEnemyUnit(1, 1, 3, false, 12, 16);
        AddEnemyUnit(1, 3, 3, false, 12, 16);
        AddEnemyUnit(1, 10, 2, false, 12, 16);
        AddEnemyUnit(1, 12, 1, false, 12, 16);
        AddEnemyUnit(1, 14, 4, false, 12, 16);
        AddEnemyUnit(1, 16, 5, false, 12, 16);
        AddEnemyUnit(1, 18, 7, false, 12, 16);
        AddEnemyUnit(1, 5, 8, false, 12, 16);
        AddEnemyUnit(1, 2, 9, false, 12, 20);
        AddEnemyUnit(1, 9, 10, false, 12, 20);

        for (int i = 0; i < 9; i++)
        {
            GameController.Instance.SetPlayerPlatoon(i);
        }
        maxDeployedPlatoon = 8;
    }

    private void Level8Setup()
    {
        Debug.Log("Level 8 Setup");

        Platoon p0 = new Platoon(0);
        enemyPlatoons[0] = p0;

        Platoon p1 = new Platoon(1);
        enemyPlatoons[1] = p1;

        Platoon p2 = new Platoon(2);
        enemyPlatoons[2] = p2;

        Platoon p3 = new Platoon(3);
        enemyPlatoons[3] = p3;

        Platoon p4 = new Platoon(4);
        enemyPlatoons[4] = p4;

        Platoon p5 = new Platoon(5);
        enemyPlatoons[5] = p5;

        Platoon p6 = new Platoon(6);
        enemyPlatoons[6] = p6;

        Platoon p7 = new Platoon(7);
        enemyPlatoons[7] = p7;

        Platoon p8 = new Platoon(8);
        enemyPlatoons[8] = p8;

        Platoon p9 = new Platoon(9);
        enemyPlatoons[9] = p9;

        Platoon p10 = new Platoon(10);
        enemyPlatoons[10] = p10;

        Platoon p11 = new Platoon(11);
        enemyPlatoons[11] = p11;

        enemyPlatoonsRemaining = 12;

        //AddEnemyUnit(int charType, int position, int charClass, bool leader, int platoon, int level)

        //209
        AddEnemyUnit(1, 20, 23, true, 0, 25, true);
        AddEnemyUnit(2, 8, 0, false, 0, 15);
        AddEnemyUnit(1, 2, 3, false, 0, 15);
        AddEnemyUnit(1, 12, 2, false, 0, 15);
        AddEnemyUnit(1, 10, 2, false, 0, 15);
        AddEnemyUnit(1, 15, 1, false, 0, 15);
        AddEnemyUnit(1, 4, 3, false, 0, 15);
        AddEnemyUnit(1, 17, 1, false, 0, 15);
        AddEnemyUnit(1, 23, 2, false, 0, 15);
        AddEnemyUnit(1, 19, 4, false, 0, 16);
        AddEnemyUnit(1, 5, 6, false, 0, 16);
        AddEnemyUnit(1, 1, 8, false, 0, 16);
        AddEnemyUnit(1, 3, 10, false, 0, 16);

        //209
        AddEnemyUnit(1, 18, 19, true, 1, 25, true);
        AddEnemyUnit(2, 1, 2, false, 1, 15);
        AddEnemyUnit(1, 13, 1, false, 1, 15);
        AddEnemyUnit(1, 19, 2, false, 1, 15);
        AddEnemyUnit(1, 11, 1, false, 1, 15);
        AddEnemyUnit(1, 9, 2, false, 1, 15);
        AddEnemyUnit(1, 3, 3, false, 1, 15);
        AddEnemyUnit(1, 0, 3, false, 1, 15);
        AddEnemyUnit(1, 22, 2, false, 1, 15);
        AddEnemyUnit(1, 15, 10, false, 1, 16);
        AddEnemyUnit(1, 2, 9, false, 1, 16);
        AddEnemyUnit(1, 7, 7, false, 1, 16);
        AddEnemyUnit(1, 14, 5, false, 1, 16);

        //209
        AddEnemyUnit(1, 2, 12, true, 2, 25, false);
        AddEnemyUnit(2, 24, 1, false, 2, 15);
        AddEnemyUnit(1, 14, 2, false, 2, 15);
        AddEnemyUnit(1, 23, 2, false, 2, 15);
        AddEnemyUnit(1, 10, 1, false, 2, 15);
        AddEnemyUnit(1, 16, 3, false, 2, 15);
        AddEnemyUnit(1, 4, 3, false, 2, 15);
        AddEnemyUnit(1, 19, 1, false, 2, 15);
        AddEnemyUnit(1, 20, 1, false, 2, 15);
        AddEnemyUnit(1, 1, 8, false, 2, 16);
        AddEnemyUnit(1, 0, 9, false, 2, 16);
        AddEnemyUnit(1, 3, 7, false, 2, 16);
        AddEnemyUnit(1, 17, 4, false, 2, 16);

        //225
        AddEnemyUnit(1, 9, 16, true, 3, 25, false);
        AddEnemyUnit(2, 12, 3, false, 3, 15);
        AddEnemyUnit(1, 14, 1, false, 3, 15);
        AddEnemyUnit(1, 23, 2, false, 3, 15);
        AddEnemyUnit(1, 10, 1, false, 3, 15);
        AddEnemyUnit(1, 7, 2, false, 3, 15);
        AddEnemyUnit(1, 2, 3, false, 3, 15);
        AddEnemyUnit(1, 4, 3, false, 3, 15);
        AddEnemyUnit(1, 18, 1, false, 3, 15);
        AddEnemyUnit(1, 16, 5, false, 3, 20);
        AddEnemyUnit(1, 24, 6, false, 3, 20);
        AddEnemyUnit(1, 20, 7, false, 3, 20);
        AddEnemyUnit(1, 3, 8, false, 3, 20);

        //375
        AddEnemyUnit(1, 22, 28, true, 4, 35, false);
        AddEnemyUnit(1, 21, 4, false, 4, 30);
        AddEnemyUnit(1, 20, 5, false, 4, 30);
        AddEnemyUnit(1, 23, 6, false, 4, 30);
        AddEnemyUnit(1, 24, 7, false, 4, 30);
        AddEnemyUnit(1, 2, 9, false, 4, 30);
        AddEnemyUnit(1, 10, 5, false, 4, 30);
        AddEnemyUnit(1, 6, 8, false, 4, 30);
        AddEnemyUnit(1, 8, 10, false, 4, 30);
        AddEnemyUnit(1, 0, 9, false, 4, 25);
        AddEnemyUnit(1, 4, 9, false, 4, 25);
        AddEnemyUnit(1, 18, 4, false, 4, 25);
        AddEnemyUnit(1, 15, 7, false, 4, 25);

        //225
        AddEnemyUnit(1, 17, 21, true, 5, 25, true);
        AddEnemyUnit(2, 15, 6, false, 5, 15);
        AddEnemyUnit(1, 14, 1, false, 5, 15);
        AddEnemyUnit(1, 10, 2, false, 5, 15);
        AddEnemyUnit(1, 11, 1, false, 5, 15);
        AddEnemyUnit(1, 13, 2, false, 5, 15);
        AddEnemyUnit(1, 0, 3, false, 5, 15);
        AddEnemyUnit(1, 4, 3, false, 5, 15);
        AddEnemyUnit(1, 20, 2, false, 5, 15);
        AddEnemyUnit(1, 24, 5, false, 5, 20);
        AddEnemyUnit(1, 22, 6, false, 5, 20);
        AddEnemyUnit(1, 3, 10, false, 5, 20);
        AddEnemyUnit(1, 7, 4, false, 5, 20);

        //245
        AddEnemyUnit(1, 23, 22, true, 6, 25, true);
        AddEnemyUnit(2, 8, 4, false, 6, 16);
        AddEnemyUnit(1, 12, 1, false, 6, 18);
        AddEnemyUnit(1, 14, 2, false, 6, 18);
        AddEnemyUnit(1, 1, 3, false, 6, 18);
        AddEnemyUnit(1, 21, 1, false, 6, 18);
        AddEnemyUnit(1, 0, 3, false, 6, 18);
        AddEnemyUnit(1, 17, 2, false, 6, 18);
        AddEnemyUnit(1, 15, 8, false, 6, 20);
        AddEnemyUnit(1, 2, 9, false, 6, 20);
        AddEnemyUnit(1, 22, 7, false, 6, 20);
        AddEnemyUnit(1, 19, 4, false, 6, 20);

        //245
        AddEnemyUnit(1, 22, 20, true, 7, 25, true);
        AddEnemyUnit(2, 5, 5, false, 7, 16);
        AddEnemyUnit(1, 17, 1, false, 7, 18);
        AddEnemyUnit(1, 15, 2, false, 7, 18);
        AddEnemyUnit(1, 3, 3, false, 7, 18);
        AddEnemyUnit(1, 4, 3, false, 7, 18);
        AddEnemyUnit(1, 23, 1, false, 7, 18);
        AddEnemyUnit(1, 21, 2, false, 7, 18);
        AddEnemyUnit(1, 12, 4, false, 7, 20);
        AddEnemyUnit(1, 24, 5, false, 7, 20);
        AddEnemyUnit(1, 20, 6, false, 7, 20);
        AddEnemyUnit(1, 10, 7, false, 7, 20);

        //264
        AddEnemyUnit(1, 22, 15, true, 8, 30, false);
        AddEnemyUnit(2, 6, 7, false, 8, 17);
        AddEnemyUnit(1, 13, 1, false, 8, 20);
        AddEnemyUnit(1, 21, 2, false, 8, 20);
        AddEnemyUnit(1, 0, 3, false, 8, 20);
        AddEnemyUnit(1, 3, 3, false, 8, 20);
        AddEnemyUnit(1, 23, 2, false, 8, 20);
        AddEnemyUnit(1, 16, 1, false, 8, 20);
        AddEnemyUnit(1, 19, 4, false, 8, 20);
        AddEnemyUnit(1, 17, 6, false, 8, 20);
        AddEnemyUnit(1, 18, 5, false, 8, 20);
        AddEnemyUnit(1, 4, 10, false, 8, 20);

        //264
        AddEnemyUnit(1, 15, 18, true, 9, 30, false);
        AddEnemyUnit(2, 7, 8, false, 9, 17);
        AddEnemyUnit(1, 5, 1, false, 9, 20);
        AddEnemyUnit(1, 21, 2, false, 9, 20);
        AddEnemyUnit(1, 4, 3, false, 9, 20);
        AddEnemyUnit(1, 1, 3, false, 9, 20);
        AddEnemyUnit(1, 11, 2, false, 9, 20);
        AddEnemyUnit(1, 12, 1, false, 9, 20);
        AddEnemyUnit(1, 0, 9, false, 9, 20);
        AddEnemyUnit(1, 10, 8, false, 9, 20);
        AddEnemyUnit(1, 24, 7, false, 9, 20);
        AddEnemyUnit(1, 20, 5, false, 9, 20);

        //264
        AddEnemyUnit(1, 0, 11, true, 10, 30, false);
        AddEnemyUnit(2, 15, 9, false, 10, 17);
        AddEnemyUnit(1, 5, 1, false, 10, 20);
        AddEnemyUnit(1, 24, 2, false, 10, 20);
        AddEnemyUnit(1, 1, 3, false, 10, 20);
        AddEnemyUnit(1, 3, 3, false, 10, 20);
        AddEnemyUnit(1, 17, 2, false, 10, 20);
        AddEnemyUnit(1, 18, 1, false, 10, 20);
        AddEnemyUnit(1, 21, 6, false, 10, 20);
        AddEnemyUnit(1, 4, 8, false, 10, 20);
        AddEnemyUnit(1, 2, 9, false, 10, 20);
        AddEnemyUnit(1, 8, 10, false, 10, 20);

        //260
        AddEnemyUnit(1, 15, 13, true, 11, 30, true);
        AddEnemyUnit(2, 18, 9, false, 11, 17);
        AddEnemyUnit(2, 8, 7, false, 11, 16);
        AddEnemyUnit(1, 23, 2, false, 11, 20);
        AddEnemyUnit(1, 17, 1, false, 11, 20);
        AddEnemyUnit(1, 22, 2, false, 11, 20);
        AddEnemyUnit(1, 1, 3, false, 11, 20);
        AddEnemyUnit(1, 2, 3, false, 11, 20);
        AddEnemyUnit(1, 10, 1, false, 11, 20);
        AddEnemyUnit(1, 24, 4, false, 11, 20);
        AddEnemyUnit(1, 20, 5, false, 11, 20);

        for (int i = 0; i < 9; i++)
        {
            GameController.Instance.SetPlayerPlatoon(i);
        }
        maxDeployedPlatoon = 8;
    }

    private void Level9Setup()
    {
        Debug.Log("Level 9 Setup");

        Platoon p0 = new Platoon(0);
        enemyPlatoons[0] = p0;

        Platoon p1 = new Platoon(1);
        enemyPlatoons[1] = p1;

        Platoon p2 = new Platoon(2);
        enemyPlatoons[2] = p2;

        Platoon p3 = new Platoon(3);
        enemyPlatoons[3] = p3;

        Platoon p4 = new Platoon(4);
        enemyPlatoons[4] = p4;

        Platoon p5 = new Platoon(5);
        enemyPlatoons[5] = p5;

        Platoon p6 = new Platoon(6);
        enemyPlatoons[6] = p6;

        Platoon p7 = new Platoon(7);
        enemyPlatoons[7] = p7;

        Platoon p8 = new Platoon(8);
        enemyPlatoons[8] = p8;

        Platoon p9 = new Platoon(9);
        enemyPlatoons[9] = p9;

        Platoon p10 = new Platoon(10);
        enemyPlatoons[10] = p10;

        Platoon p11 = new Platoon(11);
        enemyPlatoons[11] = p11;

        Platoon p12 = new Platoon(12);
        enemyPlatoons[12] = p12;

        enemyPlatoonsRemaining = 13;

        //AddEnemyUnit(int charType, int position, int charClass, bool leader, int platoon, int level)

        //255
        AddEnemyUnit(1, 20, 23, true, 0, 25, true);
        AddEnemyUnit(2, 8, 1, false, 0, 20);
        AddEnemyUnit(1, 2, 3, false, 0, 19);
        AddEnemyUnit(1, 12, 2, false, 0, 19);
        AddEnemyUnit(1, 10, 2, false, 0, 19);
        AddEnemyUnit(1, 15, 1, false, 0, 19);
        AddEnemyUnit(1, 4, 3, false, 0, 19);
        AddEnemyUnit(1, 17, 1, false, 0, 19);
        AddEnemyUnit(2, 23, 2, false, 0, 20);
        AddEnemyUnit(1, 19, 4, false, 0, 19);
        AddEnemyUnit(1, 5, 6, false, 0, 19);
        AddEnemyUnit(1, 1, 8, false, 0, 19);
        AddEnemyUnit(1, 3, 10, false, 0, 19);

        //255
        AddEnemyUnit(1, 18, 19, true, 1, 25, true);
        AddEnemyUnit(2, 1, 2, false, 1, 20);
        AddEnemyUnit(1, 13, 1, false, 1, 19);
        AddEnemyUnit(1, 19, 2, false, 1, 19);
        AddEnemyUnit(1, 11, 1, false, 1, 19);
        AddEnemyUnit(1, 9, 2, false, 1, 19);
        AddEnemyUnit(1, 3, 3, false, 1, 19);
        AddEnemyUnit(1, 0, 3, false, 1, 19);
        AddEnemyUnit(2, 22, 0, false, 1, 20);
        AddEnemyUnit(1, 15, 10, false, 1, 19);
        AddEnemyUnit(1, 2, 9, false, 1, 19);
        AddEnemyUnit(1, 7, 7, false, 1, 19);
        AddEnemyUnit(1, 14, 5, false, 1, 19);

        //255
        AddEnemyUnit(1, 2, 14, true, 2, 25, false);
        AddEnemyUnit(2, 24, 1, false, 2, 20);
        AddEnemyUnit(1, 14, 2, false, 2, 19);
        AddEnemyUnit(1, 23, 2, false, 2, 19);
        AddEnemyUnit(1, 10, 1, false, 2, 19);
        AddEnemyUnit(1, 16, 3, false, 2, 19);
        AddEnemyUnit(1, 4, 3, false, 2, 19);
        AddEnemyUnit(1, 19, 1, false, 2, 19);
        AddEnemyUnit(2, 20, 0, false, 2, 20);
        AddEnemyUnit(1, 1, 8, false, 2, 19);
        AddEnemyUnit(1, 0, 9, false, 2, 19);
        AddEnemyUnit(1, 3, 7, false, 2, 19);
        AddEnemyUnit(1, 17, 4, false, 2, 19);

        //255
        AddEnemyUnit(1, 9, 17, true, 3, 25, false);
        AddEnemyUnit(2, 0, 0, false, 3, 20);
        AddEnemyUnit(1, 14, 1, false, 3, 19);
        AddEnemyUnit(1, 23, 2, false, 3, 19);
        AddEnemyUnit(1, 10, 1, false, 3, 19);
        AddEnemyUnit(1, 7, 2, false, 3, 19);
        AddEnemyUnit(1, 2, 3, false, 3, 19);
        AddEnemyUnit(1, 4, 3, false, 3, 19);
        AddEnemyUnit(2, 18, 1, false, 3, 20);
        AddEnemyUnit(1, 16, 5, false, 3, 19);
        AddEnemyUnit(1, 24, 6, false, 3, 19);
        AddEnemyUnit(1, 20, 7, false, 3, 19);
        AddEnemyUnit(1, 3, 8, false, 3, 19);

        //430
        AddEnemyUnit(1, 22, 29, true, 4, 45, false);
        AddEnemyUnit(2, 5, 5, false, 4, 30);
        AddEnemyUnit(1, 23, 6, false, 4, 30);
        AddEnemyUnit(1, 24, 7, false, 4, 30);
        AddEnemyUnit(1, 2, 9, false, 4, 30);
        AddEnemyUnit(1, 10, 5, false, 4, 30);
        AddEnemyUnit(1, 11, 8, false, 4, 35);
        AddEnemyUnit(1, 8, 10, false, 4, 35);
        AddEnemyUnit(1, 3, 10, false, 4, 35);
        AddEnemyUnit(1, 4, 9, false, 4, 35);
        AddEnemyUnit(1, 18, 4, false, 4, 35);
        AddEnemyUnit(1, 15, 7, false, 4, 35);

        //275
        AddEnemyUnit(1, 17, 21, true, 5, 30, true);
        AddEnemyUnit(2, 15, 6, false, 5, 20);
        AddEnemyUnit(1, 14, 1, false, 5, 20);
        AddEnemyUnit(1, 10, 2, false, 5, 20);
        AddEnemyUnit(1, 11, 1, false, 5, 20);
        AddEnemyUnit(1, 13, 2, false, 5, 20);
        AddEnemyUnit(1, 0, 3, false, 5, 20);
        AddEnemyUnit(1, 4, 3, false, 5, 20);
        AddEnemyUnit(2, 20, 6, false, 5, 21);
        AddEnemyUnit(1, 24, 5, false, 5, 21);
        AddEnemyUnit(1, 22, 6, false, 5, 21);
        AddEnemyUnit(1, 3, 10, false, 5, 21);
        AddEnemyUnit(1, 7, 4, false, 5, 21);

        //275
        AddEnemyUnit(1, 23, 22, true, 6, 30, true);
        AddEnemyUnit(2, 8, 3, false, 6, 20);
        AddEnemyUnit(1, 12, 1, false, 6, 20);
        AddEnemyUnit(1, 14, 2, false, 6, 20);
        AddEnemyUnit(1, 13, 2, false, 6, 20);
        AddEnemyUnit(1, 1, 3, false, 6, 20);
        AddEnemyUnit(1, 21, 1, false, 6, 20);
        AddEnemyUnit(1, 0, 3, false, 6, 20);
        AddEnemyUnit(2, 17, 3, false, 6, 21);
        AddEnemyUnit(1, 15, 8, false, 6, 21);
        AddEnemyUnit(1, 2, 9, false, 6, 21);
        AddEnemyUnit(1, 22, 7, false, 6, 21);
        AddEnemyUnit(1, 19, 4, false, 6, 21);

        //300
        AddEnemyUnit(1, 22, 20, true, 7, 35, true);
        AddEnemyUnit(2, 5, 5, false, 7, 20);
        AddEnemyUnit(1, 17, 1, false, 7, 20);
        AddEnemyUnit(1, 15, 2, false, 7, 20);
        AddEnemyUnit(1, 3, 3, false, 7, 20);
        AddEnemyUnit(1, 4, 3, false, 7, 20);
        AddEnemyUnit(1, 23, 1, false, 7, 20);
        AddEnemyUnit(1, 21, 2, false, 7, 21);
        AddEnemyUnit(1, 12, 4, false, 7, 26);
        AddEnemyUnit(1, 24, 5, false, 7, 26);
        AddEnemyUnit(1, 20, 6, false, 7, 26);
        AddEnemyUnit(1, 10, 7, false, 7, 26);

        //300
        AddEnemyUnit(1, 22, 15, true, 8, 35, false);
        AddEnemyUnit(2, 6, 4, false, 8, 20);
        AddEnemyUnit(1, 13, 1, false, 8, 20);
        AddEnemyUnit(1, 21, 2, false, 8, 20);
        AddEnemyUnit(1, 0, 3, false, 8, 20);
        AddEnemyUnit(1, 3, 3, false, 8, 20);
        AddEnemyUnit(1, 23, 2, false, 8, 20);
        AddEnemyUnit(1, 16, 1, false, 8, 21);
        AddEnemyUnit(1, 19, 4, false, 8, 26);
        AddEnemyUnit(1, 17, 6, false, 8, 26);
        AddEnemyUnit(1, 18, 5, false, 8, 26);
        AddEnemyUnit(1, 4, 10, false, 8, 26);

        //309
        AddEnemyUnit(1, 15, 18, true, 9, 35, false);
        AddEnemyUnit(2, 7, 8, false, 9, 17);
        AddEnemyUnit(1, 5, 1, false, 9, 20);
        AddEnemyUnit(1, 21, 2, false, 9, 20);
        AddEnemyUnit(1, 4, 3, false, 9, 20);
        AddEnemyUnit(1, 1, 3, false, 9, 20);
        AddEnemyUnit(1, 11, 2, false, 9, 20);
        AddEnemyUnit(1, 12, 1, false, 9, 20);
        AddEnemyUnit(1, 0, 9, false, 9, 30);
        AddEnemyUnit(1, 10, 8, false, 9, 30);
        AddEnemyUnit(1, 24, 7, false, 9, 30);
        AddEnemyUnit(1, 20, 5, false, 9, 30);

        //309
        AddEnemyUnit(1, 0, 11, true, 10, 35, false);
        AddEnemyUnit(2, 15, 7, false, 10, 17);
        AddEnemyUnit(1, 5, 1, false, 10, 20);
        AddEnemyUnit(1, 24, 2, false, 10, 20);
        AddEnemyUnit(1, 1, 3, false, 10, 20);
        AddEnemyUnit(1, 3, 3, false, 10, 20);
        AddEnemyUnit(1, 17, 2, false, 10, 20);
        AddEnemyUnit(1, 18, 1, false, 10, 20);
        AddEnemyUnit(1, 21, 6, false, 10, 30);
        AddEnemyUnit(1, 4, 8, false, 10, 30);
        AddEnemyUnit(1, 2, 9, false, 10, 30);
        AddEnemyUnit(1, 8, 10, false, 10, 30);

        //309
        AddEnemyUnit(1, 15, 13, true, 11, 35, true);
        AddEnemyUnit(2, 18, 9, false, 11, 17);
        AddEnemyUnit(1, 8, 1, false, 11, 20);
        AddEnemyUnit(1, 23, 2, false, 11, 20);
        AddEnemyUnit(1, 17, 1, false, 11, 20);
        AddEnemyUnit(1, 22, 2, false, 11, 20);
        AddEnemyUnit(1, 1, 3, false, 11, 20);
        AddEnemyUnit(1, 3, 3, false, 11, 20);
        AddEnemyUnit(1, 2, 8, false, 11, 30);
        AddEnemyUnit(1, 10, 7, false, 11, 30);
        AddEnemyUnit(1, 24, 4, false, 11, 30);
        AddEnemyUnit(1, 20, 5, false, 11, 30);

        //308
        AddEnemyUnit(1, 7, 24, true, 12, 36, false);
        AddEnemyUnit(2, 8, 9, false, 12, 17);
        AddEnemyUnit(2, 18, 8, false, 12, 17);
        AddEnemyUnit(1, 1, 8, false, 12, 26);
        AddEnemyUnit(1, 2, 9, false, 12, 26);
        AddEnemyUnit(1, 10, 10, false, 12, 26);
        AddEnemyUnit(1, 12, 4, false, 12, 26);
        AddEnemyUnit(1, 23, 7, false, 12, 26);
        AddEnemyUnit(1, 16, 5, false, 12, 26);
        AddEnemyUnit(2, 15, 3, false, 12, 24);
        AddEnemyUnit(2, 5, 6, false, 12, 24);

        for (int i = 0; i < 9; i++)
        {
            GameController.Instance.SetPlayerPlatoon(i);
        }
        maxDeployedPlatoon = 8;
    }

    private void Level10Setup()
    {
        Debug.Log("Level 10 Setup");

        Platoon p0 = new Platoon(0);
        enemyPlatoons[0] = p0;

        Platoon p1 = new Platoon(1);
        enemyPlatoons[1] = p1;

        Platoon p2 = new Platoon(2);
        enemyPlatoons[2] = p2;

        Platoon p3 = new Platoon(3);
        enemyPlatoons[3] = p3;

        Platoon p4 = new Platoon(4);
        enemyPlatoons[4] = p4;

        Platoon p5 = new Platoon(5);
        enemyPlatoons[5] = p5;

        Platoon p6 = new Platoon(6);
        enemyPlatoons[6] = p6;

        Platoon p7 = new Platoon(7);
        enemyPlatoons[7] = p7;

        Platoon p8 = new Platoon(8);
        enemyPlatoons[8] = p8;

        Platoon p9 = new Platoon(9);
        enemyPlatoons[9] = p9;

        enemyPlatoonsRemaining = 10;

        //AddEnemyUnit(int charType, int position, int charClass, bool leader, int platoon, int level)

        //320
        AddEnemyUnit(1, 20, 23, true, 0, 36, false);
        AddEnemyUnit(2, 8, 1, false, 0, 30);
        AddEnemyUnit(1, 2, 3, false, 0, 20);
        AddEnemyUnit(1, 12, 2, false, 0, 20);
        AddEnemyUnit(1, 10, 2, false, 0, 20);
        AddEnemyUnit(1, 15, 1, false, 0, 20);
        AddEnemyUnit(1, 4, 3, false, 0, 20);
        AddEnemyUnit(1, 17, 1, false, 0, 20);
        AddEnemyUnit(2, 23, 2, false, 0, 30);
        AddEnemyUnit(1, 19, 4, false, 0, 19);
        AddEnemyUnit(1, 5, 6, false, 0, 19);
        AddEnemyUnit(1, 1, 8, false, 0, 19);
        AddEnemyUnit(1, 3, 10, false, 0, 19);

        //320
        AddEnemyUnit(1, 18, 19, true, 1, 36, false);
        AddEnemyUnit(2, 1, 2, false, 1, 30);
        AddEnemyUnit(1, 13, 1, false, 1, 20);
        AddEnemyUnit(1, 19, 2, false, 1, 20);
        AddEnemyUnit(1, 11, 1, false, 1, 20);
        AddEnemyUnit(1, 9, 2, false, 1, 20);
        AddEnemyUnit(1, 3, 3, false, 1, 20);
        AddEnemyUnit(1, 0, 3, false, 1, 20);
        AddEnemyUnit(2, 22, 0, false, 1, 30);
        AddEnemyUnit(1, 15, 10, false, 1, 30);
        AddEnemyUnit(1, 2, 9, false, 1, 30);
        AddEnemyUnit(1, 7, 7, false, 1, 30);
        AddEnemyUnit(1, 14, 5, false, 1, 30);

        //280
        AddEnemyUnit(1, 17, 21, true, 2, 40, false);
        AddEnemyUnit(2, 15, 6, false, 2, 30);
        AddEnemyUnit(1, 14, 1, false, 2, 20);
        AddEnemyUnit(1, 10, 2, false, 2, 20);
        AddEnemyUnit(1, 11, 1, false, 2, 20);
        AddEnemyUnit(1, 13, 2, false, 2, 20);
        AddEnemyUnit(1, 0, 3, false, 2, 20);
        AddEnemyUnit(1, 4, 3, false, 2, 20);
        AddEnemyUnit(2, 20, 6, false, 2, 30);
        AddEnemyUnit(1, 24, 5, false, 2, 30);
        AddEnemyUnit(1, 22, 6, false, 2, 30);
        AddEnemyUnit(1, 3, 10, false, 2, 30);
        AddEnemyUnit(1, 7, 4, false, 2, 30);

        //280
        AddEnemyUnit(1, 23, 22, true, 3, 40, false);
        AddEnemyUnit(2, 8, 3, false, 3, 30);
        AddEnemyUnit(1, 12, 1, false, 3, 20);
        AddEnemyUnit(1, 14, 2, false, 3, 20);
        AddEnemyUnit(1, 13, 2, false, 3, 20);
        AddEnemyUnit(1, 1, 3, false, 3, 20);
        AddEnemyUnit(1, 21, 1, false, 3, 20);
        AddEnemyUnit(1, 0, 3, false, 3, 20);
        AddEnemyUnit(2, 17, 3, false, 3, 30);
        AddEnemyUnit(1, 15, 8, false, 3, 30);
        AddEnemyUnit(1, 2, 9, false, 3, 30);
        AddEnemyUnit(1, 22, 7, false, 3, 30);
        AddEnemyUnit(1, 19, 4, false, 3, 30);


        //525
        AddEnemyUnit(1, 22, 30, true, 4, 45, false);
        AddEnemyUnit(2, 5, 5, false, 4, 40);
        AddEnemyUnit(1, 23, 6, false, 4, 40);
        AddEnemyUnit(1, 24, 7, false, 4, 40);
        AddEnemyUnit(1, 2, 9, false, 4, 40);
        AddEnemyUnit(1, 10, 5, false, 4, 40);
        AddEnemyUnit(1, 11, 16, false, 4, 40);
        AddEnemyUnit(1, 8, 19, false, 4, 40);
        AddEnemyUnit(1, 3, 13, false, 4, 40);
        AddEnemyUnit(1, 4, 14, false, 4, 40);
        AddEnemyUnit(1, 18, 21, false, 4, 40);
        AddEnemyUnit(1, 15, 15, false, 4, 40);

        

        //360
        AddEnemyUnit(1, 22, 20, true, 5, 40, false);
        AddEnemyUnit(2, 5, 5, false, 5, 30);
        AddEnemyUnit(1, 17, 5, false, 5, 24);
        AddEnemyUnit(1, 15, 4, false, 5, 24);
        AddEnemyUnit(1, 3, 9, false, 5, 24);
        AddEnemyUnit(1, 4, 8, false, 5, 24);
        AddEnemyUnit(1, 23, 10, false, 5, 24);
        AddEnemyUnit(1, 21, 8, false, 5, 24);
        AddEnemyUnit(1, 12, 4, false, 5, 29);
        AddEnemyUnit(1, 24, 5, false, 5, 29);
        AddEnemyUnit(1, 20, 6, false, 5, 29);
        AddEnemyUnit(1, 10, 7, false, 5, 29);

        //360
        AddEnemyUnit(1, 22, 15, true, 6, 40, true);
        AddEnemyUnit(2, 6, 4, false, 6, 30);
        AddEnemyUnit(1, 13, 7, false, 6, 24);
        AddEnemyUnit(1, 21, 6, false, 6, 24);
        AddEnemyUnit(1, 0, 8, false, 6, 24);
        AddEnemyUnit(1, 3, 9, false, 6, 24);
        AddEnemyUnit(1, 23, 7, false, 6, 24);
        AddEnemyUnit(1, 16, 8, false, 6, 24);
        AddEnemyUnit(1, 19, 4, false, 6, 29);
        AddEnemyUnit(1, 17, 6, false, 6, 29);
        AddEnemyUnit(1, 18, 5, false, 6, 29);
        AddEnemyUnit(1, 4, 10, false, 6, 29);

        //370
        AddEnemyUnit(1, 15, 18, true, 7, 40, true);
        AddEnemyUnit(2, 7, 8, false, 7, 23);
        AddEnemyUnit(1, 5, 6, false, 7, 30);
        AddEnemyUnit(1, 21, 4, false, 7, 30);
        AddEnemyUnit(1, 4, 9, false, 7, 30);
        AddEnemyUnit(1, 1, 10, false, 7, 30);
        AddEnemyUnit(1, 11, 6, false, 7, 30);
        AddEnemyUnit(1, 12, 7, false, 7, 30);
        AddEnemyUnit(1, 0, 9, false, 7, 26);
        AddEnemyUnit(1, 10, 8, false, 7, 26);
        AddEnemyUnit(1, 24, 7, false, 7, 26);
        AddEnemyUnit(1, 20, 5, false, 7, 26);

        //370
        AddEnemyUnit(1, 0, 11, true, 8, 40, true);
        AddEnemyUnit(2, 15, 7, false, 8, 23);
        AddEnemyUnit(1, 5, 4, false, 8, 30);
        AddEnemyUnit(1, 24, 5, false, 8, 30);
        AddEnemyUnit(1, 1, 10, false, 8, 30);
        AddEnemyUnit(1, 3, 10, false, 8, 30);
        AddEnemyUnit(1, 17, 4, false, 8, 30);
        AddEnemyUnit(1, 18, 5, false, 8, 30);
        AddEnemyUnit(1, 21, 6, false, 8, 26);
        AddEnemyUnit(1, 4, 8, false, 8, 26);
        AddEnemyUnit(1, 2, 9, false, 8, 26);
        AddEnemyUnit(1, 8, 7, false, 8, 26);

        //370
        AddEnemyUnit(1, 7, 25, true, 9, 40, true);
        AddEnemyUnit(2, 8, 9, false, 9, 27);
        AddEnemyUnit(2, 18, 8, false, 9, 28);
        AddEnemyUnit(1, 1, 8, false, 9, 29);
        AddEnemyUnit(1, 2, 9, false, 9, 29);
        AddEnemyUnit(1, 10, 10, false, 9, 29);
        AddEnemyUnit(1, 12, 4, false, 9, 29);
        AddEnemyUnit(1, 23, 7, false, 9, 29);
        AddEnemyUnit(1, 16, 5, false, 9, 29);
        AddEnemyUnit(2, 15, 3, false, 9, 28);
        AddEnemyUnit(2, 5, 6, false, 9, 28);

        for (int i = 0; i < 9; i++)
        {
            GameController.Instance.SetPlayerPlatoon(i);
        }
        maxDeployedPlatoon = 8;
    }

    private void Level11Setup()
    {
        Debug.Log("Level 11 Setup");

        Platoon p0 = new Platoon(0);
        enemyPlatoons[0] = p0;

        Platoon p1 = new Platoon(1);
        enemyPlatoons[1] = p1;

        Platoon p2 = new Platoon(2);
        enemyPlatoons[2] = p2;

        Platoon p3 = new Platoon(3);
        enemyPlatoons[3] = p3;

        Platoon p4 = new Platoon(4);
        enemyPlatoons[4] = p4;

        Platoon p5 = new Platoon(5);
        enemyPlatoons[5] = p5;

        Platoon p6 = new Platoon(6);
        enemyPlatoons[6] = p6;

        Platoon p7 = new Platoon(7);
        enemyPlatoons[7] = p7;

        Platoon p8 = new Platoon(8);
        enemyPlatoons[8] = p8;

        Platoon p9 = new Platoon(9);
        enemyPlatoons[9] = p9;

        Platoon p10 = new Platoon(10);
        enemyPlatoons[10] = p10;

        Platoon p11 = new Platoon(11);
        enemyPlatoons[11] = p11;

        Platoon p12 = new Platoon(12);
        enemyPlatoons[12] = p12;

        Platoon p13 = new Platoon(13);
        enemyPlatoons[13] = p13;

        Platoon p14 = new Platoon(14);
        enemyPlatoons[14] = p14;

        enemyPlatoonsRemaining = 15;

        //AddEnemyUnit(int charType, int position, int charClass, bool leader, int platoon, int level)

        //530
        AddEnemyUnit(1, 22, 27, true, 0, 50, true);
        AddEnemyUnit(2, 5, 7, false, 0, 35);
        AddEnemyUnit(2, 13, 8, false, 0, 35);
        AddEnemyUnit(2, 15, 9, false, 0, 35);
        AddEnemyUnit(1, 20, 21, false, 0, 45);
        AddEnemyUnit(1, 24, 17, false, 0, 45);
        AddEnemyUnit(1, 3, 12, false, 0, 45);
        AddEnemyUnit(1, 4, 9, false, 0, 45);
        AddEnemyUnit(1, 18, 4, false, 0, 45);
        AddEnemyUnit(1, 12, 7, false, 0, 45);

        //370
        AddEnemyUnit(1, 18, 19, true, 1, 40, false);
        AddEnemyUnit(2, 1, 2, false, 1, 30);
        AddEnemyUnit(2, 22, 0, false, 1, 30);
        AddEnemyUnit(2, 14, 3, false, 1, 27);
        AddEnemyUnit(1, 13, 6, false, 1, 27);
        AddEnemyUnit(1, 19, 5, false, 1, 27);
        AddEnemyUnit(1, 11, 7, false, 1, 27);
        AddEnemyUnit(1, 9, 4, false, 1, 27);
        AddEnemyUnit(1, 3, 9, false, 1, 27);
        AddEnemyUnit(1, 0, 10, false, 1, 27);
        AddEnemyUnit(1, 15, 5, false, 1, 27);
        AddEnemyUnit(1, 2, 9, false, 1, 27);
        AddEnemyUnit(1, 7, 7, false, 1, 27);

        //370
        AddEnemyUnit(1, 18, 20, true, 2, 40, false);
        AddEnemyUnit(2, 1, 1, false, 2, 30);
        AddEnemyUnit(2, 22, 0, false, 2, 30);
        AddEnemyUnit(2, 14, 3, false, 2, 27);
        AddEnemyUnit(1, 13, 4, false, 2, 27);
        AddEnemyUnit(1, 19, 5, false, 2, 27);
        AddEnemyUnit(1, 11, 6, false, 2, 27);
        AddEnemyUnit(1, 9, 7, false, 2, 27);
        AddEnemyUnit(1, 3, 8, false, 2, 27);
        AddEnemyUnit(1, 0, 10, false, 2, 27);
        AddEnemyUnit(1, 15, 5, false, 2, 27);
        AddEnemyUnit(1, 2, 9, false, 2, 27);
        AddEnemyUnit(1, 7, 7, false, 2, 27);

        //370
        AddEnemyUnit(1, 18, 21, true, 3, 40, false);
        AddEnemyUnit(2, 1, 2, false, 3, 30);
        AddEnemyUnit(2, 22, 1, false, 3, 30);
        AddEnemyUnit(2, 14, 6, false, 3, 27);
        AddEnemyUnit(1, 13, 7, false, 3, 27);
        AddEnemyUnit(1, 19, 6, false, 3, 27);
        AddEnemyUnit(1, 11, 5, false, 3, 27);
        AddEnemyUnit(1, 9, 4, false, 3, 27);
        AddEnemyUnit(1, 3, 9, false, 3, 27);
        AddEnemyUnit(1, 0, 8, false, 3, 27);
        AddEnemyUnit(1, 15, 5, false, 3, 27);
        AddEnemyUnit(1, 2, 9, false, 3, 27);
        AddEnemyUnit(1, 7, 7, false, 3, 27);

        //370
        AddEnemyUnit(1, 18, 22, true, 4, 40, false);
        AddEnemyUnit(2, 1, 1, false, 4, 30);
        AddEnemyUnit(2, 22, 0, false, 4, 30);
        AddEnemyUnit(2, 14, 6, false, 4, 27);
        AddEnemyUnit(1, 13, 5, false, 4, 27);
        AddEnemyUnit(1, 19, 6, false, 4, 27);
        AddEnemyUnit(1, 11, 7, false, 4, 27);
        AddEnemyUnit(1, 9, 8, false, 4, 27);
        AddEnemyUnit(1, 3, 9, false, 4, 27);
        AddEnemyUnit(1, 0, 10, false, 4, 27);
        AddEnemyUnit(1, 15, 5, false, 4, 27);
        AddEnemyUnit(1, 2, 9, false, 4, 27);
        AddEnemyUnit(1, 7, 7, false, 4, 27);

        //390
        AddEnemyUnit(1, 17, 15, true, 5, 46, false);
        AddEnemyUnit(2, 15, 6, false, 5, 32);
        AddEnemyUnit(2, 8, 4, false, 5, 30);
        AddEnemyUnit(1, 14, 5, false, 5, 28);
        AddEnemyUnit(1, 10, 4, false, 5, 28);
        AddEnemyUnit(1, 11, 7, false, 5, 28);
        AddEnemyUnit(1, 1, 8, false, 5, 28);
        AddEnemyUnit(1, 0, 9, false, 5, 28);
        AddEnemyUnit(1, 24, 5, false, 5, 28);
        AddEnemyUnit(1, 22, 4, false, 5, 28);
        AddEnemyUnit(1, 2, 10, false, 5, 28);
        AddEnemyUnit(1, 7, 6, false, 5, 28);

        //390
        AddEnemyUnit(1, 17, 16, true, 6, 46, false);
        AddEnemyUnit(2, 15, 6, false, 6, 32);
        AddEnemyUnit(2, 5, 5, false, 6, 30);
        AddEnemyUnit(1, 14, 5, false, 6, 28);
        AddEnemyUnit(1, 10, 4, false, 6, 28);
        AddEnemyUnit(1, 11, 7, false, 6, 28);
        AddEnemyUnit(1, 3, 8, false, 6, 28);
        AddEnemyUnit(1, 4, 9, false, 6, 28);
        AddEnemyUnit(1, 24, 5, false, 6, 28);
        AddEnemyUnit(1, 22, 4, false, 6, 28);
        AddEnemyUnit(1, 2, 10, false, 6, 28);
        AddEnemyUnit(1, 7, 6, false, 6, 28);

        //390
        AddEnemyUnit(1, 17, 17, true, 7, 46, false);
        AddEnemyUnit(2, 15, 3, false, 7, 32);
        AddEnemyUnit(2, 8, 5, false, 7, 30);
        AddEnemyUnit(1, 14, 5, false, 7, 28);
        AddEnemyUnit(1, 10, 4, false, 7, 28);
        AddEnemyUnit(1, 11, 7, false, 7, 28);
        AddEnemyUnit(1, 1, 8, false, 7, 28);
        AddEnemyUnit(1, 0, 9, false, 7, 28);
        AddEnemyUnit(1, 24, 5, false, 7, 28);
        AddEnemyUnit(1, 22, 4, false, 7, 28);
        AddEnemyUnit(1, 2, 10, false, 7, 28);
        AddEnemyUnit(1, 7, 6, false, 7, 28);

        //390
        AddEnemyUnit(1, 17, 18, true, 8, 46, false);
        AddEnemyUnit(2, 15, 3, false, 8, 32);
        AddEnemyUnit(2, 5, 4, false, 8, 30);
        AddEnemyUnit(1, 14, 5, false, 8, 28);
        AddEnemyUnit(1, 10, 4, false, 8, 28);
        AddEnemyUnit(1, 11, 7, false, 8, 28);
        AddEnemyUnit(1, 3, 8, false, 8, 28);
        AddEnemyUnit(1, 4, 9, false, 8, 28);
        AddEnemyUnit(1, 24, 5, false, 8, 28);
        AddEnemyUnit(1, 22, 4, false, 8, 28);
        AddEnemyUnit(1, 2, 10, false, 8, 28);
        AddEnemyUnit(1, 7, 6, false, 8, 28);

        //420
        AddEnemyUnit(1, 12, 32, true, 9, 40, false);
        AddEnemyUnit(1, 0, 21, false, 9, 31);
        AddEnemyUnit(1, 1, 21, false, 9, 31);
        AddEnemyUnit(1, 2, 21, false, 9, 31);
        AddEnemyUnit(1, 3, 21, false, 9, 31);
        AddEnemyUnit(1, 4, 21, false, 9, 31);
        AddEnemyUnit(1, 20, 21, false, 9, 31);
        AddEnemyUnit(1, 21, 21, false, 9, 32);
        AddEnemyUnit(1, 22, 21, false, 9, 31);
        AddEnemyUnit(1, 23, 21, false, 9, 32);
        AddEnemyUnit(1, 24, 21, false, 9, 32);
        AddEnemyUnit(1, 17, 21, false, 9, 32);
        AddEnemyUnit(1, 7, 21, false, 9, 31);

        //420
        AddEnemyUnit(1, 17, 16, true, 10, 40, true);
        AddEnemyUnit(2, 5, 7, false, 10, 30);
        AddEnemyUnit(2, 23, 8, false, 10, 30);
        AddEnemyUnit(2, 20, 9, false, 10, 30);
        AddEnemyUnit(2, 2, 2, false, 10, 30);
        AddEnemyUnit(2, 7, 1, false, 10, 30);
        AddEnemyUnit(2, 8, 4, false, 10, 35);
        AddEnemyUnit(2, 22, 6, false, 10, 35);
        AddEnemyUnit(2, 12, 3, false, 10, 35);

        //420
        AddEnemyUnit(1, 17, 31, true, 11, 40, true);
        AddEnemyUnit(2, 5, 4, false, 11, 30);
        AddEnemyUnit(1, 8, 6, false, 11, 35);
        AddEnemyUnit(1, 23, 5, false, 11, 35);
        AddEnemyUnit(1, 15, 4, false, 11, 35);
        AddEnemyUnit(1, 4, 9, false, 11, 35);
        AddEnemyUnit(1, 22, 15, false, 11, 30);
        AddEnemyUnit(1, 3, 13, false, 11, 30);
        AddEnemyUnit(1, 2, 14, false, 11, 30);
        AddEnemyUnit(1, 10, 20, false, 11, 30);
        AddEnemyUnit(1, 24, 21, false, 11, 30);
        AddEnemyUnit(1, 20, 16, false, 11, 30);

        //420
        AddEnemyUnit(1, 17, 28, true, 12, 40, true);
        AddEnemyUnit(2, 5, 5, false, 12, 30);
        AddEnemyUnit(1, 8, 7, false, 12, 35);
        AddEnemyUnit(1, 23, 5, false, 12, 35);
        AddEnemyUnit(1, 15, 4, false, 12, 35);
        AddEnemyUnit(1, 4, 10, false, 12, 35);
        AddEnemyUnit(1, 22, 15, false, 12, 30);
        AddEnemyUnit(1, 3, 12, false, 12, 30);
        AddEnemyUnit(1, 2, 14, false, 12, 30);
        AddEnemyUnit(1, 10, 19, false, 12, 30);
        AddEnemyUnit(1, 24, 22, false, 12, 30);
        AddEnemyUnit(1, 20, 17, false, 12, 30);

        //420
        AddEnemyUnit(1, 17, 29, true, 13, 40, true);
        AddEnemyUnit(2, 5, 5, false, 13, 30);
        AddEnemyUnit(1, 8, 7, false, 13, 35);
        AddEnemyUnit(1, 23, 5, false, 13, 35);
        AddEnemyUnit(1, 15, 8, false, 13, 35);
        AddEnemyUnit(1, 4, 9, false, 13, 35);
        AddEnemyUnit(1, 22, 16, false, 13, 30);
        AddEnemyUnit(1, 3, 11, false, 13, 30);
        AddEnemyUnit(1, 2, 13, false, 13, 30);
        AddEnemyUnit(1, 10, 23, false, 13, 30);
        AddEnemyUnit(1, 24, 21, false, 13, 30);
        AddEnemyUnit(1, 20, 18, false, 13, 30);

        //420
        AddEnemyUnit(1, 17, 30, true, 14, 40, true);
        AddEnemyUnit(2, 5, 4, false, 14, 30);
        AddEnemyUnit(1, 8, 6, false, 14, 35);
        AddEnemyUnit(1, 23, 5, false, 14, 35);
        AddEnemyUnit(1, 15, 8, false, 14, 35);
        AddEnemyUnit(1, 4, 10, false, 14, 35);
        AddEnemyUnit(1, 22, 17, false, 14, 30);
        AddEnemyUnit(1, 3, 11, false, 14, 30);
        AddEnemyUnit(1, 2, 12, false, 14, 30);
        AddEnemyUnit(1, 10, 23, false, 14, 30);
        AddEnemyUnit(1, 24, 22, false, 14, 30);
        AddEnemyUnit(1, 20, 18, false, 14, 30);

        for (int i = 0; i < 9; i++)
        {
            GameController.Instance.SetPlayerPlatoon(i);
        }
        maxDeployedPlatoon = 8;
    }

    private void Level12Setup()
    {
        Debug.Log("Level 12 Setup");

        Platoon p0 = new Platoon(0);
        enemyPlatoons[0] = p0;

        Platoon p1 = new Platoon(1);
        enemyPlatoons[1] = p1;

        Platoon p2 = new Platoon(2);
        enemyPlatoons[2] = p2;

        Platoon p3 = new Platoon(3);
        enemyPlatoons[3] = p3;

        Platoon p4 = new Platoon(4);
        enemyPlatoons[4] = p4;

        Platoon p5 = new Platoon(5);
        enemyPlatoons[5] = p5;

        Platoon p6 = new Platoon(6);
        enemyPlatoons[6] = p6;

        Platoon p7 = new Platoon(7);
        enemyPlatoons[7] = p7;

        Platoon p8 = new Platoon(8);
        enemyPlatoons[8] = p8;

        Platoon p9 = new Platoon(9);
        enemyPlatoons[9] = p9;

        Platoon p10 = new Platoon(10);
        enemyPlatoons[10] = p10;

        Platoon p11 = new Platoon(11);
        enemyPlatoons[11] = p11;

        enemyPlatoonsRemaining = 12;

        //AddEnemyUnit(int charType, int position, int charClass, bool leader, int platoon, int level)


        //575
        AddEnemyUnit(1, 17, 43, true, 0, 55, true);
        AddEnemyUnit(2, 5, 7, false, 0, 40);
        AddEnemyUnit(2, 23, 8, false, 0, 40);
        AddEnemyUnit(2, 20, 4, false, 0, 45);
        AddEnemyUnit(2, 8, 5, false, 0, 45);
        AddEnemyUnit(2, 22, 6, false, 0, 50);
        AddEnemyUnit(2, 12, 3, false, 0, 50);
        AddEnemyUnit(3, 2, 6, false, 0, 40);
        AddEnemyUnit(3, 7, 4, false, 0, 40);


        //415
        AddEnemyUnit(1, 18, 24, true, 1, 45, false);
        AddEnemyUnit(2, 8, 4, false, 1, 30);
        AddEnemyUnit(2, 17, 3, false, 1, 30);
        AddEnemyUnit(3, 1, 0, false, 1, 30);
        AddEnemyUnit(3, 22, 1, false, 1, 30);
        AddEnemyUnit(1, 21, 4, false, 1, 30);
        AddEnemyUnit(1, 23, 5, false, 1, 30);
        AddEnemyUnit(1, 15, 6, false, 1, 30);
        AddEnemyUnit(1, 19, 7, false, 1, 30);
        AddEnemyUnit(1, 0, 10, false, 1, 30);
        AddEnemyUnit(1, 2, 9, false, 1, 30);
        AddEnemyUnit(1, 6, 8, false, 1, 30);

        //415
        AddEnemyUnit(1, 18, 27, true, 2, 45, false);
        AddEnemyUnit(2, 5, 4, false, 2, 30);
        AddEnemyUnit(2, 17, 6, false, 2, 30);
        AddEnemyUnit(3, 3, 2, false, 2, 30);
        AddEnemyUnit(3, 22, 3, false, 2, 30);
        AddEnemyUnit(1, 21, 4, false, 2, 30);
        AddEnemyUnit(1, 23, 5, false, 2, 30);
        AddEnemyUnit(1, 15, 6, false, 2, 30);
        AddEnemyUnit(1, 19, 7, false, 2, 30);
        AddEnemyUnit(1, 4, 10, false, 2, 30);
        AddEnemyUnit(1, 2, 9, false, 2, 30);
        AddEnemyUnit(1, 8, 8, false, 2, 30);

        //415
        AddEnemyUnit(1, 18, 34, true, 3, 45, false);
        AddEnemyUnit(2, 8, 5, false, 3, 30);
        AddEnemyUnit(2, 17, 6, false, 3, 30);
        AddEnemyUnit(3, 22, 0, false, 3, 30);
        AddEnemyUnit(3, 1, 2, false, 3, 30);
        AddEnemyUnit(1, 21, 4, false, 3, 30);
        AddEnemyUnit(1, 23, 5, false, 3, 30);
        AddEnemyUnit(1, 15, 6, false, 3, 30);
        AddEnemyUnit(1, 19, 7, false, 3, 30);
        AddEnemyUnit(1, 0, 10, false, 3, 30);
        AddEnemyUnit(1, 2, 9, false, 3, 30);
        AddEnemyUnit(1, 6, 8, false, 3, 30);

        //415
        AddEnemyUnit(1, 18, 33, true, 4, 45, false);
        AddEnemyUnit(2, 5, 5, false, 4, 30);
        AddEnemyUnit(2, 17, 3, false, 4, 30);
        AddEnemyUnit(3, 22, 1, false, 4, 30);
        AddEnemyUnit(3, 3, 3, false, 4, 30);
        AddEnemyUnit(1, 21, 4, false, 4, 30);
        AddEnemyUnit(1, 23, 5, false, 4, 30);
        AddEnemyUnit(1, 15, 6, false, 4, 30);
        AddEnemyUnit(1, 19, 7, false, 4, 30);
        AddEnemyUnit(1, 4, 10, false, 4, 30);
        AddEnemyUnit(1, 2, 9, false, 4, 30);
        AddEnemyUnit(1, 8, 8, false, 4, 30);

        //415
        AddEnemyUnit(1, 18, 32, true, 5, 45, false);
        AddEnemyUnit(2, 5, 5, false, 5, 30);
        AddEnemyUnit(2, 17, 6, false, 5, 30);
        AddEnemyUnit(3, 22, 0, false, 5, 30);
        AddEnemyUnit(3, 3, 3, false, 5, 30);
        AddEnemyUnit(1, 21, 4, false, 5, 30);
        AddEnemyUnit(1, 23, 5, false, 5, 30);
        AddEnemyUnit(1, 15, 6, false, 5, 30);
        AddEnemyUnit(1, 19, 7, false, 5, 30);
        AddEnemyUnit(1, 4, 10, false, 5, 30);
        AddEnemyUnit(1, 2, 9, false, 5, 30);
        AddEnemyUnit(1, 8, 8, false, 5, 30);

        //415
        AddEnemyUnit(1, 18, 26, true, 6, 45, false);
        AddEnemyUnit(2, 8, 4, false, 6, 30);
        AddEnemyUnit(2, 17, 3, false, 6, 30);
        AddEnemyUnit(3, 22, 1, false, 6, 30);
        AddEnemyUnit(3, 1, 2, false, 6, 30);
        AddEnemyUnit(1, 21, 4, false, 6, 30);
        AddEnemyUnit(1, 23, 5, false, 6, 30);
        AddEnemyUnit(1, 15, 6, false, 6, 30);
        AddEnemyUnit(1, 19, 7, false, 6, 30);
        AddEnemyUnit(1, 0, 10, false, 6, 30);
        AddEnemyUnit(1, 2, 9, false, 6, 30);
        AddEnemyUnit(1, 6, 8, false, 6, 30);
        

        //432
        AddEnemyUnit(1, 17, 35, true, 7, 45, true);
        AddEnemyUnit(2, 5, 4, false, 7, 30);
        AddEnemyUnit(2, 8, 5, false, 7, 30);
        AddEnemyUnit(2, 15, 3, false, 7, 34);
        AddEnemyUnit(2, 19, 6, false, 7, 34);
        AddEnemyUnit(3, 12, 3, false, 7, 33);
        AddEnemyUnit(3, 14, 2, false, 7, 33);
        AddEnemyUnit(3, 10, 1, false, 7, 33);
        AddEnemyUnit(1, 22, 22, false, 7, 30);
        AddEnemyUnit(1, 7, 15, false, 7, 30);
        AddEnemyUnit(1, 2, 14, false, 7, 30);

        //432
        AddEnemyUnit(1, 17, 36, true, 8, 45, true);
        AddEnemyUnit(2, 5, 4, false, 8, 30);
        AddEnemyUnit(2, 8, 5, false, 8, 30);
        AddEnemyUnit(2, 15, 3, false, 8, 34);
        AddEnemyUnit(2, 19, 6, false, 8, 34);
        AddEnemyUnit(3, 12, 3, false, 8, 33);
        AddEnemyUnit(3, 14, 2, false, 8, 33);
        AddEnemyUnit(3, 10, 0, false, 8, 33);
        AddEnemyUnit(1, 22, 19, false, 8, 30);
        AddEnemyUnit(1, 7, 18, false, 8, 30);
        AddEnemyUnit(1, 2, 13, false, 8, 30);

        //432
        AddEnemyUnit(1, 17, 40, true, 9, 45, true);
        AddEnemyUnit(2, 5, 4, false, 9, 30);
        AddEnemyUnit(2, 8, 5, false, 9, 30);
        AddEnemyUnit(2, 15, 3, false, 9, 34);
        AddEnemyUnit(2, 19, 6, false, 9, 34);
        AddEnemyUnit(3, 12, 3, false, 9, 33);
        AddEnemyUnit(3, 14, 1, false, 9, 33);
        AddEnemyUnit(3, 10, 0, false, 9, 33);
        AddEnemyUnit(1, 22, 20, false, 9, 30);
        AddEnemyUnit(1, 7, 16, false, 9, 30);
        AddEnemyUnit(1, 2, 12, false, 9, 30);

        //432
        AddEnemyUnit(1, 17, 39, true, 10, 45, true);
        AddEnemyUnit(2, 5, 4, false, 10, 30);
        AddEnemyUnit(2, 8, 5, false, 10, 30);
        AddEnemyUnit(2, 15, 3, false, 10, 34);
        AddEnemyUnit(2, 19, 6, false, 10, 34);
        AddEnemyUnit(3, 12, 2, false, 10, 33);
        AddEnemyUnit(3, 14, 1, false, 10, 33);
        AddEnemyUnit(3, 10, 0, false, 10, 33);
        AddEnemyUnit(1, 22, 23, false, 10, 30);
        AddEnemyUnit(1, 7, 17, false, 10, 30);
        AddEnemyUnit(1, 2, 11, false, 10, 30);


        //500
        AddEnemyUnit(1, 7, 37, true, 11, 50, false);
        AddEnemyUnit(3, 8, 10, false, 11, 30);
        AddEnemyUnit(3, 18, 8, false, 11, 30);
        AddEnemyUnit(3, 15, 7, false, 11, 30);
        AddEnemyUnit(3, 5, 0, false, 11, 30);
        AddEnemyUnit(3, 2, 2, false, 11, 30);
        AddEnemyUnit(1, 1, 13, false, 11, 40);
        AddEnemyUnit(1, 3, 14, false, 11, 40);
        AddEnemyUnit(1, 10, 18, false, 11, 40);
        AddEnemyUnit(1, 12, 15, false, 11, 40);
        AddEnemyUnit(1, 23, 21, false, 11, 40);
        AddEnemyUnit(1, 16, 19, false, 11, 40);
        AddEnemyUnit(1, 0, 25, false, 11, 40);

        for (int i = 0; i < 9; i++)
        {
            GameController.Instance.SetPlayerPlatoon(i);
        }
        maxDeployedPlatoon = 8;
    }

    private void Level13Setup()
    {
        Debug.Log("Level 13 Setup");

        Platoon p0 = new Platoon(0);
        enemyPlatoons[0] = p0;

        Platoon p1 = new Platoon(1);
        enemyPlatoons[1] = p1;

        Platoon p2 = new Platoon(2);
        enemyPlatoons[2] = p2;

        Platoon p3 = new Platoon(3);
        enemyPlatoons[3] = p3;

        Platoon p4 = new Platoon(4);
        enemyPlatoons[4] = p4;

        Platoon p5 = new Platoon(5);
        enemyPlatoons[5] = p5;

        Platoon p6 = new Platoon(6);
        enemyPlatoons[6] = p6;

        Platoon p7 = new Platoon(7);
        enemyPlatoons[7] = p7;

        Platoon p8 = new Platoon(8);
        enemyPlatoons[8] = p8;

        Platoon p9 = new Platoon(9);
        enemyPlatoons[9] = p9;

        Platoon p10 = new Platoon(10);
        enemyPlatoons[10] = p10;

        enemyPlatoonsRemaining = 11;

        //AddEnemyUnit(int charType, int position, int charClass, bool leader, int platoon, int level)


        //620
        AddEnemyUnit(1, 17, 56, true, 0, 60, false);
        AddEnemyUnit(2, 5, 7, false, 0, 40);
        AddEnemyUnit(2, 23, 9, false, 0, 40);
        AddEnemyUnit(3, 2, 6, false, 0, 45);
        AddEnemyUnit(3, 11, 7, false, 0, 45);
        AddEnemyUnit(3, 15, 8, false, 0, 45);
        AddEnemyUnit(3, 21, 5, false, 0, 45);
        AddEnemyUnit(3, 20, 0, false, 0, 55);
        AddEnemyUnit(3, 12, 1, false, 0, 55);
        AddEnemyUnit(3, 3, 2, false, 0, 55);
        AddEnemyUnit(3, 22, 3, false, 0, 55);


        //415
        AddEnemyUnit(1, 18, 24, true, 1, 50, true);
        AddEnemyUnit(2, 8, 4, false, 1, 30);
        AddEnemyUnit(2, 17, 3, false, 1, 35);
        AddEnemyUnit(3, 1, 0, false, 1, 35);
        AddEnemyUnit(3, 22, 1, false, 1, 35);
        AddEnemyUnit(1, 21, 4, false, 1, 40);
        AddEnemyUnit(1, 23, 5, false, 1, 40);
        AddEnemyUnit(1, 15, 6, false, 1, 40);
        AddEnemyUnit(1, 19, 7, false, 1, 40);
        AddEnemyUnit(1, 0, 10, false, 1, 40);
        AddEnemyUnit(1, 2, 9, false, 1, 40);
        AddEnemyUnit(1, 6, 8, false, 1, 40);

        //415
        AddEnemyUnit(1, 18, 25, true, 2, 50, true);
        AddEnemyUnit(2, 5, 4, false, 2, 30);
        AddEnemyUnit(2, 17, 6, false, 2, 35);
        AddEnemyUnit(3, 3, 2, false, 2, 35);
        AddEnemyUnit(3, 22, 3, false, 2, 35);
        AddEnemyUnit(1, 21, 4, false, 2, 40);
        AddEnemyUnit(1, 23, 5, false, 2, 40);
        AddEnemyUnit(1, 15, 6, false, 2, 40);
        AddEnemyUnit(1, 19, 7, false, 2, 40);
        AddEnemyUnit(1, 4, 10, false, 2, 40);
        AddEnemyUnit(1, 2, 9, false, 2, 40);
        AddEnemyUnit(1, 8, 8, false, 2, 40);

        //415
        AddEnemyUnit(1, 18, 38, true, 3, 50, true);
        AddEnemyUnit(2, 8, 5, false, 3, 30);
        AddEnemyUnit(2, 17, 6, false, 3, 35);
        AddEnemyUnit(3, 22, 0, false, 3, 35);
        AddEnemyUnit(3, 1, 2, false, 3, 35);
        AddEnemyUnit(1, 21, 4, false, 3, 40);
        AddEnemyUnit(1, 23, 5, false, 3, 40);
        AddEnemyUnit(1, 15, 6, false, 3, 40);
        AddEnemyUnit(1, 19, 7, false, 3, 40);
        AddEnemyUnit(1, 0, 10, false, 3, 40);
        AddEnemyUnit(1, 2, 9, false, 3, 40);
        AddEnemyUnit(1, 6, 8, false, 3, 40);

        //415
        AddEnemyUnit(1, 18, 36, true, 4, 50, true);
        AddEnemyUnit(2, 5, 5, false, 4, 30);
        AddEnemyUnit(2, 17, 3, false, 4, 35);
        AddEnemyUnit(3, 22, 1, false, 4, 35);
        AddEnemyUnit(3, 3, 3, false, 4, 35);
        AddEnemyUnit(1, 21, 4, false, 4, 40);
        AddEnemyUnit(1, 23, 5, false, 4, 40);
        AddEnemyUnit(1, 15, 6, false, 4, 40);
        AddEnemyUnit(1, 19, 7, false, 4, 40);
        AddEnemyUnit(1, 4, 10, false, 4, 40);
        AddEnemyUnit(1, 2, 9, false, 4, 40);
        AddEnemyUnit(1, 8, 8, false, 4, 40);

        //415
        AddEnemyUnit(1, 18, 40, true, 5, 50, true);
        AddEnemyUnit(2, 5, 5, false, 5, 30);
        AddEnemyUnit(2, 17, 6, false, 5, 35);
        AddEnemyUnit(3, 22, 0, false, 5, 35);
        AddEnemyUnit(3, 3, 3, false, 5, 35);
        AddEnemyUnit(1, 21, 4, false, 5, 40);
        AddEnemyUnit(1, 23, 5, false, 5, 40);
        AddEnemyUnit(1, 15, 6, false, 5, 40);
        AddEnemyUnit(1, 19, 7, false, 5, 40);
        AddEnemyUnit(1, 4, 10, false, 5, 40);
        AddEnemyUnit(1, 2, 9, false, 5, 40);
        AddEnemyUnit(1, 8, 8, false, 5, 40);

        //415
        AddEnemyUnit(1, 18, 33, true, 6, 50, true);
        AddEnemyUnit(2, 8, 4, false, 6, 30);
        AddEnemyUnit(2, 17, 3, false, 6, 35);
        AddEnemyUnit(3, 22, 1, false, 6, 35);
        AddEnemyUnit(3, 1, 2, false, 6, 35);
        AddEnemyUnit(1, 21, 4, false, 6, 40);
        AddEnemyUnit(1, 23, 5, false, 6, 40);
        AddEnemyUnit(1, 15, 6, false, 6, 40);
        AddEnemyUnit(1, 19, 7, false, 6, 40);
        AddEnemyUnit(1, 0, 10, false, 6, 40);
        AddEnemyUnit(1, 2, 9, false, 6, 40);
        AddEnemyUnit(1, 6, 8, false, 6, 40);


        //440
        AddEnemyUnit(1, 17, 28, true, 7, 55, false);
        AddEnemyUnit(2, 5, 4, false, 7, 40);
        AddEnemyUnit(2, 8, 5, false, 7, 40);
        AddEnemyUnit(2, 15, 3, false, 7, 40);
        AddEnemyUnit(2, 19, 6, false, 7, 40);
        AddEnemyUnit(3, 12, 3, false, 7, 40);
        AddEnemyUnit(3, 14, 2, false, 7, 40);
        AddEnemyUnit(3, 10, 1, false, 7, 40);
        AddEnemyUnit(1, 22, 22, false, 7, 40);
        AddEnemyUnit(1, 7, 15, false, 7, 40);
        AddEnemyUnit(1, 2, 14, false, 7, 40);

        //440
        AddEnemyUnit(1, 17, 31, true, 8, 55, false);
        AddEnemyUnit(2, 5, 4, false, 8, 40);
        AddEnemyUnit(2, 8, 5, false, 8, 40);
        AddEnemyUnit(2, 15, 3, false, 8, 40);
        AddEnemyUnit(2, 19, 6, false, 8, 40);
        AddEnemyUnit(3, 12, 3, false, 8, 40);
        AddEnemyUnit(3, 14, 2, false, 8, 40);
        AddEnemyUnit(3, 10, 0, false, 8, 40);
        AddEnemyUnit(1, 22, 19, false, 8, 40);
        AddEnemyUnit(1, 7, 18, false, 8, 40);
        AddEnemyUnit(1, 2, 13, false, 8, 40);

        //440
        AddEnemyUnit(1, 17, 30, true, 9, 55, false);
        AddEnemyUnit(2, 5, 4, false, 9, 40);
        AddEnemyUnit(2, 8, 5, false, 9, 40);
        AddEnemyUnit(2, 15, 3, false, 9, 40);
        AddEnemyUnit(2, 19, 6, false, 9, 40);
        AddEnemyUnit(3, 12, 3, false, 9, 40);
        AddEnemyUnit(3, 14, 1, false, 9, 40);
        AddEnemyUnit(3, 10, 0, false, 9, 40);
        AddEnemyUnit(1, 22, 20, false, 9, 40);
        AddEnemyUnit(1, 7, 16, false, 9, 40);
        AddEnemyUnit(1, 2, 12, false, 9, 40);

        //440
        AddEnemyUnit(1, 17, 29, true, 10, 55, false);
        AddEnemyUnit(2, 5, 4, false, 10, 40);
        AddEnemyUnit(2, 8, 5, false, 10, 40);
        AddEnemyUnit(2, 15, 3, false, 10, 40);
        AddEnemyUnit(2, 19, 6, false, 10, 40);
        AddEnemyUnit(3, 12, 2, false, 10, 40);
        AddEnemyUnit(3, 14, 1, false, 10, 40);
        AddEnemyUnit(3, 10, 0, false, 10, 40);
        AddEnemyUnit(1, 22, 23, false, 10, 40);
        AddEnemyUnit(1, 7, 17, false, 10, 40);
        AddEnemyUnit(1, 2, 11, false, 10, 40);

        for (int i = 0; i < 10; i++)
        {
            GameController.Instance.SetPlayerPlatoon(i);
        }
        maxDeployedPlatoon = 9;
    }

    private void Level14Setup()
    {
        Debug.Log("Level 14 Setup");

        Platoon p0 = new Platoon(0);
        enemyPlatoons[0] = p0;

        Platoon p1 = new Platoon(1);
        enemyPlatoons[1] = p1;

        Platoon p2 = new Platoon(2);
        enemyPlatoons[2] = p2;

        Platoon p3 = new Platoon(3);
        enemyPlatoons[3] = p3;

        Platoon p4 = new Platoon(4);
        enemyPlatoons[4] = p4;

        Platoon p5 = new Platoon(5);
        enemyPlatoons[5] = p5;

        Platoon p6 = new Platoon(6);
        enemyPlatoons[6] = p6;

        Platoon p7 = new Platoon(7);
        enemyPlatoons[7] = p7;

        Platoon p8 = new Platoon(8);
        enemyPlatoons[8] = p8;

        Platoon p9 = new Platoon(9);
        enemyPlatoons[9] = p9;

        Platoon p10 = new Platoon(10);
        enemyPlatoons[10] = p10;

        Platoon p11 = new Platoon(11);
        enemyPlatoons[11] = p11;

        enemyPlatoonsRemaining = 12;

        //AddEnemyUnit(int charType, int position, int charClass, bool leader, int platoon, int level)


        //665
        AddEnemyUnit(1, 17, 56, true, 0, 65, true);
        AddEnemyUnit(2, 5, 7, false, 0, 45);
        AddEnemyUnit(2, 23, 9, false, 0, 45);
        AddEnemyUnit(3, 2, 6, false, 0, 50);
        AddEnemyUnit(3, 11, 7, false, 0, 50);
        AddEnemyUnit(3, 15, 8, false, 0, 50);
        AddEnemyUnit(3, 21, 5, false, 0, 50);
        AddEnemyUnit(3, 20, 0, false, 0, 55);
        AddEnemyUnit(3, 12, 1, false, 0, 55);
        AddEnemyUnit(3, 3, 2, false, 0, 55);
        AddEnemyUnit(3, 22, 3, false, 0, 55);


        //495
        AddEnemyUnit(1, 18, 24, true, 1, 55, false);
        AddEnemyUnit(2, 8, 4, false, 1, 35);
        AddEnemyUnit(2, 17, 3, false, 1, 40);
        AddEnemyUnit(3, 1, 0, false, 1, 40);
        AddEnemyUnit(3, 22, 1, false, 1, 40);
        AddEnemyUnit(1, 21, 4, false, 1, 40);
        AddEnemyUnit(1, 23, 5, false, 1, 40);
        AddEnemyUnit(1, 15, 6, false, 1, 40);
        AddEnemyUnit(1, 19, 7, false, 1, 40);
        AddEnemyUnit(1, 0, 10, false, 1, 40);
        AddEnemyUnit(1, 2, 9, false, 1, 40);
        AddEnemyUnit(1, 6, 8, false, 1, 40);

        //495
        AddEnemyUnit(1, 18, 27, true, 2, 55, false);
        AddEnemyUnit(2, 5, 4, false, 2, 35);
        AddEnemyUnit(2, 17, 6, false, 2, 40);
        AddEnemyUnit(3, 3, 2, false, 2, 40);
        AddEnemyUnit(3, 22, 3, false, 2, 40);
        AddEnemyUnit(1, 21, 4, false, 2, 40);
        AddEnemyUnit(1, 23, 5, false, 2, 40);
        AddEnemyUnit(1, 15, 6, false, 2, 40);
        AddEnemyUnit(1, 19, 7, false, 2, 40);
        AddEnemyUnit(1, 4, 10, false, 2, 40);
        AddEnemyUnit(1, 2, 9, false, 2, 40);
        AddEnemyUnit(1, 8, 8, false, 2, 40);

        //495
        AddEnemyUnit(1, 18, 34, true, 3, 55, false);
        AddEnemyUnit(2, 8, 5, false, 3, 35);
        AddEnemyUnit(2, 17, 6, false, 3, 40);
        AddEnemyUnit(3, 22, 0, false, 3, 40);
        AddEnemyUnit(3, 1, 2, false, 3, 40);
        AddEnemyUnit(1, 21, 4, false, 3, 40);
        AddEnemyUnit(1, 23, 5, false, 3, 40);
        AddEnemyUnit(1, 15, 6, false, 3, 40);
        AddEnemyUnit(1, 19, 7, false, 3, 40);
        AddEnemyUnit(1, 0, 10, false, 3, 40);
        AddEnemyUnit(1, 2, 9, false, 3, 40);
        AddEnemyUnit(1, 6, 8, false, 3, 40);

        //495
        AddEnemyUnit(1, 18, 33, true, 4, 55, false);
        AddEnemyUnit(2, 5, 5, false, 4, 35);
        AddEnemyUnit(2, 17, 3, false, 4, 40);
        AddEnemyUnit(3, 22, 1, false, 4, 40);
        AddEnemyUnit(3, 3, 3, false, 4, 40);
        AddEnemyUnit(1, 21, 4, false, 4, 40);
        AddEnemyUnit(1, 23, 5, false, 4, 40);
        AddEnemyUnit(1, 15, 6, false, 4, 40);
        AddEnemyUnit(1, 19, 7, false, 4, 40);
        AddEnemyUnit(1, 4, 10, false, 4, 40);
        AddEnemyUnit(1, 2, 9, false, 4, 40);
        AddEnemyUnit(1, 8, 8, false, 4, 40);

        //495
        AddEnemyUnit(1, 18, 32, true, 5, 55, false);
        AddEnemyUnit(2, 5, 5, false, 5, 35);
        AddEnemyUnit(2, 17, 6, false, 5, 40);
        AddEnemyUnit(3, 22, 0, false, 5, 40);
        AddEnemyUnit(3, 3, 3, false, 5, 40);
        AddEnemyUnit(1, 21, 4, false, 5, 40);
        AddEnemyUnit(1, 23, 5, false, 5, 40);
        AddEnemyUnit(1, 15, 6, false, 5, 40);
        AddEnemyUnit(1, 19, 7, false, 5, 40);
        AddEnemyUnit(1, 4, 10, false, 5, 40);
        AddEnemyUnit(1, 2, 9, false, 5, 40);
        AddEnemyUnit(1, 8, 8, false, 5, 40);

        //495
        AddEnemyUnit(1, 18, 26, true, 6, 55, false);
        AddEnemyUnit(2, 8, 4, false, 6, 35);
        AddEnemyUnit(2, 17, 3, false, 6, 40);
        AddEnemyUnit(3, 22, 1, false, 6, 40);
        AddEnemyUnit(3, 1, 2, false, 6, 40);
        AddEnemyUnit(1, 21, 4, false, 6, 40);
        AddEnemyUnit(1, 23, 5, false, 6, 40);
        AddEnemyUnit(1, 15, 6, false, 6, 40);
        AddEnemyUnit(1, 19, 7, false, 6, 40);
        AddEnemyUnit(1, 0, 10, false, 6, 40);
        AddEnemyUnit(1, 2, 9, false, 6, 40);
        AddEnemyUnit(1, 6, 8, false, 6, 40);


        //565
        AddEnemyUnit(1, 17, 35, true, 7, 55, true);
        AddEnemyUnit(2, 5, 4, false, 7, 45);
        AddEnemyUnit(2, 8, 5, false, 7, 45);
        AddEnemyUnit(2, 15, 3, false, 7, 45);
        AddEnemyUnit(2, 19, 6, false, 7, 45);
        AddEnemyUnit(3, 12, 3, false, 7, 40);
        AddEnemyUnit(3, 14, 2, false, 7, 40);
        AddEnemyUnit(3, 10, 1, false, 7, 40);
        AddEnemyUnit(1, 22, 22, false, 7, 40);
        AddEnemyUnit(1, 7, 15, false, 7, 40);
        AddEnemyUnit(1, 2, 14, false, 7, 40);

        //565
        AddEnemyUnit(1, 17, 36, true, 8, 55, true);
        AddEnemyUnit(2, 5, 4, false, 8, 45);
        AddEnemyUnit(2, 8, 5, false, 8, 45);
        AddEnemyUnit(2, 15, 3, false, 8, 45);
        AddEnemyUnit(2, 19, 6, false, 8, 45);
        AddEnemyUnit(3, 12, 3, false, 8, 40);
        AddEnemyUnit(3, 14, 2, false, 8, 40);
        AddEnemyUnit(3, 10, 0, false, 8, 40);
        AddEnemyUnit(1, 22, 19, false, 8, 40);
        AddEnemyUnit(1, 7, 18, false, 8, 40);
        AddEnemyUnit(1, 2, 13, false, 8, 40);

        //565
        AddEnemyUnit(1, 17, 40, true, 9, 55, true);
        AddEnemyUnit(2, 5, 4, false, 9, 45);
        AddEnemyUnit(2, 8, 5, false, 9, 45);
        AddEnemyUnit(2, 15, 3, false, 9, 45);
        AddEnemyUnit(2, 19, 6, false, 9, 45);
        AddEnemyUnit(3, 12, 3, false, 9, 40);
        AddEnemyUnit(3, 14, 1, false, 9, 40);
        AddEnemyUnit(3, 10, 0, false, 9, 40);
        AddEnemyUnit(1, 22, 20, false, 9, 40);
        AddEnemyUnit(1, 7, 16, false, 9, 40);
        AddEnemyUnit(1, 2, 12, false, 9, 40);

        //565
        AddEnemyUnit(1, 17, 39, true, 10, 55, true);
        AddEnemyUnit(2, 5, 4, false, 10, 45);
        AddEnemyUnit(2, 8, 5, false, 10, 45);
        AddEnemyUnit(2, 15, 3, false, 10, 45);
        AddEnemyUnit(2, 19, 6, false, 10, 45);
        AddEnemyUnit(3, 12, 2, false, 10, 40);
        AddEnemyUnit(3, 14, 1, false, 10, 40);
        AddEnemyUnit(3, 10, 0, false, 10, 40);
        AddEnemyUnit(1, 22, 23, false, 10, 40);
        AddEnemyUnit(1, 7, 17, false, 10, 40);
        AddEnemyUnit(1, 2, 11, false, 10, 40);


        //600
        AddEnemyUnit(1, 2, 41, true, 11, 60, false);
        AddEnemyUnit(3, 8, 10, false, 11, 40);
        AddEnemyUnit(3, 18, 8, false, 11, 40);
        AddEnemyUnit(3, 15, 4, false, 11, 40);
        AddEnemyUnit(3, 5, 0, false, 11, 40);
        AddEnemyUnit(3, 4, 2, false, 11, 40);
        AddEnemyUnit(1, 1, 13, false, 11, 50);
        AddEnemyUnit(1, 3, 24, false, 11, 50);
        AddEnemyUnit(1, 10, 18, false, 11, 50);
        AddEnemyUnit(1, 12, 15, false, 11, 50);
        AddEnemyUnit(1, 23, 21, false, 11, 50);
        AddEnemyUnit(1, 16, 19, false, 11, 50);
        AddEnemyUnit(1, 0, 25, false, 11, 50);

        for (int i = 0; i < 11; i++)
        {
            GameController.Instance.SetPlayerPlatoon(i);
        }
        maxDeployedPlatoon = 10;
    }

    private void Level15Setup()
    {
        Debug.Log("Level 15 Setup");

        Platoon p0 = new Platoon(0);
        enemyPlatoons[0] = p0;

        Platoon p1 = new Platoon(1);
        enemyPlatoons[1] = p1;

        Platoon p2 = new Platoon(2);
        enemyPlatoons[2] = p2;

        Platoon p3 = new Platoon(3);
        enemyPlatoons[3] = p3;

        Platoon p4 = new Platoon(4);
        enemyPlatoons[4] = p4;

        Platoon p5 = new Platoon(5);
        enemyPlatoons[5] = p5;

        Platoon p6 = new Platoon(6);
        enemyPlatoons[6] = p6;

        Platoon p7 = new Platoon(7);
        enemyPlatoons[7] = p7;

        Platoon p8 = new Platoon(8);
        enemyPlatoons[8] = p8;

        Platoon p9 = new Platoon(9);
        enemyPlatoons[9] = p9;

        Platoon p10 = new Platoon(10);
        enemyPlatoons[10] = p10;

        Platoon p11 = new Platoon(11);
        enemyPlatoons[11] = p11;

        Platoon p12 = new Platoon(12);
        enemyPlatoons[12] = p12;

        enemyPlatoonsRemaining = 13;

        //AddEnemyUnit(int charType, int position, int charClass, bool leader, int platoon, int level)


        //720
        AddEnemyUnit(1, 17, 43, true, 0, 70, true);
        AddEnemyUnit(2, 5, 7, false, 0, 50);
        AddEnemyUnit(2, 13, 8, false, 0, 50);
        AddEnemyUnit(2, 23, 9, false, 0, 50);
        AddEnemyUnit(3, 15, 10, false, 0, 50);
        AddEnemyUnit(3, 21, 5, false, 0, 60);
        AddEnemyUnit(3, 20, 4, false, 0, 60);
        AddEnemyUnit(3, 3, 6, false, 0, 60);
        AddEnemyUnit(3, 16, 7, false, 0, 60);
        AddEnemyUnit(3, 22, 8, false, 0, 60);


        //540
        AddEnemyUnit(1, 12, 26, true, 1, 60, false);
        AddEnemyUnit(3, 7, 0, false, 1, 40);
        AddEnemyUnit(3, 0, 0, false, 1, 40);
        AddEnemyUnit(3, 4, 0, false, 1, 40);
        AddEnemyUnit(3, 20, 0, false, 1, 40);
        AddEnemyUnit(3, 24, 0, false, 1, 40);
        AddEnemyUnit(3, 22, 1, false, 1, 40);
        AddEnemyUnit(3, 2, 2, false, 1, 40);
        AddEnemyUnit(3, 17, 3, false, 1, 40);
        AddEnemyUnit(1, 18, 15, false, 1, 40);
        AddEnemyUnit(1, 8, 11, false, 1, 40);
        AddEnemyUnit(1, 16, 19, false, 1, 40);
        AddEnemyUnit(1, 6, 24, false, 1, 40);


        //540
        AddEnemyUnit(1, 12, 37, true, 2, 60, false);
        AddEnemyUnit(3, 7, 1, false, 2, 40);
        AddEnemyUnit(3, 0, 1, false, 2, 40);
        AddEnemyUnit(3, 4, 1, false, 2, 40);
        AddEnemyUnit(3, 20, 1, false, 2, 40);
        AddEnemyUnit(3, 24, 1, false, 2, 40);
        AddEnemyUnit(3, 22, 0, false, 2, 40);
        AddEnemyUnit(3, 2, 2, false, 2, 40);
        AddEnemyUnit(3, 17, 3, false, 2, 40);
        AddEnemyUnit(1, 18, 16, false, 2, 40);
        AddEnemyUnit(1, 8, 12, false, 2, 40);
        AddEnemyUnit(1, 16, 20, false, 2, 40);
        AddEnemyUnit(1, 6, 25, false, 2, 40);

        //540
        AddEnemyUnit(1, 12, 38, true, 3, 60, false);
        AddEnemyUnit(3, 7, 2, false, 3, 40);
        AddEnemyUnit(3, 0, 2, false, 3, 40);
        AddEnemyUnit(3, 4, 2, false, 3, 40);
        AddEnemyUnit(3, 20, 2, false, 3, 40);
        AddEnemyUnit(3, 24, 2, false, 3, 40);
        AddEnemyUnit(3, 22, 0, false, 3, 40);
        AddEnemyUnit(3, 2, 1, false, 3, 40);
        AddEnemyUnit(3, 17, 3, false, 3, 40);
        AddEnemyUnit(1, 18, 17, false, 3, 40);
        AddEnemyUnit(1, 8, 13, false, 3, 40);
        AddEnemyUnit(1, 16, 21, false, 3, 40);
        AddEnemyUnit(1, 6, 24, false, 3, 40);

        //540
        AddEnemyUnit(1, 12, 34, true, 4, 60, false);
        AddEnemyUnit(3, 7, 3, false, 4, 40);
        AddEnemyUnit(3, 0, 3, false, 4, 40);
        AddEnemyUnit(3, 4, 3, false, 4, 40);
        AddEnemyUnit(3, 20, 3, false, 4, 40);
        AddEnemyUnit(3, 24, 3, false, 4, 40);
        AddEnemyUnit(3, 22, 0, false, 4, 40);
        AddEnemyUnit(3, 2, 1, false, 4, 40);
        AddEnemyUnit(3, 17, 2, false, 4, 40);
        AddEnemyUnit(1, 18, 18, false, 4, 40);
        AddEnemyUnit(1, 8, 14, false, 4, 40);
        AddEnemyUnit(1, 16, 22, false, 4, 40);
        AddEnemyUnit(1, 6, 25, false, 4, 40);

        
        //590
        AddEnemyUnit(1, 12, 27, true, 5, 60, true);
        AddEnemyUnit(3, 7, 4, false, 5, 50);
        AddEnemyUnit(3, 0, 4, false, 5, 50);
        AddEnemyUnit(3, 4, 4, false, 5, 50);
        AddEnemyUnit(3, 20, 4, false, 5, 50);
        AddEnemyUnit(3, 24, 4, false, 5, 50);
        AddEnemyUnit(3, 22, 5, false, 5, 40);
        AddEnemyUnit(3, 2, 6, false, 5, 40);
        AddEnemyUnit(3, 17, 7, false, 5, 40);
        AddEnemyUnit(3, 18, 8, false, 5, 40);
        AddEnemyUnit(1, 6, 11, false, 5, 40);
        AddEnemyUnit(1, 16, 17, false, 5, 40);
        AddEnemyUnit(1, 8, 22, false, 5, 40);
        

        //590
        AddEnemyUnit(1, 12, 39, true, 6, 60, true);
        AddEnemyUnit(3, 7, 5, false, 6, 50);
        AddEnemyUnit(3, 0, 5, false, 6, 50);
        AddEnemyUnit(3, 4, 5, false, 6, 50);
        AddEnemyUnit(3, 20, 5, false, 6, 50);
        AddEnemyUnit(3, 24, 5, false, 6, 50);
        AddEnemyUnit(3, 22, 4, false, 6, 40);
        AddEnemyUnit(3, 2, 6, false, 6, 40);
        AddEnemyUnit(3, 17, 7, false, 6, 40);
        AddEnemyUnit(3, 18, 8, false, 6, 40);
        AddEnemyUnit(1, 6, 11, false, 6, 40);
        AddEnemyUnit(1, 16, 15, false, 6, 40);
        AddEnemyUnit(1, 8, 23, false, 6, 40);

        //590
        AddEnemyUnit(1, 12, 36, true, 7, 60, true);
        AddEnemyUnit(3, 7, 6, false, 7, 50);
        AddEnemyUnit(3, 0, 6, false, 7, 50);
        AddEnemyUnit(3, 4, 6, false, 7, 50);
        AddEnemyUnit(3, 20, 6, false, 7, 50);
        AddEnemyUnit(3, 24, 6, false, 7, 50);
        AddEnemyUnit(3, 22, 4, false, 7, 40);
        AddEnemyUnit(3, 2, 5, false, 7, 40);
        AddEnemyUnit(3, 17, 7, false, 7, 40);
        AddEnemyUnit(3, 18, 8, false, 7, 40);
        AddEnemyUnit(1, 6, 12, false, 7, 40);
        AddEnemyUnit(1, 16, 16, false, 7, 40);
        AddEnemyUnit(1, 8, 19, false, 7, 40);

        //590
        AddEnemyUnit(1, 12, 33, true, 8, 60, true);
        AddEnemyUnit(3, 7, 7, false, 8, 50);
        AddEnemyUnit(3, 0, 7, false, 8, 50);
        AddEnemyUnit(3, 4, 7, false, 8, 50);
        AddEnemyUnit(3, 20, 7, false, 8, 50);
        AddEnemyUnit(3, 24, 7, false, 8, 50);
        AddEnemyUnit(3, 22, 4, false, 8, 40);
        AddEnemyUnit(3, 2, 5, false, 8, 40);
        AddEnemyUnit(3, 17, 6, false, 8, 40);
        AddEnemyUnit(3, 18, 8, false, 8, 40);
        AddEnemyUnit(1, 6, 13, false, 8, 40);
        AddEnemyUnit(1, 16, 17, false, 8, 40);
        AddEnemyUnit(1, 8, 20, false, 8, 40);

        //590
        AddEnemyUnit(1, 12, 32, true, 9, 60, true);
        AddEnemyUnit(3, 7, 8, false, 9, 50);
        AddEnemyUnit(3, 0, 8, false, 9, 50);
        AddEnemyUnit(3, 4, 8, false, 9, 50);
        AddEnemyUnit(3, 20, 8, false, 9, 50);
        AddEnemyUnit(3, 24, 8, false, 9, 50);
        AddEnemyUnit(3, 22, 4, false, 9, 40);
        AddEnemyUnit(3, 2, 5, false, 9, 40);
        AddEnemyUnit(3, 17, 6, false, 9, 40);
        AddEnemyUnit(3, 18, 7, false, 9, 40);
        AddEnemyUnit(1, 6, 14, false, 9, 40);
        AddEnemyUnit(1, 16, 18, false, 9, 40);
        AddEnemyUnit(1, 8, 21, false, 9, 40);


        //635
        AddEnemyUnit(1, 2, 42, true, 10, 65, false);
        AddEnemyUnit(3, 5, 9, false, 10, 45);
        AddEnemyUnit(1, 8, 24, false, 10, 48);
        AddEnemyUnit(1, 15, 25, false, 10, 48);
        AddEnemyUnit(1, 19, 19, false, 10, 48);
        AddEnemyUnit(1, 12, 17, false, 10, 48);
        AddEnemyUnit(1, 4, 14, false, 10, 48);
        AddEnemyUnit(1, 10, 20, false, 10, 48);
        AddEnemyUnit(1, 22, 23, false, 10, 48);
        AddEnemyUnit(1, 7, 18, false, 10, 48);
        AddEnemyUnit(1, 24, 22, false, 10, 48);
        AddEnemyUnit(1, 20, 21, false, 10, 48);

        //635
        AddEnemyUnit(1, 2, 42, true, 11, 65, false);
        AddEnemyUnit(3, 5, 11, false, 11, 45);
        AddEnemyUnit(1, 8, 24, false, 11, 48);
        AddEnemyUnit(1, 15, 25, false, 11, 48);
        AddEnemyUnit(1, 19, 19, false, 11, 48);
        AddEnemyUnit(1, 12, 17, false, 11, 48);
        AddEnemyUnit(1, 4, 14, false, 11, 48);
        AddEnemyUnit(1, 10, 20, false, 11, 48);
        AddEnemyUnit(1, 22, 23, false, 11, 48);
        AddEnemyUnit(1, 7, 18, false, 11, 48);
        AddEnemyUnit(1, 24, 22, false, 11, 48);
        AddEnemyUnit(1, 20, 21, false, 11, 48);

        //635
        AddEnemyUnit(1, 2, 42, true, 12, 65, false);
        AddEnemyUnit(3, 5, 12, false, 12, 45);
        AddEnemyUnit(1, 8, 24, false, 12, 48);
        AddEnemyUnit(1, 15, 25, false, 12, 48);
        AddEnemyUnit(1, 19, 19, false, 12, 48);
        AddEnemyUnit(1, 12, 17, false, 12, 48);
        AddEnemyUnit(1, 4, 14, false, 12, 48);
        AddEnemyUnit(1, 10, 20, false, 12, 48);
        AddEnemyUnit(1, 22, 23, false, 12, 48);
        AddEnemyUnit(1, 7, 18, false, 12, 48);
        AddEnemyUnit(1, 24, 22, false, 12, 48);
        AddEnemyUnit(1, 20, 21, false, 12, 48);



        for (int i = 0; i < 11; i++)
        {
            GameController.Instance.SetPlayerPlatoon(i);
        }
        maxDeployedPlatoon = 10;
    }

    private void Level16Setup()
    {
        Debug.Log("Level 16 Setup");

        Platoon p0 = new Platoon(0);
        enemyPlatoons[0] = p0;

        Platoon p1 = new Platoon(1);
        enemyPlatoons[1] = p1;

        Platoon p2 = new Platoon(2);
        enemyPlatoons[2] = p2;

        Platoon p3 = new Platoon(3);
        enemyPlatoons[3] = p3;

        Platoon p4 = new Platoon(4);
        enemyPlatoons[4] = p4;

        Platoon p5 = new Platoon(5);
        enemyPlatoons[5] = p5;

        Platoon p6 = new Platoon(6);
        enemyPlatoons[6] = p6;

        Platoon p7 = new Platoon(7);
        enemyPlatoons[7] = p7;

        Platoon p8 = new Platoon(8);
        enemyPlatoons[8] = p8;

        Platoon p9 = new Platoon(9);
        enemyPlatoons[9] = p9;

        Platoon p10 = new Platoon(10);
        enemyPlatoons[10] = p10;

        Platoon p11 = new Platoon(11);
        enemyPlatoons[11] = p11;

        Platoon p12 = new Platoon(12);
        enemyPlatoons[12] = p12;

        Platoon p13 = new Platoon(13);
        enemyPlatoons[13] = p13;

        Platoon p14 = new Platoon(14);
        enemyPlatoons[14] = p14;

        Platoon p15 = new Platoon(15);
        enemyPlatoons[15] = p15;

        enemyPlatoonsRemaining = 16;

        //AddEnemyUnit(int charType, int position, int charClass, bool leader, int platoon, int level)


        //775
        AddEnemyUnit(1, 17, 43, true, 0, 75, true);
        AddEnemyUnit(2, 24, 3, false, 0, 60);
        AddEnemyUnit(2, 23, 6, false, 0, 60);
        AddEnemyUnit(2, 5, 4, false, 0, 50);
        AddEnemyUnit(2, 15, 5, false, 0, 50);
        AddEnemyUnit(3, 21, 0, false, 0, 60);
        AddEnemyUnit(3, 20, 1, false, 0, 60);
        AddEnemyUnit(3, 4, 2, false, 0, 60);
        AddEnemyUnit(3, 14, 4, false, 0, 60);
        AddEnemyUnit(3, 8, 5, false, 0, 60);
        AddEnemyUnit(3, 18, 6, false, 0, 60);


        //564
        AddEnemyUnit(1, 12, 26, true, 1, 60, true);
        AddEnemyUnit(2, 0, 3, false, 1, 42);
        AddEnemyUnit(2, 2, 3, false, 1, 42);
        AddEnemyUnit(2, 4, 3, false, 1, 42);
        AddEnemyUnit(2, 6, 3, false, 1, 42);
        AddEnemyUnit(2, 8, 3, false, 1, 42);
        AddEnemyUnit(2, 10, 3, false, 1, 42);
        AddEnemyUnit(2, 14, 6, false, 1, 42);
        AddEnemyUnit(2, 16, 6, false, 1, 42);
        AddEnemyUnit(2, 18, 6, false, 1, 42);
        AddEnemyUnit(2, 20, 6, false, 1, 42);
        AddEnemyUnit(2, 22, 6, false, 1, 42);
        AddEnemyUnit(2, 24, 6, false, 1, 42);

        //564
        AddEnemyUnit(1, 12, 27, true, 2, 60, true);
        AddEnemyUnit(2, 5, 4, false, 2, 42);
        AddEnemyUnit(2, 8, 4, false, 2, 42);
        AddEnemyUnit(2, 20, 4, false, 2, 42);
        AddEnemyUnit(2, 23, 4, false, 2, 42);
        AddEnemyUnit(2, 10, 3, false, 2, 42);
        AddEnemyUnit(2, 11, 6, false, 2, 42);
        AddEnemyUnit(2, 13, 6, false, 2, 42);
        AddEnemyUnit(2, 14, 3, false, 2, 42);

        //564
        AddEnemyUnit(1, 12, 32, true, 3, 60, true);
        AddEnemyUnit(2, 5, 5, false, 3, 42);
        AddEnemyUnit(2, 8, 5, false, 3, 42);
        AddEnemyUnit(2, 20, 5, false, 3, 42);
        AddEnemyUnit(2, 23, 5, false, 3, 42);
        AddEnemyUnit(2, 10, 3, false, 3, 42);
        AddEnemyUnit(2, 11, 6, false, 3, 42);
        AddEnemyUnit(2, 13, 6, false, 3, 42);
        AddEnemyUnit(2, 14, 3, false, 3, 42);

        //564
        AddEnemyUnit(1, 12, 33, true, 4, 60, true);
        AddEnemyUnit(2, 5, 7, false, 4, 42);
        AddEnemyUnit(2, 8, 7, false, 4, 42);
        AddEnemyUnit(2, 20, 7, false, 4, 42);
        AddEnemyUnit(2, 23, 7, false, 4, 42);
        AddEnemyUnit(2, 10, 3, false, 4, 42);
        AddEnemyUnit(2, 11, 6, false, 4, 42);
        AddEnemyUnit(2, 13, 6, false, 4, 42);
        AddEnemyUnit(2, 14, 3, false, 4, 42);

        //564
        AddEnemyUnit(1, 12, 34, true, 5, 60, true);
        AddEnemyUnit(2, 5, 8, false, 5, 42);
        AddEnemyUnit(2, 8, 8, false, 5, 42);
        AddEnemyUnit(2, 20, 8, false, 5, 42);
        AddEnemyUnit(2, 23, 8, false, 5, 42);
        AddEnemyUnit(2, 10, 3, false, 5, 42);
        AddEnemyUnit(2, 11, 6, false, 5, 42);
        AddEnemyUnit(2, 13, 6, false, 5, 42);
        AddEnemyUnit(2, 14, 3, false, 5, 42);

        //564
        AddEnemyUnit(1, 12, 35, true, 6, 60, true);
        AddEnemyUnit(2, 5, 9, false, 6, 42);
        AddEnemyUnit(2, 8, 9, false, 6, 42);
        AddEnemyUnit(2, 20, 9, false, 6, 42);
        AddEnemyUnit(2, 23, 9, false, 6, 42);
        AddEnemyUnit(2, 10, 3, false, 6, 42);
        AddEnemyUnit(2, 11, 6, false, 6, 42);
        AddEnemyUnit(2, 13, 6, false, 6, 42);
        AddEnemyUnit(2, 14, 3, false, 6, 42);

        //605
        AddEnemyUnit(1, 12, 36, true, 7, 65, true);
        AddEnemyUnit(3, 0, 0, false, 7, 45);
        AddEnemyUnit(3, 2, 0, false, 7, 45);
        AddEnemyUnit(3, 4, 0, false, 7, 45);
        AddEnemyUnit(3, 6, 0, false, 7, 45);
        AddEnemyUnit(3, 8, 0, false, 7, 45);
        AddEnemyUnit(3, 10, 0, false, 7, 45);
        AddEnemyUnit(3, 14, 0, false, 7, 45);
        AddEnemyUnit(3, 16, 0, false, 7, 45);
        AddEnemyUnit(3, 18, 0, false, 7, 45);
        AddEnemyUnit(3, 20, 0, false, 7, 45);
        AddEnemyUnit(3, 22, 0, false, 7, 45);
        AddEnemyUnit(3, 24, 0, false, 7, 45);

        //605
        AddEnemyUnit(1, 12, 37, true, 8, 65, true);
        AddEnemyUnit(3, 0, 1, false, 8, 45);
        AddEnemyUnit(3, 2, 1, false, 8, 45);
        AddEnemyUnit(3, 4, 1, false, 8, 45);
        AddEnemyUnit(3, 6, 1, false, 8, 45);
        AddEnemyUnit(3, 8, 1, false, 8, 45);
        AddEnemyUnit(3, 10, 1, false, 8, 45);
        AddEnemyUnit(3, 14, 1, false, 8, 45);
        AddEnemyUnit(3, 16, 1, false, 8, 45);
        AddEnemyUnit(3, 18, 1, false, 8, 45);
        AddEnemyUnit(3, 20, 1, false, 8, 45);
        AddEnemyUnit(3, 22, 1, false, 8, 45);
        AddEnemyUnit(3, 24, 1, false, 8, 45);

        //605
        AddEnemyUnit(1, 12, 38, true, 9, 65, true);
        AddEnemyUnit(3, 0, 2, false, 9, 45);
        AddEnemyUnit(3, 2, 2, false, 9, 45);
        AddEnemyUnit(3, 4, 2, false, 9, 45);
        AddEnemyUnit(3, 6, 2, false, 9, 45);
        AddEnemyUnit(3, 8, 2, false, 9, 45);
        AddEnemyUnit(3, 10, 2, false, 9, 45);
        AddEnemyUnit(3, 14, 2, false, 9, 45);
        AddEnemyUnit(3, 16, 2, false, 9, 45);
        AddEnemyUnit(3, 18, 2, false, 9, 45);
        AddEnemyUnit(3, 20, 2, false, 9, 45);
        AddEnemyUnit(3, 22, 2, false, 9, 45);
        AddEnemyUnit(3, 24, 2, false, 9, 45);

        //605
        AddEnemyUnit(1, 12, 39, true, 10, 65, true);
        AddEnemyUnit(3, 0, 3, false, 10, 45);
        AddEnemyUnit(3, 2, 3, false, 10, 45);
        AddEnemyUnit(3, 4, 3, false, 10, 45);
        AddEnemyUnit(3, 6, 3, false, 10, 45);
        AddEnemyUnit(3, 8, 3, false, 10, 45);
        AddEnemyUnit(3, 10, 3, false, 10, 45);
        AddEnemyUnit(3, 14, 3, false, 10, 45);
        AddEnemyUnit(3, 16, 3, false, 10, 45);
        AddEnemyUnit(3, 18, 3, false, 10, 45);
        AddEnemyUnit(3, 20, 3, false, 10, 45);
        AddEnemyUnit(3, 22, 3, false, 10, 45);
        AddEnemyUnit(3, 24, 3, false, 10, 45);

        //629
        AddEnemyUnit(1, 12, 40, true, 11, 65, true);
        AddEnemyUnit(3, 0, 4, false, 11, 47);
        AddEnemyUnit(3, 2, 4, false, 11, 47);
        AddEnemyUnit(3, 4, 4, false, 11, 47);
        AddEnemyUnit(3, 6, 4, false, 11, 47);
        AddEnemyUnit(3, 8, 4, false, 11, 47);
        AddEnemyUnit(3, 10, 4, false, 11, 47);
        AddEnemyUnit(3, 14, 4, false, 11, 47);
        AddEnemyUnit(3, 16, 4, false, 11, 47);
        AddEnemyUnit(3, 18, 4, false, 11, 47);
        AddEnemyUnit(3, 20, 4, false, 11, 47);
        AddEnemyUnit(3, 22, 4, false, 11, 47);
        AddEnemyUnit(3, 24, 4, false, 11, 47);

        //629
        AddEnemyUnit(1, 12, 41, true, 12, 65, true);
        AddEnemyUnit(3, 0, 5, false, 12, 47);
        AddEnemyUnit(3, 2, 5, false, 12, 47);
        AddEnemyUnit(3, 4, 5, false, 12, 47);
        AddEnemyUnit(3, 6, 5, false, 12, 47);
        AddEnemyUnit(3, 8, 5, false, 12, 47);
        AddEnemyUnit(3, 10, 5, false, 12, 47);
        AddEnemyUnit(3, 14, 5, false, 12, 47);
        AddEnemyUnit(3, 16, 5, false, 12, 47);
        AddEnemyUnit(3, 18, 5, false, 12, 47);
        AddEnemyUnit(3, 20, 5, false, 12, 47);
        AddEnemyUnit(3, 22, 5, false, 12, 47);
        AddEnemyUnit(3, 24, 5, false, 12, 47);

        //629
        AddEnemyUnit(1, 12, 42, true, 13, 65, true);
        AddEnemyUnit(3, 0, 6, false, 13, 47);
        AddEnemyUnit(3, 2, 6, false, 13, 47);
        AddEnemyUnit(3, 4, 6, false, 13, 47);
        AddEnemyUnit(3, 6, 6, false, 13, 47);
        AddEnemyUnit(3, 8, 6, false, 13, 47);
        AddEnemyUnit(3, 10, 6, false, 13, 47);
        AddEnemyUnit(3, 14, 6, false, 13, 47);
        AddEnemyUnit(3, 16, 6, false, 13, 47);
        AddEnemyUnit(3, 18, 6, false, 13, 47);
        AddEnemyUnit(3, 20, 6, false, 13, 47);
        AddEnemyUnit(3, 22, 6, false, 13, 47);
        AddEnemyUnit(3, 24, 6, false, 13, 47);

        //629
        AddEnemyUnit(1, 12, 29, true, 14, 65, true);
        AddEnemyUnit(3, 0, 7, false, 14, 47);
        AddEnemyUnit(3, 2, 7, false, 14, 47);
        AddEnemyUnit(3, 4, 7, false, 14, 47);
        AddEnemyUnit(3, 6, 7, false, 14, 47);
        AddEnemyUnit(3, 8, 7, false, 14, 47);
        AddEnemyUnit(3, 10, 7, false, 14, 47);
        AddEnemyUnit(3, 14, 7, false, 14, 47);
        AddEnemyUnit(3, 16, 7, false, 14, 47);
        AddEnemyUnit(3, 18, 7, false, 14, 47);
        AddEnemyUnit(3, 20, 7, false, 14, 47);
        AddEnemyUnit(3, 22, 7, false, 14, 47);
        AddEnemyUnit(3, 24, 7, false, 14, 47);

        //629
        AddEnemyUnit(1, 12, 25, true, 15, 65, true);
        AddEnemyUnit(3, 0, 8, false, 15, 47);
        AddEnemyUnit(3, 2, 8, false, 15, 47);
        AddEnemyUnit(3, 4, 8, false, 15, 47);
        AddEnemyUnit(3, 6, 8, false, 15, 47);
        AddEnemyUnit(3, 8, 8, false, 15, 47);
        AddEnemyUnit(3, 10, 8, false, 15, 47);
        AddEnemyUnit(3, 14, 8, false, 15, 47);
        AddEnemyUnit(3, 16, 8, false, 15, 47);
        AddEnemyUnit(3, 18, 8, false, 15, 47);
        AddEnemyUnit(3, 20, 8, false, 15, 47);
        AddEnemyUnit(3, 22, 8, false, 15, 47);
        AddEnemyUnit(3, 24, 8, false, 15, 47);


        for (int i = 0; i < 11; i++)
        {
            GameController.Instance.SetPlayerPlatoon(i);
        }
        maxDeployedPlatoon = 10;
    }

    private void Level17Setup()
    {
        Debug.Log("Level 17 Setup");

        Platoon p0 = new Platoon(0);
        enemyPlatoons[0] = p0;

        Platoon p1 = new Platoon(1);
        enemyPlatoons[1] = p1;

        Platoon p2 = new Platoon(2);
        enemyPlatoons[2] = p2;

        Platoon p3 = new Platoon(3);
        enemyPlatoons[3] = p3;

        Platoon p4 = new Platoon(4);
        enemyPlatoons[4] = p4;

        Platoon p5 = new Platoon(5);
        enemyPlatoons[5] = p5;

        Platoon p6 = new Platoon(6);
        enemyPlatoons[6] = p6;

        Platoon p7 = new Platoon(7);
        enemyPlatoons[7] = p7;

        Platoon p8 = new Platoon(8);
        enemyPlatoons[8] = p8;

        Platoon p9 = new Platoon(9);
        enemyPlatoons[9] = p9;

        Platoon p10 = new Platoon(10);
        enemyPlatoons[10] = p10;

        Platoon p11 = new Platoon(11);
        enemyPlatoons[11] = p11;

        Platoon p12 = new Platoon(12);
        enemyPlatoons[12] = p12;

        Platoon p13 = new Platoon(13);
        enemyPlatoons[13] = p13;

        Platoon p14 = new Platoon(14);
        enemyPlatoons[14] = p14;

        enemyPlatoonsRemaining = 15;

        //AddEnemyUnit(int charType, int position, int charClass, bool leader, int platoon, int level)


        //775
        AddEnemyUnit(1, 17, 43, true, 0, 80, false);
        AddEnemyUnit(2, 24, 3, false, 0, 60);
        AddEnemyUnit(2, 23, 6, false, 0, 60);
        AddEnemyUnit(2, 5, 4, false, 0, 60);
        AddEnemyUnit(2, 15, 5, false, 0, 60);
        AddEnemyUnit(3, 21, 0, false, 0, 60);
        AddEnemyUnit(3, 20, 1, false, 0, 60);
        AddEnemyUnit(3, 4, 2, false, 0, 60);
        AddEnemyUnit(3, 14, 4, false, 0, 60);
        AddEnemyUnit(3, 8, 5, false, 0, 60);
        AddEnemyUnit(3, 18, 6, false, 0, 60);


        //564
        AddEnemyUnit(1, 12, 26, true, 1, 60, false);
        AddEnemyUnit(2, 0, 3, false, 1, 45);
        AddEnemyUnit(2, 2, 3, false, 1, 45);
        AddEnemyUnit(2, 4, 3, false, 1, 45);
        AddEnemyUnit(2, 6, 3, false, 1, 45);
        AddEnemyUnit(2, 8, 3, false, 1, 45);
        AddEnemyUnit(2, 10, 3, false, 1, 45);
        AddEnemyUnit(2, 14, 6, false, 1, 45);
        AddEnemyUnit(2, 16, 6, false, 1, 45);
        AddEnemyUnit(2, 18, 6, false, 1, 45);
        AddEnemyUnit(2, 20, 6, false, 1, 45);
        AddEnemyUnit(2, 22, 6, false, 1, 45);
        AddEnemyUnit(2, 24, 6, false, 1, 45);

        //564
        AddEnemyUnit(1, 12, 27, true, 2, 60, false);
        AddEnemyUnit(2, 5, 4, false, 2, 45);
        AddEnemyUnit(2, 8, 4, false, 2, 45);
        AddEnemyUnit(2, 20, 5, false, 2, 45);
        AddEnemyUnit(2, 23, 5, false, 2, 45);
        AddEnemyUnit(2, 10, 3, false, 2, 45);
        AddEnemyUnit(2, 11, 6, false, 2, 45);
        AddEnemyUnit(2, 13, 6, false, 2, 45);
        AddEnemyUnit(2, 14, 3, false, 2, 45);

        //564
        AddEnemyUnit(1, 12, 33, true, 3, 60, false);
        AddEnemyUnit(2, 5, 7, false, 3, 45);
        AddEnemyUnit(2, 8, 7, false, 3, 45);
        AddEnemyUnit(2, 20, 7, false, 3, 45);
        AddEnemyUnit(2, 23, 7, false, 3, 45);
        AddEnemyUnit(2, 10, 3, false, 3, 45);
        AddEnemyUnit(2, 11, 6, false, 3, 45);
        AddEnemyUnit(2, 13, 6, false, 3, 45);
        AddEnemyUnit(2, 14, 3, false, 3, 45);

        //564
        AddEnemyUnit(1, 12, 34, true, 4, 60, false);
        AddEnemyUnit(2, 5, 8, false, 4, 45);
        AddEnemyUnit(2, 8, 8, false, 4, 45);
        AddEnemyUnit(2, 20, 8, false, 4, 45);
        AddEnemyUnit(2, 23, 8, false, 4, 45);
        AddEnemyUnit(2, 10, 3, false, 4, 45);
        AddEnemyUnit(2, 11, 6, false, 4, 45);
        AddEnemyUnit(2, 13, 6, false, 4, 45);
        AddEnemyUnit(2, 14, 3, false, 4, 45);

        //564
        AddEnemyUnit(1, 12, 35, true, 5, 60, false);
        AddEnemyUnit(2, 5, 9, false, 5, 45);
        AddEnemyUnit(2, 8, 9, false, 5, 45);
        AddEnemyUnit(2, 20, 9, false, 5, 45);
        AddEnemyUnit(2, 23, 9, false, 5, 45);
        AddEnemyUnit(2, 10, 3, false, 5, 45);
        AddEnemyUnit(2, 11, 6, false, 5, 45);
        AddEnemyUnit(2, 13, 6, false, 5, 45);
        AddEnemyUnit(2, 14, 3, false, 5, 45);

        //605
        AddEnemyUnit(1, 12, 36, true, 6, 65, false);
        AddEnemyUnit(3, 0, 0, false, 6, 48);
        AddEnemyUnit(3, 2, 0, false, 6, 48);
        AddEnemyUnit(3, 4, 0, false, 6, 48);
        AddEnemyUnit(3, 6, 0, false, 6, 48);
        AddEnemyUnit(3, 8, 0, false, 6, 48);
        AddEnemyUnit(3, 10, 0, false, 6, 48);
        AddEnemyUnit(3, 14, 0, false, 6, 48);
        AddEnemyUnit(3, 16, 0, false, 6, 48);
        AddEnemyUnit(3, 18, 0, false, 6, 48);
        AddEnemyUnit(3, 20, 0, false, 6, 48);
        AddEnemyUnit(3, 22, 0, false, 6, 48);
        AddEnemyUnit(3, 24, 0, false, 6, 48);

        //605
        AddEnemyUnit(1, 12, 37, true, 7, 65, false);
        AddEnemyUnit(3, 0, 1, false, 7, 48);
        AddEnemyUnit(3, 2, 1, false, 7, 48);
        AddEnemyUnit(3, 4, 1, false, 7, 48);
        AddEnemyUnit(3, 6, 1, false, 7, 48);
        AddEnemyUnit(3, 8, 1, false, 7, 48);
        AddEnemyUnit(3, 10, 1, false, 7, 48);
        AddEnemyUnit(3, 14, 1, false, 7, 48);
        AddEnemyUnit(3, 16, 1, false, 7, 48);
        AddEnemyUnit(3, 18, 1, false, 7, 48);
        AddEnemyUnit(3, 20, 1, false, 7, 48);
        AddEnemyUnit(3, 22, 1, false, 7, 48);
        AddEnemyUnit(3, 24, 1, false, 7, 48);

        //605
        AddEnemyUnit(1, 12, 38, true, 8, 65, false);
        AddEnemyUnit(3, 0, 2, false, 8, 48);
        AddEnemyUnit(3, 2, 2, false, 8, 48);
        AddEnemyUnit(3, 4, 2, false, 8, 48);
        AddEnemyUnit(3, 6, 2, false, 8, 48);
        AddEnemyUnit(3, 8, 2, false, 8, 48);
        AddEnemyUnit(3, 10, 2, false, 8, 48);
        AddEnemyUnit(3, 14, 2, false, 8, 48);
        AddEnemyUnit(3, 16, 2, false, 8, 48);
        AddEnemyUnit(3, 18, 2, false, 8, 48);
        AddEnemyUnit(3, 20, 2, false, 8, 48);
        AddEnemyUnit(3, 22, 2, false, 8, 48);
        AddEnemyUnit(3, 24, 2, false, 8, 48);

        //605
        AddEnemyUnit(1, 12, 39, true, 9, 65, false);
        AddEnemyUnit(3, 0, 3, false, 9, 48);
        AddEnemyUnit(3, 2, 3, false, 9, 48);
        AddEnemyUnit(3, 4, 3, false, 9, 48);
        AddEnemyUnit(3, 6, 3, false, 9, 48);
        AddEnemyUnit(3, 8, 3, false, 9, 48);
        AddEnemyUnit(3, 10, 3, false, 9, 48);
        AddEnemyUnit(3, 14, 3, false, 9, 48);
        AddEnemyUnit(3, 16, 3, false, 9, 48);
        AddEnemyUnit(3, 18, 3, false, 9, 48);
        AddEnemyUnit(3, 20, 3, false, 9, 48);
        AddEnemyUnit(3, 22, 3, false, 9, 48);
        AddEnemyUnit(3, 24, 3, false, 9, 48);

        //629
        AddEnemyUnit(1, 12, 40, true, 10, 65, false);
        AddEnemyUnit(3, 0, 4, false, 10, 50);
        AddEnemyUnit(3, 2, 4, false, 10, 50);
        AddEnemyUnit(3, 4, 4, false, 10, 50);
        AddEnemyUnit(3, 6, 4, false, 10, 50);
        AddEnemyUnit(3, 8, 4, false, 10, 50);
        AddEnemyUnit(3, 10, 4, false, 10, 50);
        AddEnemyUnit(3, 14, 4, false, 10, 50);
        AddEnemyUnit(3, 16, 4, false, 10, 50);
        AddEnemyUnit(3, 18, 4, false, 10, 50);
        AddEnemyUnit(3, 20, 4, false, 10, 50);
        AddEnemyUnit(3, 22, 4, false, 10, 50);
        AddEnemyUnit(3, 24, 4, false, 10, 50);
    
        //629
        AddEnemyUnit(1, 12, 41, true, 11, 65, false);
        AddEnemyUnit(3, 0, 5, false, 11, 50);
        AddEnemyUnit(3, 2, 5, false, 11, 50);
        AddEnemyUnit(3, 4, 5, false, 11, 50);
        AddEnemyUnit(3, 6, 5, false, 11, 50);
        AddEnemyUnit(3, 8, 5, false, 11, 50);
        AddEnemyUnit(3, 10, 5, false, 11, 50);
        AddEnemyUnit(3, 14, 5, false, 11, 50);
        AddEnemyUnit(3, 16, 5, false, 11, 50);
        AddEnemyUnit(3, 18, 5, false, 11, 50);
        AddEnemyUnit(3, 20, 5, false, 11, 50);
        AddEnemyUnit(3, 22, 5, false, 11, 50);
        AddEnemyUnit(3, 24, 5, false, 11, 50);

        //629
        AddEnemyUnit(1, 12, 42, true, 12, 65, false);
        AddEnemyUnit(3, 0, 6, false, 12, 50);
        AddEnemyUnit(3, 2, 6, false, 12, 50);
        AddEnemyUnit(3, 4, 6, false, 12, 50);
        AddEnemyUnit(3, 6, 6, false, 12, 50);
        AddEnemyUnit(3, 8, 6, false, 12, 50);
        AddEnemyUnit(3, 10, 6, false, 12, 50);
        AddEnemyUnit(3, 14, 6, false, 12, 50);
        AddEnemyUnit(3, 16, 6, false, 12, 50);
        AddEnemyUnit(3, 18, 6, false, 12, 50);
        AddEnemyUnit(3, 20, 6, false, 12, 50);
        AddEnemyUnit(3, 22, 6, false, 12, 50);
        AddEnemyUnit(3, 24, 6, false, 12, 50);

        //629
        AddEnemyUnit(1, 12, 31, true, 13, 65, false);
        AddEnemyUnit(3, 0, 7, false, 13, 50);
        AddEnemyUnit(3, 2, 7, false, 13, 50);
        AddEnemyUnit(3, 4, 7, false, 13, 50);
        AddEnemyUnit(3, 6, 7, false, 13, 50);
        AddEnemyUnit(3, 8, 7, false, 13, 50);
        AddEnemyUnit(3, 10, 7, false, 13, 50);
        AddEnemyUnit(3, 14, 7, false, 13, 50);
        AddEnemyUnit(3, 16, 7, false, 13, 50);
        AddEnemyUnit(3, 18, 7, false, 13, 50);
        AddEnemyUnit(3, 20, 7, false, 13, 50);
        AddEnemyUnit(3, 22, 7, false, 13, 50);
        AddEnemyUnit(3, 24, 7, false, 13, 50);

        //629
        AddEnemyUnit(1, 12, 32, true, 14, 65, false);
        AddEnemyUnit(3, 0, 8, false, 14, 50);
        AddEnemyUnit(3, 2, 8, false, 14, 50);
        AddEnemyUnit(3, 4, 8, false, 14, 50);
        AddEnemyUnit(3, 6, 8, false, 14, 50);
        AddEnemyUnit(3, 8, 8, false, 14, 50);
        AddEnemyUnit(3, 10, 8, false, 14, 50);
        AddEnemyUnit(3, 14, 8, false, 14, 50);
        AddEnemyUnit(3, 16, 8, false, 14, 50);
        AddEnemyUnit(3, 18, 8, false, 14, 50);
        AddEnemyUnit(3, 20, 8, false, 14, 50);
        AddEnemyUnit(3, 22, 8, false, 14, 50);
        AddEnemyUnit(3, 24, 8, false, 14, 50);


        for (int i = 0; i < 11; i++)
        {
            GameController.Instance.SetPlayerPlatoon(i);
        }
        maxDeployedPlatoon = 10;
    }

    private void Level18Setup()
    {
        Debug.Log("Level 18 Setup");

        Platoon p0 = new Platoon(0);
        enemyPlatoons[0] = p0;

        Platoon p1 = new Platoon(1);
        enemyPlatoons[1] = p1;

        Platoon p2 = new Platoon(2);
        enemyPlatoons[2] = p2;

        Platoon p3 = new Platoon(3);
        enemyPlatoons[3] = p3;

        Platoon p4 = new Platoon(4);
        enemyPlatoons[4] = p4;

        Platoon p5 = new Platoon(5);
        enemyPlatoons[5] = p5;

        Platoon p6 = new Platoon(6);
        enemyPlatoons[6] = p6;

        Platoon p7 = new Platoon(7);
        enemyPlatoons[7] = p7;

        Platoon p8 = new Platoon(8);
        enemyPlatoons[8] = p8;

        Platoon p9 = new Platoon(9);
        enemyPlatoons[9] = p9;

        Platoon p10 = new Platoon(10);
        enemyPlatoons[10] = p10;

        Platoon p11 = new Platoon(11);
        enemyPlatoons[11] = p11;

        Platoon p12 = new Platoon(12);
        enemyPlatoons[12] = p12;

        Platoon p13 = new Platoon(13);
        enemyPlatoons[13] = p13;

        Platoon p14 = new Platoon(14);
        enemyPlatoons[14] = p14;

        enemyPlatoonsRemaining = 15;

        //AddEnemyUnit(int charType, int position, int charClass, bool leader, int platoon, int level)


        //865
        AddEnemyUnit(1, 17, 44, true, 0, 85, false);
        AddEnemyUnit(1, 24, 26, false, 0, 65);
        AddEnemyUnit(1, 23, 27, false, 0, 65);
        AddEnemyUnit(1, 0, 38, false, 0, 65);
        AddEnemyUnit(1, 15, 29, false, 0, 65);
        AddEnemyUnit(1, 21, 30, false, 0, 65);
        AddEnemyUnit(1, 20, 31, false, 0, 65);
        AddEnemyUnit(1, 4, 40, false, 0, 65);
        AddEnemyUnit(1, 14, 34, false, 0, 65);
        AddEnemyUnit(1, 8, 33, false, 0, 65);
        AddEnemyUnit(1, 18, 28, false, 0, 65);
        AddEnemyUnit(1, 3, 39, false, 0, 65);
        AddEnemyUnit(1, 6, 32, false, 0, 65);


        //670
        AddEnemyUnit(1, 12, 34, true, 1, 70, true);
        AddEnemyUnit(3, 0, 0, false, 1, 50);
        AddEnemyUnit(3, 1, 1, false, 1, 50);
        AddEnemyUnit(3, 3, 2, false, 1, 50);
        AddEnemyUnit(3, 4, 3, false, 1, 50);
        AddEnemyUnit(1, 8, 25, false, 1, 50);
        AddEnemyUnit(1, 10, 24, false, 1, 50);
        AddEnemyUnit(1, 14, 23, false, 1, 50);
        AddEnemyUnit(1, 16, 22, false, 1, 50);
        AddEnemyUnit(1, 18, 21, false, 1, 50);
        AddEnemyUnit(1, 20, 20, false, 1, 50);
        AddEnemyUnit(1, 22, 19, false, 1, 50);
        AddEnemyUnit(1, 24, 18, false, 1, 50);

        //670
        AddEnemyUnit(1, 12, 35, true, 2, 70, true);
        AddEnemyUnit(3, 0, 0, false, 2, 50);
        AddEnemyUnit(3, 1, 1, false, 2, 50);
        AddEnemyUnit(3, 3, 2, false, 2, 50);
        AddEnemyUnit(3, 4, 3, false, 2, 50);
        AddEnemyUnit(1, 8, 17, false, 2, 50);
        AddEnemyUnit(1, 10, 16, false, 2, 50);
        AddEnemyUnit(1, 14, 15, false, 2, 50);
        AddEnemyUnit(1, 16, 14, false, 2, 50);
        AddEnemyUnit(1, 18, 13, false, 2, 50);
        AddEnemyUnit(1, 20, 12, false, 2, 50);
        AddEnemyUnit(1, 22, 11, false, 2, 50);
        AddEnemyUnit(1, 24, 11, false, 2, 50);

        //670
        AddEnemyUnit(1, 12, 36, true, 3, 70, true);
        AddEnemyUnit(3, 0, 0, false, 3, 50);
        AddEnemyUnit(3, 1, 1, false, 3, 50);
        AddEnemyUnit(3, 3, 2, false, 3, 50);
        AddEnemyUnit(3, 4, 3, false, 3, 50);
        AddEnemyUnit(1, 8, 12, false, 3, 50);
        AddEnemyUnit(1, 10, 13, false, 3, 50);
        AddEnemyUnit(1, 14, 14, false, 3, 50);
        AddEnemyUnit(1, 16, 15, false, 3, 50);
        AddEnemyUnit(1, 18, 16, false, 3, 50);
        AddEnemyUnit(1, 20, 17, false, 3, 50);
        AddEnemyUnit(1, 22, 18, false, 3, 50);
        AddEnemyUnit(1, 24, 19, false, 3, 50);

        //670
        AddEnemyUnit(1, 12, 37, true, 4, 70, true);
        AddEnemyUnit(3, 0, 0, false, 4, 50);
        AddEnemyUnit(3, 1, 1, false, 4, 50);
        AddEnemyUnit(3, 3, 2, false, 4, 50);
        AddEnemyUnit(3, 4, 3, false, 4, 50);
        AddEnemyUnit(1, 8, 20, false, 4, 50);
        AddEnemyUnit(1, 10, 21, false, 4, 50);
        AddEnemyUnit(1, 14, 22, false, 4, 50);
        AddEnemyUnit(1, 16, 23, false, 4, 50);
        AddEnemyUnit(1, 18, 24, false, 4, 50);
        AddEnemyUnit(1, 20, 25, false, 4, 50);
        AddEnemyUnit(1, 22, 25, false, 4, 50);
        AddEnemyUnit(1, 24, 24, false, 4, 50);

        //670
        AddEnemyUnit(1, 12, 38, true, 5, 70, true);
        AddEnemyUnit(3, 0, 0, false, 5, 50);
        AddEnemyUnit(3, 1, 1, false, 5, 50);
        AddEnemyUnit(3, 3, 2, false, 5, 50);
        AddEnemyUnit(3, 4, 3, false, 5, 50);
        AddEnemyUnit(1, 8, 23, false, 5, 50);
        AddEnemyUnit(1, 10, 22, false, 5, 50);
        AddEnemyUnit(1, 14, 21, false, 5, 50);
        AddEnemyUnit(1, 16, 20, false, 5, 50);
        AddEnemyUnit(1, 18, 19, false, 5, 50);
        AddEnemyUnit(1, 20, 18, false, 5, 50);
        AddEnemyUnit(1, 22, 17, false, 5, 50);
        AddEnemyUnit(1, 24, 16, false, 5, 50);

        //670
        AddEnemyUnit(1, 12, 39, true, 6, 70, true);
        AddEnemyUnit(3, 0, 0, false, 6, 50);
        AddEnemyUnit(3, 1, 1, false, 6, 50);
        AddEnemyUnit(3, 3, 2, false, 6, 50);
        AddEnemyUnit(3, 4, 3, false, 6, 50);
        AddEnemyUnit(1, 8, 15, false, 6, 50);
        AddEnemyUnit(1, 10, 14, false, 6, 50);
        AddEnemyUnit(1, 14, 13, false, 6, 50);
        AddEnemyUnit(1, 16, 12, false, 6, 50);
        AddEnemyUnit(1, 18, 11, false, 6, 50);
        AddEnemyUnit(1, 20, 11, false, 6, 50);
        AddEnemyUnit(1, 22, 12, false, 6, 50);
        AddEnemyUnit(1, 24, 13, false, 6, 50);

        //670
        AddEnemyUnit(1, 12, 40, true, 7, 70, true);
        AddEnemyUnit(3, 0, 0, false, 7, 50);
        AddEnemyUnit(3, 1, 1, false, 7, 50);
        AddEnemyUnit(3, 3, 2, false, 7, 50);
        AddEnemyUnit(3, 4, 3, false, 7, 50);
        AddEnemyUnit(1, 8, 14, false, 7, 50);
        AddEnemyUnit(1, 10, 15, false, 7, 50);
        AddEnemyUnit(1, 14, 16, false, 7, 50);
        AddEnemyUnit(1, 16, 17, false, 7, 50);
        AddEnemyUnit(1, 18, 18, false, 7, 50);
        AddEnemyUnit(1, 20, 19, false, 7, 50);
        AddEnemyUnit(1, 22, 20, false, 7, 50);
        AddEnemyUnit(1, 24, 21, false, 7, 50);

        //710
        AddEnemyUnit(1, 12, 33, true, 8, 75, true);
        AddEnemyUnit(3, 0, 0, false, 8, 50);
        AddEnemyUnit(3, 1, 1, false, 8, 50);
        AddEnemyUnit(3, 3, 2, false, 8, 50);
        AddEnemyUnit(3, 4, 3, false, 8, 50);
        AddEnemyUnit(3, 8, 4, false, 8, 55);
        AddEnemyUnit(3, 10, 5, false, 8, 55);
        AddEnemyUnit(3, 14, 6, false, 8, 55);
        AddEnemyUnit(3, 16, 7, false, 8, 55);
        AddEnemyUnit(1, 18, 22, false, 8, 55);
        AddEnemyUnit(1, 20, 23, false, 8, 55);
        AddEnemyUnit(1, 22, 24, false, 8, 55);
        AddEnemyUnit(1, 24, 25, false, 8, 55);

        //710
        AddEnemyUnit(1, 12, 32, true, 9, 75, true);
        AddEnemyUnit(3, 0, 0, false, 9, 50);
        AddEnemyUnit(3, 1, 1, false, 9, 50);
        AddEnemyUnit(3, 3, 2, false, 9, 50);
        AddEnemyUnit(3, 4, 3, false, 9, 50);
        AddEnemyUnit(3, 8, 8, false, 9, 55);
        AddEnemyUnit(3, 10, 5, false, 9, 55);
        AddEnemyUnit(3, 14, 6, false, 9, 55);
        AddEnemyUnit(3, 16, 7, false, 9, 55);
        AddEnemyUnit(1, 18, 25, false, 9, 55);
        AddEnemyUnit(1, 20, 24, false, 9, 55);
        AddEnemyUnit(1, 22, 23, false, 9, 55);
        AddEnemyUnit(1, 24, 22, false, 9, 55);

        //710
        AddEnemyUnit(1, 12, 31, true, 10, 75, true);
        AddEnemyUnit(3, 0, 0, false, 10, 50);
        AddEnemyUnit(3, 1, 1, false, 10, 50);
        AddEnemyUnit(3, 3, 2, false, 10, 50);
        AddEnemyUnit(3, 4, 3, false, 10, 50);
        AddEnemyUnit(3, 8, 4, false, 10, 55);
        AddEnemyUnit(3, 10, 8, false, 10, 55);
        AddEnemyUnit(3, 14, 6, false, 10, 55);
        AddEnemyUnit(3, 16, 7, false, 10, 55);
        AddEnemyUnit(1, 18, 21, false, 10, 55);
        AddEnemyUnit(1, 20, 20, false, 10, 55);
        AddEnemyUnit(1, 22, 19, false, 10, 55);
        AddEnemyUnit(1, 24, 18, false, 10, 55);

        //710
        AddEnemyUnit(1, 12, 30, true, 11, 75, true);
        AddEnemyUnit(3, 0, 0, false, 11, 50);
        AddEnemyUnit(3, 1, 1, false, 11, 50);
        AddEnemyUnit(3, 3, 2, false, 11, 50);
        AddEnemyUnit(3, 4, 3, false, 11, 50);
        AddEnemyUnit(3, 8, 4, false, 11, 55);
        AddEnemyUnit(3, 10, 5, false, 11, 55);
        AddEnemyUnit(3, 14, 8, false, 11, 55);
        AddEnemyUnit(3, 16, 7, false, 11, 55);
        AddEnemyUnit(1, 18, 17, false, 11, 55);
        AddEnemyUnit(1, 20, 16, false, 11, 55);
        AddEnemyUnit(1, 22, 15, false, 11, 55);
        AddEnemyUnit(1, 24, 14, false, 11, 55);

        //710
        AddEnemyUnit(1, 12, 27, true, 12, 75, true);
        AddEnemyUnit(3, 0, 0, false, 12, 50);
        AddEnemyUnit(3, 1, 1, false, 12, 50);
        AddEnemyUnit(3, 3, 2, false, 12, 50);
        AddEnemyUnit(3, 4, 3, false, 12, 50);
        AddEnemyUnit(3, 8, 4, false, 12, 55);
        AddEnemyUnit(3, 10, 5, false, 12, 55);
        AddEnemyUnit(3, 14, 6, false, 12, 55);
        AddEnemyUnit(3, 16, 8, false, 12, 55);
        AddEnemyUnit(1, 18, 13, false, 12, 55);
        AddEnemyUnit(1, 20, 12, false, 12, 55);
        AddEnemyUnit(1, 22, 11, false, 12, 55);
        AddEnemyUnit(1, 24, 11, false, 12, 55);

        //710
        AddEnemyUnit(1, 12, 26, true, 13, 75, true);
        AddEnemyUnit(3, 0, 0, false, 13, 50);
        AddEnemyUnit(3, 1, 1, false, 13, 50);
        AddEnemyUnit(3, 3, 2, false, 13, 50);
        AddEnemyUnit(3, 4, 3, false, 13, 50);
        AddEnemyUnit(3, 8, 4, false, 13, 55);
        AddEnemyUnit(3, 10, 5, false, 13, 55);
        AddEnemyUnit(3, 14, 6, false, 13, 55);
        AddEnemyUnit(3, 16, 7, false, 13, 55);
        AddEnemyUnit(1, 18, 12, false, 13, 55);
        AddEnemyUnit(1, 20, 13, false, 13, 55);
        AddEnemyUnit(1, 22, 14, false, 13, 55);
        AddEnemyUnit(1, 24, 15, false, 13, 55);

        //695
        AddEnemyUnit(1, 12, 42, true, 14, 75, false);
        AddEnemyUnit(2, 5, 7, false, 14, 50);
        AddEnemyUnit(2, 20, 8, false, 14, 50);
        AddEnemyUnit(3, 8, 12, false, 14, 55);
        AddEnemyUnit(3, 23, 9, false, 14, 55);
        AddEnemyUnit(3, 14, 10, false, 14, 50);
        AddEnemyUnit(3, 13, 10, false, 14, 50);
        AddEnemyUnit(3, 11, 10, false, 14, 50);
        AddEnemyUnit(3, 10, 10, false, 14, 50);


        for (int i = 0; i < 11; i++)
        {
            GameController.Instance.SetPlayerPlatoon(i);
        }
        maxDeployedPlatoon = 10;
    }

    private void Level19Setup()
    {
        Debug.Log("Level 19 Setup");

        Platoon p0 = new Platoon(0);
        enemyPlatoons[0] = p0;

        Platoon p1 = new Platoon(1);
        enemyPlatoons[1] = p1;

        Platoon p2 = new Platoon(2);
        enemyPlatoons[2] = p2;

        Platoon p3 = new Platoon(3);
        enemyPlatoons[3] = p3;

        Platoon p4 = new Platoon(4);
        enemyPlatoons[4] = p4;

        Platoon p5 = new Platoon(5);
        enemyPlatoons[5] = p5;

        Platoon p6 = new Platoon(6);
        enemyPlatoons[6] = p6;

        Platoon p7 = new Platoon(7);
        enemyPlatoons[7] = p7;

        Platoon p8 = new Platoon(8);
        enemyPlatoons[8] = p8;

        Platoon p9 = new Platoon(9);
        enemyPlatoons[9] = p9;

        Platoon p10 = new Platoon(10);
        enemyPlatoons[10] = p10;

        Platoon p11 = new Platoon(11);
        enemyPlatoons[11] = p11;

        Platoon p12 = new Platoon(12);
        enemyPlatoons[12] = p12;

        Platoon p13 = new Platoon(13);
        enemyPlatoons[13] = p13;

        Platoon p14 = new Platoon(14);
        enemyPlatoons[14] = p14;

        enemyPlatoonsRemaining = 15;

        //AddEnemyUnit(int charType, int position, int charClass, bool leader, int platoon, int level)


        //865
        AddEnemyUnit(1, 17, 44, true, 0, 85, true);
        AddEnemyUnit(1, 24, 26, false, 0, 68);
        AddEnemyUnit(1, 23, 27, false, 0, 68);
        AddEnemyUnit(1, 0, 38, false, 0, 68);
        AddEnemyUnit(1, 15, 29, false, 0, 68);
        AddEnemyUnit(1, 21, 30, false, 0, 68);
        AddEnemyUnit(1, 20, 31, false, 0, 68);
        AddEnemyUnit(1, 4, 40, false, 0, 68);
        AddEnemyUnit(1, 14, 34, false, 0, 68);
        AddEnemyUnit(1, 8, 33, false, 0, 68);
        AddEnemyUnit(1, 18, 28, false, 0, 68);
        AddEnemyUnit(1, 3, 39, false, 0, 68);
        AddEnemyUnit(1, 6, 32, false, 0, 68);


        //670
        AddEnemyUnit(1, 12, 34, true, 1, 70, false);
        AddEnemyUnit(3, 0, 0, false, 1, 55);
        AddEnemyUnit(3, 1, 1, false, 1, 55);
        AddEnemyUnit(3, 3, 2, false, 1, 55);
        AddEnemyUnit(3, 4, 3, false, 1, 55);
        AddEnemyUnit(1, 8, 25, false, 1, 55);
        AddEnemyUnit(1, 10, 24, false, 1, 55);
        AddEnemyUnit(1, 14, 23, false, 1, 55);
        AddEnemyUnit(1, 16, 22, false, 1, 55);
        AddEnemyUnit(1, 18, 21, false, 1, 55);
        AddEnemyUnit(1, 20, 20, false, 1, 55);
        AddEnemyUnit(1, 22, 19, false, 1, 55);
        AddEnemyUnit(1, 24, 18, false, 1, 55);

        //670
        AddEnemyUnit(1, 12, 35, true, 2, 70, false);
        AddEnemyUnit(3, 0, 0, false, 2, 55);
        AddEnemyUnit(3, 1, 1, false, 2, 55);
        AddEnemyUnit(3, 3, 2, false, 2, 55);
        AddEnemyUnit(3, 4, 3, false, 2, 55);
        AddEnemyUnit(1, 8, 17, false, 2, 55);
        AddEnemyUnit(1, 10, 16, false, 2, 55);
        AddEnemyUnit(1, 14, 15, false, 2, 55);
        AddEnemyUnit(1, 16, 14, false, 2, 55);
        AddEnemyUnit(1, 18, 13, false, 2, 55);
        AddEnemyUnit(1, 20, 12, false, 2, 55);
        AddEnemyUnit(1, 22, 11, false, 2, 55);
        AddEnemyUnit(1, 24, 11, false, 2, 55);

        //670
        AddEnemyUnit(1, 12, 36, true, 3, 70, false);
        AddEnemyUnit(3, 0, 0, false, 3, 55);
        AddEnemyUnit(3, 1, 1, false, 3, 55);
        AddEnemyUnit(3, 3, 2, false, 3, 55);
        AddEnemyUnit(3, 4, 3, false, 3, 55);
        AddEnemyUnit(1, 8, 12, false, 3, 55);
        AddEnemyUnit(1, 10, 13, false, 3, 55);
        AddEnemyUnit(1, 14, 14, false, 3, 55);
        AddEnemyUnit(1, 16, 15, false, 3, 55);
        AddEnemyUnit(1, 18, 16, false, 3, 55);
        AddEnemyUnit(1, 20, 17, false, 3, 55);
        AddEnemyUnit(1, 22, 18, false, 3, 55);
        AddEnemyUnit(1, 24, 19, false, 3, 55);

        //670
        AddEnemyUnit(1, 12, 37, true, 4, 70, false);
        AddEnemyUnit(3, 0, 0, false, 4, 55);
        AddEnemyUnit(3, 1, 1, false, 4, 55);
        AddEnemyUnit(3, 3, 2, false, 4, 55);
        AddEnemyUnit(3, 4, 3, false, 4, 55);
        AddEnemyUnit(1, 8, 20, false, 4, 55);
        AddEnemyUnit(1, 10, 21, false, 4, 55);
        AddEnemyUnit(1, 14, 22, false, 4, 55);
        AddEnemyUnit(1, 16, 23, false, 4, 55);
        AddEnemyUnit(1, 18, 24, false, 4, 55);
        AddEnemyUnit(1, 20, 25, false, 4, 55);
        AddEnemyUnit(1, 22, 25, false, 4, 55);
        AddEnemyUnit(1, 24, 24, false, 4, 55);

        //670
        AddEnemyUnit(1, 12, 38, true, 5, 70, false);
        AddEnemyUnit(3, 0, 0, false, 5, 55);
        AddEnemyUnit(3, 1, 1, false, 5, 55);
        AddEnemyUnit(3, 3, 2, false, 5, 55);
        AddEnemyUnit(3, 4, 3, false, 5, 55);
        AddEnemyUnit(1, 8, 23, false, 5, 55);
        AddEnemyUnit(1, 10, 22, false, 5, 55);
        AddEnemyUnit(1, 14, 21, false, 5, 55);
        AddEnemyUnit(1, 16, 20, false, 5, 55);
        AddEnemyUnit(1, 18, 19, false, 5, 55);
        AddEnemyUnit(1, 20, 18, false, 5, 55);
        AddEnemyUnit(1, 22, 17, false, 5, 55);
        AddEnemyUnit(1, 24, 16, false, 5, 55);

        //670
        AddEnemyUnit(1, 12, 39, true, 6, 70, false);
        AddEnemyUnit(3, 0, 0, false, 6, 55);
        AddEnemyUnit(3, 1, 1, false, 6, 55);
        AddEnemyUnit(3, 3, 2, false, 6, 55);
        AddEnemyUnit(3, 4, 3, false, 6, 55);
        AddEnemyUnit(1, 8, 15, false, 6, 55);
        AddEnemyUnit(1, 10, 14, false, 6, 55);
        AddEnemyUnit(1, 14, 13, false, 6, 55);
        AddEnemyUnit(1, 16, 12, false, 6, 55);
        AddEnemyUnit(1, 18, 11, false, 6, 55);
        AddEnemyUnit(1, 20, 11, false, 6, 55);
        AddEnemyUnit(1, 22, 12, false, 6, 55);
        AddEnemyUnit(1, 24, 13, false, 6, 55);

        //670
        AddEnemyUnit(1, 12, 40, true, 7, 70, false);
        AddEnemyUnit(3, 0, 0, false, 7, 55);
        AddEnemyUnit(3, 1, 1, false, 7, 55);
        AddEnemyUnit(3, 3, 2, false, 7, 55);
        AddEnemyUnit(3, 4, 3, false, 7, 55);
        AddEnemyUnit(1, 8, 14, false, 7, 55);
        AddEnemyUnit(1, 10, 15, false, 7, 55);
        AddEnemyUnit(1, 14, 16, false, 7, 55);
        AddEnemyUnit(1, 16, 17, false, 7, 55);
        AddEnemyUnit(1, 18, 18, false, 7, 55);
        AddEnemyUnit(1, 20, 19, false, 7, 55);
        AddEnemyUnit(1, 22, 20, false, 7, 55);
        AddEnemyUnit(1, 24, 21, false, 7, 55);

        //710
        AddEnemyUnit(1, 12, 33, true, 8, 75, false);
        AddEnemyUnit(3, 0, 0, false, 8, 60);
        AddEnemyUnit(3, 1, 1, false, 8, 60);
        AddEnemyUnit(3, 3, 2, false, 8, 60);
        AddEnemyUnit(3, 4, 3, false, 8, 60);
        AddEnemyUnit(3, 8, 4, false, 8, 60);
        AddEnemyUnit(3, 10, 5, false, 8, 60);
        AddEnemyUnit(3, 14, 6, false, 8, 60);
        AddEnemyUnit(3, 16, 7, false, 8, 60);
        AddEnemyUnit(1, 18, 26, false, 8, 55);
        AddEnemyUnit(1, 20, 32, false, 8, 55);
        AddEnemyUnit(1, 22, 35, false, 8, 55);
        AddEnemyUnit(1, 24, 38, false, 8, 55);

        //710
        AddEnemyUnit(1, 12, 32, true, 9, 75, false);
        AddEnemyUnit(3, 0, 0, false, 9, 60);
        AddEnemyUnit(3, 1, 1, false, 9, 60);
        AddEnemyUnit(3, 3, 2, false, 9, 60);
        AddEnemyUnit(3, 4, 3, false, 9, 60);
        AddEnemyUnit(3, 8, 8, false, 9, 60);
        AddEnemyUnit(3, 10, 5, false, 9, 60);
        AddEnemyUnit(3, 14, 6, false, 9, 60);
        AddEnemyUnit(3, 16, 7, false, 9, 60);
        AddEnemyUnit(1, 18, 27, false, 9, 55);
        AddEnemyUnit(1, 20, 36, false, 9, 55);
        AddEnemyUnit(1, 22, 33, false, 9, 55);
        AddEnemyUnit(1, 24, 39, false, 9, 55);

        //710
        AddEnemyUnit(1, 12, 29, true, 10, 75, false);
        AddEnemyUnit(3, 0, 0, false, 10, 60);
        AddEnemyUnit(3, 1, 1, false, 10, 60);
        AddEnemyUnit(3, 3, 2, false, 10, 60);
        AddEnemyUnit(3, 4, 3, false, 10, 60);
        AddEnemyUnit(3, 8, 4, false, 10, 60);
        AddEnemyUnit(3, 10, 8, false, 10, 60);
        AddEnemyUnit(3, 14, 6, false, 10, 60);
        AddEnemyUnit(3, 16, 7, false, 10, 60);
        AddEnemyUnit(1, 18, 28, false, 10, 55);
        AddEnemyUnit(1, 20, 34, false, 10, 55);
        AddEnemyUnit(1, 22, 37, false, 10, 55);
        AddEnemyUnit(1, 24, 40, false, 10, 55);

        //710
        AddEnemyUnit(1, 12, 28, true, 11, 75, false);
        AddEnemyUnit(3, 0, 0, false, 11, 60);
        AddEnemyUnit(3, 1, 1, false, 11, 60);
        AddEnemyUnit(3, 3, 2, false, 11, 60);
        AddEnemyUnit(3, 4, 3, false, 11, 60);
        AddEnemyUnit(3, 8, 4, false, 11, 60);
        AddEnemyUnit(3, 10, 5, false, 11, 60);
        AddEnemyUnit(3, 14, 8, false, 11, 60);
        AddEnemyUnit(3, 16, 7, false, 11, 60);
        AddEnemyUnit(1, 18, 29, false, 11, 55);
        AddEnemyUnit(1, 20, 32, false, 11, 55);
        AddEnemyUnit(1, 22, 36, false, 11, 55);
        AddEnemyUnit(1, 24, 40, false, 11, 55);

        //710
        AddEnemyUnit(1, 12, 27, true, 12, 75, false);
        AddEnemyUnit(3, 0, 0, false, 12, 60);
        AddEnemyUnit(3, 1, 1, false, 12, 60);
        AddEnemyUnit(3, 3, 2, false, 12, 60);
        AddEnemyUnit(3, 4, 3, false, 12, 60);
        AddEnemyUnit(3, 8, 4, false, 12, 60);
        AddEnemyUnit(3, 10, 5, false, 12, 60);
        AddEnemyUnit(3, 14, 6, false, 12, 60);
        AddEnemyUnit(3, 16, 8, false, 12, 60);
        AddEnemyUnit(1, 18, 30, false, 12, 55);
        AddEnemyUnit(1, 20, 33, false, 12, 55);
        AddEnemyUnit(1, 22, 37, false, 12, 55);
        AddEnemyUnit(1, 24, 38, false, 12, 55);

        //710
        AddEnemyUnit(1, 12, 26, true, 13, 75, false);
        AddEnemyUnit(3, 0, 0, false, 13, 60);
        AddEnemyUnit(3, 1, 1, false, 13, 60);
        AddEnemyUnit(3, 3, 2, false, 13, 60);
        AddEnemyUnit(3, 4, 3, false, 13, 60);
        AddEnemyUnit(3, 8, 4, false, 13, 60);
        AddEnemyUnit(3, 10, 5, false, 13, 60);
        AddEnemyUnit(3, 14, 6, false, 13, 60);
        AddEnemyUnit(3, 16, 7, false, 13, 60);
        AddEnemyUnit(1, 18, 31, false, 13, 55);
        AddEnemyUnit(1, 20, 34, false, 13, 55);
        AddEnemyUnit(1, 22, 35, false, 13, 55);
        AddEnemyUnit(1, 24, 39, false, 13, 55);

        //775
        AddEnemyUnit(1, 12, 42, true, 14, 75, true);
        AddEnemyUnit(2, 5, 8, false, 14, 55);
        AddEnemyUnit(2, 20, 9, false, 14, 55);
        AddEnemyUnit(3, 8, 12, false, 14, 60);
        AddEnemyUnit(3, 23, 11, false, 14, 60);
        AddEnemyUnit(3, 14, 10, false, 14, 60);
        AddEnemyUnit(3, 13, 10, false, 14, 60);
        AddEnemyUnit(3, 11, 10, false, 14, 60);
        AddEnemyUnit(3, 10, 10, false, 14, 60);


        for (int i = 0; i < 11; i++)
        {
            GameController.Instance.SetPlayerPlatoon(i);
        }
        maxDeployedPlatoon = 10;
    }

    private void Level20Setup()
    {
        Debug.Log("Level 20 Setup");

        Platoon p0 = new Platoon(0);
        enemyPlatoons[0] = p0;

        Platoon p1 = new Platoon(1);
        enemyPlatoons[1] = p1;

        Platoon p2 = new Platoon(2);
        enemyPlatoons[2] = p2;

        Platoon p3 = new Platoon(3);
        enemyPlatoons[3] = p3;

        Platoon p4 = new Platoon(4);
        enemyPlatoons[4] = p4;

        Platoon p5 = new Platoon(5);
        enemyPlatoons[5] = p5;

        Platoon p6 = new Platoon(6);
        enemyPlatoons[6] = p6;

        Platoon p7 = new Platoon(7);
        enemyPlatoons[7] = p7;

        Platoon p8 = new Platoon(8);
        enemyPlatoons[8] = p8;

        Platoon p9 = new Platoon(9);
        enemyPlatoons[9] = p9;

        Platoon p10 = new Platoon(10);
        enemyPlatoons[10] = p10;

        Platoon p11 = new Platoon(11);
        enemyPlatoons[11] = p11;

        Platoon p12 = new Platoon(12);
        enemyPlatoons[12] = p12;

        Platoon p13 = new Platoon(13);
        enemyPlatoons[13] = p13;

        Platoon p14 = new Platoon(14);
        enemyPlatoons[14] = p14;

        Platoon p15 = new Platoon(15);
        enemyPlatoons[15] = p15;

        enemyPlatoonsRemaining = 16;

        //AddEnemyUnit(int charType, int position, int charClass, bool leader, int platoon, int level)


        //865
        AddEnemyUnit(1, 17, 45, true, 0, 90, true);
        AddEnemyUnit(1, 24, 26, false, 0, 72);
        AddEnemyUnit(1, 23, 27, false, 0, 72);
        AddEnemyUnit(1, 0, 38, false, 0, 72);
        AddEnemyUnit(1, 15, 29, false, 0, 72);
        AddEnemyUnit(1, 21, 30, false, 0, 72);
        AddEnemyUnit(1, 20, 31, false, 0, 72);
        AddEnemyUnit(1, 4, 40, false, 0, 72);
        AddEnemyUnit(1, 14, 34, false, 0, 72);
        AddEnemyUnit(1, 8, 33, false, 0, 72);
        AddEnemyUnit(1, 18, 28, false, 0, 72);
        AddEnemyUnit(1, 3, 39, false, 0, 72);
        AddEnemyUnit(1, 6, 32, false, 0, 72);

        //670
        AddEnemyUnit(1, 12, 34, true, 1, 75, true);
        AddEnemyUnit(3, 0, 6, false, 1, 60);
        AddEnemyUnit(3, 1, 5, false, 1, 60);
        AddEnemyUnit(3, 3, 4, false, 1, 60);
        AddEnemyUnit(3, 4, 8, false, 1, 60);
        AddEnemyUnit(1, 8, 34, false, 1, 65);
        AddEnemyUnit(1, 10, 30, false, 1, 65);
        AddEnemyUnit(1, 14, 26, false, 1, 65);
        AddEnemyUnit(1, 16, 28, false, 1, 65);
        AddEnemyUnit(1, 18, 32, false, 1, 65);
        AddEnemyUnit(1, 20, 36, false, 1, 65);
        AddEnemyUnit(1, 22, 40, false, 1, 65);
        AddEnemyUnit(1, 24, 38, false, 1, 65);

        //670
        AddEnemyUnit(1, 12, 35, true, 2, 75, true);
        AddEnemyUnit(3, 0, 7, false, 2, 60);
        AddEnemyUnit(3, 1, 6, false, 2, 60);
        AddEnemyUnit(3, 3, 5, false, 2, 60);
        AddEnemyUnit(3, 4, 4, false, 2, 60);
        AddEnemyUnit(1, 8, 34, false, 2, 65);
        AddEnemyUnit(1, 10, 30, false, 2, 65);
        AddEnemyUnit(1, 14, 26, false, 2, 65);
        AddEnemyUnit(1, 16, 27, false, 2, 65);
        AddEnemyUnit(1, 18, 30, false, 2, 65);
        AddEnemyUnit(1, 20, 33, false, 2, 65);
        AddEnemyUnit(1, 22, 36, false, 2, 65);
        AddEnemyUnit(1, 24, 39, false, 2, 65);

        //670
        AddEnemyUnit(1, 12, 36, true, 3, 75, true);
        AddEnemyUnit(3, 0, 8, false, 3, 60);
        AddEnemyUnit(3, 1, 7, false, 3, 60);
        AddEnemyUnit(3, 3, 6, false, 3, 60);
        AddEnemyUnit(3, 4, 5, false, 3, 60);
        AddEnemyUnit(1, 8, 38, false, 3, 65);
        AddEnemyUnit(1, 10, 35, false, 3, 65);
        AddEnemyUnit(1, 14, 32, false, 3, 65);
        AddEnemyUnit(1, 16, 29, false, 3, 65);
        AddEnemyUnit(1, 18, 26, false, 3, 65);
        AddEnemyUnit(1, 20, 28, false, 3, 65);
        AddEnemyUnit(1, 22, 31, false, 3, 65);
        AddEnemyUnit(1, 24, 34, false, 3, 65);

        //670
        AddEnemyUnit(1, 12, 37, true, 4, 75, true);
        AddEnemyUnit(3, 0, 4, false, 4, 60);
        AddEnemyUnit(3, 1, 8, false, 4, 60);
        AddEnemyUnit(3, 3, 7, false, 4, 60);
        AddEnemyUnit(3, 4, 6, false, 4, 60);
        AddEnemyUnit(1, 8, 37, false, 4, 65);
        AddEnemyUnit(1, 10, 40, false, 4, 65);
        AddEnemyUnit(1, 14, 39, false, 4, 65);
        AddEnemyUnit(1, 16, 37, false, 4, 65);
        AddEnemyUnit(1, 18, 35, false, 4, 65);
        AddEnemyUnit(1, 20, 33, false, 4, 65);
        AddEnemyUnit(1, 22, 31, false, 4, 65);
        AddEnemyUnit(1, 24, 29, false, 4, 65);

        //670
        AddEnemyUnit(1, 12, 38, true, 5, 75, true);
        AddEnemyUnit(3, 0, 5, false, 5, 60);
        AddEnemyUnit(3, 1, 4, false, 5, 60);
        AddEnemyUnit(3, 3, 8, false, 5, 60);
        AddEnemyUnit(3, 4, 7, false, 5, 60);
        AddEnemyUnit(1, 8, 27, false, 5, 65);
        AddEnemyUnit(1, 10, 26, false, 5, 65);
        AddEnemyUnit(1, 14, 28, false, 5, 65);
        AddEnemyUnit(1, 16, 30, false, 5, 65);
        AddEnemyUnit(1, 18, 32, false, 5, 65);
        AddEnemyUnit(1, 20, 34, false, 5, 65);
        AddEnemyUnit(1, 22, 36, false, 5, 65);
        AddEnemyUnit(1, 24, 38, false, 5, 65);

        //670
        AddEnemyUnit(1, 12, 39, true, 6, 75, true);
        AddEnemyUnit(3, 0, 6, false, 6, 60);
        AddEnemyUnit(3, 1, 5, false, 6, 60);
        AddEnemyUnit(3, 3, 4, false, 6, 60);
        AddEnemyUnit(3, 4, 8, false, 6, 60);
        AddEnemyUnit(1, 8, 40, false, 6, 65);
        AddEnemyUnit(1, 10, 40, false, 6, 65);
        AddEnemyUnit(1, 14, 39, false, 6, 65);
        AddEnemyUnit(1, 16, 38, false, 6, 65);
        AddEnemyUnit(1, 18, 37, false, 6, 65);
        AddEnemyUnit(1, 20, 36, false, 6, 65);
        AddEnemyUnit(1, 22, 35, false, 6, 65);
        AddEnemyUnit(1, 24, 34, false, 6, 65);

        //670
        AddEnemyUnit(1, 12, 40, true, 7, 75, true);
        AddEnemyUnit(3, 0, 4, false, 7, 60);
        AddEnemyUnit(3, 1, 5, false, 7, 60);
        AddEnemyUnit(3, 3, 6, false, 7, 60);
        AddEnemyUnit(3, 4, 7, false, 7, 60);
        AddEnemyUnit(1, 8, 26, false, 7, 65);
        AddEnemyUnit(1, 10, 27, false, 7, 65);
        AddEnemyUnit(1, 14, 28, false, 7, 65);
        AddEnemyUnit(1, 16, 29, false, 7, 65);
        AddEnemyUnit(1, 18, 30, false, 7, 65);
        AddEnemyUnit(1, 20, 31, false, 7, 65);
        AddEnemyUnit(1, 22, 32, false, 7, 65);
        AddEnemyUnit(1, 24, 33, false, 7, 65);

        //710
        AddEnemyUnit(1, 12, 33, true, 8, 75, false);
        AddEnemyUnit(3, 0, 0, false, 8, 55);
        AddEnemyUnit(3, 1, 1, false, 8, 55);
        AddEnemyUnit(3, 3, 2, false, 8, 55);
        AddEnemyUnit(3, 4, 3, false, 8, 55);
        AddEnemyUnit(3, 8, 4, false, 8, 60);
        AddEnemyUnit(3, 10, 5, false, 8, 60);
        AddEnemyUnit(3, 14, 6, false, 8, 60);
        AddEnemyUnit(3, 16, 7, false, 8, 60);
        AddEnemyUnit(1, 18, 26, false, 8, 60);
        AddEnemyUnit(1, 20, 32, false, 8, 60);
        AddEnemyUnit(1, 22, 35, false, 8, 60);
        AddEnemyUnit(1, 24, 38, false, 8, 60);

        //710
        AddEnemyUnit(1, 12, 32, true, 9, 75, false);
        AddEnemyUnit(3, 0, 0, false, 9, 55);
        AddEnemyUnit(3, 1, 1, false, 9, 55);
        AddEnemyUnit(3, 3, 2, false, 9, 55);
        AddEnemyUnit(3, 4, 3, false, 9, 55);
        AddEnemyUnit(3, 8, 8, false, 9, 60);
        AddEnemyUnit(3, 10, 5, false, 9, 60);
        AddEnemyUnit(3, 14, 6, false, 9, 60);
        AddEnemyUnit(3, 16, 7, false, 9, 60);
        AddEnemyUnit(1, 18, 27, false, 9, 60);
        AddEnemyUnit(1, 20, 36, false, 9, 60);
        AddEnemyUnit(1, 22, 33, false, 9, 60);
        AddEnemyUnit(1, 24, 39, false, 9, 60);

        //710
        AddEnemyUnit(1, 12, 29, true, 10, 75, false);
        AddEnemyUnit(3, 0, 0, false, 10, 55);
        AddEnemyUnit(3, 1, 1, false, 10, 55);
        AddEnemyUnit(3, 3, 2, false, 10, 55);
        AddEnemyUnit(3, 4, 3, false, 10, 55);
        AddEnemyUnit(3, 8, 4, false, 10, 60);
        AddEnemyUnit(3, 10, 8, false, 10, 60);
        AddEnemyUnit(3, 14, 6, false, 10, 60);
        AddEnemyUnit(3, 16, 7, false, 10, 60);
        AddEnemyUnit(1, 18, 28, false, 10, 60);
        AddEnemyUnit(1, 20, 34, false, 10, 60);
        AddEnemyUnit(1, 22, 37, false, 10, 60);
        AddEnemyUnit(1, 24, 40, false, 10, 60);

        //710
        AddEnemyUnit(1, 12, 30, true, 11, 75, false);
        AddEnemyUnit(3, 0, 0, false, 11, 55);
        AddEnemyUnit(3, 1, 1, false, 11, 55);
        AddEnemyUnit(3, 3, 2, false, 11, 55);
        AddEnemyUnit(3, 4, 3, false, 11, 55);
        AddEnemyUnit(3, 8, 4, false, 11, 60);
        AddEnemyUnit(3, 10, 5, false, 11, 60);
        AddEnemyUnit(3, 14, 8, false, 11, 60);
        AddEnemyUnit(3, 16, 7, false, 11, 60);
        AddEnemyUnit(1, 18, 29, false, 11, 60);
        AddEnemyUnit(1, 20, 32, false, 11, 60);
        AddEnemyUnit(1, 22, 36, false, 11, 60);
        AddEnemyUnit(1, 24, 40, false, 11, 60);

        //710
        AddEnemyUnit(1, 12, 27, true, 12, 75, false);
        AddEnemyUnit(3, 0, 0, false, 12, 55);
        AddEnemyUnit(3, 1, 1, false, 12, 55);
        AddEnemyUnit(3, 3, 2, false, 12, 55);
        AddEnemyUnit(3, 4, 3, false, 12, 55);
        AddEnemyUnit(3, 8, 4, false, 12, 60);
        AddEnemyUnit(3, 10, 5, false, 12, 60);
        AddEnemyUnit(3, 14, 6, false, 12, 60);
        AddEnemyUnit(3, 16, 8, false, 12, 60);
        AddEnemyUnit(1, 18, 30, false, 12, 60);
        AddEnemyUnit(1, 20, 33, false, 12, 60);
        AddEnemyUnit(1, 22, 37, false, 12, 60);
        AddEnemyUnit(1, 24, 38, false, 12, 60);

        //710
        AddEnemyUnit(1, 12, 26, true, 13, 75, false);
        AddEnemyUnit(3, 0, 0, false, 13, 55);
        AddEnemyUnit(3, 1, 1, false, 13, 55);
        AddEnemyUnit(3, 3, 2, false, 13, 55);
        AddEnemyUnit(3, 4, 3, false, 13, 55);
        AddEnemyUnit(3, 8, 4, false, 13, 60);
        AddEnemyUnit(3, 10, 5, false, 13, 60);
        AddEnemyUnit(3, 14, 6, false, 13, 60);
        AddEnemyUnit(3, 16, 7, false, 13, 60);
        AddEnemyUnit(1, 18, 31, false, 13, 60);
        AddEnemyUnit(1, 20, 34, false, 13, 60);
        AddEnemyUnit(1, 22, 35, false, 13, 60);
        AddEnemyUnit(1, 24, 39, false, 13, 60);

        //710
        AddEnemyUnit(1, 12, 31, true, 14, 75, false);
        AddEnemyUnit(3, 0, 0, false, 14, 55);
        AddEnemyUnit(3, 1, 1, false, 14, 55);
        AddEnemyUnit(3, 3, 2, false, 14, 55);
        AddEnemyUnit(3, 4, 3, false, 14, 55);
        AddEnemyUnit(3, 8, 8, false, 14, 60);
        AddEnemyUnit(3, 10, 7, false, 14, 60);
        AddEnemyUnit(3, 14, 6, false, 14, 60);
        AddEnemyUnit(3, 16, 5, false, 14, 60);
        AddEnemyUnit(1, 18, 41, false, 14, 60);
        AddEnemyUnit(1, 20, 42, false, 14, 60);
        AddEnemyUnit(1, 22, 28, false, 14, 60);
        AddEnemyUnit(1, 24, 29, false, 14, 60);

        //775
        AddEnemyUnit(1, 12, 46, true, 15, 80, false);
        AddEnemyUnit(2, 5, 7, false, 15, 60);
        AddEnemyUnit(2, 20, 9, false, 15, 60);
        AddEnemyUnit(3, 8, 11, false, 15, 65);
        AddEnemyUnit(3, 23, 9, false, 15, 65);
        AddEnemyUnit(3, 14, 10, false, 15, 60);
        AddEnemyUnit(3, 13, 10, false, 15, 60);
        AddEnemyUnit(3, 11, 10, false, 15, 60);
        AddEnemyUnit(3, 10, 10, false, 15, 60);


        for (int i = 0; i < 11; i++)
        {
            GameController.Instance.SetPlayerPlatoon(i);
        }
        maxDeployedPlatoon = 10;
    }

    private void Level21Setup()
    {
        Debug.Log("Level 21 Setup");

        Platoon p0 = new Platoon(0);
        enemyPlatoons[0] = p0;

        Platoon p1 = new Platoon(1);
        enemyPlatoons[1] = p1;

        Platoon p2 = new Platoon(2);
        enemyPlatoons[2] = p2;

        Platoon p3 = new Platoon(3);
        enemyPlatoons[3] = p3;

        Platoon p4 = new Platoon(4);
        enemyPlatoons[4] = p4;

        Platoon p5 = new Platoon(5);
        enemyPlatoons[5] = p5;

        Platoon p6 = new Platoon(6);
        enemyPlatoons[6] = p6;

        Platoon p7 = new Platoon(7);
        enemyPlatoons[7] = p7;

        Platoon p8 = new Platoon(8);
        enemyPlatoons[8] = p8;

        Platoon p9 = new Platoon(9);
        enemyPlatoons[9] = p9;

        Platoon p10 = new Platoon(10);
        enemyPlatoons[10] = p10;


        enemyPlatoonsRemaining = 11;

        //AddEnemyUnit(int charType, int position, int charClass, bool leader, int platoon, int level)


        //865
        AddEnemyUnit(1, 17, 46, true, 0, 95, true);
        AddEnemyUnit(3, 23, 9, false, 0, 75);
        AddEnemyUnit(1, 12, 40, false, 0, 80);
        AddEnemyUnit(1, 0, 41, false, 0, 80);
        AddEnemyUnit(1, 15, 35, false, 0, 80);
        AddEnemyUnit(1, 21, 36, false, 0, 80);
        AddEnemyUnit(1, 20, 37, false, 0, 80);
        AddEnemyUnit(1, 4, 42, false, 0, 80);
        AddEnemyUnit(1, 14, 34, false, 0, 80);
        AddEnemyUnit(1, 8, 33, false, 0, 80);
        AddEnemyUnit(1, 16, 28, false, 0, 80);
        AddEnemyUnit(1, 3, 39, false, 0, 80);

        //670
        AddEnemyUnit(1, 12, 34, true, 1, 75, true);
        AddEnemyUnit(2, 2, 6, false, 1, 65);
        AddEnemyUnit(2, 5, 5, false, 1, 65);
        AddEnemyUnit(1, 3, 38, false, 1, 60);
        AddEnemyUnit(1, 8, 34, false, 1, 60);
        AddEnemyUnit(1, 10, 30, false, 1, 60);
        AddEnemyUnit(1, 14, 26, false, 1, 60);
        AddEnemyUnit(1, 16, 28, false, 1, 60);
        AddEnemyUnit(1, 18, 32, false, 1, 60);
        AddEnemyUnit(1, 20, 36, false, 1, 60);
        AddEnemyUnit(1, 22, 40, false, 1, 60);
        AddEnemyUnit(1, 24, 38, false, 1, 60);

        //670
        AddEnemyUnit(1, 12, 35, true, 2, 75, true);
        AddEnemyUnit(2, 2, 3, false, 2, 65);
        AddEnemyUnit(2, 5, 4, false, 2, 65);
        AddEnemyUnit(1, 3, 40, false, 2, 60);
        AddEnemyUnit(1, 8, 34, false, 2, 60);
        AddEnemyUnit(1, 10, 30, false, 2, 60);
        AddEnemyUnit(1, 14, 26, false, 2, 60);
        AddEnemyUnit(1, 16, 27, false, 2, 60);
        AddEnemyUnit(1, 18, 30, false, 2, 60);
        AddEnemyUnit(1, 20, 33, false, 2, 60);
        AddEnemyUnit(1, 22, 36, false, 2, 60);
        AddEnemyUnit(1, 24, 39, false, 2, 60);

        //670
        AddEnemyUnit(1, 12, 36, true, 3, 75, true);
        AddEnemyUnit(2, 2, 3, false, 3, 65);
        AddEnemyUnit(2, 5, 5, false, 3, 65);
        AddEnemyUnit(1, 3, 34, false, 3, 60);
        AddEnemyUnit(1, 8, 38, false, 3, 60);
        AddEnemyUnit(1, 10, 35, false, 3, 60);
        AddEnemyUnit(1, 14, 32, false, 3, 60);
        AddEnemyUnit(1, 16, 29, false, 3, 60);
        AddEnemyUnit(1, 18, 26, false, 3, 60);
        AddEnemyUnit(1, 20, 28, false, 3, 60);
        AddEnemyUnit(1, 22, 31, false, 3, 60);
        AddEnemyUnit(1, 24, 34, false, 3, 60);

        //670
        AddEnemyUnit(1, 12, 37, true, 4, 80, true);
        AddEnemyUnit(3, 8, 10, false, 4, 75);
        AddEnemyUnit(3, 1, 8, false, 4, 70);
        AddEnemyUnit(3, 3, 7, false, 4, 70);
        AddEnemyUnit(3, 4, 6, false, 4, 70);
        AddEnemyUnit(3, 0, 4, false, 4, 70);
        AddEnemyUnit(3, 10, 5, false, 4, 70);
        AddEnemyUnit(1, 14, 39, false, 4, 65);
        AddEnemyUnit(1, 16, 37, false, 4, 65);
        AddEnemyUnit(1, 18, 35, false, 4, 65);
        AddEnemyUnit(1, 20, 33, false, 4, 65);
        AddEnemyUnit(1, 22, 31, false, 4, 65);
        AddEnemyUnit(1, 24, 29, false, 4, 65);

        //670
        AddEnemyUnit(1, 12, 38, true, 5, 80, false);
        AddEnemyUnit(3, 0, 5, false, 5, 70);
        AddEnemyUnit(3, 1, 4, false, 5, 70);
        AddEnemyUnit(3, 3, 8, false, 5, 70);
        AddEnemyUnit(3, 4, 7, false, 5, 70);
        AddEnemyUnit(3, 8, 10, false, 5, 75);
        AddEnemyUnit(3, 10, 6, false, 5, 70);
        AddEnemyUnit(1, 14, 28, false, 5, 65);
        AddEnemyUnit(1, 16, 30, false, 5, 65);
        AddEnemyUnit(1, 18, 32, false, 5, 65);
        AddEnemyUnit(1, 20, 34, false, 5, 65);
        AddEnemyUnit(1, 22, 36, false, 5, 65);
        AddEnemyUnit(1, 24, 38, false, 5, 65);

        //670
        AddEnemyUnit(1, 12, 39, true, 6, 80, false);
        AddEnemyUnit(3, 0, 6, false, 6, 70);
        AddEnemyUnit(3, 1, 5, false, 6, 70);
        AddEnemyUnit(3, 3, 4, false, 6, 70);
        AddEnemyUnit(3, 4, 8, false, 6, 70);
        AddEnemyUnit(3, 8, 7, false, 6, 70);
        AddEnemyUnit(3, 10, 10, false, 6, 75);
        AddEnemyUnit(1, 14, 39, false, 6, 65);
        AddEnemyUnit(1, 16, 38, false, 6, 65);
        AddEnemyUnit(1, 18, 37, false, 6, 65);
        AddEnemyUnit(1, 20, 36, false, 6, 65);
        AddEnemyUnit(1, 22, 35, false, 6, 65);
        AddEnemyUnit(1, 24, 34, false, 6, 65);

        //670
        AddEnemyUnit(1, 12, 40, true, 7, 80, false);
        AddEnemyUnit(3, 0, 4, false, 7, 70);
        AddEnemyUnit(3, 1, 5, false, 7, 70);
        AddEnemyUnit(3, 3, 6, false, 7, 70);
        AddEnemyUnit(3, 4, 7, false, 7, 70);
        AddEnemyUnit(3, 8, 8, false, 7, 70);
        AddEnemyUnit(3, 10, 10, false, 7, 75);
        AddEnemyUnit(1, 14, 28, false, 7, 65);
        AddEnemyUnit(1, 16, 29, false, 7, 65);
        AddEnemyUnit(1, 18, 30, false, 7, 65);
        AddEnemyUnit(1, 20, 31, false, 7, 65);
        AddEnemyUnit(1, 22, 32, false, 7, 65);
        AddEnemyUnit(1, 24, 33, false, 7, 65);

        //710
        AddEnemyUnit(1, 12, 44, true, 8, 85, false);
        AddEnemyUnit(3, 23, 9, false, 8, 75);
        AddEnemyUnit(2, 8, 9, false, 8, 70);
        AddEnemyUnit(1, 17, 34, false, 8, 70);
        AddEnemyUnit(1, 10, 40, false, 8, 70);
        AddEnemyUnit(1, 14, 36, false, 8, 70);
        AddEnemyUnit(1, 16, 27, false, 8, 70);
        AddEnemyUnit(1, 21, 26, false, 8, 70);
        AddEnemyUnit(1, 20, 32, false, 8, 70);
        AddEnemyUnit(1, 22, 35, false, 8, 70);
        AddEnemyUnit(1, 2, 38, false, 8, 70);

        //710
        AddEnemyUnit(1, 12, 44, true, 9, 85, false);
        AddEnemyUnit(3, 20, 11, false, 9, 75);
        AddEnemyUnit(2, 5, 8, false, 9, 70);
        AddEnemyUnit(1, 8, 38, false, 9, 70);
        AddEnemyUnit(1, 10, 30, false, 9, 70);
        AddEnemyUnit(1, 14, 37, false, 9, 70);
        AddEnemyUnit(1, 23, 29, false, 9, 70);
        AddEnemyUnit(1, 18, 27, false, 9, 70);
        AddEnemyUnit(1, 2, 36, false, 9, 70);
        AddEnemyUnit(1, 22, 33, false, 9, 70);
        AddEnemyUnit(1, 24, 39, false, 9, 70);

        //710
        AddEnemyUnit(1, 12, 44, true, 10, 85, false);
        AddEnemyUnit(3, 23, 12, false, 10, 75);
        AddEnemyUnit(2, 8, 7, false, 10, 70);
        AddEnemyUnit(1, 21, 32, false, 10, 70);
        AddEnemyUnit(1, 17, 33, false, 10, 70);
        AddEnemyUnit(1, 14, 36, false, 10, 70);
        AddEnemyUnit(1, 16, 30, false, 10, 70);
        AddEnemyUnit(1, 0, 28, false, 10, 70);
        AddEnemyUnit(1, 20, 34, false, 10, 70);
        AddEnemyUnit(1, 22, 37, false, 10, 70);
        AddEnemyUnit(1, 2, 40, false, 10, 70);


        for (int i = 0; i < 11; i++)
        {
            GameController.Instance.SetPlayerPlatoon(i);
        }
        maxDeployedPlatoon = 10;
    }

    private void Level22Setup()
    {
        Debug.Log("Level 22 Setup");

        Platoon p0 = new Platoon(0);
        enemyPlatoons[0] = p0;

        Platoon p1 = new Platoon(1);
        enemyPlatoons[1] = p1;

        Platoon p2 = new Platoon(2);
        enemyPlatoons[2] = p2;

        Platoon p3 = new Platoon(3);
        enemyPlatoons[3] = p3;

        Platoon p4 = new Platoon(4);
        enemyPlatoons[4] = p4;

        Platoon p5 = new Platoon(5);
        enemyPlatoons[5] = p5;

        Platoon p6 = new Platoon(6);
        enemyPlatoons[6] = p6;

        Platoon p7 = new Platoon(7);
        enemyPlatoons[7] = p7;

        Platoon p8 = new Platoon(8);
        enemyPlatoons[8] = p8;

        Platoon p9 = new Platoon(9);
        enemyPlatoons[9] = p9;

        Platoon p10 = new Platoon(10);
        enemyPlatoons[10] = p10;

        Platoon p11 = new Platoon(11);
        enemyPlatoons[11] = p11;

        Platoon p12 = new Platoon(12);
        enemyPlatoons[12] = p12;

        Platoon p13 = new Platoon(13);
        enemyPlatoons[13] = p13;

        Platoon p14 = new Platoon(14);
        enemyPlatoons[14] = p14;

        enemyPlatoonsRemaining = 15;

        //AddEnemyUnit(int charType, int position, int charClass, bool leader, int platoon, int level)


        //865
        AddEnemyUnit(1, 17, 47, true, 0, 95, false);
        AddEnemyUnit(3, 23, 11, false, 0, 80);
        AddEnemyUnit(1, 12, 40, false, 0, 85);
        AddEnemyUnit(1, 0, 41, false, 0, 85);
        AddEnemyUnit(1, 15, 35, false, 0, 85);
        AddEnemyUnit(1, 21, 36, false, 0, 85);
        AddEnemyUnit(1, 20, 37, false, 0, 85);
        AddEnemyUnit(1, 4, 42, false, 0, 85);
        AddEnemyUnit(1, 14, 34, false, 0, 85);
        AddEnemyUnit(1, 8, 33, false, 0, 85);
        AddEnemyUnit(1, 16, 28, false, 0, 85);
        AddEnemyUnit(1, 3, 39, false, 0, 85);

        //670
        AddEnemyUnit(1, 12, 34, true, 1, 75, false);
        AddEnemyUnit(2, 2, 6, false, 1, 60);
        AddEnemyUnit(2, 5, 5, false, 1, 60);
        AddEnemyUnit(1, 3, 38, false, 1, 65);
        AddEnemyUnit(1, 8, 34, false, 1, 65);
        AddEnemyUnit(1, 10, 30, false, 1, 65);
        AddEnemyUnit(1, 14, 26, false, 1, 65);
        AddEnemyUnit(1, 16, 28, false, 1, 65);
        AddEnemyUnit(1, 18, 32, false, 1, 65);
        AddEnemyUnit(1, 20, 36, false, 1, 65);
        AddEnemyUnit(1, 22, 40, false, 1, 65);
        AddEnemyUnit(1, 24, 38, false, 1, 65);

        //670
        AddEnemyUnit(1, 12, 35, true, 2, 75, true);
        AddEnemyUnit(2, 2, 3, false, 2, 60);
        AddEnemyUnit(2, 5, 4, false, 2, 60);
        AddEnemyUnit(1, 3, 40, false, 2, 65);
        AddEnemyUnit(1, 8, 34, false, 2, 65);
        AddEnemyUnit(1, 10, 30, false, 2, 65);
        AddEnemyUnit(1, 14, 26, false, 2, 65);
        AddEnemyUnit(1, 16, 27, false, 2, 65);
        AddEnemyUnit(1, 18, 30, false, 2, 65);
        AddEnemyUnit(1, 20, 33, false, 2, 65);
        AddEnemyUnit(1, 22, 36, false, 2, 65);
        AddEnemyUnit(1, 24, 39, false, 2, 65);

        //670
        AddEnemyUnit(1, 12, 36, true, 3, 75, true);
        AddEnemyUnit(2, 2, 3, false, 3, 60);
        AddEnemyUnit(2, 5, 5, false, 3, 60);
        AddEnemyUnit(1, 3, 34, false, 3, 65);
        AddEnemyUnit(1, 8, 38, false, 3, 65);
        AddEnemyUnit(1, 10, 35, false, 3, 65);
        AddEnemyUnit(1, 14, 32, false, 3, 65);
        AddEnemyUnit(1, 16, 29, false, 3, 65);
        AddEnemyUnit(1, 18, 26, false, 3, 65);
        AddEnemyUnit(1, 20, 28, false, 3, 65);
        AddEnemyUnit(1, 22, 31, false, 3, 65);
        AddEnemyUnit(1, 24, 34, false, 3, 65);

        //670
        AddEnemyUnit(1, 12, 37, true, 4, 80, false);
        AddEnemyUnit(3, 8, 10, false, 4, 75);
        AddEnemyUnit(3, 1, 8, false, 4, 70);
        AddEnemyUnit(3, 3, 7, false, 4, 70);
        AddEnemyUnit(3, 4, 6, false, 4, 70);
        AddEnemyUnit(3, 0, 4, false, 4, 70);
        AddEnemyUnit(3, 10, 5, false, 4, 70);
        AddEnemyUnit(1, 14, 39, false, 4, 70);
        AddEnemyUnit(1, 16, 37, false, 4, 70);
        AddEnemyUnit(1, 18, 35, false, 4, 70);
        AddEnemyUnit(1, 20, 33, false, 4, 70);
        AddEnemyUnit(1, 22, 31, false, 4, 70);
        AddEnemyUnit(1, 24, 29, false, 4, 70);

        //670
        AddEnemyUnit(1, 12, 38, true, 5, 80, false);
        AddEnemyUnit(3, 0, 5, false, 5, 70);
        AddEnemyUnit(3, 1, 4, false, 5, 70);
        AddEnemyUnit(3, 3, 8, false, 5, 70);
        AddEnemyUnit(3, 4, 7, false, 5, 70);
        AddEnemyUnit(3, 8, 10, false, 5, 75);
        AddEnemyUnit(3, 10, 6, false, 5, 70);
        AddEnemyUnit(1, 14, 28, false, 5, 70);
        AddEnemyUnit(1, 16, 30, false, 5, 70);
        AddEnemyUnit(1, 18, 32, false, 5, 70);
        AddEnemyUnit(1, 20, 34, false, 5, 70);
        AddEnemyUnit(1, 22, 36, false, 5, 70);
        AddEnemyUnit(1, 24, 38, false, 5, 70);

        //670
        AddEnemyUnit(1, 12, 39, true, 6, 80, true);
        AddEnemyUnit(3, 0, 6, false, 6, 70);
        AddEnemyUnit(3, 1, 5, false, 6, 70);
        AddEnemyUnit(3, 3, 4, false, 6, 70);
        AddEnemyUnit(3, 4, 8, false, 6, 70);
        AddEnemyUnit(3, 8, 7, false, 6, 70);
        AddEnemyUnit(3, 10, 10, false, 6, 75);
        AddEnemyUnit(1, 14, 39, false, 6, 70);
        AddEnemyUnit(1, 16, 38, false, 6, 70);
        AddEnemyUnit(1, 18, 37, false, 6, 70);
        AddEnemyUnit(1, 20, 36, false, 6, 70);
        AddEnemyUnit(1, 22, 35, false, 6, 70);
        AddEnemyUnit(1, 24, 34, false, 6, 70);

        //670
        AddEnemyUnit(1, 12, 40, true, 7, 80, true);
        AddEnemyUnit(3, 0, 4, false, 7, 70);
        AddEnemyUnit(3, 1, 5, false, 7, 70);
        AddEnemyUnit(3, 3, 6, false, 7, 70);
        AddEnemyUnit(3, 4, 7, false, 7, 70);
        AddEnemyUnit(3, 8, 8, false, 7, 70);
        AddEnemyUnit(3, 10, 10, false, 7, 75);
        AddEnemyUnit(1, 14, 28, false, 7, 70);
        AddEnemyUnit(1, 16, 29, false, 7, 70);
        AddEnemyUnit(1, 18, 30, false, 7, 70);
        AddEnemyUnit(1, 20, 31, false, 7, 70);
        AddEnemyUnit(1, 22, 32, false, 7, 70);
        AddEnemyUnit(1, 24, 33, false, 7, 70);

        //710
        AddEnemyUnit(1, 12, 44, true, 8, 85, false);
        AddEnemyUnit(3, 23, 0, false, 8, 75);
        AddEnemyUnit(3, 19, 1, false, 8, 75);
        AddEnemyUnit(3, 4, 4, false, 8, 70);
        AddEnemyUnit(3, 8, 5, false, 8, 70);
        AddEnemyUnit(1, 17, 34, false, 8, 70);
        AddEnemyUnit(1, 10, 40, false, 8, 70);
        AddEnemyUnit(1, 14, 36, false, 8, 70);
        AddEnemyUnit(1, 16, 27, false, 8, 70);
        AddEnemyUnit(1, 21, 26, false, 8, 70);
        AddEnemyUnit(1, 20, 32, false, 8, 70);
        AddEnemyUnit(1, 22, 35, false, 8, 70);
        AddEnemyUnit(1, 2, 38, false, 8, 70);

        //710
        AddEnemyUnit(1, 12, 44, true, 9, 85, false);
        AddEnemyUnit(3, 20, 2, false, 9, 75);
        AddEnemyUnit(3, 16, 3, false, 9, 75);
        AddEnemyUnit(3, 6, 6, false, 9, 70);
        AddEnemyUnit(3, 0, 7, false, 9, 70);
        AddEnemyUnit(1, 8, 38, false, 9, 70);
        AddEnemyUnit(1, 10, 30, false, 9, 70);
        AddEnemyUnit(1, 14, 37, false, 9, 70);
        AddEnemyUnit(1, 23, 29, false, 9, 70);
        AddEnemyUnit(1, 18, 27, false, 9, 70);
        AddEnemyUnit(1, 2, 36, false, 9, 70);
        AddEnemyUnit(1, 22, 33, false, 9, 70);
        AddEnemyUnit(1, 24, 39, false, 9, 70);

        //710
        AddEnemyUnit(1, 12, 44, true, 10, 85, true);
        AddEnemyUnit(3, 23, 1, false, 10, 75);
        AddEnemyUnit(3, 19, 2, false, 10, 75);
        AddEnemyUnit(3, 4, 8, false, 10, 70);
        AddEnemyUnit(3, 8, 5, false, 10, 70);
        AddEnemyUnit(1, 21, 32, false, 10, 70);
        AddEnemyUnit(1, 17, 33, false, 10, 70);
        AddEnemyUnit(1, 14, 36, false, 10, 70);
        AddEnemyUnit(1, 16, 30, false, 10, 70);
        AddEnemyUnit(1, 0, 28, false, 10, 70);
        AddEnemyUnit(1, 20, 34, false, 10, 70);
        AddEnemyUnit(1, 22, 37, false, 10, 70);
        AddEnemyUnit(1, 2, 40, false, 10, 70);

        //670
        AddEnemyUnit(1, 12, 33, true, 11, 75, true);
        AddEnemyUnit(2, 2, 6, false, 11, 60);
        AddEnemyUnit(2, 5, 4, false, 11, 60);
        AddEnemyUnit(1, 3, 34, false, 11, 65);
        AddEnemyUnit(1, 8, 38, false, 11, 65);
        AddEnemyUnit(1, 10, 35, false, 11, 65);
        AddEnemyUnit(1, 14, 37, false, 11, 65);
        AddEnemyUnit(1, 16, 40, false, 11, 65);
        AddEnemyUnit(1, 18, 26, false, 11, 65);
        AddEnemyUnit(1, 20, 28, false, 11, 65);
        AddEnemyUnit(1, 22, 31, false, 11, 65);
        AddEnemyUnit(1, 24, 34, false, 11, 65);

        //670
        AddEnemyUnit(1, 12, 32, true, 12, 80, false);
        AddEnemyUnit(3, 0, 4, false, 12, 70);
        AddEnemyUnit(3, 1, 5, false, 12, 70);
        AddEnemyUnit(3, 3, 6, false, 12, 70);
        AddEnemyUnit(3, 4, 7, false, 12, 70);
        AddEnemyUnit(3, 8, 8, false, 12, 70);
        AddEnemyUnit(3, 10, 10, false, 12, 75);
        AddEnemyUnit(1, 14, 28, false, 12, 70);
        AddEnemyUnit(1, 16, 29, false, 12, 70);
        AddEnemyUnit(1, 18, 30, false, 12, 70);
        AddEnemyUnit(1, 20, 31, false, 12, 70);
        AddEnemyUnit(1, 22, 32, false, 12, 70);
        AddEnemyUnit(1, 24, 33, false, 12, 70);

        //710
        AddEnemyUnit(1, 12, 43, true, 13, 85, false);
        AddEnemyUnit(3, 8, 12, false, 13, 75);
        AddEnemyUnit(3, 23, 11, false, 13, 75);
        AddEnemyUnit(3, 20, 9, false, 13, 75);
        AddEnemyUnit(3, 22, 10, false, 13, 75);
        AddEnemyUnit(2, 14, 6, false, 13, 70);
        AddEnemyUnit(2, 0, 6, false, 13, 70);
        AddEnemyUnit(2, 10, 6, false, 13, 70);
        AddEnemyUnit(2, 6, 6, false, 13, 70);
        AddEnemyUnit(2, 2, 6, false, 13, 70);

        //710
        AddEnemyUnit(1, 12, 43, true, 14, 85, true);
        AddEnemyUnit(3, 8, 12, false, 14, 75);
        AddEnemyUnit(3, 23, 11, false, 14, 75);
        AddEnemyUnit(3, 20, 9, false, 14, 75);
        AddEnemyUnit(3, 22, 10, false, 14, 75);
        AddEnemyUnit(2, 14, 3, false, 14, 70);
        AddEnemyUnit(2, 0, 3, false, 14, 70);
        AddEnemyUnit(2, 10, 3, false, 14, 70);
        AddEnemyUnit(2, 6, 3, false, 14, 70);
        AddEnemyUnit(2, 2, 3, false, 14, 70);


        for (int i = 0; i < 11; i++)
        {
            GameController.Instance.SetPlayerPlatoon(i);
        }
        maxDeployedPlatoon = 10;
    }

    private void Level23Setup()
    {
        Debug.Log("Level 23 Setup");

        Platoon p0 = new Platoon(0);
        enemyPlatoons[0] = p0;

        Platoon p1 = new Platoon(1);
        enemyPlatoons[1] = p1;

        Platoon p2 = new Platoon(2);
        enemyPlatoons[2] = p2;

        Platoon p3 = new Platoon(3);
        enemyPlatoons[3] = p3;

        Platoon p4 = new Platoon(4);
        enemyPlatoons[4] = p4;

        Platoon p5 = new Platoon(5);
        enemyPlatoons[5] = p5;

        Platoon p6 = new Platoon(6);
        enemyPlatoons[6] = p6;

        Platoon p7 = new Platoon(7);
        enemyPlatoons[7] = p7;

        Platoon p8 = new Platoon(8);
        enemyPlatoons[8] = p8;

        Platoon p9 = new Platoon(9);
        enemyPlatoons[9] = p9;

        Platoon p10 = new Platoon(10);
        enemyPlatoons[10] = p10;

        Platoon p11 = new Platoon(11);
        enemyPlatoons[11] = p11;

        Platoon p12 = new Platoon(12);
        enemyPlatoons[12] = p12;

        Platoon p13 = new Platoon(13);
        enemyPlatoons[13] = p13;

        Platoon p14 = new Platoon(14);
        enemyPlatoons[14] = p14;

        Platoon p15 = new Platoon(15);
        enemyPlatoons[15] = p15;

        enemyPlatoonsRemaining = 16;

        //AddEnemyUnit(int charType, int position, int charClass, bool leader, int platoon, int level)


        //865
        AddEnemyUnit(1, 17, 48, true, 0, 100, true);
        AddEnemyUnit(3, 23, 12, false, 0, 100);
        AddEnemyUnit(1, 12, 40, false, 0, 85);
        AddEnemyUnit(1, 0, 41, false, 0, 85);
        AddEnemyUnit(1, 15, 35, false, 0, 85);
        AddEnemyUnit(1, 21, 36, false, 0, 85);
        AddEnemyUnit(1, 20, 37, false, 0, 85);
        AddEnemyUnit(1, 4, 42, false, 0, 85);
        AddEnemyUnit(1, 14, 34, false, 0, 85);
        AddEnemyUnit(1, 8, 33, false, 0, 85);
        AddEnemyUnit(1, 16, 28, false, 0, 85);
        AddEnemyUnit(1, 3, 39, false, 0, 85);

        //670
        AddEnemyUnit(1, 12, 34, true, 1, 80, true);
        AddEnemyUnit(2, 2, 6, false, 1, 70);
        AddEnemyUnit(2, 5, 5, false, 1, 70);
        AddEnemyUnit(1, 3, 38, false, 1, 65);
        AddEnemyUnit(1, 8, 34, false, 1, 65);
        AddEnemyUnit(1, 10, 30, false, 1, 65);
        AddEnemyUnit(1, 14, 26, false, 1, 65);
        AddEnemyUnit(1, 16, 28, false, 1, 65);
        AddEnemyUnit(1, 18, 32, false, 1, 65);
        AddEnemyUnit(1, 20, 36, false, 1, 65);
        AddEnemyUnit(1, 22, 40, false, 1, 65);
        AddEnemyUnit(1, 24, 38, false, 1, 65);

        //670
        AddEnemyUnit(1, 12, 35, true, 2, 80, false);
        AddEnemyUnit(2, 2, 3, false, 2, 70);
        AddEnemyUnit(2, 5, 4, false, 2, 70);
        AddEnemyUnit(1, 3, 40, false, 2, 65);
        AddEnemyUnit(1, 8, 34, false, 2, 65);
        AddEnemyUnit(1, 10, 30, false, 2, 65);
        AddEnemyUnit(1, 14, 26, false, 2, 65);
        AddEnemyUnit(1, 16, 27, false, 2, 65);
        AddEnemyUnit(1, 18, 30, false, 2, 65);
        AddEnemyUnit(1, 20, 33, false, 2, 65);
        AddEnemyUnit(1, 22, 36, false, 2, 65);
        AddEnemyUnit(1, 24, 39, false, 2, 65);

        //670
        AddEnemyUnit(1, 12, 36, true, 3, 80, false);
        AddEnemyUnit(2, 2, 3, false, 3, 70);
        AddEnemyUnit(2, 5, 5, false, 3, 70);
        AddEnemyUnit(1, 3, 34, false, 3, 65);
        AddEnemyUnit(1, 8, 38, false, 3, 65);
        AddEnemyUnit(1, 10, 35, false, 3, 65);
        AddEnemyUnit(1, 14, 32, false, 3, 65);
        AddEnemyUnit(1, 16, 29, false, 3, 65);
        AddEnemyUnit(1, 18, 26, false, 3, 65);
        AddEnemyUnit(1, 20, 28, false, 3, 65);
        AddEnemyUnit(1, 22, 31, false, 3, 65);
        AddEnemyUnit(1, 24, 34, false, 3, 65);

        //670
        AddEnemyUnit(1, 12, 37, true, 4, 85, true);
        AddEnemyUnit(3, 8, 10, false, 4, 75);
        AddEnemyUnit(3, 1, 8, false, 4, 75);
        AddEnemyUnit(3, 3, 7, false, 4, 75);
        AddEnemyUnit(3, 4, 6, false, 4, 75);
        AddEnemyUnit(3, 0, 4, false, 4, 75);
        AddEnemyUnit(3, 10, 5, false, 4, 75);
        AddEnemyUnit(1, 14, 39, false, 4, 70);
        AddEnemyUnit(1, 16, 37, false, 4, 70);
        AddEnemyUnit(1, 18, 35, false, 4, 70);
        AddEnemyUnit(1, 20, 33, false, 4, 70);
        AddEnemyUnit(1, 22, 31, false, 4, 70);
        AddEnemyUnit(1, 24, 29, false, 4, 70);

        //670
        AddEnemyUnit(1, 12, 38, true, 5, 85, true);
        AddEnemyUnit(3, 0, 5, false, 5, 75);
        AddEnemyUnit(3, 1, 4, false, 5, 75);
        AddEnemyUnit(3, 3, 8, false, 5, 75);
        AddEnemyUnit(3, 4, 7, false, 5, 75);
        AddEnemyUnit(3, 8, 10, false, 5, 75);
        AddEnemyUnit(3, 10, 6, false, 5, 75);
        AddEnemyUnit(1, 14, 28, false, 5, 70);
        AddEnemyUnit(1, 16, 30, false, 5, 70);
        AddEnemyUnit(1, 18, 32, false, 5, 70);
        AddEnemyUnit(1, 20, 34, false, 5, 70);
        AddEnemyUnit(1, 22, 36, false, 5, 70);
        AddEnemyUnit(1, 24, 38, false, 5, 70);

        //670
        AddEnemyUnit(1, 12, 39, true, 6, 85, false);
        AddEnemyUnit(3, 0, 6, false, 6, 75);
        AddEnemyUnit(3, 1, 5, false, 6, 75);
        AddEnemyUnit(3, 3, 4, false, 6, 75);
        AddEnemyUnit(3, 4, 8, false, 6, 75);
        AddEnemyUnit(3, 8, 7, false, 6, 75);
        AddEnemyUnit(3, 10, 10, false, 6, 75);
        AddEnemyUnit(1, 14, 39, false, 6, 70);
        AddEnemyUnit(1, 16, 38, false, 6, 70);
        AddEnemyUnit(1, 18, 37, false, 6, 70);
        AddEnemyUnit(1, 20, 36, false, 6, 70);
        AddEnemyUnit(1, 22, 35, false, 6, 70);
        AddEnemyUnit(1, 24, 34, false, 6, 70);

        //670
        AddEnemyUnit(1, 12, 40, true, 7, 85, false);
        AddEnemyUnit(3, 0, 4, false, 7, 75);
        AddEnemyUnit(3, 1, 5, false, 7, 75);
        AddEnemyUnit(3, 3, 6, false, 7, 75);
        AddEnemyUnit(3, 4, 7, false, 7, 75);
        AddEnemyUnit(3, 8, 8, false, 7, 75);
        AddEnemyUnit(3, 10, 10, false, 7, 75);
        AddEnemyUnit(1, 14, 28, false, 7, 70);
        AddEnemyUnit(1, 16, 29, false, 7, 70);
        AddEnemyUnit(1, 18, 30, false, 7, 70);
        AddEnemyUnit(1, 20, 31, false, 7, 70);
        AddEnemyUnit(1, 22, 32, false, 7, 70);
        AddEnemyUnit(1, 24, 33, false, 7, 70);

        //710
        AddEnemyUnit(1, 12, 44, true, 8, 90, true);
        AddEnemyUnit(3, 23, 1, false, 8, 80);
        AddEnemyUnit(3, 19, 2, false, 8, 80);
        AddEnemyUnit(3, 4, 8, false, 8, 80);
        AddEnemyUnit(3, 8, 6, false, 8, 80);
        AddEnemyUnit(1, 17, 34, false, 8, 70);
        AddEnemyUnit(1, 10, 40, false, 8, 70);
        AddEnemyUnit(1, 14, 36, false, 8, 70);
        AddEnemyUnit(1, 16, 27, false, 8, 70);
        AddEnemyUnit(1, 21, 26, false, 8, 70);
        AddEnemyUnit(1, 20, 32, false, 8, 70);
        AddEnemyUnit(1, 22, 35, false, 8, 70);
        AddEnemyUnit(1, 2, 38, false, 8, 70);

        //710
        AddEnemyUnit(1, 12, 44, true, 9, 90, true);
        AddEnemyUnit(3, 20, 2, false, 9, 80);
        AddEnemyUnit(3, 16, 3, false, 9, 80);
        AddEnemyUnit(3, 0, 6, false, 9, 80);
        AddEnemyUnit(3, 6, 7, false, 9, 80);
        AddEnemyUnit(1, 8, 38, false, 9, 70);
        AddEnemyUnit(1, 10, 30, false, 9, 70);
        AddEnemyUnit(1, 14, 37, false, 9, 70);
        AddEnemyUnit(1, 23, 29, false, 9, 70);
        AddEnemyUnit(1, 18, 27, false, 9, 70);
        AddEnemyUnit(1, 2, 36, false, 9, 70);
        AddEnemyUnit(1, 22, 33, false, 9, 70);
        AddEnemyUnit(1, 24, 39, false, 9, 70);

        //710
        AddEnemyUnit(1, 12, 44, true, 10, 90, false);
        AddEnemyUnit(3, 23, 0, false, 10, 80);
        AddEnemyUnit(3, 19, 1, false, 10, 80);
        AddEnemyUnit(3, 4, 4, false, 10, 80);
        AddEnemyUnit(3, 8, 5, false, 10, 80);
        AddEnemyUnit(1, 21, 32, false, 10, 70);
        AddEnemyUnit(1, 17, 33, false, 10, 70);
        AddEnemyUnit(1, 14, 36, false, 10, 70);
        AddEnemyUnit(1, 16, 30, false, 10, 70);
        AddEnemyUnit(1, 0, 28, false, 10, 70);
        AddEnemyUnit(1, 20, 34, false, 10, 70);
        AddEnemyUnit(1, 22, 37, false, 10, 70);
        AddEnemyUnit(1, 2, 40, false, 10, 70);

        //670
        AddEnemyUnit(1, 12, 33, true, 11, 80, false);
        AddEnemyUnit(2, 2, 6, false, 11, 70);
        AddEnemyUnit(2, 5, 4, false, 11, 70);
        AddEnemyUnit(1, 3, 34, false, 11, 65);
        AddEnemyUnit(1, 8, 38, false, 11, 65);
        AddEnemyUnit(1, 10, 35, false, 11, 65);
        AddEnemyUnit(1, 14, 37, false, 11, 65);
        AddEnemyUnit(1, 16, 40, false, 11, 65);
        AddEnemyUnit(1, 18, 26, false, 11, 65);
        AddEnemyUnit(1, 20, 28, false, 11, 65);
        AddEnemyUnit(1, 22, 31, false, 11, 65);
        AddEnemyUnit(1, 24, 34, false, 11, 65);

        //670
        AddEnemyUnit(1, 12, 32, true, 12, 85, true);
        AddEnemyUnit(3, 0, 4, false, 12, 75);
        AddEnemyUnit(3, 1, 5, false, 12, 75);
        AddEnemyUnit(3, 3, 6, false, 12, 75);
        AddEnemyUnit(3, 4, 7, false, 12, 75);
        AddEnemyUnit(3, 8, 8, false, 12, 75);
        AddEnemyUnit(3, 10, 10, false, 12, 75);
        AddEnemyUnit(1, 14, 28, false, 12, 70);
        AddEnemyUnit(1, 16, 29, false, 12, 70);
        AddEnemyUnit(1, 18, 30, false, 12, 70);
        AddEnemyUnit(1, 20, 31, false, 12, 70);
        AddEnemyUnit(1, 22, 32, false, 12, 70);
        AddEnemyUnit(1, 24, 33, false, 12, 70);

        //710
        AddEnemyUnit(1, 12, 43, true, 13, 90, true);
        AddEnemyUnit(3, 8, 12, false, 13, 75);
        AddEnemyUnit(3, 23, 11, false, 13, 75);
        AddEnemyUnit(3, 20, 9, false, 13, 75);
        AddEnemyUnit(3, 22, 10, false, 13, 75);
        AddEnemyUnit(2, 14, 6, false, 13, 75);
        AddEnemyUnit(2, 0, 6, false, 13, 75);
        AddEnemyUnit(2, 10, 6, false, 13, 75);
        AddEnemyUnit(2, 6, 6, false, 13, 75);
        AddEnemyUnit(2, 2, 6, false, 13, 75);

        //710
        AddEnemyUnit(1, 12, 43, true, 14, 90, false);
        AddEnemyUnit(3, 8, 12, false, 14, 75);
        AddEnemyUnit(3, 23, 11, false, 14, 75);
        AddEnemyUnit(3, 20, 9, false, 14, 75);
        AddEnemyUnit(3, 22, 10, false, 14, 75);
        AddEnemyUnit(2, 14, 3, false, 14, 75);
        AddEnemyUnit(2, 0, 3, false, 14, 75);
        AddEnemyUnit(2, 10, 3, false, 14, 75);
        AddEnemyUnit(2, 6, 3, false, 14, 75);
        AddEnemyUnit(2, 2, 3, false, 14, 75);

        //710
        AddEnemyUnit(1, 12, 56, true, 15, 90, false);
        AddEnemyUnit(3, 8, 12, false, 15, 75);
        AddEnemyUnit(3, 23, 11, false, 15, 75);
        AddEnemyUnit(3, 20, 9, false, 15, 75);
        AddEnemyUnit(3, 22, 3, false, 15, 75);
        AddEnemyUnit(2, 14, 3, false, 15, 75);
        AddEnemyUnit(2, 0, 3, false, 15, 75);
        AddEnemyUnit(2, 10, 6, false, 15, 75);
        AddEnemyUnit(2, 6, 6, false, 15, 75);
        AddEnemyUnit(2, 2, 6, false, 15, 75);


        for (int i = 0; i < 11; i++)
        {
            GameController.Instance.SetPlayerPlatoon(i);
        }
        maxDeployedPlatoon = 10;
    }

    private void Level24Setup()
    {
        Debug.Log("Level 24 Setup");

        Platoon p0 = new Platoon(0);
        enemyPlatoons[0] = p0;

        Platoon p1 = new Platoon(1);
        enemyPlatoons[1] = p1;

        Platoon p2 = new Platoon(2);
        enemyPlatoons[2] = p2;

        Platoon p3 = new Platoon(3);
        enemyPlatoons[3] = p3;

        Platoon p4 = new Platoon(4);
        enemyPlatoons[4] = p4;

        Platoon p5 = new Platoon(5);
        enemyPlatoons[5] = p5;

        Platoon p6 = new Platoon(6);
        enemyPlatoons[6] = p6;

        Platoon p7 = new Platoon(7);
        enemyPlatoons[7] = p7;

        Platoon p8 = new Platoon(8);
        enemyPlatoons[8] = p8;

        Platoon p9 = new Platoon(9);
        enemyPlatoons[9] = p9;

        Platoon p10 = new Platoon(10);
        enemyPlatoons[10] = p10;

        Platoon p11 = new Platoon(11);
        enemyPlatoons[11] = p11;

        Platoon p12 = new Platoon(12);
        enemyPlatoons[12] = p12;

        Platoon p13 = new Platoon(13);
        enemyPlatoons[13] = p13;

        enemyPlatoonsRemaining = 14;

        //AddEnemyUnit(int charType, int position, int charClass, bool leader, int platoon, int level)


        //1205
        AddEnemyUnit(1, 17, 49, true, 0, 105, true);
        AddEnemyUnit(3, 23, 11, false, 0, 100);
        AddEnemyUnit(2, 5, 8, false, 0, 85);
        AddEnemyUnit(2, 2, 3, false, 0, 85);
        AddEnemyUnit(2, 15, 6, false, 0, 85);
        AddEnemyUnit(3, 21, 4, false, 0, 100);
        AddEnemyUnit(3, 20, 7, false, 0, 100);
        AddEnemyUnit(1, 14, 34, false, 0, 90);
        AddEnemyUnit(1, 8, 33, false, 0, 90);
        AddEnemyUnit(1, 16, 28, false, 0, 90);
        AddEnemyUnit(1, 3, 39, false, 0, 90);

        //960
        AddEnemyUnit(1, 12, 34, true, 1, 90, true);
        AddEnemyUnit(3, 2, 4, false, 1, 75);
        AddEnemyUnit(3, 5, 4, false, 1, 75);
        AddEnemyUnit(3, 3, 4, false, 1, 75);
        AddEnemyUnit(3, 8, 4, false, 1, 75);
        AddEnemyUnit(3, 10, 4, false, 1, 75);
        AddEnemyUnit(3, 6, 5, false, 1, 75);
        AddEnemyUnit(1, 14, 31, false, 1, 75);
        AddEnemyUnit(1, 16, 28, false, 1, 70);
        AddEnemyUnit(1, 18, 32, false, 1, 70);
        AddEnemyUnit(1, 20, 36, false, 1, 70);
        AddEnemyUnit(1, 22, 40, false, 1, 70);
        AddEnemyUnit(1, 24, 38, false, 1, 70);

        //960
        AddEnemyUnit(1, 12, 35, true, 2, 90, false);
        AddEnemyUnit(3, 2, 5, false, 2, 75);
        AddEnemyUnit(3, 5, 5, false, 2, 75);
        AddEnemyUnit(3, 3, 5, false, 2, 75);
        AddEnemyUnit(3, 8, 5, false, 2, 75);
        AddEnemyUnit(3, 10, 5, false, 2, 75);
        AddEnemyUnit(3, 6, 5, false, 2, 75);
        AddEnemyUnit(1, 14, 26, false, 2, 70);
        AddEnemyUnit(1, 16, 27, false, 2, 70);
        AddEnemyUnit(1, 18, 30, false, 2, 70);
        AddEnemyUnit(1, 20, 33, false, 2, 70);
        AddEnemyUnit(1, 22, 36, false, 2, 70);
        AddEnemyUnit(1, 24, 39, false, 2, 70);

        //960
        AddEnemyUnit(1, 12, 36, true, 3, 90, false);
        AddEnemyUnit(3, 2, 6, false, 3, 75);
        AddEnemyUnit(3, 5, 6, false, 3, 75);
        AddEnemyUnit(3, 3, 6, false, 3, 75);
        AddEnemyUnit(3, 8, 6, false, 3, 75);
        AddEnemyUnit(3, 10, 6, false, 3, 75);
        AddEnemyUnit(3, 6, 6, false, 3, 75);
        AddEnemyUnit(1, 14, 32, false, 3, 70);
        AddEnemyUnit(1, 16, 29, false, 3, 70);
        AddEnemyUnit(1, 18, 26, false, 3, 70);
        AddEnemyUnit(1, 20, 28, false, 3, 70);
        AddEnemyUnit(1, 22, 31, false, 3, 70);
        AddEnemyUnit(1, 24, 34, false, 3, 70);

        //960
        AddEnemyUnit(1, 12, 37, true, 4, 90, true);
        AddEnemyUnit(3, 2, 7, false, 4, 75);
        AddEnemyUnit(3, 5, 7, false, 4, 75);
        AddEnemyUnit(3, 3, 7, false, 4, 75);
        AddEnemyUnit(3, 8, 7, false, 4, 75);
        AddEnemyUnit(3, 10, 7, false, 4, 75);
        AddEnemyUnit(3, 6, 7, false, 4, 75);
        AddEnemyUnit(1, 14, 39, false, 4, 70);
        AddEnemyUnit(1, 16, 37, false, 4, 70);
        AddEnemyUnit(1, 18, 35, false, 4, 70);
        AddEnemyUnit(1, 20, 33, false, 4, 70);
        AddEnemyUnit(1, 22, 31, false, 4, 70);
        AddEnemyUnit(1, 24, 29, false, 4, 70);

        //960
        AddEnemyUnit(1, 12, 38, true, 5, 90, true);
        AddEnemyUnit(3, 2, 8, false, 5, 75);
        AddEnemyUnit(3, 5, 8, false, 5, 75);
        AddEnemyUnit(3, 3, 8, false, 5, 75);
        AddEnemyUnit(3, 8, 8, false, 5, 75);
        AddEnemyUnit(3, 10, 8, false, 5, 75);
        AddEnemyUnit(3, 6, 8, false, 5, 75);
        AddEnemyUnit(1, 14, 28, false, 5, 70);
        AddEnemyUnit(1, 16, 30, false, 5, 70);
        AddEnemyUnit(1, 18, 32, false, 5, 70);
        AddEnemyUnit(1, 20, 34, false, 5, 70);
        AddEnemyUnit(1, 22, 36, false, 5, 70);
        AddEnemyUnit(1, 24, 38, false, 5, 70);

        //1000
        AddEnemyUnit(1, 12, 39, true, 6, 95, false);
        AddEnemyUnit(2, 5, 7, false, 6, 75);
        AddEnemyUnit(2, 8, 7, false, 6, 75);
        AddEnemyUnit(2, 18, 7, false, 6, 75);
        AddEnemyUnit(1, 2, 39, false, 6, 80);
        AddEnemyUnit(1, 7, 38, false, 6, 80);
        AddEnemyUnit(1, 22, 37, false, 6, 80);
        AddEnemyUnit(1, 23, 36, false, 6, 80);
        AddEnemyUnit(1, 15, 35, false, 6, 80);
        AddEnemyUnit(1, 16, 34, false, 6, 80);

        //1000
        AddEnemyUnit(1, 12, 40, true, 7, 95, false);
        AddEnemyUnit(2, 5, 8, false, 7, 75);
        AddEnemyUnit(2, 8, 8, false, 7, 75);
        AddEnemyUnit(2, 18, 8, false, 7, 75);
        AddEnemyUnit(1, 2, 28, false, 7, 80);
        AddEnemyUnit(1, 7, 29, false, 7, 80);
        AddEnemyUnit(1, 15, 30, false, 7, 80);
        AddEnemyUnit(1, 16, 31, false, 7, 80);
        AddEnemyUnit(1, 22, 32, false, 7, 80);
        AddEnemyUnit(1, 23, 33, false, 7, 80);

        //1000
        AddEnemyUnit(1, 12, 44, true, 8, 95, true);
        AddEnemyUnit(2, 5, 9, false, 8, 75);
        AddEnemyUnit(2, 8, 9, false, 8, 75);
        AddEnemyUnit(2, 18, 9, false, 8, 75);
        AddEnemyUnit(1, 2, 36, false, 8, 80);
        AddEnemyUnit(1, 7, 27, false, 8, 80);
        AddEnemyUnit(1, 15, 26, false, 8, 80);
        AddEnemyUnit(1, 16, 32, false, 8, 80);
        AddEnemyUnit(1, 22, 35, false, 8, 80);
        AddEnemyUnit(1, 23, 38, false, 8, 80);

        //1000
        AddEnemyUnit(1, 12, 44, true, 9, 95, true);
        AddEnemyUnit(3, 5, 9, false, 9, 80);
        AddEnemyUnit(3, 8, 9, false, 9, 80);
        AddEnemyUnit(3, 18, 9, false, 9, 80);
        AddEnemyUnit(1, 2, 37, false, 9, 75);
        AddEnemyUnit(1, 7, 29, false, 9, 75);
        AddEnemyUnit(1, 15, 27, false, 9, 75);
        AddEnemyUnit(1, 16, 36, false, 9, 75);
        AddEnemyUnit(1, 22, 33, false, 9, 75);
        AddEnemyUnit(1, 23, 39, false, 9, 75);

        //1000
        AddEnemyUnit(1, 12, 44, true, 10, 95, false);
        AddEnemyUnit(3, 5, 11, false, 10, 80);
        AddEnemyUnit(3, 8, 11, false, 10, 80);
        AddEnemyUnit(3, 18, 11, false, 10, 80);
        AddEnemyUnit(1, 2, 36, false, 10, 75);
        AddEnemyUnit(1, 7, 30, false, 10, 75);
        AddEnemyUnit(1, 15, 28, false, 10, 75);
        AddEnemyUnit(1, 16, 34, false, 10, 75);
        AddEnemyUnit(1, 22, 37, false, 10, 75);
        AddEnemyUnit(1, 23, 40, false, 10, 75);

        //1000
        AddEnemyUnit(1, 12, 33, true, 11, 95, false);
        AddEnemyUnit(3, 8, 12, false, 11, 80);
        AddEnemyUnit(3, 5, 12, false, 11, 80);
        AddEnemyUnit(3, 18, 12, false, 11, 80);
        AddEnemyUnit(1, 2, 37, false, 11, 75);
        AddEnemyUnit(1, 7, 40, false, 11, 75);
        AddEnemyUnit(1, 15, 26, false, 11, 75);
        AddEnemyUnit(1, 16, 28, false, 11, 75);
        AddEnemyUnit(1, 22, 31, false, 11, 75);
        AddEnemyUnit(1, 23, 34, false, 11, 75);

        //670
        AddEnemyUnit(1, 12, 32, true, 12, 85, true);
        AddEnemyUnit(2, 0, 3, false, 12, 70);
        AddEnemyUnit(2, 1, 3, false, 12, 70);
        AddEnemyUnit(2, 3, 3, false, 12, 70);
        AddEnemyUnit(2, 4, 3, false, 12, 70);
        AddEnemyUnit(2, 8, 3, false, 12, 70);
        AddEnemyUnit(2, 10, 3, false, 12, 70);
        AddEnemyUnit(1, 14, 28, false, 12, 70);
        AddEnemyUnit(1, 16, 29, false, 12, 70);
        AddEnemyUnit(1, 18, 30, false, 12, 70);
        AddEnemyUnit(1, 20, 31, false, 12, 70);
        AddEnemyUnit(1, 22, 32, false, 12, 70);
        AddEnemyUnit(1, 24, 33, false, 12, 70);

        //710
        AddEnemyUnit(1, 12, 32, true, 13, 85, true);
        AddEnemyUnit(2, 0, 6, false, 13, 70);
        AddEnemyUnit(2, 1, 6, false, 13, 70);
        AddEnemyUnit(2, 3, 6, false, 13, 70);
        AddEnemyUnit(2, 4, 6, false, 13, 70);
        AddEnemyUnit(2, 8, 6, false, 13, 70);
        AddEnemyUnit(2, 10, 6, false, 13, 70);
        AddEnemyUnit(1, 14, 34, false, 13, 70);
        AddEnemyUnit(1, 16, 35, false, 13, 70);
        AddEnemyUnit(1, 18, 36, false, 13, 70);
        AddEnemyUnit(1, 20, 37, false, 13, 70);
        AddEnemyUnit(1, 22, 38, false, 13, 70);
        AddEnemyUnit(1, 24, 39, false, 13, 70);


        for (int i = 0; i < 11; i++)
        {
            GameController.Instance.SetPlayerPlatoon(i);
        }
        maxDeployedPlatoon = 10;
    }

    private void Level25Setup()
    {
        Debug.Log("Level 25 Setup");

        Platoon p0 = new Platoon(0);
        enemyPlatoons[0] = p0;

        Platoon p1 = new Platoon(1);
        enemyPlatoons[1] = p1;

        Platoon p2 = new Platoon(2);
        enemyPlatoons[2] = p2;

        Platoon p3 = new Platoon(3);
        enemyPlatoons[3] = p3;

        Platoon p4 = new Platoon(4);
        enemyPlatoons[4] = p4;

        Platoon p5 = new Platoon(5);
        enemyPlatoons[5] = p5;

        Platoon p6 = new Platoon(6);
        enemyPlatoons[6] = p6;

        Platoon p7 = new Platoon(7);
        enemyPlatoons[7] = p7;

        Platoon p8 = new Platoon(8);
        enemyPlatoons[8] = p8;

        Platoon p9 = new Platoon(9);
        enemyPlatoons[9] = p9;

        Platoon p10 = new Platoon(10);
        enemyPlatoons[10] = p10;

        Platoon p11 = new Platoon(11);
        enemyPlatoons[11] = p11;

        Platoon p12 = new Platoon(12);
        enemyPlatoons[12] = p12;

        enemyPlatoonsRemaining = 13;

        //AddEnemyUnit(int charType, int position, int charClass, bool leader, int platoon, int level)


        //1205
        AddEnemyUnit(1, 17, 50, true, 0, 110, true);
        AddEnemyUnit(3, 23, 11, false, 0, 100);
        AddEnemyUnit(2, 5, 8, false, 0, 85);
        AddEnemyUnit(2, 2, 3, false, 0, 95);
        AddEnemyUnit(2, 15, 6, false, 0, 95);
        AddEnemyUnit(3, 21, 4, false, 0, 100);
        AddEnemyUnit(3, 20, 7, false, 0, 100);
        AddEnemyUnit(1, 14, 34, false, 0, 100);
        AddEnemyUnit(1, 8, 33, false, 0, 100);
        AddEnemyUnit(1, 16, 28, false, 0, 100);
        AddEnemyUnit(1, 3, 39, false, 0, 100);

        //960
        AddEnemyUnit(1, 12, 34, true, 1, 90, true);
        AddEnemyUnit(3, 2, 4, false, 1, 75);
        AddEnemyUnit(3, 5, 4, false, 1, 75);
        AddEnemyUnit(3, 3, 4, false, 1, 75);
        AddEnemyUnit(3, 8, 4, false, 1, 75);
        AddEnemyUnit(3, 10, 4, false, 1, 75);
        AddEnemyUnit(3, 6, 5, false, 1, 75);
        AddEnemyUnit(1, 14, 31, false, 1, 75);
        AddEnemyUnit(1, 16, 28, false, 1, 75);
        AddEnemyUnit(1, 18, 32, false, 1, 75);
        AddEnemyUnit(1, 20, 36, false, 1, 75);
        AddEnemyUnit(1, 22, 40, false, 1, 75);
        AddEnemyUnit(1, 24, 38, false, 1, 75);

        //960
        AddEnemyUnit(1, 12, 35, true, 2, 90, false);
        AddEnemyUnit(3, 2, 5, false, 2, 75);
        AddEnemyUnit(3, 5, 5, false, 2, 75);
        AddEnemyUnit(3, 3, 5, false, 2, 75);
        AddEnemyUnit(3, 8, 5, false, 2, 75);
        AddEnemyUnit(3, 10, 5, false, 2, 75);
        AddEnemyUnit(3, 6, 5, false, 2, 75);
        AddEnemyUnit(1, 14, 26, false, 2, 75);
        AddEnemyUnit(1, 16, 27, false, 2, 75);
        AddEnemyUnit(1, 18, 30, false, 2, 75);
        AddEnemyUnit(1, 20, 33, false, 2, 75);
        AddEnemyUnit(1, 22, 36, false, 2, 75);
        AddEnemyUnit(1, 24, 39, false, 2, 75);

        //960
        AddEnemyUnit(1, 12, 36, true, 3, 90, false);
        AddEnemyUnit(3, 2, 6, false, 3, 75);
        AddEnemyUnit(3, 5, 6, false, 3, 75);
        AddEnemyUnit(3, 3, 6, false, 3, 75);
        AddEnemyUnit(3, 8, 6, false, 3, 75);
        AddEnemyUnit(3, 10, 6, false, 3, 75);
        AddEnemyUnit(3, 6, 6, false, 3, 75);
        AddEnemyUnit(1, 14, 32, false, 3, 75);
        AddEnemyUnit(1, 16, 29, false, 3, 75);
        AddEnemyUnit(1, 18, 26, false, 3, 75);
        AddEnemyUnit(1, 20, 28, false, 3, 75);
        AddEnemyUnit(1, 22, 31, false, 3, 75);
        AddEnemyUnit(1, 24, 34, false, 3, 75);

        //960
        AddEnemyUnit(1, 12, 37, true, 4, 90, true);
        AddEnemyUnit(3, 2, 7, false, 4, 75);
        AddEnemyUnit(3, 5, 7, false, 4, 75);
        AddEnemyUnit(3, 3, 7, false, 4, 75);
        AddEnemyUnit(3, 8, 7, false, 4, 75);
        AddEnemyUnit(3, 10, 7, false, 4, 75);
        AddEnemyUnit(3, 6, 7, false, 4, 75);
        AddEnemyUnit(1, 14, 39, false, 4, 75);
        AddEnemyUnit(1, 16, 37, false, 4, 75);
        AddEnemyUnit(1, 18, 35, false, 4, 75);
        AddEnemyUnit(1, 20, 33, false, 4, 75);
        AddEnemyUnit(1, 22, 31, false, 4, 75);
        AddEnemyUnit(1, 24, 29, false, 4, 75);

        //960
        AddEnemyUnit(1, 12, 38, true, 5, 90, true);
        AddEnemyUnit(3, 2, 8, false, 5, 75);
        AddEnemyUnit(3, 5, 8, false, 5, 75);
        AddEnemyUnit(3, 3, 8, false, 5, 75);
        AddEnemyUnit(3, 8, 8, false, 5, 75);
        AddEnemyUnit(3, 10, 8, false, 5, 75);
        AddEnemyUnit(3, 6, 8, false, 5, 75);
        AddEnemyUnit(1, 14, 28, false, 5, 75);
        AddEnemyUnit(1, 16, 30, false, 5, 75);
        AddEnemyUnit(1, 18, 32, false, 5, 75);
        AddEnemyUnit(1, 20, 34, false, 5, 75);
        AddEnemyUnit(1, 22, 36, false, 5, 75);
        AddEnemyUnit(1, 24, 38, false, 5, 75);

        //1000
        AddEnemyUnit(1, 12, 39, true, 6, 95, false);
        AddEnemyUnit(2, 5, 7, false, 6, 75);
        AddEnemyUnit(2, 8, 7, false, 6, 75);
        AddEnemyUnit(2, 18, 7, false, 6, 75);
        AddEnemyUnit(1, 2, 39, false, 6, 85);
        AddEnemyUnit(1, 7, 38, false, 6, 85);
        AddEnemyUnit(1, 22, 37, false, 6, 85);
        AddEnemyUnit(1, 23, 36, false, 6, 85);
        AddEnemyUnit(1, 15, 35, false, 6, 85);
        AddEnemyUnit(1, 16, 34, false, 6, 85);

        //1000
        AddEnemyUnit(1, 12, 40, true, 7, 95, false);
        AddEnemyUnit(2, 5, 8, false, 7, 75);
        AddEnemyUnit(2, 8, 8, false, 7, 75);
        AddEnemyUnit(2, 18, 8, false, 7, 75);
        AddEnemyUnit(1, 2, 28, false, 7, 85);
        AddEnemyUnit(1, 7, 29, false, 7, 85);
        AddEnemyUnit(1, 15, 30, false, 7, 85);
        AddEnemyUnit(1, 16, 31, false, 7, 85);
        AddEnemyUnit(1, 22, 32, false, 7, 85);
        AddEnemyUnit(1, 23, 33, false, 7, 85);

        //1000
        AddEnemyUnit(1, 12, 44, true, 8, 95, true);
        AddEnemyUnit(2, 5, 9, false, 8, 75);
        AddEnemyUnit(2, 8, 9, false, 8, 75);
        AddEnemyUnit(2, 18, 9, false, 8, 75);
        AddEnemyUnit(1, 2, 36, false, 8, 85);
        AddEnemyUnit(1, 7, 27, false, 8, 85);
        AddEnemyUnit(1, 15, 26, false, 8, 85);
        AddEnemyUnit(1, 16, 32, false, 8, 85);
        AddEnemyUnit(1, 22, 35, false, 8, 85);
        AddEnemyUnit(1, 23, 38, false, 8, 85);

        //1000
        AddEnemyUnit(1, 12, 44, true, 9, 100, true);
        AddEnemyUnit(3, 5, 9, false, 9, 85);
        AddEnemyUnit(3, 8, 9, false, 9, 85);
        AddEnemyUnit(3, 18, 9, false, 9, 85);
        AddEnemyUnit(1, 2, 37, false, 9, 75);
        AddEnemyUnit(1, 7, 29, false, 9, 75);
        AddEnemyUnit(1, 15, 27, false, 9, 75);
        AddEnemyUnit(1, 16, 36, false, 9, 75);
        AddEnemyUnit(1, 22, 33, false, 9, 75);
        AddEnemyUnit(1, 23, 39, false, 9, 75);

        //1000
        AddEnemyUnit(1, 12, 44, true, 10, 100, false);
        AddEnemyUnit(3, 5, 11, false, 10, 85);
        AddEnemyUnit(3, 8, 11, false, 10, 85);
        AddEnemyUnit(3, 18, 11, false, 10, 85);
        AddEnemyUnit(1, 2, 36, false, 10, 75);
        AddEnemyUnit(1, 7, 30, false, 10, 75);
        AddEnemyUnit(1, 15, 28, false, 10, 75);
        AddEnemyUnit(1, 16, 34, false, 10, 75);
        AddEnemyUnit(1, 22, 37, false, 10, 75);
        AddEnemyUnit(1, 23, 40, false, 10, 75);

        //1000
        AddEnemyUnit(1, 12, 33, true, 11, 100, false);
        AddEnemyUnit(3, 8, 12, false, 11, 85);
        AddEnemyUnit(3, 5, 12, false, 11, 85);
        AddEnemyUnit(3, 18, 12, false, 11, 85);
        AddEnemyUnit(1, 2, 37, false, 11, 75);
        AddEnemyUnit(1, 7, 40, false, 11, 75);
        AddEnemyUnit(1, 15, 26, false, 11, 75);
        AddEnemyUnit(1, 16, 28, false, 11, 75);
        AddEnemyUnit(1, 22, 31, false, 11, 75);
        AddEnemyUnit(1, 23, 34, false, 11, 75);

        //670
        AddEnemyUnit(1, 12, 32, true, 12, 90, true);
        AddEnemyUnit(2, 5, 4, false, 12, 70);
        AddEnemyUnit(2, 8, 4, false, 12, 70);
        AddEnemyUnit(2, 15, 5, false, 12, 70);
        AddEnemyUnit(1, 14, 28, false, 12, 75);
        AddEnemyUnit(1, 2, 29, false, 12, 75);
        AddEnemyUnit(1, 18, 30, false, 12, 75);
        AddEnemyUnit(1, 20, 31, false, 12, 75);
        AddEnemyUnit(1, 22, 32, false, 12, 75);
        AddEnemyUnit(1, 24, 33, false, 12, 75);


        for (int i = 0; i < 11; i++)
        {
            GameController.Instance.SetPlayerPlatoon(i);
        }
        maxDeployedPlatoon = 10;
    }

    private void Level26Setup()
    {
        Debug.Log("Level 26 Setup");

        Platoon p0 = new Platoon(0);
        enemyPlatoons[0] = p0;

        Platoon p1 = new Platoon(1);
        enemyPlatoons[1] = p1;

        Platoon p2 = new Platoon(2);
        enemyPlatoons[2] = p2;

        Platoon p3 = new Platoon(3);
        enemyPlatoons[3] = p3;

        Platoon p4 = new Platoon(4);
        enemyPlatoons[4] = p4;

        Platoon p5 = new Platoon(5);
        enemyPlatoons[5] = p5;

        Platoon p6 = new Platoon(6);
        enemyPlatoons[6] = p6;

        Platoon p7 = new Platoon(7);
        enemyPlatoons[7] = p7;

        Platoon p8 = new Platoon(8);
        enemyPlatoons[8] = p8;

        Platoon p9 = new Platoon(9);
        enemyPlatoons[9] = p9;

        Platoon p10 = new Platoon(10);
        enemyPlatoons[10] = p10;

        Platoon p11 = new Platoon(11);
        enemyPlatoons[11] = p11;

        Platoon p12 = new Platoon(12);
        enemyPlatoons[12] = p12;

        Platoon p13 = new Platoon(13);
        enemyPlatoons[13] = p13;

        Platoon p14 = new Platoon(14);
        enemyPlatoons[14] = p14;

        enemyPlatoonsRemaining = 15;

        //AddEnemyUnit(int charType, int position, int charClass, bool leader, int platoon, int level)


        //1205
        AddEnemyUnit(1, 12, 54, true, 0, 115, true);
        AddEnemyUnit(1, 0, 38, false, 0, 103);
        AddEnemyUnit(1, 2, 39, false, 0, 103);
        AddEnemyUnit(1, 4, 40, false, 0, 103);
        AddEnemyUnit(1, 6, 28, false, 0, 103);
        AddEnemyUnit(1, 8, 29, false, 0, 103);
        AddEnemyUnit(1, 10, 35, false, 0, 103);
        AddEnemyUnit(1, 14, 36, false, 0, 103);
        AddEnemyUnit(1, 16, 30, false, 0, 103);
        AddEnemyUnit(1, 18, 31, false, 0, 103);
        AddEnemyUnit(1, 20, 32, false, 0, 103);
        AddEnemyUnit(1, 22, 33, false, 0, 103);
        AddEnemyUnit(1, 24, 34, false, 0, 103);


        //960
        AddEnemyUnit(1, 12, 45, true, 1, 95, true);
        AddEnemyUnit(1, 0, 27, false, 1, 78);
        AddEnemyUnit(1, 2, 27, false, 1, 78);
        AddEnemyUnit(1, 4, 27, false, 1, 78);
        AddEnemyUnit(1, 6, 27, false, 1, 78);
        AddEnemyUnit(1, 8, 27, false, 1, 78);
        AddEnemyUnit(1, 10, 27, false, 1, 78);
        AddEnemyUnit(1, 14, 27, false, 1, 78);
        AddEnemyUnit(1, 16, 27, false, 1, 78);
        AddEnemyUnit(1, 18, 27, false, 1, 78);
        AddEnemyUnit(1, 20, 27, false, 1, 78);
        AddEnemyUnit(1, 22, 27, false, 1, 78);
        AddEnemyUnit(1, 24, 27, false, 1, 78);

        //960
        AddEnemyUnit(1, 12, 47, true, 2, 95, true);
        AddEnemyUnit(1, 0, 35, false, 2, 78);
        AddEnemyUnit(1, 2, 35, false, 2, 78);
        AddEnemyUnit(1, 4, 35, false, 2, 78);
        AddEnemyUnit(1, 6, 35, false, 2, 78);
        AddEnemyUnit(1, 8, 35, false, 2, 78);
        AddEnemyUnit(1, 10, 35, false, 2, 78);
        AddEnemyUnit(1, 14, 35, false, 2, 78);
        AddEnemyUnit(1, 16, 35, false, 2, 78);
        AddEnemyUnit(1, 18, 35, false, 2, 78);
        AddEnemyUnit(1, 20, 35, false, 2, 78);
        AddEnemyUnit(1, 22, 35, false, 2, 78);
        AddEnemyUnit(1, 24, 35, false, 2, 78);

        //960
        AddEnemyUnit(1, 12, 50, true, 3, 95, true);
        AddEnemyUnit(1, 0, 38, false, 3, 78);
        AddEnemyUnit(1, 2, 38, false, 3, 78);
        AddEnemyUnit(1, 4, 38, false, 3, 78);
        AddEnemyUnit(1, 6, 38, false, 3, 78);
        AddEnemyUnit(1, 8, 38, false, 3, 78);
        AddEnemyUnit(1, 10, 38, false, 3, 78);
        AddEnemyUnit(1, 14, 38, false, 3, 78);
        AddEnemyUnit(1, 16, 38, false, 3, 78);
        AddEnemyUnit(1, 18, 38, false, 3, 78);
        AddEnemyUnit(1, 20, 38, false, 3, 78);
        AddEnemyUnit(1, 22, 38, false, 3, 78);
        AddEnemyUnit(1, 24, 38, false, 3, 78);

        //960
        AddEnemyUnit(1, 12, 46, true, 4, 95, true);
        AddEnemyUnit(1, 0, 32, false, 4, 78);
        AddEnemyUnit(1, 2, 32, false, 4, 78);
        AddEnemyUnit(1, 4, 32, false, 4, 78);
        AddEnemyUnit(1, 6, 32, false, 4, 78);
        AddEnemyUnit(1, 8, 32, false, 4, 78);
        AddEnemyUnit(1, 10, 32, false, 4, 78);
        AddEnemyUnit(1, 14, 32, false, 4, 78);
        AddEnemyUnit(1, 16, 32, false, 4, 78);
        AddEnemyUnit(1, 18, 32, false, 4, 78);
        AddEnemyUnit(1, 20, 32, false, 4, 78);
        AddEnemyUnit(1, 22, 32, false, 4, 78);
        AddEnemyUnit(1, 24, 32, false, 4, 78);

        //960
        AddEnemyUnit(1, 12, 48, true, 5, 95, true);
        AddEnemyUnit(1, 0, 37, false, 5, 78);
        AddEnemyUnit(1, 2, 37, false, 5, 78);
        AddEnemyUnit(1, 4, 37, false, 5, 78);
        AddEnemyUnit(1, 6, 37, false, 5, 78);
        AddEnemyUnit(1, 8, 37, false, 5, 78);
        AddEnemyUnit(1, 10, 37, false, 5, 78);
        AddEnemyUnit(1, 14, 37, false, 5, 78);
        AddEnemyUnit(1, 16, 37, false, 5, 78);
        AddEnemyUnit(1, 18, 37, false, 5, 78);
        AddEnemyUnit(1, 20, 37, false, 5, 78);
        AddEnemyUnit(1, 22, 37, false, 5, 78);
        AddEnemyUnit(1, 24, 37, false, 5, 78);

        //960
        AddEnemyUnit(1, 12, 49, true, 6, 95, true);
        AddEnemyUnit(1, 0, 41, false, 6, 78);
        AddEnemyUnit(1, 2, 41, false, 6, 78);
        AddEnemyUnit(1, 4, 41, false, 6, 78);
        AddEnemyUnit(1, 6, 41, false, 6, 78);
        AddEnemyUnit(1, 8, 41, false, 6, 78);
        AddEnemyUnit(1, 10, 41, false, 6, 78);
        AddEnemyUnit(1, 14, 41, false, 6, 78);
        AddEnemyUnit(1, 16, 41, false, 6, 78);
        AddEnemyUnit(1, 18, 41, false, 6, 78);
        AddEnemyUnit(1, 20, 41, false, 6, 78);
        AddEnemyUnit(1, 22, 41, false, 6, 78);
        AddEnemyUnit(1, 24, 41, false, 6, 78);

        //1000
        AddEnemyUnit(1, 12, 26, true, 7, 100, false);
        AddEnemyUnit(1, 0, 38, false, 7, 82);
        AddEnemyUnit(1, 2, 39, false, 7, 82);
        AddEnemyUnit(1, 4, 40, false, 7, 82);
        AddEnemyUnit(1, 6, 28, false, 7, 82);
        AddEnemyUnit(1, 8, 29, false, 7, 82);
        AddEnemyUnit(1, 10, 35, false, 7, 82);
        AddEnemyUnit(1, 14, 36, false, 7, 82);
        AddEnemyUnit(1, 16, 30, false, 7, 82);
        AddEnemyUnit(1, 18, 31, false, 7, 82);
        AddEnemyUnit(1, 20, 32, false, 7, 82);
        AddEnemyUnit(1, 22, 33, false, 7, 82);
        AddEnemyUnit(1, 24, 34, false, 7, 82);

        //1000
        AddEnemyUnit(1, 12, 27, true, 8, 100, false);
        AddEnemyUnit(1, 0, 38, false, 8, 82);
        AddEnemyUnit(1, 2, 39, false, 8, 82);
        AddEnemyUnit(1, 4, 40, false, 8, 82);
        AddEnemyUnit(1, 6, 28, false, 8, 82);
        AddEnemyUnit(1, 8, 29, false, 8, 82);
        AddEnemyUnit(1, 10, 35, false, 8, 82);
        AddEnemyUnit(1, 14, 36, false, 8, 82);
        AddEnemyUnit(1, 16, 30, false, 8, 82);
        AddEnemyUnit(1, 18, 31, false, 8, 82);
        AddEnemyUnit(1, 20, 32, false, 8, 82);
        AddEnemyUnit(1, 22, 33, false, 8, 82);
        AddEnemyUnit(1, 24, 34, false, 8, 82);

        //1000
        AddEnemyUnit(1, 12, 37, true, 9, 100, false);
        AddEnemyUnit(1, 0, 38, false, 9, 82);
        AddEnemyUnit(1, 2, 39, false, 9, 82);
        AddEnemyUnit(1, 4, 40, false, 9, 82);
        AddEnemyUnit(1, 6, 28, false, 9, 82);
        AddEnemyUnit(1, 8, 29, false, 9, 82);
        AddEnemyUnit(1, 10, 35, false, 9, 82);
        AddEnemyUnit(1, 14, 36, false, 9, 82);
        AddEnemyUnit(1, 16, 30, false, 9, 82);
        AddEnemyUnit(1, 18, 31, false, 9, 82);
        AddEnemyUnit(1, 20, 32, false, 9, 82);
        AddEnemyUnit(1, 22, 33, false, 9, 82);
        AddEnemyUnit(1, 24, 34, false, 9, 82);

        //1000
        AddEnemyUnit(1, 12, 41, true, 10, 100, false);
        AddEnemyUnit(1, 0, 38, false, 10, 82);
        AddEnemyUnit(1, 2, 39, false, 10, 82);
        AddEnemyUnit(1, 4, 40, false, 10, 82);
        AddEnemyUnit(1, 6, 28, false, 10, 82);
        AddEnemyUnit(1, 8, 29, false, 10, 82);
        AddEnemyUnit(1, 10, 35, false, 10, 82);
        AddEnemyUnit(1, 14, 36, false, 10, 82);
        AddEnemyUnit(1, 16, 30, false, 10, 82);
        AddEnemyUnit(1, 18, 31, false, 10, 82);
        AddEnemyUnit(1, 20, 32, false, 10, 82);
        AddEnemyUnit(1, 22, 33, false, 10, 82);
        AddEnemyUnit(1, 24, 34, false, 10, 82);

        //1000
        AddEnemyUnit(1, 12, 42, true, 11, 100, false);
        AddEnemyUnit(1, 0, 38, false, 11, 82);
        AddEnemyUnit(1, 2, 39, false, 11, 82);
        AddEnemyUnit(1, 4, 40, false, 11, 82);
        AddEnemyUnit(1, 6, 28, false, 11, 82);
        AddEnemyUnit(1, 8, 29, false, 11, 82);
        AddEnemyUnit(1, 10, 35, false, 11, 82);
        AddEnemyUnit(1, 14, 36, false, 11, 82);
        AddEnemyUnit(1, 16, 30, false, 11, 82);
        AddEnemyUnit(1, 18, 31, false, 11, 82);
        AddEnemyUnit(1, 20, 32, false, 11, 82);
        AddEnemyUnit(1, 22, 33, false, 11, 82);
        AddEnemyUnit(1, 24, 34, false, 11, 82);

        //1000
        AddEnemyUnit(1, 12, 44, true, 12, 100, false);
        AddEnemyUnit(1, 0, 38, false, 12, 82);
        AddEnemyUnit(1, 2, 39, false, 12, 82);
        AddEnemyUnit(1, 4, 40, false, 12, 82);
        AddEnemyUnit(1, 6, 28, false, 12, 82);
        AddEnemyUnit(1, 8, 29, false, 12, 82);
        AddEnemyUnit(1, 10, 35, false, 12, 82);
        AddEnemyUnit(1, 14, 36, false, 12, 82);
        AddEnemyUnit(1, 16, 30, false, 12, 82);
        AddEnemyUnit(1, 18, 31, false, 12, 82);
        AddEnemyUnit(1, 20, 32, false, 12, 82);
        AddEnemyUnit(1, 22, 33, false, 12, 82);
        AddEnemyUnit(1, 24, 34, false, 12, 82);

        //1000
        AddEnemyUnit(1, 12, 25, true, 13, 100, false);
        AddEnemyUnit(1, 0, 38, false, 13, 82);
        AddEnemyUnit(1, 2, 39, false, 13, 82);
        AddEnemyUnit(1, 4, 40, false, 13, 82);
        AddEnemyUnit(1, 6, 28, false, 13, 82);
        AddEnemyUnit(1, 8, 29, false, 13, 82);
        AddEnemyUnit(1, 10, 35, false, 13, 82);
        AddEnemyUnit(1, 14, 36, false, 13, 82);
        AddEnemyUnit(1, 16, 30, false, 13, 82);
        AddEnemyUnit(1, 18, 31, false, 13, 82);
        AddEnemyUnit(1, 20, 32, false, 13, 82);
        AddEnemyUnit(1, 22, 33, false, 13, 82);
        AddEnemyUnit(1, 24, 34, false, 13, 82);

        //1000
        AddEnemyUnit(1, 12, 24, true, 14, 100, false);
        AddEnemyUnit(1, 0, 38, false, 14, 82);
        AddEnemyUnit(1, 2, 39, false, 14, 82);
        AddEnemyUnit(1, 4, 40, false, 14, 82);
        AddEnemyUnit(1, 6, 28, false, 14, 82);
        AddEnemyUnit(1, 8, 29, false, 14, 82);
        AddEnemyUnit(1, 10, 35, false, 14, 82);
        AddEnemyUnit(1, 14, 36, false, 14, 82);
        AddEnemyUnit(1, 16, 30, false, 14, 82);
        AddEnemyUnit(1, 18, 31, false, 14, 82);
        AddEnemyUnit(1, 20, 32, false, 14, 82);
        AddEnemyUnit(1, 22, 33, false, 14, 82);
        AddEnemyUnit(1, 24, 34, false, 14, 82);




        for (int i = 0; i < 11; i++)
        {
            GameController.Instance.SetPlayerPlatoon(i);
        }
        maxDeployedPlatoon = 10;
    }

    private void Level27Setup()
    {
        Debug.Log("Level 27 Setup");

        Platoon p0 = new Platoon(0);
        enemyPlatoons[0] = p0;

        Platoon p1 = new Platoon(1);
        enemyPlatoons[1] = p1;

        Platoon p2 = new Platoon(2);
        enemyPlatoons[2] = p2;

        Platoon p3 = new Platoon(3);
        enemyPlatoons[3] = p3;

        Platoon p4 = new Platoon(4);
        enemyPlatoons[4] = p4;

        Platoon p5 = new Platoon(5);
        enemyPlatoons[5] = p5;

        Platoon p6 = new Platoon(6);
        enemyPlatoons[6] = p6;

        Platoon p7 = new Platoon(7);
        enemyPlatoons[7] = p7;

        Platoon p8 = new Platoon(8);
        enemyPlatoons[8] = p8;

        Platoon p9 = new Platoon(9);
        enemyPlatoons[9] = p9;

        Platoon p10 = new Platoon(10);
        enemyPlatoons[10] = p10;

        Platoon p11 = new Platoon(11);
        enemyPlatoons[11] = p11;

        Platoon p12 = new Platoon(12);
        enemyPlatoons[12] = p12;

        Platoon p13 = new Platoon(13);
        enemyPlatoons[13] = p13;

        Platoon p14 = new Platoon(14);
        enemyPlatoons[14] = p14;

        Platoon p15 = new Platoon(15);
        enemyPlatoons[15] = p15;

        enemyPlatoonsRemaining = 16;

        //AddEnemyUnit(int charType, int position, int charClass, bool leader, int platoon, int level)


        //1205
        AddEnemyUnit(1, 12, 58, true, 0, 120, true);
        AddEnemyUnit(1, 0, 50, false, 0, 105);
        AddEnemyUnit(1, 2, 49, false, 0, 105);
        AddEnemyUnit(1, 4, 40, false, 0, 107);
        AddEnemyUnit(1, 6, 28, false, 0, 107);
        AddEnemyUnit(1, 8, 29, false, 0, 107);
        AddEnemyUnit(1, 10, 47, false, 0, 105);
        AddEnemyUnit(1, 14, 48, false, 0, 105);
        AddEnemyUnit(1, 16, 45, false, 0, 105);
        AddEnemyUnit(1, 18, 31, false, 0, 107);
        AddEnemyUnit(1, 20, 32, false, 0, 107);
        AddEnemyUnit(1, 22, 46, false, 0, 105);
        AddEnemyUnit(1, 24, 34, false, 0, 107);


        //960
        AddEnemyUnit(1, 12, 45, true, 1, 100, true);
        AddEnemyUnit(1, 0, 27, false, 1, 80);
        AddEnemyUnit(1, 2, 27, false, 1, 80);
        AddEnemyUnit(1, 4, 27, false, 1, 80);
        AddEnemyUnit(1, 6, 27, false, 1, 80);
        AddEnemyUnit(1, 8, 27, false, 1, 80);
        AddEnemyUnit(1, 10, 27, false, 1, 80);
        AddEnemyUnit(1, 14, 27, false, 1, 80);
        AddEnemyUnit(1, 16, 27, false, 1, 80);
        AddEnemyUnit(1, 18, 27, false, 1, 80);
        AddEnemyUnit(1, 20, 27, false, 1, 80);
        AddEnemyUnit(1, 22, 27, false, 1, 80);
        AddEnemyUnit(1, 24, 27, false, 1, 80);

        //960
        AddEnemyUnit(1, 12, 47, true, 2, 100, true);
        AddEnemyUnit(1, 0, 35, false, 2, 80);
        AddEnemyUnit(1, 2, 35, false, 2, 80);
        AddEnemyUnit(1, 4, 35, false, 2, 80);
        AddEnemyUnit(1, 6, 35, false, 2, 80);
        AddEnemyUnit(1, 8, 35, false, 2, 80);
        AddEnemyUnit(1, 10, 35, false, 2, 80);
        AddEnemyUnit(1, 14, 35, false, 2, 80);
        AddEnemyUnit(1, 16, 35, false, 2, 80);
        AddEnemyUnit(1, 18, 35, false, 2, 80);
        AddEnemyUnit(1, 20, 35, false, 2, 80);
        AddEnemyUnit(1, 22, 35, false, 2, 80);
        AddEnemyUnit(1, 24, 35, false, 2, 80);

        //960
        AddEnemyUnit(1, 12, 50, true, 3, 100, true);
        AddEnemyUnit(1, 0, 38, false, 3, 80);
        AddEnemyUnit(1, 2, 38, false, 3, 80);
        AddEnemyUnit(1, 4, 38, false, 3, 80);
        AddEnemyUnit(1, 6, 38, false, 3, 80);
        AddEnemyUnit(1, 8, 38, false, 3, 80);
        AddEnemyUnit(1, 10, 38, false, 3, 80);
        AddEnemyUnit(1, 14, 38, false, 3, 80);
        AddEnemyUnit(1, 16, 38, false, 3, 80);
        AddEnemyUnit(1, 18, 38, false, 3, 80);
        AddEnemyUnit(1, 20, 38, false, 3, 80);
        AddEnemyUnit(1, 22, 38, false, 3, 80);
        AddEnemyUnit(1, 24, 38, false, 3, 80);

        //960
        AddEnemyUnit(1, 12, 46, true, 4, 100, true);
        AddEnemyUnit(1, 0, 32, false, 4, 80);
        AddEnemyUnit(1, 2, 32, false, 4, 80);
        AddEnemyUnit(1, 4, 32, false, 4, 80);
        AddEnemyUnit(1, 6, 32, false, 4, 80);
        AddEnemyUnit(1, 8, 32, false, 4, 80);
        AddEnemyUnit(1, 10, 32, false, 4, 80);
        AddEnemyUnit(1, 14, 32, false, 4, 80);
        AddEnemyUnit(1, 16, 32, false, 4, 80);
        AddEnemyUnit(1, 18, 32, false, 4, 80);
        AddEnemyUnit(1, 20, 32, false, 4, 80);
        AddEnemyUnit(1, 22, 32, false, 4, 80);
        AddEnemyUnit(1, 24, 32, false, 4, 80);

        //960
        AddEnemyUnit(1, 12, 48, true, 5, 100, true);
        AddEnemyUnit(1, 0, 37, false, 5, 80);
        AddEnemyUnit(1, 2, 37, false, 5, 80);
        AddEnemyUnit(1, 4, 37, false, 5, 80);
        AddEnemyUnit(1, 6, 37, false, 5, 80);
        AddEnemyUnit(1, 8, 37, false, 5, 80);
        AddEnemyUnit(1, 10, 37, false, 5, 80);
        AddEnemyUnit(1, 14, 37, false, 5, 80);
        AddEnemyUnit(1, 16, 37, false, 5, 80);
        AddEnemyUnit(1, 18, 37, false, 5, 80);
        AddEnemyUnit(1, 20, 37, false, 5, 80);
        AddEnemyUnit(1, 22, 37, false, 5, 80);
        AddEnemyUnit(1, 24, 37, false, 5, 80);

        //960
        AddEnemyUnit(1, 12, 49, true, 6, 100, true);
        AddEnemyUnit(1, 0, 41, false, 6, 80);
        AddEnemyUnit(1, 2, 41, false, 6, 80);
        AddEnemyUnit(1, 4, 41, false, 6, 80);
        AddEnemyUnit(1, 6, 41, false, 6, 80);
        AddEnemyUnit(1, 8, 41, false, 6, 80);
        AddEnemyUnit(1, 10, 41, false, 6, 80);
        AddEnemyUnit(1, 14, 41, false, 6, 80);
        AddEnemyUnit(1, 16, 41, false, 6, 80);
        AddEnemyUnit(1, 18, 41, false, 6, 80);
        AddEnemyUnit(1, 20, 41, false, 6, 80);
        AddEnemyUnit(1, 22, 41, false, 6, 80);
        AddEnemyUnit(1, 24, 41, false, 6, 80);

        //1000
        AddEnemyUnit(1, 12, 26, true, 7, 105, false);
        AddEnemyUnit(1, 0, 38, false, 7, 85);
        AddEnemyUnit(1, 2, 39, false, 7, 85);
        AddEnemyUnit(1, 4, 40, false, 7, 85);
        AddEnemyUnit(1, 6, 28, false, 7, 85);
        AddEnemyUnit(1, 8, 29, false, 7, 85);
        AddEnemyUnit(1, 10, 35, false, 7, 85);
        AddEnemyUnit(1, 14, 36, false, 7, 85);
        AddEnemyUnit(1, 16, 30, false, 7, 85);
        AddEnemyUnit(1, 18, 31, false, 7, 85);
        AddEnemyUnit(1, 20, 32, false, 7, 85);
        AddEnemyUnit(1, 22, 33, false, 7, 85);
        AddEnemyUnit(1, 24, 34, false, 7, 85);

        //1000
        AddEnemyUnit(1, 12, 27, true, 8, 105, false);
        AddEnemyUnit(1, 0, 38, false, 8, 85);
        AddEnemyUnit(1, 2, 39, false, 8, 85);
        AddEnemyUnit(1, 4, 40, false, 8, 85);
        AddEnemyUnit(1, 6, 28, false, 8, 85);
        AddEnemyUnit(1, 8, 29, false, 8, 85);
        AddEnemyUnit(1, 10, 35, false, 8, 85);
        AddEnemyUnit(1, 14, 36, false, 8, 85);
        AddEnemyUnit(1, 16, 30, false, 8, 85);
        AddEnemyUnit(1, 18, 31, false, 8, 85);
        AddEnemyUnit(1, 20, 32, false, 8, 85);
        AddEnemyUnit(1, 22, 33, false, 8, 85);
        AddEnemyUnit(1, 24, 34, false, 8, 85);

        //1000
        AddEnemyUnit(1, 12, 37, true, 9, 105, false);
        AddEnemyUnit(1, 0, 38, false, 9, 85);
        AddEnemyUnit(1, 2, 39, false, 9, 85);
        AddEnemyUnit(1, 4, 40, false, 9, 85);
        AddEnemyUnit(1, 6, 28, false, 9, 85);
        AddEnemyUnit(1, 8, 29, false, 9, 85);
        AddEnemyUnit(1, 10, 35, false, 9, 85);
        AddEnemyUnit(1, 14, 36, false, 9, 85);
        AddEnemyUnit(1, 16, 30, false, 9, 85);
        AddEnemyUnit(1, 18, 31, false, 9, 85);
        AddEnemyUnit(1, 20, 32, false, 9, 85);
        AddEnemyUnit(1, 22, 33, false, 9, 85);
        AddEnemyUnit(1, 24, 34, false, 9, 85);

        //1000
        AddEnemyUnit(1, 12, 41, true, 10, 105, false);
        AddEnemyUnit(1, 0, 38, false, 10, 85);
        AddEnemyUnit(1, 2, 39, false, 10, 85);
        AddEnemyUnit(1, 4, 40, false, 10, 85);
        AddEnemyUnit(1, 6, 28, false, 10, 85);
        AddEnemyUnit(1, 8, 29, false, 10, 85);
        AddEnemyUnit(1, 10, 35, false, 10, 85);
        AddEnemyUnit(1, 14, 36, false, 10, 85);
        AddEnemyUnit(1, 16, 30, false, 10, 85);
        AddEnemyUnit(1, 18, 31, false, 10, 85);
        AddEnemyUnit(1, 20, 32, false, 10, 85);
        AddEnemyUnit(1, 22, 33, false, 10, 85);
        AddEnemyUnit(1, 24, 34, false, 10, 85);

        //1000
        AddEnemyUnit(1, 12, 42, true, 11, 105, false);
        AddEnemyUnit(1, 0, 38, false, 11, 85);
        AddEnemyUnit(1, 2, 39, false, 11, 85);
        AddEnemyUnit(1, 4, 40, false, 11, 85);
        AddEnemyUnit(1, 6, 28, false, 11, 85);
        AddEnemyUnit(1, 8, 29, false, 11, 85);
        AddEnemyUnit(1, 10, 35, false, 11, 85);
        AddEnemyUnit(1, 14, 36, false, 11, 85);
        AddEnemyUnit(1, 16, 30, false, 11, 85);
        AddEnemyUnit(1, 18, 31, false, 11, 85);
        AddEnemyUnit(1, 20, 32, false, 11, 85);
        AddEnemyUnit(1, 22, 33, false, 11, 85);
        AddEnemyUnit(1, 24, 34, false, 11, 85);

        //1000
        AddEnemyUnit(1, 12, 44, true, 12, 105, false);
        AddEnemyUnit(1, 0, 38, false, 12, 85);
        AddEnemyUnit(1, 2, 39, false, 12, 85);
        AddEnemyUnit(1, 4, 40, false, 12, 85);
        AddEnemyUnit(1, 6, 28, false, 12, 85);
        AddEnemyUnit(1, 8, 29, false, 12, 85);
        AddEnemyUnit(1, 10, 35, false, 12, 85);
        AddEnemyUnit(1, 14, 36, false, 12, 85);
        AddEnemyUnit(1, 16, 30, false, 12, 85);
        AddEnemyUnit(1, 18, 31, false, 12, 85);
        AddEnemyUnit(1, 20, 32, false, 12, 85);
        AddEnemyUnit(1, 22, 33, false, 12, 85);
        AddEnemyUnit(1, 24, 34, false, 12, 85);

        //1000
        AddEnemyUnit(1, 12, 25, true, 13, 105, false);
        AddEnemyUnit(1, 0, 38, false, 13, 85);
        AddEnemyUnit(1, 2, 39, false, 13, 85);
        AddEnemyUnit(1, 4, 40, false, 13, 85);
        AddEnemyUnit(1, 6, 28, false, 13, 85);
        AddEnemyUnit(1, 8, 29, false, 13, 85);
        AddEnemyUnit(1, 10, 35, false, 13, 85);
        AddEnemyUnit(1, 14, 36, false, 13, 85);
        AddEnemyUnit(1, 16, 30, false, 13, 85);
        AddEnemyUnit(1, 18, 31, false, 13, 85);
        AddEnemyUnit(1, 20, 32, false, 13, 85);
        AddEnemyUnit(1, 22, 33, false, 13, 85);
        AddEnemyUnit(1, 24, 34, false, 13, 85);

        //1000
        AddEnemyUnit(1, 12, 24, true, 14, 105, false);
        AddEnemyUnit(1, 0, 38, false, 14, 85);
        AddEnemyUnit(1, 2, 39, false, 14, 85);
        AddEnemyUnit(1, 4, 40, false, 14, 85);
        AddEnemyUnit(1, 6, 28, false, 14, 85);
        AddEnemyUnit(1, 8, 29, false, 14, 85);
        AddEnemyUnit(1, 10, 35, false, 14, 85);
        AddEnemyUnit(1, 14, 36, false, 14, 85);
        AddEnemyUnit(1, 16, 30, false, 14, 85);
        AddEnemyUnit(1, 18, 31, false, 14, 85);
        AddEnemyUnit(1, 20, 32, false, 14, 85);
        AddEnemyUnit(1, 22, 33, false, 14, 85);
        AddEnemyUnit(1, 24, 34, false, 14, 85);

        //1000
        AddEnemyUnit(1, 12, 54, true, 15, 105, false);
        AddEnemyUnit(1, 0, 38, false, 15, 85);
        AddEnemyUnit(1, 2, 39, false, 15, 85);
        AddEnemyUnit(1, 4, 40, false, 15, 85);
        AddEnemyUnit(1, 6, 28, false, 15, 85);
        AddEnemyUnit(1, 8, 29, false, 15, 85);
        AddEnemyUnit(1, 10, 35, false, 15, 85);
        AddEnemyUnit(1, 14, 36, false, 15, 85);
        AddEnemyUnit(1, 16, 30, false, 15, 85);
        AddEnemyUnit(1, 18, 31, false, 15, 85);
        AddEnemyUnit(1, 20, 32, false, 15, 85);
        AddEnemyUnit(1, 22, 33, false, 15, 85);
        AddEnemyUnit(1, 24, 34, false, 15, 85);



        for (int i = 0; i < 11; i++)
        {
            GameController.Instance.SetPlayerPlatoon(i);
        }
        maxDeployedPlatoon = 10;
    }

    private void Level28Setup()
    {
        Debug.Log("Level 28 Setup");

        Platoon p0 = new Platoon(0);
        enemyPlatoons[0] = p0;

        Platoon p1 = new Platoon(1);
        enemyPlatoons[1] = p1;

        Platoon p2 = new Platoon(2);
        enemyPlatoons[2] = p2;

        Platoon p3 = new Platoon(3);
        enemyPlatoons[3] = p3;

        Platoon p4 = new Platoon(4);
        enemyPlatoons[4] = p4;

        Platoon p5 = new Platoon(5);
        enemyPlatoons[5] = p5;

        Platoon p6 = new Platoon(6);
        enemyPlatoons[6] = p6;

        Platoon p7 = new Platoon(7);
        enemyPlatoons[7] = p7;

        Platoon p8 = new Platoon(8);
        enemyPlatoons[8] = p8;

        Platoon p9 = new Platoon(9);
        enemyPlatoons[9] = p9;

        Platoon p10 = new Platoon(10);
        enemyPlatoons[10] = p10;

        Platoon p11 = new Platoon(11);
        enemyPlatoons[11] = p11;

        Platoon p12 = new Platoon(12);
        enemyPlatoons[12] = p12;

        Platoon p13 = new Platoon(13);
        enemyPlatoons[13] = p13;

        Platoon p14 = new Platoon(14);
        enemyPlatoons[14] = p14;

        Platoon p15 = new Platoon(15);
        enemyPlatoons[15] = p15;

        Platoon p16 = new Platoon(16);
        enemyPlatoons[16] = p16;

        enemyPlatoonsRemaining = 17;

        //AddEnemyUnit(int charType, int position, int charClass, bool leader, int platoon, int level)


        //1205
        AddEnemyUnit(1, 12, 52, true, 0, 125, true);
        AddEnemyUnit(1, 0, 50, false, 0, 110);
        AddEnemyUnit(1, 2, 49, false, 0, 110);
        AddEnemyUnit(1, 4, 50, false, 0, 110);
        AddEnemyUnit(1, 6, 45, false, 0, 110);
        AddEnemyUnit(1, 8, 45, false, 0, 110);
        AddEnemyUnit(1, 10, 47, false, 0, 110);
        AddEnemyUnit(1, 14, 47, false, 0, 110);
        AddEnemyUnit(1, 16, 48, false, 0, 110);
        AddEnemyUnit(1, 18, 48, false, 0, 110);
        AddEnemyUnit(1, 20, 46, false, 0, 110);
        AddEnemyUnit(1, 22, 49, false, 0, 110);
        AddEnemyUnit(1, 24, 46, false, 0, 110);


        //960
        AddEnemyUnit(1, 12, 45, true, 1, 105, true);
        AddEnemyUnit(1, 0, 27, false, 1, 85);
        AddEnemyUnit(1, 2, 27, false, 1, 85);
        AddEnemyUnit(1, 4, 27, false, 1, 85);
        AddEnemyUnit(1, 6, 27, false, 1, 85);
        AddEnemyUnit(1, 8, 27, false, 1, 85);
        AddEnemyUnit(1, 10, 27, false, 1, 85);
        AddEnemyUnit(1, 14, 27, false, 1, 85);
        AddEnemyUnit(1, 16, 27, false, 1, 85);
        AddEnemyUnit(1, 18, 27, false, 1, 85);
        AddEnemyUnit(1, 20, 27, false, 1, 85);
        AddEnemyUnit(1, 22, 27, false, 1, 85);
        AddEnemyUnit(1, 24, 27, false, 1, 85);

        //960
        AddEnemyUnit(1, 12, 47, true, 2, 105, true);
        AddEnemyUnit(1, 0, 35, false, 2, 85);
        AddEnemyUnit(1, 2, 35, false, 2, 85);
        AddEnemyUnit(1, 4, 35, false, 2, 85);
        AddEnemyUnit(1, 6, 35, false, 2, 85);
        AddEnemyUnit(1, 8, 35, false, 2, 85);
        AddEnemyUnit(1, 10, 35, false, 2, 85);
        AddEnemyUnit(1, 14, 35, false, 2, 85);
        AddEnemyUnit(1, 16, 35, false, 2, 85);
        AddEnemyUnit(1, 18, 35, false, 2, 85);
        AddEnemyUnit(1, 20, 35, false, 2, 85);
        AddEnemyUnit(1, 22, 35, false, 2, 85);
        AddEnemyUnit(1, 24, 35, false, 2, 85);

        //960
        AddEnemyUnit(1, 12, 50, true, 3, 105, true);
        AddEnemyUnit(1, 0, 38, false, 3, 85);
        AddEnemyUnit(1, 2, 38, false, 3, 85);
        AddEnemyUnit(1, 4, 38, false, 3, 85);
        AddEnemyUnit(1, 6, 38, false, 3, 85);
        AddEnemyUnit(1, 8, 38, false, 3, 85);
        AddEnemyUnit(1, 10, 38, false, 3, 85);
        AddEnemyUnit(1, 14, 38, false, 3, 85);
        AddEnemyUnit(1, 16, 38, false, 3, 85);
        AddEnemyUnit(1, 18, 38, false, 3, 85);
        AddEnemyUnit(1, 20, 38, false, 3, 85);
        AddEnemyUnit(1, 22, 38, false, 3, 85);
        AddEnemyUnit(1, 24, 38, false, 3, 85);

        //960
        AddEnemyUnit(1, 12, 46, true, 4, 105, true);
        AddEnemyUnit(1, 0, 32, false, 4, 85);
        AddEnemyUnit(1, 2, 32, false, 4, 85);
        AddEnemyUnit(1, 4, 32, false, 4, 85);
        AddEnemyUnit(1, 6, 32, false, 4, 85);
        AddEnemyUnit(1, 8, 32, false, 4, 85);
        AddEnemyUnit(1, 10, 32, false, 4, 85);
        AddEnemyUnit(1, 14, 32, false, 4, 85);
        AddEnemyUnit(1, 16, 32, false, 4, 85);
        AddEnemyUnit(1, 18, 32, false, 4, 85);
        AddEnemyUnit(1, 20, 32, false, 4, 85);
        AddEnemyUnit(1, 22, 32, false, 4, 85);
        AddEnemyUnit(1, 24, 32, false, 4, 85);

        //960
        AddEnemyUnit(1, 12, 48, true, 5, 105, true);
        AddEnemyUnit(1, 0, 37, false, 5, 85);
        AddEnemyUnit(1, 2, 37, false, 5, 85);
        AddEnemyUnit(1, 4, 37, false, 5, 85);
        AddEnemyUnit(1, 6, 37, false, 5, 85);
        AddEnemyUnit(1, 8, 37, false, 5, 85);
        AddEnemyUnit(1, 10, 37, false, 5, 85);
        AddEnemyUnit(1, 14, 37, false, 5, 85);
        AddEnemyUnit(1, 16, 37, false, 5, 85);
        AddEnemyUnit(1, 18, 37, false, 5, 85);
        AddEnemyUnit(1, 20, 37, false, 5, 85);
        AddEnemyUnit(1, 22, 37, false, 5, 85);
        AddEnemyUnit(1, 24, 37, false, 5, 85);

        //960
        AddEnemyUnit(1, 12, 49, true, 6, 105, true);
        AddEnemyUnit(1, 0, 41, false, 6, 85);
        AddEnemyUnit(1, 2, 41, false, 6, 85);
        AddEnemyUnit(1, 4, 41, false, 6, 85);
        AddEnemyUnit(1, 6, 41, false, 6, 85);
        AddEnemyUnit(1, 8, 41, false, 6, 85);
        AddEnemyUnit(1, 10, 41, false, 6, 85);
        AddEnemyUnit(1, 14, 41, false, 6, 85);
        AddEnemyUnit(1, 16, 41, false, 6, 85);
        AddEnemyUnit(1, 18, 41, false, 6, 85);
        AddEnemyUnit(1, 20, 41, false, 6, 85);
        AddEnemyUnit(1, 22, 41, false, 6, 85);
        AddEnemyUnit(1, 24, 41, false, 6, 85);

        //1000
        AddEnemyUnit(1, 12, 26, true, 7, 110, false);
        AddEnemyUnit(1, 0, 38, false, 7, 90);
        AddEnemyUnit(1, 2, 39, false, 7, 90);
        AddEnemyUnit(1, 4, 40, false, 7, 90);
        AddEnemyUnit(1, 6, 28, false, 7, 90);
        AddEnemyUnit(1, 8, 29, false, 7, 90);
        AddEnemyUnit(1, 10, 35, false, 7, 90);
        AddEnemyUnit(1, 14, 36, false, 7, 90);
        AddEnemyUnit(1, 16, 30, false, 7, 90);
        AddEnemyUnit(1, 18, 31, false, 7, 90);
        AddEnemyUnit(1, 20, 32, false, 7, 90);
        AddEnemyUnit(1, 22, 33, false, 7, 90);
        AddEnemyUnit(1, 24, 34, false, 7, 90);

        //1000
        AddEnemyUnit(1, 12, 27, true, 8, 110, false);
        AddEnemyUnit(1, 0, 38, false, 8, 90);
        AddEnemyUnit(1, 2, 39, false, 8, 90);
        AddEnemyUnit(1, 4, 40, false, 8, 90);
        AddEnemyUnit(1, 6, 28, false, 8, 90);
        AddEnemyUnit(1, 8, 29, false, 8, 90);
        AddEnemyUnit(1, 10, 35, false, 8, 90);
        AddEnemyUnit(1, 14, 36, false, 8, 90);
        AddEnemyUnit(1, 16, 30, false, 8, 90);
        AddEnemyUnit(1, 18, 31, false, 8, 90);
        AddEnemyUnit(1, 20, 32, false, 8, 90);
        AddEnemyUnit(1, 22, 33, false, 8, 90);
        AddEnemyUnit(1, 24, 34, false, 8, 90);

        //1000
        AddEnemyUnit(1, 12, 37, true, 9, 110, false);
        AddEnemyUnit(1, 0, 38, false, 9, 90);
        AddEnemyUnit(1, 2, 39, false, 9, 90);
        AddEnemyUnit(1, 4, 40, false, 9, 90);
        AddEnemyUnit(1, 6, 28, false, 9, 90);
        AddEnemyUnit(1, 8, 29, false, 9, 90);
        AddEnemyUnit(1, 10, 35, false, 9, 90);
        AddEnemyUnit(1, 14, 36, false, 9, 90);
        AddEnemyUnit(1, 16, 30, false, 9, 90);
        AddEnemyUnit(1, 18, 31, false, 9, 90);
        AddEnemyUnit(1, 20, 32, false, 9, 90);
        AddEnemyUnit(1, 22, 33, false, 9, 90);
        AddEnemyUnit(1, 24, 34, false, 9, 90);

        //1000
        AddEnemyUnit(1, 12, 41, true, 10, 110, false);
        AddEnemyUnit(1, 0, 38, false, 10, 90);
        AddEnemyUnit(1, 2, 39, false, 10, 90);
        AddEnemyUnit(1, 4, 40, false, 10, 90);
        AddEnemyUnit(1, 6, 28, false, 10, 90);
        AddEnemyUnit(1, 8, 29, false, 10, 90);
        AddEnemyUnit(1, 10, 35, false, 10, 90);
        AddEnemyUnit(1, 14, 36, false, 10, 90);
        AddEnemyUnit(1, 16, 30, false, 10, 90);
        AddEnemyUnit(1, 18, 31, false, 10, 90);
        AddEnemyUnit(1, 20, 32, false, 10, 90);
        AddEnemyUnit(1, 22, 33, false, 10, 90);
        AddEnemyUnit(1, 24, 34, false, 10, 90);

        //1000
        AddEnemyUnit(1, 12, 42, true, 11, 110, false);
        AddEnemyUnit(1, 0, 38, false, 11, 90);
        AddEnemyUnit(1, 2, 39, false, 11, 90);
        AddEnemyUnit(1, 4, 40, false, 11, 90);
        AddEnemyUnit(1, 6, 28, false, 11, 90);
        AddEnemyUnit(1, 8, 29, false, 11, 90);
        AddEnemyUnit(1, 10, 35, false, 11, 90);
        AddEnemyUnit(1, 14, 36, false, 11, 90);
        AddEnemyUnit(1, 16, 30, false, 11, 90);
        AddEnemyUnit(1, 18, 31, false, 11, 90);
        AddEnemyUnit(1, 20, 32, false, 11, 90);
        AddEnemyUnit(1, 22, 33, false, 11, 90);
        AddEnemyUnit(1, 24, 34, false, 11, 90);

        //1000
        AddEnemyUnit(1, 12, 44, true, 12, 110, false);
        AddEnemyUnit(1, 0, 38, false, 12, 90);
        AddEnemyUnit(1, 2, 39, false, 12, 90);
        AddEnemyUnit(1, 4, 40, false, 12, 90);
        AddEnemyUnit(1, 6, 28, false, 12, 90);
        AddEnemyUnit(1, 8, 29, false, 12, 90);
        AddEnemyUnit(1, 10, 35, false, 12, 90);
        AddEnemyUnit(1, 14, 36, false, 12, 90);
        AddEnemyUnit(1, 16, 30, false, 12, 90);
        AddEnemyUnit(1, 18, 31, false, 12, 90);
        AddEnemyUnit(1, 20, 32, false, 12, 90);
        AddEnemyUnit(1, 22, 33, false, 12, 90);
        AddEnemyUnit(1, 24, 34, false, 12, 90);

        //1000
        AddEnemyUnit(1, 12, 25, true, 13, 110, false);
        AddEnemyUnit(1, 0, 38, false, 13, 90);
        AddEnemyUnit(1, 2, 39, false, 13, 90);
        AddEnemyUnit(1, 4, 40, false, 13, 90);
        AddEnemyUnit(1, 6, 28, false, 13, 90);
        AddEnemyUnit(1, 8, 29, false, 13, 90);
        AddEnemyUnit(1, 10, 35, false, 13, 90);
        AddEnemyUnit(1, 14, 36, false, 13, 90);
        AddEnemyUnit(1, 16, 30, false, 13, 90);
        AddEnemyUnit(1, 18, 31, false, 13, 90);
        AddEnemyUnit(1, 20, 32, false, 13, 90);
        AddEnemyUnit(1, 22, 33, false, 13, 90);
        AddEnemyUnit(1, 24, 34, false, 13, 90);

        //1000
        AddEnemyUnit(1, 12, 24, true, 14, 110, false);
        AddEnemyUnit(1, 0, 38, false, 14, 90);
        AddEnemyUnit(1, 2, 39, false, 14, 90);
        AddEnemyUnit(1, 4, 40, false, 14, 90);
        AddEnemyUnit(1, 6, 28, false, 14, 90);
        AddEnemyUnit(1, 8, 29, false, 14, 90);
        AddEnemyUnit(1, 10, 35, false, 14, 90);
        AddEnemyUnit(1, 14, 36, false, 14, 90);
        AddEnemyUnit(1, 16, 30, false, 14, 90);
        AddEnemyUnit(1, 18, 31, false, 14, 90);
        AddEnemyUnit(1, 20, 32, false, 14, 90);
        AddEnemyUnit(1, 22, 33, false, 14, 90);
        AddEnemyUnit(1, 24, 34, false, 14, 90);

        //1000
        AddEnemyUnit(1, 12, 54, true, 15, 110, false);
        AddEnemyUnit(1, 0, 38, false, 15, 90);
        AddEnemyUnit(1, 2, 39, false, 15, 90);
        AddEnemyUnit(1, 4, 40, false, 15, 90);
        AddEnemyUnit(1, 6, 28, false, 15, 90);
        AddEnemyUnit(1, 8, 29, false, 15, 90);
        AddEnemyUnit(1, 10, 35, false, 15, 90);
        AddEnemyUnit(1, 14, 36, false, 15, 90);
        AddEnemyUnit(1, 16, 30, false, 15, 90);
        AddEnemyUnit(1, 18, 31, false, 15, 90);
        AddEnemyUnit(1, 20, 32, false, 15, 90);
        AddEnemyUnit(1, 22, 33, false, 15, 90);
        AddEnemyUnit(1, 24, 34, false, 15, 90);

        //1000
        AddEnemyUnit(1, 12, 58, true, 16, 110, false);
        AddEnemyUnit(1, 0, 38, false, 16, 90);
        AddEnemyUnit(1, 2, 39, false, 16, 90);
        AddEnemyUnit(1, 4, 40, false, 16, 90);
        AddEnemyUnit(1, 6, 28, false, 16, 90);
        AddEnemyUnit(1, 8, 29, false, 16, 90);
        AddEnemyUnit(1, 10, 35, false, 16, 90);
        AddEnemyUnit(1, 14, 36, false, 16, 90);
        AddEnemyUnit(1, 16, 30, false, 16, 90);
        AddEnemyUnit(1, 18, 31, false, 16, 90);
        AddEnemyUnit(1, 20, 32, false, 16, 90);
        AddEnemyUnit(1, 22, 33, false, 16, 90);
        AddEnemyUnit(1, 24, 34, false, 16, 90);

        for (int i = 0; i < 11; i++)
        {
            GameController.Instance.SetPlayerPlatoon(i);
        }
        maxDeployedPlatoon = 10;
    }

    private void Level29Setup()
    {
        Debug.Log("Level 29 Setup");

        Platoon p0 = new Platoon(0);
        enemyPlatoons[0] = p0;

        Platoon p1 = new Platoon(1);
        enemyPlatoons[1] = p1;

        Platoon p2 = new Platoon(2);
        enemyPlatoons[2] = p2;

        Platoon p3 = new Platoon(3);
        enemyPlatoons[3] = p3;

        Platoon p4 = new Platoon(4);
        enemyPlatoons[4] = p4;

        Platoon p5 = new Platoon(5);
        enemyPlatoons[5] = p5;

        Platoon p6 = new Platoon(6);
        enemyPlatoons[6] = p6;

        Platoon p7 = new Platoon(7);
        enemyPlatoons[7] = p7;

        Platoon p8 = new Platoon(8);
        enemyPlatoons[8] = p8;

        Platoon p9 = new Platoon(9);
        enemyPlatoons[9] = p9;

        Platoon p10 = new Platoon(10);
        enemyPlatoons[10] = p10;

        Platoon p11 = new Platoon(11);
        enemyPlatoons[11] = p11;

        Platoon p12 = new Platoon(12);
        enemyPlatoons[12] = p12;

        Platoon p13 = new Platoon(13);
        enemyPlatoons[13] = p13;

        Platoon p14 = new Platoon(14);
        enemyPlatoons[14] = p14;

        Platoon p15 = new Platoon(15);
        enemyPlatoons[15] = p15;

        Platoon p16 = new Platoon(16);
        enemyPlatoons[16] = p16;

        Platoon p17 = new Platoon(17);
        enemyPlatoons[17] = p17;

        enemyPlatoonsRemaining = 18;

        //AddEnemyUnit(int charType, int position, int charClass, bool leader, int platoon, int level)


        //1205
        AddEnemyUnit(1, 12, 55, true, 0, 130, true);
        AddEnemyUnit(1, 0, 50, false, 0, 112);
        AddEnemyUnit(1, 2, 49, false, 0, 112);
        AddEnemyUnit(1, 4, 50, false, 0, 112);
        AddEnemyUnit(1, 6, 45, false, 0, 112);
        AddEnemyUnit(1, 8, 45, false, 0, 112);
        AddEnemyUnit(1, 10, 47, false, 0, 112);
        AddEnemyUnit(1, 14, 47, false, 0, 112);
        AddEnemyUnit(1, 16, 48, false, 0, 112);
        AddEnemyUnit(1, 18, 48, false, 0, 112);
        AddEnemyUnit(1, 20, 46, false, 0, 112);
        AddEnemyUnit(1, 22, 49, false, 0, 112);
        AddEnemyUnit(1, 24, 46, false, 0, 112);


        //960
        AddEnemyUnit(1, 12, 45, true, 1, 110, true);
        AddEnemyUnit(1, 0, 27, false, 1, 88);
        AddEnemyUnit(1, 2, 27, false, 1, 88);
        AddEnemyUnit(1, 4, 27, false, 1, 88);
        AddEnemyUnit(1, 6, 27, false, 1, 88);
        AddEnemyUnit(1, 8, 27, false, 1, 88);
        AddEnemyUnit(1, 10, 27, false, 1, 88);
        AddEnemyUnit(1, 14, 27, false, 1, 88);
        AddEnemyUnit(1, 16, 27, false, 1, 88);
        AddEnemyUnit(1, 18, 27, false, 1, 88);
        AddEnemyUnit(1, 20, 27, false, 1, 88);
        AddEnemyUnit(1, 22, 27, false, 1, 88);
        AddEnemyUnit(1, 24, 27, false, 1, 88);

        //960
        AddEnemyUnit(1, 12, 47, true, 2, 110, true);
        AddEnemyUnit(1, 0, 35, false, 2, 88);
        AddEnemyUnit(1, 2, 35, false, 2, 88);
        AddEnemyUnit(1, 4, 35, false, 2, 88);
        AddEnemyUnit(1, 6, 35, false, 2, 88);
        AddEnemyUnit(1, 8, 35, false, 2, 88);
        AddEnemyUnit(1, 10, 35, false, 2, 88);
        AddEnemyUnit(1, 14, 35, false, 2, 88);
        AddEnemyUnit(1, 16, 35, false, 2, 88);
        AddEnemyUnit(1, 18, 35, false, 2, 88);
        AddEnemyUnit(1, 20, 35, false, 2, 88);
        AddEnemyUnit(1, 22, 35, false, 2, 88);
        AddEnemyUnit(1, 24, 35, false, 2, 88);

        //960
        AddEnemyUnit(1, 12, 50, true, 3, 110, true);
        AddEnemyUnit(1, 0, 38, false, 3, 88);
        AddEnemyUnit(1, 2, 38, false, 3, 88);
        AddEnemyUnit(1, 4, 38, false, 3, 88);
        AddEnemyUnit(1, 6, 38, false, 3, 88);
        AddEnemyUnit(1, 8, 38, false, 3, 88);
        AddEnemyUnit(1, 10, 38, false, 3, 88);
        AddEnemyUnit(1, 14, 38, false, 3, 88);
        AddEnemyUnit(1, 16, 38, false, 3, 88);
        AddEnemyUnit(1, 18, 38, false, 3, 88);
        AddEnemyUnit(1, 20, 38, false, 3, 88);
        AddEnemyUnit(1, 22, 38, false, 3, 88);
        AddEnemyUnit(1, 24, 38, false, 3, 88);

        //960
        AddEnemyUnit(1, 12, 46, true, 4, 110, true);
        AddEnemyUnit(1, 0, 32, false, 4, 88);
        AddEnemyUnit(1, 2, 32, false, 4, 88);
        AddEnemyUnit(1, 4, 32, false, 4, 88);
        AddEnemyUnit(1, 6, 32, false, 4, 88);
        AddEnemyUnit(1, 8, 32, false, 4, 88);
        AddEnemyUnit(1, 10, 32, false, 4, 88);
        AddEnemyUnit(1, 14, 32, false, 4, 88);
        AddEnemyUnit(1, 16, 32, false, 4, 88);
        AddEnemyUnit(1, 18, 32, false, 4, 88);
        AddEnemyUnit(1, 20, 32, false, 4, 88);
        AddEnemyUnit(1, 22, 32, false, 4, 88);
        AddEnemyUnit(1, 24, 32, false, 4, 88);

        //960
        AddEnemyUnit(1, 12, 48, true, 5, 110, true);
        AddEnemyUnit(1, 0, 37, false, 5, 88);
        AddEnemyUnit(1, 2, 37, false, 5, 88);
        AddEnemyUnit(1, 4, 37, false, 5, 88);
        AddEnemyUnit(1, 6, 37, false, 5, 88);
        AddEnemyUnit(1, 8, 37, false, 5, 88);
        AddEnemyUnit(1, 10, 37, false, 5, 88);
        AddEnemyUnit(1, 14, 37, false, 5, 88);
        AddEnemyUnit(1, 16, 37, false, 5, 88);
        AddEnemyUnit(1, 18, 37, false, 5, 88);
        AddEnemyUnit(1, 20, 37, false, 5, 88);
        AddEnemyUnit(1, 22, 37, false, 5, 88);
        AddEnemyUnit(1, 24, 37, false, 5, 88);

        //960
        AddEnemyUnit(1, 12, 49, true, 6, 110, true);
        AddEnemyUnit(1, 0, 41, false, 6, 88);
        AddEnemyUnit(1, 2, 41, false, 6, 88);
        AddEnemyUnit(1, 4, 41, false, 6, 88);
        AddEnemyUnit(1, 6, 41, false, 6, 88);
        AddEnemyUnit(1, 8, 41, false, 6, 88);
        AddEnemyUnit(1, 10, 41, false, 6, 88);
        AddEnemyUnit(1, 14, 41, false, 6, 88);
        AddEnemyUnit(1, 16, 41, false, 6, 88);
        AddEnemyUnit(1, 18, 41, false, 6, 88);
        AddEnemyUnit(1, 20, 41, false, 6, 88);
        AddEnemyUnit(1, 22, 41, false, 6, 88);
        AddEnemyUnit(1, 24, 41, false, 6, 88);

        //1000
        AddEnemyUnit(1, 12, 26, true, 7, 115, false);
        AddEnemyUnit(1, 0, 38, false, 7, 92);
        AddEnemyUnit(1, 2, 39, false, 7, 92);
        AddEnemyUnit(1, 4, 40, false, 7, 92);
        AddEnemyUnit(1, 6, 28, false, 7, 92);
        AddEnemyUnit(1, 8, 29, false, 7, 92);
        AddEnemyUnit(1, 10, 35, false, 7, 92);
        AddEnemyUnit(1, 14, 36, false, 7, 92);
        AddEnemyUnit(1, 16, 30, false, 7, 92);
        AddEnemyUnit(1, 18, 31, false, 7, 92);
        AddEnemyUnit(1, 20, 32, false, 7, 92);
        AddEnemyUnit(1, 22, 33, false, 7, 92);
        AddEnemyUnit(1, 24, 34, false, 7, 92);

        //1000
        AddEnemyUnit(1, 12, 27, true, 8, 115, false);
        AddEnemyUnit(1, 0, 38, false, 8, 92);
        AddEnemyUnit(1, 2, 39, false, 8, 92);
        AddEnemyUnit(1, 4, 40, false, 8, 92);
        AddEnemyUnit(1, 6, 28, false, 8, 92);
        AddEnemyUnit(1, 8, 29, false, 8, 92);
        AddEnemyUnit(1, 10, 35, false, 8, 92);
        AddEnemyUnit(1, 14, 36, false, 8, 92);
        AddEnemyUnit(1, 16, 30, false, 8, 92);
        AddEnemyUnit(1, 18, 31, false, 8, 92);
        AddEnemyUnit(1, 20, 32, false, 8, 92);
        AddEnemyUnit(1, 22, 33, false, 8, 92);
        AddEnemyUnit(1, 24, 34, false, 8, 92);

        //1000
        AddEnemyUnit(1, 12, 37, true, 9, 115, false);
        AddEnemyUnit(1, 0, 38, false, 9, 92);
        AddEnemyUnit(1, 2, 39, false, 9, 92);
        AddEnemyUnit(1, 4, 40, false, 9, 92);
        AddEnemyUnit(1, 6, 28, false, 9, 92);
        AddEnemyUnit(1, 8, 29, false, 9, 92);
        AddEnemyUnit(1, 10, 35, false, 9, 92);
        AddEnemyUnit(1, 14, 36, false, 9, 92);
        AddEnemyUnit(1, 16, 30, false, 9, 92);
        AddEnemyUnit(1, 18, 31, false, 9, 92);
        AddEnemyUnit(1, 20, 32, false, 9, 92);
        AddEnemyUnit(1, 22, 33, false, 9, 92);
        AddEnemyUnit(1, 24, 34, false, 9, 92);

        //1000
        AddEnemyUnit(1, 12, 41, true, 10, 115, false);
        AddEnemyUnit(1, 0, 38, false, 10, 92);
        AddEnemyUnit(1, 2, 39, false, 10, 92);
        AddEnemyUnit(1, 4, 40, false, 10, 92);
        AddEnemyUnit(1, 6, 28, false, 10, 92);
        AddEnemyUnit(1, 8, 29, false, 10, 92);
        AddEnemyUnit(1, 10, 35, false, 10, 92);
        AddEnemyUnit(1, 14, 36, false, 10, 92);
        AddEnemyUnit(1, 16, 30, false, 10, 92);
        AddEnemyUnit(1, 18, 31, false, 10, 92);
        AddEnemyUnit(1, 20, 32, false, 10, 92);
        AddEnemyUnit(1, 22, 33, false, 10, 92);
        AddEnemyUnit(1, 24, 34, false, 10, 92);

        //1000
        AddEnemyUnit(1, 12, 42, true, 11, 115, false);
        AddEnemyUnit(1, 0, 38, false, 11, 92);
        AddEnemyUnit(1, 2, 39, false, 11, 92);
        AddEnemyUnit(1, 4, 40, false, 11, 92);
        AddEnemyUnit(1, 6, 28, false, 11, 92);
        AddEnemyUnit(1, 8, 29, false, 11, 92);
        AddEnemyUnit(1, 10, 35, false, 11, 92);
        AddEnemyUnit(1, 14, 36, false, 11, 92);
        AddEnemyUnit(1, 16, 30, false, 11, 92);
        AddEnemyUnit(1, 18, 31, false, 11, 92);
        AddEnemyUnit(1, 20, 32, false, 11, 92);
        AddEnemyUnit(1, 22, 33, false, 11, 92);
        AddEnemyUnit(1, 24, 34, false, 11, 92);

        //1000
        AddEnemyUnit(1, 12, 44, true, 12, 115, false);
        AddEnemyUnit(1, 0, 38, false, 12, 92);
        AddEnemyUnit(1, 2, 39, false, 12, 92);
        AddEnemyUnit(1, 4, 40, false, 12, 92);
        AddEnemyUnit(1, 6, 28, false, 12, 92);
        AddEnemyUnit(1, 8, 29, false, 12, 92);
        AddEnemyUnit(1, 10, 35, false, 12, 92);
        AddEnemyUnit(1, 14, 36, false, 12, 92);
        AddEnemyUnit(1, 16, 30, false, 12, 92);
        AddEnemyUnit(1, 18, 31, false, 12, 92);
        AddEnemyUnit(1, 20, 32, false, 12, 92);
        AddEnemyUnit(1, 22, 33, false, 12, 92);
        AddEnemyUnit(1, 24, 34, false, 12, 92);

        //1000
        AddEnemyUnit(1, 12, 25, true, 13, 115, false);
        AddEnemyUnit(1, 0, 38, false, 13, 92);
        AddEnemyUnit(1, 2, 39, false, 13, 92);
        AddEnemyUnit(1, 4, 40, false, 13, 92);
        AddEnemyUnit(1, 6, 28, false, 13, 92);
        AddEnemyUnit(1, 8, 29, false, 13, 92);
        AddEnemyUnit(1, 10, 35, false, 13, 92);
        AddEnemyUnit(1, 14, 36, false, 13, 92);
        AddEnemyUnit(1, 16, 30, false, 13, 92);
        AddEnemyUnit(1, 18, 31, false, 13, 92);
        AddEnemyUnit(1, 20, 32, false, 13, 92);
        AddEnemyUnit(1, 22, 33, false, 13, 92);
        AddEnemyUnit(1, 24, 34, false, 13, 92);

        //1000
        AddEnemyUnit(1, 12, 24, true, 14, 115, false);
        AddEnemyUnit(1, 0, 38, false, 14, 92);
        AddEnemyUnit(1, 2, 39, false, 14, 92);
        AddEnemyUnit(1, 4, 40, false, 14, 92);
        AddEnemyUnit(1, 6, 28, false, 14, 92);
        AddEnemyUnit(1, 8, 29, false, 14, 92);
        AddEnemyUnit(1, 10, 35, false, 14, 92);
        AddEnemyUnit(1, 14, 36, false, 14, 92);
        AddEnemyUnit(1, 16, 30, false, 14, 92);
        AddEnemyUnit(1, 18, 31, false, 14, 92);
        AddEnemyUnit(1, 20, 32, false, 14, 92);
        AddEnemyUnit(1, 22, 33, false, 14, 92);
        AddEnemyUnit(1, 24, 34, false, 14, 92);

        //1000
        AddEnemyUnit(1, 12, 54, true, 15, 115, false);
        AddEnemyUnit(1, 0, 38, false, 15, 92);
        AddEnemyUnit(1, 2, 39, false, 15, 92);
        AddEnemyUnit(1, 4, 40, false, 15, 92);
        AddEnemyUnit(1, 6, 28, false, 15, 92);
        AddEnemyUnit(1, 8, 29, false, 15, 92);
        AddEnemyUnit(1, 10, 35, false, 15, 92);
        AddEnemyUnit(1, 14, 36, false, 15, 92);
        AddEnemyUnit(1, 16, 30, false, 15, 92);
        AddEnemyUnit(1, 18, 31, false, 15, 92);
        AddEnemyUnit(1, 20, 32, false, 15, 92);
        AddEnemyUnit(1, 22, 33, false, 15, 92);
        AddEnemyUnit(1, 24, 34, false, 15, 92);

        //1000
        AddEnemyUnit(1, 12, 58, true, 16, 115, false);
        AddEnemyUnit(1, 0, 38, false, 16, 92);
        AddEnemyUnit(1, 2, 39, false, 16, 92);
        AddEnemyUnit(1, 4, 40, false, 16, 92);
        AddEnemyUnit(1, 6, 28, false, 16, 92);
        AddEnemyUnit(1, 8, 29, false, 16, 92);
        AddEnemyUnit(1, 10, 35, false, 16, 92);
        AddEnemyUnit(1, 14, 36, false, 16, 92);
        AddEnemyUnit(1, 16, 30, false, 16, 92);
        AddEnemyUnit(1, 18, 31, false, 16, 92);
        AddEnemyUnit(1, 20, 32, false, 16, 92);
        AddEnemyUnit(1, 22, 33, false, 16, 92);
        AddEnemyUnit(1, 24, 34, false, 16, 92);

        //1000
        AddEnemyUnit(1, 12, 52, true, 17, 115, false);
        AddEnemyUnit(1, 0, 38, false, 17, 92);
        AddEnemyUnit(1, 2, 39, false, 17, 92);
        AddEnemyUnit(1, 4, 40, false, 17, 92);
        AddEnemyUnit(1, 6, 28, false, 17, 92);
        AddEnemyUnit(1, 8, 29, false, 17, 92);
        AddEnemyUnit(1, 10, 35, false, 17, 92);
        AddEnemyUnit(1, 14, 36, false, 17, 92);
        AddEnemyUnit(1, 16, 30, false, 17, 92);
        AddEnemyUnit(1, 18, 31, false, 17, 92);
        AddEnemyUnit(1, 20, 32, false, 17, 92);
        AddEnemyUnit(1, 22, 33, false, 17, 92);
        AddEnemyUnit(1, 24, 34, false, 17, 92);

        for (int i = 0; i < 11; i++)
        {
            GameController.Instance.SetPlayerPlatoon(i);
        }
        maxDeployedPlatoon = 10;
    }

    private void Level30Setup()
    {
        Debug.Log("Level 30 Setup");

        Platoon p0 = new Platoon(0);
        enemyPlatoons[0] = p0;

        Platoon p1 = new Platoon(1);
        enemyPlatoons[1] = p1;

        Platoon p2 = new Platoon(2);
        enemyPlatoons[2] = p2;

        Platoon p3 = new Platoon(3);
        enemyPlatoons[3] = p3;

        Platoon p4 = new Platoon(4);
        enemyPlatoons[4] = p4;

        Platoon p5 = new Platoon(5);
        enemyPlatoons[5] = p5;

        Platoon p6 = new Platoon(6);
        enemyPlatoons[6] = p6;

        Platoon p7 = new Platoon(7);
        enemyPlatoons[7] = p7;

        Platoon p8 = new Platoon(8);
        enemyPlatoons[8] = p8;

        Platoon p9 = new Platoon(9);
        enemyPlatoons[9] = p9;

        Platoon p10 = new Platoon(10);
        enemyPlatoons[10] = p10;

        Platoon p11 = new Platoon(11);
        enemyPlatoons[11] = p11;

        Platoon p12 = new Platoon(12);
        enemyPlatoons[12] = p12;

        Platoon p13 = new Platoon(13);
        enemyPlatoons[13] = p13;

        Platoon p14 = new Platoon(14);
        enemyPlatoons[14] = p14;

        Platoon p15 = new Platoon(15);
        enemyPlatoons[15] = p15;

        Platoon p16 = new Platoon(16);
        enemyPlatoons[16] = p16;

        Platoon p17 = new Platoon(17);
        enemyPlatoons[17] = p17;

        Platoon p18 = new Platoon(18);
        enemyPlatoons[18] = p18;

        Platoon p19 = new Platoon(19);
        enemyPlatoons[19] = p19;

        enemyPlatoonsRemaining = 20;

        //AddEnemyUnit(int charType, int position, int charClass, bool leader, int platoon, int level)


        //1205
        AddEnemyUnit(1, 12, 51, true, 0, 135, true);
        AddEnemyUnit(1, 0, 50, false, 0, 115);
        AddEnemyUnit(1, 2, 49, false, 0, 115);
        AddEnemyUnit(1, 4, 52, false, 0, 115);
        AddEnemyUnit(1, 6, 45, false, 0, 115);
        AddEnemyUnit(1, 8, 54, false, 0, 115);
        AddEnemyUnit(1, 10, 47, false, 0, 115);
        AddEnemyUnit(1, 14, 55, false, 0, 115);
        AddEnemyUnit(1, 16, 48, false, 0, 115);
        AddEnemyUnit(1, 18, 44, false, 0, 115);
        AddEnemyUnit(1, 20, 46, false, 0, 115);
        AddEnemyUnit(1, 22, 55, false, 0, 115);
        AddEnemyUnit(1, 24, 46, false, 0, 115);


        //960
        AddEnemyUnit(1, 12, 45, true, 1, 110, true);
        AddEnemyUnit(1, 0, 27, false, 1, 90);
        AddEnemyUnit(1, 2, 27, false, 1, 90);
        AddEnemyUnit(1, 4, 27, false, 1, 90);
        AddEnemyUnit(1, 6, 27, false, 1, 90);
        AddEnemyUnit(1, 8, 27, false, 1, 90);
        AddEnemyUnit(1, 10, 27, false, 1, 90);
        AddEnemyUnit(1, 14, 27, false, 1, 90);
        AddEnemyUnit(1, 16, 27, false, 1, 90);
        AddEnemyUnit(1, 18, 27, false, 1, 90);
        AddEnemyUnit(1, 20, 27, false, 1, 90);
        AddEnemyUnit(1, 22, 27, false, 1, 90);
        AddEnemyUnit(1, 24, 27, false, 1, 90);

        //960
        AddEnemyUnit(1, 12, 47, true, 2, 110, true);
        AddEnemyUnit(1, 0, 35, false, 2, 90);
        AddEnemyUnit(1, 2, 35, false, 2, 90);
        AddEnemyUnit(1, 4, 35, false, 2, 90);
        AddEnemyUnit(1, 6, 35, false, 2, 90);
        AddEnemyUnit(1, 8, 35, false, 2, 90);
        AddEnemyUnit(1, 10, 35, false, 2, 90);
        AddEnemyUnit(1, 14, 35, false, 2, 90);
        AddEnemyUnit(1, 16, 35, false, 2, 90);
        AddEnemyUnit(1, 18, 35, false, 2, 90);
        AddEnemyUnit(1, 20, 35, false, 2, 90);
        AddEnemyUnit(1, 22, 35, false, 2, 90);
        AddEnemyUnit(1, 24, 35, false, 2, 90);

        //960
        AddEnemyUnit(1, 12, 50, true, 3, 110, true);
        AddEnemyUnit(1, 0, 38, false, 3, 90);
        AddEnemyUnit(1, 2, 38, false, 3, 90);
        AddEnemyUnit(1, 4, 38, false, 3, 90);
        AddEnemyUnit(1, 6, 38, false, 3, 90);
        AddEnemyUnit(1, 8, 38, false, 3, 90);
        AddEnemyUnit(1, 10, 38, false, 3, 90);
        AddEnemyUnit(1, 14, 38, false, 3, 90);
        AddEnemyUnit(1, 16, 38, false, 3, 90);
        AddEnemyUnit(1, 18, 38, false, 3, 90);
        AddEnemyUnit(1, 20, 38, false, 3, 90);
        AddEnemyUnit(1, 22, 38, false, 3, 90);
        AddEnemyUnit(1, 24, 38, false, 3, 90);

        //960
        AddEnemyUnit(1, 12, 46, true, 4, 110, true);
        AddEnemyUnit(1, 0, 32, false, 4, 90);
        AddEnemyUnit(1, 2, 32, false, 4, 90);
        AddEnemyUnit(1, 4, 32, false, 4, 90);
        AddEnemyUnit(1, 6, 32, false, 4, 90);
        AddEnemyUnit(1, 8, 32, false, 4, 90);
        AddEnemyUnit(1, 10, 32, false, 4, 90);
        AddEnemyUnit(1, 14, 32, false, 4, 90);
        AddEnemyUnit(1, 16, 32, false, 4, 90);
        AddEnemyUnit(1, 18, 32, false, 4, 90);
        AddEnemyUnit(1, 20, 32, false, 4, 90);
        AddEnemyUnit(1, 22, 32, false, 4, 90);
        AddEnemyUnit(1, 24, 32, false, 4, 90);

        //960
        AddEnemyUnit(1, 12, 48, true, 5, 110, true);
        AddEnemyUnit(1, 0, 37, false, 5, 90);
        AddEnemyUnit(1, 2, 37, false, 5, 90);
        AddEnemyUnit(1, 4, 37, false, 5, 90);
        AddEnemyUnit(1, 6, 37, false, 5, 90);
        AddEnemyUnit(1, 8, 37, false, 5, 90);
        AddEnemyUnit(1, 10, 37, false, 5, 90);
        AddEnemyUnit(1, 14, 37, false, 5, 90);
        AddEnemyUnit(1, 16, 37, false, 5, 90);
        AddEnemyUnit(1, 18, 37, false, 5, 90);
        AddEnemyUnit(1, 20, 37, false, 5, 90);
        AddEnemyUnit(1, 22, 37, false, 5, 90);
        AddEnemyUnit(1, 24, 37, false, 5, 90);

        //960
        AddEnemyUnit(1, 12, 49, true, 6, 110, true);
        AddEnemyUnit(1, 0, 41, false, 6, 90);
        AddEnemyUnit(1, 2, 41, false, 6, 90);
        AddEnemyUnit(1, 4, 41, false, 6, 90);
        AddEnemyUnit(1, 6, 41, false, 6, 90);
        AddEnemyUnit(1, 8, 41, false, 6, 90);
        AddEnemyUnit(1, 10, 41, false, 6, 90);
        AddEnemyUnit(1, 14, 41, false, 6, 90);
        AddEnemyUnit(1, 16, 41, false, 6, 90);
        AddEnemyUnit(1, 18, 41, false, 6, 90);
        AddEnemyUnit(1, 20, 41, false, 6, 90);
        AddEnemyUnit(1, 22, 41, false, 6, 90);
        AddEnemyUnit(1, 24, 41, false, 6, 90);

        //1000
        AddEnemyUnit(1, 12, 54, true, 7, 115, false);
        AddEnemyUnit(1, 0, 38, false, 7, 95);
        AddEnemyUnit(1, 2, 39, false, 7, 95);
        AddEnemyUnit(1, 4, 40, false, 7, 95);
        AddEnemyUnit(1, 6, 28, false, 7, 95);
        AddEnemyUnit(1, 8, 29, false, 7, 95);
        AddEnemyUnit(1, 10, 35, false, 7, 95);
        AddEnemyUnit(1, 14, 36, false, 7, 95);
        AddEnemyUnit(1, 16, 30, false, 7, 95);
        AddEnemyUnit(1, 18, 31, false, 7, 95);
        AddEnemyUnit(1, 20, 32, false, 7, 95);
        AddEnemyUnit(1, 22, 33, false, 7, 95);
        AddEnemyUnit(1, 24, 34, false, 7, 95);

        //1000
        AddEnemyUnit(1, 12, 55, true, 8, 115, false);
        AddEnemyUnit(1, 0, 38, false, 8, 95);
        AddEnemyUnit(1, 2, 39, false, 8, 95);
        AddEnemyUnit(1, 4, 40, false, 8, 95);
        AddEnemyUnit(1, 6, 28, false, 8, 95);
        AddEnemyUnit(1, 8, 29, false, 8, 95);
        AddEnemyUnit(1, 10, 35, false, 8, 95);
        AddEnemyUnit(1, 14, 36, false, 8, 95);
        AddEnemyUnit(1, 16, 30, false, 8, 95);
        AddEnemyUnit(1, 18, 31, false, 8, 95);
        AddEnemyUnit(1, 20, 32, false, 8, 95);
        AddEnemyUnit(1, 22, 33, false, 8, 95);
        AddEnemyUnit(1, 24, 34, false, 8, 95);

        //1000
        AddEnemyUnit(1, 12, 52, true, 9, 115, false);
        AddEnemyUnit(1, 0, 38, false, 9, 95);
        AddEnemyUnit(1, 2, 39, false, 9, 95);
        AddEnemyUnit(1, 4, 40, false, 9, 95);
        AddEnemyUnit(1, 6, 28, false, 9, 95);
        AddEnemyUnit(1, 8, 29, false, 9, 95);
        AddEnemyUnit(1, 10, 35, false, 9, 95);
        AddEnemyUnit(1, 14, 36, false, 9, 95);
        AddEnemyUnit(1, 16, 30, false, 9, 95);
        AddEnemyUnit(1, 18, 31, false, 9, 95);
        AddEnemyUnit(1, 20, 32, false, 9, 95);
        AddEnemyUnit(1, 22, 33, false, 9, 95);
        AddEnemyUnit(1, 24, 34, false, 9, 95);

        //1000
        AddEnemyUnit(1, 12, 58, true, 10, 115, false);
        AddEnemyUnit(1, 0, 38, false, 10, 95);
        AddEnemyUnit(1, 2, 39, false, 10, 95);
        AddEnemyUnit(1, 4, 40, false, 10, 95);
        AddEnemyUnit(1, 6, 28, false, 10, 95);
        AddEnemyUnit(1, 8, 29, false, 10, 95);
        AddEnemyUnit(1, 10, 35, false, 10, 95);
        AddEnemyUnit(1, 14, 36, false, 10, 95);
        AddEnemyUnit(1, 16, 30, false, 10, 95);
        AddEnemyUnit(1, 18, 31, false, 10, 95);
        AddEnemyUnit(1, 20, 32, false, 10, 95);
        AddEnemyUnit(1, 22, 33, false, 10, 95);
        AddEnemyUnit(1, 24, 34, false, 10, 95);

        //1000
        AddEnemyUnit(1, 12, 44, true, 11, 115, false);
        AddEnemyUnit(1, 0, 38, false, 11, 95);
        AddEnemyUnit(1, 2, 39, false, 11, 95);
        AddEnemyUnit(1, 4, 40, false, 11, 95);
        AddEnemyUnit(1, 6, 28, false, 11, 95);
        AddEnemyUnit(1, 8, 29, false, 11, 95);
        AddEnemyUnit(1, 10, 35, false, 11, 95);
        AddEnemyUnit(1, 14, 36, false, 11, 95);
        AddEnemyUnit(1, 16, 30, false, 11, 95);
        AddEnemyUnit(1, 18, 31, false, 11, 95);
        AddEnemyUnit(1, 20, 32, false, 11, 95);
        AddEnemyUnit(1, 22, 33, false, 11, 95);
        AddEnemyUnit(1, 24, 34, false, 11, 95);

        //1000
        AddEnemyUnit(1, 12, 42, true, 12, 115, false);
        AddEnemyUnit(1, 0, 38, false, 12, 95);
        AddEnemyUnit(1, 2, 39, false, 12, 95);
        AddEnemyUnit(1, 4, 40, false, 12, 95);
        AddEnemyUnit(1, 6, 28, false, 12, 95);
        AddEnemyUnit(1, 8, 29, false, 12, 95);
        AddEnemyUnit(1, 10, 35, false, 12, 95);
        AddEnemyUnit(1, 14, 36, false, 12, 95);
        AddEnemyUnit(1, 16, 30, false, 12, 95);
        AddEnemyUnit(1, 18, 31, false, 12, 95);
        AddEnemyUnit(1, 20, 32, false, 12, 95);
        AddEnemyUnit(1, 22, 33, false, 12, 95);
        AddEnemyUnit(1, 24, 34, false, 12, 95);

        //1000
        AddEnemyUnit(1, 12, 25, true, 13, 115, false);
        AddEnemyUnit(1, 0, 38, false, 13, 95);
        AddEnemyUnit(1, 2, 39, false, 13, 95);
        AddEnemyUnit(1, 4, 40, false, 13, 95);
        AddEnemyUnit(1, 6, 28, false, 13, 95);
        AddEnemyUnit(1, 8, 29, false, 13, 95);
        AddEnemyUnit(1, 10, 35, false, 13, 95);
        AddEnemyUnit(1, 14, 36, false, 13, 95);
        AddEnemyUnit(1, 16, 30, false, 13, 95);
        AddEnemyUnit(1, 18, 31, false, 13, 95);
        AddEnemyUnit(1, 20, 32, false, 13, 95);
        AddEnemyUnit(1, 22, 33, false, 13, 95);
        AddEnemyUnit(1, 24, 34, false, 13, 95);

        //1000
        AddEnemyUnit(1, 12, 24, true, 14, 115, false);
        AddEnemyUnit(1, 0, 38, false, 14, 95);
        AddEnemyUnit(1, 2, 39, false, 14, 95);
        AddEnemyUnit(1, 4, 40, false, 14, 95);
        AddEnemyUnit(1, 6, 28, false, 14, 95);
        AddEnemyUnit(1, 8, 29, false, 14, 95);
        AddEnemyUnit(1, 10, 35, false, 14, 95);
        AddEnemyUnit(1, 14, 36, false, 14, 95);
        AddEnemyUnit(1, 16, 30, false, 14, 95);
        AddEnemyUnit(1, 18, 31, false, 14, 95);
        AddEnemyUnit(1, 20, 32, false, 14, 95);
        AddEnemyUnit(1, 22, 33, false, 14, 95);
        AddEnemyUnit(1, 24, 34, false, 14, 95);

        //1000
        AddEnemyUnit(1, 12, 22, true, 15, 115, false);
        AddEnemyUnit(1, 0, 31, false, 15, 95);
        AddEnemyUnit(1, 2, 28, false, 15, 95);
        AddEnemyUnit(1, 4, 29, false, 15, 95);
        AddEnemyUnit(1, 6, 30, false, 15, 95);
        AddEnemyUnit(1, 8, 29, false, 15, 95);
        AddEnemyUnit(1, 10, 28, false, 15, 95);
        AddEnemyUnit(1, 14, 31, false, 15, 95);
        AddEnemyUnit(1, 16, 30, false, 15, 95);
        AddEnemyUnit(1, 18, 31, false, 15, 95);
        AddEnemyUnit(1, 20, 28, false, 15, 95);
        AddEnemyUnit(1, 22, 29, false, 15, 95);
        AddEnemyUnit(1, 24, 30, false, 15, 95);

        //1000
        AddEnemyUnit(1, 12, 56, true, 16, 115, false);
        AddEnemyUnit(3, 5, 9, false, 16, 100);
        AddEnemyUnit(3, 8, 9, false, 16, 100);
        AddEnemyUnit(3, 20, 9, false, 16, 100);
        AddEnemyUnit(3, 2, 10, false, 16, 90);
        AddEnemyUnit(3, 22, 10, false, 16, 90);
        AddEnemyUnit(3, 10, 10, false, 16, 90);
        AddEnemyUnit(3, 14, 10, false, 16, 90);
        AddEnemyUnit(3, 24, 10, false, 16, 90);
        AddEnemyUnit(3, 18, 10, false, 16, 90);

        //1000
        AddEnemyUnit(1, 12, 56, true, 17, 115, false);
        AddEnemyUnit(3, 5, 11, false, 17, 100);
        AddEnemyUnit(3, 8, 11, false, 17, 100);
        AddEnemyUnit(3, 20, 11, false, 17, 100);
        AddEnemyUnit(3, 2, 10, false, 17, 90);
        AddEnemyUnit(3, 22, 10, false, 17, 90);
        AddEnemyUnit(3, 10, 10, false, 17, 90);
        AddEnemyUnit(3, 14, 10, false, 17, 90);
        AddEnemyUnit(3, 24, 10, false, 17, 90);
        AddEnemyUnit(3, 18, 10, false, 17, 90);

        //1000
        AddEnemyUnit(1, 12, 56, true, 18, 115, false);
        AddEnemyUnit(3, 5, 12, false, 18, 100);
        AddEnemyUnit(3, 8, 12, false, 18, 100);
        AddEnemyUnit(3, 20, 12, false, 18, 100);
        AddEnemyUnit(3, 2, 10, false, 18, 90);
        AddEnemyUnit(3, 22, 10, false, 18, 90);
        AddEnemyUnit(3, 10, 10, false, 18, 90);
        AddEnemyUnit(3, 14, 10, false, 18, 90);
        AddEnemyUnit(3, 24, 10, false, 18, 90);
        AddEnemyUnit(3, 18, 10, false, 18, 90);

        //1000
        AddEnemyUnit(1, 22, 56, true, 19, 120, false);
        AddEnemyUnit(3, 16, 13, false, 19, 100);
        AddEnemyUnit(3, 20, 10, false, 19, 90);
        AddEnemyUnit(3, 2, 10, false, 19, 90);
        AddEnemyUnit(3, 0, 10, false, 19, 90);
        AddEnemyUnit(3, 10, 10, false, 19, 90);
        AddEnemyUnit(3, 14, 10, false, 19, 90);
        AddEnemyUnit(3, 24, 10, false, 19, 90);
        AddEnemyUnit(3, 4, 10, false, 19, 90);
        AddEnemyUnit(3, 3, 10, false, 19, 90);
        AddEnemyUnit(3, 1, 10, false, 19, 90);


        for (int i = 0; i < 11; i++)
        {
            GameController.Instance.SetPlayerPlatoon(i);
        }
        maxDeployedPlatoon = 10;
    }

    private void Level1Won()
    {
        if (ArmyData.Instance.platoons.Count > 1)
        {
            return;
        }

        Platoon p1 = new Platoon(1);
        ArmyData.Instance.platoons.Add(p1);

        Platoon p2 = new Platoon(2);
        ArmyData.Instance.platoons.Add(p2);

        Platoon p3 = new Platoon(3);
        ArmyData.Instance.platoons.Add(p3);
        for (int i = 0; i < 13; i++)
        {
            HumanData h = new HumanData();
            h.SetPlatoonPosition(1, 2 * i);

            if (i == 12)
            {
                h.SetClass(1, 1);
                p1.SetLeader(h);
                h.LevelUp();
            }

            ArmyData.Instance.units.Add(h);
            p1.AddUnit(h, 2 * i);
        }

        for (int i = 0; i < 13; i++)
        {
            HumanData h = new HumanData();
            h.SetPlatoonPosition(2, 2 * i);

            if (i == 0)
            {
                h.SetClass(3, 2);
                p2.SetLeader(h);
                h.LevelUp();
            }

            ArmyData.Instance.units.Add(h);
            p2.AddUnit(h, 2 * i);
        }

        for (int i = 0; i < 13; i++)
        {
            HumanData h = new HumanData();
            h.SetPlatoonPosition(3, 2 * i);

            if (i == 11)
            {
                h.SetClass(2, 3);
                p3.SetLeader(h);
                h.LevelUp();
            }

            ArmyData.Instance.units.Add(h);
            p3.AddUnit(h, 2 * i);
        }
    }

    private void Level2Won()
    {
        Debug.Log("Level2Won");
        if (ArmyData.Instance.platoons.Count > 4)
        {
            return;
        }
        else
        {
            Debug.Log("Level2Won new platoons, army size: " + ArmyData.Instance.platoons.Count);
        }

        Platoon p1 = new Platoon(4);
        ArmyData.Instance.platoons.Add(p1);

        Platoon p2 = new Platoon(5);
        ArmyData.Instance.platoons.Add(p2);

        Platoon p3 = new Platoon(6);
        ArmyData.Instance.platoons.Add(p3);

        for (int i = 0; i < 13; i++)
        {
            HumanData h = new HumanData();
            h.SetPlatoonPosition(4, 2 * i);

            if (i == 10)
            {
                h.SetClass(1, 4);
                p1.SetLeader(h);
                h.LevelUp();
                h.LevelUp();
            }

            if (i == 2)
            {
                h.SetClass(3, 4);
                h.LevelUp();
            }

            if (i == 12)
            {
                h.SetClass(2, 4);
                h.LevelUp();
            }
            h.LevelUp();
            ArmyData.Instance.units.Add(h);
            p1.AddUnit(h, 2 * i);
        }

        for (int i = 0; i < 13; i++)
        {
            HumanData h = new HumanData();
            h.SetPlatoonPosition(5, 2 * i);

            if (i == 2)
            {
                h.SetClass(3, 5);
                p2.SetLeader(h);
                h.LevelUp();
                h.LevelUp();
            }

            if (i == 10)
            {
                h.SetClass(1, 5);
                h.LevelUp();
            }

            if (i == 12)
            {
                h.SetClass(2, 5);
                h.LevelUp();
            }
            h.LevelUp();
            ArmyData.Instance.units.Add(h);
            p2.AddUnit(h, 2 * i);
        }

        for (int i = 0; i < 13; i++)
        {
            HumanData h = new HumanData();
            h.SetPlatoonPosition(6, 2 * i);

            if (i == 12)
            {
                h.SetClass(2, 6);
                p3.SetLeader(h);
                h.LevelUp();
                h.LevelUp();
            }

            if (i == 10)
            {
                h.SetClass(1, 6);
                h.LevelUp();
            }

            if (i == 2)
            {
                h.SetClass(3, 6);
                h.LevelUp();
            }
            h.LevelUp();
            ArmyData.Instance.units.Add(h);
            p3.AddUnit(h, 2 * i);
        }
    }

    private void Level3Won()
    {
        Debug.Log("Level3Won");
        if (ArmyData.Instance.platoons.Count > 7)
        {
            return;
        }

        Platoon p1 = new Platoon(7);
        ArmyData.Instance.platoons.Add(p1);

        for (int i = 0; i < 13; i++)
        {
            if (i == 0 || i == 2)
            {
                AnimalData a = new AnimalData();
                a.SetPlatoonPosition(7, 2 * i);
                a.SetClass(2, 7);
                a.LevelUp();
                a.LevelUp();
                a.LevelUp();
                a.LevelUp();
                ArmyData.Instance.units.Add(a);
                p1.AddUnit(a, 2 * i);
            }
            else if (i == 5 || i == 7)
            {
                AnimalData a = new AnimalData();
                a.SetPlatoonPosition(7, 2 * i);
                a.SetClass(1, 7);
                a.LevelUp();
                a.LevelUp();
                a.LevelUp();
                a.LevelUp();
                ArmyData.Instance.units.Add(a);
                p1.AddUnit(a, 2 * i);
            }
            else if (i == 10 || i == 12)
            {
                AnimalData a = new AnimalData();
                a.SetPlatoonPosition(7, 2 * i);
                a.SetClass(0, 7);
                a.LevelUp();
                a.LevelUp();
                a.LevelUp();
                a.LevelUp();
                ArmyData.Instance.units.Add(a);
                p1.AddUnit(a, 2 * i);
            }
            else if (i == 6)
            {
                HumanData h = new HumanData();
                h.SetPlatoonPosition(7, 2 * i);
                p1.SetLeader(h);
                h.LevelUp();
                h.LevelUp();
                h.LevelUp();
                h.LevelUp();
                h.LevelUp();
                h.LevelUp();
                h.LevelUp();
                ArmyData.Instance.units.Add(h);
                p1.AddUnit(h, 2 * i);
            }
            else
            {
                HumanData h = new HumanData();
                h.SetPlatoonPosition(7, 2 * i);
                h.LevelUp();
                h.LevelUp();
                ArmyData.Instance.units.Add(h);
                p1.AddUnit(h, 2 * i);
            }

        }
    }

    private void Level4Won()
    {
        Debug.Log("Level4Won");
        if (ArmyData.Instance.platoons.Count > 8)
        {
            return;
        }

        Platoon p1 = new Platoon(8);
        ArmyData.Instance.platoons.Add(p1);

        for (int i = 0; i < 13; i++)
        {
            if (i == 0 || i == 2)
            {
                AnimalData a = new AnimalData();
                a.SetPlatoonPosition(8, 2 * i);
                a.SetClass(2, 8);
                LevelPlayerUnit(a, 6);
                ArmyData.Instance.units.Add(a);
                p1.AddUnit(a, 2 * i);
            }
            else if (i == 5 || i == 7)
            {
                AnimalData a = new AnimalData();
                a.SetPlatoonPosition(8, 2 * i);
                a.SetClass(1, 8);
                LevelPlayerUnit(a, 6);
                ArmyData.Instance.units.Add(a);
                p1.AddUnit(a, 2 * i);
            }
            else if (i == 10 || i == 12)
            {
                AnimalData a = new AnimalData();
                a.SetPlatoonPosition(8, 2 * i);
                a.SetClass(0, 8);
                LevelPlayerUnit(a, 6);
                ArmyData.Instance.units.Add(a);
                p1.AddUnit(a, 2 * i);
            }
            else if (i == 6)
            {
                HumanData h = new HumanData();
                h.SetPlatoonPosition(8, 2 * i);
                p1.SetLeader(h);
                LevelPlayerUnit(h, 9);
                ArmyData.Instance.units.Add(h);
                p1.AddUnit(h, 2 * i);
            }
            else
            {
                HumanData h = new HumanData();
                h.SetPlatoonPosition(8, 2 * i);
                LevelPlayerUnit(h, 4);
                ArmyData.Instance.units.Add(h);
                p1.AddUnit(h, 2 * i);
            }

        }       

        
    }

    public void LevelPlayerUnit(UnitData u, int levels)
    {
        for(int i = levels; i > 0; i--)
        {
            u.LevelUp();
        }
    }

    private void Level5Won()
    {
    }

    private void Level6Won()
    {
    }

    private void Level7Won()
    {

    }

    private void Level8Won()
    {

    }

    private void Level9Won()
    {

    }

    private void Level10Won()
    {
    }

    private void Level11Won()
    {
    }

    private void Level12Won()
    {
        Debug.Log("Level12Won");
        if (ArmyData.Instance.platoons.Count > 9)
        {
            return;
        }

        Platoon p1 = new Platoon(9);
        ArmyData.Instance.platoons.Add(p1);

        for (int i = 0; i < 13; i++)
        {
            if (i == 0 || i == 2)
            {
                MythData a = new MythData();
                a.SetPlatoonPosition(9, 2 * i);
                a.SetClass(2, 9);
                LevelPlayerUnit(a, 15);
                ArmyData.Instance.units.Add(a);
                p1.AddUnit(a, 2 * i);
            }
            else if (i == 5 || i == 7)
            {
                MythData a = new MythData();
                a.SetPlatoonPosition(9, 2 * i);
                a.SetClass(3, 9);
                LevelPlayerUnit(a, 15);
                ArmyData.Instance.units.Add(a);
                p1.AddUnit(a, 2 * i);
            }
            else if (i == 10 || i == 12)
            {
                MythData a = new MythData();
                a.SetPlatoonPosition(9, 2 * i);
                a.SetClass(1, 9);
                LevelPlayerUnit(a, 15);
                ArmyData.Instance.units.Add(a);
                p1.AddUnit(a, 2 * i);
            }
            else if (i == 1 || i == 11)
            {
                MythData a = new MythData();
                a.SetPlatoonPosition(9, 2 * i);
                a.SetClass(0, 9);
                LevelPlayerUnit(a, 15);
                ArmyData.Instance.units.Add(a);
                p1.AddUnit(a, 2 * i);
            }
            else if (i == 6)
            {
                HumanData h = new HumanData();
                h.SetPlatoonPosition(9, 2 * i);
                p1.SetLeader(h);
                LevelPlayerUnit(h, 9);
                h.SetClass(1, 9);
                LevelPlayerUnit(h, 9);
                ArmyData.Instance.units.Add(h);
                p1.AddUnit(h, 2 * i);
            }
            else
            {
                HumanData h = new HumanData();
                h.SetPlatoonPosition(9, 2 * i);
                LevelPlayerUnit(h, 9);
                ArmyData.Instance.units.Add(h);
                p1.AddUnit(h, 2 * i);
            }

        }
    }

    private void Level13Won()
    {
        Debug.Log("Level13Won");
        if(ArmyData.Instance.platoons.Count > 10)
        {
            return;
        }
        Platoon p1 = new Platoon(10);
        ArmyData.Instance.platoons.Add(p1);

        for (int i = 0; i < 13; i++)
        {
            if (i == 0 || i == 2)
            {
                MythData a = new MythData();
                a.SetPlatoonPosition(10, 2 * i);
                a.SetClass(2, 10);
                LevelPlayerUnit(a, 17);
                ArmyData.Instance.units.Add(a);
                p1.AddUnit(a, 2 * i);
            }
            else if (i == 5 || i == 7)
            {
                MythData a = new MythData();
                a.SetPlatoonPosition(10, 2 * i);
                a.SetClass(3, 10);
                LevelPlayerUnit(a, 17);
                ArmyData.Instance.units.Add(a);
                p1.AddUnit(a, 2 * i);
            }
            else if (i == 10 || i == 12)
            {
                MythData a = new MythData();
                a.SetPlatoonPosition(10, 2 * i);
                a.SetClass(1, 10);
                LevelPlayerUnit(a, 17);
                ArmyData.Instance.units.Add(a);
                p1.AddUnit(a, 2 * i);
            }
            else if (i == 1 || i == 11)
            {
                MythData a = new MythData();
                a.SetPlatoonPosition(10, 2 * i);
                a.SetClass(0, 10);
                LevelPlayerUnit(a, 17);
                ArmyData.Instance.units.Add(a);
                p1.AddUnit(a, 2 * i);
            }
            else if (i == 6)
            {
                HumanData h = new HumanData();
                h.SetPlatoonPosition(10, 2 * i);
                p1.SetLeader(h);
                LevelPlayerUnit(h, 10);
                h.SetClass(2, 10);
                LevelPlayerUnit(h, 10);
                ArmyData.Instance.units.Add(h);
                p1.AddUnit(h, 2 * i);
            }
            else
            {
                HumanData h = new HumanData();
                h.SetPlatoonPosition(10, 2 * i);
                LevelPlayerUnit(h, 10);
                ArmyData.Instance.units.Add(h);
                p1.AddUnit(h, 2 * i);
            }

        }
    }

    private void Level14Won()
    {
        /*Debug.Log("Level14Won");
        Platoon p1 = new Platoon(12);
        ArmyData.Instance.platoons.Add(p1);

        for (int i = 0; i < 13; i++)
        {
            if (i == 0 || i == 2)
            {
                MythData a = new MythData();
                a.SetPlatoonPosition(12, 2 * i);
                a.SetClass(2);
                LevelPlayerUnit(a, 20);
                ArmyData.Instance.units.Add(a);
                p1.AddUnit(a, 2 * i);
            }
            else if (i == 5 || i == 7)
            {
                MythData a = new MythData();
                a.SetPlatoonPosition(12, 2 * i);
                a.SetClass(3);
                LevelPlayerUnit(a, 20);
                ArmyData.Instance.units.Add(a);
                p1.AddUnit(a, 2 * i);
            }
            else if (i == 10 || i == 12)
            {
                MythData a = new MythData();
                a.SetPlatoonPosition(12, 2 * i);
                a.SetClass(1);
                LevelPlayerUnit(a, 20);
                ArmyData.Instance.units.Add(a);
                p1.AddUnit(a, 2 * i);
            }
            else if (i == 1 || i == 11)
            {
                MythData a = new MythData();
                a.SetPlatoonPosition(12, 2 * i);
                a.SetClass(0);
                LevelPlayerUnit(a, 20);
                ArmyData.Instance.units.Add(a);
                p1.AddUnit(a, 2 * i);
            }
            else if (i == 6)
            {
                HumanData h = new HumanData();
                h.SetPlatoonPosition(12, 2 * i);
                p1.SetLeader(h);
                LevelPlayerUnit(h, 10);
                h.SetClass(2);
                LevelPlayerUnit(h, 10);
                ArmyData.Instance.units.Add(h);
                p1.AddUnit(h, 2 * i);
            }
            else
            {
                HumanData h = new HumanData();
                h.SetPlatoonPosition(12, 2 * i);
                LevelPlayerUnit(h, 10);
                ArmyData.Instance.units.Add(h);
                p1.AddUnit(h, 2 * i);
            }

        }*/
    }

    private void Level15Won()
    {

    }

    private void Level16Won()
    {
    }

    private void Level17Won()
    {
    }

    private void Level18Won()
    {
    }

    private void Level19Won()
    {
    }

    private void Level20Won()
    {
    }

    private void Level21Won()
    {
    }

    private void Level22Won()
    {
    }

    private void Level23Won()
    {
    }

    private void Level24Won()
    {
    }

    private void Level25Won()
    {
    }

    private void Level26Won()
    {
    }

    private void Level27Won()
    {
    }

    private void Level28Won()
    {
    }

    private void Level29Won()
    {
    }

    private void Level30Won()
    {
    }

    public string GetEnemyUnitJSONString()
    {
        string jsonStr = "";
        for (int i = 0; i < enemyUnits.Count; i++)
        {
            jsonStr = jsonStr + enemyUnits[i].CreateJSON() + "xghx";
        }

        return jsonStr;        
    }

    public void LoadMidLevelEnemyUnitData(string enemyStr)
    {
       string[] separator = new string[] { "xghx" };
       string[] result;
       result = enemyStr.Split(separator, System.StringSplitOptions.None);
       foreach (string str in result)
       {
           HumanData d = JsonUtility.FromJson<HumanData>(str);
           if (d != null && d.GetCharType() == 2)
           {
               AnimalData e = JsonUtility.FromJson<AnimalData>(str);
               enemyUnits.Add(e);
           }
           else if (d != null && d.GetCharType() == 3)
           {
               MythData f = JsonUtility.FromJson<MythData>(str);
               enemyUnits.Add(f);
           }
           else if (d != null)
           {
               enemyUnits.Add(d);
               if (d.IsHero() && d.platoonNum == -1)
               {
                   GameController.Instance.heroInPlatoon = false;
               }
           }
       }

       foreach (UnitData u in enemyUnits)
       {
           if (u.platoonNum != -1)
           {
                if (enemyPlatoons[u.platoonNum] == null)
                {
                    enemyPlatoons[u.platoonNum] = new Platoon(u.platoonNum);

                    enemyPlatoonsRemaining++;
                    startingEnemyPlatoonCount++;
                }
                enemyPlatoons[u.platoonNum].AddUnit(u, u.positionNum);


                if (u.specialCharNum == 2)
                {
                    enemyPlatoons[u.platoonNum].SetLeader(u);
                }
            }
       }
    }

    public string GetTownString()
    {
        string jsonStr = "";
        for (int i = 0; i < GameController.Instance.towns.Count; i++)
        {
            jsonStr = jsonStr + GameController.Instance.towns[i].OwnedByWho() + "xghx";
        }
       
        return jsonStr;
    }

    public void SetTownOwnership(string townStr)
    {
        string[] separator = new string[] { "xghx" };
        string[] result;
        result = townStr.Split(separator, System.StringSplitOptions.None);
        int i = 0;
        foreach (string str in result)
        {
            if(str.Length > 0)
            {
                GameController.Instance.towns[i].SetOwnership(Convert.ToInt16(str));
                i++;
            }
            
        }
    }

    public string GetAmbushString()
    {
        string jsonStr = "";
        for (int i = 0; i < GameController.Instance.ambush.Count; i++)
        {
            jsonStr = jsonStr + GameController.Instance.ambush[i].used + "xghx";
        }

        return jsonStr;
    }

    public void SetAmbushUsed(string townStr)
    {
        string[] separator = new string[] { "xghx" };
        string[] result;
        result = townStr.Split(separator, System.StringSplitOptions.None);
        int i = 0;
        foreach (string str in result)
        {
            if (str.Length > 0)
            {
                GameController.Instance.ambush[i].SetUsed(Convert.ToInt16(str));
                i++;
            }

        }
    }

    public string GetPlayerLeaderLocationsString()
    {
        string leaderString = "";
        Debug.Log("Getting player leaders, starting count: " + startingPlayerPlatoonCount);
        for (int i = 0; i < startingPlayerPlatoonCount; i++)
        {
            //Check if platoon is dead
            if (playerPlatoonControllers[i] == null)
            {
                leaderString = leaderString + new Vector3(-1, -1, -1) + "xghx";
            }
            else
            {
                leaderString = leaderString + playerPlatoonControllers[i].gameObject.transform.position + "xghx";
            }
            
        }

        return leaderString;
    }

    public string GetEnemyLeaderLocationsString()
    {
        string leaderString = "";
        Debug.Log("Getting enemy leaders, starting count: " + startingEnemyPlatoonCount);
        for (int i = 0; i < startingEnemyPlatoonCount; i++)
        {
            //Check if platoon is dead
            if (enemyPlatoonControllers[i] == null)
            {
                leaderString = leaderString + new Vector3(-1, -1, -1) + "xghx";
            }
            else
            {
                leaderString = leaderString + enemyPlatoonControllers[i].gameObject.transform.position + "xghx";
            }

        }

        return leaderString;
    }

    public void SetLeaderLocations(string playerLeaderString, string enemyLeaderString)
    {
        string[] separator = new string[] { "xghx" };
        string[] result;
        result = playerLeaderString.Split(separator, System.StringSplitOptions.None);
        int i = 0;
        foreach (string str in result)
        {
            if (str.Length > 0)
            {
                Vector3 pVector = StringToVector3(str);
                if(pVector.Equals(new Vector3(-1, -1, -1)))
                {
                    Debug.Log("Player Platoon: " + i + " Destroyed");
                    playerPlatoonsRemaining--;
                    Destroy(playerPlatoonControllers[i].gameObject);
                    playerPlatoonControllers[i] = null;
                    playerPlatoons[i] = null;
                }
                else
                {
                    Debug.Log("Player Platoon: " + i + " At: " + pVector);
                    playerPlatoonControllers[i].gameObject.transform.SetPositionAndRotation(pVector, Quaternion.identity);
                }
            }
            i++;
        }

        result = enemyLeaderString.Split(separator, System.StringSplitOptions.None);
        i = 0;
        foreach (string str in result)
        {
            if (str.Length > 0)
            {
                Vector3 eVector = StringToVector3(str);
               
                if (eVector.Equals(new Vector3(-1, -1, -1)))
                {
                    Debug.Log("Enemy Platoon: " + i + " Destroyed");
                    enemyPlatoonsRemaining--;
                    Destroy(enemyPlatoonControllers[i].gameObject);
                    enemyPlatoonControllers[i] = null;
                    enemyPlatoons[i] = null;
                }
                else
                {
                    Debug.Log("Enemy Platoon: " + i + " At: " + eVector);
                    enemyPlatoonControllers[i].gameObject.transform.SetPositionAndRotation(eVector, Quaternion.identity);
                }
            }
            i++;
        }
    }

    public static Vector3 StringToVector3(string sVector)
    {
        // Remove the parentheses
        if (sVector.StartsWith("(") && sVector.EndsWith(")"))
        {
            sVector = sVector.Substring(1, sVector.Length - 2);
        }

        // split the items
        string[] sArray = sVector.Split(',');

        // store as a Vector3
        Vector3 result = new Vector3(
            float.Parse(sArray[0]),
            float.Parse(sArray[1]),
            float.Parse(sArray[2]));

        return result;
    }
}
