using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Player Inventory")]
public class PlayerInventory : ScriptableObject 
{
    public List<InventoryItem> myInventory = new List<InventoryItem>();

    public void Clear()
    {
        //myInventory.Clear();
        for (int i = 0; i < myInventory.Count;)
        {
            InventoryItem item = myInventory[i];

            if (item.itemType != ItemType.MagicCircleIngredients && item.itemType != ItemType.MagicCircleCookType)
            {
                item.numberHeld = 0;
                myInventory.RemoveAt(i);
            }
            else
            {
                ++i;
            }
        }
    }
}
