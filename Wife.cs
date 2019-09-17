using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wife : EnemyAI
{
    private Rigidbody2D rb2d;
    public Transform target;
    public Transform homePosition;
    public float chaseRadius;
    public float attackRadius;
    public float moveSpeed = 6f;

    // Start is called before the first frame update
    void Start()
    {
        enemyDamage = 1f;
        currentState = EnemyState.idle;
        rb2d = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if(currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * 10 * Time.deltaTime);
                rb2d.MovePosition(temp);
                ChangeState(EnemyState.walk);
            }
        }
    }

    void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }
}
