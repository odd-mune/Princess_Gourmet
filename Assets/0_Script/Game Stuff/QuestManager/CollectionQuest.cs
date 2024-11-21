using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionQuest : Quest
{
    [Tooltip("모을 아이템 목록")]
    public List<ItemInfo> ItemToCollectInfos;

    public override QuestType GetQuestType() { return QuestType.Collection; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
