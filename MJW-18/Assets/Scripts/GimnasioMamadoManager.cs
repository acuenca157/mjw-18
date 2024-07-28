using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GimnasioMamadoManager : MonoBehaviour
{
    public MinigamesController minigamesController;
    public Transform mainCamera;
    public TextMeshProUGUI burpeeInstructions;
    public Sprite[] sprites;
    public GameObject manos;
    public Vector3[] cameraPositions;
    public Vector3[] cameraRotation;
    public SpriteRenderer lladosRenderer;
    public float rotatingSpeed = 1.0f;


    private int burpeeStage = 0;
    private int currentInputs = 0;
    private float elapsedTime = 0.0f;

    public float[] TimeLimitByDifficulty;
    public int[] InputsForWinByDifficulty;
    public int DifficultyLevel = 0;
    private Vector3 previousRotationTarget;


    // Start is called before the first frame update
    void Start()
    {
        minigamesController = FindObjectOfType<MinigamesController>();
        DifficultyLevel = minigamesController.getLevel();
        burpeeStage = 0;
        lladosRenderer.sprite = sprites[burpeeStage];
        currentInputs = 0;
        elapsedTime = 0.0f;
        burpeeInstructions.text = "Alterna A y D !!!";
        previousRotationTarget = mainCamera.rotation.eulerAngles;

        mainCamera.localPosition = cameraPositions[0];
        mainCamera.rotation = Quaternion.Euler(cameraRotation[0]);
        manos.SetActive(false);
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

    private Vector3 cameraPositionTarget;
    private Vector3 cameraRotationTarget;
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

        UpdateGraphics();
    }

    float timeCountTransition = 0.0f;
    void UpdateGraphics(){

        int inputsToChangeSprite = 4;
        int modulatedCurrentInputs = currentInputs / inputsToChangeSprite;
        lladosRenderer.sprite = sprites[modulatedCurrentInputs % 3];
        cameraPositionTarget = cameraPositions[modulatedCurrentInputs % 3];
        cameraRotationTarget = cameraRotation[modulatedCurrentInputs % 3];

        if(modulatedCurrentInputs % 3 == 1)
        {
            manos.SetActive(true);
        }
        else
        {
            manos.SetActive(false);
        }

        //previousRotationTarget = cameraRotation[Mathf.Clamp((currentInputs/3 % 3) - 1, 0, 3)];

        timeCountTransition += Time.deltaTime;
        mainCamera.localPosition = Vector3.Lerp(mainCamera.localPosition, cameraPositionTarget, Time.deltaTime * 5.0f);
        mainCamera.rotation = Quaternion.RotateTowards(mainCamera.rotation, Quaternion.Euler(cameraRotationTarget), Time.deltaTime * rotatingSpeed);//Quaternion.Slerp(mainCamera.rotation, Quaternion.Euler(cameraRotationTarget), Time.deltaTime * 5.0f);
    }

    void FailureLogic()
    {
        if(burpeeStage == 0 && currentInputs > InputsForWinByDifficulty[DifficultyLevel] / 3)
        {
            burpeeStage = 1;
            burpeeInstructions.text = "Hittea la barrita !!!";
        }
        if(burpeeStage == 1 && currentInputs > (InputsForWinByDifficulty[DifficultyLevel] / 3) * 2)
        {
            burpeeStage = 2;
            burpeeInstructions.text = "WASD . . . Gira que te gira !!!";
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
