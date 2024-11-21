using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public enum TextShowState
{
    Hidden,
    FadeIn,
    Show,
    FadeOut,
    Count,
}

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerStorage;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            PlayerManager player = other.GetComponent<PlayerManager>();
            foreach (var info in player.TransitionableScenes)
            {
                if (info.Name == sceneToLoad && info.IsTransitionable)
                {
                    Transition();
                    break;
                }
            }
        }
    }

    public void Transition()
    {
        playerStorage.initialValue = playerPosition;
        SceneManager.LoadScene(sceneToLoad);
    }
}