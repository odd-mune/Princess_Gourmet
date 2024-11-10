using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public enum QuestState
{
    Undiscovered,
    OnProgress,
    Completed,
    Failed,
    Count,
}

public enum QuestType
{
    Collection,
    Hunting,
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
    [Tooltip("퀘스트 수행 중 대사")]
    [SerializeField] private DialogueData QuestOnProgressDialogue;
    [Tooltip("퀘스트 수행 완료 대사")]
    [SerializeField] private DialogueData QuestCompletionDialogueOrNull;
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

    private DialogueData mCurrentDialogueOrNull;

    public DialogueData currentDialogueOrNull { get { return mCurrentDialogueOrNull; } }

    public virtual QuestType GetQuestType() { return QuestType.Count; }

    public UnityEvent OnQuestComplete;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void Init()
    {
        mCurrentDialogueOrNull = QuestAcceptDialogue;
    }

    public void OnQuestTaken()
    {
        mCurrentState = QuestState.OnProgress;
        mCurrentDialogueOrNull = QuestOnProgressDialogue;
    }

    public void OnQuestCompleted()
    {
        mCurrentState = QuestState.Completed;
        mCurrentDialogueOrNull = QuestCompletionDialogueOrNull;
        OnQuestComplete.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
