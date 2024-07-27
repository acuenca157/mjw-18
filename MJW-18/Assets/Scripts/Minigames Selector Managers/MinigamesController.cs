using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigamesController : MonoBehaviour
{
    CurtainsController curtainsController;
    TVController tvController;
    
    // Start is called before the first frame update
    void Start()
    {
        curtainsController = FindObjectOfType<CurtainsController>();
        tvController = FindObjectOfType<TVController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            curtainsController.makeCurtainTransition();
            tvController.toggleTV();
        }
    }
}
