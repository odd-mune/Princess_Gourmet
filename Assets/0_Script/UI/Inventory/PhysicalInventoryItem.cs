using UnityEngine;
using TMPro;

public class PhysicalInventoryItem : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private InventoryItem thisItem;
    [SerializeField] TMP_Text pickUpText;   // from Item
    protected bool mIsPickUpable;

    public bool isPickUpable
    {
        get { return mIsPickUpable; }
        protected set { mIsPickUpable = value; }
    }

    void Start()
    {
        mIsPickUpable = true;
    }

    private bool AddItemToInventory()
    {
        if(playerInventory && thisItem)
        {
            if(playerInventory.myInventory.Contains(thisItem))
            {
                thisItem.numberHeld += 1;
                return true;
            }
            else
            {
                playerInventory.myInventory.Add(thisItem);
                thisItem.numberHeld = 1;
                return true;
            }
        }

        return false;
    }

    public virtual bool PickUp()
    {
        if (isPickUpable == true)
        {
            return AddItemToInventory();
        }

        return false;
    }

    public string GetName()
    {
        return thisItem.itemName;
    }

    public string GetDescription()
    {
        return thisItem.itemDescription;
    }
}
