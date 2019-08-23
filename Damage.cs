using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public float defaultDamageValue = 20f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Enemy>() != null)
        {
            DoDamage(collision);
            Destroy(this.gameObject);
        }

        
        
    }
    void DoDamage(Collider2D collision)
    {
        collision.GetComponent<Enemy>().hit_points -= defaultDamageValue;
    }
}
