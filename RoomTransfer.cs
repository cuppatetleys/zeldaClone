using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMove : MonoBehaviour
{
    public float cameraChangeX;
    public float cameraChangeY;
    public Vector3 PlayerChange;
    private CameraMovement cam;

    private void Start()
    {
        cam = Camera.main.GetComponent<CameraMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            cam.minposition.x += cameraChangeX;
            cam.minposition.y += cameraChangeY;

            cam.maxposition.x += cameraChangeX;
            cam.maxposition.y += cameraChangeY;

            collision.transform.position += PlayerChange;
        }
    }
}
