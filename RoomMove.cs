using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RoomMove : MonoBehaviour
{
    public float cameraChangeX;
    public float cameraChangeY;
    public Vector3 PlayerChange;
    private CameraMovement cam;

    //for Title card
    public bool needText;
    public string placeName;
    public GameObject text;
    public Text placeText;

    private void Start()
    {
        cam = Camera.main.GetComponent<CameraMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !collision.isTrigger)
        {
            cam.minposition.x += cameraChangeX;
            cam.minposition.y += cameraChangeY;

            cam.maxposition.x += cameraChangeX;
            cam.maxposition.y += cameraChangeY;

            collision.transform.position += PlayerChange;

            if(needText)
            {
                StartCoroutine(ShowPlaceName());
            }
        }
    }

    private IEnumerator ShowPlaceName()
    {
        text.SetActive(true);
        placeText.text = placeName;
        yield return new WaitForSeconds(4f);
        text.SetActive(false);
    }
}
