using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    public UnityEngine.UI.Button playButton;
    public UnityEngine.UI.Button optionsButton;
    public UnityEngine.UI.Button quitButton;

    public Material trapMat;
    public Material goalMat;
    public UnityEngine.UI.Toggle colorblindMode;

    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(PlayMaze);
        quitButton.onClick.AddListener(QuitMaze);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayMaze()
    {
        SceneManager.LoadScene("maze");

        if (colorblindMode.isOn)
        {
            trapMat.color = new Color32(255, 112, 0, 1);
            goalMat.color = Color.blue;
        }
    }

    public void QuitMaze()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

}
