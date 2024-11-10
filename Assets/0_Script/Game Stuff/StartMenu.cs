using UnityEngine;

public class StartMenu : MonoBehaviour
{
    [Tooltip("오디오 매니저")]
    public AudioManager AudioManager;

    public void PlayIntroLoop()
    {
        AudioManager.Play("intro");
    }

    private void OnDisable()
    {
        AudioManager.Stop("intro");
    }
}
