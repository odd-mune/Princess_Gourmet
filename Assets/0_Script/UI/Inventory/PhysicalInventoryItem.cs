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

    public virtual void PickUp()
    {
        if (isPickUpable == true)
        {
            AddItemToInventory();
        }
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
