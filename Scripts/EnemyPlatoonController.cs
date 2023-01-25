using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlatoonController : MovingObject {

    public Vector2 preferredLocation;
    public int adjustX = 250;
    public int adjustY = 250;

    public int platoonNum;

    public bool isHealing;

    //private bool paused = false;

    private float hoverTimer = 2f;
    private float hoverTime;
    private bool hovering = false;

    private BoxCollider bCollider;

    // Use this for initialization
    void Start () {
        bCollider = gameObject.GetComponent<BoxCollider>();
        bCollider.size = new Vector3(.7f, .7f, .7f);
    }

    // Update is called once per frame
    void Update () {
        if (!GameController.Instance.armyPause)
        {
            if (gameObject.transform.position.x - adjustX != preferredLocation.x || gameObject.transform.position.z - adjustY != preferredLocation.y)
            {
                MoveTowards(new Vector3(preferredLocation.x + adjustX, gameObject.transform.position.y, preferredLocation.y + adjustY));
                animator.SetBool("isWalking", true);
                //bCollider.size = new Vector3(.3f, .3f, .3f);
            }
            else
            {
                animator.SetBool("isWalking", false);
                //bCollider.size = new Vector3(1.7f, 1.7f, 1.7f);
            }

            if (hovering)
            {
                hoverTime += Time.deltaTime;
            }

            if (hovering && hoverTimer < hoverTime && !CanvasController.Instance.platoonInspectionPanel.activeInHierarchy)
            {
                ShowPlatoonInfoHover();
            }

            if (isHealing)
            {
                PlatoonHealing();
            }
        }
        else
        {
            MoveTowards(transform.position);
        }
    }

    private void OnMouseEnter()
    {
        hovering = true;
    }

    private void OnMouseExit()
    {
        hovering = false;
        //CanvasController.Instance.Hideplat
        hoverTime = 0f;
    }

    private void ShowPlatoonInfoHover()
    {
        CanvasController.Instance.inspectingThroughHover = true;
        CanvasController.Instance.viewingEnemyPlatoons = true;
        CanvasController.Instance.OpenPlatoonInspect(platoonNum);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Town")
        {
            isHealing = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Town")
        {
            isHealing = false;
        }
    }

    private void PlatoonHealing()
    {

    }

    public int GetPlatoonNum()
    {
        return platoonNum;
    }
}
