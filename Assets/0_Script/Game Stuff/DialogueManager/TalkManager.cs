using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    void GenerateData()
    {
        // 아이템
        talkData.Add(1, new string[] {"요리왕국의 역사서"});
        talkData.Add(2, new string[] {"요리왕국 인기 Top 100 요리레시피"});
        talkData.Add(3, new string[] {"공주의 비밀 일기장"});
        talkData.Add(4, new string[] {"옷을 갈아입을 수 있는 옷장이다."});
        talkData.Add(5, new string[] {"마을로 가는 길"});
        talkData.Add(6, new string[] {"숲으로 가는 길"});

        // NPC 대화 
        talkData.Add(1000, new string[] {"안녕하십니까! 공주님", "지금은 알현실에 들어가실 수 없으십니다."});
    }

    public string GetTalk(int id, int talkIndex)
    {
        if (talkIndex == talkData[id].Length)
        {
            return null;
        }
        else
        {
            return talkData[id][talkIndex];
        }
    }
}
