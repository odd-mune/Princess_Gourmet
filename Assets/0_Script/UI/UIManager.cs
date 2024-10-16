using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Tooltip Tooltip;
    public GameObject Player;

    private GameObject mGameObjectToShowTooltipOrNull;
    private Vector3 mTooltipRootPosition;
    private Camera mPlayerCamera;

    // Start is called before the first frame update
    void Start()
    {
        mTooltipRootPosition = Tooltip.transform.position;
        mPlayerCamera = Player.gameObject.transform.GetChild(0).GetComponent<Camera>();
    }

    // FixedUpdate is called once per frame
    void FixedUpdate()
    {
        List<GameObject> currentCollidingItems = Player.GetComponent<PlayerManager>().GetCurrentCollidingItems();
        List<GameObject> currentPickUpObjects = Player.GetComponent<PlayerManager>().GetCurrentPickUpObjects();
        if (currentCollidingItems.Count > 0)
        {
            SetGameObjectToShowTooltip(currentCollidingItems);
        }
        else if (currentPickUpObjects.Count > 0)
        {
            SetGameObjectToShowTooltip(currentPickUpObjects);
        }
        else
        {
            Tooltip.gameObject.SetActive(false);
            mGameObjectToShowTooltipOrNull = null;
        }
    }

    private void SetGameObjectToShowTooltip(List<GameObject> objects)
    {
        if (mGameObjectToShowTooltipOrNull != null)
        {
            PhysicalInventoryItem itemOrNull = mGameObjectToShowTooltipOrNull.GetComponent<PhysicalInventoryItem>();
            if (itemOrNull != null && itemOrNull.isPickUpable == false)
            {
                Tooltip.gameObject.SetActive(false);
                mGameObjectToShowTooltipOrNull = null;
            }
        }

        int numObjects = objects.Count;
        for (int i = 0; i < numObjects; ++i)
        {
            GameObject gameObjectToShowTooltip = objects[i];
            PhysicalInventoryItem itemOrNull = gameObjectToShowTooltip.GetComponent<PhysicalInventoryItem>();
            if (itemOrNull != null && itemOrNull.isPickUpable == false)
            {
                continue;
            }

            if (mGameObjectToShowTooltipOrNull != gameObjectToShowTooltip)
            {
                if (mGameObjectToShowTooltipOrNull != null)
                {
                    Tooltip.gameObject.SetActive(false);
                }
                mGameObjectToShowTooltipOrNull = gameObjectToShowTooltip;
                PhysicalInventoryItem itemToShowTooltip = mGameObjectToShowTooltipOrNull.GetComponent<PhysicalInventoryItem>();
                if (itemToShowTooltip.isPickUpable)
                {
                    Tooltip.SetName(itemToShowTooltip.GetName());
                    Tooltip.SetDescription(itemToShowTooltip.GetDescription());
                    Tooltip.gameObject.SetActive(true);
                }
            }

            if (mGameObjectToShowTooltipOrNull != null)
            {
                Vector3 playerPosition = Player.transform.position;
                Vector3 itemPosition = mGameObjectToShowTooltipOrNull.transform.position;
                Vector3 playerScreenSpacePosition = mPlayerCamera.WorldToScreenPoint(playerPosition);
                Vector3 itemScreenSpacePosition = mPlayerCamera.WorldToScreenPoint(itemPosition);
                float y = Mathf.Max(playerScreenSpacePosition.y, itemScreenSpacePosition.y);
                Vector3 tooltipPosition = new Vector3(itemScreenSpacePosition.x, y + ((RectTransform)Tooltip.transform).rect.height, Tooltip.transform.position.z);
                Tooltip.transform.position = tooltipPosition;
            }
            break;
        }
    }
}
