using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UncontrolledPlayerController : MovingObject {

    public Vector2 preferredLocation;
    
    public int adjustX;
    public int adjustY;
    public bool isIgnavumLevel1 = false;

    private bool isDead = false;
   

    // Use this for initialization
    void Start () {
		
	}

    void Update()
    {
        if (gameObject.transform.position.x - adjustX != preferredLocation.x || gameObject.transform.position.z - adjustY != preferredLocation.y)
        {
            MoveTowards(new Vector3(preferredLocation.x + adjustX, gameObject.transform.position.y, preferredLocation.y + adjustY));
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        Level1IgnavumUpdates();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && !isDead)
        {
            isDead = true;
            LevelController.Instance.playerUnitCount--;
            Destroy(gameObject);
        }
    }

    private void Level1IgnavumUpdates()
    {
        if (LevelController.Instance.playerUnitCount < 3 && isIgnavumLevel1)
        {
            ignavumRetreat();
        }

        if (isIgnavumLevel1 && gameObject.transform.position.z - adjustY > 240)
        {
            LevelController.Instance.playerUnitCount--;
            StoryController.Instance.ShowStory(34);
            Destroy(gameObject);
        }
    }

    private void ignavumRetreat()
    {
        preferredLocation = new Vector2(0, 250);
    }

    
}
