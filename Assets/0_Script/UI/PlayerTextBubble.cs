using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTextBubble : MonoBehaviour
{
    [Tooltip("offset")]
    public Vector3 Offset;
    private PlayerManager mPlayerManager;

    // Start is called before the first frame update
    void Start()
    {
        mPlayerManager = FindObjectOfType<PlayerManager>();
        Offset.z = 0.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = mPlayerManager.transform.position + Offset;
    }
}
