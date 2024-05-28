using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseJournalManager : IPauseManager
{
    //Start is called before the first frame update
    void Start()
    {
        ButtonName = "journal";
    }
}
