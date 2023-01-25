using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MythData : UnitData
{

    /*public ClassData pegasus = new ClassData();
    public ClassData harpy = new ClassData();
    public ClassData siren = new ClassData();
    public ClassData centaur = new ClassData();
    public ClassData minotaur = new ClassData();
    public ClassData griffin = new ClassData();
    public ClassData roc = new ClassData();
    public ClassData phoenix = new ClassData();
    public ClassData cyclops = new ClassData();
    public ClassData cerberus = new ClassData();
    public ClassData chimera = new ClassData();
    public ClassData dragon = new ClassData();*/

    public string jsonString = "";

    public int[] classData = new int[14];
    private bool[] unlockedClasses = new bool[14];

    public MythData()
    {
        SetUnitData(3);
    }

    public MythData(bool team)
    {
        playerTeam = team;
        SetUnitData(3);
    }

    public override void SetEnemyClass(int classNum)
    {
        charClass = classNum;

        if (classNum == 9 || classNum == 11 || classNum == 12)
        {

            classSize = 2;
        }
        else if (classNum == 13)
        {
            classSize = 3;
        }
        else
        {
            classSize = 1;
        }
    }

    public override void SetClass(int classNum, int platoonNum)
    {
        /*int freeSpace = 3;
        if (platoonNum != -1)
        {
            freeSpace = ArmyData.Instance.platoons[platoonNum].availableSpace;
            if(classSize == 2)
            {
                freeSpace++;
            }else if(classSize == 3)
            {
                freeSpace += 2;
            }
        }*/

        if(platoonNum == -1)
        {
            if (classNum >= 0 && classNum <= classData.Length)
            {
                charClass = classNum;
            }

            if (classNum == 9 || classNum == 11 || classNum == 12)
            {

                classSize = 2;
            }
            else if (classNum == 13)
            {
                classSize = 3;
            }
            else
            {
                classSize = 1;
            }
        }
        else if(classNum >= 0 && classNum <= classData.Length && 
            ((classSize == 1 && (classNum == 10 || classNum < 9)) ||
            (classSize == 2 && (classNum == 9 || classNum == 11 || classNum == 12))))
        {
            charClass = classNum;
        }
        else
        {
            CanvasController.Instance.ClassChangeErrorMessage();

            
        }


        /*if (classNum >= 0 && classNum <= classData.Length && (classNum == 10 || classNum < 9))
        {
            charClass = classNum;
        }
        else
        {
            Debug.Log("Set Class error");
        }

        if(classNum == 9 || classNum == 11 || classNum == 12 )
        {
            
            classSize = 2;
        }else if(classNum == 13)
        {            
            classSize = 3;
        }
        else
        {            
            classSize = 1;
        }*/
    }

    public void Undead(int deathType)
    {

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
            case 7:
            case 8:
                stats[1] += 3;
                break;
            case 1:
            case 9:
            case 13:
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
            case 2:
            case 4:
                stats[0] += 2;
                break;
            case 5:
                stats[0] += 4;
                break;
            case 0:
            case 3:
            case 12:
            case 13:
                stats[0] += 3;
                break;
            case 11:
            case 6:
                stats[0] += 5;
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
            case 4:
            case 5:
            case 6:
            case 13:
                stats[4] += 2;
                healthGain = 20;
                break;
            case 2:
            case 12:
                stats[4] += 3;
                healthGain = 30;
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
            case 4:
            case 5:
                stats[5] += 3;
                break;
            case 8:
                stats[5] += 3;
                break;
            case 12:
                stats[5] += 3;
                break;
            case 10:
                stats[5] += 5;
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
            case 4:
            case 11:
            case 13:
                stats[6] += 4;
                break;
            case 12:
                stats[6] += 2;
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
            case 10:
                stats[3] += 2;
                break;
            case 8:
                stats[3] += 4;
                break;
            case 1:
                stats[3] += 2;
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
            case 7:
                stats[2] += 2;
                break;
            case 0:
            case 1:
            case 3:
            case 10:
            case 13:
                stats[2] += 3;
                break;
            case 9:
                stats[2] += 6;
                break;
            default:
                stats[2] += 1;
                break;
        }

    }

    /*public MythData CreateFromJSON(string jsonString)
    {
        JsonClassString();
        return JsonUtility.FromJson<MythData>(jsonString);
    }

    public void JsonClassString()
    {
        string str = "";
        for(int i = 0; i < classData.Length; i++)
        {
            str = str + classData[i];
        }
        Debug.Log("Myth Data: " + str);
        jsonString = str;
    }*/

    public override string GetCharClassText()
    {
        string className = "";


        switch (charClass)
        {
            case 0:
                className = "Gargoyle";
                break;
            case 1:
                className = "Harpy";
                break;
            case 2:
                className = "Troll";
                break;
            case 3:
                className = "Centaur";
                break;
            case 4:
                className = "Manticore";
                break;
            case 5:
                className = "Chimera";
                break;
            case 6:
                className = "Minotaur";
                break;
            case 7:
                className = "Griffin";
                break;
            case 8:
                className = "Basilisk";
                break;
            case 9:
                className = "Phoenix";
                break;
            case 10:
                className = "Gorgon";
                break;
            case 11:
                className = "Cerberus";
                break;
            case 12:
                className = "Hydra";
                break;
            case 13:
                className = "Dragon";
                break;
        }

        return className;
    }

    public override bool[] GetUnlockedClasses()
    {
        return unlockedClasses;
    }

    public override void UpdateClassUnlocks()
    {
        unlockedClasses[0] = true;
        unlockedClasses[1] = true;
        unlockedClasses[2] = true;
        unlockedClasses[3] = true;

        if ((classData[0] >= 20 && classData[1] >= 20) || (classData[0] >= 20 && classData[2] >= 20) || (classData[3] >= 20 && classData[0] >= 20) ||
            (classData[1] >= 20 && classData[2] >= 20) || (classData[1] >= 20 && classData[3] >= 20) || (classData[2] >= 20 && classData[3] >= 20))
        {
            unlockedClasses[7] = true;
            unlockedClasses[4] = true;
            unlockedClasses[5] = true;
            unlockedClasses[6] = true;
            unlockedClasses[8] = true;
        }

        if ((classData[4] >= 20 && classData[5] >= 20 && classData[6] >= 20) || (classData[4] >= 20 && classData[5] >= 20 && classData[7] >= 20) || (classData[4] >= 20 && classData[5] >= 20 && classData[8] >= 20)
            || (classData[4] >= 20 && classData[6] >= 20 && classData[7] >= 20) || (classData[4] >= 20 && classData[8] >= 20 && classData[6] >= 20) || (classData[4] >= 20 && classData[7] >= 20 && classData[8] >= 20)
            || (classData[5] >= 20 && classData[6] >= 20 && classData[7] >= 20) || (classData[5] >= 20 && classData[8] >= 20 && classData[6] >= 20) || (classData[5] >= 20 && classData[7] >= 20 && classData[8] >= 20)
            || (classData[8] >= 20 && classData[6] >= 20 && classData[7] >= 20))
        {
            unlockedClasses[10] = true;
            unlockedClasses[11] = true;
            unlockedClasses[9] = true;
            unlockedClasses[12] = true;
        }

        if((classData[9] >= 20 && classData[10] >= 20 && classData[11] >= 20) || (classData[9] >= 20 && classData[10] >= 20 && classData[12] >= 20) || (classData[9] >= 20 && classData[11] >= 20 && classData[12] >= 20)
            || (classData[10] >= 20 && classData[11] >= 20 && classData[12] >= 20))
        {
            unlockedClasses[13] = true;
        }

    }
}
