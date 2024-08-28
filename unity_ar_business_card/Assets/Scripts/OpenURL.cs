using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenURL : MonoBehaviour
{

    public Button linkedInButton;
    public Button githubButton;
    public Button gmailButton;
    public Button mediumButton;

    // Start is called before the first frame update
    void Start()
    {
        linkedInButton.onClick.AddListener(LinkedInURL);
        githubButton.onClick.AddListener(GithubURL);
        gmailButton.onClick.AddListener(GmailURL);
        mediumButton.onClick.AddListener(MediumURL);
    }

    public void LinkedInURL()
    {
        Application.OpenURL("https://www.linkedin.com/in/clay-creates/");
    }

    public void GithubURL()
    {
        Application.OpenURL("https://github.com/clay-creates");
    }

    public void GmailURL()
    {
        Application.OpenURL("mailto:claycreatesstuff@gmail.com?subject=Hello&body=I%20would%20like%20to%20get%20in%20touch!");
    }

    public void MediumURL()
    {
        Application.OpenURL("https://medium.com/@claycreatesstuff");
    }
}
