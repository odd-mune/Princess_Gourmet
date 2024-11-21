using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjData : MonoBehaviour
{
    public int id;
    [Tooltip("ID를 -1로 줄 시 DialogueData를 사용함")]
    public DialogueData DialogueDataOrNull = null;
    public bool isNpc;
}
