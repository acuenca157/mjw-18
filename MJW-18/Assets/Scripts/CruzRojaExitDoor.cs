using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CruzRojaExitDoor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other) {
        Debug.Log("EVENT WON");
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("EVENT WON");
    }
}
