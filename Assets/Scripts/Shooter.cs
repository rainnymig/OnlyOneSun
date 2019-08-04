using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private float shootForce = 100;
    [SerializeField] private float initialArrowOffset = 0.1f;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            ShootArrow();
        }
    }

    private void ShootArrow()
    {
        GameObject arrowInstance = Instantiate(arrowPrefab);
        arrowInstance.transform.position = transform.position + transform.right * initialArrowOffset;
        arrowInstance.transform.rotation = transform.rotation;
        Rigidbody2D arrowRb = arrowInstance.GetComponent<Rigidbody2D>();
        arrowRb.AddForce(arrowInstance.transform.right * shootForce);
    }
}
