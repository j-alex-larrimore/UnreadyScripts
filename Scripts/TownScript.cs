using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownScript : MonoBehaviour {

    //0 = nobody, 1 = player, 2 = enemy
    public int ownedBy = 0;
    public bool isEnemyBase = false;
    public bool isPlayerBase = false;

    //private List<GameObject> playerOccupants = new List<GameObject>();
    //private List<GameObject> enemyOccupants = new List<GameObject>();
    int playerCount = 0;
    int enemyCount = 0;
    private bool[] playersOccupying = new bool[20];
    private bool[] enemiesOccupying = new bool[30];

    public GameObject townObject;

    private Material material;

    private float takeOverTime = 0f;
    private float takeOverTimer = 1f;

    private void Awake()
    {
        material = townObject.GetComponent<Renderer>().material;
        GameController.Instance.towns.Add(this);
    }

    // Use this for initialization
    void Start () {
        SetColor();
	}
	
	// Update is called once per frame
	void Update () {
        takeOverTime += Time.deltaTime;

        /*if (ownedBy == 1 && !NoEnemies() && NoPlayers())
        {
            EnemyTakeOver();
        }else if(ownedBy == 2 && !NoPlayers() && NoEnemies())
        {
            PlayerTakeOver();
        }*/
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && ownedBy != 2 && takeOverTime > takeOverTimer)
        {
            takeOverTime = 0f;
            //Debug.Log("Enemy on base!");
            //enemyCount++;
            //enemiesOccupying[other.gameObject.GetComponent<EnemyPlatoonController>().platoonNum] = true;
            EnemyTakeOver();
        }
        /*else if (other.tag == "Enemy" && !enemiesOccupying[other.gameObject.GetComponent<EnemyPlatoonController>().platoonNum])
        {
            //Debug.Log("Enemy on base B!");
            enemyCount++;
            enemiesOccupying[other.gameObject.GetComponent<EnemyPlatoonController>().platoonNum] = true;
        }*/

        if ((other.tag == "Player" || other.tag == "UncontrolledPlayer") && ownedBy != 1 && takeOverTime > takeOverTimer)
        {
            takeOverTime = 0f;
            //Debug.Log("Adding Player " + playerCount + " " + other.gameObject.GetComponent<PlayerPlatoonController>().platoonNum);
            //playerCount++;
            // playersOccupying[other.gameObject.GetComponent<PlayerPlatoonController>().platoonNum] = true;
            PlayerTakeOver();            
        }
        /*else if((other.tag == "Player" || other.tag == "UncontrolledPlayer") && !playersOccupying[other.gameObject.GetComponent<PlayerPlatoonController>().platoonNum])
        {
            Debug.Log("Adding PlayerB " + playerCount);
            playerCount++;
            playersOccupying[other.gameObject.GetComponent<PlayerPlatoonController>().platoonNum] = true;
        }*/
        SetColor();
    }

    /*void OnTriggerExit(Collider other)
    {
        Debug.Log("On Trigger Exit " + other.tag);
        if (other.tag == "Enemy")
        {
            enemyCount++;
            enemiesOccupying[other.gameObject.GetComponent<EnemyPlatoonController>().platoonNum] = false;
        }

        if ((other.tag == "Player" || other.tag == "UncontrolledPlayer"))
        {
            Debug.Log("Removing Player " + playerCount + " " + other.gameObject.GetComponent<PlayerPlatoonController>().platoonNum);
            playerCount--;
            playersOccupying[other.gameObject.GetComponent<PlayerPlatoonController>().platoonNum] = false;
        }
    }*/
    

    private void SetColor()
    {
        if(ownedBy == 0)
        {
            material.color = Color.white;
        }else if(ownedBy == 1)
        {
            material.color = Color.cyan;
        }
        else
        {
            material.color = Color.red;
        }
    }

    /*private bool NoPlayers()
    {
        
            //Debug.Log(playerOccupants.Count);
            if(playerCount <= 0)
            {
            return true;
            }

        return false;
    }

    private bool NoEnemies()
    {
        //Debug.Log(playerOccupants.Count);
        if (enemyCount <= 0)
        {
            return true;
        }

        return false;
    }*/

    private void EnemyTakeOver()
    {
        if (ownedBy == 1)
        {
            LevelController.Instance.playerControlledTowns--;
        }

        ownedBy = 2;
        if (isPlayerBase)
        {
            LevelController.Instance.playerBaseCaptured = true;
        }
    }

    private void PlayerTakeOver()
    {
        ownedBy = 1;
        LevelController.Instance.playerControlledTowns++;

        if (isEnemyBase)
        {
            LevelController.Instance.enemyBaseCaptured = true;
        }
    }

    public int OwnedByWho()
    {
        return ownedBy;
    }

    public void SetOwnership(int owned)
    {
        if(owned == 0)
        {
            ownedBy = 0;
        }else if(owned == 1)
        {
            PlayerTakeOver();
        }
        else
        {
            ownedBy = 2;
        }
        SetColor();
    }
}
