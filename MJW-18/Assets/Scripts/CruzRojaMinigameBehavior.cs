using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class CruzRojaMinigameBehavior : MonoBehaviour
{
    public MinigamesController minigamesController;
    public Transform OllaTransform;
    public Transform CameraTransform;
    public Transform PuertaTransform;
    public Scrollbar ScrollbarBalanceo;
    public Scrollbar ScrollbarInputJugador;
    public float LoopTime;
    public float HorizontalMovementScale;
    public float VerticalMovementScale;
    public float CharacterRunningSpeed;
    public float[] ScrollbarSizeByDifficulty;
    public float[] ScrollbarMaxSpeedByDifficulty;
    public int DifficultyLevel = 0;
    private Vector3 sinoidalMovement;
    [SerializeField] [Range(0f, 10f)] private float winRange;

    private Vector3 initialCameraPosition = new Vector3(0.0f,0.0f,0.0f);
    private float timeOutsideRange = 0.0f;
    public float timeLimitOutside = 0.8f;
    private int timesOutsideRange = 0;

    // Start is called before the first frame update
    void Start()
    {
        minigamesController = FindObjectOfType<MinigamesController>();
        sinoidalMovement = new Vector3();

        initialCameraPosition = CameraTransform.position;
        timeOutsideRange = 0.0f;
        timesOutsideRange = 0;

        //Spawn position for the scrollbar
        //balanceo can be random, input msut be inside balanceo but also a little randomized

        float startingScrollbarPosition = UnityEngine.Random.Range(0.0f, 1.0f);

        if(ScrollbarBalanceo == null)
        {
            ScrollbarBalanceo = GameObject.Find("ScrollbarBalanceo").GetComponent<Scrollbar>();
        }

        if(ScrollbarInputJugador == null)
        {
            ScrollbarInputJugador = GameObject.Find("ScrollbarInputJugador").GetComponent<Scrollbar>();
        }

        ScrollbarBalanceo.value = startingScrollbarPosition;
        ScrollbarBalanceo.size = ScrollbarSizeByDifficulty[DifficultyLevel];
        ScrollbarInputJugador.value = startingScrollbarPosition - (ScrollbarBalanceo.size/2.0f) + UnityEngine.Random.Range(0.0f, ScrollbarBalanceo.size);

    }

    private void Reset()
    {
        CameraTransform.position = initialCameraPosition;
        Start();
    }


    // Update is called once per frame
    void Update()
    {
        OllaAnimation();
        ScrollbarUpdate();
        PlayerInput();
        FailureLogic();
    }

    void OllaAnimation()
    {
        sinoidalMovement = new Vector3(
            Mathf.Sin(Time.realtimeSinceStartup * (1.0f/LoopTime)) * HorizontalMovementScale,
            -Mathf.Abs(Mathf.Sin(Time.realtimeSinceStartup * (1.0f/LoopTime))) * VerticalMovementScale,
            OllaTransform.localPosition.z);
        OllaTransform.localPosition = sinoidalMovement;

        CameraTransform.localPosition += new Vector3(0.0f, 0.0f, Time.deltaTime * CharacterRunningSpeed);
    }


    float nextTargetValue = -1.0f;
    float nextTargetSpeed = -1.0f;
    float currentSpeed = 0.0f;
    void ScrollbarUpdate()
    {
        if(nextTargetValue == -1.0f || (ScrollbarBalanceo.value < nextTargetValue+0.05 && ScrollbarBalanceo.value > nextTargetValue-0.05f))
        {
            //new target value
            do{
                nextTargetValue = UnityEngine.Random.Range(0.0f, 1.0f);
            }while(Mathf.Abs(ScrollbarBalanceo.value - nextTargetValue) < 0.25f);
            
            nextTargetSpeed =  0.001f * (float)UnityEngine.Random.Range(0, 3);
            currentSpeed = -currentSpeed * 0.25f;
        }

        float maxSpeed = ScrollbarMaxSpeedByDifficulty[DifficultyLevel] + nextTargetSpeed;
        currentSpeed += Time.deltaTime * Mathf.Abs(currentSpeed - maxSpeed) * 0.2f;
        currentSpeed = Mathf.Clamp(currentSpeed, -100.0f, maxSpeed);
        ScrollbarBalanceo.value += currentSpeed * MathF.Sign(nextTargetValue - ScrollbarBalanceo.value) + UnityEngine.Random.Range(-0.001f, 0.001f);
    }

    public float playerCursorSpeed = 0.002f;
    void PlayerInput()
    {
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.Mouse1))
        {
            ScrollbarInputJugador.value += playerCursorSpeed * Time.deltaTime;    
        }
        else if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.Mouse0))
        {
            ScrollbarInputJugador.value -= playerCursorSpeed * Time.deltaTime;        
        }
    }


    void FailureLogic()
    {
        if(timeOutsideRange > timeLimitOutside)
        {
            Reset();
            minigamesController.deactivateMinigame(false);
        }


        if(winRange > Vector3.Distance(CameraTransform.position, PuertaTransform.position))
        {
            Reset();
            minigamesController.deactivateMinigame(true);
        }

        float deltaSize = ScrollbarBalanceo.size / 2.0f; 
        if(ScrollbarInputJugador.value > ScrollbarBalanceo.value+deltaSize 
        || ScrollbarInputJugador.value < ScrollbarBalanceo.value-deltaSize)
        {
            timeOutsideRange += Time.deltaTime;
            //Debug.Log("Outside Range!!!");
        }
        else
        {
            if(timeOutsideRange > 0.1f) timesOutsideRange++;
            timeOutsideRange = 0.0f;
            //Debug.Log("Inside Range . . .");
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(PuertaTransform.position, winRange);
    }
}
