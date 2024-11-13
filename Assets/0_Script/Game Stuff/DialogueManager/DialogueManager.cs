using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Security.Principal;
using System.Runtime.Serialization.Json;

public class DialogueManager : MonoBehaviour
{
    public TalkManager talkManager;
    public GameObject talkPanel;
    private Image mPrincessPortrait;
    private Image mTalkerPortrait;
    public TMP_Text talkText;
    public GameObject scanObject;
    public bool isAction;
    public int talkIndex;

    private DialogueData mCurrentDialogueDataOrNull;

    private void Start()
    {
        if (talkPanel == null)
        {
            Debug.LogError("Talk panel을 DialogueManager에 연동해주세요!!");
            Debug.Break();
        }

        for (int i = 0; i < talkPanel.transform.childCount; i++)
        {
            Transform child = talkPanel.transform.GetChild(i);

            if (child.gameObject.name == "PrincessPortrait")
            {
                mPrincessPortrait = child.GetComponent<Image>();
            }
            else if (child.gameObject.name == "TalkerPortrait")
            {
                mTalkerPortrait = child.GetComponent<Image>();
            }

            if (mPrincessPortrait != null && mTalkerPortrait != null)
            {
                break;
            }
        }

        if (mTalkerPortrait == null || mTalkerPortrait == null)
        {
            Debug.LogError("Talk Panel 하위에 Image component를 가진 PrincessPortrait과 TalkerPortrait이 없습니다!!");
            Debug.Break();
        }
    }

    public bool Action(GameObject scanObj, out DialogueData currentDialogueDataOrNull)
    {
        currentDialogueDataOrNull = null;
        bool isDialogueComplete = true;
        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>();
        if (objData != null)
        {
            if (objData.id == -1 || (objData.DialogueDataOrNull != null && objData.id != objData.DialogueDataOrNull.Id))
            {
                isDialogueComplete = Talk(objData.DialogueDataOrNull, objData.isNpc);
                if (isDialogueComplete == false)
                {
                    currentDialogueDataOrNull = objData.DialogueDataOrNull;
                }
            }
            else
            {
                isDialogueComplete = Talk(objData.id, objData.isNpc, out currentDialogueDataOrNull);
            }

            talkPanel.SetActive(isAction);
        }
        return isDialogueComplete;
    }

    public bool Action(DialogueData dialogueDataOrNull, bool isNpc)
    {
        bool isDialogueComplete = true;
        if (dialogueDataOrNull != null)
        {
            isDialogueComplete = Talk(dialogueDataOrNull, isNpc);

            talkPanel.SetActive(isAction);
        }

        return isDialogueComplete;
    }

    bool Talk(int id, bool isNpc, out DialogueData currentDialogueDataOrNull)
    {
        currentDialogueDataOrNull = null;
        DialogueData dialogueDataOrNull = talkManager.GetDialogueDataOrNull(id);
        bool result = Talk(dialogueDataOrNull, isNpc);
        if (result == false)
        {
            currentDialogueDataOrNull = dialogueDataOrNull;
        }

        return result;
    }

    bool Talk(DialogueData dialogueDataOrNull, bool isNpc)
    {
        if (dialogueDataOrNull.hasBeenPlayed == true && dialogueDataOrNull.IsPlayedOnce == true)
        {
            return true;
        }
        
        if (mCurrentDialogueDataOrNull != null && mCurrentDialogueDataOrNull != dialogueDataOrNull)
        {
            mTalkerPortrait.gameObject.SetActive(false);
            mPrincessPortrait.gameObject.SetActive(false);
            mCurrentDialogueDataOrNull.OnDialogueEnd();
        }

        mCurrentDialogueDataOrNull = dialogueDataOrNull;

        DialogueData.Dialogue dialogueOrNull = null;
        if (dialogueDataOrNull != null && dialogueDataOrNull.DialogueOnInteraction.Count > talkIndex)
        {
            dialogueOrNull = dialogueDataOrNull.DialogueOnInteraction[talkIndex];
        }

        if (dialogueOrNull == null)
        {
            if (isAction == true)
            {
                mTalkerPortrait.gameObject.SetActive(false);
                mPrincessPortrait.gameObject.SetActive(false);
                mCurrentDialogueDataOrNull.OnDialogueEnd();
                mCurrentDialogueDataOrNull = null;
            }

            isAction = false;
            talkIndex = 0;
            return true;
        }

        talkText.text = dialogueOrNull.dialogue;

        //초상화잠시지움portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
        mTalkerPortrait.sprite = dialogueOrNull.talkerSprite;
        mPrincessPortrait.sprite = dialogueOrNull.princessSprite;

        if (mTalkerPortrait.sprite != null)
        {
            mTalkerPortrait.gameObject.SetActive(true);
        }
        if (mPrincessPortrait.sprite != null)
        {
            mPrincessPortrait.gameObject.SetActive(true);
        }

        if (dialogueOrNull.isPrincessSpeaking)
        {
            mPrincessPortrait.color = new Color(1, 1, 1, 1);
            mTalkerPortrait.color = new Color(0.5f, 0.5f, 0.5f, 1);
        }
        else
        {
            mPrincessPortrait.color = new Color(0.5f, 0.5f, 0.5f, 1);
            mTalkerPortrait.color = new Color(1, 1, 1, 1);
        }

        isAction = true;
        talkIndex++;

        return false;
    }
}
