using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class DataManagement : MonoBehaviour
{

    //public static DataManagement Instance;

    public AudioClip loginSound;
    public InputField userName;
    public InputField password;

    public GameObject errorLoginScreen;

    //private bool gameStart = false;
    public string playerName;
    public int levelsComplete = 0;
    private string playFabId;
    public Text startErrorText;
    public Dictionary<string, PlayFab.ClientModels.UserDataRecord> user;
    //private bool continueSelected = false;
    private bool newUser = false;

    private void Awake()
    {
        /*if (Instance != null && Instance != this)
        {
            DestroyImmediate(gameObject);
            return;
        }

        Instance = this;*/

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        PlayFabSettings.TitleId = "6514";

    }

    private void OnGetAccountInfoSuccess(GetPlayerCombinedInfoResult result)
    {
        if (result.InfoResultPayload.PlayerProfile.DisplayName == null)
        {
            UpdateUserTitleDisplayNameRequest request = new UpdateUserTitleDisplayNameRequest()
            {
                DisplayName = userName.text
            };
            PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnPlayerNameUpdated, OnLoginFailure);
        }
        else
        {
            Debug.Log(result.InfoResultPayload.PlayerProfile.DisplayName.ToString());
        }
    }

    private void OnPlayerNameUpdated(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Player display name updated");
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.Log("Playfab Login Error :(");
        Debug.LogError(error.GenerateErrorReport());
        startErrorText.text = error.GenerateErrorReport();
    }

    private void OnLoginSuccess(LoginResult result)
    {
        SoundController.Instance.PlaySingle(loginSound);
        GetPlayerCombinedInfoRequest request = new GetPlayerCombinedInfoRequest()
        {
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams()
            {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.GetPlayerCombinedInfo(request, OnGetAccountInfoSuccess, OnLoginFailure);
        playerName = userName.text;
        playFabId = result.PlayFabId;
        startErrorText.text = "";
        CanvasController.Instance.DisableLogin();
        GetUserData();
        SoundController.Instance.LoginRegister();
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        newUser = true;
        Login();
    }

    private void OnRegisterFailure(PlayFabError error)
    {
        Debug.Log("Playfab Register Error :(");
        Debug.LogError(error.GenerateErrorReport());
        startErrorText.text = error.GenerateErrorReport();
    }

    public void SignUp()
    {
        if (password.text.Length > 0 && userName.text.Length > 0)
        {
            var request = new RegisterPlayFabUserRequest()
            {
                TitleId = PlayFabSettings.TitleId,
                Username = userName.text,
                Password = password.text,
                DisplayName = userName.text,
                RequireBothUsernameAndEmail = false
            };
            PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterFailure);
        }
    }

    public void Login()
    {
        if (password.text.Length > 0 && userName.text.Length > 0)
        {
            var request = new LoginWithPlayFabRequest()
            {
                TitleId = PlayFabSettings.TitleId,
                Username = userName.text,
                Password = password.text
            };
            PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnLoginFailure);
        }
    }

    public void GetUserData()
    {
        GetUserDataRequest request = new GetUserDataRequest()
        {
            PlayFabId = playFabId,
            Keys = null
        };

        PlayFabClientAPI.GetUserData(request, (result) => {
            if ((result.Data == null) || (result.Data.Count == 0))
            {
                user = result.Data;
                CanvasController.Instance.NewGame();

            }
            else
            {
                user = result.Data;
                Debug.Log("Data received: " + user);
                
                if (!newUser)
                {
                    CanvasController.Instance.NewOrContinue();
                }

            }
        }, (error) => {
            Debug.Log("Got error retrieving user data:");
            Debug.Log(error.ErrorMessage);
        });
    }

    public void GetUserDataRetry()
    {
        GetUserDataRequest request = new GetUserDataRequest()
        {
            PlayFabId = playFabId,
            Keys = null
        };

        PlayFabClientAPI.GetUserData(request, (result) => {
            if ((result.Data == null) || (result.Data.Count == 0))
            {
                user = result.Data;
                CanvasController.Instance.NewGame();

            }
            else
            {
                user = result.Data;

                if (!newUser)
                {
                    Continue();
                    GameController.Instance.LevelOver();
                    //CanvasController.Instance.ShowMap();
                }

            }
        }, (error) => {
            Debug.Log(error.ErrorMessage);
        });
    }

    public void Continue()
    {
        ArmyData.Instance.ClearArmy();
        CanvasController.Instance.saveRetryScreen.SetActive(false);
        CanvasController.Instance.gameLostScreen.SetActive(false);
        CanvasController.Instance.hardGameLostScreen.SetActive(false);
        ArmyData.Instance.StringToArmy(Convert.ToString(user["Army"].Value));
        ArmyData.Instance.LoadPlatoonLeads(Convert.ToString(user["Leaders"].Value));
        GameController.Instance.difficulty = Convert.ToBoolean(user["Difficulty"].Value);
        GameController.Instance.levelsCompleted = Convert.ToInt16(user["LevelsCompleted"].Value);
        GameController.Instance.restarts = Convert.ToInt16(user["Restarts"].Value);
        GameController.Instance.GameContinued();
        /* GameController.Instance.DisableContinue();
         CharacterData.Instance.SetFlyingJSON(Convert.ToString(user["Flying"].Value));
         CharacterData.Instance.SetShieldJSON(Convert.ToString(user["Shield"].Value));
         CharacterData.Instance.SetArmorJSON(Convert.ToString(user["Armor"].Value));
         CharacterData.Instance.SetAttackRangeJSON(Convert.ToString(user["AttackRange"].Value));
         CharacterData.Instance.SetAttackDamageJSON(Convert.ToString(user["AttackDamage"].Value));
         CharacterData.Instance.SetMoveDistanceJSON(Convert.ToString(user["MoveDistance"].Value));
         CharacterData.Instance.SetMaxHealthJSON(Convert.ToString(user["MaxHealth"].Value));
         CharacterData.Instance.SetCurrentHealthJSON(Convert.ToString(user["CurrentHealth"].Value));
         GameController.Instance.SetShopPurchases(Convert.ToString(user["ShopPurchases"].Value));
         GameController.Instance.currentLevel = Convert.ToInt16(user["CurrentLevel"].Value);
         GameController.Instance.LoadCoins(Convert.ToInt16(user["Coins"].Value));
         GameController.Instance.Continue();
         GameController.Instance.CheckForDeadChars();
         InvalidateSavedData();*/
    }

    //USER SAVED STATS

    public void LevelComplete(int lc)
    {
        if (lc > levelsComplete)
        {
            UpdateLevelsCompleted(lc);
        }
    }

    public void UpdateLevelsCompleted(int level)
    {
        String str = "" + level;
        UpdateUserDataRequest request = new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>(){
                {"LevelsCompleted", str}
            }
        };

        PlayFabClientAPI.UpdateUserData(request, (result) =>
        {

        }, (error) => {
            Debug.Log("Update data error " + error);
        });
    }

    public void GetLevelsCompleted()
    {
        List<string> scores = new List<string>();
        scores.Add("LevelsCompleted");
        GetUserDataRequest request = new GetUserDataRequest()
        {
            PlayFabId = playFabId,
            Keys = scores
        };

        PlayFabClientAPI.GetUserData(request, (result) => {
            if ((result.Data == null) || (result.Data.Count == 0))
            {
                LevelComplete(0);
                levelsComplete = 0;
                
                GetUserData();
            }
            else
            {
                foreach (var item in result.Data)
                {
                    levelsComplete = Convert.ToInt16(item.Value.Value);
                }
                GetUserData();
            }
        }, (error) => {
            Debug.Log(error.ErrorMessage);
        });
    }

    public void SaveProgress()
    {

        UpdateUserDataRequest request = new UpdateUserDataRequest()
        {

            Data = new Dictionary<string, string>()
            {
                {"Army", ArmyData.Instance.getJsonString() },
                {"LevelsCompleted", "" + (GameController.Instance.levelsCompleted)},
                {"Restarts", "" + (GameController.Instance.restarts)},
                {"Difficulty", "" + GameController.Instance.difficulty },
                {"Leaders", "" + ArmyData.Instance.SavePlatoonLeaders()}
            }
        };

        PlayFabClientAPI.UpdateUserData(request, (result) =>
        {
            CanvasController.Instance.SetSaveText("Data Saved!");
            //GetUserData();
        }, (error) => {
           /* if((int)error.Error == 1100 || (int)error.Error == 1100)
            {
                
            }*/
            errorLoginScreen.SetActive(true);
            CanvasController.Instance.SetSaveText("Update data error " + error + " " + error.Error);
        });
    }

    public void EmergencyLogin()
    {
        if (password.text.Length > 0 && userName.text.Length > 0)
        {
            var request = new LoginWithPlayFabRequest()
            {
                TitleId = PlayFabSettings.TitleId,
                Username = userName.text,
                Password = password.text
            };
            PlayFabClientAPI.LoginWithPlayFab(request, OnEmergencyLoginSuccess, OnLoginFailure);
        }
    }

    private void OnEmergencyLoginSuccess(LoginResult result)
    {
        SoundController.Instance.PlaySingle(loginSound);
        GetPlayerCombinedInfoRequest request = new GetPlayerCombinedInfoRequest()
        {
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams()
            {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.GetPlayerCombinedInfo(request, OnGetAccountInfoSuccess, OnLoginFailure);
        playerName = userName.text;
        playFabId = result.PlayFabId;
        startErrorText.text = "";
        errorLoginScreen.SetActive(false);
        SoundController.Instance.LoginRegister();
    }

    /*
     private void InvalidateSavedData()
     {
         UpdateUserDataRequest request = new UpdateUserDataRequest()
         {
             Data = new Dictionary<string, string>(){
                 {"CurrentLevel", ""+0}
             }
         };
         
     }*/
}
