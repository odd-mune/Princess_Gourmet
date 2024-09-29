using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public enum InventoryType
{
    Inventory,
    Ingredients,
    MagicCircle,
}

public class InventoryManager : MonoBehaviour
{
    [Header("Inventory Information")]
    public PlayerInventory playerInventory;
    [SerializeField] private GameObject blankInventorySlot;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private GameObject useButton;
    [SerializeField] private GameObject discardButton;
    public InventoryItem currentItem;
    public CraftingManager craftingManager;
    public InventoryType inventoryType;

    public void SetTextAndButton(string description, bool buttonActive)
    {
        descriptionText.text = description;
        if (useButton != null)
        {
            if (buttonActive)
            {   
                useButton.SetActive(true);
            }
            else
            {
                useButton.SetActive(false);
            }
        }
    }

    public void SetNameAndButton(string name, bool buttonActive)
    {
        nameText.text = name;
        if (useButton != null)
        {
            if (buttonActive)
            {
                useButton.SetActive(true);
            }
            else
            {
                useButton.SetActive(false);
            }
        }
        
    }

    public void MakeInventorySlots()
    {
        if(playerInventory)
        {
            for(int i = 0; i < playerInventory.myInventory.Count; i ++)
            {
                if (playerInventory.myInventory[i].numberHeld > 0)
                {
                    switch (inventoryType)
                    {
                        case InventoryType.Inventory:
                            break;
                        case InventoryType.Ingredients:
                            if (playerInventory.myInventory[i].itemType != ItemType.Ingredient)
                            {
                                continue;
                            }
                            break;
                        case InventoryType.MagicCircle:
                            if (playerInventory.myInventory[i].itemType != ItemType.MagicCircle)
                            {
                                continue;
                            }
                            break;
                        default:
                            break;
                    }

                    GameObject temp = MakeNewInventorySlot();
                    temp.transform.SetParent(inventoryPanel.transform);
                    InventorySlot newSlot = temp.GetComponent<InventorySlot>();
                    if (newSlot)
                    {
                        newSlot.Setup(playerInventory.myInventory[i], this, craftingManager);
                    }
                }
            }
        }
    }

    public GameObject MakeNewInventorySlot()
    {
        return Instantiate(blankInventorySlot, inventoryPanel.transform.position, Quaternion.identity);
    }

    void OnEnable()
    {
        ClearInventorySlots();
        MakeInventorySlots();
        SetTextAndButton("", false);
        SetNameAndButton("", false);
    }

    public void SetupDescriptionAndButton(string newDescriptionString, bool isButtonUsable, InventoryItem newItem)
    {
        currentItem = newItem;
        descriptionText.text = newDescriptionString;
        if (useButton != null)
        {
            useButton.SetActive(isButtonUsable);
        }
    }

    public void SetupNameAndButton(string newNameString, bool isButtonUsable, InventoryItem newItem)
    {
        currentItem = newItem;
        nameText.text = newNameString;
        if (useButton != null)
        {
            useButton.SetActive(isButtonUsable);
        }
    }

    public void ClearInventorySlots()
    {
        for(int i = 0; i < inventoryPanel.transform.childCount; i++)
        {
            Destroy(inventoryPanel.transform.GetChild(i).gameObject);
        }
    }

    public void useButtonPressed()
    {
        if(currentItem)
        {
            currentItem.Use();
            //clear all of the inventory slots
            ClearInventorySlots();
            //refill all slots with new numbers
            MakeInventorySlots();
            if (currentItem.numberHeld == 0)
            {
                SetTextAndButton("", false);
                SetNameAndButton("", false);
            }
        }
    }
}
