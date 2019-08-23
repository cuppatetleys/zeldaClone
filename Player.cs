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
    //private Animator anim;        //22/02/2019 - Just coming back into Unity again after a long time of not doing much, so will leave anim until later


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //22/02/2019 - we use GetAxisRaw because it only returns 3 states: positive, negative or zero - which is all we need for the time being.
        //22/09/2019 (20 mins later) decided to change to GetAxis so I could control the speed at which the character moves to a greater degree.
        Vector2 movement_vector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        rb2d.MovePosition(rb2d.position + movement_vector * Time.deltaTime * move_speed);

        //firing arrow
        if (Input.GetKeyDown(KeyCode.Space))
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
