using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalData : UnitData
{

    /*public ClassData leopard = new ClassData();
    public ClassData wolf = new ClassData();
    public ClassData bear = new ClassData();
    public ClassData lion = new ClassData();
    public ClassData rhino = new ClassData();
    public ClassData jaguar = new ClassData();
    public ClassData tiger = new ClassData();
    public ClassData elephant = new ClassData();
    public ClassData hippo = new ClassData();
    public ClassData crocodile = new ClassData();*/


    public string jsonString = "";

    public int[] classData = new int[10];
    private bool[] unlockedClasses = new bool[10];

    public AnimalData()
    {
        SetUnitData(2);
    }

    public AnimalData(bool team)
    {
        playerTeam = team;
        SetUnitData(2);
    }

    public override void SetEnemyClass(int classNum)
    {
        charClass = classNum;

        if (classNum >= 4 && classNum != 6)
        {
            classSize = 2;
        }
        else
        {
            classSize = 1;
        }
    }

    public override void SetClass(int classNum, int platoonNum)
    {
        if (platoonNum == -1)
        {
            if (classNum >= 0 && classNum <= classData.Length)
            {
                charClass = classNum;
            }

            if (classNum >= 4 && classNum != 6)
            {

                classSize = 2;
            }else
            {
                classSize = 1;
            }
        }
        else if (classNum >= 0 && classNum <= classData.Length &&
            ((classSize == 1 && (classNum == 6 || classNum < 4)) ||
            (classSize == 2 && (classNum > 6 || classNum == 4 || classNum == 5))))
        {
            charClass = classNum;
        }
        else
        {
            CanvasController.Instance.ClassChangeErrorMessage();
        }

        /*int freeSpace = 2;
        if (platoonNum != -1)
        {
            freeSpace = ArmyData.Instance.platoons[platoonNum].availableSpace;
            if (classSize == 2)
            {
                freeSpace++;
            }
        }

        if (classNum >= 0 && classNum <= classData.Length && (classNum == 6 || classNum < 4 ||
            freeSpace > 0))
        {
            charClass = classNum;
        }
        else
        {
            Debug.Log("Set Class error");
        }

        if (classNum == 4 || classNum == 5 || classNum > 6)
        {
            if(platoonNum > 0 && classSize == 1)
            {
                ArmyData.Instance.platoons[platoonNum].availableSpace--;
            }
            classSize = 2;
        }
        else
        {
            if (platoonNum > 0 && classSize == 2)
            {
                ArmyData.Instance.platoons[platoonNum].availableSpace++;
            }
            classSize = 1;
        }*/
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

            case 6:
                stats[1] += 3;
                break;
            case 0:
            case 9:
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
            case 9:
                stats[0] += 2;
                break;
            case 1:
            case 5:
                stats[0] += 3;
                break;
            case 3:
            case 8:
                stats[0] += 4;
                break;
            case 7:
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
            case 2:
            case 5:
            case 8:
            case 9:
                stats[4] += 2;
                healthGain = 20;
                break;
            case 4:
            case 7:
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
        stats[5] += 1;
    }

    public void IntUp()
    {
        stats[6] += 1;
    }

    public void AgiUp()
    {
        switch (charClass)
        {
            case 8:
                stats[3] += 3;
                break;
            case 9:
                stats[3] += 4;
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
            default:
                stats[2] += 1;
                break;
        }
    }

    public void Undead(int deathType)
    {

    }

    /* public AnimalData CreateFromJSON(string jsonString)
     {
         JsonClassString();
         return JsonUtility.FromJson<AnimalData>(jsonString);
     }

     public void JsonClassString()
     {
         string str = "";
         for (int i = 0; i < classData.Length; i++)
         {
             str = str + classData[i];
         }
         Debug.Log("Animal Data: " + str);
         jsonString = str;
     }*/

    public override string GetCharClassText()
    {
        string className = "";


        switch (charClass)
        {
            case 0:
                className = "Fox";
                break;
            case 1:
                className = "Wolf";
                break;
            case 2:
                className = "Boar";
                break;
            case 3:
                className = "Lion";
                break;
            case 4:
                className = "Rhino";
                break;
            case 5:
                className = "Bear";
                break;
            case 6:
                className = "Eagle";
                break;
            case 7:
                className = "Elephant";
                break;
            case 8:
                className = "Hippo";
                break;
            case 9:
                className = "Crocodile";
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

        if ((classData[0] >= 20 && classData[1] >= 20) || (classData[2] >= 20 && classData[1] >= 20) || (classData[0] >= 20 && classData[2] >= 20))
        {
            unlockedClasses[3] = true;
            unlockedClasses[4] = true;
            unlockedClasses[5] = true;
            unlockedClasses[6] = true;
        }

        if ((classData[3] >= 25 && classData[4] >= 25) || (classData[3] >= 25 && classData[5] >= 25) || (classData[3] >= 25 && classData[6] >= 25) ||
            (classData[5] >= 25 && classData[4] >= 25) || (classData[4] >= 25 && classData[6] >= 25) || (classData[5] >= 25 && classData[6] >= 25))
        {
            unlockedClasses[7] = true;
            unlockedClasses[8] = true;
            unlockedClasses[9] = true;
        }
    }
}
