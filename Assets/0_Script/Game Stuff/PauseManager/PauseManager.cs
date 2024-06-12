using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : IPauseManager
{
    public string mainMenu;

    //Start is called before the first frame update
    void Start()
    {
        ButtonName = "pause";
    }

    public void QuitToMain()
    {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f;
    }
}
