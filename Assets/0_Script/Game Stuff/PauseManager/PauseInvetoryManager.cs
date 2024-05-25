using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseInvetoryManager : MonoBehaviour
{
    private bool isPaused;
    public GameObject inventoryPanel;

    //Start is called before the first frame update
    void Start()
    {
        isPaused = false;
    }

    //FixedUpdate is called once per frame
    void FixedUpdate()
    {
        if(Input.GetButtonDown("inventory"))
        {
            ChangePause();
        }
    }

    public void ChangePause()
    {
        isPaused = !isPaused;
        if(isPaused)
            {
                inventoryPanel.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                inventoryPanel.SetActive(false);
                Time.timeScale = 1f;
            }
    }
}
