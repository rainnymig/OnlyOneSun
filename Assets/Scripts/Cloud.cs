using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{

    public float CloudDrag = 8f;

    private float originalArrowDrag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Arrow"))
        {
            Debug.Log("arrow hits cloud");
            GameObject arrowObj = collision.gameObject;
            Rigidbody2D arrowRb = arrowObj.GetComponent<Rigidbody2D>();
            originalArrowDrag = arrowRb.drag;
            arrowRb.drag = CloudDrag;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Arrow"))
        {
            Debug.Log("arrow leaves cloud");
            GameObject arrowObj = collision.gameObject;
            Rigidbody2D arrowRb = arrowObj.GetComponent<Rigidbody2D>();
            arrowRb.drag = originalArrowDrag;
        }
    }
}
