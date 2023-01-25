using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassDescriptionPanel : MonoBehaviour {

    public static ClassDescriptionPanel Instance;

    private int classNum;

    private float hoverTimer = 1.5f;
    private float hoverTime = 0f;
    private bool hovering = false;

    private bool currentClass = false;
    private int currentPanel = 0;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            DestroyImmediate(this);
            return;
        }

        Instance = this;
    }

    // Use this for initialization
    void Start () {
        Debug.Log("Class description panel start");
	}
	
	// Update is called once per frame
	void Update () {
        if (hovering)
        {
            hoverTime += Time.deltaTime;
        }

        if (hovering && hoverTimer < hoverTime)
        {
            HideClassDescriptions();
            ShowClassDescriptions();
        }

        if(CanvasController.Instance.humanClassChangePanel1.activeInHierarchy == false &&
            CanvasController.Instance.humanClassChangePanel2.activeInHierarchy == false &&
            CanvasController.Instance.humanClassChangePanel3.activeInHierarchy == false &&
            CanvasController.Instance.animalClassChangePanel.activeInHierarchy == false &&
            CanvasController.Instance.mythClassChangePanel.activeInHierarchy == false &&
            CanvasController.Instance.mythClassChangePanel2.activeInHierarchy == false &&
            CanvasController.Instance.unitInspectionPanel.activeInHierarchy == false)
        {
            StopClassHover();
        }
}

    public void ShowClassDescriptions()
    {
        CanvasController.Instance.classDescriptionCanvas.SetActive(true);
        CanvasController.Instance.classDescriptionPanel.SetActive(true);

        if (currentClass)
        {
            currentPanel = CanvasController.Instance.selectedUnit.GetCharClass();
        }
        else
        {
            currentPanel = classNum;
        }

        if (CanvasController.Instance.selectedUnit.GetCharType() == 2)
        {
            currentPanel += 59;
        }else if (CanvasController.Instance.selectedUnit.GetCharType() == 3)
        {
            currentPanel += 69;
        }

        CanvasController.Instance.classDescriptionPanels[currentPanel].SetActive(true);
    }

    public void HideClassDescription()
    {
        CanvasController.Instance.classDescriptionPanels[currentPanel].SetActive(false);
    }

    public void HideClassDescriptions()
    {
        CanvasController.Instance.classDescriptionPanel.SetActive(false);
        CanvasController.Instance.classDescriptionCanvas.SetActive(false);
        CanvasController.Instance.classDescriptionPanels[currentPanel].SetActive(false);
    }

    public void CurrentClassHover()
    {
        hovering = true;
        currentClass = true;
    }

    public void ButtonClassHover(int buttonNum)
    {
        hovering = true;
        currentClass = false;
        classNum = buttonNum;
    }

    public void StopClassHover()
    {
        hovering = false;
        HideClassDescription();
        hoverTime = 0f;
    }

    public void StopCurrentClassHover()
    {
        hovering = false;
        HideClassDescription();
        hoverTime = 0f;
    }
}
