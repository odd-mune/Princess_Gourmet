using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerStorage;

    // place text
    public bool needText;
    public string placeName;
    public GameObject text;
    public Text placeText;


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            playerStorage.initialValue = playerPosition;
            SceneManager.LoadScene(sceneToLoad);
        }


        // place text
        if(needText)
        {
            StartCoroutine(placeNameCO());
        }
    }



    // place text
    private IEnumerator placeNameCO()
    {
        text.SetActive(true);
        placeText.text = placeName;
        yield return new WaitForSeconds(5f);
        text.SetActive(false);
    }
}