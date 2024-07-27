using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GimnasioMamadoManager : MonoBehaviour
{
    public MinigamesController minigamesController;
    public TextMeshProUGUI burpeeInstructions;
    public Sprite[] sprites;
    public SpriteRenderer lladosRenderer;

    private int burpeeStage = 0;
    private int currentInputs = 0;
    private float elapsedTime = 0.0f;

    public float[] TimeLimitByDifficulty;
    public int[] InputsForWinByDifficulty;
    public int DifficultyLevel = 0;

    // Start is called before the first frame update
    void Start()
    {
        minigamesController = FindObjectOfType<MinigamesController>();
        burpeeStage = 0;
        lladosRenderer.sprite = sprites[burpeeStage];
        currentInputs = 0;
        elapsedTime = 0.0f;
        burpeeInstructions.text = "Alternate A and D !!!";
    }

    void Reset()
    {
        Start();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
        FailureLogic();
    }

    private bool AlternateInput = false;
    private bool AlternateInputDouble = false;
    void PlayerInput()
    {
        if(burpeeStage == 0)
        {
            if(AlternateInput && Input.GetKeyDown(KeyCode.A))
            {
                currentInputs++;
                AlternateInput = !AlternateInput;
            }
            if(!AlternateInput && Input.GetKeyDown(KeyCode.D))
            {
                currentInputs++;
                AlternateInput = !AlternateInput;
            }
        }
        else if(burpeeStage == 1)
        {
            if(Input.GetKeyDown(KeyCode.Space)) currentInputs++;
        }
        else if(burpeeStage == 2)
        {
            if(AlternateInput && AlternateInputDouble && Input.GetKeyDown(KeyCode.W))
            {
                currentInputs++;
                AlternateInput = !AlternateInput;
            }
            if(!AlternateInput && AlternateInputDouble && Input.GetKeyDown(KeyCode.A))
            {
                currentInputs++;
                AlternateInputDouble = !AlternateInputDouble;
                AlternateInput = !AlternateInput;
            }
            if(AlternateInput && !AlternateInputDouble && Input.GetKeyDown(KeyCode.S))
            {
                currentInputs++;
                AlternateInput = !AlternateInput;
            }
            if(!AlternateInput && !AlternateInputDouble && Input.GetKeyDown(KeyCode.D))
            {
                currentInputs++;
                AlternateInput = !AlternateInput;
                AlternateInputDouble = !AlternateInputDouble;
            }
        }
    }

    void FailureLogic()
    {

        lladosRenderer.sprite = sprites[currentInputs/3 % 3];

        if(burpeeStage == 0 && currentInputs > InputsForWinByDifficulty[DifficultyLevel] / 3)
        {
            burpeeStage = 1;
            Debug.Log("Stage 2!!!");
            burpeeInstructions.text = "Hit that Spacebar !!!";
        }
        if(burpeeStage == 1 && currentInputs > (InputsForWinByDifficulty[DifficultyLevel] / 3) * 2)
        {
            burpeeStage = 2;
            burpeeInstructions.text = "WASD . . . Spin to Win !!!";
        }
        
        if(currentInputs >= InputsForWinByDifficulty[DifficultyLevel])
        {
            Reset();
            minigamesController.deactivateMinigame(true);
        }

        if(elapsedTime > TimeLimitByDifficulty[DifficultyLevel])
        {
            Reset();
            minigamesController.deactivateMinigame(false);
        }

        elapsedTime += Time.deltaTime;        
    }
}
