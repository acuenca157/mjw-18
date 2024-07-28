using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameButton : MonoBehaviour
{

    public Button StartButton;
    public Button ExitButton;
    public Button OptionButton;

    // Start is called before the first frame update
    void Start()
    {
        StartButton.onClick.AddListener(StartGameOnClick);
        ExitButton.onClick.AddListener(ExitGameOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartGameOnClick()
    {
        SceneManager.LoadScene("CinematicIntro");
    }

    void ExitGameOnClick()
    {
        Application.Quit();
    }
}
