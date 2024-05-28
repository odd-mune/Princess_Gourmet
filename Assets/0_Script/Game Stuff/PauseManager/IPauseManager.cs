using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPauseManager : MonoBehaviour
{
    static private GameObject mCurrentActiveGameObjectOrNull = null;

    protected bool isPaused;
    public GameObject GameObjectToPause;
    protected string ButtonName;
    protected bool isInventoryPressed;

    //Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        isInventoryPressed = false;
    }

    //FixedUpdate is called once per frame
    void Update()
    {
        if(isInventoryPressed == false)
        {
            if (Input.GetButtonDown(ButtonName))
            {
                isInventoryPressed = true;
                ChangePause();
            }
        }
        else if (Input.GetButtonDown(ButtonName) == false)
        {
            isInventoryPressed = false;
        }
    }

    public void ChangePause()
    {
        if (mCurrentActiveGameObjectOrNull == null || mCurrentActiveGameObjectOrNull == GameObjectToPause)
        {
            isPaused = !isPaused;
            if(isPaused)
            {
                isPaused = true;
                GameObjectToPause.SetActive(true);
                Time.timeScale = 0f;
                mCurrentActiveGameObjectOrNull = GameObjectToPause;
            }
            else
            {
                GameObjectToPause.SetActive(false);
                Time.timeScale = 1f;
                mCurrentActiveGameObjectOrNull = null;
            }
        }
    }
}
