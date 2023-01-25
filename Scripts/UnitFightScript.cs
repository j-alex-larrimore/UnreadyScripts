using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitFightScript : MonoBehaviour {

    GameObject targetHalo;
    GameObject selectHalo;

    public int unitSize = 1;

    public bool targeted = false;
    private int positionNum = -1;
    private BoxCollider bCollider;

	// Use this for initialization
	void Start () {
        bCollider = GetComponent<BoxCollider>();
        bCollider.size = new Vector3(1.5f, 1.5f, 1.5f);
        AddHalos();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void AddHalos()
    {
        if(unitSize == 1)
        {
            targetHalo = Instantiate(BattleController.Instance.smallTargetHalo, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
            selectHalo = Instantiate(BattleController.Instance.smallSelectHalo, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);            
        }
        else if(unitSize == 2)
        {
            targetHalo = Instantiate(BattleController.Instance.mediumTargetHalo, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.identity);
            selectHalo = Instantiate(BattleController.Instance.mediumSelectHalo, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.identity);
        }
        else if(unitSize == 3)
        {
            targetHalo = Instantiate(BattleController.Instance.largeTargetHalo, new Vector3(transform.position.x, transform.position.y + 3, transform.position.z), Quaternion.identity);
            selectHalo = Instantiate(BattleController.Instance.largeSelectHalo, new Vector3(transform.position.x, transform.position.y + 3, transform.position.z), Quaternion.identity);
        }
        targetHalo.transform.parent = gameObject.transform;
        selectHalo.transform.parent = gameObject.transform;
    }

    public void SetTargeted()
    {
        targetHalo.SetActive(true);        
        selectHalo.SetActive(false);
        targeted = true;
    }

    public void SetSelected()
    {
        //targetHalo.SetActive(false);
        BattleController.Instance.TargetSelected(positionNum);
        selectHalo.SetActive(true);
        targetHalo.SetActive(false);
    }

    public void NoHalos()
    {
        targeted = false;
        targetHalo.SetActive(false);
        selectHalo.SetActive(false);
    }

    void OnMouseDown()
    {
        if (targeted)
        {
            SetSelected();
            //BattleController.Instance.UnitSelected(positionNum);
        }
    }

    public void SetPositionNum(int num)
    {
        //Debug.Log("num! " + num);
        positionNum = num;
    }
}
