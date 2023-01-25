using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour {

    public static BattleController Instance;

    //new computer/Unity version requires a different speed?
    private float speedAdjust = 5;

    private bool countingSpeed = true;

    public GameObject smallTargetHalo;
    public GameObject smallSelectHalo;
    public GameObject mediumTargetHalo;
    public GameObject mediumSelectHalo;
    public GameObject largeTargetHalo;
    public GameObject largeSelectHalo;

    //For version 2
    private bool autoTurn;
    //private List<int> targetPositions = new List<int>();
    private bool[] targetPositions = new bool[25];
    private bool targetSelected = false;
    private int playerTargetNum = -1;
    private int skillSelected = 1;
    private bool globalSelfSkill = false;
    private bool attacking = false;
    private bool healSkill = false;
    //private GameObject[] enemyAvailableTargetArray = new GameObject[25];
    //private GameObject[] playerAvailableTargetArray = new GameObject[25];
    //private GameObject[] enemyTargetArray = new GameObject[25];
    //private GameObject[] playerTargetArray = new GameObject[25];

    private bool assaultAttack = false;
    
    public List<UnitData> playerUnitList = new List<UnitData>();
    private UnitData[] playerUnits = new UnitData[25];
    private GameObject[] playerArray = new GameObject[25];
    //private UnitFightScript[] playerFightScripts = new UnitFightScript[25];
    public List<UnitData> enemyUnitList = new List<UnitData>();
    private UnitData[] enemyUnits = new UnitData[25];
    private GameObject[] enemyArray = new GameObject[25];
    //private UnitFightScript[] enemyFightScripts = new UnitFightScript[25];

    public GameObject[] enemyHealthBars = new GameObject[25];
    public GameObject[] playerHealthBars = new GameObject[25];

    private float normalMoveTime = 70f;
    private float sneakMoveTime = 300f;
    private float projectileMoveTime = 350f;
    private float moveDelay = 1.1f;
    private float animationDelay = .25f;
    private float magicDelay = .25f;
    private float destroyDelay = .5f;

    private List<GameObject> destroyedObjects = new List<GameObject>();
    private GameObject hurtObject;

    private UnitData movingUnit;
    private bool playerUnitMoving = true;
    private Vector3 movingTargetPosition;
    //private Vector3 startingPosition;

    //private UnitData targetedUnit;

    private bool retreat = false;

    private int playerDamage = 0;
    private int enemyDamage = 0;
    private int playerCount = 0;
    private int enemyCount = 0;

    public int totalSpeedCounted = 0;
    private int totalSpeedTurnLimit;
    private int totalSpeedBattleLimit;

    //private bool corruptedUnit = false;

    private bool playerTarget = false;

    private bool autoBattle = false;
    private bool fightOver = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            DestroyImmediate(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this);
    }

    // Use this for initialization
    void Start () {
        totalSpeedCounted = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (GameController.Instance.inBattle) {

            /*//Fix with UI Button Instead
            if (Input.GetKeyDown(KeyCode.L))
            {

                retreat = true;
                
            }*/

            if (countingSpeed)
            {
                FightEndCheck(false);
                if (GameController.Instance.inBattle)
                {
                    SpeedCounter();
                }
            }
           
        }        
    }

    public void ReadyButton()
    {
        if ((playerTargetNum != -1 || globalSelfSkill) && !attacking)
        {
            attacking = true;
            TakeTurn();
        }
    }

    public void EasyMode()
    {
        //normalMoveTime /= 2;
        //sneakMoveTime /= 2;
        //projectileMoveTime /= 2;
        //moveDelay /= 2;
        //animationDelay /= 2;
        //magicDelay /= 2;
        //destroyDelay /= 2;
    }

    public void StartBattle()
    {
        totalSpeedTurnLimit = LevelController.Instance.levelNum * 100;
        totalSpeedBattleLimit = 10 * totalSpeedTurnLimit;
    }

    public int GetTotalSpeedTurnLimit()
    {
        return totalSpeedTurnLimit;
    }

    public void Retreat()
    {
        retreat = true;
        GameController.Instance.FightReturn(false, false);
        FightOver();
    }

    public void PauseBattle()
    {
        countingSpeed = false;
    }

    public void UnpauseBattle()
    {
        countingSpeed = true;
    }

    private void FightEndCheckDelay()
    {
        FightEndCheck(true);
    }

    private void FightEndCheck(bool alreadyInvoked)
    {
        if (!GameController.Instance.autoBattle && !alreadyInvoked && (playerCount <= 0 || LevelController.Instance.heroDead))
        {
            Invoke("FightEndCheckDelay", 2);
        }
        else
        {
            //if either unit is wiped out
            if (playerCount <= 0)
            {
                FightOver();
                GameController.Instance.FightReturn(false, true);
            }
            else if (enemyCount <= 0)
            {
                FightOver();
                GameController.Instance.FightReturn(true, true);
            }


            //time is over
            if (totalSpeedCounted >= totalSpeedBattleLimit)
            {
                CheckForWinner();
            }


            if (LevelController.Instance.heroDead)
            {

                GameController.Instance.FightReturn(false, false);
                FightOver();
            }
        }
        
        
    }

    private void CheckForWinner()
    {
        if (playerDamage > enemyDamage)
        {
            GameController.Instance.FightReturn(false, false);

        }
        else 
        {
            GameController.Instance.FightReturn(true, false);
        }
        FightOver();
    }

    private void FightOver()
    {
        CancelInvoke();
        StopAllCoroutines();
        fightOver = true;
        foreach (UnitData u in enemyUnitList)
        {
            
            Destroy(enemyArray[u.positionNum]);
            //u.SetSpeedCount(0);
        }
        foreach (UnitData u in playerUnitList)
        {
            Destroy(playerArray[u.positionNum]);
            //u.SetSpeedCount(0);
        }

        for(int i = 0; i < 25; i++)
        {
            enemyUnits[i] = null;
            playerUnits[i] = null;
        }

        enemyUnitList.Clear();
        playerUnitList.Clear();
        retreat = false;

        //v2
        movingUnit = null;
        countingSpeed = true;
        ClearTargets();
        targetSelected = false;
        playerTargetNum = -1;
        skillSelected = 1;
        globalSelfSkill = false;
        attacking = false;
        healSkill = false;

        //assaultAttack = false;
        playerDamage = 0;
        enemyDamage = 0;
        playerCount = 0;
        enemyCount = 0;
        totalSpeedCounted = 0;
    }

    public void AddPlayerUnit(UnitData u, GameObject g)
    {
        playerUnitList.Add(u);
        playerCount++;
        playerArray[u.positionNum] = g;
        g.GetComponent<UnitFightScript>().SetPositionNum(u.positionNum);
        playerUnits[u.positionNum] = u;
    }

    public void AddEnemyUnit(UnitData u, GameObject g)
    {
        enemyUnitList.Add(u);
        enemyCount++;
        g.GetComponent<UnitFightScript>().SetPositionNum(u.positionNum);
        enemyArray[u.positionNum] = g;
        enemyUnits[u.positionNum] = u;
    }

    private void SpeedCounter()
    {
        UnitData nextUnit = playerUnitList[0];
        int nextSpeed = -1;
        bool playerUnit = false;
        countingSpeed = false;
        totalSpeedCounted++;
        //corruptedUnit = false;

        if (IsUnitSpeedFull())
        {
            return;
        }

        foreach (UnitData p in playerUnitList)
        {
            if (!p.isDead())
            {
                int newSpeed = p.GetSpeedCount() + p.GetSpeed();
                p.SetSpeedCount(newSpeed);
                if (newSpeed > nextSpeed)
                {
                    nextUnit = p;
                    nextSpeed = newSpeed;
                    playerUnit = true;
                    playerTarget = false;
                }
            }
            
        }

        foreach(UnitData e in enemyUnitList)
        {
            if (!e.isDead())
            {
                int newSpeed = e.GetSpeedCount() + e.GetSpeed();
                e.SetSpeedCount(newSpeed);
                if (newSpeed > nextSpeed)
                {
                    nextUnit = e;
                    nextSpeed = newSpeed;
                    playerUnit = false;
                    playerTarget = true;
                }
            }
            
        }

        if(nextSpeed < totalSpeedTurnLimit && !CanvasController.Instance.battlePaused)
        {
            countingSpeed = true;
        }
        else
        {            
            if (!nextUnit.isDead())
            {
                CanvasController.Instance.ClearTexts();
                CanvasController.Instance.battleUnitText.text = nextUnit.unitName + " (" + nextUnit.GetCurrentLevel() + "/" + nextUnit.totalLevels + ")";
                if (playerUnit && !GameController.Instance.autoBattle)
                {
                    StartTurn(nextUnit, playerUnit);
                }
                else
                {
                    nextUnit.TakeTurn();
                    AutoTurn(nextUnit, playerUnit);

                }
                nextUnit.OffNormal();
            }         
        }
    }

    private bool IsUnitSpeedFull()
    {
        UnitData nextUnit = playerUnitList[0];
        int nextSpeed = -1;
        bool playerUnit = false;

        foreach (UnitData p in playerUnitList)
        {
            if (!p.isDead())
            {
                int newSpeed = p.GetSpeedCount();
                if (newSpeed > nextSpeed)
                {
                    nextUnit = p;
                    nextSpeed = newSpeed;
                    playerUnit = true;
                    playerTarget = false;
                    p.UpdateSpeedBar();
                }
            }

        }

        foreach (UnitData e in enemyUnitList)
        {
            if (!e.isDead())
            {
                int newSpeed = e.GetSpeedCount();
                if (newSpeed > nextSpeed)
                {
                    nextUnit = e;
                    e.UpdateSpeedBar();
                    nextSpeed = newSpeed;
                    playerUnit = false;
                    playerTarget = true;
                }
            }

        }

        if (nextSpeed < totalSpeedTurnLimit && !CanvasController.Instance.battlePaused)
        {
            return false;
        }
        else
        {
            if (!nextUnit.isDead())
            {
                CanvasController.Instance.ClearTexts();                
                CanvasController.Instance.battleUnitText.text = nextUnit.unitName + " (" + nextUnit.GetCurrentLevel() + "/" + nextUnit.totalLevels + ")";
                

                if (playerUnit && !GameController.Instance.autoBattle)
                {
                    StartTurn(nextUnit, playerUnit);
                }
                else
                {
                    nextUnit.TakeTurn();
                    AutoTurn(nextUnit, playerUnit);

                }
                nextUnit.OffNormal();
            }
            return true;
        }

        
    }

    private void AutoTurn(UnitData unit, bool playerUnit)
    {
        //assaultAttack = false;
        movingUnit = unit;
        autoTurn = true;
        if (!fightOver)
        {
            SkillSelect(unit, playerUnit);
        }

        if (!GameController.Instance.autoBattle)
        {
            Invoke("TurnOver", 2);
        }
        else
        {
            //TurnOver();
            StartCoroutine(DelayFunction(false, 0.3f));
        }
    }

    IEnumerator DelayFunction(bool status, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        TurnOver();
    }

    private void StartTurn(UnitData unit, bool playerUnit)
    {
        attacking = false;
        unit.SetSkill3CD(unit.GetSkill3CD() - 1);
        unit.SetSkill2CD(unit.GetSkill2CD() - 1);

        //assaultAttack = false;
        movingUnit = unit;
        autoTurn = false;
        //targetPositions.Clear();
        ClearTargets();
        targetSelected = false;
        playerTargetNum = -1;
        skillSelected = 1;
        CanvasController.Instance.ButtonOneHighlight();
        globalSelfSkill = false;
        healSkill = false;
        ShowSkillButtons();
        AdjustTargetsForSkill();
        SelectFirstTarget();
        //CanvasController.Instance.battleUnitText.text = unit.unitName + " (" + unit.GetCurrentLevel() + "/" + unit.totalLevels + ")";

        //Set skill buttons 1 through 3
        //Player selection of another skill changes selected skill number AND highlighted targets
    }

    private void SelectFirstTarget()
    {
        if (healSkill)
        {
            return;
        }
        else
        {
            for (int i = 0; i < 25; i++)
            {
                if (targetPositions[i] && enemyArray[i] != null)
                {
                    UnitFightScript h = enemyArray[i].gameObject.GetComponent<UnitFightScript>();
                    h.SetSelected();
                    return;
                }
            }
        }
        
    }

    private void ClearTargets()
    {

        for (int i = 0; i < 25; i++)
        {
            targetPositions[i] = false;
            
        }
    }

    public void RangeSkill()
    {
        PlayerRangeMagicSneakTargetSelect(movingUnit);
        HighlightEnemyTargets();
        healSkill = false;
        globalSelfSkill = false;
    }

    public void HealSkill()
    {
        healSkill = true;
        globalSelfSkill = false;
        foreach (UnitData player in playerUnitList)
        {
            if (!player.isDead())
            {
                //targetPositions.Add(player.positionNum);
                targetPositions[player.positionNum] = true;
            }
        }
        HighlightPlayerTargets();
    }

    public void MeleeSkill()
    {
        PlayerMeleeTargetSelect(movingUnit);
        HighlightEnemyTargets();
        healSkill = false;
        globalSelfSkill = false;
    }

    public void GlobalSkill()
    {
        foreach (UnitData enemy in enemyUnitList)
        {
            if (!enemy.isDead())
            {
                UnitFightScript h = enemyArray[enemy.positionNum].gameObject.GetComponent<UnitFightScript>();
                h.SetSelected();
            }
        }
        healSkill = false;
        globalSelfSkill = true;
    }

    public void SelfSkill()
    {
        UnitFightScript h = playerArray[movingUnit.positionNum].gameObject.GetComponent<UnitFightScript>();
        h.SetSelected();
        globalSelfSkill = true;
        healSkill = false;
    }
    
    private void HighlightEnemyTargets()
    {
        /*foreach (int i in targetPositions)
        {
            if(enemyArray[i] != null)
            {
                UnitFightScript h = enemyArray[i].gameObject.GetComponent<UnitFightScript>();
                h.SetTargeted();
            }
        }*/

        for(int i = 0; i < 25; i++)
        {
            if (targetPositions[i] && enemyArray[i] != null)
            {
                UnitFightScript h = enemyArray[i].gameObject.GetComponent<UnitFightScript>();
                h.SetTargeted();
            }
        }
    }

    private void HighlightPlayerTargets()
    {
        /*foreach (int i in targetPositions)
        {
            if (playerArray[i] != null)
            {
                UnitFightScript h = playerArray[i].gameObject.GetComponent<UnitFightScript>();
                h.SetTargeted();
            }
        }*/

        for (int i = 0; i < 25; i++)
        {
            if (targetPositions[i] && playerArray[i] != null)
            {
                UnitFightScript h = playerArray[i].gameObject.GetComponent<UnitFightScript>();
                h.SetTargeted();
            }
        }
    }

    private void ResetHalos()
    {
        targetSelected = false;
        foreach (GameObject g in playerArray)
        {
            if (g != null)
            {
                UnitFightScript h = g.GetComponent<UnitFightScript>();
                h.NoHalos();
            }            
        }

        foreach (GameObject g in enemyArray)
        {
            if (g != null)
            {
                UnitFightScript h = g.GetComponent<UnitFightScript>();
                h.NoHalos();
            }
        }
    }

    public void TargetSelected(int targetLocation)
    {
        targetSelected = true;
        if (!healSkill)
        {
            if (playerTargetNum != -1 && enemyArray[playerTargetNum] != null)
            {
                UnitFightScript h = enemyArray[playerTargetNum].gameObject.GetComponent<UnitFightScript>();
                if (h.targeted)
                {
                    h.SetTargeted();
                }
            }
        }
        else
        {
            if (playerTargetNum != -1 && playerArray[playerTargetNum] != null)
            {
                UnitFightScript h = playerArray[playerTargetNum].gameObject.GetComponent<UnitFightScript>();
                if (h.targeted)
                {
                    h.SetTargeted();
                }
            }
        }
        playerTargetNum = targetLocation;
    }

    private void TakeTurn()
    {
        movingUnit.GainXP(2, enemyUnitList[0]);
        CanvasController.Instance.battleUnitText.text = movingUnit.unitName + " (" + movingUnit.GetCurrentLevel() + "/" + movingUnit.totalLevels + ")";
        movingUnit.TakeTurn();
        if (skillSelected == 1)
        {
            Skill1(movingUnit, true);
        }
        else if(skillSelected == 2)
        {
            movingUnit.SetSkill2CD(2);
            Skill2(movingUnit, true);
        }
        else if(skillSelected == 3)
        {
            movingUnit.SetSkill3CD(4);
            Skill3(movingUnit, true);
        }

        if (!GameController.Instance.autoBattle)
        {
            ResetHalos();
            CanvasController.Instance.readyButton.SetActive(false);
            CanvasController.Instance.skillButton1.SetActive(false);
            CanvasController.Instance.skillButton2.SetActive(false);
            CanvasController.Instance.skillButton3.SetActive(false);
            Invoke("TurnOver", 2);
        }
        else
        {
            TurnOver();
        }
    }

    private void TurnOver()
    {
        if (!CanvasController.Instance.battlePaused)
        {
            countingSpeed = true;

        }
    }

    private void SkillSelect(UnitData unit, bool playerUnit)
    {
        unit.SetSkill3CD(unit.GetSkill3CD() - 1);
        unit.SetSkill2CD(unit.GetSkill2CD() - 1);

        //unit.GainXP(2, enemyUnitList[0]);

        CanvasController.Instance.battleStatusText.text = "";
        CanvasController.Instance.battleSkillText.text = "";

        if (autoTurn)
        {
            if (unit.charType == 1 && unit.GetCharClass() > 3)
            {

                if (unit.GetCharClass() > 25 && unit.GetSkill3CD() < 0)
                {
                    unit.SetSkill3CD(4);
                    Skill3(unit, playerUnit);
                    //return 3;
                    return;
                }

                if (unit.GetSkill2CD() < 0)
                {
                    unit.SetSkill2CD(2);
                    Skill2(unit, playerUnit);
                    return;
                    //return 2;
                }
            }
            else if (unit.charType == 2 && unit.GetCharClass() > 2)
            {
                if (unit.GetCharClass() > 6 && unit.GetSkill3CD() < 0)
                {
                    unit.SetSkill3CD(4);
                    Skill3(unit, playerUnit);
                    return;
                    //return 3;
                }

                if (unit.GetSkill2CD() < 0)
                {
                    unit.SetSkill2CD(2);
                    Skill2(unit, playerUnit);
                    return;
                    //return 2;
                }
            }
            else if (unit.charType == 3)
            {
                if (unit.GetCharClass() > 8 && unit.GetSkill3CD() < 0)
                {
                    unit.SetSkill3CD(4);
                    Skill3(unit, playerUnit);
                    return;
                    //return 3;
                }

                if (unit.GetSkill2CD() < 0)
                {
                    unit.SetSkill2CD(2);
                    Skill2(unit, playerUnit);
                    return;
                    //return 2;
                }
            }
            else if (unit.charType == 4 && unit.GetCharClass() > 1)
            {
                if (unit.GetCharClass() == 2 && unit.GetSkill3CD() < 0)
                {
                    unit.SetSkill3CD(4);
                    Skill3(unit, playerUnit);
                    return;
                    //return 3;
                }

                if (unit.GetSkill2CD() < 0)
                {
                    unit.SetSkill2CD(2);
                    Skill2(unit, playerUnit);
                    return;
                    //return 2;
                }
            }

            Skill1(unit, playerUnit);
            //return 1;
        }
        else
        {
            
        }
        
    }

    private void ShowSkillButtons()
    {
        CanvasController.Instance.readyButton.SetActive(true);
        CanvasController.Instance.skillButton1.SetActive(true);
        CanvasController.Instance.skillButton2.SetActive(false);
        CanvasController.Instance.skillButton3.SetActive(false);

        CanvasController.Instance.SetBattleButtonOneText(movingUnit);

        if (movingUnit.charType == 1)
        {
            if(movingUnit.charClass >= 4 && movingUnit.GetSkill2CD() < 0)
            {
                CanvasController.Instance.skillButton2.SetActive(true);
                CanvasController.Instance.SetBattleButtonTwoText(movingUnit);
            }

            if (movingUnit.charClass >= 26 && movingUnit.GetSkill3CD() < 0)
            {
                CanvasController.Instance.skillButton3.SetActive(true);
                CanvasController.Instance.SetBattleButtonThreeText(movingUnit);
            }

        }
        else if (movingUnit.charType == 2)
        {
            if (movingUnit.charClass >= 3 && movingUnit.GetSkill2CD() < 0)
            {
                CanvasController.Instance.skillButton2.SetActive(true);
                CanvasController.Instance.SetBattleButtonTwoText(movingUnit);
            }

            if (movingUnit.charClass >= 7 && movingUnit.GetSkill3CD() < 0)
            {
                CanvasController.Instance.skillButton3.SetActive(true);
                CanvasController.Instance.SetBattleButtonThreeText(movingUnit);
            }
        }
        else
        {
            if(movingUnit.GetSkill2CD() < 0)
            {
                CanvasController.Instance.skillButton2.SetActive(true);
                CanvasController.Instance.SetBattleButtonTwoText(movingUnit);
            }
            
            if (movingUnit.charClass >= 9 && movingUnit.GetSkill3CD() < 0)
            {
                CanvasController.Instance.skillButton3.SetActive(true);
                CanvasController.Instance.SetBattleButtonThreeText(movingUnit);
            }
        }
    }



    public void Skill1Clicked()
    {
        skillSelected = 1;
        globalSelfSkill = false;
        targetSelected = false;
        playerTargetNum = -1;
        if (movingUnit == null)
        {
            return;
        }
        ResetHalos();
        CanvasController.Instance.ButtonOneHighlight();
        AdjustTargetsForSkill();
    }

    public void Skill2Clicked()
    {
        targetSelected = false;
        playerTargetNum = -1;
        skillSelected = 2;
        globalSelfSkill = false;
        if (movingUnit == null)
        {
            return;
        }
        ResetHalos();
        AdjustTargetsForSkill();
        CanvasController.Instance.ButtonTwoHighlight();
    }

    public void Skill3Clicked()
    {
        
        targetSelected = false;
        playerTargetNum = -1;
        skillSelected = 3;
        globalSelfSkill = false;
        if (movingUnit == null)
        {
            return;
        }
        ResetHalos();
        AdjustTargetsForSkill();
        CanvasController.Instance.ButtonThreeHighlight();
    }

    public void AdjustTargetsForSkill()
    {
        if (movingUnit.charType == 1)
        {

            if (skillSelected == 1)
            {
                switch (movingUnit.charClass)
                {
                    case 1:
                    case 2:
                    case 4:
                    case 6:
                    case 15:
                    case 16:
                    case 17:
                    case 19:
                    case 20:
                    case 22:
                    case 23:
                    case 26:
                    case 27:
                    case 28:
                    case 29:
                    case 31:
                    case 33:
                    case 34:
                    case 35:
                    case 36:
                    case 43:
                    case 45:
                    case 47:
                    case 54:
                    case 55:
                    case 56:
                    case 58:
                        RangeSkill();
                        break;
                    case 30:
                        HealSkill();
                        break;
                    default:
                        MeleeSkill();
                        break;
                }
            }
            else if(skillSelected == 2)
            {
                switch (movingUnit.charClass)
                {
                    case 4:
                    case 6:
                    case 7:
                    case 15:
                    case 16:
                    case 17:
                    case 19:
                    case 20:
                    case 22:
                    case 23:
                    case 25:
                    case 26:
                    case 27:
                    case 28:
                    case 29:
                    case 30:
                    case 33:
                    case 35:
                    case 36:
                    case 37:
                    case 42:
                    case 43:
                    case 44:
                    case 45:
                    case 47:
                    case 48:
                    case 52:
                    case 54:
                    case 56:
                    case 58:
                        RangeSkill();
                        break;
                    case 5:
                    case 12:
                    case 21:
                    case 24:
                    case 31:
                    case 32:
                    case 34:
                    case 41:
                    case 46:
                    case 49:
                    case 51:
                        HealSkill();
                        break;
                    default:
                        MeleeSkill();
                        break;
                }
            }
            else
            {
                switch (movingUnit.charClass)
                {
                    case 26:
                    case 27:
                    case 29:
                    case 30:
                    case 31:
                    case 35:
                    case 36:
                    case 37:
                    case 42:
                    case 44:
                    case 47:
                    case 48:
                    case 52:
                    case 54:
                    case 55:
                        RangeSkill();
                        break;
                    case 33:
                    case 32:
                    case 34:
                    case 41:
                    case 46:
                        HealSkill();
                        break;
                    case 28:
                    case 43:
                    case 45:
                    case 51:
                    case 56:
                    case 58:
                        GlobalSkill();
                        break;
                    case 40:
                    case 49:
                        SelfSkill();
                        break;
                    default:
                        MeleeSkill();
                        break;
                }
            }
        }
        else if (movingUnit.charType == 2)
        {
            if (skillSelected == 1)
            {
                switch (movingUnit.charClass)
                {
                    case 0:
                    case 6:
                        RangeSkill();
                        break;
                    default:
                        MeleeSkill();
                        break;
                }
            }
            else if (skillSelected == 2)
            {
                switch (movingUnit.charClass)
                {
                    case 9:
                    case 6:
                        RangeSkill();
                        break;
                    default:
                        MeleeSkill();
                        break;
                }
            }
            else
            {
                switch (movingUnit.charClass)
                {
                    case 7:
                        MeleeSkill();
                        break;
                    default:
                        RangeSkill();
                        break;
                }
            }

        }
        else
        {
            if (skillSelected == 1)
            {
                switch (movingUnit.charClass)
                {
                    case 0:
                    case 1:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                    case 13:
                        RangeSkill();
                        break;
                    default:
                        MeleeSkill();
                        break;
                }
            }
            else if (skillSelected == 2)
            {
                switch (movingUnit.charClass)
                {
                    case 1:
                    case 3:
                    case 8:
                    case 9:
                    case 10:
                        RangeSkill();
                        break;
                    default:
                        MeleeSkill();
                        break;
                }
            }
            else
            {
                switch (movingUnit.charClass)
                {
                    case 9:
                        SelfSkill();
                        break;
                    case 13:
                        GlobalSkill();
                        break;
                    default:
                        RangeSkill();
                        break;
                }
            }
        }
    }


    /***********
     * SKILLS
     * **********/


    private void Skill1(UnitData unit, bool playerUnit)
    {
        if(unit.charType == 1)
        {
            Skill1Human(unit, playerUnit);
        }else if(unit.charType == 2)
        {
            Skill1Animal(unit, playerUnit);
        }
        else if (unit.charType == 3)
        {
            Skill1Myth(unit, playerUnit);
        }
        else if (unit.charType == 4)
        {
            Skill1Undead(unit, playerUnit);
        }
    }

    private void Skill2(UnitData unit, bool playerUnit)
    {
        if (unit.charType == 1)
        {
            Skill2Human(unit, playerUnit);
        }
        else if (unit.charType == 2)
        {
            Skill2Animal(unit, playerUnit);
        }
        else if (unit.charType == 3)
        {
            Skill2Myth(unit, playerUnit);
        }
        else if (unit.charType == 4)
        {
            Skill2Undead(unit, playerUnit);
        }
    }

    private void Skill3(UnitData unit, bool playerUnit)
    {
        if (unit.charType == 1)
        {
            Skill3Human(unit, playerUnit);
        }
        else if (unit.charType == 2)
        {
            Skill3Animal(unit, playerUnit);
        }
        else if (unit.charType == 3)
        {
            Skill3Myth(unit, playerUnit);
        }
        else if (unit.charType == 4)
        {
            Skill3Undead(unit, playerUnit);
        }
    }

    private void Skill1Human(UnitData unit, bool playerUnit)
    {
        Debug.Log("Skill 1 H " + playerUnit);
        switch (unit.GetCharClass())
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
                Slash(unit, playerUnit);
                break;
            case 1:
            case 4:
            case 19:
            case 27:
            case 45:
                MagicMissile(unit, playerUnit);
                break;
            case 2:
            case 17:
            case 36:
            case 54:
                Stab(unit, playerUnit);
                break;
            case 5:
            case 21:
            case 32:
            case 46:
                Bash(unit, playerUnit);
                break;
            case 6:
            case 15:
            case 35:
            case 47:
                Shoot(unit, playerUnit);
                break;
            case 8:
                Punch(unit, playerUnit);
                break;
            case 24:
                Knockout(unit, playerUnit);
                break;
            case 16:
            case 43:
            case 56:
                Snare(unit, playerUnit);
                break;
            case 20:
            case 26:
            case 58:
                Pain(unit, playerUnit);
                break;
            case 22:
            case 31:
                Flash(unit, playerUnit);
                break;
            case 23:
            case 33:
                Slow(unit, playerUnit);
                break;
            case 28:
                EarthSpike(unit, playerUnit);
                break;
            case 29:
                Fireball(unit, playerUnit);
                break;
            case 30:
                Refresh(unit, playerUnit);
                break;
            case 34:
                Tangle(unit, playerUnit);
                break;
            case 55:
                Corrupt(unit, playerUnit);
                break;
            case 44:
                Trip(unit, playerUnit);
                break;
            case 51:
            case 52:
                Hack(unit, playerUnit);
                break;
            default:
                break;
        }
    }

    private void Skill1Animal(UnitData unit, bool playerUnit)
    {
        switch (unit.GetCharClass())
        {
            case 0:
                SneakBite(unit, playerUnit);
                break;
            case 1:
            case 3:
            case 5:
            case 8:
            case 9:
                Bite(unit, playerUnit);
                break;
            case 2:
            case 4:
            case 7:
                Gore(unit, playerUnit);
                break;
            case 6:
                Claw(unit, playerUnit);
                break;
            default:
                break;
        }
    }

    private void Skill1Myth(UnitData unit, bool playerUnit)
    {
        switch (unit.GetCharClass())
        {
            case 0:
            case 1:
            case 7:
            case 9:
            case 13:
                Claw(unit, playerUnit);
                break;
            case 2:
            case 6:
                Bash(unit, playerUnit);
                break;
            case 3:
            case 5:
                Slash(unit, playerUnit);
                break;
            case 4:
            case 11:
            case 12:
                Bite(unit, playerUnit);
                break;
            case 8:
            case 10:
                SneakBite(unit, playerUnit);
                break;
            default:
                break;
        }
    }

    private void Skill1Undead(UnitData unit, bool playerUnit)
    {
        switch (unit.GetCharClass())
        {
            case 0:
                break;
            default:
                break;
        }
    }

    private void Skill2Human(UnitData unit, bool playerUnit)
    {
        switch (unit.GetCharClass())
        {
            case 4:
                MagicBolt(unit, playerUnit);
                break;
            case 5:
            case 21:
            case 24:
            case 32:
            case 34:
            case 41:
            case 46:
            case 49:
                Heal(unit, playerUnit);
                break;
            case 6:
            case 16:
            case 43:
            case 56:
                Spike(unit, playerUnit);
                break;
            case 7:
            case 17:
            case 25:
            case 36:
            case 37:
            case 44:
            case 48:
            case 54:
                BackStab(unit, playerUnit);
                break;
            case 8:
                Haymaker(unit, playerUnit);
                break;
            case 9:
            case 14:
                Chop(unit, playerUnit);
                break;
            case 10:
            case 40:
            case 55:
                Trip(unit, playerUnit);
                break;
            case 11:
            case 38:
            case 50:
                Hack(unit, playerUnit);
                break;
            case 12:
            case 51:
                Guard(unit, playerUnit);
                break;
            case 13:
            case 39:
                Disarm(unit, playerUnit);
                break;
            case 15:
            case 35:
            case 47:
                Snipe(unit, playerUnit);
                break;
            case 18:
                Slice(unit, playerUnit);
                break;
            case 19:
            case 27:
            case 45:
            case 52:
                MagicStorm(unit, playerUnit);
                break;
            case 20:
            case 26:
            case 42:
            case 58:
                Corrupt(unit, playerUnit);
                break;
            case 22:
            case 29:
                Incinerate(unit, playerUnit);
                break;
            case 23:
            case 33:
                Sleep(unit, playerUnit);
                break;
                
            case 28:
                Rockslide(unit, playerUnit);
                break;
            case 30:
                Flood(unit, playerUnit);
                break;
            case 31:
                WindArmor(unit, playerUnit);
                break;
            default:
                break;
        }
    }

    private void Skill2Animal(UnitData unit, bool playerUnit)
    {
        switch (unit.GetCharClass())
        {
            case 3:
            case 5:
                Savage(unit, playerUnit);
                break;
            case 4:
            case 7:
            case 8:
                Trample(unit, playerUnit);
                break;
            case 6:
                Peck(unit, playerUnit);
                break;
            case 9:
                SneakBite(unit, playerUnit);
                break;
            default:
                break;
        }
    }

    private void Skill2Myth(UnitData unit, bool playerUnit)
    {
        switch (unit.GetCharClass())
        {
            case 0:
                Slice(unit, playerUnit);
                break;
            case 1:
                SneakBite(unit, playerUnit);
                break;
            case 2:
                Hack(unit, playerUnit);
                break;
            case 3:
            case 10:
                Snipe(unit, playerUnit);
                break;
            case 4:
                Sting(unit, playerUnit);
                break;
            case 5:
                Poison(unit, playerUnit);
                break;
            case 6:
                Knockout(unit, playerUnit);
                break;
            case 7:
            case 11:
            case 13:
                Savage(unit, playerUnit);
                break;
            case 8:
                Sleep(unit, playerUnit);
                break;
            case 9:
                Peck(unit, playerUnit);
                break;
            case 12:
                Trample(unit, playerUnit);
                break;
            default:
                break;
        }
    }

    private void Skill2Undead(UnitData unit, bool playerUnit)
    {
        switch (unit.GetCharClass())
        {
            case 0:
                break;
            default:
                break;
        }
    }

    private void Skill3Human(UnitData unit, bool playerUnit)
    {
        switch (unit.GetCharClass())
        {
            case 26:
                Psychosis(unit, playerUnit);
                break;
            case 27:
                MagicAssault(unit, playerUnit);
                break;
            case 28:
                Earthquake(unit, playerUnit);
                break;
            case 29:
                Inferno(unit, playerUnit);
                break;
            case 30:
                Tsunami(unit, playerUnit);
                break;
            case 31:
                Hurricane(unit, playerUnit);
                break;
            case 32:
            case 33:
                MassHeal(unit, playerUnit);
                break;
            case 34:
                NaturesBounty(unit, playerUnit);
                break;
            case 35:
                PoisonArrow(unit, playerUnit);
                break;
            case 36:
                Keelhaul(unit, playerUnit);
                break;
            case 37:
                Skewer(unit, playerUnit);
                break;
            case 38:
                DestroySkill(unit, playerUnit);
                break;
            case 39:
                CriticalStrike(unit, playerUnit);
                break;
            case 40:
                WorkTheCrowd(unit, playerUnit);
                break;
            case 41:
                Fortify(unit, playerUnit);
                break;
            case 42:
                DarkStrike(unit, playerUnit);
                break;
            case 43:
                AnimalAssault(unit, playerUnit);
                break;
            case 44:
                CheapShot(unit, playerUnit);
                break;
            case 45:
                MagicalOnslaught(unit, playerUnit);
                break;
            case 46:
                Revive(unit, playerUnit);
                break;
            case 47:
                Bullseye(unit, playerUnit);
                break;
            case 48:
            case 55:
                Assassinate(unit, playerUnit);
                break;
            case 49:
                GuardianAngel(unit, playerUnit);
                break;
            case 50:
                Execute(unit, playerUnit);
                break;
            case 51:
                Dragonfire(unit, playerUnit);
                break;
            case 52:
                Hellstrike(unit, playerUnit);
                break;
            case 54:
                Pilfer(unit, playerUnit);
                break;
            case 56:
                MythicAssault(unit, playerUnit);
                break;
            case 58:
                MassHysteria(unit, playerUnit);
                break;
            default:
                break;
        }
    }

    private void Skill3Animal(UnitData unit, bool playerUnit)
    {
        switch (unit.GetCharClass())
        {
            case 7:
                Stomp(unit, playerUnit);
                break;
            case 8:
            case 9:
                Drown(unit, playerUnit);
                break;
            default:
                break;
        }
    }

    private void Skill3Myth(UnitData unit, bool playerUnit)
    {
        switch (unit.GetCharClass())
        {
            case 9:
                Rebirth(unit, playerUnit);
                break;
            case 10:
                Petrify(unit, playerUnit);
                break;
            case 11:
                Hellfire(unit, playerUnit);
                break;
            case 12:
                Plague(unit, playerUnit);
                break;
            case 13:
                Dragonfire(unit, playerUnit);
                break;
            default:
                break;
        }
    }

    private void Skill3Undead(UnitData unit, bool playerUnit)
    {
        switch (unit.GetCharClass())
        {
            case 0:
                break;
            default:
                break;
        }
    }


    private int MeleeMovements(UnitData unit, bool playerUnit)
    {
        int targetNum = -1;
        //movingUnit = unit;
        GameObject g;
        
        if (playerUnit)
        {
            playerUnitMoving = true;

            g = playerArray[unit.positionNum];
            //startingPosition = g.transform.position;
            //Move towards enemy during attack animation
            //MoveTowards(enemyArray[targetNum].transform.position, unit, playerUnit);  
        }
        else
        {
            playerUnitMoving = false;

            g = enemyArray[unit.positionNum];
            //startingPosition = enemyArray[unit.positionNum].transform.position;    
        }
        

        if (playerTarget)
        {
            if (autoTurn)
            {
                targetNum = EnemyMeleeTargetSelect(unit);
            }
            else
            {
                targetNum = playerTargetNum;
            }


            if (targetNum != -1 && playerArray[targetNum] != null && !GameController.Instance.autoBattle)
            {
                movingTargetPosition = new Vector3(playerArray[targetNum].transform.position.x + 2, playerArray[targetNum].transform.position.y, playerArray[targetNum].transform.position.z);
                FaceTarget(movingTargetPosition, g);
            }
        }
        else 
        {            
            if (autoTurn)
            {
                targetNum = PlayerMeleeTargetSelect(unit);
            }
            else
            {
                targetNum = playerTargetNum;
            }

            if (targetNum != -1 && enemyArray[targetNum] != null && !GameController.Instance.autoBattle)
            {
                movingTargetPosition = new Vector3(enemyArray[targetNum].transform.position.x - 2, enemyArray[targetNum].transform.position.y, enemyArray[targetNum].transform.position.z);
                FaceTarget(movingTargetPosition, g);
            }
        }

        if (!GameController.Instance.autoBattle)
        {
            MoveTowards(normalMoveTime);

            StartCoroutine(MoveBack(moveDelay, movingUnit));

            Invoke("DelayAttackAnim", .3f);
        }

        return targetNum;
    }

    private int SneakAttackMovements(UnitData unit, bool playerUnit)
    {
        int targetNum = -1;
        //movingUnit = unit;
        GameObject g;

        if (playerUnit)
        {
            playerUnitMoving = true;
            g = playerArray[unit.positionNum];
            //startingPosition = g.transform.position;
            //Move towards enemy during attack animation
            //MoveTowards(enemyArray[targetNum].transform.position, unit, playerUnit);  
        }
        else
        {
            playerUnitMoving = false;
            g = enemyArray[unit.positionNum];
            //startingPosition = g.transform.position;            
        }


        if (playerTarget)
        {         

            if (autoTurn)
            {
                targetNum = EnemyRangeMagicSneakTargetSelect(unit);
            }
            else
            {
                targetNum = playerTargetNum;
            }

            if (targetNum != -1 && playerArray[targetNum] != null && !GameController.Instance.autoBattle)
            {
                movingTargetPosition = new Vector3(playerArray[targetNum].transform.position.x - 2, playerArray[targetNum].transform.position.y, playerArray[targetNum].transform.position.z);
                FaceTarget(movingTargetPosition, g);
                g.transform.Rotate(new Vector3(0f, 180f, 0f));

            }
        }
        else
        {
            
            if (autoTurn)
            {
                targetNum = PlayerRangeMagicSneakTargetSelect(unit);
            }
            else
            {
                targetNum = playerTargetNum;
            }

            if (targetNum != -1 && enemyArray[targetNum] != null && !GameController.Instance.autoBattle)
            {
                movingTargetPosition = new Vector3(enemyArray[targetNum].transform.position.x + 2, enemyArray[targetNum].transform.position.y, enemyArray[targetNum].transform.position.z);
                FaceTarget(movingTargetPosition, g);
                g.transform.Rotate(new Vector3(0f, 180f, 0f));
            }
        }

        if (!GameController.Instance.autoBattle)
        {
            MoveTowards(sneakMoveTime);

            StartCoroutine(SneakMoveBack(moveDelay, movingUnit));
            Invoke("SneakFaceBack", moveDelay + .2f);

            Invoke("DelayAttackAnim", .1f);
        }


        return targetNum;
    }

    private int RangeAttack(UnitData unit, bool playerUnit, int skillNum, bool movingProjectile)
    {
        int targetNum = -1;
        //movingUnit = unit;
        GameObject g;

        if (playerUnit)
        {
            playerUnitMoving = true;
            g = playerArray[unit.positionNum];
        }
        else
        {
            playerUnitMoving = false;
            g = enemyArray[unit.positionNum];
        }


        if (playerTarget)
        {
            
            if (autoTurn)
            {
                targetNum = EnemyRangeMagicSneakTargetSelect(unit);
            }
            else
            {
                targetNum = playerTargetNum;
            }

            if (targetNum != -1 && playerArray[targetNum] != null)
            {
                movingTargetPosition = playerArray[targetNum].transform.position;
            }
        }
        else
        {
            
            if (autoTurn)
            {
                targetNum = PlayerRangeMagicSneakTargetSelect(unit);
            }
            else
            {
                targetNum = playerTargetNum;
            }

            if (targetNum != -1 && enemyArray[targetNum] != null)
            {
                movingTargetPosition = enemyArray[targetNum].transform.position;
            }
        }

        if (!GameController.Instance.autoBattle)
        {
            FaceTarget(movingTargetPosition, g);
            if (movingProjectile)
            {
                ////Instantiate Projectile
                GameObject projectile = (GameObject)Instantiate(GameController.Instance.projectiles[skillNum],
                    new Vector3(g.transform.position.x, g.transform.position.y + 5f, g.transform.position.z), g.transform.rotation);
                ////Face Target
                FaceTarget(movingTargetPosition, projectile);
                ////Move Projectile Towards Target
                Rigidbody rigidBody = projectile.GetComponent<Rigidbody>();
                StartCoroutine(SmoothMovementRoutine(movingTargetPosition, rigidBody, projectileMoveTime));
            }
        }        

        return targetNum;
    }

    private int HealingSkill(UnitData unit, bool playerUnit, int skillNum, int healDmg)
    {
        int targetNum = -1;
        //movingUnit = unit;
        GameObject g;

        if (playerUnit)
        {
            playerUnitMoving = true;
            g = playerArray[unit.positionNum];
        }
        else
        {
            playerUnitMoving = false;
            g = enemyArray[unit.positionNum];
        }


        if (playerTarget)
        {
            if (autoTurn)
            {
                targetNum = PlayerHealTargetSelect(unit, healDmg);
            }
            else
            {
                targetNum = playerTargetNum;
            }
            movingTargetPosition = playerArray[targetNum].transform.position;
        }
        else if (!playerTarget)
        {            
            if (autoTurn)
            {
                targetNum = EnemyHealTargetSelect(unit, healDmg);
            }
            else
            {
                targetNum = playerTargetNum;
            }
            movingTargetPosition = enemyArray[targetNum].transform.position;
        }

        if (targetNum == -1)
        {
            targetNum = unit.positionNum;
        }


        if (!GameController.Instance.autoBattle)
        {
            FaceTarget(movingTargetPosition, g);
            ////Instantiate Projectile
            GameObject projectile = (GameObject)Instantiate(GameController.Instance.projectiles[skillNum], g.transform.position, g.transform.rotation);
            ////Face Target
            FaceTarget(movingTargetPosition, projectile);
            ////Move Projectile Towards Target
            Rigidbody rigidBody = projectile.GetComponent<Rigidbody>();
            StartCoroutine(SmoothMovementRoutine(movingTargetPosition, rigidBody, projectileMoveTime));
        }           


        return targetNum;
    }

    private int ModifyDamage(UnitData unit, int damage)
    {
        if (unit.IsOffDown() && !unit.IsOffUp())
        {
            return damage / 2;
        }else if(unit.IsOffUp() && !unit.IsOffDown())
        {
            return damage * 2;
        }

        return damage;
    }

    /***************
     * ACTUAL SKILL FUNCTIONS
     * ************/
        /***********
        * HUMAN 1ST TIER
        * *********/
    private void Slash(UnitData unit, bool playerUnit)
    {
        SoundController.Instance.PlaySingle(0);
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            MeleeMovements(unit, playerUnit);
        }
        else
        {
            targetNum = MeleeMovements(unit, playerUnit);
        }
        int dmg = unit.GetStrength() * 2;
        dmg = ModifyDamage(unit, dmg);

        if (targetNum == -1)
        {
            return;
        }

        if (playerTarget)
        {
            if (playerUnits[targetNum].TakeDamage(dmg, 1, unit))
            {
                //DeadCharacter(playerUnits[targetNum], playerArray[targetNum], true);
            }
            else
            {
                HurtCharacter(playerArray[targetNum], false);
            }
        }
        else
        {
            if (enemyUnits[targetNum].TakeDamage(dmg, 1, unit))
            {
                //DeadCharacter(enemyUnits[targetNum], enemyArray[targetNum], false);
            }
            else
            {
                HurtCharacter(enemyArray[targetNum], false);
            }
        }
        CanvasController.Instance.battleSkillText.text = "uses Slash!";
        
    }

    private void MagicMissile(UnitData unit, bool playerUnit)
    {
        SoundController.Instance.PlaySingle(1);
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            RangeAttack(unit, playerUnit, 0, true);
        }
        else
        {
            targetNum = RangeAttack(unit, playerUnit, 0, true);

        }
        int dmg = unit.GetIntelligence() * 2;
        dmg = ModifyDamage(unit, dmg);

        if (!playerTarget)
        {            
            //Do damage
            if (enemyUnits[targetNum].TakeDamage(dmg, 4, unit))
            {
                //DeadCharacter(enemyUnits[targetNum], enemyArray[targetNum], false);
            }
            else
            {
                HurtCharacter(enemyArray[targetNum], true);
            }
        }
        else
        {
            if (playerUnits[targetNum].TakeDamage(dmg, 4, unit))
            {
                //DeadCharacter(playerUnits[targetNum], playerArray[targetNum], true);
            }
            else
            {
                HurtCharacter(playerArray[targetNum], true);
            }
        }
        CanvasController.Instance.battleSkillText.text = "casts Magic Missle!";
    }

    private void Stab(UnitData unit, bool playerUnit)
    {
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            SneakAttackMovements(unit, playerUnit);
        }
        else
        {
            targetNum = SneakAttackMovements(unit, playerUnit);

        }
        SoundController.Instance.PlaySingle(2);

        if (!playerTarget)
        {            
            //Do damage
            if (enemyUnits[targetNum].TakeDamage(unit.GetAgility() * 2, 2, unit))
            {
                //DeadCharacter(enemyUnits[targetNum], enemyArray[targetNum], false);
            }
            else
            {
                HurtCharacter(enemyArray[targetNum], false);
            }
        }
        else
        {
            if (playerUnits[targetNum].TakeDamage(unit.GetAgility() * 2, 2, unit))
            {
                //DeadCharacter(playerUnits[targetNum], playerArray[targetNum], true);
            }
            else
            {
                HurtCharacter(playerArray[targetNum], false);
            }
        }
        CanvasController.Instance.battleSkillText.text = "uses Stab!";
    }


    /************
     * HUMAN 2ND TIER SKILL 1
     * ***********/

    private void Bash(UnitData unit, bool playerUnit)
    {
        SoundController.Instance.PlaySingle(4);
        int targetNum; 
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            MeleeMovements(unit, playerUnit);
        }
        else
        {
            targetNum = MeleeMovements(unit, playerUnit);
        }
        int dmg = unit.GetStrength();
        dmg = ModifyDamage(unit, dmg);

        if (targetNum == -1)
        {
            return;
        }

        if (!playerTarget)
        {
            //Do damage
            if (enemyUnits[targetNum].TakeDamage(dmg, 1, unit))
            {
                //DeadCharacter(enemyUnits[targetNum], enemyArray[targetNum], false);
            }
            else
            {
                HurtCharacter(enemyArray[targetNum], false);
                if(enemyUnits[targetNum].Daze(1, unit))
                {
                    CanvasController.Instance.battleStatusText.text = "and is Dazed!";
                }
                else
                {
                    CanvasController.Instance.battleStatusText.text = "resisted Daze!";
                }
            }
        }
        else
        {
            if (playerUnits[targetNum].TakeDamage(dmg, 1, unit))
            {
                //DeadCharacter(playerUnits[targetNum], playerArray[targetNum], true);
            }
            else
            {
                HurtCharacter(playerArray[targetNum], false);                
                if (playerUnits[targetNum].Daze(1, unit))
                {
                    CanvasController.Instance.battleStatusText.text = "and is Dazed!";
                }
                else
                {
                    CanvasController.Instance.battleStatusText.text = "resisted Daze!";
                }
            }
        }
        CanvasController.Instance.battleSkillText.text = "uses Bash!";
    }

    private void Shoot(UnitData unit, bool playerUnit)
    {
        SoundController.Instance.PlaySingle(6);
        int targetNum; 
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            RangeAttack(unit, playerUnit, 2, true);
        }
        else
        {
            targetNum = RangeAttack(unit, playerUnit, 2, true);
        }

        if (!playerTarget)
        {
            //Do damage
            if (enemyUnits[targetNum].TakeDamage(unit.GetDexterity() * 2, 3, unit))
            {
                //DeadCharacter(enemyUnits[targetNum], enemyArray[targetNum], false);
            }
            else
            {
                HurtCharacter(enemyArray[targetNum], true);
            }
        }
        else
        {
            if (playerUnits[targetNum].TakeDamage(unit.GetDexterity() * 2, 3, unit))
            {
                //DeadCharacter(playerUnits[targetNum], playerArray[targetNum], true);
            }
            else
            {
                HurtCharacter(playerArray[targetNum], true);
            }
        }
        CanvasController.Instance.battleSkillText.text = "uses Shoot!";
    }

    private void Punch(UnitData unit, bool playerUnit)
    {
        SoundController.Instance.PlaySingle(9);
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            MeleeMovements(unit, playerUnit);
        }
        else
        {
            targetNum = MeleeMovements(unit, playerUnit);
        }
        int dmg = unit.GetStrength();
        dmg = ModifyDamage(unit, dmg);

        if (!playerTarget)
        {
            //Do damage
            if (enemyUnits[targetNum].TakeDamage(unit.GetDexterity() + dmg, 1, unit))
            {
                //DeadCharacter(enemyUnits[targetNum], enemyArray[targetNum], false);
            }
            else
            {
                HurtCharacter(enemyArray[targetNum], false);
            }
        }
        else
        {
            if (playerUnits[targetNum].TakeDamage(unit.GetDexterity() + dmg, 1, unit))
            {
                //DeadCharacter(playerUnits[targetNum], playerArray[targetNum], true);
            }
            else
            {
                HurtCharacter(playerArray[targetNum], false);
            }
        }
        CanvasController.Instance.battleSkillText.text = "uses Punch!";
    }

    /************
     * HUMAN 2ND TIER SKILL 2
     * ***********/

    private void MagicBolt(UnitData unit, bool playerUnit)
    {
        int targetNum; 
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            RangeAttack(unit, playerUnit, 1, true);
        }
        else
        {
            targetNum = RangeAttack(unit, playerUnit, 1, true);
        }
        int dmg = unit.GetIntelligence() * 3;
        dmg = ModifyDamage(unit, dmg);
        SoundController.Instance.PlaySingle(3);
        if (!playerTarget)
        {
            //Do damage
            if (enemyUnits[targetNum].TakeDamage(dmg, 4, unit))
            {
                //DeadCharacter(enemyUnits[targetNum], enemyArray[targetNum], false);
            }
            else
            {
                HurtCharacter(enemyArray[targetNum], true);
            }
        }
        else
        {
            if (playerUnits[targetNum].TakeDamage(dmg, 4, unit))
            {
                //DeadCharacter(playerUnits[targetNum], playerArray[targetNum], true);
            }
            else
            {
                HurtCharacter(playerArray[targetNum], true);
            }
        }
        CanvasController.Instance.battleSkillText.text = "uses Magic Bolt!";
    }

    

    private void Heal(UnitData unit, bool playerUnit)
    {
        SoundController.Instance.PlaySingle(5);

        playerTarget = !playerTarget;
        int dmg = unit.GetIntelligence() * -3;
        dmg = ModifyDamage(unit, dmg);

        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            HealingSkill(unit, playerUnit, 24, dmg);
        }
        else
        {
            targetNum = HealingSkill(unit, playerUnit, 24, dmg);
        }

        if (playerTarget)
        {            
            playerUnits[targetNum].TakeDamage(dmg, 6, unit);
        }
        else
        {
            enemyUnits[targetNum].TakeDamage(dmg, 6, unit);
        }
        CanvasController.Instance.battleSkillText.text = "casts Heal!";
    }

    private void Spike(UnitData unit, bool playerUnit)
    {
        SoundController.Instance.PlaySingle(7);
        
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            SneakAttackMovements(unit, playerUnit);
        }
        else
        {
            targetNum = SneakAttackMovements(unit, playerUnit);
        }
        if (!playerTarget)
        {
            SpecialEffect(38, enemyArray[targetNum].gameObject.transform.position);

            //Do damage
            if (enemyUnits[targetNum].TakeDamage(unit.GetDexterity() * 2, 5, unit))
            {
                //DeadCharacter(enemyUnits[targetNum], enemyArray[targetNum], false);
            }
            else
            {
                HurtCharacter(enemyArray[targetNum], false);
                if(enemyUnits[targetNum].Slow(5, unit))
                {
                    CanvasController.Instance.battleStatusText.text = "and is Slowed!";
                }
                else
                {
                    CanvasController.Instance.battleStatusText.text = "resisted Slow!";
                }
            }
        }
        else
        {
            SpecialEffect(38, playerArray[targetNum].gameObject.transform.position);

            if (playerUnits[targetNum].TakeDamage(unit.GetDexterity() * 2, 5, unit))
            {
                //DeadCharacter(playerUnits[targetNum], playerArray[targetNum], true);
            }
            else
            {
                HurtCharacter(playerArray[targetNum], false);
                if(playerUnits[targetNum].Slow(5, unit))
                {
                    CanvasController.Instance.battleStatusText.text = "and is Slowed!";
                }
                else
                {
                    CanvasController.Instance.battleStatusText.text = "resisted Slow!";
                }
            }
        }
        CanvasController.Instance.battleSkillText.text = "uses a Spike Trap!";
    }

    private void BackStab(UnitData unit, bool playerUnit)
    {
        SoundController.Instance.PlaySingle(8);
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            SneakAttackMovements(unit, playerUnit);
        }
        else
        {
            targetNum = SneakAttackMovements(unit, playerUnit);
        }
        if (playerTarget)
        {
            //Do damage
            if (playerUnits[targetNum].TakeDamage(unit.GetAgility() * 3, 2, unit))
            {
                //DeadCharacter(playerUnits[targetNum], playerArray[targetNum], true);
            }
            else
            {
                HurtCharacter(playerArray[targetNum], false);
            }
        }
        else
        {
            if (enemyUnits[targetNum].TakeDamage(unit.GetAgility() * 3, 2, unit))
            {
                //DeadCharacter(enemyUnits[targetNum], enemyArray[targetNum], false);
            }
            else
            {
                HurtCharacter(enemyArray[targetNum], false);
            }
        }
        CanvasController.Instance.battleSkillText.text = "uses Backstab!";
    }

    private void Haymaker(UnitData unit, bool playerUnit)
    {
        SoundController.Instance.PlaySingle(10);
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            MeleeMovements(unit, playerUnit);
        }
        else
        {
            targetNum = MeleeMovements(unit, playerUnit);
        }
        int dmg = unit.GetStrength();
        dmg = ModifyDamage(unit, dmg);

        if (playerTarget)
        {
            //Do damage
            if (playerUnits[targetNum].TakeDamage(unit.GetDexterity() + dmg, 1, unit))
            {
                //DeadCharacter(playerUnits[targetNum], playerArray[targetNum], true);
            }
            else
            {
                HurtCharacter(playerArray[targetNum], false);
                if(playerUnits[targetNum].Daze(1, unit))
                {
                    CanvasController.Instance.battleStatusText.text = "and is Dazed!";
                }
                else
                {
                    CanvasController.Instance.battleStatusText.text = "resisted Daze!";
                }
            }
        }
        else
        {
            if (enemyUnits[targetNum].TakeDamage(unit.GetDexterity() + dmg, 1, unit))
            {
                //DeadCharacter(enemyUnits[targetNum], enemyArray[targetNum], false);
            }
            else
            {
                HurtCharacter(enemyArray[targetNum], false);
                if(enemyUnits[targetNum].Daze(1, unit))
                {
                    CanvasController.Instance.battleStatusText.text = "and is Dazed!";
                }
                else
                {
                    CanvasController.Instance.battleStatusText.text = "resisted Daze!";
                }
            }
        }
        CanvasController.Instance.battleSkillText.text = "uses Haymaker!";
    }

    private void Chop(UnitData unit, bool playerUnit)
    {
        SoundController.Instance.PlaySingle(11);
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            MeleeMovements(unit, playerUnit);
        }
        else
        {
            targetNum = MeleeMovements(unit, playerUnit);
        }
        int dmg = unit.GetStrength() * 2;
        dmg = ModifyDamage(unit, dmg);

        if (!playerTarget)
        {
            //Do damage
            if (enemyUnits[targetNum].TakeDamage(dmg, 1, unit))
            {
                //DeadCharacter(enemyUnits[targetNum], enemyArray[targetNum], false);
            }
            else
            {
                HurtCharacter(enemyArray[targetNum], false);
                if(enemyUnits[targetNum].DefDown(1, unit))
                {
                    CanvasController.Instance.battleStatusText.text = "and has reduced defense!";
                }
                else
                {
                    CanvasController.Instance.battleStatusText.text = "resisted Defense Down!";
                }
            }
        }
        else
        {
            if (playerUnits[targetNum].TakeDamage(dmg, 1, unit))
            {
                //DeadCharacter(playerUnits[targetNum], playerArray[targetNum], true);
            }
            else
            {
                HurtCharacter(playerArray[targetNum], false);
                if(playerUnits[targetNum].DefDown(1, unit))
                {
                    CanvasController.Instance.battleStatusText.text = "and has Defense Down!";
                }
                else
                {
                    CanvasController.Instance.battleStatusText.text = "resisted Defense Down!";
                }
            }
        }
        CanvasController.Instance.battleSkillText.text = "uses Chop!";
    }

    private void Trip(UnitData unit, bool playerUnit)
    {
        SoundController.Instance.PlaySingle(12);
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            MeleeMovements(unit, playerUnit);
        }
        else
        {
            targetNum = MeleeMovements(unit, playerUnit);
        }
        int dmg = unit.GetStrength();
        dmg = ModifyDamage(unit, dmg);

        if (playerTarget)
        {
            //Do damage
            if (playerUnits[targetNum].TakeDamage(unit.GetAgility() + dmg, 1, unit))
            {
                //DeadCharacter(playerUnits[targetNum], playerArray[targetNum], true);
            }
            else
            {
                HurtCharacter(playerArray[targetNum], false);
                if(playerUnits[targetNum].Daze(2, unit))
                {
                    CanvasController.Instance.battleStatusText.text = "and is Dazed!";
                }
                else
                {
                    CanvasController.Instance.battleStatusText.text = "resisted Daze!";
                }
            }
        }
        else
        {
            if (enemyUnits[targetNum].TakeDamage(unit.GetAgility() + dmg, 1, unit))
            {
                //DeadCharacter(enemyUnits[targetNum], enemyArray[targetNum], false);
            }
            else
            {
                HurtCharacter(enemyArray[targetNum], false);
                if(enemyUnits[targetNum].Daze(2, unit))
                {
                    CanvasController.Instance.battleStatusText.text = "and is Dazed!";
                }
                else
                {
                    CanvasController.Instance.battleStatusText.text = "resisted Daze!";
                }
            }
        }
        CanvasController.Instance.battleSkillText.text = "uses Trip!";
    }

    /************
     * HUMAN 3RD TIER SKILL 1
     * ***********/

    private void Snare(UnitData unit, bool playerUnit)
    {
        SoundController.Instance.PlaySingle(17);
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            SneakAttackMovements(unit, playerUnit);
        }
        else
        {
            targetNum = SneakAttackMovements(unit, playerUnit);
        }
        if (!playerTarget)
        {
            SpecialEffect(39, enemyArray[targetNum].gameObject.transform.position);
            HurtCharacter(enemyArray[targetNum], false);
            bool slow = enemyUnits[targetNum].Slow(5, unit);
            bool daze = enemyUnits[targetNum].Daze(5, unit);
            CanvasController.Instance.battleBattleText.text = "" + enemyUnits[targetNum].unitName;
            if (slow && daze)
            {
                CanvasController.Instance.battleStatusText.text = "is Slowed and Dazed!";
            }
            else if (slow)
            {
                CanvasController.Instance.battleStatusText.text = "is Slowed!";
            }
            else if (daze)
            {
                CanvasController.Instance.battleStatusText.text = "is Dazed!";
            }
            else
            {
                CanvasController.Instance.battleStatusText.text = "resisted Slow and Daze!";
            }
        }
        else
        {
            SpecialEffect(39, playerArray[targetNum].gameObject.transform.position);
            HurtCharacter(playerArray[targetNum], false);
            bool slow = playerUnits[targetNum].Slow(5, unit);
            bool daze = playerUnits[targetNum].Daze(5, unit);
            CanvasController.Instance.battleBattleText.text = "" + playerUnits[targetNum].unitName;
            if (slow && daze)
            {
                CanvasController.Instance.battleStatusText.text = "is Slowed and Dazed!";
            }
            else if (slow)
            {
                CanvasController.Instance.battleStatusText.text = "is Slowed!";
            }
            else if (daze)
            {
                CanvasController.Instance.battleStatusText.text = "is Dazed!";
            }
            else
            {
                CanvasController.Instance.battleStatusText.text = "resisted Slow and Daze!";
            }
        }
        CanvasController.Instance.battleSkillText.text = "uses a Snare trap!";
    }

    private void Pain(UnitData unit, bool playerUnit)
    {
        SoundController.Instance.PlaySingle(20);
        int targetNum; 
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            RangeAttack(unit, playerUnit, 5, true);
        }
        else
        {
            targetNum = RangeAttack(unit, playerUnit, 5, true);
        }
        int dmg = unit.GetIntelligence();
        dmg = ModifyDamage(unit, dmg);

        if (!playerTarget)
        {
            //Do damage
            if (enemyUnits[targetNum].TakeDamage(dmg, 4, unit))
            {
                //DeadCharacter(enemyUnits[targetNum], enemyArray[targetNum], false);
            }
            else
            {
                HurtCharacter(enemyArray[targetNum], true);
                if(enemyUnits[targetNum].Daze(4, unit))
                {
                    CanvasController.Instance.battleStatusText.text = "and is Dazed!";
                }
                else
                {
                    CanvasController.Instance.battleStatusText.text = "resisted Daze!";
                }
            }
        }
        else
        {
            if (playerUnits[targetNum].TakeDamage(dmg, 4, unit))
            {
                //DeadCharacter(playerUnits[targetNum], playerArray[targetNum], true);
            }
            else
            {
                HurtCharacter(playerArray[targetNum], true);
                if(playerUnits[targetNum].Daze(4, unit))
                {
                    CanvasController.Instance.battleStatusText.text = "and is Dazed!";
                }
                else
                {
                    CanvasController.Instance.battleStatusText.text = "resisted Daze!";
                }
            }
        }
        CanvasController.Instance.battleSkillText.text = "casts Pain!";
    }

    private void Flash(UnitData unit, bool playerUnit)
    {
        SoundController.Instance.PlaySingle(22);
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            RangeAttack(unit, playerUnit, 7, true);
        }
        else
        {
            targetNum = RangeAttack(unit, playerUnit, 7, true);
        }
        if (!playerTarget)
        {

            HurtCharacter(enemyArray[targetNum], false);
            bool blind = enemyUnits[targetNum].Blind(4, unit);
            bool daze = enemyUnits[targetNum].Daze(4, unit);
            CanvasController.Instance.battleBattleText.text = "" + enemyUnits[targetNum].unitName;
            if (blind && daze)
            {
                CanvasController.Instance.battleStatusText.text = "is Blinded and Dazed!";
            }
            else if (blind)
            {
                CanvasController.Instance.battleStatusText.text = "is Blinded!";
            }
            else if (daze)
            {
                CanvasController.Instance.battleStatusText.text = "is Dazed!";
            }
            else
            {
                CanvasController.Instance.battleStatusText.text = "resisted Blind and Daze!";
            }
        }
        else
        {
            HurtCharacter(playerArray[targetNum], false);
            bool blind = playerUnits[targetNum].Blind(4, unit);
            bool daze = playerUnits[targetNum].Daze(4, unit);
            CanvasController.Instance.battleBattleText.text = "" + playerUnits[targetNum].unitName;
            if (blind && daze)
            {
                CanvasController.Instance.battleStatusText.text = "is Blinded and Dazed!";
            }
            else if (blind)
            {
                CanvasController.Instance.battleStatusText.text = "is Blinded!";
            }
            else if (daze)
            {
                CanvasController.Instance.battleStatusText.text = "is Dazed!";
            }
            else
            {
                CanvasController.Instance.battleStatusText.text = "resisted Blind and Daze!";
            }
        }
        CanvasController.Instance.battleSkillText.text = "casts Flash!";
    }

    private void Slow(UnitData unit, bool playerUnit)
    {
        SoundController.Instance.PlaySingle(24);
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            RangeAttack(unit, playerUnit, 9, true);
        }
        else
        {
            targetNum = RangeAttack(unit, playerUnit, 9, true);
        }
        if (!playerTarget)
        {
            HurtCharacter(enemyArray[targetNum], false);
            bool slow = enemyUnits[targetNum].Slow(4, unit);
            bool daze = enemyUnits[targetNum].Daze(4, unit);
            CanvasController.Instance.battleBattleText.text = "" + enemyUnits[targetNum].unitName;
            if (slow && daze)
            {
                CanvasController.Instance.battleStatusText.text = "is Slowed and Dazed!";
            }
            else if (slow)
            {
                CanvasController.Instance.battleStatusText.text = "is Slowed!";
            }
            else if (daze)
            {
                CanvasController.Instance.battleStatusText.text = "is Dazed!";
            }
            else
            {
                CanvasController.Instance.battleStatusText.text = "resisted Slow and Daze!";
            }
        }
        else
        {

            HurtCharacter(playerArray[targetNum], false);
            bool slow = playerUnits[targetNum].Slow(4, unit);
            bool daze = playerUnits[targetNum].Daze(4, unit);
            CanvasController.Instance.battleBattleText.text = "" + playerUnits[targetNum].unitName;
            if (slow && daze)
            {
                CanvasController.Instance.battleStatusText.text = "is Slowed and Dazed!";
            }
            else if (slow)
            {
                CanvasController.Instance.battleStatusText.text = "is Slowed!";
            }
            else if (daze)
            {
                CanvasController.Instance.battleStatusText.text = "is Dazed!";
            }
            else
            {
                CanvasController.Instance.battleStatusText.text = "resisted Slow and Daze!";
            }
        }
        CanvasController.Instance.battleSkillText.text = "casts Slow!";
    }

    /************
     * HUMAN 3RD TIER SKILL 2
     * ***********/

    private void Hack(UnitData unit, bool playerUnit)
    {
        SoundController.Instance.PlaySingle(13);
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            MeleeMovements(unit, playerUnit);
        }
        else
        {
            targetNum = MeleeMovements(unit, playerUnit);
        }
        int dmg = unit.GetStrength() * 3;
        dmg = ModifyDamage(unit, dmg);

        if (!playerTarget)
        {
            //Do damage
            if (enemyUnits[targetNum].TakeDamage(dmg, 1, unit))
            {
                //DeadCharacter(enemyUnits[targetNum], enemyArray[targetNum], false);
            }
            else
            {
                HurtCharacter(enemyArray[targetNum], false);
            }
        }
        else
        {
            if (playerUnits[targetNum].TakeDamage(dmg, 1, unit))
            {
                //DeadCharacter(playerUnits[targetNum], playerArray[targetNum], true);
            }
            else
            {
                HurtCharacter(playerArray[targetNum], false);
            }
        }
        CanvasController.Instance.battleSkillText.text = "uses Hack!";
    }

    private void Guard(UnitData unit, bool playerUnit)
    {
        SoundController.Instance.PlaySingle(14);
        playerTarget = !playerTarget;
        unit.DefUp();

        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            HealingSkill(unit, playerUnit, 11, 0);
        }
        else
        {
            targetNum = HealingSkill(unit, playerUnit, 11, 0);
        }

        if (playerTarget)
        {
            unit.GuardUnit(playerUnits[targetNum], playerArray[unit.positionNum]);
            CanvasController.Instance.battleBattleText.text = "" + playerUnits[targetNum].unitName;           
        }
        else
        {
            unit.GuardUnit(enemyUnits[targetNum], enemyArray[unit.positionNum]);
            CanvasController.Instance.battleBattleText.text = "" + enemyUnits[targetNum].unitName;

        }
        CanvasController.Instance.battleStatusText.text = " is Guarded!";
        CanvasController.Instance.battleSkillText.text = "uses Guard!";
    }

    private void Disarm(UnitData unit, bool playerUnit)
    {
        SoundController.Instance.PlaySingle(15);
      
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            MeleeMovements(unit, playerUnit);
        }
        else
        {
            targetNum = MeleeMovements(unit, playerUnit);
        }
        int dmg = unit.GetStrength() * 2;
        dmg = ModifyDamage(unit, dmg);

        if (!playerTarget)
        {
            //Do damage
            if (enemyUnits[targetNum].TakeDamage(dmg, 1, unit))
            {
                //DeadCharacter(enemyUnits[targetNum], enemyArray[targetNum], false);
            }
            else
            {
                HurtCharacter(enemyArray[targetNum], false);
                if(enemyUnits[targetNum].CDDelay(1, unit))
                {
                    CanvasController.Instance.battleStatusText.text = "and had cooldowns delayed!";
                }
                else
                {
                    CanvasController.Instance.battleStatusText.text = "resisted a cooldown delay!";
                }
            }
        }
        else
        {
            if (playerUnits[targetNum].TakeDamage(dmg, 1, unit))
            {
                //DeadCharacter(playerUnits[targetNum], playerArray[targetNum], true);
            }
            else
            {
                HurtCharacter(playerArray[targetNum], false);
                if(playerUnits[targetNum].CDDelay(1, unit))
                {
                    CanvasController.Instance.battleStatusText.text = "and had cooldowns delayed!";
                }
                else
                {
                    CanvasController.Instance.battleStatusText.text = "resisted a cooldown delay!";
                }
            }
        }
        CanvasController.Instance.battleSkillText.text = "uses Disarm!";
    }

    private void Snipe(UnitData unit, bool playerUnit)
    {
        SoundController.Instance.PlaySingle(16);
       
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            RangeAttack(unit, playerUnit, 3, true);
        }
        else
        {
            targetNum = RangeAttack(unit, playerUnit, 3, true);
        }
        if (!playerTarget)
        {
            //Do damage
            if (enemyUnits[targetNum].TakeDamage(unit.GetDexterity() * 3, 3, unit))
            {
                //DeadCharacter(enemyUnits[targetNum], enemyArray[targetNum], false);
            }
            else
            {
                HurtCharacter(enemyArray[targetNum], true);
            }
        }
        else
        {
            if (playerUnits[targetNum].TakeDamage(unit.GetDexterity() * 3, 3, unit))
            {
                //DeadCharacter(playerUnits[targetNum], playerArray[targetNum], true);
            }
            else
            {
                HurtCharacter(playerArray[targetNum], true);
            }
        }
        CanvasController.Instance.battleSkillText.text = "uses Snipe!";
    }

    private void Slice(UnitData unit, bool playerUnit)
    {
        SoundController.Instance.PlaySingle(18);
        
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            MeleeMovements(unit, playerUnit);
        }
        else
        {
            targetNum = MeleeMovements(unit, playerUnit);
        }
        int dmg = unit.GetStrength() * 2;
        dmg = ModifyDamage(unit, dmg);

        if (!playerTarget)
        {
            //Do damage
            if (enemyUnits[targetNum].TakeDamage(dmg, 1, unit))
            {
                //DeadCharacter(enemyUnits[targetNum], enemyArray[targetNum], false);
            }
            else
            {
                HurtCharacter(enemyArray[targetNum], false);
                if(enemyUnits[targetNum].Bleed(1, unit))
                {
                    CanvasController.Instance.battleStatusText.text = "and is Bleeding!";
                }
                else
                {
                    CanvasController.Instance.battleStatusText.text = "resisted Bleed!";
                }
            }
        }
        else
        {
            if (playerUnits[targetNum].TakeDamage(dmg, 1, unit))
            {
                //DeadCharacter(playerUnits[targetNum], playerArray[targetNum], true);
            }
            else
            {
                HurtCharacter(playerArray[targetNum], false);
                if(playerUnits[targetNum].Bleed(1, unit))
                {
                    CanvasController.Instance.battleStatusText.text = "and is Bleeding!";
                }
                else
                {
                    CanvasController.Instance.battleStatusText.text = "resisted Bleed!";
                }
            }
        }
        CanvasController.Instance.battleSkillText.text = "uses Slice!";
    }

    private void MagicStorm(UnitData unit, bool playerUnit)
    {
        SoundController.Instance.PlaySingle(19);
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            RangeAttack(unit, playerUnit, 4, false);
        }
        else
        {
            targetNum = RangeAttack(unit, playerUnit, 4, false);
        }
        //enemyPriorityNum == (int)Mathf.Abs(enemy.positionNum % 5 - modVal) + (enemy.positionNum / 5)
        int targetCount = 0;
        if (playerTarget)
        {
            SpecialEffect(4, new Vector3(playerArray[targetNum].transform.position.x, playerArray[targetNum].transform.position.y + 5f, playerArray[targetNum].transform.position.z));
            for (int i = 0; i < 25; i++)
            {
                if (playerUnits[i] != null && !playerUnits[i].isDead() && (Mathf.Abs((targetNum / 5) - (i / 5)) <= 1)  && (Mathf.Abs((targetNum % 5) - (i % 5)) <= 1) && (Mathf.Abs(targetNum - i) <= 6))
                {
                    targetCount++;
                    MagicStormSingleTarget(unit, i);
                }
            }
        }
        else
        {
            SpecialEffect(4, new Vector3(enemyArray[targetNum].transform.position.x, enemyArray[targetNum].transform.position.y + 5f, enemyArray[targetNum].transform.position.z));
            for (int i = 0; i < 25; i++)
            {
                if (enemyUnits[i] != null && !enemyUnits[i].isDead() && (Mathf.Abs((targetNum / 5) - (i / 5)) <= 1) && (Mathf.Abs((targetNum % 5) - (i % 5)) <= 1) && (Mathf.Abs(targetNum - i) <= 6))
                {
                    targetCount++;
                    MagicStormSingleTarget(unit, i);
                }
            }
        }
        CanvasController.Instance.battleBattleText.text = targetCount + " targets attacked for " + unit.GetIntelligence() * 2 + " potential damage!";
        CanvasController.Instance.battleSkillText.text = "casts Magic Storm!";
    }

    private void MagicStormSingleTarget(UnitData unit, int targetNum)
    {
        int dmg = unit.GetIntelligence() * 2;
        dmg = ModifyDamage(unit, dmg);

        if (!playerTarget)
        {
            //Do damage
            if (enemyUnits[targetNum].TakeDamage(dmg, 4, unit))
            {
                //DeadCharacter(enemyUnits[targetNum], enemyArray[targetNum], false);
            }
            else
            {
                HurtCharacter(enemyArray[targetNum], true);
            }
        }
        else
        {
            if (playerUnits[targetNum].TakeDamage(dmg, 4, unit))
            {
                //DeadCharacter(playerUnits[targetNum], playerArray[targetNum], true);
            }
            else
            {
                HurtCharacter(playerArray[targetNum], true);
            }
        }
    }

    private void Corrupt(UnitData unit, bool playerUnit)
    {
        SoundController.Instance.PlaySingle(21);
        int targetNum;
        
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            RangeAttack(unit, playerUnit, 6, false);
        }
        else
        {
            targetNum = RangeAttack(unit, playerUnit, 6, false);
        }
        if (playerTarget) {

            SpecialEffect(6, new Vector3(playerArray[targetNum].transform.position.x, playerArray[targetNum].transform.position.y + 5f, playerArray[targetNum].transform.position.z));
            if (!playerUnits[targetNum].ResistanceCheck(4, unit))
            {
                movingUnit = playerUnits[targetNum];
                //corruptedUnit = true;
                AutoTurn(playerUnits[targetNum], true);
                CanvasController.Instance.battleBattleText.text = playerUnits[targetNum].unitName;
                CanvasController.Instance.battleStatusText.text = "is Corrupted!";                
            }
            else
            {
                CanvasController.Instance.battleStatusText.text = "resisted Corrupt!";
            }                    
        }
        else
        {
            SpecialEffect(6, new Vector3(enemyArray[targetNum].transform.position.x, enemyArray[targetNum].transform.position.y + 5f, enemyArray[targetNum].transform.position.z));
            if (!enemyUnits[targetNum].ResistanceCheck(4, unit))
            {
                movingUnit = enemyUnits[targetNum];
                AutoTurn(enemyUnits[targetNum], false);
                //corruptedUnit = true;
                CanvasController.Instance.battleBattleText.text = enemyUnits[targetNum].unitName;
                CanvasController.Instance.battleStatusText.text = "is Corrupted!";
            }
            else
            {
                CanvasController.Instance.battleStatusText.text = "resisted Corrupt!";
            }
        }
        CanvasController.Instance.battleSkillText.text = "casts Corrupt!";
    }

    private void Incinerate(UnitData unit, bool playerUnit)
    {
        SoundController.Instance.PlaySingle(23);
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            RangeAttack(unit, playerUnit, 8, false);
        }
        else
        {
            targetNum = RangeAttack(unit, playerUnit, 8, false);
        }
        int dmg = unit.GetIntelligence() * 2;
        dmg = ModifyDamage(unit, dmg);

        if (!playerTarget)
        {
            //Do damage
            if (enemyUnits[targetNum].TakeDamage(dmg, 4, unit))
            {
                //DeadCharacter(enemyUnits[targetNum], enemyArray[targetNum], false);
            }
            else
            {
                HurtCharacter(enemyArray[targetNum], true);
                if(enemyUnits[targetNum].Burn(4, unit))
                {
                    CanvasController.Instance.battleStatusText.text = "and is Burning!";
                }
                else
                {
                    CanvasController.Instance.battleStatusText.text = "resisted Burn!";
                }
            }
        }
        else
        {
            if (playerUnits[targetNum].TakeDamage(dmg, 4, unit))
            {
                //DeadCharacter(playerUnits[targetNum], playerArray[targetNum], true);
            }
            else
            {
                HurtCharacter(playerArray[targetNum], true);
                if(playerUnits[targetNum].Burn(4, unit))
                {
                    CanvasController.Instance.battleStatusText.text = "and is Burning!";
                }
                else
                {
                    CanvasController.Instance.battleStatusText.text = "resisted Burn!";
                }
            }
        }
        CanvasController.Instance.battleSkillText.text = "casts Incinerate!";
    }

    private void Sleep(UnitData unit, bool playerUnit)
    {
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            RangeAttack(unit, playerUnit, 10, false);
        }
        else
        {
            targetNum = RangeAttack(unit, playerUnit, 10, false);
        }
        SoundController.Instance.PlaySingle(25);
        if (!playerTarget)
        {           
            HurtCharacter(enemyArray[targetNum], true);
            bool daze = enemyUnits[targetNum].Daze(4, unit);
            bool defdown =  enemyUnits[targetNum].DefDown(4, unit);
            bool slow = enemyUnits[targetNum].Slow(4, unit);
            CanvasController.Instance.battleBattleText.text = "" + enemyUnits[targetNum].unitName;

            if (slow && defdown && daze) {
                CanvasController.Instance.battleStatusText.text = "is Slowed and Dazed and has Defense Down!";
            }
            else if (slow && daze)
            {
                CanvasController.Instance.battleStatusText.text = "is Slowed and Dazed!";
            }
            else if (slow && defdown)
            {
                CanvasController.Instance.battleStatusText.text = "is Slowed and has Defense Down!";
            }
            else if (defdown && daze)
            {
                CanvasController.Instance.battleStatusText.text = "is Dazed and has Defense Down!";
            }
            else if (slow)
            {
                CanvasController.Instance.battleStatusText.text = "is Slowed!";
            }
            else if (daze)
            {
                CanvasController.Instance.battleStatusText.text = "is Dazed!";
            }
            else if (defdown)
            {
                CanvasController.Instance.battleStatusText.text = "has Defense Down!";
            }
            else
            {
                CanvasController.Instance.battleStatusText.text = "resisted Slow, Defense Down and Daze!";
            }
        }
        else
        {            
            HurtCharacter(playerArray[targetNum], true);
            bool daze = playerUnits[targetNum].Daze(4, unit);
            bool defdown = playerUnits[targetNum].DefDown(4, unit);
            bool slow =  playerUnits[targetNum].Slow(4, unit);
            CanvasController.Instance.battleBattleText.text = "" + playerUnits[targetNum].unitName;

            if (slow && defdown && daze)
            {
                CanvasController.Instance.battleStatusText.text = "is Slowed and Dazed and has Defense Down!";
            }
            else if (slow && daze)
            {
                CanvasController.Instance.battleStatusText.text = "is Slowed and Dazed!";
            }
            else if (slow && defdown)
            {
                CanvasController.Instance.battleStatusText.text = "is Slowed and has Defense Down!";
            }
            else if (defdown && daze)
            {
                CanvasController.Instance.battleStatusText.text = "is Dazed and has Defense Down!";
            }
            else if (slow)
            {
                CanvasController.Instance.battleStatusText.text = "is Slowed!";
            }
            else if (daze)
            {
                CanvasController.Instance.battleStatusText.text = "is Dazed!";
            }
            else if (defdown)
            {
                CanvasController.Instance.battleStatusText.text = "has Defense Down!";
            }
            else
            {
                CanvasController.Instance.battleStatusText.text = "resisted Slow, Defense Down and Daze!";
            }
        }
        CanvasController.Instance.battleSkillText.text = "casts Sleep!";
    }

    private void Knockout(UnitData unit, bool playerUnit)
    {
        SoundController.Instance.PlaySingle(26);
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            MeleeMovements(unit, playerUnit);
        }
        else
        {
            targetNum = MeleeMovements(unit, playerUnit);
        }
        int dmg = unit.GetStrength();
        dmg = ModifyDamage(unit, dmg);

        if (!playerTarget)
        {
            //Do damage
            if (enemyUnits[targetNum].TakeDamage(dmg, 1, unit))
            {
                //DeadCharacter(enemyUnits[targetNum], enemyArray[targetNum], false);
            }
            else
            {
                HurtCharacter(enemyArray[targetNum], false);
                bool daze = enemyUnits[targetNum].Daze(1, unit);
                bool defdown = enemyUnits[targetNum].DefDown(1, unit);
                if (defdown && daze)
                {
                    CanvasController.Instance.battleStatusText.text = "is Dazed and has Defense Down!";
                }
                else if (defdown)
                {
                    CanvasController.Instance.battleStatusText.text = "has Defense Down!";
                }
                else if (daze)
                {
                    CanvasController.Instance.battleStatusText.text = "is Dazed!";
                }
                else
                {
                    CanvasController.Instance.battleStatusText.text = "resisted Defense Down and Daze!";
                }
            }
        }
        else
        {
            if (playerUnits[targetNum].TakeDamage(dmg, 1, unit))
            {
                //DeadCharacter(playerUnits[targetNum], playerArray[targetNum], true);
            }
            else
            {
                HurtCharacter(playerArray[targetNum], false);
                bool daze = playerUnits[targetNum].Daze(1, unit);
                bool defdown = playerUnits[targetNum].DefDown(1, unit);

                if (defdown && daze)
                {
                    CanvasController.Instance.battleStatusText.text = "is Dazed and has Defense Down!";
                }
                else if (defdown)
                {
                    CanvasController.Instance.battleStatusText.text = "has Defense Down!";
                }
                else if (daze)
                {
                    CanvasController.Instance.battleStatusText.text = "is Dazed!";
                }
                else
                {
                    CanvasController.Instance.battleStatusText.text = "resisted Defense Down and Daze!";
                }
            }
        }
        CanvasController.Instance.battleSkillText.text = "uses Knockout!";
    }


    /************
    * HUMAN 4TH TIER SKILL 1
    * ***********/

    private void EarthSpike(UnitData unit, bool playerUnit)
    {
        SoundController.Instance.PlaySingle(29);
      
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            RangeAttack(unit, playerUnit, 14, false);
        }
        else
        {
            targetNum = RangeAttack(unit, playerUnit, 14, false);
        }
        int dmg = unit.GetIntelligence();
        dmg = ModifyDamage(unit, dmg);

        if (!playerTarget)
        {
            SpecialEffect(29, new Vector3(enemyArray[targetNum].transform.position.x, enemyArray[targetNum].transform.position.y + 5f, enemyArray[targetNum].transform.position.z));
            //Do damage
            if (enemyUnits[targetNum].TakeDamage(dmg, 4, unit))
            {
                //DeadCharacter(enemyUnits[targetNum], enemyArray[targetNum], false);
            }
            else
            {
                HurtCharacter(enemyArray[targetNum], true);
                if(enemyUnits[targetNum].Slow(4, unit))
                {
                    CanvasController.Instance.battleStatusText.text = "and is Slowed!";
                }
                else
                {
                    CanvasController.Instance.battleStatusText.text = "resisted Slow!";
                }
            }
        }
        else
        {
            SpecialEffect(29, new Vector3(playerArray[targetNum].transform.position.x, playerArray[targetNum].transform.position.y + 5f, playerArray[targetNum].transform.position.z));
            if (playerUnits[targetNum].TakeDamage(dmg, 4, unit))
            {
                //DeadCharacter(playerUnits[targetNum], playerArray[targetNum], true);
            }
            else
            {
                HurtCharacter(playerArray[targetNum], true);
                if(playerUnits[targetNum].Slow(4, unit))
                {
                    CanvasController.Instance.battleStatusText.text = "and is Slowed!";
                }
                else
                {
                    CanvasController.Instance.battleStatusText.text = "resisted Slow!";
                }
            }
        }
        CanvasController.Instance.battleSkillText.text = "casts Earthspike!";
    }

    private void Fireball(UnitData unit, bool playerUnit)
    {
        SoundController.Instance.PlaySingle(32);
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            RangeAttack(unit, playerUnit, 17, false);
        }
        else
        {
            targetNum = RangeAttack(unit, playerUnit, 17, false);
        }
        int dmg = unit.GetIntelligence();
        dmg = ModifyDamage(unit, dmg);

        if (!playerTarget)
        {
            //Do damage
            if (enemyUnits[targetNum].TakeDamage(dmg, 4, unit))
            {
                //DeadCharacter(enemyUnits[targetNum], enemyArray[targetNum], false);
            }
            else
            {
                HurtCharacter(enemyArray[targetNum], true);
                if(enemyUnits[targetNum].Burn(4, unit))
                {
                    CanvasController.Instance.battleStatusText.text = "and is Burned!";
                }
                else
                {
                    CanvasController.Instance.battleStatusText.text = "resisted Burn!";
                }
            }
        }
        else
        {
            if (playerUnits[targetNum].TakeDamage(dmg, 4, unit))
            {
                //DeadCharacter(playerUnits[targetNum], playerArray[targetNum], true);
            }
            else
            {
                HurtCharacter(playerArray[targetNum], true);
               if(playerUnits[targetNum].Burn(4, unit))
                {
                    CanvasController.Instance.battleStatusText.text = "and is Burned!";
                }
                else
                {
                    CanvasController.Instance.battleStatusText.text = "resisted Burn!";
                }
            }
        }
        CanvasController.Instance.battleSkillText.text = "casts Fireball!";
    }

    private void Refresh(UnitData unit, bool playerUnit)
    {
        SoundController.Instance.PlaySingle(34);
        playerTarget = !playerTarget;
        int dmg = unit.GetIntelligence() * -1;
        dmg = ModifyDamage(unit, dmg);

 
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            HealingSkill(unit, playerUnit, 19, unit.GetIntelligence() * -1);
        }
        else
        {
            targetNum = HealingSkill(unit, playerUnit, 19, unit.GetIntelligence() * -1);
        }

        if (playerTarget)
        {
            playerUnits[targetNum].TakeDamage(dmg, 6, unit);
            playerUnits[targetNum].Haste();

        }
        else
        {
            enemyUnits[targetNum].TakeDamage(dmg, 6, unit);
            enemyUnits[targetNum].Haste();
        }
        CanvasController.Instance.battleStatusText.text = "and has Haste!";
        CanvasController.Instance.battleSkillText.text = "casts Refresh!";
    }

    private void Tangle(UnitData unit, bool playerUnit)
    {
        SoundController.Instance.PlaySingle(40);
        
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            RangeAttack(unit, playerUnit, 27, false);
        }
        else
        {
            targetNum = RangeAttack(unit, playerUnit, 27, false);
        }

        if (!playerTarget)
        {
            HurtCharacter(enemyArray[targetNum], true);
            SpecialEffect(27, new Vector3(enemyArray[targetNum].transform.position.x, enemyArray[targetNum].transform.position.y + 5f, enemyArray[targetNum].transform.position.z));
            bool cddelay = enemyUnits[targetNum].CDDelay(4, unit);
            bool slow = enemyUnits[targetNum].Slow(4, unit);

            if (cddelay && slow)
            {
                CanvasController.Instance.battleStatusText.text = "is Slowed and had their cooldowns delayed!";
            }
            else if (cddelay)
            {
                CanvasController.Instance.battleStatusText.text = "had cooldowns delayed!";
            }
            else if (slow)
            {
                CanvasController.Instance.battleStatusText.text = "is Slowed!";
            }
            else
            {
                CanvasController.Instance.battleStatusText.text = "resisted Slow and a cooldown delay!";
            }
        }
        else
        {
            HurtCharacter(playerArray[targetNum], true);
            bool cddelay = playerUnits[targetNum].CDDelay(4, unit);
            bool slow = playerUnits[targetNum].Slow(4, unit);
            SpecialEffect(27, new Vector3(playerArray[targetNum].transform.position.x, playerArray[targetNum].transform.position.y + 5f, playerArray[targetNum].transform.position.z));
            if (cddelay && slow)
            {
                CanvasController.Instance.battleStatusText.text = "is Slowed and had their cooldowns delayed!";
            }
            else if (cddelay)
            {
                CanvasController.Instance.battleStatusText.text = "had cooldowns delayed!";
            }
            else if (slow)
            {
                CanvasController.Instance.battleStatusText.text = "is Slowed!";
            }
            else
            {
                CanvasController.Instance.battleStatusText.text = "resisted Slow and a cooldown delay!";
            }
        }
        CanvasController.Instance.battleSkillText.text = "casts Tangle!";
    }
    /************
    * HUMAN 4TH TIER SKILL 2
    * ***********/

    private void Rockslide(UnitData unit, bool playerUnit)
    {
        SoundController.Instance.PlaySingle(30);
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            RangeAttack(unit, playerUnit, 15, false);
        }
        else
        {
            targetNum = RangeAttack(unit, playerUnit, 15, false);
        }
        //enemyPriorityNum == (int)Mathf.Abs(enemy.positionNum % 5 - modVal) + (enemy.positionNum / 5)
        int targetCount = 0;
        if (playerTarget)
        {
            SpecialEffect(15, new Vector3(playerArray[targetNum].transform.position.x, playerArray[targetNum].transform.position.y + 5f, playerArray[targetNum].transform.position.z));
            for (int i = 0; i < 25; i++)
            {
                if (playerUnits[i] != null && !playerUnits[i].isDead() && (Mathf.Abs((targetNum / 5) - (i / 5)) <= 1) && (Mathf.Abs((targetNum % 5) - (i % 5)) <= 1) && (Mathf.Abs(targetNum - i) <= 6))
                {
                    targetCount++;
                    RockslideSingleTarget(unit, i);
                }
            }
        }
        else
        {
            SpecialEffect(15, new Vector3(enemyArray[targetNum].transform.position.x, enemyArray[targetNum].transform.position.y + 5f, enemyArray[targetNum].transform.position.z));
            for (int i = 0; i < 25; i++)
            {
                if (enemyUnits[i] != null && !enemyUnits[i].isDead() && (Mathf.Abs((targetNum / 5) - (i / 5)) <= 1) && (Mathf.Abs((targetNum % 5) - (i % 5)) <= 1) && (Mathf.Abs(targetNum - i) <= 6))
                {
                    targetCount++;
                    RockslideSingleTarget(unit, i);
                }
            }
        }
        CanvasController.Instance.battleSkillText.text = "casts Rockslide!";
        CanvasController.Instance.battleBattleText.text = targetCount + " targets attacked for " + unit.GetIntelligence() + " potential damage";
        CanvasController.Instance.battleStatusText.text = " and potentially dazed!";
    }

    private void RockslideSingleTarget(UnitData unit, int targetNum)
    {
        int dmg = unit.GetIntelligence();
        dmg = ModifyDamage(unit, dmg);

        if (!playerTarget)
        {
            //Do damage
            if (enemyUnits[targetNum].TakeDamage(dmg, 4, unit))
            {
                //DeadCharacter(enemyUnits[targetNum], enemyArray[targetNum], false);
            }
            else
            {
                enemyUnits[targetNum].Daze(4, unit);
                HurtCharacter(enemyArray[targetNum], true);
            }
        }
        else
        {
            if (playerUnits[targetNum].TakeDamage(dmg, 4, unit))
            {
                //DeadCharacter(playerUnits[targetNum], playerArray[targetNum], true);
            }
            else
            {
                playerUnits[targetNum].Daze(4, unit);
                HurtCharacter(playerArray[targetNum], true);
            }
        }
    }

    private void Flood(UnitData unit, bool playerUnit)
    {
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            RangeAttack(unit, playerUnit, 20, false);
        }
        else
        {
            targetNum = RangeAttack(unit, playerUnit, 20, false);
        }
        int dmg = unit.GetIntelligence();
        dmg = ModifyDamage(unit, dmg);
        SoundController.Instance.PlaySingle(35);
        if (!playerTarget)
        {
            SpecialEffect(20, new Vector3(enemyArray[targetNum].transform.position.x, enemyArray[targetNum].transform.position.y + 5f, enemyArray[targetNum].transform.position.z));
            //Do damage
            if (enemyUnits[targetNum].TakeDamage(dmg, 4, unit))
            {
                //DeadCharacter(enemyUnits[targetNum], enemyArray[targetNum], false);
            }
            else
            {
                HurtCharacter(enemyArray[targetNum], true);
                if(enemyUnits[targetNum].CDReset(4, unit))
                {
                    CanvasController.Instance.battleStatusText.text = "and had their cooldowns reset!";
                }
                else
                {
                    CanvasController.Instance.battleStatusText.text = "resisted having their cooldowns reset!";
                }
            }
        }
        else
        {
            SpecialEffect(20, new Vector3(playerArray[targetNum].transform.position.x, playerArray[targetNum].transform.position.y + 5f, playerArray[targetNum].transform.position.z));
            if (playerUnits[targetNum].TakeDamage(dmg, 4, unit))
            {
                //DeadCharacter(playerUnits[targetNum], playerArray[targetNum], true);
            }
            else
            {
                HurtCharacter(playerArray[targetNum], true);
                if(playerUnits[targetNum].CDReset(4, unit))
                {
                    CanvasController.Instance.battleStatusText.text = "and had their cooldowns reset!";
                }
                else
                {
                    CanvasController.Instance.battleStatusText.text = "resisted having their cooldowns reset!";
                }
            }
        }
        CanvasController.Instance.battleSkillText.text = "casts Flood!";

    }

    private void WindArmor(UnitData unit, bool playerUnit)
    {
        SoundController.Instance.PlaySingle(37);

        playerTarget = !playerTarget;
        int dmg = unit.GetIntelligence() * -1;
        dmg = ModifyDamage(unit, dmg);

        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            HealingSkill(unit, playerUnit, 22, unit.GetIntelligence() * -1);
        }
        else
        {
            targetNum = HealingSkill(unit, playerUnit, 22, unit.GetIntelligence() * -1);
        }

        if (playerTarget)
        {
            playerUnits[targetNum].TakeDamage(dmg, 6, unit);
            playerUnits[targetNum].Haste();
            playerUnits[targetNum].DefUp();
        }
        else
        {
            enemyUnits[targetNum].TakeDamage(dmg, 6, unit);
            enemyUnits[targetNum].Haste();
            enemyUnits[targetNum].DefUp();
        }
        CanvasController.Instance.battleStatusText.text = "and has Haste and Defense Up!";
        CanvasController.Instance.battleSkillText.text = "casts Wind Armor!";
    }

    /************
    * HUMAN 4TH TIER SKILL 3
    * ***********/

    private void Psychosis(UnitData unit, bool playerUnit)
    {
        SoundController.Instance.PlaySingle(27);
       
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            RangeAttack(unit, playerUnit, 12, false);
        }
        else
        {
            targetNum = RangeAttack(unit, playerUnit, 12, false);
        }

        if (playerTarget)
        {

            bool cddelay1 = playerUnits[targetNum].Daze(4, unit);
            bool slow = playerUnits[targetNum].Slow(4, unit);
            CanvasController.Instance.battleBattleText.text = "" + playerUnits[targetNum].unitName;
            SpecialEffect(12, new Vector3(playerArray[targetNum].transform.position.x, playerArray[targetNum].transform.position.y + 5f, playerArray[targetNum].transform.position.z));
            if (slow && cddelay1)
            {
                CanvasController.Instance.battleStatusText.text = "is Slowed, Dazed and Corrupted!";
            }
            else if (slow)
            {
                CanvasController.Instance.battleStatusText.text = "is Slowed and Corrupted!";
            }
            else if (cddelay1)
            {
                CanvasController.Instance.battleStatusText.text = "is Dazed and Corrupted!";
            }
            else
            {
                CanvasController.Instance.battleStatusText.text = "resisted Slow and Daze but is Corrupted!";
            }
            //corruptedUnit = true;
            movingUnit = playerUnits[targetNum];
            AutoTurn(playerUnits[targetNum], true);
        }
        else
        {
            bool cddelay1 = enemyUnits[targetNum].Daze(4, unit);
            bool slow = enemyUnits[targetNum].Slow(4, unit);
            CanvasController.Instance.battleBattleText.text = "" + enemyUnits[targetNum].unitName;
            SpecialEffect(12, new Vector3(enemyArray[targetNum].transform.position.x, enemyArray[targetNum].transform.position.y + 5f, enemyArray[targetNum].transform.position.z));
            if (slow && cddelay1)
            {
                CanvasController.Instance.battleStatusText.text = "is Slowed, Dazed and Corrupted!";
            }
            else if (slow)
            {
                CanvasController.Instance.battleStatusText.text = "is Slowed and Corrupted!";
            }
            else if (cddelay1)
            {
                CanvasController.Instance.battleStatusText.text = "is Dazed and Corrupted!";
            }
            else
            {
                CanvasController.Instance.battleStatusText.text = "resisted Slow and Daze but is Corrupted!";
            }
            //corruptedUnit = true;
            movingUnit = enemyUnits[targetNum];
            AutoTurn(enemyUnits[targetNum], false);

        }
        CanvasController.Instance.battleSkillText.text = "casts Psychosis!";
    }

    private void MagicAssault(UnitData unit, bool playerUnit)
    {
        SoundController.Instance.PlaySingle(28);
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            RangeAttack(unit, playerUnit, 13, false);
        }
        else
        {
            targetNum = RangeAttack(unit, playerUnit, 13, false);
        }
        //enemyPriorityNum == (int)Mathf.Abs(enemy.positionNum % 5 - modVal) + (enemy.positionNum / 5)
        int targetCount = 0;
        if (playerTarget)
        {
            for (int i = 0; i < 25; i++)
            {
                SpecialEffect(13, new Vector3(playerArray[targetNum].transform.position.x, playerArray[targetNum].transform.position.y + 5f, playerArray[targetNum].transform.position.z));
                if (playerUnits[i] != null && !playerUnits[i].isDead() && (Mathf.Abs((targetNum / 5) - (i / 5)) <= 1) && (Mathf.Abs((targetNum % 5) - (i % 5)) <= 1) && (Mathf.Abs(targetNum - i) <= 6))
                {
                    MagicAssaultSingleTarget(unit, i);
                    targetCount++;
                }
            }
        }
        else
        {
            SpecialEffect(13, new Vector3(enemyArray[targetNum].transform.position.x, enemyArray[targetNum].transform.position.y + 5f, enemyArray[targetNum].transform.position.z));
            for (int i = 0; i < 25; i++)
            {
                if (enemyUnits[i] != null && !enemyUnits[i].isDead() && (Mathf.Abs((targetNum / 5) - (i / 5)) <= 1) && (Mathf.Abs((targetNum % 5) - (i % 5)) <= 1) && (Mathf.Abs(targetNum - i) <= 6))
                {
                    MagicAssaultSingleTarget(unit, i);
                    targetCount++;
                }
            }
        }
        CanvasController.Instance.battleBattleText.text = targetCount + " targets attacked for " + unit.GetIntelligence() * 4 + " potential damage!";
        CanvasController.Instance.battleSkillText.text = "casts Magic Assault!";
    }

    private void MagicAssaultSingleTarget(UnitData unit, int targetNum)
    {
        int dmg = unit.GetIntelligence() * 4;
        dmg = ModifyDamage(unit, dmg);

        if (!playerTarget)
        {
            //Do damage
            if (enemyUnits[targetNum].TakeDamage(dmg, 4, unit))
            {
                //DeadCharacter(enemyUnits[targetNum], enemyArray[targetNum], false);
            }
            else
            {
                HurtCharacter(enemyArray[targetNum], true);
            }
        }
        else
        {
            if (playerUnits[targetNum].TakeDamage(dmg, 4, unit))
            {
                //DeadCharacter(playerUnits[targetNum], playerArray[targetNum], true);
            }
            else
            {
                HurtCharacter(playerArray[targetNum], true);
            }
        }
    }

    private void Earthquake(UnitData unit, bool playerUnit)
    {
        SoundController.Instance.PlaySingle(31);
        int targetCount = 0;
        if (playerTarget)
        {
            SpecialEffect(16, GameController.Instance.GetPlayerMiddlePosition());
            for (int i = 0; i < 25; i++)
            {
                if (playerUnits[i] != null && !playerUnits[i].isDead())
                {
                    targetCount++;
                    EarthquakeSingleTarget(unit, i);
                }
            }
        }
        else
        {
            SpecialEffect(16, GameController.Instance.GetEnemyMiddlePosition());
            for (int i = 0; i < 25; i++)
            {
                if (enemyUnits[i] != null && !enemyUnits[i].isDead())
                {
                    targetCount++;
                    EarthquakeSingleTarget(unit, i);
                }
            }
        }
        CanvasController.Instance.battleBattleText.text = targetCount + " targets attacked for " + unit.GetIntelligence() + " potential damage!";
        CanvasController.Instance.battleStatusText.text = " and potentially slowed!";
        CanvasController.Instance.battleSkillText.text = "casts Earthquake!";
    }

    private void EarthquakeSingleTarget(UnitData unit, int targetNum)
    {
        int dmg = unit.GetIntelligence();
        dmg = ModifyDamage(unit, dmg);

        if (!playerTarget)
        {
            //Do damage
            if (enemyUnits[targetNum].TakeDamage(dmg, 4, unit))
            {
                //DeadCharacter(enemyUnits[targetNum], enemyArray[targetNum], false);
            }
            else
            {
                enemyUnits[targetNum].Slow(4, unit);
                HurtCharacter(enemyArray[targetNum], true);
            }
        }
        else
        {
            if (playerUnits[targetNum].TakeDamage(dmg, 4, unit))
            {
                //DeadCharacter(playerUnits[targetNum], playerArray[targetNum], true);
            }
            else
            {
                playerUnits[targetNum].Slow(4, unit);
                HurtCharacter(playerArray[targetNum], true);
            }
        }
    }

    private void Inferno(UnitData unit, bool playerUnit)
    {
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            RangeAttack(unit, playerUnit, 18, false);
        }
        else
        {
            targetNum = RangeAttack(unit, playerUnit, 18, false);
        }
        //enemyPriorityNum == (int)Mathf.Abs(enemy.positionNum % 5 - modVal) + (enemy.positionNum / 5)
        int targetCount = 0;
        SoundController.Instance.PlaySingle(33);

        if (playerTarget)
        {
            SpecialEffect(18, new Vector3(playerArray[targetNum].transform.position.x, playerArray[targetNum].transform.position.y + 5f, playerArray[targetNum].transform.position.z));
            for (int i = 0; i < 25; i++)
            {
                if (playerUnits[i] != null && !playerUnits[i].isDead() && (Mathf.Abs((targetNum / 5) - (i / 5)) <= 1) && (Mathf.Abs((targetNum % 5) - (i % 5)) <= 1) && (Mathf.Abs(targetNum - i) <= 6))
                {
                    targetCount++;
                    InfernoSingleTarget(unit, i);
                }
            }
        }
        else
        {
            SpecialEffect(18, new Vector3(enemyArray[targetNum].transform.position.x, enemyArray[targetNum].transform.position.y + 5f, enemyArray[targetNum].transform.position.z));
            for (int i = 0; i < 25; i++)
            {
                if (enemyUnits[i] != null && !enemyUnits[i].isDead() && (Mathf.Abs((targetNum / 5) - (i / 5)) <= 1) && (Mathf.Abs((targetNum % 5) - (i % 5)) <= 1) && (Mathf.Abs(targetNum - i) <= 6))
                {
                    targetCount++;
                    InfernoSingleTarget(unit, i);
                }
            }
        }
        CanvasController.Instance.battleBattleText.text = targetCount + " targets attacked for " + unit.GetIntelligence() * 3 + " potential damage!";
        CanvasController.Instance.battleStatusText.text = " and potentially burned!";
        CanvasController.Instance.battleSkillText.text = "casts Inferno!";
    }

    private void InfernoSingleTarget(UnitData unit, int targetNum)
    {
        int dmg = unit.GetIntelligence() * 3;
        dmg = ModifyDamage(unit, dmg);

        if (!playerTarget)
        {
            //Do damage
            if (enemyUnits[targetNum].TakeDamage(dmg, 4, unit))
            {
                //DeadCharacter(enemyUnits[targetNum], enemyArray[targetNum], false);
            }
            else
            {
                enemyUnits[targetNum].Burn(4, unit);
                HurtCharacter(enemyArray[targetNum], true);
            }
        }
        else
        {
            if (playerUnits[targetNum].TakeDamage(dmg, 4, unit))
            {
                //DeadCharacter(playerUnits[targetNum], playerArray[targetNum], true);
            }
            else
            {
                playerUnits[targetNum].Burn(4, unit);
                HurtCharacter(playerArray[targetNum], true);
            }
        }
    }

    private void Tsunami(UnitData unit, bool playerUnit)
    {
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            RangeAttack(unit, playerUnit, 21, false);
        }
        else
        {
            targetNum = RangeAttack(unit, playerUnit, 21, false);
        }
        //enemyPriorityNum == (int)Mathf.Abs(enemy.positionNum % 5 - modVal) + (enemy.positionNum / 5)
        int targetCount = 0;
        SoundController.Instance.PlaySingle(36);

        if (playerTarget)
        {
            SpecialEffect(21, new Vector3(playerArray[targetNum].transform.position.x, playerArray[targetNum].transform.position.y + 5f, playerArray[targetNum].transform.position.z));
            for (int i = 0; i < 25; i++)
            {
                if (playerUnits[i] != null && !playerUnits[i].isDead() && (Mathf.Abs((targetNum / 5) - (i / 5)) <= 1) && (Mathf.Abs((targetNum % 5) - (i % 5)) <= 1) && (Mathf.Abs(targetNum - i) <= 6))
                {
                    targetCount++;
                    TsunamiSingleTarget(unit, i);
                }
            }
        }
        else
        {
            SpecialEffect(21, new Vector3(enemyArray[targetNum].transform.position.x, enemyArray[targetNum].transform.position.y + 5f, enemyArray[targetNum].transform.position.z));
            for (int i = 0; i < 25; i++)
            {
                if (enemyUnits[i] != null && !enemyUnits[i].isDead() && (Mathf.Abs((targetNum / 5) - (i / 5)) <= 1) && (Mathf.Abs((targetNum % 5) - (i % 5)) <= 1) && (Mathf.Abs(targetNum - i) <= 6))
                {
                    targetCount++;
                    TsunamiSingleTarget(unit, i);
                }
            }
        }
        CanvasController.Instance.battleBattleText.text = targetCount + " targets attacked for " + unit.GetIntelligence() * 2 + " potential damage!";
        CanvasController.Instance.battleStatusText.text = " and potentially had their cooldowns reset!";
        CanvasController.Instance.battleSkillText.text = "casts Tsunami!";
    }

    private void TsunamiSingleTarget(UnitData unit, int targetNum)
    {
        int dmg = unit.GetIntelligence() * 2;
        dmg = ModifyDamage(unit, dmg);

        if (!playerTarget)
        {
            //Do damage
            if (enemyUnits[targetNum].TakeDamage(dmg, 4, unit))
            {
                //DeadCharacter(enemyUnits[targetNum], enemyArray[targetNum], false);
            }
            else
            {
                enemyUnits[targetNum].CDReset(4, unit);
                HurtCharacter(enemyArray[targetNum], true);
            }
        }
        else
        {
            if (playerUnits[targetNum].TakeDamage(dmg, 4, unit))
            {
                //DeadCharacter(playerUnits[targetNum], playerArray[targetNum], true);
            }
            else
            {
                playerUnits[targetNum].CDReset(4, unit);
                HurtCharacter(playerArray[targetNum], true);
            }
        }
    }

    private void Hurricane(UnitData unit, bool playerUnit)
    {
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            RangeAttack(unit, playerUnit, 23, false);
        }
        else
        {
            targetNum = RangeAttack(unit, playerUnit, 23, false);
        }
        //enemyPriorityNum == (int)Mathf.Abs(enemy.positionNum % 5 - modVal) + (enemy.positionNum / 5)
        int targetCount = 0;
        SoundController.Instance.PlaySingle(38);

        if (playerTarget)
        {
            SpecialEffect(23, new Vector3(playerArray[targetNum].transform.position.x, playerArray[targetNum].transform.position.y + 5f, playerArray[targetNum].transform.position.z));
            for (int i = 0; i < 25; i++)
            {
                if (playerUnits[i] != null && !playerUnits[i].isDead() && (Mathf.Abs((targetNum / 5) - (i / 5)) <= 1) && (Mathf.Abs((targetNum % 5) - (i % 5)) <= 1) && (Mathf.Abs(targetNum - i) <= 6))
                {
                    targetCount++;
                    HurricaneSingleTarget(unit, i);
                }
            }
        }
        else
        {
            SpecialEffect(23, new Vector3(enemyArray[targetNum].transform.position.x, enemyArray[targetNum].transform.position.y + 5f, enemyArray[targetNum].transform.position.z));
            for (int i = 0; i < 25; i++)
            {
                if (enemyUnits[i] != null && !enemyUnits[i].isDead() && (Mathf.Abs((targetNum / 5) - (i / 5)) <= 1) && (Mathf.Abs((targetNum % 5) - (i % 5)) <= 1) && (Mathf.Abs(targetNum - i) <= 6))
                {
                    targetCount++;
                    HurricaneSingleTarget(unit, i);
                }
            }
        }
        CanvasController.Instance.battleBattleText.text = targetCount + " targets attacked for " + unit.GetIntelligence() * 2 + " potential damage!";
        CanvasController.Instance.battleStatusText.text = " and potentially Slowed and received Defense Down!";
        CanvasController.Instance.battleSkillText.text = "casts Hurricane!";
    }

    private void HurricaneSingleTarget(UnitData unit, int targetNum)
    {
        int dmg = unit.GetIntelligence() * 2;
        dmg = ModifyDamage(unit, dmg);

        if (!playerTarget)
        {
            //Do damage
            if (enemyUnits[targetNum].TakeDamage(dmg, 4, unit))
            {
                //DeadCharacter(enemyUnits[targetNum], enemyArray[targetNum], false);
            }
            else
            {
                enemyUnits[targetNum].DefDown(4, unit);
                enemyUnits[targetNum].Slow(4, unit);
                HurtCharacter(enemyArray[targetNum], true);
            }
        }
        else
        {
            if (playerUnits[targetNum].TakeDamage(dmg, 4, unit))
            {
                //DeadCharacter(playerUnits[targetNum], playerArray[targetNum], true);
            }
            else
            {
                playerUnits[targetNum].DefDown(4, unit);
                playerUnits[targetNum].Slow(4, unit);
                HurtCharacter(playerArray[targetNum], true);
            }
        }
    }

    private void MassHeal(UnitData unit, bool playerUnit)
    {
        playerTarget = !playerTarget;
      
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            HealingSkill(unit, playerUnit, 25, unit.GetIntelligence() * -4);
        }
        else
        {
            targetNum = HealingSkill(unit, playerUnit, 25, unit.GetIntelligence() * -4);
        }
        int targetCount = 0;
        SoundController.Instance.PlaySingle(39);

        if (playerTarget)
        {
            for (int i = 0; i < playerUnits.Length; i++)
            {
                //i != targetNum && 
                if (playerUnits[i] != null && !playerUnits[i].isDead() && ((Mathf.Abs((targetNum / 5) - (i / 5)) <= 1) && (Mathf.Abs((targetNum % 5) - (i % 5)) <= 1) && (Mathf.Abs(targetNum - i) <= 6)))
                {
                    MassHealSingleTarget(unit, i);
                    targetCount++;
                }
            }
        }
        else
        {
            for (int i = 0; i < enemyUnits.Length; i++)
            {
                //i != targetNum && 
                if (enemyUnits[i] != null && !enemyUnits[i].isDead() && ((Mathf.Abs((targetNum / 5) - (i / 5)) <= 1) && (Mathf.Abs((targetNum % 5) - (i % 5)) <= 1) && (Mathf.Abs(targetNum - i) <= 6)))
                {
                    MassHealSingleTarget(unit, i);
                    targetCount++;
                }
            }
        }
        CanvasController.Instance.battleSkillText.text = "casts Mass Heal!";
        CanvasController.Instance.battleBattleText.text = targetCount + " targets healed for " + unit.GetIntelligence() * 4 + " potential damage!";
    }

    private void MassHealSingleTarget(UnitData unit, int targetNum)
    {        
        int dmg = unit.GetIntelligence() * -4;
        dmg = ModifyDamage(unit, dmg);

        if (playerTarget)
        {
            playerUnits[targetNum].TakeDamage(dmg, 6, unit);
        }
        else
        {
            enemyUnits[targetNum].TakeDamage(dmg, 6, unit);
        }
    }

    private void NaturesBounty(UnitData unit, bool playerUnit)
    {
        playerTarget = !playerTarget;
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            HealingSkill(unit, playerUnit, 28, unit.GetIntelligence() * -2);
        }
        else
        {
            targetNum = HealingSkill(unit, playerUnit, 28, unit.GetIntelligence() * -2);
        }
        int targetCount = 0;
        SoundController.Instance.PlaySingle(41);

        if (playerTarget)
        {
            for (int i = 0; i < 25; i++)
            {
                if (playerUnits[i] != null && !playerUnits[i].isDead() && (Mathf.Abs((targetNum / 5) - (i / 5)) <= 1) && (Mathf.Abs((targetNum % 5) - (i % 5)) <= 1) && (Mathf.Abs(targetNum - i) <= 6))
                {
                    NaturesBountySingleTarget(unit, i);
                    targetCount++;
                }
            }
        }
        else
        {
            for (int i = 0; i < 25; i++)
            {
                if (enemyUnits[i] != null && !enemyUnits[i].isDead() && (Mathf.Abs((targetNum / 5) - (i / 5)) <= 1) && (Mathf.Abs((targetNum % 5) - (i % 5)) <= 1) && (Mathf.Abs(targetNum - i) <= 6))
                {
                    targetCount++;
                    NaturesBountySingleTarget(unit, i);
                }
            }
        }
        CanvasController.Instance.battleSkillText.text = "casts Nature's Bounty!";
        CanvasController.Instance.battleBattleText.text = targetCount + " targets healed for " + unit.GetIntelligence() * 2 + " potential damage!";
        CanvasController.Instance.battleStatusText.text = "and received Haste and Defense Up!";
    }

    private void NaturesBountySingleTarget(UnitData unit, int targetNum)
    {
        int dmg = unit.GetIntelligence() * -2;
        dmg = ModifyDamage(unit, dmg);

        if (playerTarget)
        {
            playerUnits[targetNum].TakeDamage(dmg, 6, unit);
            playerUnits[targetNum].DefUp();
            playerUnits[targetNum].Haste();
        }
        else
        {
            enemyUnits[targetNum].TakeDamage(dmg, 6, unit);
            enemyUnits[targetNum].DefUp();
            enemyUnits[targetNum].Haste();
        }
    }

    private void PoisonArrow(UnitData unit, bool playerUnit)
    {
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            RangeAttack(unit, playerUnit, 29, true);
        }
        else
        {
            targetNum = RangeAttack(unit, playerUnit, 29, true);
        }
        SoundController.Instance.PlaySingle(42);
        if (!playerTarget)
        {
            //Do damage
            if (enemyUnits[targetNum].TakeDamage(unit.GetDexterity() * 4, 3, unit))
            {
                //DeadCharacter(enemyUnits[targetNum], enemyArray[targetNum], false);
            }
            else
            {
                HurtCharacter(enemyArray[targetNum], true);
                if(enemyUnits[targetNum].Burn(4, unit))
                {
                    CanvasController.Instance.battleStatusText.text = "and is Burned!";
                }
                else
                {
                    CanvasController.Instance.battleStatusText.text = "resisted Burn!";
                }
            }
        }
        else
        {
            if (playerUnits[targetNum].TakeDamage(unit.GetDexterity() * 4, 3, unit))
            {
                //DeadCharacter(playerUnits[targetNum], playerArray[targetNum], true);
            }
            else
            {
                HurtCharacter(playerArray[targetNum], true);
                if(playerUnits[targetNum].Burn(4, unit))
                {
                    CanvasController.Instance.battleStatusText.text = "and is Burned!";
                }
                else
                {
                    CanvasController.Instance.battleStatusText.text = "resisted Burn!";
                }
            }
        }
        CanvasController.Instance.battleSkillText.text = "uses Poison Arrow!";
    }

    private void Keelhaul(UnitData unit, bool playerUnit)
    { 
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            SneakAttackMovements(unit, playerUnit);
        }
        else
        {
            targetNum = SneakAttackMovements(unit, playerUnit);
        }
        SoundController.Instance.PlaySingle(43);

        if (!playerTarget)
        {
            SpecialEffect(40, enemyArray[targetNum].gameObject.transform.position);

            //Do damage
            if (enemyUnits[targetNum].TakeDamage(unit.GetDexterity() * 3, 5, unit))
            {
                //DeadCharacter(enemyUnits[targetNum], enemyArray[targetNum], false);
            }
            else
            {
                HurtCharacter(enemyArray[targetNum], false);
                if(enemyUnits[targetNum].CDReset(5, unit))
                {
                    CanvasController.Instance.battleStatusText.text = "and had their cooldowns reset!";
                }
                else
                {
                    CanvasController.Instance.battleStatusText.text = "resisted having their cooldowns reset!";
                }
            }
        }
        else
        {
            SpecialEffect(40, playerArray[targetNum].gameObject.transform.position);
            if (playerUnits[targetNum].TakeDamage(unit.GetAgility() * 3, 5, unit))
            {
                //DeadCharacter(playerUnits[targetNum], playerArray[targetNum], true);
            }
            else
            {
                HurtCharacter(playerArray[targetNum], false);
                if(playerUnits[targetNum].CDReset(5, unit))
                {
                    CanvasController.Instance.battleStatusText.text = "and had their cooldowns reset!";
                }
                else
                {
                    CanvasController.Instance.battleStatusText.text = "resisted having their cooldowns reset!";
                }
            }
        }
        CanvasController.Instance.battleSkillText.text = "uses Keelhaul!";
    }

    private void Skewer(UnitData unit, bool playerUnit)
    {
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            SneakAttackMovements(unit, playerUnit);
        }
        else
        {
            targetNum = SneakAttackMovements(unit, playerUnit);
        }
        SoundController.Instance.PlaySingle(44);

        if (playerTarget)
        {
            SpecialEffect(41, playerArray[targetNum].gameObject.transform.position);
            //Do damage
            if (playerUnits[targetNum].TakeDamage(unit.GetAgility() * 5, 2, unit))
            {
                //DeadCharacter(playerUnits[targetNum], playerArray[targetNum], true);
            }
            else
            {
                HurtCharacter(playerArray[targetNum], false);
            }
        }
        else
        {
            SpecialEffect(41, enemyArray[targetNum].gameObject.transform.position);
            if (enemyUnits[targetNum].TakeDamage(unit.GetAgility() * 5, 2, unit))
            {
                //DeadCharacter(enemyUnits[targetNum], enemyArray[targetNum], false);
            }
            else
            {
                HurtCharacter(enemyArray[targetNum], false);
            }
        }
        CanvasController.Instance.battleSkillText.text = "uses Skewer!";
    }

    private void DestroySkill(UnitData unit, bool playerUnit)
    {
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            MeleeMovements(unit, playerUnit);
        }
        else
        {
            targetNum = MeleeMovements(unit, playerUnit);
        }
        int dmg = unit.GetStrength() * 5;
        dmg = ModifyDamage(unit, dmg);
        SoundController.Instance.PlaySingle(45);

        if (playerTarget)
        {
            SpecialEffect(42, playerArray[targetNum].gameObject.transform.position);
            //Do damage
            if (playerUnits[targetNum].TakeDamage(dmg, 1, unit))
            {
                //DeadCharacter(playerUnits[targetNum], playerArray[targetNum], true);
            }
            else
            {
                HurtCharacter(playerArray[targetNum], false);
            }
        }
        else
        {
            SpecialEffect(42, enemyArray[targetNum].gameObject.transform.position);
            if (enemyUnits[targetNum].TakeDamage(dmg, 1, unit))
            {
                //DeadCharacter(enemyUnits[targetNum], enemyArray[targetNum], false);
            }
            else
            {
                HurtCharacter(enemyArray[targetNum], false);
            }
        }
        CanvasController.Instance.battleSkillText.text = "uses Destroy!";
    }

    private void CriticalStrike(UnitData unit, bool playerUnit)
    {
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            MeleeMovements(unit, playerUnit);
        }
        else
        {
            targetNum = MeleeMovements(unit, playerUnit);
        }
        int dmg = unit.GetStrength();
        dmg = ModifyDamage(unit, dmg);
        SoundController.Instance.PlaySingle(47);


        if (playerTarget)
        {
            SpecialEffect(42, playerArray[targetNum].gameObject.transform.position);
            //Do damage
            if (playerUnits[targetNum].TakeDamage(dmg, 1, unit))
            {
                //DeadCharacter(playerUnits[targetNum], playerArray[targetNum], true);
            }
            else
            {
                HurtCharacter(playerArray[targetNum], false);
                bool bleed = playerUnits[targetNum].Bleed(1, unit);
                bool cddelay = playerUnits[targetNum].CDDelay(1, unit);
                bool daze = playerUnits[targetNum].Daze(1, unit);
                bool blind = playerUnits[targetNum].Blind(1, unit);
                if (bleed && blind && daze && cddelay)
                {
                    CanvasController.Instance.battleStatusText.text = "is bleeding, Blind, Dazed and had their cooldowns increased!";
                }
                else if (bleed && daze && cddelay)
                {
                    CanvasController.Instance.battleStatusText.text = "is Bleeding and Dazed and had their cooldowns increased!";
                }
                else if (blind && daze && cddelay)
                {
                    CanvasController.Instance.battleStatusText.text = "is Blind and Dazed and had their cooldowns increased!";
                }
                else if (bleed && blind && cddelay)
                {
                    CanvasController.Instance.battleStatusText.text = "is Bleeding and Blind and had their cooldowns increased!";
                }
                else if (bleed && daze && blind)
                {
                    CanvasController.Instance.battleStatusText.text = "is Bleeding and Dazed and Blind!";
                }
                else if (bleed && daze)
                {
                    CanvasController.Instance.battleStatusText.text = "is Bleeding and Dazed!";
                }
                else if (blind && daze)
                {
                    CanvasController.Instance.battleStatusText.text = "is Blind and Dazed!";
                }
                else if (cddelay && daze)
                {
                    CanvasController.Instance.battleStatusText.text = "is Dazed and had their cooldowns increased!";
                }
                else if (bleed && cddelay)
                {
                    CanvasController.Instance.battleStatusText.text = "is Bleeding and had their cooldowns increased!";
                }
                else if (blind && bleed)
                {
                    CanvasController.Instance.battleStatusText.text = "is Blind and Bleeding!";
                }
                else if (cddelay && blind)
                {
                    CanvasController.Instance.battleStatusText.text = "is Blind and had their cooldowns increased!";
                }
                else if (blind)
                {
                    CanvasController.Instance.battleStatusText.text = "is Blinded!";
                }
                else if (daze)
                {
                    CanvasController.Instance.battleStatusText.text = "is Dazed!";
                }
                else if (bleed)
                {
                    CanvasController.Instance.battleStatusText.text = "is Bleeding!";
                }
                else if (cddelay)
                {
                    CanvasController.Instance.battleStatusText.text = "had their cooldowns increased!";
                }
                else
                {
                    CanvasController.Instance.battleStatusText.text = "resisted Bleed, Slow, Daze and having their cooldowns increased!";
                }
            }
        }
        else
        {
            SpecialEffect(43, enemyArray[targetNum].gameObject.transform.position);
            if (enemyUnits[targetNum].TakeDamage(dmg, 1, unit))
            {
                //DeadCharacter(enemyUnits[targetNum], enemyArray[targetNum], false);
            }
            else
            {
                HurtCharacter(enemyArray[targetNum], false);
                bool bleed = enemyUnits[targetNum].Bleed(1, unit);
                bool cddelay = enemyUnits[targetNum].CDDelay(1, unit);
                bool daze = enemyUnits[targetNum].Daze(1, unit);
                bool blind = enemyUnits[targetNum].Blind(1, unit);
                if (bleed && blind && daze && cddelay)
                {
                    CanvasController.Instance.battleStatusText.text = "is bleeding, Blind, Dazed and had their cooldowns increased!";
                }
                else if (bleed && daze && cddelay)
                {
                    CanvasController.Instance.battleStatusText.text = "is Bleeding and Dazed and had their cooldowns increased!";
                }
                else if (blind && daze && cddelay)
                {
                    CanvasController.Instance.battleStatusText.text = "is Blind and Dazed and had their cooldowns increased!";
                }
                else if (bleed && blind && cddelay)
                {
                    CanvasController.Instance.battleStatusText.text = "is Bleeding and Blind and had their cooldowns increased!";
                }
                else if (bleed && daze && blind)
                {
                    CanvasController.Instance.battleStatusText.text = "is Bleeding and Dazed and Blind!";
                }
                else if (bleed && daze)
                {
                    CanvasController.Instance.battleStatusText.text = "is Bleeding and Dazed!";
                }
                else if (blind && daze)
                {
                    CanvasController.Instance.battleStatusText.text = "is Blind and Dazed!";
                }
                else if (cddelay && daze)
                {
                    CanvasController.Instance.battleStatusText.text = "is Dazed and had their cooldowns increased!";
                }
                else if (bleed && cddelay)
                {
                    CanvasController.Instance.battleStatusText.text = "is Bleeding and had their cooldowns increased!";
                }
                else if (blind && bleed)
                {
                    CanvasController.Instance.battleStatusText.text = "is Blind and Bleeding!";
                }
                else if (cddelay && blind)
                {
                    CanvasController.Instance.battleStatusText.text = "is Blind and had their cooldowns increased!";
                }
                else if (blind)
                {
                    CanvasController.Instance.battleStatusText.text = "is Blinded!";
                }
                else if (daze)
                {
                    CanvasController.Instance.battleStatusText.text = "is Dazed!";
                }
                else if (bleed)
                {
                    CanvasController.Instance.battleStatusText.text = "is Bleeding!";
                }
                else if (cddelay)
                {
                    CanvasController.Instance.battleStatusText.text = "had their cooldowns increased!";
                }
                else
                {
                    CanvasController.Instance.battleStatusText.text = "resisted Bleed, Slow, Daze and having their cooldowns increased!";
                }
            }
        }
        CanvasController.Instance.battleSkillText.text = "uses Critical Strike!";
    }

    private void WorkTheCrowd(UnitData unit, bool playerUnit)
    {
        int dmg = unit.GetIntelligence() * -3;
        dmg = ModifyDamage(unit, dmg);
        SoundController.Instance.PlaySingle(48);

        unit.TakeDamage(dmg, 6, unit);
        unit.Haste();
        unit.DefUp();
        unit.OffUp();
        CanvasController.Instance.battleSkillText.text = "casts Work the Crowd!";
        CanvasController.Instance.battleStatusText.text = "and received Haste, Offense Up and Defense Up!";
        if (playerUnit)
        {
            SpecialEffect(44, playerArray[unit.positionNum].gameObject.transform.position);
        }
        else
        {
            SpecialEffect(44, enemyArray[unit.positionNum].gameObject.transform.position);
        }
        
    }

    private void Fortify(UnitData unit, bool playerUnit)
    {
        playerTarget = !playerTarget;
        unit.DefUp();

        SoundController.Instance.PlaySingle(49);

        int dmg = unit.GetIntelligence() * -1;
        dmg = ModifyDamage(unit, dmg);

        unit.TakeDamage(dmg, 6, unit);

        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            HealingSkill(unit, playerUnit, 37, 0);
        }
        else
        {
            targetNum = HealingSkill(unit, playerUnit, 37, 0);
        }

        if (playerTarget)
        {

            unit.GuardUnit(playerUnits[targetNum], playerArray[unit.positionNum]);
            playerUnits[targetNum].TakeDamage(dmg, 6, unit);
            CanvasController.Instance.battleBattleText.text = "" + playerUnits[targetNum].unitName;
        }
        else
        {
            unit.GuardUnit(enemyUnits[targetNum], enemyArray[unit.positionNum]);
            enemyUnits[targetNum].TakeDamage(dmg, 6, unit);
            CanvasController.Instance.battleBattleText.text = "" + enemyUnits[targetNum].unitName;
        }

        CanvasController.Instance.battleStatusText.text = " is Guarded!";
        CanvasController.Instance.battleSkillText.text = "uses Fortify!";
    }

    private void DarkStrike(UnitData unit, bool playerUnit)
    {
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            RangeAttack(unit, playerUnit, 1, false);
        }
        else
        {
            targetNum = RangeAttack(unit, playerUnit, 1, false);
        }
        int dmg = unit.GetStrength() * 2;
        dmg = ModifyDamage(unit, dmg);

        SoundController.Instance.PlaySingle(50);

        if (playerTarget)
        {
            SpecialEffect(45, playerArray[targetNum].gameObject.transform.position);
            if (playerUnits[targetNum].TakeDamage(dmg, 1, unit))
            {
                //DeadCharacter(playerUnits[targetNum], playerArray[targetNum], true);
            }
            else
            {
                HurtCharacter(playerArray[targetNum], false);

                if (!playerUnits[targetNum].ResistanceCheck(1, unit))
                {
                    CanvasController.Instance.battleStatusText.text = "and is Corrupted!";
                    //corruptedUnit = true;
                    movingUnit = playerUnits[targetNum];
                    AutoTurn(playerUnits[targetNum], true);
                }
                else
                {
                    CanvasController.Instance.battleStatusText.text = "resisted Corrupt!";
                }
            }

        }
        else
        {
            SpecialEffect(45, enemyArray[targetNum].gameObject.transform.position);
            if (enemyUnits[targetNum].TakeDamage(dmg, 1, unit))
            {
                //DeadCharacter(enemyUnits[targetNum], enemyArray[targetNum], false);
            }
            else
            {
                HurtCharacter(enemyArray[targetNum], false);

                if (!enemyUnits[targetNum].ResistanceCheck(1, unit))
                {
                    CanvasController.Instance.battleStatusText.text = "and is Corrupted!";
                    //corruptedUnit = true;
                    movingUnit = enemyUnits[targetNum];
                    AutoTurn(enemyUnits[targetNum], false);
                }
                else
                {
                    CanvasController.Instance.battleStatusText.text = "resisted Corrupt!";
                }
            }

            
        }
        CanvasController.Instance.battleSkillText.text = "uses Dark Strike!";
    }

    private void AnimalAssault(UnitData unit, bool playerUnit)
    {
        int targetCount = 0;
        SoundController.Instance.PlaySingle(51);

        if (!playerTarget)
        {
            //Object reference not set!!!
            if (playerArray[unit.positionNum])
            {
                SpecialEffect(46, playerArray[unit.positionNum].gameObject.transform.position);
            }
            for (int i = 0; i < 25; i++)
            {
                if (playerUnits[i] != null && !playerUnits[i].isDead() && playerUnits[i].GetCharType() == 2)
                {
                    playerTarget = false;
                    AnimalAssaultSingleTarget(playerUnit, i);
                    targetCount++;
                }
            }
        }
        else
        {
            if (enemyArray[unit.positionNum])
            {
                SpecialEffect(46, enemyArray[unit.positionNum].gameObject.transform.position);
            }
            for (int i = 0; i < 25; i++)
            {
                if (enemyUnits[i] != null && !enemyUnits[i].isDead() && enemyUnits[i].GetCharType() == 2)
                {
                    playerTarget = true;
                    AnimalAssaultSingleTarget(playerUnit, i);
                    targetCount++;
                }
            }
        }
        CanvasController.Instance.battleSkillText.text = "uses a " + targetCount + " Animal Assault!";
    }

    private void AnimalAssaultSingleTarget(bool playerUnit, int mythNum)
    {
        autoTurn = true;
        //assaultAttack = true;
        if (playerUnit)
        {
            movingUnit = playerUnits[mythNum];

            Skill1(playerUnits[mythNum], playerUnit);
        }
        else
        {
            movingUnit = enemyUnits[mythNum];
            Skill1(enemyUnits[mythNum], playerUnit);
        }
    }

    private void CheapShot(UnitData unit, bool playerUnit)
    {
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            SneakAttackMovements(unit, playerUnit);
        }
        else
        {
            targetNum = SneakAttackMovements(unit, playerUnit);
        }
        SoundController.Instance.PlaySingle(52);

        if (playerTarget)
        {
            SpecialEffect(47, playerArray[targetNum].gameObject.transform.position);
            //Do damage
            if (playerUnits[targetNum].TakeDamage(unit.GetAgility(), 2, unit))
            {
                //DeadCharacter(playerUnits[targetNum], playerArray[targetNum], true);
            }
            else
            {
                HurtCharacter(playerArray[targetNum], false);
                bool blind = playerUnits[targetNum].Blind(2, unit);
                bool cdreset = playerUnits[targetNum].CDReset(2, unit);
                bool slow = playerUnits[targetNum].Slow(2, unit);
                if (slow && cdreset && blind)
                {
                    CanvasController.Instance.battleStatusText.text = "is Slowed, Blind and had their cooldowns reset!";
                }
                else if (slow && cdreset)
                {
                    CanvasController.Instance.battleStatusText.text = "is Slowed and had their cooldowns reset!";
                }
                else if (blind && cdreset)
                {
                    CanvasController.Instance.battleStatusText.text = "is Blind and had their cooldowns reset!";
                }
                else if (slow && blind)
                {
                    CanvasController.Instance.battleStatusText.text = "is Slowed and Blind!";
                }
                else if (slow)
                {
                    CanvasController.Instance.battleStatusText.text = "is Slowed!";
                }
                else if (blind)
                {
                    CanvasController.Instance.battleStatusText.text = "is Blind!";
                }
                else if (cdreset)
                {
                    CanvasController.Instance.battleStatusText.text = "had their cooldowns reset!";
                }
                else
                {
                    CanvasController.Instance.battleStatusText.text = "resisted Slow and Blind and having their cooldowns reset!";
                }
            }
        }
        else
        {
            SpecialEffect(47, enemyArray[targetNum].gameObject.transform.position);
            if (enemyUnits[targetNum].TakeDamage(unit.GetAgility(), 2, unit))
            {
                //DeadCharacter(enemyUnits[targetNum], enemyArray[targetNum], false);
            }
            else
            {
                HurtCharacter(enemyArray[targetNum], false);
                bool blind = enemyUnits[targetNum].Blind(2, unit);
                bool cdreset = enemyUnits[targetNum].CDReset(2, unit);
                bool slow = enemyUnits[targetNum].Slow(2, unit);
                if (slow && cdreset && blind)
                {
                    CanvasController.Instance.battleStatusText.text = "is Slowed, Blind and had their cooldowns reset!";
                }
                else if (slow && cdreset)
                {
                    CanvasController.Instance.battleStatusText.text = "is Slowed and had their cooldowns reset!";
                }
                else if (blind && cdreset)
                {
                    CanvasController.Instance.battleStatusText.text = "is Blind and had their cooldowns reset!";
                }
                else if (slow && blind)
                {
                    CanvasController.Instance.battleStatusText.text = "is Slowed and Blind!";
                }
                else if (slow)
                {
                    CanvasController.Instance.battleStatusText.text = "is Slowed!";
                }
                else if (blind)
                {
                    CanvasController.Instance.battleStatusText.text = "is Blind!";
                }
                else if (cdreset)
                {
                    CanvasController.Instance.battleStatusText.text = "had their cooldowns reset!";
                }
                else
                {
                    CanvasController.Instance.battleStatusText.text = "resisted Slow and Blind and having their cooldowns reset!";
                }
            }
        }
        CanvasController.Instance.battleSkillText.text = "uses Cheap Shot!";
    }

    /************
    * HUMAN 5TH TIER SKILL 1
    * ***********/

    /************
    * HUMAN 5TH TIER SKILL 2
    * ***********/

    /************
    * HUMAN 5TH TIER SKILL 3
    * ***********/

    private void MagicalOnslaught(UnitData unit, bool playerUnit)
    {
        int targetCount = 0;
        SoundController.Instance.PlaySingle(53);

        if (playerTarget)
        {
            SpecialEffect(30, GameController.Instance.GetPlayerMiddlePosition());
            for (int i = 0; i < 25; i++)
            {
                if (playerUnits[i] != null && !playerUnits[i].isDead())
                {
                    targetCount++;
                    MagicalOnslaughtSingleTarget(unit, i);
                }                
            }
        }
        else
        {
            SpecialEffect(30, GameController.Instance.GetEnemyMiddlePosition());
            for (int i = 0; i < 25; i++)
            {
                if (enemyUnits[i] != null && !enemyUnits[i].isDead())
                {
                    targetCount++;
                    MagicalOnslaughtSingleTarget(unit, i);
                }
            }
        }
        CanvasController.Instance.battleSkillText.text = "casts Magical Onslaught!";
        CanvasController.Instance.battleBattleText.text = targetCount + " targets attacked for " + unit.GetIntelligence() * 3 + " potential damage!";
    }

    private void MagicalOnslaughtSingleTarget(UnitData unit, int targetNum)
    {
        int dmg = unit.GetIntelligence() * 3;
        dmg = ModifyDamage(unit, dmg);

        if (!playerTarget)
        {
            //Do damage
            if (enemyUnits[targetNum].TakeDamage(dmg, 4, unit))
            {
                //DeadCharacter(enemyUnits[targetNum], enemyArray[targetNum], false);
            }
            else
            {
                HurtCharacter(enemyArray[targetNum], true);
            }
        }
        else
        {
            if (playerUnits[targetNum].TakeDamage(dmg, 4, unit))
            {
                //DeadCharacter(playerUnits[targetNum], playerArray[targetNum], true);
            }
            else
            {
                HurtCharacter(playerArray[targetNum], true);
            }
        }
    }

    private void Revive(UnitData unit, bool playerUnit)
    {
        playerTarget = !playerTarget;
        SoundController.Instance.PlaySingle(54);

        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            HealingSkill(unit, playerUnit, 31, unit.GetConstitution() * -300);
        }
        else
        {
            targetNum = HealingSkill(unit, playerUnit, 31, unit.GetConstitution() * -300);
        }

        if (playerTarget)
        {
            playerUnits[targetNum].TakeDamage(playerUnits[targetNum].GetConstitution() * -30, 6, unit);
        }
        else
        {
            enemyUnits[targetNum].TakeDamage(enemyUnits[targetNum].GetConstitution() * -30, 6, unit);
        }
        CanvasController.Instance.battleSkillText.text = "casts Revive!";
    }

    private void Bullseye(UnitData unit, bool playerUnit)
    {
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            RangeAttack(unit, playerUnit, 32, true);
        }
        else
        {
            targetNum = RangeAttack(unit, playerUnit, 32, true);
        }
        SoundController.Instance.PlaySingle(55);
        if (!playerTarget)
        {
            //Do damage
            if (enemyUnits[targetNum].TakeDamage(unit.GetDexterity() * 6, 3, unit))
            {
               //DeadCharacter(enemyUnits[targetNum], enemyArray[targetNum], false);
            }
            else
            {
                HurtCharacter(enemyArray[targetNum], true);
            }
        }
        else
        {
            if (playerUnits[targetNum].TakeDamage(unit.GetDexterity() * 6, 3, unit))
            {
                //DeadCharacter(playerUnits[targetNum], playerArray[targetNum], true);
            }
            else
            {
                HurtCharacter(playerArray[targetNum], true);
            }
        }
        CanvasController.Instance.battleSkillText.text = "uses Bullseye!";
    }

    private void Assassinate(UnitData unit, bool playerUnit)
    {
        int targetNum; 
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            SneakAttackMovements(unit, playerUnit);
        }
        else
        {
            targetNum = SneakAttackMovements(unit, playerUnit);
        }
        SoundController.Instance.PlaySingle(56);
        if (playerTarget)
        {
            SpecialEffect(48, playerArray[targetNum].gameObject.transform.position);
            //Do damage
            if (playerUnits[targetNum].TakeDamage(unit.GetAgility() * 6, 2, unit))
            {
                //DeadCharacter(playerUnits[targetNum], playerArray[targetNum], true);
            }
            else
            {
                HurtCharacter(playerArray[targetNum], false);
            }
        }
        else
        {
            SpecialEffect(48, enemyArray[targetNum].gameObject.transform.position);
            if (enemyUnits[targetNum].TakeDamage(unit.GetAgility() * 6, 2, unit))
            {
                //DeadCharacter(enemyUnits[targetNum], enemyArray[targetNum], false);
            }
            else
            {
                HurtCharacter(enemyArray[targetNum], false);
            }
        }
        CanvasController.Instance.battleSkillText.text = "uses Assassinate!";
    }

    private void GuardianAngel(UnitData unit, bool playerUnit)
    {
        playerTarget = !playerTarget;
        unit.DefUp();
        int targetCount = 0;
        SoundController.Instance.PlaySingle(57);

        if (playerTarget)
        {
            SpecialEffect(49, GameController.Instance.GetPlayerMiddlePosition());
            for (int i = 0; i < 25; i++)
            {
                if (playerUnits[i] != null && !playerUnits[i].isDead())
                {
                    GuardianAngelSingleTarget(unit, i);
                    targetCount++;
                }
            }
        }
        else
        {
            SpecialEffect(49, GameController.Instance.GetEnemyMiddlePosition());
            for (int i = 0; i < 25; i++)
            {
                if (enemyUnits[i] != null && !enemyUnits[i].isDead()) { 
                    GuardianAngelSingleTarget(unit, i);
                    targetCount++;
                }
            }
        }
        CanvasController.Instance.battleSkillText.text = "uses Guardian Angel!";
        CanvasController.Instance.battleStatusText.text = " and is guarding " + targetCount + " teammates!";
    }

    private void GuardianAngelSingleTarget(UnitData unit, int targetNum)
    {

        if (playerTarget)
        {
            unit.GuardUnit(playerUnits[targetNum], playerArray[unit.positionNum]);
        }
        else
        {
            unit.GuardUnit(enemyUnits[targetNum], enemyArray[unit.positionNum]);
        }
    }

    private void Execute(UnitData unit, bool playerUnit)
    {
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            MeleeMovements(unit, playerUnit);
        }
        else
        {
            targetNum = MeleeMovements(unit, playerUnit);
        }
        int dmg = unit.GetStrength() * 6;
        dmg = ModifyDamage(unit, dmg);
        SoundController.Instance.PlaySingle(58);

        if (playerTarget)
        {
            SpecialEffect(50, playerArray[targetNum].gameObject.transform.position);
            //Do damage
            if (playerUnits[targetNum].TakeDamage(dmg, 1, unit))
            {
                //DeadCharacter(playerUnits[targetNum], playerArray[targetNum], true);
            }
            else
            {
                HurtCharacter(playerArray[targetNum], false);
            }
        }
        else
        {
            SpecialEffect(50, enemyArray[targetNum].gameObject.transform.position);
            if (enemyUnits[targetNum].TakeDamage(dmg, 1, unit))
            {
                //DeadCharacter(enemyUnits[targetNum], enemyArray[targetNum], false);
            }
            else
            {
                HurtCharacter(enemyArray[targetNum], false);
            }
        }
        CanvasController.Instance.battleSkillText.text = "uses Execute!";
    }

    private void Hellstrike(UnitData unit, bool playerUnit)
    {
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            RangeAttack(unit, playerUnit, 1, false);
        }
        else
        {
            targetNum = RangeAttack(unit, playerUnit, 1, false);
        }
        int dmg = unit.GetStrength() * 3;
        dmg = ModifyDamage(unit, dmg);
        SoundController.Instance.PlaySingle(60);

        if (playerTarget)
        {
            SpecialEffect(52, playerArray[targetNum].gameObject.transform.position);
            if (playerUnits[targetNum].TakeDamage(dmg, 1, unit))
            {
                //DeadCharacter(playerUnits[targetNum], playerArray[targetNum], true);
            }
            else
            {
                HurtCharacter(playerArray[targetNum], false);

                if (!playerUnits[targetNum].ResistanceCheck(1, unit))
                {
                    CanvasController.Instance.battleStatusText.text = "and is Corrupted!";
                    //corruptedUnit = true;
                    movingUnit = playerUnits[targetNum];
                    AutoTurn(playerUnits[targetNum], false);
                }
                else
                {
                    CanvasController.Instance.battleStatusText.text = "resisted Corrupt!";
                }
            }            
        }
        else
        {
            SpecialEffect(52, enemyArray[targetNum].gameObject.transform.position);
            if (enemyUnits[targetNum].TakeDamage(dmg, 1, unit))
            {
                //DeadCharacter(enemyUnits[targetNum], enemyArray[targetNum], false);
            }
            else
            {
                HurtCharacter(enemyArray[targetNum], false);

                if (!enemyUnits[targetNum].ResistanceCheck(1, unit))
                {
                    CanvasController.Instance.battleStatusText.text = "and is Corrupted!";
                    //corruptedUnit = true;
                    movingUnit = playerUnits[targetNum];
                    AutoTurn(enemyUnits[targetNum], false);
                }
                else
                {
                    CanvasController.Instance.battleStatusText.text = "resisted Corrupt!";
                }
            }
        }
        CanvasController.Instance.battleSkillText.text = "uses Hellstrike!";
    }

    private void Pilfer(UnitData unit, bool playerUnit)
    {
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            SneakAttackMovements(unit, playerUnit);
        }
        else
        {
            targetNum = SneakAttackMovements(unit, playerUnit);
        }
        unit.DefUp();
        unit.Haste();
        unit.OffUp();
        SoundController.Instance.PlaySingle(61);

        if (playerTarget)
        {
            SpecialEffect(53, playerArray[targetNum].gameObject.transform.position);
            HurtCharacter(playerArray[targetNum], false);
            bool blind = playerUnits[targetNum].Blind(2, unit);
            bool defdown = playerUnits[targetNum].DefDown(2, unit);
            bool slow = playerUnits[targetNum].Slow(2, unit);
            
            if(blind && defdown && slow)
            {
                CanvasController.Instance.battleStatusText.text = "is Slowed and Blind and has Defense Down!";
            }
            else if (slow && blind)
            {
                CanvasController.Instance.battleStatusText.text = "is Slowed and Blind!";
            }
            else if (slow && defdown)
            {
                CanvasController.Instance.battleStatusText.text = "is Slowed and has Defense Down!";
            }
            else if (blind && defdown)
            {
                CanvasController.Instance.battleStatusText.text = "is Blind and has Defense Down!";
            }
            else if (blind)
            {
                CanvasController.Instance.battleStatusText.text = "is Blind!";
            }
            else if (slow)
            {
                CanvasController.Instance.battleStatusText.text = "is Slowed!";
            }
            else if (defdown)
            {
                CanvasController.Instance.battleStatusText.text = "has Defense Down!";
            }
            else
            {
                CanvasController.Instance.battleStatusText.text = "resisted Slow and Blind and Defense Down!";
            }
        }
        else
        {            
             HurtCharacter(enemyArray[targetNum], false);
             bool blind = enemyUnits[targetNum].Blind(2, unit);
             bool defdown = enemyUnits[targetNum].DefDown(2, unit);
             bool slow = enemyUnits[targetNum].Slow(2, unit);
            SpecialEffect(53, enemyArray[targetNum].gameObject.transform.position);
            if (blind && defdown && slow)
            {
                CanvasController.Instance.battleStatusText.text = "is Slowed and Blind and has Defense Down!";
            }
            else if (slow && blind)
            {
                CanvasController.Instance.battleStatusText.text = "is Slowed and Blind!";
            }
            else if (slow && defdown)
            {
                CanvasController.Instance.battleStatusText.text = "is Slowed and has Defense Down!";
            }
            else if (blind && defdown)
            {
                CanvasController.Instance.battleStatusText.text = "is Blind and has Defense Down!";
            }
            else if (blind)
            {
                CanvasController.Instance.battleStatusText.text = "is Blind!";
            }
            else if (slow)
            {
                CanvasController.Instance.battleStatusText.text = "is Slowed!";
            }
            else if (defdown)
            {
                CanvasController.Instance.battleStatusText.text = "has Defense Down!";
            }
            else
            {
                CanvasController.Instance.battleStatusText.text = "resisted Slow and Blind and Defense Down!";
            }

        }
        CanvasController.Instance.battleSkillText.text = "uses Pilfer!";
    }

    private void MythicAssault(UnitData unit, bool playerUnit)
    {
        autoTurn = true;
        int targetCount = 0;
        SoundController.Instance.PlaySingle(62);
        if (!playerTarget)
        {
            if (playerArray[unit.positionNum])
            {
                SpecialEffect(54, playerArray[unit.positionNum].gameObject.transform.position);
            }
            for (int i = 0; i < 25; i++)
            {
                if (playerUnits[i] != null && !playerUnits[i].isDead() && playerUnits[i].GetCharType() == 3)
                {
                    playerTarget = false;
                    MythicAssaultSingleTarget(playerUnit, i);
                    targetCount++;
                }
            }
        }
        else
        {
            if (enemyArray[unit.positionNum])
            {
                SpecialEffect(54, enemyArray[unit.positionNum].gameObject.transform.position);
            }
            //SpecialEffect(54, enemyArray[unit.positionNum].gameObject.transform.position);
            for (int i = 0; i < 25; i++)
            {
                if (enemyUnits[i] != null && !enemyUnits[i].isDead() && enemyUnits[i].GetCharType() == 3)
                {
                    playerTarget = true;
                    MythicAssaultSingleTarget(playerUnit, i);
                    targetCount++;
                }
            }
        }
        CanvasController.Instance.battleSkillText.text = "uses a " + targetCount + " Mythic Assault!";
    }

    private void MythicAssaultSingleTarget(bool playerUnit, int mythNum)
    {
        autoTurn = true;
        //assaultAttack = true;
        if (playerUnit)
        {
            movingUnit = playerUnits[mythNum];

            Skill1(playerUnits[mythNum], playerUnit);
        }
        else
        {
            movingUnit = enemyUnits[mythNum];
            Skill1(enemyUnits[mythNum], playerUnit);
        }
    }

    private void MassHysteria(UnitData unit, bool playerUnit)
    {
        int targetCount = 0;
        SoundController.Instance.PlaySingle(63);
        if (playerTarget)
        {
            SpecialEffect(55, GameController.Instance.GetPlayerMiddlePosition());
            for (int i = 0; i < 25; i++)
            {
                if (playerUnits[i] != null && !playerUnits[i].isDead())
                {
                    playerTarget = true;
                    MassHysteriaSingleTarget(unit, i);
                    targetCount++;
                }
            }
        }
        else
        {
            SpecialEffect(55, GameController.Instance.GetEnemyMiddlePosition());
            for (int i = 0; i < 25; i++)
            {
                if ( enemyUnits[i] != null && !enemyUnits[i].isDead())
                {
                    playerTarget = false;
                    MassHysteriaSingleTarget(unit, i);
                    targetCount++;
                }
            }
        }
        CanvasController.Instance.battleSkillText.text = "casts Mass Hysteria!";
        CanvasController.Instance.battleStatusText.text = targetCount + " targets potentially corrupted!";
    }

    private void MassHysteriaSingleTarget(UnitData unit, int targetNum)
    {
        if (playerTarget && playerUnits[targetNum] != null)
        {
            if (!playerUnits[targetNum].ResistanceCheck(4, unit))
            {
                //corruptedUnit = true;
                movingUnit = playerUnits[targetNum];
                AutoTurn(playerUnits[targetNum], true);
            }
        }
        else if(!playerTarget && enemyUnits[targetNum] != null)
        {
            if (!enemyUnits[targetNum].ResistanceCheck(4, unit))
            {
                //corruptedUnit = true;
                movingUnit = enemyUnits[targetNum];
                AutoTurn(enemyUnits[targetNum], false);
            }
        }
    }


    /***********
    * Animal
    * *********/
    private void SneakBite(UnitData unit, bool playerUnit)
    {
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            SneakAttackMovements(unit, playerUnit);
        }
        else
        {
            targetNum = SneakAttackMovements(unit, playerUnit);
        }
        SoundController.Instance.PlaySingle(64);

        if (targetNum == -1)
        {
            return;
        }

        if (!playerTarget)
        {
            //Do damage
            if (enemyUnits[targetNum].TakeDamage(unit.GetAgility() * 2, 2, unit))
            {
                //DeadCharacter(enemyUnits[targetNum], enemyArray[targetNum], false);
            }
            else
            {
                HurtCharacter(enemyArray[targetNum], false);
            }
        }
        else
        {
            if (playerUnits[targetNum].TakeDamage(unit.GetAgility() * 2, 2, unit))
            {
                //DeadCharacter(playerUnits[targetNum], playerArray[targetNum], true);
            }
            else
            {
                HurtCharacter(playerArray[targetNum], false);
            }
        }
        CanvasController.Instance.battleSkillText.text = "uses Nip!";
    }


    private void Bite(UnitData unit, bool playerUnit)
    {
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            MeleeMovements(unit, playerUnit);
        }
        else
        {
            targetNum = MeleeMovements(unit, playerUnit);
        }
        int dmg = unit.GetStrength() * 2;
        dmg = ModifyDamage(unit, dmg);
        SoundController.Instance.PlaySingle(65);

        if(targetNum == -1)
        {
            return;
        }

        if (!playerTarget)
        {
            //Do damage
            if (enemyUnits[targetNum].TakeDamage(dmg, 1, unit))
            {
                //DeadCharacter(enemyUnits[targetNum], enemyArray[targetNum], false);
            }
            else
            {
                HurtCharacter(enemyArray[targetNum], false);
            }
        }
        else
        {
            if (playerUnits[targetNum].TakeDamage(dmg, 1, unit))
            {
                //DeadCharacter(playerUnits[targetNum], playerArray[targetNum], true);
            }
            else
            {
                HurtCharacter(playerArray[targetNum], false);
            }
        }
        CanvasController.Instance.battleSkillText.text = "uses Bite!";
    }

    private void Gore(UnitData unit, bool playerUnit)
    {
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            MeleeMovements(unit, playerUnit);
        }
        else
        {
            targetNum = MeleeMovements(unit, playerUnit);
        }
        int dmg = unit.GetStrength();
        dmg = ModifyDamage(unit, dmg);
        SoundController.Instance.PlaySingle(66);

        if (targetNum == -1)
        {
            return;
        }

        if (!playerTarget)
        {
            //Do damage
            if (enemyUnits[targetNum].TakeDamage(dmg, 1, unit))
            {
                //DeadCharacter(enemyUnits[targetNum], enemyArray[targetNum], false);
            }
            else
            {
                HurtCharacter(enemyArray[targetNum], false);
                if(enemyUnits[targetNum].Slow(1, unit))
                {
                    CanvasController.Instance.battleStatusText.text = "and is Slowed!";
                }
                else
                {
                    CanvasController.Instance.battleStatusText.text = "resisted Slow!";
                }
            }
        }
        else
        {
            if (playerUnits[targetNum].TakeDamage(dmg, 1, unit))
            {
                //DeadCharacter(playerUnits[targetNum], playerArray[targetNum], true);
            }
            else
            {
                HurtCharacter(playerArray[targetNum], false);
                if(playerUnits[targetNum].Slow(1, unit))
                {
                    CanvasController.Instance.battleStatusText.text = "and is Slowed!";
                }
                else
                {
                    CanvasController.Instance.battleStatusText.text = "resisted Slow!";
                }
            }
        }
        CanvasController.Instance.battleSkillText.text = "uses Gore!";
    }

    private void Claw(UnitData unit, bool playerUnit)
    {
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            SneakAttackMovements(unit, playerUnit);
        }
        else
        {
            targetNum = SneakAttackMovements(unit, playerUnit);
        }
        SoundController.Instance.PlaySingle(77);

        if (targetNum == -1)
        {
            return;
        }

        if (!playerTarget)
        {
            //Do damage
            if (enemyUnits[targetNum].TakeDamage(unit.GetDexterity() * 2, 3, unit))
            {
                //DeadCharacter(enemyUnits[targetNum], enemyArray[targetNum], false);
            }
            else
            {
                HurtCharacter(enemyArray[targetNum], false);
            }
        }
        else
        {
            if (playerUnits[targetNum].TakeDamage(unit.GetDexterity() * 2, 3, unit))
            {
                //DeadCharacter(playerUnits[targetNum], playerArray[targetNum], true);
            }
            else
            {
                HurtCharacter(playerArray[targetNum], false);
            }
        }
        CanvasController.Instance.battleSkillText.text = "uses Claw!";
    }

    private void Savage(UnitData unit, bool playerUnit)
    {
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            MeleeMovements(unit, playerUnit);
        }
        else
        {
            targetNum = MeleeMovements(unit, playerUnit);
        }
        int dmg = unit.GetStrength() * 3;
        dmg = ModifyDamage(unit, dmg);

        SoundController.Instance.PlaySingle(67);

        if (!playerTarget)
        {
            //Do damage
            if (enemyUnits[targetNum].TakeDamage(dmg, 1, unit))
            {
                //DeadCharacter(enemyUnits[targetNum], enemyArray[targetNum], false);
            }
            else
            {
                HurtCharacter(enemyArray[targetNum], false);
            }
        }
        else
        {
            if (playerUnits[targetNum].TakeDamage(dmg, 1, unit))
            {
                //DeadCharacter(playerUnits[targetNum], playerArray[targetNum], true);
            }
            else
            {
                HurtCharacter(playerArray[targetNum], false);
            }
        }
        CanvasController.Instance.battleSkillText.text = "uses Savage!";
    }

    private void Trample(UnitData unit, bool playerUnit)
    {
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            MeleeMovements(unit, playerUnit);
        }
        else
        {
            targetNum = MeleeMovements(unit, playerUnit);
        }
        int dmg = unit.GetStrength() * 2;
        dmg = ModifyDamage(unit, dmg);

        SoundController.Instance.PlaySingle(68);

        if (!playerTarget)
        {
            //Do damage
            if (enemyUnits[targetNum].TakeDamage(dmg, 1, unit))
            {
                //DeadCharacter(enemyUnits[targetNum], enemyArray[targetNum], false);
            }
            else
            {
                HurtCharacter(enemyArray[targetNum], false);
                if(enemyUnits[targetNum].Daze(1, unit))
                {
                    CanvasController.Instance.battleStatusText.text = "and is Dazed!";
                }
                else
                {
                    CanvasController.Instance.battleStatusText.text = "resisted Daze!";
                }
            }
        }
        else
        {
            if (playerUnits[targetNum].TakeDamage(dmg, 1, unit))
            {
                //DeadCharacter(playerUnits[targetNum], playerArray[targetNum], true);
            }
            else
            {
                HurtCharacter(playerArray[targetNum], false);
                if(playerUnits[targetNum].Daze(1, unit))
                {
                    CanvasController.Instance.battleStatusText.text = "and is Dazed!";
                }
                else
                {
                    CanvasController.Instance.battleStatusText.text = "resisted Daze!";
                }
            }
        }
        CanvasController.Instance.battleSkillText.text = "uses Trample!";
    }

    private void Peck(UnitData unit, bool playerUnit)
    {
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            SneakAttackMovements(unit, playerUnit);
        }
        else
        {
            targetNum = SneakAttackMovements(unit, playerUnit);
        }

        SoundController.Instance.PlaySingle(70);

        if (!playerTarget)
        {
            //Do damage
            if (enemyUnits[targetNum].TakeDamage(unit.GetDexterity() * 3, 3, unit))
            {
                //DeadCharacter(enemyUnits[targetNum], enemyArray[targetNum], false);
            }
            else
            {
                HurtCharacter(enemyArray[targetNum], false);
            }
        }
        else
        {
            if (playerUnits[targetNum].TakeDamage(unit.GetDexterity() * 3, 3, unit))
            {
                //DeadCharacter(playerUnits[targetNum], playerArray[targetNum], true);
            }
            else
            {
                HurtCharacter(playerArray[targetNum], false);
            }
        }
        CanvasController.Instance.battleSkillText.text = "uses Peck!";
    }

    private void Stomp(UnitData unit, bool playerUnit)
    {
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            MeleeMovements(unit, playerUnit);
        }
        else
        {
            targetNum = MeleeMovements(unit, playerUnit);
        }
        int dmg = unit.GetStrength() * 3;
        dmg = ModifyDamage(unit, dmg);

        SoundController.Instance.PlaySingle(69);

        if (!playerTarget)
        {
            SpecialEffect(56, enemyArray[targetNum].gameObject.transform.position);
            //Do damage
            if (enemyUnits[targetNum].TakeDamage(dmg, 1, unit))
            {
                //DeadCharacter(enemyUnits[targetNum], enemyArray[targetNum], false);
            }
            else
            {
                HurtCharacter(enemyArray[targetNum], false);
                bool daze = enemyUnits[targetNum].Daze(1, unit);
                bool defdown = enemyUnits[targetNum].DefDown(1, unit);
                if(daze && defdown)
                {
                    CanvasController.Instance.battleStatusText.text = "and is Dazed and has Defense Down!";
                }
                else if (daze)
                {
                    CanvasController.Instance.battleStatusText.text = "and is Dazed!";
                }
                else if (defdown)
                {
                    CanvasController.Instance.battleStatusText.text = "and has Defense Down!";
                }
                else
                {
                    CanvasController.Instance.battleStatusText.text = "resisted Daze and Defense Down!";
                }
            }
        }
        else
        {

            SpecialEffect(56, playerArray[targetNum].gameObject.transform.position);
            if (playerUnits[targetNum].TakeDamage(dmg, 1, unit))
            {
                //DeadCharacter(playerUnits[targetNum], playerArray[targetNum], true);
            }
            else
            {
                HurtCharacter(playerArray[targetNum], false);
                bool daze = playerUnits[targetNum].Daze(1, unit);
                bool defdown = playerUnits[targetNum].DefDown(1, unit);

                if (daze && defdown)
                {
                    CanvasController.Instance.battleStatusText.text = "and is Dazed and has Defense Down!";
                }
                else if (daze)
                {
                    CanvasController.Instance.battleStatusText.text = "and is Dazed!";
                }
                else if (defdown)
                {
                    CanvasController.Instance.battleStatusText.text = "and has Defense Down!";
                }
                else
                {
                    CanvasController.Instance.battleStatusText.text = "resisted Daze and Defense Down!";
                }
            }
        }
        CanvasController.Instance.battleSkillText.text = "uses Stomp!";
    }

    private void Drown(UnitData unit, bool playerUnit)
    {
        SoundController.Instance.PlaySingle(78);
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            SneakAttackMovements(unit, playerUnit);
        }
        else
        {
            targetNum = SneakAttackMovements(unit, playerUnit);
        }

        if (!playerTarget)
        {
            SpecialEffect(57, enemyArray[targetNum].gameObject.transform.position);
            //BONUS DMG since animals don't have a lot of agi gain
            if (enemyUnits[targetNum].TakeDamage(unit.GetAgility() * 4, 2, unit))
            {
                //DeadCharacter(enemyUnits[targetNum], enemyArray[targetNum], false);
            }
            else
            {
                bool daze = enemyUnits[targetNum].Daze(2, unit);
                bool slow = enemyUnits[targetNum].Slow(2, unit);
                HurtCharacter(enemyArray[targetNum], false);
                if (daze && slow)
                {
                    CanvasController.Instance.battleStatusText.text = "and is Dazed and Slowed!";
                }
                else if (daze)
                {
                    CanvasController.Instance.battleStatusText.text = "and is Dazed!";
                }
                else if (slow)
                {
                    CanvasController.Instance.battleStatusText.text = "and is Slowed!";
                }
                else
                {
                    CanvasController.Instance.battleStatusText.text = "resisted Daze and Slow!";
                }
            }
        }
        else
        {
            SpecialEffect(57, playerArray[targetNum].gameObject.transform.position);

            if (playerUnits[targetNum].TakeDamage(unit.GetAgility() * 4, 2, unit))
            {
                //DeadCharacter(playerUnits[targetNum], playerArray[targetNum], true);
            }
            else
            {
                bool slow = playerUnits[targetNum].Slow(2, unit);
                bool daze = playerUnits[targetNum].Daze(2, unit);
                HurtCharacter(playerArray[targetNum], false);
                if (daze && slow)
                {
                    CanvasController.Instance.battleStatusText.text = "and is Dazed and Slowed!";
                }
                else if (daze)
                {
                    CanvasController.Instance.battleStatusText.text = "and is Dazed!";
                }
                else if (slow)
                {
                    CanvasController.Instance.battleStatusText.text = "and is Slowed!";
                }
                else
                {
                    CanvasController.Instance.battleStatusText.text = "resisted Daze and Slow!";
                }
            }
        }
        CanvasController.Instance.battleSkillText.text = "uses Drown!";
    }

    /***********
    * Myth
    * *********/

    private void Sting(UnitData unit, bool playerUnit)
    {
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            MeleeMovements(unit, playerUnit);
        }
        else
        {
            targetNum = MeleeMovements(unit, playerUnit);
        }

        int dmg = unit.GetIntelligence();
        dmg = ModifyDamage(unit, dmg);

        SoundController.Instance.PlaySingle(71);

        if (!playerTarget)
        {
            //Do damage
            if (enemyUnits[targetNum].TakeDamage(dmg, 4, unit))
            {
                //DeadCharacter(enemyUnits[targetNum], enemyArray[targetNum], false);
            }
            else
            {
                HurtCharacter(enemyArray[targetNum], true);
                bool burn = enemyUnits[targetNum].Burn(4, unit);
                bool bleed = enemyUnits[targetNum].Bleed(4, unit);
                if (burn && bleed)
                {
                    CanvasController.Instance.battleStatusText.text = "and is Burned and Bleeding!";
                }
                else if (burn)
                {
                    CanvasController.Instance.battleStatusText.text = "and is Burned!";
                }
                else if (bleed)
                {
                    CanvasController.Instance.battleStatusText.text = "and is Bleeding!";
                }
                else
                {
                    CanvasController.Instance.battleStatusText.text = "resisted Bleed and Burn!";
                }
            }
        }
        else
        {
            if (playerUnits[targetNum].TakeDamage(dmg, 4, unit))
            {
                //DeadCharacter(playerUnits[targetNum], playerArray[targetNum], true);
            }
            else
            {
                HurtCharacter(playerArray[targetNum], true);
                bool burn = playerUnits[targetNum].Burn(4, unit);
                bool bleed = playerUnits[targetNum].Bleed(4, unit);
                if (burn && bleed)
                {
                    CanvasController.Instance.battleStatusText.text = "and is Burned and Bleeding!";
                }
                else if (burn)
                {
                    CanvasController.Instance.battleStatusText.text = "and is Burned!";
                }
                else if (bleed)
                {
                    CanvasController.Instance.battleStatusText.text = "and is Bleeding!";
                }
                else
                {
                    CanvasController.Instance.battleStatusText.text = "resisted Bleed and Burn!";
                }
            }
        }
        CanvasController.Instance.battleSkillText.text = "uses Sting!";
    }

    private void Poison(UnitData unit, bool playerUnit)
    {
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            MeleeMovements(unit, playerUnit);
        }
        else
        {
            targetNum = MeleeMovements(unit, playerUnit);
        }

        SoundController.Instance.PlaySingle(72);

        if (!playerTarget)
        {
            HurtCharacter(enemyArray[targetNum], true);
            bool bleed = enemyUnits[targetNum].Slow(4, unit);
            bool defdown = enemyUnits[targetNum].OffDown(4, unit);
            bool burn = enemyUnits[targetNum].Burn(4, unit);

            if (burn && bleed && defdown)
            {
                CanvasController.Instance.battleStatusText.text = "and is Burned and Slowed and has Offense Down!";
            }
            else if (burn && bleed)
            {
                CanvasController.Instance.battleStatusText.text = "and is Burned and Slowed!";
            }
            else if (burn && defdown)
            {
                CanvasController.Instance.battleStatusText.text = "and is Burned and has Offense Down!";
            }
            else if (defdown && bleed)
            {
                CanvasController.Instance.battleStatusText.text = "and is Slowed and has Offense Down!";
            }
            else if (burn)
            {
                CanvasController.Instance.battleStatusText.text = "and is Burned!";
            }
            else if (bleed)
            {
                CanvasController.Instance.battleStatusText.text = "and is Slowed!";
            }
            else if (defdown)
            {
                CanvasController.Instance.battleStatusText.text = "and has Offense Down!";
            }
            else
            {
                CanvasController.Instance.battleStatusText.text = "resisted Slow and Burn and Offense Down!";
            }
        }
        else
        {
            HurtCharacter(playerArray[targetNum], true);
            bool bleed = playerUnits[targetNum].Bleed(4, unit);
            bool defdown = playerUnits[targetNum].DefDown(4, unit);
            bool burn = playerUnits[targetNum].Burn(4, unit);

            if (burn && bleed && defdown)
            {
                CanvasController.Instance.battleStatusText.text = "and is Burned and Slowed and has Offense Down!";
            }
            else if (burn && bleed)
            {
                CanvasController.Instance.battleStatusText.text = "and is Burned and Slowed!";
            }
            else if (burn && defdown)
            {
                CanvasController.Instance.battleStatusText.text = "and is Burned and has Offense Down!";
            }
            else if (defdown && bleed)
            {
                CanvasController.Instance.battleStatusText.text = "and is Slowed and has Offense Down!";
            }
            else if (burn)
            {
                CanvasController.Instance.battleStatusText.text = "and is Burned!";
            }
            else if (bleed)
            {
                CanvasController.Instance.battleStatusText.text = "and is Slowed!";
            }
            else if (defdown)
            {
                CanvasController.Instance.battleStatusText.text = "and has Offense Down!";
            }
            else
            {
                CanvasController.Instance.battleStatusText.text = "resisted Slow and Burn and Offense Down!";
            }
        }
        CanvasController.Instance.battleSkillText.text = "uses Poison!";
    }

    private void Rebirth(UnitData unit, bool playerUnit)
    {
        SoundController.Instance.PlaySingle(73);
        unit.TakeDamage(unit.GetConstitution() * -20, 6, unit);
        unit.Haste();
        unit.DefUp();
        unit.OffUp();
        CanvasController.Instance.battleSkillText.text = "uses Rebirth!";
        CanvasController.Instance.battleStatusText.text = "and has Haste, Offense Up and Defense Up!";
        if (playerUnit)
        {
            SpecialEffect(58, playerArray[unit.positionNum].gameObject.transform.position);
        }
        else
        {
            SpecialEffect(58, enemyArray[unit.positionNum].gameObject.transform.position);
        }

    }

    private void Petrify(UnitData unit, bool playerUnit)
    {
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            RangeAttack(unit, playerUnit, 35, false);
        }
        else
        {
            targetNum = RangeAttack(unit, playerUnit, 35, false);
        }
        //enemyPriorityNum == (int)Mathf.Abs(enemy.positionNum % 5 - modVal) + (enemy.positionNum / 5)
        int targetCount = 0;

        SoundController.Instance.PlaySingle(74);

        if (playerTarget)
        {
            SpecialEffect(35, new Vector3(playerArray[targetNum].transform.position.x, playerArray[targetNum].transform.position.y + 5f, playerArray[targetNum].transform.position.z));
            for (int i = 0; i < 25; i++)
            {
                if (playerUnits[i] != null && !playerUnits[i].isDead() && (Mathf.Abs((targetNum / 5) - (i / 5)) <= 1) && (Mathf.Abs((targetNum % 5) - (i % 5)) <= 1) && (Mathf.Abs(targetNum - i) <= 6))
                {
                    targetCount++;
                    PetrifySingleTarget(unit, i);
                }
            }
        }
        else
        {
            SpecialEffect(35, new Vector3(enemyArray[targetNum].transform.position.x, enemyArray[targetNum].transform.position.y + 5f, enemyArray[targetNum].transform.position.z));
            for (int i = 0; i < 25; i++)
            {
                if (enemyUnits[i] != null && !enemyUnits[i].isDead() && (Mathf.Abs((targetNum / 5) - (i / 5)) <= 1) && (Mathf.Abs((targetNum % 5) - (i % 5)) <= 1) && (Mathf.Abs(targetNum - i) <= 6))
                {
                    targetCount++;
                    PetrifySingleTarget(unit, i);
                }
            }
        }
        CanvasController.Instance.battleSkillText.text = "uses Petrify!";
        CanvasController.Instance.battleStatusText.text = targetCount + " enemies potentially suffering from Daze, Slow, Offense Down and having their cooldowns delayed!";

    }

    private void PetrifySingleTarget(UnitData unit, int targetNum)
    {
        if (!playerTarget)
        {
            HurtCharacter(enemyArray[targetNum], true);
            enemyUnits[targetNum].Daze(4, unit);
            enemyUnits[targetNum].Slow(4, unit);
            enemyUnits[targetNum].OffDown(4, unit);
            enemyUnits[targetNum].CDDelay(4, unit);
        }
        else
        {
            HurtCharacter(playerArray[targetNum], true);
            playerUnits[targetNum].Daze(4, unit);
            playerUnits[targetNum].Slow(4, unit);
            playerUnits[targetNum].OffDown(4, unit);
            playerUnits[targetNum].CDDelay(4, unit);
        }
    }

    private void Hellfire(UnitData unit, bool playerUnit)
    {
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            RangeAttack(unit, playerUnit, 36, false);
        }
        else
        {
            targetNum = RangeAttack(unit, playerUnit, 36, false);
        }
        //enemyPriorityNum == (int)Mathf.Abs(enemy.positionNum % 5 - modVal) + (enemy.positionNum / 5)
        int targetCount = 0;

        SoundController.Instance.PlaySingle(75);

        if (playerTarget)
        {
            SpecialEffect(36, new Vector3(playerArray[targetNum].transform.position.x, playerArray[targetNum].transform.position.y + 5f, playerArray[targetNum].transform.position.z));
            for (int i = 0; i < 25; i++)
            {
                if (playerUnits[i] != null && !playerUnits[i].isDead() && (Mathf.Abs((targetNum / 5) - (i / 5)) <= 1) && (Mathf.Abs((targetNum % 5) - (i % 5)) <= 1) && (Mathf.Abs(targetNum - i) <= 6))
                {
                    targetCount++;
                    HellfireSingleTarget(unit, i);
                }
            }
        }
        else
        {
            SpecialEffect(36, new Vector3(enemyArray[targetNum].transform.position.x, enemyArray[targetNum].transform.position.y + 5f, enemyArray[targetNum].transform.position.z));
            for (int i = 0; i < 25; i++)
            {
                if (enemyUnits[i] != null && !enemyUnits[i].isDead() && (Mathf.Abs((targetNum / 5) - (i / 5)) <= 1) && (Mathf.Abs((targetNum % 5) - (i % 5)) <= 1) && (Mathf.Abs(targetNum - i) <= 6))
                {
                    targetCount++;
                    HellfireSingleTarget(unit, i);
                }
            }
        }
        CanvasController.Instance.battleSkillText.text = "uses Hellfire!";
        CanvasController.Instance.battleBattleText.text = targetCount + " targets attacked for " + unit.GetIntelligence() * 2 + " potential damage";
        CanvasController.Instance.battleStatusText.text = " and potentially burning and with reduced offense!";
    }

    private void HellfireSingleTarget(UnitData unit, int targetNum)
    {
        int dmg = unit.GetIntelligence() * 2;
        dmg = ModifyDamage(unit, dmg);

        if (!playerTarget)
        {
            //Do damage
            if (enemyUnits[targetNum].TakeDamage(dmg, 4, unit))
            {
                //DeadCharacter(enemyUnits[targetNum], enemyArray[targetNum], false);
            }
            else
            {
                HurtCharacter(enemyArray[targetNum], true);
                enemyUnits[targetNum].Burn(4, unit);
                enemyUnits[targetNum].OffDown(4, unit);
            }
        }
        else
        {
            if (playerUnits[targetNum].TakeDamage(dmg, 4, unit))
            {
                //DeadCharacter(playerUnits[targetNum], playerArray[targetNum], true);
            }
            else
            {
                HurtCharacter(playerArray[targetNum], true);
                playerUnits[targetNum].Burn(4, unit);
                playerUnits[targetNum].OffDown(4, unit);
            }
        }
    }

    private void Plague(UnitData unit, bool playerUnit)
    {
        int targetNum;
        if (!autoTurn)
        {
            targetNum = playerTargetNum;
            RangeAttack(unit, playerUnit, 33, false);
        }
        else
        {
            targetNum = RangeAttack(unit, playerUnit, 33, false);
        }
        //enemyPriorityNum == (int)Mathf.Abs(enemy.positionNum % 5 - modVal) + (enemy.positionNum / 5)
        int targetCount = 0;

        SoundController.Instance.PlaySingle(76);

        if (playerTarget)
        {
            SpecialEffect(33, new Vector3(playerArray[targetNum].transform.position.x, playerArray[targetNum].transform.position.y + 5f, playerArray[targetNum].transform.position.z));
            for (int i = 0; i < 25; i++)
            {
                if (playerUnits[i] != null && !playerUnits[i].isDead() && (Mathf.Abs((targetNum / 5) - (i / 5)) <= 1) && (Mathf.Abs((targetNum % 5) - (i % 5)) <= 1) && (Mathf.Abs(targetNum - i) <= 6))
                {
                    targetCount++;
                    PlagueSingleTarget(unit, i);
                }
            }
        }
        else
        {
            SpecialEffect(33, new Vector3(enemyArray[targetNum].transform.position.x, enemyArray[targetNum].transform.position.y + 5f, enemyArray[targetNum].transform.position.z));
            for (int i = 0; i < 25; i++)
            {
                if (enemyUnits[i] != null && !enemyUnits[i].isDead() && (Mathf.Abs((targetNum / 5) - (i / 5)) <= 1) && (Mathf.Abs((targetNum % 5) - (i % 5)) <= 1) && (Mathf.Abs(targetNum - i) <= 6))
                {
                    targetCount++;
                    PlagueSingleTarget(unit, i);
                }
            }
        }
        CanvasController.Instance.battleSkillText.text = "uses Plague!";
        CanvasController.Instance.battleStatusText.text = targetCount + " enemies potentially suffering from Slow, Burn, Daze and having their cooldowns reset!";
    }

    private void PlagueSingleTarget(UnitData unit, int targetNum)
    {
        if (!playerTarget)
        {
            HurtCharacter(enemyArray[targetNum], true);
            enemyUnits[targetNum].Slow(4, unit);
            enemyUnits[targetNum].Daze(4, unit);
            enemyUnits[targetNum].Burn(4, unit);
            enemyUnits[targetNum].CDReset(4, unit);
        }
        else
        {

            HurtCharacter(playerArray[targetNum], true);
            playerUnits[targetNum].Slow(4, unit);
            playerUnits[targetNum].Daze(4, unit);
            playerUnits[targetNum].Burn(4, unit);
            playerUnits[targetNum].CDReset(4, unit);
        }
    }

    private void Dragonfire(UnitData unit, bool playerUnit)
    {
        int targetCount = 0;

        SoundController.Instance.PlaySingle(59);

        if (playerTarget)
        {
            SpecialEffect(51, GameController.Instance.GetPlayerMiddlePosition());
            for (int i = 0; i < 25; i++)
            {
                if (playerUnits[i] != null && !playerUnits[i].isDead())
                {
                    DragonfireSingleTarget(unit, i);
                    targetCount++;
                }
            }
        }
        else
        {
            SpecialEffect(51, GameController.Instance.GetEnemyMiddlePosition());
            for (int i = 0; i < 25; i++)
            {
                if (enemyUnits[i] != null && !enemyUnits[i].isDead())
                {
                    DragonfireSingleTarget(unit, i);
                    targetCount++;
                }
            }
        }
        CanvasController.Instance.battleSkillText.text = "uses Dragonfire!";
        CanvasController.Instance.battleBattleText.text = targetCount + " targets attacked for " + unit.GetIntelligence() * 2 + " potential damage";
        CanvasController.Instance.battleStatusText.text = " and potentially burning!";
    }

    private void DragonfireSingleTarget(UnitData unit, int targetNum)
    {
        int dmg = unit.GetIntelligence() * 2;
        dmg = ModifyDamage(unit, dmg);

        if (!playerTarget)
        {
            //Do damage
            if (enemyUnits[targetNum].TakeDamage(dmg, 4, unit))
            {
                //DeadCharacter(enemyUnits[targetNum], enemyArray[targetNum], false);
            }
            else
            {
                HurtCharacter(enemyArray[targetNum], true);
                enemyUnits[targetNum].Burn(4, unit);
            }
        }
        else
        {
            if (playerUnits[targetNum].TakeDamage(dmg, 4, unit))
            {
                //DeadCharacter(playerUnits[targetNum], playerArray[targetNum], true);
            }
            else
            {
                HurtCharacter(playerArray[targetNum], true);
                playerUnits[targetNum].Burn(4, unit);
            }
        }
    }

    /***********
    * Undead
    * *********/




    /***********
    * TARGET SELECTION
    * **********/

    private int PlayerMeleeTargetSelect(UnitData unit)
    {
        int enemyPosition = -1;
        int enemyPriority = 10;

        foreach (UnitData enemy in enemyUnitList)
        {
            int enemyPriorityNum = MeleeTargetSelect(unit, enemy);

            if (enemyPriorityNum <= enemyPriority && !enemy.isDead())
            {
                enemyPriority = enemyPriorityNum;
                enemyPosition = enemy.positionNum;
                //targetPositions.Add(enemy.positionNum);
                targetPositions[enemy.positionNum] = true;
            }
        }

        foreach (UnitData enemy in enemyUnitList)
        {
            if (!enemy.isDead())
            {
                int enemyPriorityNum = MeleeTargetSelect(unit, enemy);
                if (enemyPriorityNum > enemyPriority)
                {
                    //targetPositions.Remove(enemy.positionNum);
                    targetPositions[enemy.positionNum] = false;
                }
                else
                {
                    UnitFightScript h = enemyArray[enemy.positionNum].gameObject.GetComponent<UnitFightScript>();                    
                }
                
            }
        }

        return enemyPosition;

    }

    private int PlayerRangeMagicSneakTargetSelect(UnitData unit)
    {
        int enemyPosition = -1;
        int enemyPriority = 10;

        foreach (UnitData enemy in enemyUnitList)
        {
            int enemyPriorityNum = RangeMagicSneakTargetSelect(unit, enemy);

            if (enemyPriorityNum <= enemyPriority && !enemy.isDead())
            {
                enemyPriority = enemyPriorityNum;
                enemyPosition = enemy.positionNum;
                //targetPositions.Add(enemy.positionNum);
                targetPositions[enemy.positionNum] = true;
            }            
        }

        foreach (UnitData enemy in enemyUnitList)
        {
            if (!enemy.isDead())
            {
                int enemyPriorityNum = RangeMagicSneakTargetSelect(unit, enemy);
                if (enemyPriorityNum > enemyPriority)
                {
                    targetPositions[enemy.positionNum] = false;
                }else
                {
                    UnitFightScript h = enemyArray[enemy.positionNum].gameObject.GetComponent<UnitFightScript>();
                    
                }
            }            
        }

        return enemyPosition;
    }

    private int PlayerHealTargetSelect(UnitData unit, int healDmg)
    {
        int teammatePosition = -1;
        int healAmount = 0;
        int remainingHealth = 1000000;
        healDmg = (int)Mathf.Abs(healDmg);

        foreach (UnitData player in playerUnitList)
        {
            if (!player.isDead() && player.GetMissingHealth() >= healAmount)
            {
                if (player.GetCurrentHealth() < remainingHealth || (player.GetMissingHealth() >= healAmount && healAmount < healDmg))
                {
                    healAmount = player.GetMissingHealth();
                    if(healAmount > healDmg)
                    {
                        healAmount = healDmg;
                    }
                    remainingHealth = player.GetCurrentHealth();
                    teammatePosition = player.positionNum;
                }
                targetPositions[player.positionNum] = true;
            }
        }
        
        return teammatePosition;
    }

    private int EnemyMeleeTargetSelect(UnitData unit)
    {
        int enemyPosition = -1;
        int enemyPriority = 10;

        foreach (UnitData enemy in playerUnitList)
        {
            int enemyPriorityNum = MeleeTargetSelect(unit, enemy);

            if (enemyPriorityNum < enemyPriority && !enemy.isDead())
            {
                enemyPriority = enemyPriorityNum;
                enemyPosition = enemy.positionNum;
                //targetedUnit = enemy;
            }
        }

        return enemyPosition;
    }

    private int EnemyRangeMagicSneakTargetSelect(UnitData unit)
    {
        int enemyPosition = -1;
        int enemyPriority = 10;

        foreach (UnitData enemy in playerUnitList)
        {
            int enemyPriorityNum = RangeMagicSneakTargetSelect(unit, enemy);

            if (enemyPriorityNum < enemyPriority && !enemy.isDead())
            {
                enemyPriority = enemyPriorityNum;
                enemyPosition = enemy.positionNum;
                //targetedUnit = enemy;
            }
        }

        return enemyPosition;
    }

    private int EnemyHealTargetSelect(UnitData unit, int healDmg)
    {
        int teammatePosition = -1;
        int healAmount = 0;
        int remainingHealth = 100000;

        healDmg = (int)Mathf.Abs(healDmg);

        foreach (UnitData enemy in enemyUnitList)
        {
            if (!enemy.isDead() && enemy.GetMissingHealth() >= healAmount)
            {
                if (enemy.GetCurrentHealth() < remainingHealth)
                {
                    healAmount = enemy.GetMissingHealth();
                    if (healAmount > healDmg)
                    {
                        healAmount = healDmg;
                    }
                    remainingHealth = enemy.GetCurrentHealth();
                    teammatePosition = enemy.positionNum;
                }
            }
        }

        

        return teammatePosition;
    }

    private int MeleeTargetSelect(UnitData unit, UnitData enemy)
    {

        int enemyPriorityNum = -1;

        int modVal = unit.positionNum % 5;

        enemyPriorityNum = (int)Mathf.Abs(enemy.positionNum % 5 - modVal) + (enemy.positionNum / 5);

        return enemyPriorityNum;

    }

    private int RangeMagicSneakTargetSelect(UnitData unit, UnitData enemy)
    {
        
        int enemyPriorityNum = -1;

        int modVal = unit.positionNum % 5;

        enemyPriorityNum = (int)Mathf.Abs(enemy.positionNum % 5 - modVal) + (4 - (enemy.positionNum / 5));

        return enemyPriorityNum;
    }

    


    /*protected void MoveBack()
    {
        //Move(startingPosition, normalMoveTime);
        Move(normalMoveTime);
    }

    protected void SneakMoveBack()
    {
        //Move(startingPosition, sneakMoveTime);
        Move(sneakMoveTime);
    }*/

    private IEnumerator MoveBack(float delayTime, UnitData u)
    {
        yield return new WaitForSeconds(delayTime);
        Vector3 targetLoc = GameController.Instance.GetUnitPosition(u.OnPlayerTeam(), u.GetClassSize(), u.positionNum);
        Move(targetLoc, normalMoveTime, u);
    }

    private IEnumerator SneakMoveBack(float delayTime, UnitData u)
    {
        yield return new WaitForSeconds(delayTime);
        Vector3 targetLoc = GameController.Instance.GetUnitPosition(u.OnPlayerTeam(), u.GetClassSize(), u.positionNum);
        Move(targetLoc, sneakMoveTime, u);
    }

    private void SneakFaceBack()
    {
        if (playerUnitMoving)
        {
            if(playerArray[movingUnit.positionNum] != null)
            {
                FaceTarget(movingTargetPosition, playerArray[movingUnit.positionNum]);

            }
        }
        else
        {
            if (enemyArray[movingUnit.positionNum] != null)
            {
                FaceTarget(movingTargetPosition, enemyArray[movingUnit.positionNum]);
            }
        }
       
    }

    protected void MoveTowards(float moveTime)
    {
        Move(movingTargetPosition, moveTime);
    }

    protected void Move(Vector3 targetLoc, float moveTime, UnitData u)
    {
        //StopAllCoroutines();

        if (playerUnitMoving && playerArray[u.positionNum] != null)
        {
            Rigidbody rigidBody = playerArray[u.positionNum].GetComponent<Rigidbody>();
            StartCoroutine(SmoothMovementRoutine(targetLoc, rigidBody, moveTime));
        }
        else if (enemyArray[u.positionNum] != null)
        {
            Rigidbody rigidBody = enemyArray[u.positionNum].GetComponent<Rigidbody>();
            StartCoroutine(SmoothMovementRoutine(targetLoc, rigidBody, moveTime));
        }
    }

    protected void Move(Vector3 targetLoc, float moveTime)
    {
        //StopAllCoroutines();

        if (playerUnitMoving && playerArray[movingUnit.positionNum] != null)
        //if (((playerUnitMoving && !corruptedUnit) || (!playerUnitMoving && corruptedUnit)) && playerArray[movingUnit.positionNum] != null)
        {
            Rigidbody rigidBody = playerArray[movingUnit.positionNum].GetComponent<Rigidbody>();            
            StartCoroutine(SmoothMovementRoutine(targetLoc, rigidBody, moveTime));
        }
        else if(enemyArray[movingUnit.positionNum] != null)
        {
            Rigidbody rigidBody = enemyArray[movingUnit.positionNum].GetComponent<Rigidbody>();            
            StartCoroutine(SmoothMovementRoutine(targetLoc, rigidBody, moveTime));
        }        
    }

    protected IEnumerator SmoothMovementRoutine(Vector3 endPosition, Rigidbody rigidBody, float moveTime)
    {
        
        float remainingDistanceToEndPosition;
        do
        {
            if(rigidBody == null)
            {
                break;
            }
            remainingDistanceToEndPosition = (rigidBody.position - endPosition).sqrMagnitude;
            Vector3 updatedPosition = Vector3.MoveTowards(rigidBody.position, endPosition, moveTime * speedAdjust * Time.deltaTime);
            rigidBody.MovePosition(updatedPosition);
            yield return null;
        } while (remainingDistanceToEndPosition > 0.01f);

    }

    private void FaceTarget(Vector3 endPosition, GameObject movingObject)
    {
        if(movingObject != null)
        {
            movingObject.transform.LookAt(endPosition);

        }
    }

    private void DelayAttackAnim()
    {
        if (playerUnitMoving && playerArray[movingUnit.positionNum] != null)
        {
            playerArray[movingUnit.positionNum].GetComponent<Animator>().SetTrigger("attack");
        }
        else if (!playerUnitMoving && enemyArray[movingUnit.positionNum] != null)
        {
            enemyArray[movingUnit.positionNum].GetComponent<Animator>().SetTrigger("attack");
        }
    }

    public void DeadCharacter(UnitData deadUnit, GameObject g, bool playerUnit, UnitData attacker)
    {
        //movingUnit.GainXP(1, deadUnit);
        if (playerUnit)
        {
            /*foreach(UnitData u in enemyUnitList)
            {
                u.GainXP(0, deadUnit);
            }*/
            playerCount--;
            Destroy(playerHealthBars[deadUnit.positionNum]);
        }
        else
        {
            attacker.GainXP(1, deadUnit);
            foreach (UnitData u in playerUnitList)
            {
                u.GainXP(0, deadUnit);
            }
            if (attacker.OnPlayerTeam())
            {
                CanvasController.Instance.battleUnitText.text = attacker.unitName + " (" + attacker.GetCurrentLevel() + "/" + attacker.totalLevels + ")";
            }
            enemyCount--;
            Destroy(enemyHealthBars[deadUnit.positionNum]);
            
        }
        //Destroy(g);
        destroyedObjects.Add(g);
        if (GameController.Instance.autoBattle)
        {
            DelayDestroy();
        }
        else
        {
            Invoke("DeadCharacter", animationDelay);
            Invoke("DelayDestroy", destroyDelay);
        }
    }

    public void HurtCharacter(GameObject g, bool isMagic)
    {
        if (GameController.Instance.autoBattle)
        {
            return;
        }

        hurtObject = g;
        if (!isMagic) {
            Invoke("DelayHurt", animationDelay);
        }
        else
        {
            Invoke("DelayHurt", magicDelay);
        }
                
    }

    private void DelayHurt()
    {
        if(hurtObject != null)
        {
            hurtObject.GetComponent<Animator>().SetTrigger("isHit");
        }
        
    }

    private void DeadCharacter()
    {
        if (!GameController.Instance.autoBattle)
        {
            return;
        }

        if (destroyedObjects.Count > 0)
        {
            foreach(GameObject g in destroyedObjects)
            {
                g.GetComponent<Animator>().SetTrigger("isDead");
            }            
        }
    }

    private void DelayDestroy()
    {
        if (destroyedObjects.Count > 0)
        {
            for(int i = destroyedObjects.Count - 1; i >= 0; i--)
            {
                Destroy(destroyedObjects[i]);
            }
        }

        destroyedObjects.Clear();
    }

    public void ClearHealthBars()
    {
        for(int i = 0; i < 25; i++)
        {
            if(enemyHealthBars[i] != null)
            {
                Destroy(enemyHealthBars[i]);
            }
            if(playerHealthBars[i] != null)
            {
                Destroy(playerHealthBars[i]);
            }
        }
    }

    public void SpecialEffect(int effectNum, Vector3 targetLoc)
    {
       GameObject effect = (GameObject)Instantiate(GameController.Instance.projectiles[effectNum], targetLoc, Quaternion.identity);
       
    }

    public GameObject GetPlayerObject(int positionNum)
    {
        return playerArray[positionNum];
    }

    public GameObject GetEnemyObject(int positionNum)
    {
        return enemyArray[positionNum];
    }

    public void NewBattle()
    {
        fightOver = false;
    }
}
