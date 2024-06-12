using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public Text talkText;
    public GameObject scanObject;

    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        talkText.text = "이것의 이름은 " + scanObject.name + "이다.";
    }
}
