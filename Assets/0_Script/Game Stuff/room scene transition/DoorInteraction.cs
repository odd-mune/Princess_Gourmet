using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorInteraction : MonoBehaviour
{
    bool isOpen;
    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerStorage;

    void Update()
    {
        if (isOpen && Input.GetKeyDown(KeyCode.Space))
            Open();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isOpen = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isOpen = false;
        }
    }

    void Open()
    {
        playerStorage.initialValue = playerPosition;
        SceneManager.LoadScene(sceneToLoad);
    }
}



