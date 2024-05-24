using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grow : MonoBehaviour
{
    public Sprite[] objectSprites;
    public float changetimer;

    private SpriteRenderer sp;
    private float currentTimer = 0.0f;

    private bool changeAt = false;
    private int spriteIndex = 0;

    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        sp.RegisterSpriteChangeCallback(OnSpriteChanged);
    }

    private void OnSpriteChanged(SpriteRenderer sp)
    {
        currentTimer = 0f;

        if (sp.sprite.name != objectSprites[objectSprites.Length - 1].name)
        {
            changeAt = true;
            spriteIndex++;
        }
        else
        {
            changeAt = false;     
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            spriteIndex = 0;
            sp.sprite = objectSprites[0];
            currentTimer = 0f;
        }

        if(changeAt)
        {
            currentTimer += Time.deltaTime;
            if(currentTimer > changetimer)
            {
                sp.sprite = objectSprites[spriteIndex];
            }
        }
    }
}
