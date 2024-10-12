using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerDownHandler
{
    public InventoryItem item;
    public int index;
    public CraftingManager craftingManager;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (craftingManager != null)
        {
            craftingManager.OnClickSlot(this);
        }
    }
}
