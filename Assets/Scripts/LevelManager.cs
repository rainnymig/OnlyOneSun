using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{

    public enum LevelState
    {
        RUNNING,
        WIN,
        FAILED,
    }

    public enum ArrowType
    {
        normal,
        ice,
    }

    [SerializeField] private List<GameObject> suns;
    [SerializeField] private int normalArrowCount;
    [SerializeField] private int iceArrowCount;
    [SerializeField] private string nextLevel;
    [SerializeField] private string thisLevel;
    [Range(0, 1)][SerializeField] private float initialHeatLevel = 0.5f;
    [SerializeField] private float fullHeatTime = 30;

    [SerializeField] private TextMeshProUGUI winTextDisplay;
    [SerializeField] private TextMeshProUGUI failTextDisplay;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject failPanel;

    //  only check result when all arrows are settled
    private bool shouldCheckResult = true;
    private LevelState levelState = LevelState.RUNNING;
    private float arrowInTheAirCount = 0;
    private float heatMeter;
    private float heatTimer;
    private string winText = "you made the world a better place";
    private string failText;

    private UnityEvent onWinEvent;
    private UnityEvent onFailEvent;


    private void Start()
    {
        if(onWinEvent == null)
        {
            onWinEvent = new UnityEvent();
        }
        onWinEvent.AddListener(onWin);
        if(onFailEvent == null)
        {
            onFailEvent = new UnityEvent();
        }
        onFailEvent.AddListener(onFail);


        heatMeter = initialHeatLevel;
        heatTimer = fullHeatTime * initialHeatLevel;
        if (shouldCheckResult)
        {
            checkResult();
        }
    }

    private void Update()
    {
        heatTimer += Time.deltaTime;
        heatMeter = Mathf.Clamp(heatTimer / fullHeatTime, 0, 1);
        if(heatMeter == 1)
        {
            failText = "it is too late. the world burns.";
            onFailEvent.Invoke();
        }
    }

    public float GetHeatLevel()
    {
        return heatMeter;
    }

    public void SetHeatLevel(float level)
    {
        heatMeter = Mathf.Clamp(level, 0, 1);
        heatTimer = fullHeatTime * heatMeter;
    }

    public void AddHeatLevel(float level)
    {
        SetHeatLevel(heatMeter + level);
    }

    public void KillSun(GameObject sun)
    {
        suns.Remove(sun);
    }

    public void PlayerDie()
    {
        levelState = LevelState.FAILED;
        failText = "you died from falling.";
        onFailEvent.Invoke();
    }

    public int getArrowCount(ArrowType type)
    {
        switch (type)
        {
            case ArrowType.normal:
                return normalArrowCount;
            case ArrowType.ice:
                return iceArrowCount;
            default:
                return 0;
        }
    }

    public void AddFlyingArrow(ArrowType type)
    {
        ++arrowInTheAirCount;
        switch (type)
        {
            case ArrowType.normal:
                --normalArrowCount;
                break;
            case ArrowType.ice:
                --iceArrowCount;
                break;
            default:
                break;
        }
        shouldCheckResult = false;
    }

    public void RemoveFlyingArrow()
    {
        if(arrowInTheAirCount > 0)
        {
            --arrowInTheAirCount;
        }
        if(arrowInTheAirCount == 0)
        {
            shouldCheckResult = true;
            checkResult();
        }
    }

    private void checkResult()
    {
        if (suns.Count > 1)
        {
            if(normalArrowCount + iceArrowCount > 0)
            {
                levelState = LevelState.RUNNING;
            }
            else
            {
                levelState = LevelState.FAILED;
                failText = "you run out of arrows.";
                onFailEvent.Invoke();
            }
        }
        else if (suns.Count == 1)
        {
            levelState = LevelState.WIN;
            onWinEvent.Invoke();
        }
        else
        {
            levelState = LevelState.FAILED;
            failText = "the world falls into eternal darkness.";
            onFailEvent.Invoke();
        }
    }

    public void ReloadThisLevel()
    {
        if (thisLevel != null)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(thisLevel);
        }
    }

    public void LoadNextLevel()
    {
        if(nextLevel != null)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(nextLevel);
        }
    }
    
    public void ExitToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }


    private void onWin()
    {
        Debug.Log("win!!!");
        displayWinPanel();
    }

    private void onFail()
    {
        Debug.Log("fail!!!");
        displayFailPanel();
    }

    private void displayWinPanel()
    {
        winTextDisplay.text = winText;
        winPanel.SetActive(true);
        Time.timeScale = 0;
    }

    private void hideWinPanel()
    {
        Time.timeScale = 1;
        winPanel.SetActive(false);
    }
    private void displayFailPanel()
    {
        failTextDisplay.text = failText;
        failPanel.SetActive(true);
        Time.timeScale = 0;
    }

    private void hideFailPanel()
    {
        Time.timeScale = 1;
        failPanel.SetActive(false);
    }

}
