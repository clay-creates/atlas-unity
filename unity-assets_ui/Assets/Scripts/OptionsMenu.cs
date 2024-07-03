using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{

    public Button BackButton;

    private void Start()
    {
        BackButton.onClick.AddListener(() => Back());
    }

    public static class SceneManagementHelper
    {
        public static void SaveCurrentSceneIndex()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            PlayerPrefs.SetInt("currentSceneIndex", currentSceneIndex);
        }
    }

    public void Back()
    {
        int previousLevel = PlayerPrefs.GetInt("currentSceneIndex", -1);
        if (previousLevel != -1)
        {
            SceneManager.LoadScene(previousLevel);
            PlayerPrefs.DeleteKey("currentSceneIndex");
        }
    }
}
