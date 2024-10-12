using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum ItemType
{
    Ingredient,
    Tool,
    MagicCircleIngredients,
    MagicCircleCookType,
}

public enum CookType
{
    StirFry,
    Roast,
    Boil,
    
}

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items")]
public class InventoryItem : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public ItemType itemType;
    public Sprite itemImage;
    public Sprite magicCircleImageOrNull;
    public int numIngredients;
    public List<CookType> cookTypes;
    public int numberHeld;
    public bool usable;
    public bool unique;
    public UnityEvent thisEvent;

    public void Use()
    {
        if (numberHeld > 0)
        {
            thisEvent.Invoke();
        }
    }

    public void DecreaseAmount(int amountToIncrease)
    {
        numberHeld -= amountToIncrease;
        if (numberHeld < 0)
        {
            numberHeld = 0;
        }
    }
}
