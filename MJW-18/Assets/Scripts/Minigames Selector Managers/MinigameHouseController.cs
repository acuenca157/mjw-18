using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameHouseController : MonoBehaviour
{
    public string levelTitle;

    [Range(0, 100f)]
    [SerializeField]
    private float actionRadius = 5.0f;
    private Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (actionRadius >  Vector2.Distance(playerTransform.position, this.transform.position)) {
            Debug.Log("Entro en " + levelTitle);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, actionRadius);
    }
}
