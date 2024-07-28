using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeComparisonMinigameManager : MonoBehaviour
{
    public MinigamesController minigamesController;
    public int DifficultyLevel = 0;
    public int[] NumberOfObjectsByDifficulty;
    public GameObject[] ListOfObjectsToSpawn;
    public float[] ListOfObjectsSizes;
    public Transform CursorHand;
    public float CursorSpeed;

    private GameObject[] ObjectsSpawned;

    
    public Camera MainCamera;

    private int correctObject = -1; 
    public float TimeLimit = 2.0f;
    private float timePassed = 0.0f;

    private Vector3 correctPosition;
    private bool blockInput = false;
    public float distanceCutoff;
    public float displayMessageTime = 0.5f;

    private Vector3[] positions;
    
    // Start is called before the first frame update
    void Start()
    {
        if(positions == null)
        {
            positions = new Vector3[6];
            positions[0] = new Vector3(  00.0f,-1.0f, 1.0f);
            positions[1] = new Vector3(  00.0f, 1.0f, 1.0f);
            positions[2] = new Vector3( -2.0f, -1.0f,1.0f);
            positions[3] = new Vector3(  2.0f, -1.0f,1.0f);
            positions[4] = new Vector3( -2.0f,  1.0f,1.0f);
            positions[5] = new Vector3(  2.0f,  1.0f,1.0f);
        }

        timePassed = 0.0f;
        blockInput = false;

        minigamesController = FindObjectOfType<MinigamesController>();
        DifficultyLevel = minigamesController.getLevel();


        //shuffle positions
        for (int t = 0; t < positions.Length; t++ )
        {
            Vector3 tmp = positions[t];
            int r = Random.Range(t, positions.Length);
            positions[t] = positions[r];
            positions[r] = tmp;
        }
        
        correctObject = Random.Range(0, NumberOfObjectsByDifficulty[DifficultyLevel]-1);
        
        if(ObjectsSpawned == null)
        {
            ObjectsSpawned = new GameObject[6];
            for(int i = 0; i < 6; ++i)
            {
                ObjectsSpawned[i] = (GameObject)Instantiate(ListOfObjectsToSpawn[i], positions[i], Quaternion.identity);
                ObjectsSpawned[i].transform.SetParent(MainCamera.transform);
                
                //DEBUG Sprites should have their won corrct size without setting scale
                ObjectsSpawned[i].transform.localScale = new Vector3(1.0f, ListOfObjectsSizes[i], 1.0f);
            }
        }

        for(int i = 0; i < 6; ++i)
        {
            if( i < NumberOfObjectsByDifficulty[DifficultyLevel])
            {
                ObjectsSpawned[i].SetActive(true);
                ObjectsSpawned[i].transform.localPosition = positions[i];
            }
            else 
            {
                ObjectsSpawned[i].SetActive(false);
            }
        }

        //correctScale
        CursorHand.localScale = new Vector3(1.0f, ListOfObjectsSizes[correctObject], 1.0f);
        correctPosition = ObjectsSpawned[correctObject].transform.position;
    }

    void Reset(){
        Start();
    }

    // Update is called once per frame
    void Update()
    {
        if(!blockInput) PlayerInput();
        FailureLogic();
    }

    void FailureLogic()
    {
        if(timePassed > TimeLimit)
        {
            blockInput = true; 
            float handCorrectDistance = Vector3.Distance(
                new Vector3(correctPosition.x, correctPosition.y, 1.0f), 
                new Vector3(CursorHand.position.x, CursorHand.position.y, 1.0f));

            //Debug.Log(handCorrectDistance);

            if(handCorrectDistance < distanceCutoff)
            {
                //Display correct message for 1 sec
                Debug.Log("YOU WON");
                if(timePassed > TimeLimit + displayMessageTime)
                {
                    Reset();
                    minigamesController.deactivateMinigame(true);
                }
            }
            else
            {
                Debug.Log("YOU LOST");
                //Display incorrect message for 1 sec
                if(timePassed > TimeLimit + displayMessageTime)
                {
                    Reset();
                    minigamesController.deactivateMinigame(false);
                }
            }
        }

        timePassed += Time.deltaTime;        
    }

    void PlayerInput()
    {
        if(Input.GetKey(KeyCode.A)) CursorHand.position -= new Vector3(CursorSpeed, 0.0f, 0.0f)  * Time.deltaTime; 
        if(Input.GetKey(KeyCode.D)) CursorHand.position += new Vector3(CursorSpeed, 0.0f, 0.0f) * Time.deltaTime; 
        if(Input.GetKey(KeyCode.W)) CursorHand.position += new Vector3(0.0f, CursorSpeed, 0.0f)    * Time.deltaTime; 
        if(Input.GetKey(KeyCode.S)) CursorHand.position -= new Vector3(0.0f, CursorSpeed, 0.0f)  * Time.deltaTime; 

        //Vector3 worldPositionMouse = MainCamera.ScreenToWorldPoint(Input.mousePosition);
        //CursorHand.position = new Vector3(worldPositionMouse.x, worldPositionMouse.y, 1.0f);
    }
}
