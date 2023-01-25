using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ArmyData : MonoBehaviour {

    public static ArmyData Instance;

    public List<UnitData> units = new List<UnitData>();
    public List<Platoon> platoons = new List<Platoon>();

    private string jsonString = "";
   // private int unitCount = 80;

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

    public void ClearArmy()
    {
        //Should actually delete all units and platoons

        units.Clear();
        platoons.Clear();
    }

    public void StartingArmy()
    {
            Platoon p = new Platoon(0);
            platoons.Add(p);
            for (int i = 0; i < 13; i++)
            {
                if (i == 9)
                {
                    HumanData hLeader = new HumanData(true, true);
                    hLeader.SetPlatoonPosition(0, 2 * i);

                    p.SetLeader(hLeader);
                    hLeader.MakeHero();
                    hLeader.LevelUp();

                    units.Add(hLeader);
                    p.AddUnit(hLeader, 2 * i);
                }
                else
                {
                    HumanData h = new HumanData();
                    h.SetPlatoonPosition(0, 2 * i);

                    if (i == 0)
                    {
                        h.SetClass(3, 0);
                        h.LevelUp();
                    }

                    if (i == 12)
                    {
                        h.SetClass(1, 0);
                        h.LevelUp();
                    }
                    if (i == 10)
                    {
                        h.SetClass(2, 0);
                        h.LevelUp();
                    }



                    units.Add(h);
                    p.AddUnit(h, 2 * i);
                }

            
            }

        StoryController.Instance.ShowStory(0);


    }

    public void ArmyToString()
    {
        string str = "";
        for (int i = 0; i < units.Count; i++)
        {
            str = str + units[i].CreateJSON() + "xghx";
        }
        jsonString = str;
    }

    public void StringToArmy(string armyJson)
    {
        string[] separator = new string[] { "xghx" };
        string[] result;
        result = armyJson.Split(separator, System.StringSplitOptions.None);
        foreach(string str in result)
        {
            HumanData d = JsonUtility.FromJson<HumanData>(str);
            if(d != null && d.GetCharType() == 2)
            {
                AnimalData e = JsonUtility.FromJson<AnimalData>(str);
                units.Add(e);
            }
            else if(d != null && d.GetCharType() == 3)
            {
                MythData f = JsonUtility.FromJson<MythData>(str);
                units.Add(f);
            }
            else if(d != null)
            {
                units.Add(d);
                if (d.IsHero() && d.platoonNum == -1)
                {
                    GameController.Instance.heroInPlatoon = false;
                }
            }    
        }
        foreach(UnitData u in ArmyData.Instance.units)
        {
            if(u.platoonNum!= -1)
            {
                if (platoons.Count <= u.platoonNum)
                {
                    while (platoons.Count <= u.platoonNum)
                    {
                        int platoonNum = platoons.Count;
                        Platoon p = new Platoon(platoonNum);
                        platoons.Add(p);
                    }
                }
                platoons[u.platoonNum].AddUnit(u, u.positionNum);
            }            
        }
    }

    public string getJsonString()
    {
        ArmyToString();
        return jsonString;
    }

    public string SavePlatoonLeaders()
    {
        string platoonLeads = "";

        foreach(Platoon p in platoons)
        {
            platoonLeads += p.GetLeader().positionNum + ",";
        }

        return platoonLeads;
    }

    public void LoadPlatoonLeads(string platoonLeadString)
    {
        string separator = ",";
        int platoonCount = 0;

        while(platoonLeadString.Length > 0)
        {
            int index = platoonLeadString.IndexOf(separator);

            if(index == -1)
            {
                //Remaining string is last platoon name
                int leadPosition = Convert.ToInt16(platoonLeadString.Substring(0, index));
                platoons[platoonCount].SetLeader(platoons[platoonCount].units[leadPosition]);
                return;
            }
            else
            {
                //Everything up until index is next platoon name
                int leadPosition = Convert.ToInt16(platoonLeadString.Substring(0, index));
                platoons[platoonCount].SetLeader(platoons[platoonCount].units[leadPosition]);
                //delete everything through index and separator
                platoonLeadString = platoonLeadString.Substring(index+1);
                platoonCount++;
            }
        }
    }

    public List<UnitData> GetUnassignedUnits()
    {
        List<UnitData> uaUnits = new List<UnitData>();

        foreach(UnitData ud in units)
        {
            if(ud.platoonNum == -1)
            {
                uaUnits.Add(ud);
            }
        }

        return uaUnits;
    }

    public string GetHeroName()
    {
        foreach (UnitData ud in units)
        {
            if (ud.specialCharNum == 1)
            {
                return ud.unitName;
            }
        }
        return "error";
    }
}
