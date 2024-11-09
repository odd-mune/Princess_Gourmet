using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TalkManager talkManager;
    public GameObject talkPanel;
    public Image portraitImg;
    public TMP_Text talkText;
    public GameObject scanObject;
    public bool isAction;
    public int talkIndex;

    private DialogueData mCurrentDialogueDataOrNull;

    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>();
        Talk(objData.id, objData.isNpc);
        
        talkPanel.SetActive(isAction);
    }

    void Talk(int id, bool isNpc)
    {
        DialogueData dialogueDataOrNull = talkManager.GetDialogueDataOrNull(id);

        if (mCurrentDialogueDataOrNull != dialogueDataOrNull)
        {
            mCurrentDialogueDataOrNull.OnDialogueEnd();
        }

        DialogueData.Dialogue dialogueOrNull = null;
        if (dialogueDataOrNull.DialogueOnInteraction.Count < talkIndex)
        {
            dialogueOrNull = dialogueDataOrNull.DialogueOnInteraction[talkIndex];
        }

        if (dialogueOrNull == null)
        {
            if (isAction == true)
            {
                mCurrentDialogueDataOrNull.OnDialogueEnd();
            }

            isAction = false;
            talkIndex = 0;
            return;
        }

        if (isNpc)
        {
            talkText.text = dialogueOrNull.dialogue.Split(':')[0];

            //초상화잠시지움portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
            portraitImg.color = new Color(1, 1, 1, 1);
        }
        else
        {
            talkText.text = dialogueOrNull.dialogue;
            
            portraitImg.color = new Color(1, 1, 1, 0);
        }

        isAction = true;
        talkIndex++;
    }
}
