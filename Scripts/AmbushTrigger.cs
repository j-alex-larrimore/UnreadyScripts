using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbushTrigger : MonoBehaviour {

    public GameObject enemyAmbusher;
    public int used = 0;

    private void Awake()
    {
        GameController.Instance.ambush.Add(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" || other.tag == "UncontrolledPlayer")
        {
            enemyAmbusher.SetActive(true);
            used = 1;
            gameObject.GetComponent<Collider>().enabled = false;
            //Destroy(gameObject);
        }
    }

    public void SetUsed(int loadUsed)
    {
        used = loadUsed;
        if (used == 1)
        {
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }
}
