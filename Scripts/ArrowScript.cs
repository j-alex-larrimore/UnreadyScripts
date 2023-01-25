using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour {

    public RectTransform mapTransform;
    
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(GlobalMapController.Instance.GetNextButton().GetComponent<RectTransform>().anchoredPosition.x + mapTransform.anchoredPosition.x, 0);

        if (gameObject.GetComponent<RectTransform>().anchoredPosition.x > 500)
        {
            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(500, 0);
        }

        if (gameObject.GetComponent<RectTransform>().anchoredPosition.x < -500)
        {
            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(-500, 0);
        }



        gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(gameObject.GetComponent<RectTransform>().anchoredPosition.x,
                    GlobalMapController.Instance.GetNextButton().GetComponent<RectTransform>().anchoredPosition.y + mapTransform.anchoredPosition.y);

            
        if(gameObject.GetComponent<RectTransform>().anchoredPosition.y > 385)
        {
            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(gameObject.GetComponent<RectTransform>().anchoredPosition.x,
                385);
        }
        

    }
}
