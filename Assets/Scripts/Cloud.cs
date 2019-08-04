using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Cloud : MonoBehaviour
{

    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite icedSprite;

    public float CloudDrag = 8f;

    private float originalArrowDrag;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

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
        else if(collision.CompareTag("IceArrow"))
        {
            Debug.Log("ice arrow hits cloud");
            Destroy(collision.gameObject);
            if(icedSprite != null)
            {
                sr.sprite = icedSprite;
            }
            gameObject.tag = "IceCloud";
            rb.gravityScale = 1;
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
