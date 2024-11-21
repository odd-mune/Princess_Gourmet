using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AnimalHuntInfo
{
    [Tooltip("사냥할 동물")]
    public string AnimalToHunt;
    [Tooltip("사냥할 개체 수")]
    public int Count;
};

public class HuntingQuest : Quest
{
    [Tooltip("모을 아이템 목록")]
    public List<AnimalHuntInfo> AnimalsToHunt;

    public override QuestType GetQuestType() { return QuestType.Hunting; }
}
