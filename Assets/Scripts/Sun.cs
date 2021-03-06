﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Sun : MonoBehaviour
{
    [SerializeField] private float heatLevelDrop = 0.5f;
    private LevelManager levelManager;
    
    private Rigidbody2D rb;

    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Arrow") || collision.CompareTag("IceCloud"))
        {
            rb.gravityScale = 1;
            levelManager.KillSun(gameObject);
            levelManager.AddHeatLevel(-heatLevelDrop);
        }
    }
}
