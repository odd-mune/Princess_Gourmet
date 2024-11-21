using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


[CreateAssetMenu(fileName = "New GameSaveData", menuName = "GameSaveData")]
public class GameSaveData : ScriptableObject
{
    [Tooltip("플레이어 인벤토리")]
    public PlayerInventory PlayerInventory;
    [Tooltip("현재 공주 옷차림")]
    public string CurrentCloth = "dress";
    [Tooltip("이미 겪은 시네마 목록")]
    public Dictionary<string, bool> CinemasAlreadyWatched = new Dictionary<string, bool>();
    [Tooltip("이미 겪은 시네마 목록")]
    public bool isFlambeActivated = false;
    public void Initialize()
    {
        PlayerInventory.Clear();
        CurrentCloth = "dress";
        CinemasAlreadyWatched.Clear();
        isFlambeActivated = false;
    }
}
