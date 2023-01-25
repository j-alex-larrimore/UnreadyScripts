using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanData : UnitData
{
    /*public ClassData recruit = new ClassData();   1

    public ClassData mage = new ClassData();        3
    public ClassData rogue = new ClassData();
    public ClassData fighter = new ClassData();

    public ClassData magician = new ClassData();       7
    public ClassData healer = new ClassData();
    public ClassData scout = new ClassData();
    public ClassData bandit = new ClassData();
    public ClassData brawler = new ClassData();
    public ClassData soldier = new ClassData();
    public ClassData warrior = new ClassData();

    public ClassData barbarian = new ClassData();       15
    public ClassData knight = new ClassData();
    public ClassData duelist = new ClassData();
    public ClassData mercenary = new ClassData();
    public ClassData hunter = new ClassData();
    public ClassData trapper = new ClassData();
    public ClassData thief = new ClassData();
    public ClassData dualWielder = new ClassData();
    public ClassData wizard = new ClassData();
    public ClassData blackMage = new ClassData();
    public ClassData cleric = new ClassData();
    public ClassData elementalist = new ClassData();
    public ClassData sage = new ClassData();
    public ClassData deserter = new ClassData();
    public ClassData monk = new ClassData();

    public ClassData illusionist = new ClassData();     19
    public ClassData warlock = new ClassData();
    public ClassData geomancer = new ClassData();
    public ClassData hydromancer = new ClassData();
    public ClassData pyromancer = new ClassData();
    public ClassData aeromancer = new ClassData();
    public ClassData priest = new ClassData();
    public ClassData shaman = new ClassData();
    public ClassData druid = new ClassData();
    public ClassData ranger = new ClassData();
    public ClassData pirate = new ClassData();
    public ClassData assassin = new ClassData();
    public ClassData berserker = new ClassData();
    public ClassData swordmaster = new ClassData();
    public ClassData gladiator = new ClassData();
    public ClassData paladin = new ClassData();
    public ClassData darkKnight = new ClassData();
    public ClassData beastMaster = new ClassData();
    public ClassData outlaw = new ClassData();

    public ClassData sorcerer = new ClassData();        14
    public ClassData saint = new ClassData();
    public ClassData sniper = new ClassData();
    public ClassData ninja = new ClassData();
    public ClassData templar = new ClassData();
    public ClassData warlord = new ClassData();
    public ClassData dragonKnight = new ClassData();
    public ClassData deathKnight = new ClassData();
    public ClassData gambler = new ClassData();
    public ClassData spy = new ClassData();
    public ClassData nightblade = new ClassData();
    public ClassData mythMaster = new ClassData();
    public ClassData summoner = new ClassData();
    public ClassData necromancer = new ClassData();*/

    public string jsonString = "";

    public int[] classData = new int[59];
    private bool[] unlockedClasses = new bool[59];

    public HumanData()
    {
        SetUnitData(1);
    }

    public HumanData(bool gender, bool team)
    {
        isMale = gender;
        playerTeam = team;
        SetUnitData(1, isMale);
    }

    public HumanData(bool team)
    {
        playerTeam = team;
        SetUnitData(1);
    }

    public override void SetEnemyClass(int classNum)
    {
        charClass = classNum;
    }

    public override void SetClass(int classNum, int platoonNum)
    {
        if(classNum >= 0 && classNum <= classData.Length)
        {
            charClass = classNum;
        }
        else
        {
            Debug.Log("Set Class error");
        }
    }

    public override int GetCurrentLevel()
    {

        return classData[charClass];
    }

    public override int GetLevel(int classNum)
    {
        return classData[classNum];
    }

    

    public override void LevelUp()
    {
        SpeedUp();
        DexUp();
        ConUp();
        StrUp();
        AgiUp();
        IntUp();
        WisUp();
        totalLevels++;
        classData[charClass]++;
    }

    public void SpeedUp()
    {
        switch (charClass)
        {
            case 37:
                stats[1] += 4;
                break;
            case 48:
                stats[1] += 3;
                break;
            case 6:
            case 13:
            case 17:
            case 18:
            case 24:
            case 51:
            case 54:
            case 55:
                stats[1] += 2;
                break;
            default:
                stats[1] += 1;
                break;
        }
    }

    public void StrUp()
    {
        switch (charClass)
        {
            case 3:
            case 7:
            case 9:
            case 10:
            case 12:
            case 13:
            case 18:
            case 32:
            case 48:
                stats[0] += 2;
                break;
            case 8:
            case 14:
            case 24:
            case 25:
            case 44:
            case 46:
            case 51:
                stats[0] += 3;
                break;
            case 39:
            case 42:
            case 55:
                stats[0] += 4;
                break;
            case 11:
            case 40:
            case 52:
                stats[0] += 5;
                break;
            case 38:
                stats[0] += 7;
                break;
            case 50:
                stats[0] += 9;
                break;
            default:
                stats[0] += 1;
                break;
        }
    }

    public void ConUp()
    {
        int healthGain = 0;
        switch (charClass)
        {
            case 3:
            case 5:
            case 10:
            case 14:
            case 16:
            case 22:
            case 25:
            case 31:
            case 33:
            case 39:
            case 44:
                stats[4] += 2;
                healthGain = 20;
                break;
            case 9:
            case 21:
            case 32:
            case 34:
            case 40:
            case 42:
            case 56:
                stats[4] += 3;
                healthGain = 30;
                break;
            case 12:
            case 43:
            case 51:
            case 52:
                stats[4] += 4;
                healthGain = 40;
                break;
            case 41:
                stats[4] += 8;
                healthGain = 80;
                break;
            case 49:
                stats[4] += 9;
                healthGain = 90;
                break;
            default:
                stats[4] += 1;
                healthGain = 10;
                break;
        }
        IncreaseCurrentHealthOnLevelUp(healthGain);
    }

    public void WisUp()
    {
        switch (charClass)
        {
            case 1:
            case 4:
            case 5:
            case 21:
            case 22:
            case 29:
            case 34:
                stats[5] += 2;
                break;
            case 20:
            case 30:
            case 42:
            case 44:
            case 52:
            case 55:
                stats[5] += 3;
                break;
            case 28:
            case 33:
                stats[5] += 4;
                break;
            case 23:
            case 31:
                stats[5] += 5;
                break;
            case 26:
                stats[5] += 7;
                break;
            case 58:
                stats[5] += 8;
                break;
            default:
                stats[5] += 1;
                break;
        }
    }

    public void IntUp()
    {
        switch (charClass)
        {
            case 1:
            case 5:
            case 21:
            case 31:
            case 58:
                stats[6] += 2;
                break;
            case 4:
            case 20:
            case 22:
            case 33:
            case 46:
                stats[6] += 3;
                break;
            case 19:
            case 30:
            case 34:
                stats[6] += 5;
                break;
            case 29:
                stats[6] += 6;
                break;
            case 27:
                stats[6] += 7;
                break;
            case 45:
                stats[6] += 9;
                break;
            default:
                stats[6] += 1;
                break;
        }
    }

    public void AgiUp()
    {
        switch (charClass)
        {
            case 8:
            case 10:
            case 13:
            case 14:
            case 18:
            case 24:
                stats[3] += 2;
                break;
            case 2:
            case 7:
            case 17:
            case 25:
            case 39:
            case 44:
                stats[3] += 3;
                break;
            case 48:
            case 55:
                stats[3] += 4;
                break;
            case 36:
                stats[3] += 5;
                break;
            case 54:
                stats[3] += 7;
                break;
            default:
                stats[3] += 1;
                break;
        }
    }

    public void DexUp()
    {
        switch (charClass)
        {
            case 6:
                stats[2] += 2;
                break;
            case 36:
                stats[2] += 3;
                break;
            case 16:
                stats[2] += 4;
                break;
            case 15:
            case 43:
                stats[2] += 5;
                break;
            case 35:
                stats[2] += 7;
                break;
            case 56:
                stats[2] += 8;
                break;
            case 47:
                stats[2] += 9;
                break;
            default:
                stats[2] += 1;
                break;
        }
    }


    public void Undead(int deathType)
    {

    }

    public override string GetCharClassText()
    {
        string className = "";


        switch (charClass)
        {
            case 0:
                className = "Recruit";
                break;
            case 1:
                className = "Mage";
                break;
            case 2:
                className = "Rogue";
                break;
            case 3:
                className = "Fighter";
                break;
            case 4:
                className = "Magician";
                break;
            case 5:
                className = "Healer";
                break;
            case 6:
                className = "Scout";
                break;
            case 7:
                className = "Bandit";
                break;
            case 8:
                className = "Brawler";
                break;
            case 9:
                className = "Soldier";
                break;
            case 10:
                className = "Warrior";
                break;
            case 11:
                className = "Barbarian";
                break;
            case 12:
                className = "Knight";
                break;
            case 13:
                className = "Duelist";
                break;
            case 14:
                className = "Mercenary";
                break;
            case 15:
                className = "Hunter";
                break;
            case 16:
                className = "Trapper";
                break;
            case 17:
                className = "Thief";
                break;
            case 18:
                className = "Dual Wielder";
                break;
            case 19:
                className = "Wizard";
                break;
            case 20:
                className = "Black Mage";
                break;
            case 21:
                className = "Cleric";
                break;
            case 22:
                className = "Elementalist";
                break;
            case 23:
                className = "Sage";
                break;
            case 24:
                className = "Monk";
                break;
            case 25:
                className = "Deserter";
                break;
            case 26:
                className = "Illusionist";
                break;
            case 27:
                className = "Warlock";
                break;
            case 28:
                className = "Geomancer";
                break;
            case 29:
                className = "Pyromancer";
                break;
            case 30:
                className = "Hydromancer";
                break;
            case 31:
                className = "Aeromancer";
                break;
            case 32:
                className = "Priest";
                break;
            case 33:
                className = "Shaman";
                break;
            case 34:
                className = "Druid";
                break;
            case 35:
                className = "Ranger";
                break;
            case 36:
                className = "Pirate";
                break;
            case 37:
                className = "Assassin";
                break;
            case 38:
                className = "Berserker";
                break;
            case 39:
                className = "Swordmaster";
                break;
            case 40:
                className = "Gladiator";
                break;
            case 41:
                className = "Paladin";
                break;
            case 42:
                className = "Dark Knight";
                break;
            case 43:
                className = "Beast Master";
                break;
            case 44:
                className = "Outlaw";
                break;
            case 45:
                className = "Sorcerer";
                break;
            case 46:
                className = "Saint";
                break;
            case 47:
                className = "Sniper";
                break;
            case 48:
                className = "Ninja";
                break;
            case 49:
                className = "Templar";
                break;
            case 50:
                className = "Warlord";
                break;
            case 51:
                className = "Dragon Knight";
                break;
            case 52:
                className = "Death Knight";
                break;
            case 53:
                className = "Gambler";
                break;
            case 54:
                className = "Spy";
                break;
            case 55:
                className = "Nightblade";
                break;
            case 56:
                className = "Myth Master";
                break;
            case 57:
                className = "Summoner";
                break;
            case 58:
                className = "Necromancer";
                break;
            default:
                break;
        }
        return className;
    }

    public override void UpdateClassUnlocks()
    {
        unlockedClasses[0] = true;

        if(classData[0] >= 10)
        {
            unlockedClasses[1] = true;
            unlockedClasses[2] = true;
            unlockedClasses[3] = true;
        }

        if(classData[1] >= 10)
        {
            unlockedClasses[4] = true;
            unlockedClasses[5] = true;
        }

        if(classData[2] >= 10)
        {
            unlockedClasses[6] = true;
            unlockedClasses[7] = true;
        }

        if (classData[3] >= 10)
        {
            unlockedClasses[8] = true;
            unlockedClasses[9] = true;
            unlockedClasses[10] = true;
        }

        if (classData[8] >= 10)
        {
            unlockedClasses[11] = true;
        }

        if (classData[9] >= 10)
        {
            unlockedClasses[12] = true;
            unlockedClasses[13] = true;
        }

        if (classData[10] >= 10)
        {
            unlockedClasses[14] = true;
        }

        if (classData[6] >= 10)
        {
            unlockedClasses[15] = true;
            unlockedClasses[16] = true;
        }

        if (classData[7] >= 10)
        {
            unlockedClasses[17] = true;
            unlockedClasses[18] = true;
        }

        if (classData[4] >= 10)
        {
            unlockedClasses[19] = true;
            unlockedClasses[20] = true;
        }

        if (classData[5] >= 10)
        {
            unlockedClasses[21] = true;
            unlockedClasses[23] = true;
        }

        if (classData[5] >= 10 && classData[4] >= 10)
        {
            unlockedClasses[22] = true;
        }
        //Healer, Brawler
        if (classData[5] >= 10 && classData[8] >= 10)
        {
            unlockedClasses[24] = true;
        }
        //Bandit, Soldier
        if (classData[7] >= 10 && classData[9] >= 10)
        {
            unlockedClasses[25] = true;
        }
        //BlackMage
        if (classData[20] >= 10)
        {
            unlockedClasses[26] = true;
        }
        //Wizard
        if (classData[19] >= 10)
        {
            unlockedClasses[27] = true;
        }
        //Elementalist
        if (classData[22] >= 10)
        {
            unlockedClasses[28] = true;
            unlockedClasses[29] = true;
            unlockedClasses[30] = true;
            unlockedClasses[31] = true;
        }
        //Cleric
        if (classData[21] >= 10)
        {
            unlockedClasses[32] = true;
        }
        //Sage
        if (classData[23] >= 10)
        {
            unlockedClasses[33] = true;
        }
        //Sage, Cleric
        if (classData[23] >= 10 && classData[21] >= 10)
        {
            unlockedClasses[34] = true;
        }
        //Hunter
        if (classData[15] >= 10)
        {
            unlockedClasses[35] = true;
        }
        //Thief
        if (classData[17] >= 10)
        {
            unlockedClasses[36] = true;
        }
        //DualWielder
        if (classData[18] >= 10)
        {
            unlockedClasses[37] = true;
        }
        //Barbarian
        if (classData[11] >= 10)
        {
            unlockedClasses[38] = true;
        }
        //Duelist
        if (classData[13] >= 10)
        {
            unlockedClasses[39] = true;
        }
        //Mercenary
        if (classData[14] >= 10)
        {
            unlockedClasses[40] = true;
        }
        //Knight, Healer
        if (classData[12] >= 10 && classData[5] >= 10)
        {
            unlockedClasses[41] = true;
        }
        //Black Mage, Gladiator
        if (classData[20] >= 10 && classData[40] >= 10)
        {
            unlockedClasses[42] = true;
        }
        //Trapper, Elementalist
        if (classData[16] >= 10 && classData[22] >= 10)
        {
            unlockedClasses[43] = true;
        }
        //Deserter, Barbarian, Mercenary
        if (classData[25] >= 10 && classData[11] >= 10 && classData[14] >= 10)
        {
            unlockedClasses[44] = true;
        }
        
        if (classData[25] >= 10 && classData[11] >= 10 && classData[14] >= 10)
        {
            unlockedClasses[44] = true;
        }
        //Warlock
        if (classData[27] >= 10)
        {
            unlockedClasses[45] = true;
        }
        //Priest
        if (classData[32] >= 10)
        {
            unlockedClasses[46] = true;
        }
        //Ranger
        if (classData[35] >= 10)
        {
            unlockedClasses[47] = true;
        }
        //Assassin
        if (classData[37] >= 10)
        {
            unlockedClasses[48] = true;
        }
        //Paladin
        if (classData[41] >= 10)
        {
            unlockedClasses[49] = true;
        }
        //Berserker
        if (classData[38] >= 10)
        {
            unlockedClasses[50] = true;
        }
        //Swordmaster, MythMaster
        if (classData[56] >= 10 && classData[39] >= 10)
        {
            unlockedClasses[51] = true;
        }
        //Dark Knight, Necro
        if (classData[42] >= 10 && classData[58] >= 10)
        {
            unlockedClasses[52] = true;
        }
        //Pirate, Scout
        if (classData[6] >= 10 && classData[36] >= 10)
        {
            unlockedClasses[54] = true;
        }
        //Black mage, Outlaw
        if (classData[20] >= 10 && classData[44] >= 10)
        {
            unlockedClasses[55] = true;
        }
        //Geo, Aero, Hydro, Pyro, Beastmaster
        if (classData[28] >= 10 && classData[29] >= 10 && classData[30] >= 10 && classData[31] >= 10 && classData[43] >= 10)
        {
            unlockedClasses[56] = true;
        }
        //Illusionist, Druid, Shaman
        if (classData[33] >= 10 && classData[34] >= 10 && classData[26] >= 10)
        {
            unlockedClasses[58] = true;
        }
    }

    public override bool[] GetUnlockedClasses()
    {
        return unlockedClasses;
    }

    /*public HumanData CreateFromJSON(string jsonString)
    {
        JsonClassString();
        return JsonUtility.FromJson<HumanData>(jsonString);
    }

    private void JsonClassString()
    {
        string str = "";
        for (int i = 0; i < classData.Length; i++)
        {
            string str1 = "";
            str = str + classData[i].CreateFromJSON(str1);
            Debug.Log("str1: " + str1);
        }
        Debug.Log("Human Data: " + str);
        jsonString = str;
    }*/
}
