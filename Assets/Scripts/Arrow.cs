using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Arrow : MonoBehaviour
{
    public int ReflexTimes = 3;
    public float velocityThreshold = 0.1f;
    public GameObject ArrowDestroyParEff;

    private Rigidbody2D rb;

    private float angle;
    private bool shouldSimulateRotation = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        
        if (shouldSimulateRotation)
        {
            Vector2 v = rb.velocity;
            angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {

            Vector2 collisionPoint = collision.GetContact(0).point;
            destroyArrow(collisionPoint);
        }
        
    }

    private void destroyArrow(Vector2 partEffPos)
    {
        GameObject destroyEffect = Instantiate(ArrowDestroyParEff);
        destroyEffect.transform.position = partEffPos;
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        LevelManager.Instance.RemoveFlyingArrow();
    }
}
