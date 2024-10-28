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
    [Tooltip("아이템 이름.")]
    public string itemName;
    [Tooltip("아이템 설명. 인벤토리 설명란에 띄울 메세지임.")]
    public string itemDescription;
    [Tooltip("아이템 타입. 재료냐, 요리 도구냐, 재료 마법진이냐, 요리 방법 마법진이냐.")]
    public ItemType itemType;
    [Tooltip("아이템 썸네일 스프라이트.")]
    public Sprite itemImage;
    [Tooltip("(마법진) 요리 UI에 배경에 그릴 스프라이트.")]
    public Sprite magicCircleImageOrNull;
    [Tooltip("(재료 마법진) 재료 개수.")]
    public int numIngredients;
    [Tooltip("현재 보유 개수.")]
    public int numberHeld;
    [Tooltip("소비 가능한 아이템인지 여부.")]
    public bool usable;
    [Tooltip("유니크 아이템인지 여부. 유니크 아이템은 게임에 한 개 밖에 존재하지 않는다.")]
    public bool unique;
    [Tooltip("소비 시 호출할 이벤트.")]
    public UnityEvent eventOnUse;

    public void Use()
    {
        if (numberHeld > 0)
        {
            eventOnUse.Invoke();
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
