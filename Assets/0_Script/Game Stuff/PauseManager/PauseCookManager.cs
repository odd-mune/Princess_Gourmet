using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseCookManager : MonoBehaviour
{
    private bool isPaused;
    public GameObject cookPanel;

    //Start is called before the first frame update
    void Start()
    {
        isPaused = false;
    }

    //Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("cook"))
        {
            ChangePause();
        }
    }

    public void ChangePause()
    {
        isPaused = !isPaused;
        if(isPaused)
            {
                cookPanel.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                cookPanel.SetActive(false);
                Time.timeScale = 1f;
            }
    }
}
