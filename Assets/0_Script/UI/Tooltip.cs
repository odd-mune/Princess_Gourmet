using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tooltip : MonoBehaviour
{
    public TMP_Text ItemName;
    public TMP_Text ItemDescription;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetName(string name)
    {
        ItemName.text = name;
    }

    public void SetDescription(string description)
    {
        ItemDescription.text = description;
    }
}
