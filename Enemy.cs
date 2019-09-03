using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float hit_points = 100f;
    public float x_offset = 5f;
    public Text hit_points_text;
    string hit_points_string;

    public bool enemyIsInvincible = false;
    public float invincibilityLength = 2f;
    public bool hit = false;


    private void Update()
    {
        //Debug.Log("Beginning of frame invincibility state: " + enemyIsInvincible + "\nHit = " + hit);

        hit_points_string = hit_points.ToString();
        hit_points_text.text = hit_points_string;
        hit_points_text.transform.position = this.transform.position + new Vector3(0, x_offset, 0);

        if (hit_points <= 0)
        {
            Destroy(hit_points_text);
            Destroy(this.gameObject);
        }

        if (hit)
        {
            hit = false;
            StartCoroutine(InvincibilityFrames());
        }
    }

    IEnumerator InvincibilityFrames()
    {
        enemyIsInvincible = true;
        yield return new WaitForSeconds(invincibilityLength);
        //play animation
        enemyIsInvincible = false;
    }
}
