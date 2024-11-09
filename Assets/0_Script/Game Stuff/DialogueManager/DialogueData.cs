using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueData : MonoBehaviour
{
    [System.Serializable]
    public class Dialogue
    {
        [Tooltip("노출시킬 초상화")]
        public Sprite sprite;
        [Tooltip("대사")]
        public string dialogue;
    };

    [Tooltip("대사 ID")]
    public int Id;
    [Tooltip("단순 인터랙션 시 대사 목록")]
    public List<Dialogue> DialogueOnInteraction;

    [Tooltip("대사 종료 시 이벤트")]
    public UnityEvent OnDialogueEndEvent;

    public void OnDialogueEnd() 
    {
        OnDialogueEndEvent.Invoke();
    }
}
