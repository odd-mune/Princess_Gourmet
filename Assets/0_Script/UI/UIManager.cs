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
            Debug.Log($"Collid?ing {currentCollidingItems.Count}");
            SetGameObjectToShowTooptip(currentCollidingItems);
        }
        else if (currentPickUpObjects.Count > 0)
        {
            Debug.Log($"Pick?up {currentPickUpObjects.Count}");
            SetGameObjectToShowTooptip(currentPickUpObjects);
        }
        else
        {
            Tooltip.gameObject.SetActive(false);
            mGameObjectToShowTooltipOrNull = null;
        }
    }

    private void SetGameObjectToShowTooptip(List<GameObject> objects)
    {
        GameObject gameObjectToShowTooltip = objects[0];
        if (mGameObjectToShowTooltipOrNull != gameObjectToShowTooltip)
        {
            if (mGameObjectToShowTooltipOrNull != null)
            {
                Tooltip.gameObject.SetActive(false);
            }
            mGameObjectToShowTooltipOrNull = gameObjectToShowTooltip;
            PhysicalInventoryItem itemToShowTooltip = mGameObjectToShowTooltipOrNull.GetComponent<PhysicalInventoryItem>();
            Tooltip.SetName(itemToShowTooltip.GetName());
            Tooltip.SetDescription(itemToShowTooltip.GetDescription());
            Tooltip.gameObject.SetActive(true);
        }

        if (mGameObjectToShowTooltipOrNull != null)
        {
            Vector3 playerPosition = Player.transform.position;
            Vector3 itemPosition = mGameObjectToShowTooltipOrNull.transform.position;
            //Vector3 tooltipPosition = mTooltipRootPosition + Vector3.Scale((itemPosition - playerPosition), new Vector3((Screen.width / 64), (Screen.height / 64),  1.0f));
            //Debug.Log($"{Tooltip.transform.position} {playerPosition} {itemPosition} diff: {itemPosition - playerPosition} {tooltipPosition}");
            Vector3 playerScreenSpacePosition = mPlayerCamera.WorldToScreenPoint(playerPosition);
            Vector3 itemScreenSpacePosition = mPlayerCamera.WorldToScreenPoint(itemPosition);
            float y = Mathf.Max(playerScreenSpacePosition.y, itemScreenSpacePosition.y);
            Vector3 tooltipPosition = new Vector3(itemScreenSpacePosition.x, y + ((RectTransform)Tooltip.transform).rect.height, Tooltip.transform.position.z);
            Tooltip.transform.position = tooltipPosition;
        }
    }
}
