using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{

    //  the position to move to
    //  (in local space)
    [SerializeField] private Vector2 translateTarget = new Vector2(1, 0);
    [SerializeField] private float speed = 1;

    private Vector3 translateTargetWorld;
    private Vector3 originalPosition;
    private Rigidbody2D rb;

    private float neededMoveTime;
    private float accumulateTime = 0;


    private bool reachedTarget = false;

    private void Start()
    {
        translateTargetWorld = transform.position + new Vector3(translateTarget.x, translateTarget.y, 0);
        rb = GetComponent<Rigidbody2D>();
        originalPosition = transform.position;
        neededMoveTime = (translateTargetWorld - originalPosition).magnitude / speed;
    }

    private void FixedUpdate()
    {
        if (rb != null && rb.gravityScale != 0)
        {
            return;
        }
        accumulateTime += Time.fixedDeltaTime;
        if (!reachedTarget)
        {
            transform.position = Vector3.Lerp(originalPosition, translateTargetWorld, Mathf.Clamp(accumulateTime/neededMoveTime, 0, 1));
        }
        else
        {
            transform.position = Vector3.Lerp(translateTargetWorld, originalPosition, Mathf.Clamp(accumulateTime / neededMoveTime, 0, 1));
        }
        if (transform.position == translateTargetWorld)
        {
            reachedTarget = true;
            accumulateTime = 0;
        }
        else if (transform.position == originalPosition)
        {
            reachedTarget = false;
            accumulateTime = 0;
        }
    }
}
