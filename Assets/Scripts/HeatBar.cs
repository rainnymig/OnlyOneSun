using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class HeatBar : MonoBehaviour
{
    [SerializeField] private Color coldColor;
    [SerializeField] private Color hotColor;
    private LevelManager levelManager;

    private Image panelImage;

    private void Start()
    {
        panelImage = GetComponent<Image>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void Update()
    {
        float heatLevel = levelManager.GetHeatLevel();
        transform.localScale = new Vector3(1, heatLevel, 1);
        panelImage.color = Color.Lerp(coldColor, hotColor, heatLevel);

    }
}
