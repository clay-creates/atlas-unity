using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static OptionsMenu;

public class MainMenu : MonoBehaviour
{

    public Button Level01;
    public Button Level02;
    public Button Level03;

    public Button OptionsButton;
    public Button QuitButton;

    private void Start()
    {
        Level01.onClick.AddListener(() => LevelSelect(1));
        Level02.onClick.AddListener(() => LevelSelect(2));
        Level03.onClick.AddListener(() => LevelSelect(3));

        OptionsButton.onClick.AddListener(Options);
        QuitButton.onClick.AddListener(ExitGame);
    }

    public void LevelSelect(int level)
    {
        switch (level)
        {
            case 1:
                SceneManagementHelper.SaveCurrentSceneIndex();
                SceneManager.LoadScene("Level01");
                break;
            case 2:
                SceneManagementHelper.SaveCurrentSceneIndex();
                SceneManager.LoadScene("Level02");
                break;
            case 3:
                SceneManagementHelper.SaveCurrentSceneIndex();
                SceneManager.LoadScene("Level03");
                break;
            default:
                Debug.LogError("Invalid level selected.");
                break;
        }
    }

    public void Options()
    {
        SceneManagementHelper.SaveCurrentSceneIndex();
        SceneManager.LoadScene("Options");
    }

    public void ExitGame()
    {
        Debug.Log("Exited");
        Application.Quit();
    }
}
