using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
       
    private Rigidbody2D rb2d;
    public float move_speed = 7f;
    public GameObject arrow;
    public float arrowSpeed = 7f;
    public GameObject stickSword;
    public float arrowDestroyDelay = 3f;

    //for warping
    public bool justWarped;

    public float stickDelay = 0.5f;

    bool arrowIsCoolingDown = false;
    bool fired = false;
    public float arrowCoolDownTime = 1f;
    Animator anim;
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
       
        Vector2 movement_vector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if(movement_vector != Vector2.zero)
        {
            anim.SetBool("isjogging", true);
            anim.SetFloat("input_x", movement_vector.x);
            anim.SetFloat("input_y", movement_vector.y);
        }
        else
        {
            anim.SetBool("isjogging", false);
        }

        rb2d.MovePosition(rb2d.position + movement_vector * Time.deltaTime * move_speed);

        if(fired)
        {
            fired = false;
            StartCoroutine(ArrowCoolDown());
        }

        //firing arrow
        if (Input.GetKeyDown(KeyCode.Space) && !arrowIsCoolingDown)
        {
            FireArrow(arrow);
        }
    }
    
    void FireArrow(GameObject arrow)
    {
        Quaternion rotation = Quaternion.Euler(0, 0, 0);
        GameObject clone;
        Rigidbody2D arrow_rigidbody;
        clone = Instantiate(arrow, this.rb2d.position, rotation);
        arrow_rigidbody = clone.GetComponent<Rigidbody2D>();
        arrow_rigidbody.AddForce(new Vector2(0, arrowSpeed));
        Destroy(clone, arrowDestroyDelay);
        fired = true;
                
    }

    IEnumerator ArrowCoolDown()
    {
        arrowIsCoolingDown = true;
        yield return new WaitForSeconds(arrowCoolDownTime);
        arrowIsCoolingDown = false;
    }
}
