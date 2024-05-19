using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Item : MonoBehaviour
{
    [SerializeField] TMP_Text pickUpText;
    bool isPickUp;

    void Start()
    {
        pickUpText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isPickUp && Input.GetKeyDown(KeyCode.Space))
            PickUp();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            pickUpText.gameObject.SetActive(true);
            isPickUp = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            pickUpText.gameObject.SetActive(false);
            isPickUp = false;
        }
    }   

    void PickUp()
    {
        Destroy(gameObject);
    }
}




