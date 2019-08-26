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
        collision.GetComponent<Transform>().position = warpTarget.position;   
    }
}
