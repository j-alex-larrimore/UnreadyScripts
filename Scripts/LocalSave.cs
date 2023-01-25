using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

public class LocalSave : MonoBehaviour
{
    public static LocalSave Instance;
    //FIX THIS DICTIONARY TO NOT BE PLAYFAB
    //public Dictionary<string, PlayFab.ClientModels.UserDataRecord> user;

    public Dictionary<string, string> user = new Dictionary<string, string>();

    public AudioClip loginSound;
   // public InputField userName;
    //public InputField password;

   // public GameObject errorLoginScreen;
   // public string playerName;
    public int levelsComplete = 0;
  // private string playFabId;
  //  public Text startErrorText;
    private bool newUser = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            DestroyImmediate(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    public void Continue()
    {
        int userData = GetUserData();

        if (userData == 2)
        {
            //Old stuff
            ArmyData.Instance.ClearArmy();
            CanvasController.Instance.saveRetryScreen.SetActive(false);
            CanvasController.Instance.gameLostScreen.SetActive(false);
            CanvasController.Instance.hardGameLostScreen.SetActive(false);
            ArmyData.Instance.StringToArmy(Convert.ToString(user["Army"]));
            ArmyData.Instance.LoadPlatoonLeads(Convert.ToString(user["Leaders"]));
            GameController.Instance.difficulty = Convert.ToBoolean(user["Difficulty"]);
            GameController.Instance.levelsCompleted = Convert.ToInt16(user["LevelsCompleted"]);
            GameController.Instance.restarts = Convert.ToInt16(user["Restarts"]);
            GameController.Instance.GameContinued();
            if (GameController.Instance.levelsCompleted == 0)
            {
                CanvasController.Instance.PickDifficulty();
            }
            else
            {
                CanvasController.Instance.ShowMap();
            }
        }
        else if(userData == 1)
        {
            Debug.Log("Continuing mid battle!");
            GameController.Instance.gameLoadedMidLevel = true;
            ArmyData.Instance.ClearArmy();
            CanvasController.Instance.saveRetryScreen.SetActive(false);
            CanvasController.Instance.gameLostScreen.SetActive(false);
            CanvasController.Instance.hardGameLostScreen.SetActive(false);
            CanvasController.Instance.globalMap.SetActive(false);
            ArmyData.Instance.StringToArmy(Convert.ToString(user["Army"]));
            ArmyData.Instance.LoadPlatoonLeads(Convert.ToString(user["Leaders"]));
            GameController.Instance.difficulty = Convert.ToBoolean(user["Difficulty"]);
            GameController.Instance.levelsCompleted = Convert.ToInt16(user["LevelsCompleted"]);
            GameController.Instance.restarts = Convert.ToInt16(user["Restarts"]);
            GlobalMapController.Instance.ContinueBattle(Convert.ToInt16(user["LevelNum"]));
            //Set Platoon leader locations
            //Set ambush triggers
        }

    }

    public int GetUserData()
    {
        return LoadFile();
    }

    public int LoadFile()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfoMidGame.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfoMidGame.dat", FileMode.Open);
            PlayerDataMidGame data = (PlayerDataMidGame)bf.Deserialize(file);
            file.Close();
            user["Army"] = data.Army;
            user["Leaders"] = data.Leaders;
            user["Difficulty"] = data.Difficulty;
            user["Restarts"] = data.Restarts;
            user["LevelsCompleted"] = data.LevelsCompleted;
            user["PlayerLeaderLevelInfo"] = data.PlayerLeaderLevelInfo;
            user["EnemyLeaderLevelInfo"] = data.EnemyLeaderLevelInfo;
            user["EnemyArmy"] = data.EnemyArmy;
            user["AmbushTriggers"] = data.AmbushTriggers;
            user["LevelNum"] = data.LevelNum;
            user["BaseOwnership"] = data.BaseOwnership;          
            Debug.Log("Midgame File exists!");
            return 1;
        }
        else if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();
            user["Army"] = data.Army;
            user["Leaders"] = data.Leaders;
            user["Difficulty"] = data.Difficulty;
            user["Restarts"] = data.Restarts;
            user["LevelsCompleted"] = data.LevelsCompleted;
            Debug.Log("File exists!");
            return 2;
        }
        else
        {
            Debug.Log("No file exists :(");
            CanvasController.Instance.NewGame();
            return 3;
        }
    }

    public void MidLevelSaveProgress()
    {
        BinaryFormatter bf = new BinaryFormatter();
        //Unity's hidden data path plus our file name
        FileStream file = File.Open(Application.persistentDataPath + "/playerInfoMidGame.dat", FileMode.OpenOrCreate);

        PlayerDataMidGame data = new PlayerDataMidGame();
        data.Army = ArmyData.Instance.getJsonString();
        data.LevelsCompleted = "" + (GameController.Instance.levelsCompleted);
        data.Leaders = "" + ArmyData.Instance.SavePlatoonLeaders();
        data.Difficulty = "" + GameController.Instance.difficulty;
        data.Restarts = "" + GameController.Instance.restarts;
        data.LevelNum = "" + GlobalMapController.Instance.GetCurrentLocation();
        data.AmbushTriggers = LevelController.Instance.GetAmbushString();
        data.BaseOwnership = LevelController.Instance.GetTownString();
        data.EnemyArmy = LevelController.Instance.GetEnemyUnitJSONString();
        data.PlayerLeaderLevelInfo = LevelController.Instance.GetPlayerLeaderLocationsString();
        data.EnemyLeaderLevelInfo = LevelController.Instance.GetEnemyLeaderLocationsString();
        bf.Serialize(file, data);
        file.Close();
        CanvasController.Instance.SetSaveText("Data Saved!");
    }

    public void SaveProgress()
    {
        /*Dictionary<string, string> data = new Dictionary<string, string>()
            {
                {"Army", ArmyData.Instance.getJsonString() },
                {"LevelsCompleted", "" + (GameController.Instance.levelsCompleted)},
                {"Difficulty", "" + GameController.Instance.difficulty },
                {"Leaders", "" + ArmyData.Instance.SavePlatoonLeaders()}
            };
        
        user = data;*/
        Save();
        CanvasController.Instance.SetSaveText("Data Saved!");
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        //Unity's hidden data path plus our file name
        FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.OpenOrCreate);
        if (File.Exists(Application.persistentDataPath + "/playerInfoMidGame.dat"))
        {
            File.Delete(Application.persistentDataPath + "/playerInfoMidGame.dat");
        }
        PlayerData data = new PlayerData();
        data.Army = ArmyData.Instance.getJsonString();
        data.LevelsCompleted = "" + (GameController.Instance.levelsCompleted);
        data.Leaders = "" + ArmyData.Instance.SavePlatoonLeaders();
        data.Difficulty = "" + GameController.Instance.difficulty;
        data.Restarts = "" + GameController.Instance.restarts;
        Debug.Log("Saving data. Restarts: " + data.Restarts);
        bf.Serialize(file, data);
        file.Close();
    }

    public void GetUserDataRetry()
    {
        GetUserData();

        if (!newUser)
        {
            Continue();
            GameController.Instance.LevelOver();
            //CanvasController.Instance.ShowMap();
        }
    }

    public string GetEnemyArmy()
    {
        return Convert.ToString(user["EnemyArmy"]);
    }

    public string GetBaseOwnership()
    {
        return Convert.ToString(user["BaseOwnership"]);
    }

    public string GetAmbush()
    {
        return Convert.ToString(user["AmbushTriggers"]);
    }

    public string GetPlayerLeaderLocs()
    {
        return Convert.ToString(user["PlayerLeaderLevelInfo"]);
    }

    public string GetEnemyLeaderLocs()
    {
        return Convert.ToString(user["EnemyLeaderLevelInfo"]);
    }
}

[Serializable]
class PlayerData
{
    public string Army;
    public string LevelsCompleted;
    public string Difficulty;
    public string Leaders;
    public string Restarts;
}

[Serializable]
class PlayerDataMidGame
{
    //Does this include health?
    public string Army;
    public string LevelsCompleted;
    public string Difficulty;
    public string Leaders;
    public string Restarts;

    //Player and enemy leaders
    public string LevelNum;

    public string PlayerLeaderLevelInfo;
    public string EnemyLeaderLevelInfo;
    public string EnemyArmy;
    public string BaseOwnership;
    public string AmbushTriggers;
}
