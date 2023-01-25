using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GlobalMapController : MonoBehaviour {

	public static GlobalMapController Instance;

	private int[] locationArray = { 1, 1, 3, 4, 5,
        5, 7, 8, 9, 10,
        11, 12, 13, 14,
        15, 16, 17, 18,
        19, 20, 21, 22, 23,
        24, 25, 25, 27, 28,
        28, 30};
    public GameObject[] locationButtons;
    private int currentPlayerLocation = 0;
	public GameObject player;
    public bool storyACompleted = false;
    public bool storyBCompleted = false;

    //private float moveTime = 200.0f;
    private RectTransform prt;

    private bool fightActive = false;
    public GameObject fightButton;

    public int clickedButtonNum = -1;

    private bool mapButtonClicked = true;
    public bool paused = false;

	void Awake(){
		if (Instance != null && Instance != this) {
			DestroyImmediate (this);
			return;
		}

		Instance = this;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MapButtonReset()
    {
        mapButtonClicked = false;
        paused = false;
    }

    public int GetNextLocation()
    {
        if (GameController.Instance.levelsCompleted < locationArray.Length)
        {
            return locationArray[GameController.Instance.levelsCompleted];
        }
        else
        {
            return locationArray[GameController.Instance.levelsCompleted % 30];
        }        
    }

    public GameObject GetNextButton()
    {
        return locationButtons[GetNextLocation() - 1];
    }

	public void MapButtonClicked(int buttonNum, GameObject buttonObj){

        if ((buttonNum == GetNextLocation() ||
            !GameController.Instance.difficulty && GetNextLocation() > buttonNum) && !paused)
        {
            clickedButtonNum = buttonNum;

            mapButtonClicked = true;
            Debug.Log("Map Button Clicked: " + buttonNum);

            if(GetNextLocation() != currentPlayerLocation)
            {
                CanvasController.Instance.GlobalPlayerAnimate(buttonNum);
                currentPlayerLocation = GetNextLocation();
            }

            /*if ()
            {
                InterludeOne();
            }else if ()
            {
                InterludeTwo();
            }
            else
            {*/
                Invoke("ShowFightButton", 3);

            //}
        }
    }

    /*protected IEnumerator SmoothMovementRoutine(Vector2 endPosition){
        float remainingDistanceToEndPosition;

        do{
            remainingDistanceToEndPosition = (prt.anchoredPosition - endPosition).sqrMagnitude;
            Vector2 updatedPosition = Vector2.MoveTowards(prt.anchoredPosition, endPosition, moveTime * Time.deltaTime);
            prt.anchoredPosition = updatedPosition;
            yield return null;
        }while(remainingDistanceToEndPosition > float.Epsilon);
    }*/

    public void SetPlayerLocation(int level)
    {
        Debug.Log("Old Player Loc: " + player.GetComponent<RectTransform>().localPosition);
       // player.GetComponent<RectTransform>().localPosition = locationButtons[level].transform.localPosition;
        Debug.Log("New Player Loc: " + player.GetComponent<RectTransform>().localPosition);
    }

    public void InterludeOne()
    {
        StoryController.Instance.ShowStory(31);
    }

    public void InterludeTwo()
    {
        StoryController.Instance.ShowStory(32);
    }


    private void ShowFightButton()
    {
        Debug.Log("Show Fight Button!");
        fightActive = true;
        fightButton.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }

    public void HideFightButton()
    {
       /* Debug.Log("Hide Fight Button!");
        if (GameController.Instance.difficulty && ((GameController.Instance.levelsCompleted != 5)
                //&& (GameController.Instance.levelsCompleted != 12)
                //&& (GameController.Instance.levelsCompleted != 17)
                 && (GameController.Instance.levelsCompleted != 27)
                 && (GameController.Instance.levelsCompleted != 30)))
        {*/
            Debug.Log("Hide Fight Button2!");
            fightActive = false;
            fightButton.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
        //}        
    }

    public void FightButtonClicked()
    {
        if (fightActive && !paused && GameController.Instance.heroInPlatoon)
        {
            HideFightButton();
            CanvasController.Instance.heroWarningText.SetActive(false);
            SoundController.Instance.ButtonClick();

            GameController.Instance.ArmyUnpause();

            //Either start current level
            if ((mapButtonClicked && clickedButtonNum == GetNextLocation() ) || (!mapButtonClicked && (GameController.Instance.levelsCompleted == 1)
                //|| (GameController.Instance.levelsCompleted == 5)
                //|| (GameController.Instance.levelsCompleted == 12)
                //|| (GameController.Instance.levelsCompleted == 17)
                 //|| (GameController.Instance.levelsCompleted == 27)
                // || (GameController.Instance.levelsCompleted == 30)
                ))
            {
                Debug.Log("Loading SceneA: " + clickedButtonNum + " LevelsCompleted: " + GameController.Instance.levelsCompleted);

                int sceneToLoad = (GameController.Instance.levelsCompleted + 1) % 30;
                if (sceneToLoad == 0)
                    sceneToLoad = 30;
                SceneManager.LoadScene(sceneToLoad);
               
            }
            else if(mapButtonClicked)
            {
                Debug.Log("Loading SceneB: " + clickedButtonNum + " LevelsCompleted: " + GameController.Instance.levelsCompleted);
                switch (clickedButtonNum)
                {
                    case 1:
                        SceneManager.LoadScene(2);
                        break;
                    case 5:
                        SceneManager.LoadScene(6);
                        break;
                    case 25:
                        SceneManager.LoadScene(26);
                        break;
                    case 28:
                        SceneManager.LoadScene(29);
                        break;
                    default:
                        SceneManager.LoadScene(clickedButtonNum);
                        break;
                }
            }
            
            //Or easy mode earlier level

            /*if (mapButtonClicked || (GameController.Instance.levelsCompleted == 1)
                || (GameController.Instance.levelsCompleted == 5) 
                || (GameController.Instance.levelsCompleted == 12)
                || (GameController.Instance.levelsCompleted == 17)
                 || (GameController.Instance.levelsCompleted == 27)
                 || (GameController.Instance.levelsCompleted == 30))
            {
                Debug.Log("Loading SceneB: " + clickedButtonNum + " LevelsCompleted: " + GameController.Instance.levelsCompleted);
                SceneManager.LoadScene(clickedButtonNum);
            }
            else 
            {
                Debug.Log("Loading SceneB: " + clickedButtonNum + " LevelsCompleted: " + GameController.Instance.levelsCompleted);
                SceneManager.LoadScene(GameController.Instance.levelsCompleted);
            } */
            CanvasController.Instance.HideMap();
        }
        else if (fightActive && !paused && !GameController.Instance.heroInPlatoon)
        {
            CanvasController.Instance.heroWarningText.SetActive(true);
        }
    }

    public int GetCurrentLocation()
    {
        return currentPlayerLocation;
    }

    public void ContinueBattle(int levelNum)
    {
        Debug.Log("Continue Battle! " + levelNum);
        //SetPlayerLocation(levelNum);
        currentPlayerLocation = levelNum;
        SceneManager.LoadScene(levelNum + 1);
    }
}
