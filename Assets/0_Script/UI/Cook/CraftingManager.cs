using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour
{
    private InventorySlot currentItemSlot;
    public Image customCursor;

    public Slot[] craftingSlots;

    public List<InventorySlot> itemSlotList;
    public string[] recipes;
    public InventoryItem[] recipeResults;
    public Slot resultSlot;

    private void Start()
    {
        itemSlotList = new List<InventorySlot>();
        for(int i = 0; i < craftingSlots.Length; i++)
        {
            craftingSlots[i].index = i;
            itemSlotList.Add(null);
        }
    }

    private void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            if(currentItemSlot != null)
            {
                customCursor.gameObject.SetActive(false);
                Slot nearestSlot = null;
                float shortestDistance = float.MaxValue;

                foreach(Slot slot in craftingSlots)
                {
                    float dist = Vector2.Distance(Input.mousePosition, slot.transform.position);

                    if(dist < shortestDistance)
                    {
                        shortestDistance = dist;
                        nearestSlot = slot;
                    }
                }
                nearestSlot.gameObject.SetActive(true);
                nearestSlot.GetComponent<Image>().sprite = currentItemSlot.thisItem.itemImage;
                nearestSlot.item = currentItemSlot.thisItem;
                itemSlotList[nearestSlot.index] = currentItemSlot;

                //아이템 사용시 횟수 감소
                currentItemSlot.thisItem.DecreaseAmount(1);
                currentItemSlot.thisManager.ClearInventorySlots();
                currentItemSlot.thisManager.MakeInventorySlots();
                currentItemSlot = null;
            }
        }
    }

    public void OnClose()
    {
        for(int slotIndex = 0; slotIndex < itemSlotList.Count; slotIndex++)
        {
            if(itemSlotList[slotIndex] != null)
            {
                itemSlotList[slotIndex].thisItem.numberHeld++;
            }
            itemSlotList[slotIndex] = null;

            craftingSlots[slotIndex].GetComponent<Image>().sprite = null;
            craftingSlots[slotIndex].item = null;
        }
    }

    void CheckForCreatedRecipes()
    {
        resultSlot.gameObject.SetActive(false);
        resultSlot.item = null;

        string currentRecipeString = "";
        foreach(InventorySlot itemSlot in itemSlotList)
        {
            InventoryItem item = itemSlot.thisItem;
            if(item != null)
            {
                currentRecipeString += item.itemName;
            }
            else
            {
                currentRecipeString += "null";
            }
        }

        for (int i = 0; i < recipes.Length; i++)
        {
            if(recipes[i] == currentRecipeString)
            {
                resultSlot.gameObject.SetActive(true);
                //resultSlot.GetComponent<Image>().sprite = recipeResults[i].GetComponent<Image>().sprite;
                resultSlot.item = recipeResults[i];
            }
        }
    }

    public void OnClickSlot(Slot slot)
    {
        slot.item = null;
        itemSlotList[slot.index] = null;
        slot.gameObject.SetActive(false);
        CheckForCreatedRecipes();
    }

    public void OnMouseDownItem(InventorySlot itemSlot)
    {
        if(currentItemSlot == null)
        {
            currentItemSlot = itemSlot;
            customCursor.gameObject.SetActive(true);
            customCursor.sprite = currentItemSlot.thisItem.itemImage;
        }
    }
}
