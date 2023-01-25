using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectScript : MonoBehaviour {

    public float lifeTime = 3.5f;
    public float lifeTimer = 0f;
    
    // Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        
            lifeTimer += Time.deltaTime;
        

        if (lifeTime < lifeTimer)
        {
            Destroy(gameObject);
        }
    }


}
