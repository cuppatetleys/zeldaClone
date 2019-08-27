using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float smoothing = 0.6f;
    public Vector2 maxposition;
    public Vector2 minposition;

    void LateUpdate()
    {
        if(transform.position != target.position)
        {
            
            Vector3 transformPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

            transformPosition.x = Mathf.Clamp(transformPosition.x, minposition.x, maxposition.x);
            transformPosition.y = Mathf.Clamp(transformPosition.y, minposition.y, maxposition.y);

            transform.position = Vector3.Lerp(transform.position, transformPosition, smoothing);


        }
    }
}
