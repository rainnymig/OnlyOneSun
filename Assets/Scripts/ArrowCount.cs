using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArrowCount : MonoBehaviour
{
    [SerializeField] private LevelManager.ArrowType arrowType;
    [SerializeField] private TextMeshProUGUI display;

    private void Update()
    {
        float arrowCount = LevelManager.Instance.getArrowCount(arrowType);
        if (display != null)
        {
            display.text = arrowCount.ToString();
        }
    }
}
