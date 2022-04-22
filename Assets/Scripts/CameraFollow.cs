using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
 


    public void Update()
    {
        Follow();
    }

    public void Follow()
    {

        if (target.GetComponent<Rigidbody2D>().velocity.y >= 0)
        {
            transform.DOMoveY(target.position.y, 0.1f);
        }
        else
        {
            transform.DOMoveY(target.position.y, 2f);

        }
        // Vector3 position = transform.position ;
        // position.y = (target.position + offset).y;
        // transform.position = position;
        if (target.GetComponent<SpriteRenderer>().isVisible == true)
        {

        }
        else
        {
            Debug.Log("öldü");
        }

    }
}
