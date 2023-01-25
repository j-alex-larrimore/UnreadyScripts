using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatoonController : MovingObject {

    public int platoonNum;

    private bool isDead = false;
    public bool isHealing = true;

    public bool isSelected = false;

    public Vector3 targetLoc;

    public GameObject selectedMarker;

    //private bool paused = false;

    private float hoverTimer = 2f;
    private float hoverTime;
    private bool hovering = false;
    private BoxCollider bCollider;
    private bool containsLeader;

    // Use this for initialization
    void Start () {
        //GameController.Instance.playerPlatoons.
        targetLoc = gameObject.transform.position;
        bCollider = gameObject.GetComponent<BoxCollider>();
        bCollider.size = new Vector3(.5f, .5f, .5f);
    }
	
	// Update is called once per frame
	void Update () {
        if (!GameController.Instance.armyPause)
        {
            // if (isMoving && (gameObject.transform.position.x != targetLoc.x || gameObject.transform.position.z != targetLoc.z))
            if ((gameObject.transform.position.x != targetLoc.x || gameObject.transform.position.z != targetLoc.z) && !(LevelController.Instance.levelNum == 1))
            {
                //Debug.Log("GO: " + gameObject.transform.position + " TLoc: " + targetLoc);
                MoveTowards(new Vector3(targetLoc.x, gameObject.transform.position.y, targetLoc.z));
                animator.SetBool("isWalking", true);
               // bCollider.size = new Vector3(.3f, .3f, .3f);
            }
            else
            {
                //isMoving = false;
                animator.SetBool("isWalking", false);
                //bCollider.size = new Vector3(.8f, .8f, .8f);
            }

            if (hovering)
            {
                hoverTime += Time.deltaTime;
            }

            if (hovering && hoverTimer < hoverTime && !CanvasController.Instance.platoonInspectionPanel.activeInHierarchy)
            {
                ShowPlatoonInfoHover();
            }

            if ((isSelected || selectedMarker != null) && GameController.Instance.selectedPlatoon != this)
            {
                ClearSelected();
            }

            if (isHealing)
            {
                PlatoonHealing();
            }
        }
        else
        {
            MoveTowards(transform.position);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && !isDead && LevelController.Instance.playerPlatoons[platoonNum] != null &&
            LevelController.Instance.enemyPlatoons[other.GetComponent<EnemyPlatoonController>().GetPlatoonNum()] != null)
        {
            //GameController.Instance.StartBattle(GameController.Instance.playerPlatoons[platoonNum], GameController.Instance.enemyPlatoons[other.GetComponent<EnemyPlatoonController>().platoonNum]);
            if (!GameController.Instance.inBattle)
            {
                Debug.Log("Contact between " + LevelController.Instance.playerPlatoons[platoonNum].GetLeader().unitName + " and " +
                    LevelController.Instance.enemyPlatoons[other.GetComponent<EnemyPlatoonController>().GetPlatoonNum()].GetLeader().unitName);


                if (platoonNum == GameController.Instance.heroPlatoonNum)
                {
                    GameController.Instance.Fight(this, other.GetComponent<EnemyPlatoonController>());
                }
                else
                {
                    Debug.Log("Auto Fight! " + platoonNum + " HPN: " + GameController.Instance.heroPlatoonNum);
                    GameController.Instance.AutoFight(this, other.GetComponent<EnemyPlatoonController>());
                }
            }

        }

        if (other.tag == "Town")
        {
            isHealing = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Town")
        {
            isHealing = false;
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("Selected! " + ArmyData.Instance.platoons[platoonNum].GetLeader().unitName);
        isSelected = true;
        if (GameController.Instance.isPlatoonSelected)
        {
            if (GameController.Instance.selectedPlatoon != null) {
                GameController.Instance.selectedPlatoon.Deselect();
            }
            else
            {
                GameController.Instance.isPlatoonSelected = false;
            }
        }
        GameController.Instance.isPlatoonSelected = true;
        GameController.Instance.selectedPlatoon = this;
        CanvasController.Instance.selectedPlatoonText.text = ArmyData.Instance.platoons[platoonNum].GetLeader().unitName;
        if(selectedMarker != null)
        {
            Destroy(selectedMarker);
        }
        selectedMarker = Instantiate(GameController.Instance.selectedMarker, gameObject.transform);
        selectedMarker.gameObject.transform.position = new Vector3(selectedMarker.gameObject.transform.position.x, selectedMarker.gameObject.transform.position.y + 5, selectedMarker.gameObject.transform.position.z);
    }

    private void OnMouseEnter()
    {
        hovering = true;
    }

    private void OnMouseExit()
    {
        hovering = false;
        //CanvasController.Instance.Hideplat
        hoverTime = 0f;
    }

    private void ShowPlatoonInfoHover()
    {
        CanvasController.Instance.inspectingThroughHover = true;
        CanvasController.Instance.OpenPlatoonInspect(platoonNum);
    }

    private void PlatoonHealing()
    {
       
    }

    public void MoveUnit(Vector3 loc)
    {
        targetLoc = loc;

        float adjustMaxX = LevelController.Instance.maxX + 250;
        float adjustMinX = LevelController.Instance.minX + 250;
        float adjustMaxZ = LevelController.Instance.maxZ + 250;
        float adjustMinZ = LevelController.Instance.minZ + 250;

        if (targetLoc.x > adjustMaxX)
        {
            Debug.Log("OverX " + targetLoc.x);
            targetLoc = new Vector3(adjustMaxX, targetLoc.y, targetLoc.z);
        }else if(targetLoc.x < adjustMinX)
        {
            Debug.Log("UnderX " + targetLoc.x);
            targetLoc = new Vector3(adjustMinX, targetLoc.y, targetLoc.z);
        }

        if (targetLoc.z > adjustMaxZ)
        {
            Debug.Log("OverZ " + targetLoc.z);
            targetLoc = new Vector3(targetLoc.x, targetLoc.y, adjustMaxZ);
        }
        else if (targetLoc.z < adjustMinZ)
        {
            Debug.Log("UnderZ " + targetLoc.z);
            targetLoc = new Vector3(targetLoc.x, targetLoc.y, adjustMinZ);
        }
        //isMoving = true;
    }

    public void Deselect()
    {
        isSelected = false;
        CanvasController.Instance.selectedPlatoonText.text = "";
        if(selectedMarker != null)
        {
            Destroy(selectedMarker);
        }
    }

    public void ClearSelected()
    {
        if (selectedMarker != null)
        {
            Destroy(selectedMarker);
        }
        isSelected = false;
    }

    public int GetPlatoonNum()
    {
        return platoonNum;
    }

    public void StopMovement()
    {
        targetLoc = gameObject.transform.position;
    }
}
