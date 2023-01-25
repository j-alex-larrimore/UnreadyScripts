using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassChangeButton : MonoBehaviour {

    public int classNum;
    //public GameObject classDetails;
    public Text classText;
   /* private float hoverCounter = 0f;
    private float hoverTime = 1f;
    private bool hovering = false;*/
    
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        /*if (hovering)
        {
            hoverCounter += Time.deltaTime;
            if(hoverCounter > hoverTime)
            {
                StartHover();
            }
        }*/

        //classText.text = "" + CanvasController.Instance.selectedUnit.GetLevel(classNum);
       if(CanvasController.Instance.selectedUnit.GetLevel(classNum) == 10 && CanvasController.Instance.selectedUnit.GetCharType() == 1 ||
            CanvasController.Instance.selectedUnit.GetLevel(classNum) == 30 && CanvasController.Instance.selectedUnit.GetCharType() > 1)
        {
            SetMaxText();
        }
        else
        {
            //Debug.Log("Setting normal text " + CanvasController.Instance.selectedUnit.GetCharType() + " " + CanvasController.Instance.selectedUnit.GetLevel(classNum));
            SetNormalText();
        }
        

    }

    public void SetMaxText()
    {
        classText.color = new Color32(255, 135, 0, 255);
    }

    public void SetNormalText()
    {
        classText.color = Color.white;
    }

    public void ChangeClass()
    {
        Debug.Log("New Class: " + classNum);
        
        CanvasController.Instance.ClassChange(classNum);
    }

    public void StartHover()
    {
        //hovering = true;
        ClassDescriptionPanel.Instance.ButtonClassHover(classNum);
    }

    public void StopHover()
    {
        //hovering = false;
        ClassDescriptionPanel.Instance.StopClassHover();
        //Debug.Log("Mouse Exit: " + classNum);
    }
}
