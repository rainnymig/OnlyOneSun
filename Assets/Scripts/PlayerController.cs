using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Range(0, 0.3f)] [SerializeField] private float movementSmoothing = 0.05f;
    [SerializeField] private float moveSpeed = 40;
    [SerializeField] private GameObject bowWrapper;

    private Rigidbody2D rb;
    private float horizontalMove = 0;
    private Vector2 velocity = Vector2.zero;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;

        Vector2 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(bowWrapper.transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        bowWrapper.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void FixedUpdate()
    {
        Vector2 targetVelocity = new Vector2(horizontalMove * Time.fixedDeltaTime * 10, rb.velocity.y);
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmoothing);
    }
}
