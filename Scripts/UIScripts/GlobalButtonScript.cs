using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalButtonScript : MonoBehaviour {

	public int buttonNum;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(GlobalMapController.Instance.GetNextLocation() == buttonNum ||
            !GameController.Instance.difficulty && GlobalMapController.Instance.GetNextLocation() > buttonNum)
        {
            /*if((buttonNum == 12 && !GlobalMapController.Instance.storyACompleted && GlobalMapController.Instance.GetNextLocation() == 12) ||
               (buttonNum == 16 && !GlobalMapController.Instance.storyBCompleted && GlobalMapController.Instance.GetNextLocation() == 16))
            {
                Hide();
            }
            else
            {*/
                Show();
            //}
        }
        else
        {
           /* if (buttonNum == 30 && ((!GlobalMapController.Instance.storyACompleted && GlobalMapController.Instance.GetNextLocation() == 12) ||
                (!GlobalMapController.Instance.storyBCompleted && GlobalMapController.Instance.GetNextLocation() == 16)))
            {
                Show();
            }
            else
            {*/
                Hide();
           // }
        }
	}

	public void Clicked(){
		GameObject g = this.gameObject;
		GlobalMapController.Instance.MapButtonClicked (buttonNum, g);
	}

    private void Hide()
    {
        gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 100);

    }

    private void Show()
    {
        gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }
}



