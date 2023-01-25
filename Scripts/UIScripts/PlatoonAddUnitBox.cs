using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatoonAddUnitBox : MonoBehaviour {

    private int buttonLocation;
    
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetLocation(int x)
    {
        buttonLocation = x;
    }

    public void ButtonClicked()
    {
        //Debug.Log("ADDBUTTON CLICKED");

        CanvasController.Instance.SelectUnitToAdd(buttonLocation);
    }
}
