using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Player state machine
public enum PlayerState
{
    walk,
    attack,
    interact,
    stagger,
    idle
}

public class Player : MonoBehaviour
{
    public PlayerState currentState;
    private Rigidbody2D rb2d;
    public float move_speed = 7f;
    public GameObject arrow;
    public float arrowSpeed = 7f;
    public GameObject stickSword;
    public float arrowDestroyDelay = 3f;

    public float stickDelay = 0.3f;

    bool arrowIsCoolingDown = false;
    bool fired = false;
    public float arrowCoolDownTime = 1f;
    Animator anim;

    private Vector3 change;
    
    
    void Start()
    {
        currentState = PlayerState.walk;
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //to ensure that attacks face the correct direction even if the player starts and there is no input
        anim.SetFloat("input_x", 0f);
        anim.SetFloat("input_y", -1f);
    }

    void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

        if(Input.GetButtonDown("attack") && currentState != PlayerState.attack)
        {
            StartCoroutine(AttackCo());
        }
        else if (currentState == PlayerState.walk)
        {
            UpdateAnimationAndMove();
        }
        //old walking and animation code, discarded 03/09/2019
        //Vector2 movement_vector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));   
        //rb2d.MovePosition(rb2d.position + movement_vector * Time.deltaTime * move_speed);

        if (fired)
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

    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            anim.SetBool("isjogging", true);
            anim.SetFloat("input_x", change.x);
            anim.SetFloat("input_y", change.y);
        }
        else
        {
            anim.SetBool("isjogging", false);
        }
    }

    void MoveCharacter()
    {
        change.Normalize();
        rb2d.MovePosition(transform.position + change * Time.deltaTime * move_speed);
    }

    private IEnumerator AttackCo()
    {
        anim.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        anim.SetBool("attacking", false);
        yield return new WaitForSeconds(stickDelay);
        currentState = PlayerState.walk;
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
