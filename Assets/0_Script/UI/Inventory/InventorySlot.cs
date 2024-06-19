using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    [Header("UI Stuff to change")]
    [SerializeField] private TextMeshProUGUI itemNumberText;
    [SerializeField] private Image itemImage;

    [Header("Variables from the item")]
    public InventoryItem thisItem;
    public InventoryManager thisManager;

    public void Setup(InventoryItem newItem, InventoryManager newManager)
    {
        thisItem = newItem;
        thisManager = newManager;
        if (thisItem)
        {
            itemImage.sprite = thisItem.itemImage;
            itemNumberText.text = "" + thisItem.numberHeld;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        int clickCount = eventData.clickCount;

        if(thisItem)
        {
            if (clickCount == 1)
            {
                thisManager.SetupDescriptionAndButton(thisItem.itemDescription, thisItem.usable, thisItem);
                thisManager.SetupNameAndButton(thisItem.itemName, thisItem.usable, thisItem);
            }
            else if (clickCount == 2)
            {
                if (thisManager.cookManager.PauseCookManager != null && IPauseManager.mCurrentActiveGameObjectOrNull == thisManager.cookManager.PauseCookManager.GameObjectToPause)
                {
                    thisManager.currentItem = thisItem;
                    //thisManager.useButtonPressed();
                    
                    //clear all of the inventory slots
                    thisManager.ClearInventorySlots();
                    //refill all slots with new numbers
                    thisManager.MakeInventorySlots();    
                    thisManager.SetTextAndButton("", false);
                    thisManager.SetNameAndButton("", false);

                    if (thisItem.numberHeld == 1)
                    {
                        thisManager.playerInventory.myInventory.Remove(thisItem);
                    }
                    else
                    {
                        if (thisItem.unique == false)
                        {
                            thisItem.numberHeld += 1;
                        }
                    }

                    thisManager.cookManager.CreateNode(thisItem);
                }
            }
        }
    }
}
