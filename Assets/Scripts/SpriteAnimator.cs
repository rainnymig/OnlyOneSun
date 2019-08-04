using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimator : MonoBehaviour
{

    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private float period = 0.2f;

    private SpriteRenderer sr;
    private float accumulateTime = 0;
    private int spriteCount;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        spriteCount = sprites.Count;
    }

    private void Update()
    {
        accumulateTime += Time.deltaTime;
        if(accumulateTime > period)
        {
            accumulateTime = 0;
            if(sr != null && spriteCount > 0)
            {
                sr.sprite = sprites[Random.Range(0, spriteCount - 1)];
            }
        }
    }
}
