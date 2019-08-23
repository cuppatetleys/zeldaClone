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

    float previous_frame_hp;

    public bool enemyIsInvincible = false;

    public float enemyInvulnerability = 0.3f;


    private void Update()
    {
        hit_points_string = hit_points.ToString();
        hit_points_text.text = hit_points_string;
        hit_points_text.transform.position = this.transform.position + new Vector3(0, x_offset, 0);

        if (hit_points <= 0)
        {
            Destroy(hit_points_text);
            Destroy(this.gameObject);
        }



        previous_frame_hp = hit_points;
    }
}
