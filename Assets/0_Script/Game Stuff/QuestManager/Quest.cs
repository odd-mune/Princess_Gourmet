using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum QuestState
{
    Undiscovered,
    OnProgress,
    Completed,
    Failed,
    Count,
}

[System.Serializable]
public class ItemInfo
{
    [Tooltip("모을 아이템")]
    public InventoryItem Item;
    [Tooltip("모아야하는 개수")]
    public int Count;
};

public class Quest : MonoBehaviour
{
    [Tooltip("퀘스트 수락 대사")]
    [SerializeField] private DialogueData QuestAcceptDialogue;
    [Tooltip("퀘스트 이름")]
    [SerializeField] private string Name;

    [System.Serializable]
    public class Reward
    {
        [Tooltip("보상으로 줄 아이템 목록")]
        public List<ItemInfo> ItemInfos;
    };

    [Tooltip("퀘스트 보상")]
    [SerializeField] private Reward QuestReward;

    private QuestState mCurrentState;

    public QuestState currentState { get { return mCurrentState; } }


    // Start is called before the first frame update
    void Start()
    {
        if (QuestAcceptDialogue != null)
        {
        }
    }

    public void OnQuestTaken()
    {
        mCurrentState = QuestState.OnProgress;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
