using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCategoryButton : MonoBehaviour
{
    [Tooltip("인벤토리 카테고리")]
    public InventoryType InventoryType;
    [Tooltip("인벤토리 매니저")]
    public InventoryManager InventoryManager;

    private Button mButton;

    // Start is called before the first frame update
    void Start()
    {
        mButton = GetComponent<Button>();
        if (mButton == null)
        {
            Debug.LogError("Button component와 같이 사용해야 합니다!!");
            Debug.Break();
        }
        mButton.onClick.AddListener(() => InventoryManager.SetInventoryType(InventoryType));
    }
}
