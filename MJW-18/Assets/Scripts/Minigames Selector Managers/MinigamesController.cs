using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigamesController : MonoBehaviour
{
    CurtainsController curtainsController;
    TVController tvController;
    [SerializeField] private MinigameInfo[] minigames;
    private RoundTimeManager roundTimeManager;

    public bool IsOnMinigame = false;
    [SerializeField]
    public int actualLvl = 0;

    [SerializeField]
    private EventReference winSound, loseSound;

    // Start is called before the first frame update
    void Start()
    {
        curtainsController = FindObjectOfType<CurtainsController>();
        tvController = FindObjectOfType<TVController>();
        roundTimeManager = FindObjectOfType<RoundTimeManager>();
        IsOnMinigame = false;
    }

    public int getLevel()
    {
        return actualLvl;
    }

    public void activateMinigame(int id)
    {
        setMusic(id + 1);
        resetMinigames();
        makeTransition(minigames[id].texture);
        minigames[id].minigameObject.SetActive(true);
        IsOnMinigame = true;
    }

    public void deactivateMinigame(bool hasWin)
    {
        setMusic(0);
        makeTransition();
        resetMinigames();
        if (hasWin)
        {
            FMODUnity.RuntimeManager.PlayOneShot(winSound);
            roundTimeManager.addTime(300);
            roundTimeManager.addHP(100);
        }
        else
        {
            FMODUnity.RuntimeManager.PlayOneShot(loseSound);
            roundTimeManager.addTime(-100);
        }

        IsOnMinigame = false;
    }

    private void resetMinigames()
    {
        foreach (MinigameInfo minigame in minigames)
        {
            minigame.minigameObject.SetActive(false);
        }
    }

    private void makeTransition(RenderTexture render = null)
    {
        curtainsController.makeCurtainTransition();
        tvController.toggleTV(render);
    }

    public void endRound(bool hasWon)
    {
        setMusic(4);
        makeTransition();
        if (hasWon)
        {
            Debug.Log("Ha gando la ronda");
            if(actualLvl == 0)
            {
                SceneManager.LoadScene("GoodEnding");
            }
            else if(actualLvl == 1)
            {
                SceneManager.LoadScene("GoodEnding2");
            }
            else if(actualLvl == 2)
            {
                SceneManager.LoadScene("GoodEndingFinal");
            }
            actualLvl++;
        }
        else
        {
            Debug.Log("Ha perdido la ronda");
            SceneManager.LoadScene("BadEnding");
        }
    }
    private void setMusic(int val)
    {
        switch (val)
        {
            case 0:
                FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel("music-status", "main");
                break;
            case 1:
                FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel("music-status", "cruz");
                break;
            case 2:
                FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel("music-status", "mercadona");
                break;
            case 3:
                FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel("music-status", "gym");
                break;
            case 4:
                FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel("music-status", "pause");
                break;
            default:
                FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel("music-status", "main");
                break;
        }
    }
}


[Serializable]
public class MinigameInfo {
    public RenderTexture texture;
    public GameObject minigameObject;
}
