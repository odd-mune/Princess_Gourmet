using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject talkPanel;
    public TMP_Text talkText;
    public GameObject scanObject;
    public bool isAction;

    public void Action(GameObject scanObj)
    {
        if (isAction)
        {
            isAction = false;
        }
        else
        {
            isAction = true;
            scanObject = scanObj;
            talkText.text = "이것의 이름은 " + scanObject.name + "이다.";
        }
        talkPanel.SetActive(isAction);
    }
}
