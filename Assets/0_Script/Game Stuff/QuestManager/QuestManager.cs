using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [Tooltip("퀘스트 목록")]
    [SerializeField] private List<Quest> Quests;

    private List<Quest>[] mQuests;

    // Start is called before the first frame update
    void Start()
    {
        mQuests = new List<Quest>[(int)QuestState.Count];

        for (int i = 0; i < mQuests.Length; i++)
        {
            mQuests[i] = new List<Quest>();
        }

        foreach (var quest in Quests)
        {
            mQuests[(int)QuestState.Undiscovered].Add(quest);
        }
    }

    public void ReceiveQuest(string questName)
    {
        for (int i = 0; i < mQuests[(int)QuestState.Undiscovered].Count; i++)
        {
            Quest quest = mQuests[(int)QuestState.Undiscovered][i];
            if (quest.name == questName)
            {
                mQuests[(int)QuestState.Undiscovered].RemoveAt(i);
                quest.OnQuestTaken();
                mQuests[(int)QuestState.OnProgress].Add(quest);
            }
        }
    }

    public Quest GetQuestOrNull(string questName)
    {
        foreach (var quest in Quests)
        {
            if (quest.name == questName)
            {
                return quest;
            }
        }

        return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
