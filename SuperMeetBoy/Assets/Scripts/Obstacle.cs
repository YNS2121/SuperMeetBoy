using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("thorn"))
        {

        }
    }

            // Update is called once per frame
            void Update()
    {
        transform.Rotate(0, 0, 10);
    }
}
