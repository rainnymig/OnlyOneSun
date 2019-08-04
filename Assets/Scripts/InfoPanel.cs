using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI winText;
    [SerializeField] private TextMeshProUGUI failText;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject failPanel;

    private bool panelDisplayed = false;
    
    private void Update()
    {
        
    }
}
