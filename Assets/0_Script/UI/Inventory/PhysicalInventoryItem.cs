using UnityEngine;
using TMPro;

public class PhysicalInventoryItem : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private InventoryItem thisItem;
    [SerializeField] TMP_Text pickUpText;   // from Item
    private bool isPickable;                          // from Item 

    private void AddItemToInventory()
    {
        if(playerInventory && thisItem)
        {
            if(playerInventory.myInventory.Contains(thisItem))
            {
                thisItem.numberHeld += 1;
            }
            else
            {
                playerInventory.myInventory.Add(thisItem);
                thisItem.numberHeld = 1;
            }
        }
    }

    public void PickUp()
    {
        AddItemToInventory();
    }
}
