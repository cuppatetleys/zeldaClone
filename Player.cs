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

    public float stickDelay = 0.5f;

    bool arrowIsCoolingDown = false;
    bool fired = false;
    public float arrowCoolDownTime = 1f;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //22/06/2019 - we use GetAxisRaw because it only returns 3 states: positive, negative or zero - which is all we need for the time being.
        //22/06/2019 (20 mins later) decided to change to GetAxis so I could control the speed at which the character moves to a greater degree.
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

    IEnumerator SlashStick(GameObject stickSword)
    {
        Quaternion rotation = Quaternion.Euler(0, 0, 0);
        GameObject clone;
        //Rigidbody2D arrow_rigidbody;
        clone = Instantiate(stickSword, this.rb2d.position, rotation);
        //arrow_rigidbody = clone.GetComponent<Rigidbody2D>();
        //arrow_rigidbody.AddForce(new Vector2(0, arrowSpeed));
        Destroy(clone, arrowDestroyDelay);
        yield return new WaitForSeconds(stickDelay);
    }
}
