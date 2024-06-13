using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class Ingredient : CookItem
{
    [SerializeField]
    public enum Type
    {
        Cook,
        StirFry,
    };

    [SerializeField]
    public struct CreateInfo
    {
        Type    Type;
    };

    // Image _image;
    TMP_Text _text;

    void Start()
    {
        // _image = GetComponent<Image>();
        _text = transform.GetChild(0).GetComponentInChildren<TMP_Text>();
    }

    // public override void Solve()
    // {
    //     GetInputs();

    //     if (inputs.Count == 0)
    //     {
    //         Output = false;
    //         return;
    //     }

    //     Output = inputs[0];

    //     if (Output)
    //     {
    //         Color outColor = Color.green;
    //         ColorUtility.TryParseHtmlString("#26A96C", out outColor);
    //         _image.color = outColor;
    //         _text.text = "ON";
    //     }
    //     else
    //     {
    //         Color outColor = Color.green;
    //         ColorUtility.TryParseHtmlString("#636363", out outColor);
    //         _image.color = outColor;
    //         _text.text = "OFF";
    //     }
    // }
}