using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour {

    protected Animator animator;

    public float moveTime = 5f;
    private Rigidbody rigidBody;
    private BoxCollider boxCollider;
    private LayerMask collisionLayer;

    private bool multipleMoves = false;
    private Vector3 secondMoveLoc;

    private float distanceRounding = 7f;

    protected bool isMoving = false;

    private Vector3[] lastUpdatedPosition = new Vector3[3]; 

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
        rigidBody = GetComponent<Rigidbody>();
        collisionLayer = LayerMask.GetMask("Collision Layer");
    }

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected void MoveTowards(Vector3 targetLoc)
    {
        StopAllCoroutines();
        if (!PathBlocked(gameObject.transform.position, targetLoc))
        {
            //Debug.Log("Path not blocked, moving!");
            FaceTarget(targetLoc);
            StartCoroutine(SmoothMovementRoutine(targetLoc));
        }
    }

    protected IEnumerator SmoothMovementRoutine(Vector3 endPosition)
    {
        float remainingDistanceToEndPosition;
        do
        {
            remainingDistanceToEndPosition = (rigidBody.position - endPosition).sqrMagnitude;
            lastUpdatedPosition[2] = lastUpdatedPosition[1];
            lastUpdatedPosition[1] = lastUpdatedPosition[0];
            Vector3 updatedPosition = Vector3.MoveTowards(rigidBody.position, endPosition, moveTime * 5 * Time.deltaTime);
            lastUpdatedPosition[0] = updatedPosition;
            rigidBody.MovePosition(updatedPosition);
            // Debug.Log(" UP: " + updatedPosition + " RD: " + remainingDistanceToEndPosition);
            if (lastUpdatedPosition[0] == lastUpdatedPosition[1] && lastUpdatedPosition[0] == lastUpdatedPosition[2])
            {
                //Debug.Log("Teleporting " + " UP: " + updatedPosition + " RD: " + remainingDistanceToEndPosition + " DR " + distanceRounding);
                rigidBody.position = endPosition;
                //gameObject.transform.position = endPosition;
                if (remainingDistanceToEndPosition < distanceRounding)
                {
                    if (!multipleMoves)
                    {
                        isMoving = false;
                        //StopAllCoroutines();
                    }
                    else
                    {
                        FaceTarget(secondMoveLoc);
                        multipleMoves = false;
                        StartCoroutine(SmoothMovementRoutine(secondMoveLoc));
                    }

                }
            }
            yield return null;
        } while (remainingDistanceToEndPosition > distanceRounding);
        //Not sure this is needed
        if (multipleMoves)
        {
            FaceTarget(secondMoveLoc);
            multipleMoves = false;
            StartCoroutine(SmoothMovementRoutine(secondMoveLoc));
        }
    }

    private bool PathBlocked(Vector3 startPosition, Vector3 endPosition)
    {
        boxCollider.enabled = false;
        bool hit = Physics.Linecast(startPosition, endPosition, collisionLayer);
        boxCollider.enabled = true;

        if (hit)
        {

            return true;
        }

        return false;
    }

    private void FaceTarget(Vector3 endPosition)
    {
        transform.LookAt(endPosition);
    }

    private void ChokePoint(Vector3 startPosition, Vector3 endPosition)
    {
        foreach(Vector3 chokeLoc in LevelController.Instance.chokePoints)
        {
            boxCollider.enabled = false;
            bool hit = Physics.Linecast(startPosition, chokeLoc, collisionLayer);
            boxCollider.enabled = true;
            if (!hit)
            {

                return;
            }
        }
        Debug.Log("Choke Point Error");
    }
}
