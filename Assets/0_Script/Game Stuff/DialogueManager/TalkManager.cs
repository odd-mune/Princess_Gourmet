using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    //Dictionary<int, string[]> talkData;
    //Dictionary<int, Sprite> portraitData;

    [Tooltip("등록할 대사")]
    public List<DialogueData> DialogueDatas;

    private Dictionary<int, DialogueData> mDialogueDatas = new Dictionary<int, DialogueData>();

    void Awake()
    {
        //talkData = new Dictionary<int, string[]>();
        //portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }

    void GenerateData()
    {
        foreach (var dialogueData in DialogueDatas)
        {
            mDialogueDatas.Add(dialogueData.Id, dialogueData);
        }

        // 아이템
        //talkData.Add(1, new string[] {"요리왕국의 역사서"});
        //talkData.Add(2, new string[] {"요리왕국 인기 Top 100 요리레시피"});
        //talkData.Add(3, new string[] {"공주의 비밀 일기장"});
        //talkData.Add(4, new string[] {"옷을 갈아입을 수 있는 옷장이다."});
        //talkData.Add(5, new string[] {"<- 마을로 가는 길", "숲으로 가는 길 ->"});
        //talkData.Add(6, new string[] {"숲으로 가는 길"});

        //// NPC 대화
        //talkData.Add(3000, new string[] {"널 기다리고 있었다 공주", "얏호~"}); //불의 신전, 불
    }

    public DialogueData GetDialogueDataOrNull(int id)
    {
        DialogueData dialogueDataOrNull = null;
        bool result = mDialogueDatas.TryGetValue(id, out dialogueDataOrNull);
        if (result == false)
        {
            return null;
        }

        return dialogueDataOrNull;
    }
}
