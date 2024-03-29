using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust = 20f;
    public float knockbackTime = 0.4f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy") || collision.CompareTag("Player"))
        {
            Rigidbody2D hitRB2D = collision.GetComponent<Rigidbody2D>();
            if(hitRB2D != null)
            {
                
                Vector2 difference = hitRB2D.transform.position - transform.position;
                difference = difference.normalized * thrust * 10;
                hitRB2D.AddForce(difference, ForceMode2D.Impulse);

                if(hitRB2D.CompareTag("Player"))
                {
                    hitRB2D.gameObject.GetComponent<Player>().currentState = PlayerState.stagger;
                }
                else
                {
                    hitRB2D.gameObject.GetComponent<EnemyAI>().currentState = EnemyState.stagger;
                }
                StartCoroutine(KnockCo(hitRB2D, collision.tag));
            }
        }
    }

    private IEnumerator KnockCo(Rigidbody2D hitRB2D, string tag)
    {
        DoDamage(hitRB2D.gameObject);
        yield return new WaitForSeconds(knockbackTime);
        hitRB2D.velocity = Vector2.zero;
        if(tag == "Player")
        {
            hitRB2D.gameObject.GetComponent<Player>().currentState = PlayerState.walk;
        }
        else if (tag == "enemy")
        {
            hitRB2D.gameObject.GetComponent<EnemyAI>().currentState = EnemyState.walk;
        }
        
    }

    private void DoDamage(GameObject target)
    {
        Player player = FindObjectOfType<Player>();
        EnemyAI enemy;

        if(target.CompareTag("Player"))
        {
            enemy = this.gameObject.GetComponent<EnemyAI>();
            player.playerHealth -= enemy.enemyDamage;
            //player.playerHealthSignal.Raise();
            Debug.Log(player.playerHealth);

        } else if (target.CompareTag("enemy"))
        {
            enemy = target.GetComponent<EnemyAI>();
            enemy.health -= player.playerDamageValue;
            Debug.Log(enemy.health);
            if(enemy.health <= 0)
            {
                target.SetActive(false);
            }
        }

    }
}
