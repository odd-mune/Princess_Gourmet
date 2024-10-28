using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class PhysicalInventoryItem : MonoBehaviour
{
    [Tooltip("플레이어 인벤토리")]
    [SerializeField] private PlayerInventory playerInventory;
    [Tooltip("대응하는 아이템")]
    [SerializeField] private InventoryItem inventoryItem;
    [Tooltip("아이템 픽업 메시지로, 아이템과 닿았을 때 띄워주는 메시지이다.")]
    [SerializeField] TMP_Text pickUpText;   // from Item

    [System.Serializable]
    public class PickUpableInfo
    {
        public bool day;
        public bool night;
    };

    [Tooltip("아이템이 언제 픽업 가능한지 여부.")]
    public PickUpableInfo pickUpableInfo;
    private bool mIsPickUpable;
    protected CheckToday mCheckToday;

    public UnityEvent onPickUpableEnabled;
    public UnityEvent onPickUpableDisabled;

    public bool isPickUpable
    {
        get { bool isDay = mCheckToday.IsDay(); return mIsPickUpable && ((isDay && pickUpableInfo.day) || (isDay == false && pickUpableInfo.night)); }
        protected set { mIsPickUpable = value; if (isPickUpable == true) { onPickUpableEnabled.Invoke(); } else { onPickUpableDisabled.Invoke(); } }
    }

    protected void Start()
    {
        mCheckToday = FindObjectOfType<CheckToday>();
        isPickUpable = true;

        onStart();
    }

    protected virtual void onStart() { }

    private bool AddItemToInventory()
    {
        if(playerInventory && inventoryItem)
        {
            if(playerInventory.myInventory.Contains(inventoryItem))
            {
                inventoryItem.numberHeld += 1;
                return true;
            }
            else
            {
                playerInventory.myInventory.Add(inventoryItem);
                inventoryItem.numberHeld = 1;
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
        return inventoryItem.itemName;
    }

    public string GetDescription()
    {
        return inventoryItem.itemDescription;
    }
}
