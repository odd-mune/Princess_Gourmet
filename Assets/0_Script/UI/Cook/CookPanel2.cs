using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookPanel2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        IPauseManager.SetPausable(false);
    }

    private void OnDisable()
    {
        IPauseManager.SetPausable(true);
    }
}
