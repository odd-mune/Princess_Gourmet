using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System.Collections.Generic;
using static UnityEditor.Progress;
using System.Linq;

public class PhysicalInventoryItem : MonoBehaviour
{
    [System.Serializable]
    public class PickUpableItemInfo
    {
        [Tooltip("드랍할 아이템")]
        public InventoryItem InventoryItem;
        [Tooltip("아이템이 나올 가중치 (다른 아이템들에 비해 값이 클수록 드랍 확률이 높아짐). 값이 0일 경우 절대 드랍되지 않음.")]
        public int Weight;
        private float mActualWeight;

        public float actualWeight
        {
            get { return mActualWeight; }
            set { mActualWeight = value; }
        }
    };

    [Tooltip("플레이어 인벤토리")]
    [SerializeField] private PlayerInventory playerInventory;
    [Tooltip("대응하는 아이템")]
    [SerializeField] private List<PickUpableItemInfo> pickUpableItemInfo;
    [Tooltip("아이템 픽업 메시지로, 아이템과 닿았을 때 띄워주는 메시지이다.")]
    [SerializeField] TMP_Text pickUpText;   // from Item
    [Tooltip("이름")]
    [SerializeField] private string physicalInventoryItemName;
    [Tooltip("플레이어가 가까이 갔을 때 띄울 설명란")]
    [SerializeField] private string physicalInventoryItemDescription;

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

        if (pickUpableItemInfo.Count == 0)
        {
            Debug.LogError($"{name} {physicalInventoryItemName} 드랍 가능한 아이템 목록이 비어있습니다!!");
            Debug.Break();
        }

        onStart();
    }

    protected virtual void onStart() { }

    private bool AddItemToInventory(InventoryItem inventoryItem)
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
            int totalWeight = 0;
            foreach (var itemInfo in pickUpableItemInfo)
            {
                totalWeight += itemInfo.Weight;
            }

            if (totalWeight > 0)
            {
                foreach (var itemInfo in pickUpableItemInfo)
                {
                    itemInfo.actualWeight = (float)itemInfo.Weight / (float)totalWeight;
                }

                float value = UnityEngine.Random.Range(0.0f, 1.0f);

                InventoryItem item = null;
                float accumulatedProbability = 0.0f;
                foreach (var itemInfo in pickUpableItemInfo)
                {
                    accumulatedProbability += itemInfo.actualWeight;
                    if (value < accumulatedProbability)
                    {
                        item = itemInfo.InventoryItem;
                        break;
                    }
                }

                if (item == null)
                {
                    item = pickUpableItemInfo[pickUpableItemInfo.Count - 1].InventoryItem;
                }

                return AddItemToInventory(item);
            }
        }

        return false;
    }

    public string GetName()
    {
        return physicalInventoryItemName;
    }

    public string GetDescription()
    {
        return physicalInventoryItemDescription;
    }
}
