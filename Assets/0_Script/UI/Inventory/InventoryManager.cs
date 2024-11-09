using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditorInternal.Profiling.Memory.Experimental;
using System;
public enum InventoryType
{
    Inventory,
    Ingredients,
    MagicCircle,
    Dish,
    Tool,
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
    private InventorySlot mCurrentFocusedSlot;
    private uint mCurrentFocusedSlotIndex = uint.MaxValue;
    private Dictionary<string, uint> mItemNameToIndex = new Dictionary<string, uint>();

    public void ToggleSelectedItem(InventorySlot slot)
    {
        if (mCurrentFocusedSlot == slot)
        {
            SetFocusOn(null);
        }
        else
        {
            SetFocusOn(slot);
        }

        if (currentItem != slot.thisItem)
        {
            SetupDescriptionAndButton(slot.thisItem.itemDescription, slot.thisItem.usable, slot.thisItem);
            SetupNameAndButton(slot.thisItem.itemName, slot.thisItem.usable, slot.thisItem);
        }
        else
        {
            SetupDescriptionAndButton("", false, null);
            SetupNameAndButton("", false, null);
        }
    }

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

    public void SetFocusOn(InventorySlot slot)
    {
        if (mCurrentFocusedSlot != slot)
        {
            if (slot != null)
            {
                uint index = uint.MaxValue;
                bool result = mItemNameToIndex.TryGetValue(slot.thisItem.itemName, out index);
                if (result == false)
                {
                    Debug.LogError($"Inventory에 item {slot.thisItem.itemName}이 없습니다!!");
                    Debug.Break();
                }
                mCurrentFocusedSlotIndex = index;
            }
            else
            {
                mCurrentFocusedSlotIndex = uint.MaxValue;
            }

            Transform highlighterTransform;
            if (mCurrentFocusedSlot != null)
            {
                highlighterTransform = mCurrentFocusedSlot.transform.GetChild(0);
                highlighterTransform.gameObject.SetActive(false);
            }
            mCurrentFocusedSlot = slot;
            if (mCurrentFocusedSlot != null)
            {
                highlighterTransform = mCurrentFocusedSlot.transform.GetChild(0);
                highlighterTransform.gameObject.SetActive(true);
            }
        }
    }

    public void MakeInventorySlots()
    {
        if(playerInventory)
        {
            uint index = 0;
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
                            if (playerInventory.myInventory[i].itemType != ItemType.MagicCircleIngredients
                                && playerInventory.myInventory[i].itemType != ItemType.MagicCircleCookType)
                            {
                                continue;
                            }
                            break;
                        case InventoryType.Dish:
                            if (playerInventory.myInventory[i].itemType != ItemType.Dish)
                            {
                                continue;
                            }
                            break;
                        case InventoryType.Tool:
                            if (playerInventory.myInventory[i].itemType != ItemType.Tool)
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
                    mItemNameToIndex.Add(playerInventory.myInventory[i].itemName, index);
                    if (index == mCurrentFocusedSlotIndex)
                    {
                        SetFocusOn(newSlot);
                    }
                    ++index;
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
        mItemNameToIndex.Clear();
    }

    public void useButtonPressed()
    {
        if(currentItem)
        {
            currentItem.Use();
            if (currentItem.numberHeld == 0)
            {
                SetTextAndButton("", false);
                SetNameAndButton("", false);
            }
            //clear all of the inventory slots
            ClearInventorySlots();
            //refill all slots with new numbers
            MakeInventorySlots();
        }
    }

    public void SetInventoryType(InventoryType inventoryType)
    {
        if (this.inventoryType == inventoryType)
        {
            inventoryType = InventoryType.Inventory;
        }

        ClearInventorySlots();
        this.inventoryType = inventoryType;
        MakeInventorySlots();
    }
}
