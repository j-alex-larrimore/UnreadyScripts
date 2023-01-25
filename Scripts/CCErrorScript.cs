using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCErrorScript : MonoBehaviour {

    public GameObject textObject;

    private float showTime = 2.0f;
    private float showTimer = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        showTimer += Time.deltaTime;

        if(showTimer >= showTime)
        {
            ErrorMessageTimeOver();
        }
	}

    public void ErrorMessageTimeOver()
    {
        showTimer = 0f;
        textObject.SetActive(false);
    }
}
