using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueData : MonoBehaviour
{
    [System.Serializable]
    public class Dialogue
    {
        [Tooltip("노출시킬 상대 초상화")]
        public Sprite talkerSprite;
        [Tooltip("노출시킬 공주 초상화")]
        public Sprite princessSprite;
        [Tooltip("화자가 공주인지?")]
        public bool isPrincessSpeaking;
        [Tooltip("대사")]
        public string dialogue;
    };

    [Tooltip("대사 ID")]
    public int Id;
    [Tooltip("단순 인터랙션 시 대사 목록")]
    public List<Dialogue> DialogueOnInteraction;

    [Tooltip("대사 종료 시 이벤트")]
    public UnityEvent OnDialogueEndEvent;

    [Tooltip("단발성 대사인지 여부")]
    public bool IsPlayedOnce;

    private bool mHasBeenPlayed = false;

    public bool hasBeenPlayed { get { return mHasBeenPlayed; } }

    public void OnDialogueEnd() 
    {
        OnDialogueEndEvent.Invoke();
        mHasBeenPlayed = true;
    }
}
