using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToEnding : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private float TimeElapsed;
    // Update is called once per frame
    void Update()
    {
        TimeElapsed += Time.deltaTime;
        if(TimeElapsed > 3.0f) SceneManager.LoadScene("Ending");
    }
}
