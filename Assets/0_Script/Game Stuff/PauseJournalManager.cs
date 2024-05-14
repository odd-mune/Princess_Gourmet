using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseJournalManager : MonoBehaviour
{
    private bool isPaused;
    public GameObject journalPanel;

    //Start is called before the first frame update
    void Start()
    {
        isPaused = false;
    }

    //Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("journal"))
        {
            ChangePause();
        }
    }

    public void ChangePause()
    {
        isPaused = !isPaused;
        if(isPaused)
            {
                journalPanel.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                journalPanel.SetActive(false);
                Time.timeScale = 1f;
            }
    }
}
