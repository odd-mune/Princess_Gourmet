using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Security.Principal;

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

        if (mCurrentDialogueDataOrNull != null && mCurrentDialogueDataOrNull != dialogueDataOrNull)
        {
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
                mCurrentDialogueDataOrNull.OnDialogueEnd();
            }

            isAction = false;
            talkIndex = 0;
            return;
        }

        talkText.text = dialogueOrNull.dialogue.Split(':')[0];

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
    }
}
