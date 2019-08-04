using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArrowCount : MonoBehaviour
{
    [SerializeField] private LevelManager.ArrowType arrowType;
    [SerializeField] private TextMeshProUGUI display;
    private LevelManager levelManager;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void Update()
    {
        float arrowCount = levelManager.getArrowCount(arrowType);
        if (display != null)
        {
            display.text = arrowCount.ToString();
        }
    }
}
