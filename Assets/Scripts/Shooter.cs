using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private GameObject iceArrowPrefab;
    [SerializeField] private float shootForce = 100;
    [SerializeField] private float initialArrowOffset = 0.1f;
    [SerializeField] private SpriteRenderer arrowSpriteRenderer;
    [SerializeField] private Sprite normalArrowSprite;
    [SerializeField] private Sprite iceArrowSprite;
    private LevelManager levelManager;


    private LevelManager.ArrowType arrowType = LevelManager.ArrowType.normal;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        displayArrow();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            ShootArrow();
        }
        if(Input.GetButtonDown("SwitchArrow"))
        {
            switchArrow();
        }
    }

    private void ShootArrow()
    {
        if(levelManager.getArrowCount(arrowType) > 0)
        {
            GameObject arrowInstance;
            if(arrowType == LevelManager.ArrowType.normal)
            {
                arrowInstance = Instantiate(arrowPrefab);
            }
            else
            {
                arrowInstance = Instantiate(iceArrowPrefab);
            }

            arrowInstance.transform.position = transform.position + transform.right * initialArrowOffset;
            arrowInstance.transform.rotation = transform.rotation;
            Rigidbody2D arrowRb = arrowInstance.GetComponent<Rigidbody2D>();
            arrowRb.AddForce(arrowInstance.transform.right * shootForce);

            levelManager.AddFlyingArrow(arrowType);

            displayArrow();
        }
    }

    private void displayArrow()
    {
        if (levelManager.getArrowCount(arrowType) > 0)
        {
            if(arrowType == LevelManager.ArrowType.normal)
            {
                arrowSpriteRenderer.sprite = normalArrowSprite;
            }
            else
            {
                arrowSpriteRenderer.sprite = iceArrowSprite;
            }
        }
        else
        {
            arrowSpriteRenderer.sprite = null;
        }
    }

    private void switchArrow()
    {
        if(arrowType == LevelManager.ArrowType.normal)
        {
            arrowType = LevelManager.ArrowType.ice;
        }
        else
        {
            arrowType = LevelManager.ArrowType.normal;
        }
        displayArrow();
    }
}
