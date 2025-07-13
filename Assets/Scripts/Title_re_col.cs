using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title_re_col : MonoBehaviour
{
    public Title_System title_System;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Title_Icon"))
        {
            title_System.re_obj();
            Destroy(collision.gameObject);
        }
    }
}
