using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceText : MonoBehaviour 
{
    // place text
    public bool needText;
    public string placeName;
    public GameObject text;
    public Text placeText;

    // place text
    public void Update() 
    {
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
        yield return new WaitForSeconds(10f);
        text.SetActive(false);
    }
}