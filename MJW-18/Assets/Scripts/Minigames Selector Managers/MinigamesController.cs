using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigamesController : MonoBehaviour
{
    CurtainsController curtainsController;
    TVController tvController;
    [SerializeField] private MinigameInfo[] minigames;
    private RoundTimeManager roundTimeManager;

    public bool IsOnMinigame = false;
    [SerializeField]
    public int actualLvl = 0;

    // Start is called before the first frame update
    void Start()
    {
        curtainsController = FindObjectOfType<CurtainsController>();
        tvController = FindObjectOfType<TVController>();
        roundTimeManager = FindObjectOfType<RoundTimeManager>();
        IsOnMinigame = false;
    }

    public int getLevel() {
        return actualLvl;
    }

    public void activateMinigame(int id) {
        resetMinigames();
        makeTransition(minigames[id].texture);
        minigames[id].minigameObject.SetActive(true);
        IsOnMinigame = true;
    }

    public void deactivateMinigame(bool hasWin)
    {
        makeTransition();
        resetMinigames();
        if (hasWin)
        {
            roundTimeManager.addTime(300);
            roundTimeManager.addHP(100);
        }
        else {
            roundTimeManager.addTime(-100);
        }

        IsOnMinigame = false;
    }

    private void resetMinigames() {
        foreach (MinigameInfo minigame in minigames) { 
            minigame.minigameObject.SetActive(false);
        }
    }

    private void makeTransition(RenderTexture render = null) {
        curtainsController.makeCurtainTransition();
        tvController.toggleTV(render);
    }

    public void endRound(bool hasWon) {
        makeTransition();
        if (hasWon)
        {
            Debug.Log("Ha gando la ronda");
            actualLvl++;
        }
        else {
            Debug.Log("Ha perdido la ronda");
        }
    }
}

[Serializable]
public class MinigameInfo {
    public RenderTexture texture;
    public GameObject minigameObject;
}
