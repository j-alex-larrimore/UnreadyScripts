using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public abstract class UnitData {

    public bool isMale;

    //str(melee dmg/melee defense) spd(turn speed) dex(accuracy/rng dmg) agi(evasion/snk dmg) con(health/mgc resist) wis(skill refresh/mgc success %) int(magic dmg/snk evasion)
    public int[] stats = new int[7];

    public string unitFirstName;
    public string unitLastName;
    private string[] maleNames = {"Bob", "Frank", "Tim", "Jacob", "Alexander", "Nathaniel", "Matthew", "Jordan", "Joshua", "Samuel", "Kenneth", "Allen", "Omar", "Hector", "Benjamin", "Chris", "William", "Thomas", "Moises",
    "Aaron", "Mark", "Sean", "Nick", "Muhammed", "Oliver", "Noah", "George", "Harry", "Leo", "Charlie", "Jack", "Freddie", "Alfie", "Oscar", "Arthur", "Henry", "Archie", "Theo", "Ethan", "Lucas", "Logan", "Atticus",
    "Milo", "Jasper", "Asher", "Silas", "Wyatt", "Declan", "Finn", "Rufio", "Axel", "Felix", "Bodhi", "Levi", "Ryker", "Sebastian", "Soren", "James", "Ezra", "Julian", "Caleb", "Ronan", "Zachary", "Cassius", "Xavier",
    "Luke", "Roman", "Isaac", "Gideon", "Graham", "Thanos", "Xander", "Rhett", "Everett", "Otto", "Jayden", "Atlas", "Harrison", "Sawyer", "Maverick", "Josiah", "Beckett", "Knox", "Lucian", "David", "Rowan", "Miles",
    "Griffin", "Kane"};
    private string[] femaleNames = {"Alice", "Rebecca", "Joanna", "Samantha", "Lauren", "Maria", "Jesenia", "Leslie", "Barbara", "Kiera", "Johanna", "Kate", "Elaine",  "Rachel", "Helana", "Annie", "Brittany", "Brandi",
        "Kylie", "Sarah", "Shannon", "Alexa", "Catie", "Olivia", "Sophia", "Amelia", "Lily", "Ava", "Isla", "Aria", "Mia", "Isabelle", "Ella", "Charlotte", "Grace", "Evie", "Maya", "Harper",
    "Layla", "Freya", "Amara", "Cora", "Aurora", "Rose", "Genevieve", "Maeve", "Penelope", "Iris", "Violet", "Ophelia", "Eleanor", "Esme", "Luna", "Danielle", "Imogen", "Eloise", "Aurelia", "Hazel", "Jane", "Anna", "Nora",
    "Evangeline", "Eliza", "Emily", "Sadie", "Phoebe", "Daphne", "Lydia", "Seraphina", "Clementine", "Grace", "Naomi", "Scarlett", "Zara", "Zoe", "Sienna", "Matilda", "Audrey", "Willow", "Chloe", "Gemma", "Margaret", "Lyra"};

    public int charType; //1 = human, 2 = animal, 3 = myth, 4 = undead
    public int xp = 0;
    public int totalLevels = 0;

    public int platoonNum = -1;
    public int positionNum = -1;

    public int charClass = 0;
    
    public int currentHealth;
    private int speedCount = 0;

    private int skill2CD = 0;
    private int skill3CD = 0;

    private GameObject guardObject;
    private UnitData guardUnit;
    private UnitData guardedUnit;

    private bool isSlow = false;
    private bool isGuard = false;
    private bool isGuarded = false;
    private bool isBurning = false;
    //private bool isConfused = false;
    private bool isBlind = false;
    private bool isBleeding = false;
    private bool defDown = false;
    private bool offDown = false;

    private bool hasted = false;
    private bool defUp = false;
    private bool offUp = false;

    public bool playerTeam = true;

    public int classSize = 1;

    public int specialCharNum = 0;

    public void SetUnitData(int character)
    {
        charType = character;
        SetGender();
        SetName();
        SetDefaults();
    }

    public void SetUnitData(int character, bool gender)
    {
        charType = character;
        isMale = gender;
        SetName();
        SetDefaults();
    }

    public int GetClassSize()
    {
        return classSize;
    }

    public bool OnPlayerTeam()
    {
        return playerTeam;
    }

    public void SetPlatoonPosition(int plat, int pos)
    {
        platoonNum = plat;
        positionNum = pos;
    }

    private void SetDefaults()
    {
        stats[0] = 1;
        stats[1] = 1;
        stats[2] = 1;
        stats[3] = 1;
        stats[4] = 1;
        stats[5] = 1;
        stats[6] = 1;
        
        SetName();
    }

    public void MakeHero()
    {
        specialCharNum = 1;
    }

    public bool IsHero()
    {
        return (specialCharNum == 1);
    }

    private void SetGender()
    {
        int randGender = UnityEngine.Random.Range(0, 2);

        if(randGender == 0)
        {
            isMale = true;
        }
        else
        {
            isMale = false;
        }
    }

    public bool IsMale()
    {
        return isMale;
    }

    public int GetCharType()
    {
        return charType;
    }

    public int GetCharClass()
    {
        return charClass;
    }

    public int GetSpeed()
    {
        if (hasted)
        {
            return stats[1] * 2;
        }
        return stats[1];
    }

    public int GetStrength(bool classS)
    {
        if (classS)
        {
            return stats[0] * classSize;
        }
        return stats[0];
    }

    public int GetStrength()
    {
        return stats[0] * classSize;
    }

    public int GetWisdom(bool classS)
    {
        if (classS)
        {
            return stats[5] * classSize;
        }
        return stats[5];
    }

    public int GetWisdom()
    {
        return stats[5] * classSize;
    }

    public int GetAgility(bool classS)
    {
        if (classS)
        {
            return stats[3] * classSize;
        }
        return stats[3];
    }

    public int GetAgility()
    {
        return stats[3] * classSize;
    }

    public int GetConstitution(bool classS)
    {
        if (classS)
        {
            return stats[4] * classSize;
        }
        return stats[4];
    }

    public int GetConstitution()
    {
        return stats[4] * classSize;
    }

    public int GetIntelligence(bool classS)
    {
        if (classS)
        {
            return stats[6] * classSize;
        }
        return stats[6];
    }

    public int GetIntelligence()
    {
        return stats[6] * classSize;
    }

    public int GetDexterity(bool classS)
    {
        if (classS)
        {
            return stats[2] * classSize;
        }
        return stats[2];
    }

    public int GetDexterity()
    {
        return stats[2] * classSize;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMissingHealth()
    {
        return GetMaxHealth() - currentHealth;
    }

    public int GetDefense()
    {
        if (defUp && !defDown)
        {
            return GetStrength(true)/2;
        }else if(defDown && !defUp)
        {
            return (int)(GetStrength(true) / 8);
        }

        return (int)(GetStrength(true) / 4);
    }

    public int GetMagicDefense()
    {
        if (defUp && !defDown)
        {
            return GetIntelligence(true)/2;
        }
        else if (defDown && !defUp)
        {
            return (int)(GetIntelligence(true) / 8);
        }

        return (int)(GetIntelligence(true)/4);
    }

    public int GetSpeedCount()
    {
        if (!isDead())
        {
            return speedCount;
        }
        else
        {
            return -1;
        }
       
    }

    public void SetSpeedCount(int spd)
    {
        if (!isDead())
        {
            if (isSlow && !hasted)
            {
                if (UnityEngine.Random.Range(0, 2) > 0)
                {
                    speedCount = spd;
                }
            }
            else
            {
                speedCount = spd;
            }

            UpdateSpeedBar();
        }
        else
        {
            speedCount = 0;
        }        
    }

    

    public void TakeTurn()
    {
        speedCount = 0;
        UpdateSpeedBar();
        if (isBleeding)
        {
            currentHealth -= 2*GetConstitution(true);
        }

        defDown = false;
        if (isBurning)
        {
            currentHealth -= GetConstitution(true);
            defDown = true;
        }
        AdjustHealthBar();
        if(currentHealth < 1)
        {
            currentHealth = 1;
        }
        isSlow = false;
        hasted = false;
        defUp = false;
        isBurning = false;
        isBleeding = false;
        if (isGuarded)
        {
            UnGuarded();
        }
        isGuard = false;
    }

    /*public void Cleanse()
    {
        isSlow = false;
        defDown = false;
        offDown = false;
        isBleeding = false;
        isBurning = false;
        isBlind = false;
    }*/

    public bool IsBlinded()
    {
        return isBlind;
    }

    public void RemoveBuffs()
    {
        hasted = false;
        defUp = false;
        //isGuard = false;
        isGuarded = false;
    }

    public bool IsGuardUnit()
    {
        return isGuard;
    }
    
    public void GuardUnit(UnitData u, GameObject guardOb)
    {
        if(this != u)
        {
            isGuard = true;
            guardedUnit = u;
            u.Guarded(guardOb, this);
        }
    }

    public void Guarded(GameObject guard, UnitData gUnit)
    {
        isGuarded = true;
        guardObject = guard;
        guardUnit = gUnit;
    }

    public void UnGuarded()
    {
        isGuarded = false;
        if(guardUnit != null)
        {
            guardUnit.isGuard = false;
        }
    }

    public void SetSkill2CD(int cd)
    {
        skill2CD = cd;
    }

    public void SetSkill3CD(int cd)
    {
        skill3CD = cd;
    }

    public int GetSkill2CD()
    {
        return skill2CD;
    }

    public int GetSkill3CD()
    {
        return skill3CD;
    }

    public void SetName()
    {
        int nameNum;
        if (isMale)
        {
            nameNum = UnityEngine.Random.Range(0, maleNames.Length);
            unitName = maleNames[nameNum];
        }
        else
        {
            nameNum = UnityEngine.Random.Range(0, femaleNames.Length);
            unitName = femaleNames[nameNum];
        }
    }

    public void SetName(string newName)
    {
        unitName = newName;
    }

    public string CreateJSON()
    {
        return JsonUtility.ToJson(this);
    }   

    public void ArmyStart()
    {
        if (!GameController.Instance.gameLoadedMidLevel)
        {
            currentHealth = 10 * GetConstitution(true);
            skill2CD = 0;
            skill3CD = 0;
            speedCount = 0;
        }       
    }

    public int GetMaxHealth()
    {
        return 10 * GetConstitution(true);
    }

    public void IncreaseCurrentHealthOnLevelUp(int healthGain)
    {
        currentHealth += healthGain * classSize;
    }

    public float GetHealthPercent()
    {
        float hp = (float)GetCurrentHealth() / GetMaxHealth() * 100;
        return hp;
    }

    public bool TakeDamage(int dmgTaken, int dmgType, UnitData attacker)
    {
        string dmgString = " hit for ";
        bool dodged = false;
        int dodgedDmg = 0;

        if (isDead())
        {
            return true;
        }
        
        //type 1 = physical, 2 = sneak, 3 = range, 4 = magicDmg, 5 = trap, 6 = heal
        if (dmgType == 1)
        {
            //if (isGuarded && !isGuard && guardUnit.IsGuardUnit())
            if (isGuarded && guardUnit != null && !guardUnit.isDead())
            {
                guardUnit.TakeDamage(dmgTaken, dmgType, attacker);
                BattleController.Instance.HurtCharacter(guardObject, false);
                dmgTaken = 0;
            }
            else
            {
                dmgTaken -= GetDefense();
                int ignoreChance = 0;
                if (attacker.IsBlinded())
                {
                    ignoreChance = 50;
                }

                if (dmgTaken < 0 || UnityEngine.Random.Range(0, 100) < ignoreChance)
                {
                    dodged = true;
                    dodgedDmg = dmgTaken;
                    dmgTaken = 0;
                }
            }

            
        }else if(dmgType == 2)
        {
            if (isGuarded && guardUnit != null && !guardUnit.isDead())
            {
                guardUnit.TakeDamage(dmgTaken, dmgType, attacker);
                BattleController.Instance.HurtCharacter(guardObject, false);
                dmgTaken = 0;
            }
            else
            {
                int ignoreChance = GetWisdom(false) - attacker.GetAgility(false);
                if (attacker.IsBlinded())
                {
                    Debug.Log("Sneak attacker is blind!");
                    ignoreChance += 50;
                }
                if (ignoreChance > 75)
                {
                    ignoreChance = 75;
                }
                if (UnityEngine.Random.Range(0, 100) < ignoreChance)
                {
                    dodged = true;
                    dodgedDmg = dmgTaken;
                    dmgTaken = 0;
                }
            }
        }
        else if (dmgType == 3)
        {
            if (isGuarded && guardUnit != null && !guardUnit.isDead())
            {
                guardUnit.TakeDamage(dmgTaken, dmgType, attacker);
                BattleController.Instance.HurtCharacter(guardObject, false);
                dmgTaken = 0;
            }
            else
            {
                int ignoreChance = GetDexterity(false) - attacker.GetDexterity(false);


                //int ignoreChance = 0;
                //int randNum = UnityEngine.Random.Range(0, 100);
                

                
                //if (randNum > (70f + (float)(GetDexterity(false) * .06) - ignoreChance))
                if (attacker.IsBlinded())
                {
                    ignoreChance += 50;
                    Debug.Log("Range attacker is blind!");
                }
                if (ignoreChance > 75)
                {
                    ignoreChance = 75;
                }

                if (UnityEngine.Random.Range(0, 100) < ignoreChance)
                {
                    dodged = true;
                    dodgedDmg = dmgTaken;
                    dmgTaken = 0;
                }
            }
        }
        else if (dmgType == 4)
        {
            if (isGuarded && guardUnit != null && !guardUnit.isDead())
            {
                guardUnit.TakeDamage(dmgTaken, dmgType, attacker);
                BattleController.Instance.HurtCharacter(guardObject, false);
                dmgTaken = 0;
            }
            else
            {
                dmgTaken -= GetMagicDefense();
                if (dmgTaken < 0)
                {
                    dmgTaken = 0;
                }
            }
        }
        else if (dmgType == 5)
        {
            if (isGuarded && guardUnit != null && !guardUnit.isDead())
            {
                guardUnit.TakeDamage(dmgTaken, dmgType, attacker);
                BattleController.Instance.HurtCharacter(guardObject, false);
                dmgTaken = 0;
            }
            else
            {
                int ignoreChance = GetDexterity(false) - attacker.GetDexterity(false);
                if (attacker.IsBlinded())
                {
                    ignoreChance += 50;
                    Debug.Log("Trap attacker is blind!");
                }
                if (ignoreChance > 75)
                {
                    ignoreChance = 75;
                }
                if (UnityEngine.Random.Range(0, 100) < ignoreChance)
                {
                    dodged = true;
                    dodgedDmg = dmgTaken;
                    dmgTaken = 0;
                }
            }
        }
        else if (dmgType == 6)
        {
            dmgString = " healed ";
        }
        else
        {
            Debug.Log("Dmg type error");
        }
        

        currentHealth -= dmgTaken;
        if (currentHealth > 10 * GetConstitution(true))
        {
            currentHealth = 10 * GetConstitution(true);
        }

        if (!dodged || (dodgedDmg <= 0))
        {
            CanvasController.Instance.battleBattleText.text = unitName + dmgString + dmgTaken + " damage! " + currentHealth + " health left";
        }
        else
        {
            CanvasController.Instance.battleBattleText.text = unitName + " dodged " + dodgedDmg + " damage!";
        }

        AdjustHealthBar();

        if (currentHealth <= 0)
        {
            if (isGuard)
            {
                Debug.Log("Guard dead!");                
            }

            if (playerTeam)
            {
                BattleController.Instance.DeadCharacter(this, BattleController.Instance.GetPlayerObject(positionNum), playerTeam, attacker);
                xp = 0;
                if (specialCharNum == 1)
                {
                    LevelController.Instance.heroDead = true;
                }
            }
            else
            {
                BattleController.Instance.DeadCharacter(this, BattleController.Instance.GetEnemyObject(positionNum), playerTeam, attacker);
            }

            return true;
        }
        else
        {
            return false;
        }
    }

    public void AdjustHealthBar()
    {
        if(positionNum == -1)
        {
            return;
        }

        if (playerTeam && BattleController.Instance.playerHealthBars[positionNum] != null)
        {
            GameObject g = BattleController.Instance.playerHealthBars[positionNum].gameObject;
            Transform t = g.transform.Find("Health").gameObject.transform;
            RectTransform bar = (RectTransform)t.Find("HealthBar");
            bar.sizeDelta = new Vector2(GetHealthPercent(), bar.sizeDelta.y);
        }
        else if(!playerTeam && BattleController.Instance.enemyHealthBars[positionNum] != null)
        {
            GameObject g = BattleController.Instance.enemyHealthBars[positionNum].gameObject;
            Transform t = g.transform.Find("Health").gameObject.transform;
            RectTransform bar = (RectTransform)t.Find("HealthBar");
            bar.sizeDelta = new Vector2(GetHealthPercent(), bar.sizeDelta.y);
        }
    }

    public void AdjustXPBar()
    {
        float xpPercent = xp;
        GameObject g = BattleController.Instance.playerHealthBars[positionNum].gameObject;
        Transform t = g.transform.Find("Experience").gameObject.transform;
        RectTransform bar = (RectTransform)t.Find("ExperienceBar");
        bar.sizeDelta = new Vector2(xpPercent, bar.sizeDelta.y);
    }

    private void MaxLevel()
    {
        CanvasController.Instance.PauseBattle(true);

        GameObject g = BattleController.Instance.playerHealthBars[positionNum].gameObject;
        if(g != null)
        {
            GameObject maxImage = g.transform.Find("MaxLevelImage").gameObject;
            maxImage.SetActive(true);
        }        
    }

    private void NotMaxLevel()
    {
        GameObject g = BattleController.Instance.playerHealthBars[positionNum].gameObject;
        if(g != null)
        {
            GameObject maxImage = g.transform.Find("MaxLevelImage").gameObject;
            maxImage.SetActive(false);
        }
    }

    public void UpdateSpeedBar()
    {
        if (playerTeam && BattleController.Instance.playerHealthBars[positionNum] != null)
        {
            GameObject g = BattleController.Instance.playerHealthBars[positionNum].gameObject;
            Transform t = g.transform.Find("Speed").gameObject.transform;
            RectTransform bar = (RectTransform)t.Find("SpeedBar");

            if (speedCount <= BattleController.Instance.GetTotalSpeedTurnLimit())
            {
                bar.sizeDelta = new Vector2(speedCount / LevelController.Instance.levelNum, bar.sizeDelta.y);
            }
            else
            {
                bar.sizeDelta = new Vector2(100, bar.sizeDelta.y);
            }
        }
        else if (!playerTeam && BattleController.Instance.enemyHealthBars[positionNum] != null)
        {
            GameObject g = BattleController.Instance.enemyHealthBars[positionNum].gameObject;
            Transform t = g.transform.Find("Speed").gameObject.transform;
            RectTransform bar = (RectTransform)t.Find("SpeedBar");
            if (speedCount <= BattleController.Instance.GetTotalSpeedTurnLimit())
            {
                bar.sizeDelta = new Vector2(speedCount / LevelController.Instance.levelNum, bar.sizeDelta.y);
            }
            else
            {
                bar.sizeDelta = new Vector2(100, bar.sizeDelta.y);

            }
        }
    }

    public bool isDead()
    {
        if (currentHealth <= 0)
        {
            
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool ResistanceCheck(int dmgType, UnitData unit)
    {
        //type 1 = physical, 2 = sneak, 3 = range, 4 = magicDmg, 5 = trap
        if (dmgType == 1)
        {
            int ignoreChance = GetAgility(false) - unit.GetAgility(false);
            if (unit.IsBlinded())
            {
                ignoreChance += 50;
            }
            if(ignoreChance > 75)
            {
                ignoreChance = 75;
            }
            if (UnityEngine.Random.Range(0, 100) < ignoreChance){
                return true;
            }
        }
        else if (dmgType == 2)
        {            
            int ignoreChance = GetWisdom(false) - unit.GetAgility(false);
            if (unit.IsBlinded())
            {
                ignoreChance += 50;
            }
            if (ignoreChance > 75)
            {
                ignoreChance = 75;
            }
            if (UnityEngine.Random.Range(0, 100) < ignoreChance)
            {
                return true;
            }
        }
        else if (dmgType == 3)
        {
            int ignoreChance = GetAgility(false) - unit.GetDexterity(false);
            if (unit.IsBlinded())
            {
                ignoreChance += 50;
            }
            if (ignoreChance > 75)
            {
                ignoreChance = 75;
            }
            if (UnityEngine.Random.Range(0, 100) < ignoreChance)
            {
                return true;
            }
        }
        else if (dmgType == 4)
        {
            int ignoreChance = GetConstitution(false) - unit.GetWisdom(false);
            if (ignoreChance > 75)
            {
                ignoreChance = 75;
            }
            if (UnityEngine.Random.Range(0, 100) < ignoreChance)
            {
                return true;
            }
        }
        else if (dmgType == 5)
        {
            int ignoreChance = GetDexterity(false) - unit.GetDexterity(false);
            if (unit.IsBlinded())
            {
                ignoreChance += 50;
            }
            if (ignoreChance > 75)
            {
                ignoreChance = 75;
            }
            if (UnityEngine.Random.Range(0, 100) < ignoreChance)
            {
                return true;
            }
        }
        return false;
    }

    public bool Daze(int dmgType, UnitData unit)
    {
        if(!ResistanceCheck(dmgType, unit))
        {
            speedCount = 0;
            UpdateSpeedBar();
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CDReset(int dmgType, UnitData unit)
    {
        if (!ResistanceCheck(dmgType, unit))
        {
            skill2CD = 2;
            skill3CD = 4;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CDDelay(int dmgType, UnitData unit)
    {
        if (!ResistanceCheck(dmgType, unit))
        {
            skill2CD++;
            skill3CD++;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool Slow(int dmgType, UnitData unit)
    {
        if (!ResistanceCheck(dmgType, unit))
        {
            isSlow = true;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Haste()
    {
        hasted = true;
    }

    public void NormalSpeed()
    {
        hasted = false;
        isSlow = false;
    }

    public bool Bleed(int dmgType, UnitData unit)
    {
        if (!ResistanceCheck(dmgType, unit))
        {
            isBleeding = true;
            return true;
        }
        else
        {
            return false;
        }
    }

    /*public void NotBleed()
    {
        isBleeding = false;
    }*/

                public bool Burn(int dmgType, UnitData unit)
    {
        if (!ResistanceCheck(dmgType, unit))
        {
            isBurning = true;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void NotBurn()
    {
        isBurning = false;
    }

    public bool Blind(int dmgType, UnitData unit)
    {
        if (!ResistanceCheck(dmgType, unit))
        {
            isBlind = true;
            return true;
        }
        else
        {
            return false;
        }
    }

    /*public void NotBlind()
    {
        isBlind = false;
    }*/

    public bool DefDown(int dmgType, UnitData unit)
    {
        if (!ResistanceCheck(dmgType, unit))
        {
            defDown = true;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void DefNormal()
    {
        defDown = false;
        defUp = false;
    }

    public void DefUp()
    {
        defUp = true;
    }

    public bool OffDown(int dmgType, UnitData unit)
    {
        if (!ResistanceCheck(dmgType, unit))
        {
            offDown = true;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void OffNormal()
    {
        offDown = false;
        offUp = false;
        isBlind = false;
    }

    public void OffUp()
    {
        offUp = true;
    }

    public bool IsOffUp()
    {
        return offUp;
    }

    public bool IsOffDown()
    {
        return offDown;
    }

    public void GainXP(int xpType, UnitData u)
    {
        //xpType 0 = troop in opposing unit defeated
        //xpType 1 = killed unit
        //xpType 2 = ability used
        int levelDif = totalLevels - u.totalLevels;
        int size = u.GetClassSize();

        if ((charType == 1 && GetCurrentLevel() >= 10) || GetCurrentLevel() >= 30 || !playerTeam)
        {
            return;
        }

        if (!isDead())
        {
            if (xpType == 2)
            {
                xp++;
            }

            if (xpType == 4 && platoonNum != -1 && platoonNum <= LevelController.Instance.maxDeployedPlatoon)
            {
                xp += 20;
            }

            if (levelDif < 10)
            {
                if (xpType == 0)
                {
                    xp += (10 - levelDif) * 2 * size;
                }

                if (xpType == 1)
                {
                    int xpChange = (10 - levelDif) * 10 * size;

                    if (xpChange > 0)
                    {
                        xp += ((10 - levelDif) * 10) * size;
                    }
                }                
            }
            else
            {
                if (xpType == 0)
                {
                    xp += (int)((2 * size) / (levelDif - 9));
                }

                if (xpType == 1)
                {
                    xp += (int)((10 * size) / (levelDif - 9));
                }
            }      
            

            while (xp >= 100 && (charType != 1 || GetCurrentLevel() < 10) && GetCurrentLevel() < 30 )
            {
                LevelUp();
                xp -= 100;
                if(xpType != 4)
                {
                    SoundController.Instance.LevelUp();
                    AdjustHealthBar();
                }

                //AdjustXPBar();
            }

            if (xpType == 4)
            {
                return;
            }

                if ((charType == 1 && GetCurrentLevel() >= 10) || GetCurrentLevel() >= 30)
            {
                xp = 0;
                MaxLevel();
            }
            else
            {
                NotMaxLevel();
            }

                if (positionNum != -1 && BattleController.Instance.playerHealthBars[positionNum] != null)
            {
                /*float xpPercent = xp;
                GameObject g = BattleController.Instance.playerHealthBars[positionNum].gameObject;
                Transform t = g.transform.Find("Experience").gameObject.transform;
                RectTransform bar = (RectTransform)t.Find("ExperienceBar");
                bar.sizeDelta = new Vector2(xpPercent, bar.sizeDelta.y);
                */
                AdjustXPBar();

            }
            
        }else if (isDead())
        {
            //Debug.Log("Dead no Xp!");
        }
    }

    /*public void GainXP(UnitData u)
    {
        int size = u.GetClassSize();

        if (!isDead())
        {
            if (u.GetCurrentLevel() <= GetCurrentLevel())
            {
                xp += 5 * size;
            }
            else
            {
                xp += (5 * (u.GetCurrentLevel() - GetCurrentLevel())) * size;
            }
        }
    }*/

    public abstract void LevelUp();
    public abstract int GetCurrentLevel();
    public abstract void SetClass(int classNum, int platoonNum);
    public abstract void SetEnemyClass(int classNum);
    public abstract string GetCharClassText();
    public abstract void UpdateClassUnlocks();
    public abstract bool[] GetUnlockedClasses();
    public abstract int GetLevel(int classNum);
}
