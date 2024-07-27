using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigamesController : MonoBehaviour
{
    CurtainsController curtainsController;
    TVController tvController;
    [SerializeField] private MinigameInfo[] minigames;

    public bool IsOnMinigame = false;

    // Start is called before the first frame update
    void Start()
    {
        curtainsController = FindObjectOfType<CurtainsController>();
        tvController = FindObjectOfType<TVController>();
        IsOnMinigame = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            activateMinigame(0);
        }
    }

    public void activateMinigame(int id) {
        resetMinigames();
        Debug.Log(minigames[id].texture);
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
            Debug.Log("GANASTE");
        }
        else {
            Debug.Log("PERDISTE");
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
}

[Serializable]
public class MinigameInfo {
    public RenderTexture texture;
    public GameObject minigameObject;
}
