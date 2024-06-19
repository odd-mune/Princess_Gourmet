using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MeadowGames.UINodeConnect4.UICContextMenu
{
    public class AddNewIngredientButtonItem : ContextItem
    {
        [SerializeField]
        public enum IngredientType
        {
            Cook,
            StirFry,
        };

        [SerializeField]
        public struct IngredientCreateInfo
        {
            IngredientType    Type;
        };

        Button _button;
        public Node nodeTemplate;
        // [SerializeField]
        public IngredientCreateInfo ingredientCreateInfo;
        public PauseInvetoryManager PauseInventoryManager;

        // v4.1 - DuplicateNodeButtonItem now duplicates connections if they are connected to selected nodes
        public void CreateNode()
        {
            PauseInventoryManager.ChangePause(true);
            // Node nodeToInstantiate = ContextMenu.GraphManager.InstantiateNode(nodeTemplate, nodeTemplate.transform.position + new Vector3(10, 10, 0));
            // nodeToInstantiate.ports.Clear();
            // nodeToInstantiate.gameObject.SetActive(true);

            // ContextMenu.UpdateContextMenu();
        }

        protected override void Awake()
        {
            base.Awake();
            _button = GetComponent<Button>();
        }

        void OnEnable()
        {
            _button.onClick.AddListener(CreateNode);
        }

        void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}
