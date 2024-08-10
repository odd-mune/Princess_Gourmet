using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //이런 양식 신청 받습니다 
public struct AudioRequest
{
    public string AudioName;
    public bool Play;
};

[System.Serializable] //우리 이런거 있습니다
public struct AudioEntry
{
    public string audioName;
    public AudioClip clip;
}

public class AudioManager : MonoBehaviour
{
    [SerializeField] //array 
    AudioEntry[] audioEntries; //우리 이런 노래들 있습니다. 

    public Dictionary<string, AudioClip> audioDictionary = new Dictionary<string, AudioClip>(); //탐색을 쉽게 하기 위한 딕셔너리 사용

    private AudioSource audioSource; //선언
    private Queue<AudioRequest> audioRequests = new Queue<AudioRequest>();

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); //초기화 정의 

        foreach(AudioEntry entry in audioEntries) //딕셔너리에 우리 노래 등록
        {
            audioDictionary.Add(entry.audioName, entry.clip);
        }
    }

    public void Request(AudioRequest request) //노래 요청옴 
    {
        audioRequests.Enqueue(request); //노래 요청을 큐에 넣어 
    }

    void Update()
    {
        foreach( AudioRequest request in audioRequests ) //큐 요청 하나하나 처리 
        {
            Debug.Log($"Play {request.AudioName}");
            if (request.Play == true)
            {
                AudioClip clip = audioDictionary[request.AudioName];
                // play
                if (clip != null)
                {
                    audioSource.clip = clip;
                    audioSource.Play();
                }
            }
            else
            {
                AudioClip clip = audioDictionary[request.AudioName];
                // stop
                if (clip != null)
                {
                    audioSource.clip = clip;
                    audioSource.Stop();
                }
            }
        }
        audioRequests.Clear();
    }
}

// while (currentWalkCount < WalkCount) //걷는 동안 
// {
//     audioSource.clip = walkSound_1;
//     audioSource.play();
// }