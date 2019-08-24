using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour
{
    //need to change it so it's not a timer.
    public float warpCoolDownTime = 4f;
    public Transform warpTarget;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player"))
        {
            return;
        }
        else if (collision.GetComponent<Player>().justWarped)
        {
            return;
        }

        collision.GetComponent<Player>().justWarped = true;
        collision.GetComponent<Transform>().position = warpTarget.position;
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.GetComponent<Player>().justWarped = false;
    }
}
