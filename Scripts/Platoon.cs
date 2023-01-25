using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platoon  {

    public int platoonNum;

    private UnitData leader;

    //private bool[] availablePositions = new bool[25];
    public UnitData[] units = new UnitData[25];
    public bool[] blockedSpaces = new bool[25];
    public int availableSpace = 13;

    //private bool isActive = false;

    public Platoon(int num)
    {
        platoonNum = num;
    }

    public bool AddUnit(UnitData unit, int positionNum)
    {

        if (unit.GetClassSize() == 3 && availableSpace > 2 && positionNum > 9 && positionNum % 5 != 4 && positionNum % 5 != 3)
        {
            if (
                blockedSpaces[positionNum] == false && blockedSpaces[positionNum + 1] == false && blockedSpaces[positionNum - 9] == false &&
                blockedSpaces[positionNum - 5] == false && blockedSpaces[positionNum - 4] == false && blockedSpaces[positionNum - 10] == false
                && blockedSpaces[positionNum + 2] == false && blockedSpaces[positionNum - 3] == false && blockedSpaces[positionNum - 8] == false)
            {
                units[positionNum] = unit;
                blockedSpaces[positionNum] = true;
                blockedSpaces[positionNum + 1] = true;
                blockedSpaces[positionNum - 5] = true;
                blockedSpaces[positionNum - 4] = true;
                blockedSpaces[positionNum - 8] = true;
                blockedSpaces[positionNum + 2] = true;
                blockedSpaces[positionNum - 3] = true;
                blockedSpaces[positionNum - 10] = true;
                blockedSpaces[positionNum - 9] = true;
                availableSpace -= 3;
                //Debug.Log("Large sized unit added to position " + positionNum);
                return true;
            }
            else
            {
                Debug.Log("No space for large: " + platoonNum + " position: " + positionNum + " " + unit.GetCharClassText());
            }
        }
        else if (unit.GetClassSize() == 2 && availableSpace > 1 && positionNum > 4 && positionNum % 5 != 4  )
        {
            if (
                blockedSpaces[positionNum] == false && blockedSpaces[positionNum + 1] == false &&
                blockedSpaces[positionNum - 5] == false && blockedSpaces[positionNum - 4] == false)
            {
                units[positionNum] = unit;
                blockedSpaces[positionNum] = true;
                blockedSpaces[positionNum + 1] = true;
                blockedSpaces[positionNum - 5] = true;
                blockedSpaces[positionNum - 4] = true;
                availableSpace -= 2;
                
               // Debug.Log("Medium sized unit added to position " + positionNum);
                return true;
            }
            else
            {
                Debug.Log("No space for medium: " + platoonNum + " position: " + positionNum + " " + unit.GetCharClassText() + " ? " + unit.GetCharClass());
            }
        }else if (blockedSpaces[positionNum] == false && unit.GetClassSize() == 1)
        {
            //availablePositions[positionNum] = false;
            units[positionNum] = unit;
            blockedSpaces[positionNum] = true;
            availableSpace -= 1;
            if (unit.IsHero())
            {
                GameController.Instance.heroInPlatoon = true;
                GameController.Instance.SetHeroPlatoonNum(platoonNum);
            }
            return true;
        } else
        {
            Debug.Log("Position is blocked platoon: " + platoonNum + " position: " + positionNum);
            return false;
        }

        return false;
    }

    public void RemoveUnit(UnitData unit, int positionNum)
    {
        if (unit.GetClassSize() == 3)
        {            
                units[positionNum] = null;
                blockedSpaces[positionNum] = false;
                blockedSpaces[positionNum + 1] = false;
                blockedSpaces[positionNum - 5] = false;
                blockedSpaces[positionNum - 4] = false;
                blockedSpaces[positionNum - 8] = false;
                blockedSpaces[positionNum + 2] = false;
                blockedSpaces[positionNum - 3] = false;
                blockedSpaces[positionNum - 10] = false;
                blockedSpaces[positionNum - 9] = false;
            availableSpace += 3;

        }
        else if (unit.GetClassSize() == 2)
        {           
                units[positionNum] = null;
                blockedSpaces[positionNum] = false;
                blockedSpaces[positionNum + 1] = false;
                blockedSpaces[positionNum - 5] = false;
                blockedSpaces[positionNum - 4] = false;
            availableSpace += 2;

        }
        else if (blockedSpaces[positionNum] == true)
        {
            //availablePositions[positionNum] = false;
            units[positionNum] = null;
            blockedSpaces[positionNum] = false;
            availableSpace += 1;
        }

        if (unit.IsHero()) {
            GameController.Instance.heroInPlatoon = false; 
        }
    }

    public void SetLeader(UnitData unit)
    {
        leader = unit;
    }

    public UnitData GetLeader()
    {
        return leader;
    }

    public int TotalLevels()
    {
        int levels = 0;

        for(int i = 0; i < 25; i++)
        {
            if(units[i] != null)
            {
                levels += units[i].totalLevels * units[i].GetClassSize();
            }
        }

        return levels;
    }

}
