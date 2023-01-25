using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScript : MonoBehaviour {

    private static int splashTime = 3;
    private float splashTimer = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(splashTimer >= splashTime)
        {
            gameObject.SetActive(false);
        }
        splashTimer += Time.deltaTime;
	}
}
