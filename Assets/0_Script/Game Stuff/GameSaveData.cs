using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New GameSaveData", menuName = "GameSaveData")]
public class GameSaveData : ScriptableObject
{
    [Tooltip("플레이어 인벤토리")]
    public PlayerInventory PlayerInventory;

    public void Initialize()
    {
        PlayerInventory.Clear();
    }
}
