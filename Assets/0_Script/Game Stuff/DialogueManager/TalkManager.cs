using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;

    public Sprite[] portraitArr;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }

    void GenerateData()
    {
        // 아이템
        talkData.Add(1, new string[] {"요리왕국의 역사서"});
        talkData.Add(2, new string[] {"요리왕국 인기 Top 100 요리레시피"});
        talkData.Add(3, new string[] {"공주의 비밀 일기장"});
        talkData.Add(4, new string[] {"옷을 갈아입을 수 있는 옷장이다."});
        talkData.Add(5, new string[] {"<- 마을로 가는 길", "숲으로 가는 길 ->"});
        talkData.Add(6, new string[] {"숲으로 가는 길"});

        // NPC 대화 
            // 왕궁 경비병 1
            talkData.Add(1000, new string[] {"안녕하십니까! 공주님:0", "지금은 알현실에 들어가실 수 없으십니다.:2"});
            // 왕궁 하녀 1
            talkData.Add(2000, new string[] {"안녕하세요 공주님! 좋은 하루입니다!:4", "오늘따라 일이 많네요:7"});

        // 초상화 
        portraitData.Add(1000 + 0, portraitArr[0]);
        portraitData.Add(1000 + 1, portraitArr[1]);
        portraitData.Add(1000 + 2, portraitArr[2]);
        portraitData.Add(1000 + 3, portraitArr[3]);
        portraitData.Add(2000 + 0, portraitArr[4]);
        portraitData.Add(2000 + 1, portraitArr[5]);
        portraitData.Add(2000 + 2, portraitArr[6]);
        portraitData.Add(2000 + 3, portraitArr[7]);
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

    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex];
    }
}
