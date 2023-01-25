using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDrag : MonoBehaviour {

    public float dragSpeed = 10;
    private Vector3 dragOrigin;

    public bool cameraDragging = true;

    public float outerLeft;
    public float outerRight;
    public float outerTop;
    public float outerBottom;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        float left = Screen.width * 0.2f;
        float right = Screen.width - (Screen.width * 0.2f);

        if (mousePosition.x < left)
        {
            cameraDragging = true;
        }
        else if (mousePosition.x > right)
        {
            cameraDragging = true;
        }






        if (cameraDragging)
        {

           if (Input.GetMouseButtonDown(0))
            {
                dragOrigin = Input.mousePosition;
                //return;
            }

            if (!Input.GetMouseButton(0))
            {
                return;
            }

            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
            Vector3 move = new Vector3(-pos.y * dragSpeed, 0, pos.x * dragSpeed);
            

            transform.Translate(move, Space.World);
           // Debug.Log("move.x " + move.x + " move.y " + move.y + " transformz " + this.transform.position.z);
            if(this.transform.position.z < outerRight)
            {
                transform.SetPositionAndRotation(new Vector3(this.transform.position.x, this.transform.position.y, outerRight), this.transform.rotation);
            }
            if (this.transform.position.z > outerLeft)
            {
                transform.SetPositionAndRotation(new Vector3(this.transform.position.x, this.transform.position.y, outerLeft), this.transform.rotation);
            }
            if (this.transform.position.x > outerTop)
            {
                transform.SetPositionAndRotation(new Vector3(outerTop, this.transform.position.y, this.transform.position.z), this.transform.rotation);
            }
            if (this.transform.position.x < outerBottom)
            {
                //Debug.Log("Outertop " + "pos.x " + pos.x + " pos.y " + pos.y + " outerRight " + outerRight + " transformz " + this.transform.position);
                transform.SetPositionAndRotation(new Vector3(outerBottom, this.transform.position.y, this.transform.position.z), this.transform.rotation);
            }
        }
    }
}
