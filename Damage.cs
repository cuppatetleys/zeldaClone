using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public float defaultDamageValue = 20f;
    private int damageValues[2] = {20, 40};

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Enemy>() != null)
        {
            DoDamage(collision);
        }

        return;
        
    }
    void DoDamage(Collider2D collision)
    {
        if (collision.CompareTag("enemy") && !collision.GetComponent<Enemy>().enemyIsInvincible)
        {
            switch (this.tag)
            {
                case ("arrow"):
                    collision.GetComponent<Enemy>().hit_points -= defaultDamageValue;
                    Destroy(this.gameObject);
                    break;
                case ("stick"):
                    collision.GetComponent<Enemy>().hit_points -= defaultDamageValue;
                    break;
                default:
                    collision.GetComponent<Enemy>().hit_points -= defaultDamageValue;
                    break;
            }
        }
    }
}
