using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MinigameHouseController : MonoBehaviour
{
    MinigamesController minigamesController;
    public string levelTitle;

    [Range(0, 100f)]
    [SerializeField]
    private float actionRadius = 5.0f;
    private Transform playerTransform;
    [SerializeField]
    private int levelId;
    // Start is called before the first frame update
    void Start()
    {
        minigamesController = FindObjectOfType<MinigamesController>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (actionRadius >  Vector2.Distance(playerTransform.position, this.transform.position)) {
            minigamesController.activateMinigame(levelId);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, actionRadius);
    }
}
