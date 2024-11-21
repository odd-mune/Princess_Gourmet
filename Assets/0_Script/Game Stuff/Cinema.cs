using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Cinema : MonoBehaviour
{
    [Tooltip("타임라인 이름")]
    public string Name;
    [Tooltip("True일 시 다시 재생 안 함")]
    public bool IsPlayedOnce = true;

    // Start is called before the first frame update
    void Start()
    {
        GameStateManager gameStateManager = FindObjectOfType<GameStateManager>();
        if (gameStateManager != null)
        {
            bool bIsPlayedOnce = false;
            bool result = gameStateManager.GameSaveData.CinemasAlreadyWatched.TryGetValue(Name, out bIsPlayedOnce);
            if (result)
            {
                if (bIsPlayedOnce == IsPlayedOnce && IsPlayedOnce == true)
                {
                    gameObject.SetActive(false);
                }
            }
            else
            {
                gameStateManager.GameSaveData.CinemasAlreadyWatched.Add(Name, IsPlayedOnce);
            }
        }
    }
}
