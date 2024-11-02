using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AnimalHuntInfo
{
    [Tooltip("사냥할 동물")]
    public Animal AnimalToHunt;
    [Tooltip("사냥할 개체 수")]
    public int Count;
};

public class HuntingQuest : Quest
{
    [Tooltip("모을 아이템 목록")]
    [SerializeField] private List<AnimalHuntInfo> AnimalsToHunt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
