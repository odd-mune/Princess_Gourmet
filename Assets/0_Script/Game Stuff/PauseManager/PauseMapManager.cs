using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMapManager : MonoBehaviour
{
    private bool isPaused;
    public GameObject mapPanel;

    //Start is called before the first frame update
    void Start()
    {
        isPaused = false;
    }

    //FixedUpdate is called once per frame
    void FixedUpdate()
    {
        if(Input.GetButtonDown("map"))
        {
            ChangePause();
        }
    }

    public void ChangePause()
    {
        isPaused = !isPaused;
        if(isPaused)
            {
                mapPanel.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                mapPanel.SetActive(false);
                Time.timeScale = 1f;
            }
    }
}
