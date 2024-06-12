using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MeadowGames.UINodeConnect4.UICContextMenu
{
    public class AddNewNodeButtonItem : ContextItem
    {
        Button _button;
        public Node nodeTemplate;

        // v4.1 - DuplicateNodeButtonItem now duplicates connections if they are connected to selected nodes
        public void CreateNode()
        {
            // Dictionary<Port, Port> portMap = new Dictionary<Port, Port>();

            // for (int i = UICSystemManager.selectedElements.Count - 1; i >= 0; i--)
            // {
            //     Node nodeToDuplicate = UICSystemManager.selectedElements[i] as Node;
            //     if (nodeToDuplicate)
            //     {
            //         Node duplicatedNode = ContextMenu.GraphManager.InstantiateNode(nodeToDuplicate, nodeToDuplicate.transform.position + new Vector3(10, 10, 0));

            //         for (int portIndex = 0; portIndex < nodeToDuplicate.ports.Count; portIndex++)
            //         {
            //             portMap.Add(nodeToDuplicate.ports[portIndex], duplicatedNode.ports[portIndex]);
            //         }
            //     }
            // }

            // for (int i = UICSystemManager.selectedElements.Count - 1; i >= 0; i--)
            // {
            //     Connection connectionToDuplicate = UICSystemManager.selectedElements[i] as Connection;
            //     if (connectionToDuplicate != null)
            //     {
            //         Port p0 = portMap.TryGetValue(connectionToDuplicate.port0);
            //         Port p1 = portMap.TryGetValue(connectionToDuplicate.port1);
            //         if (p0 != null && p1 != null)
            //         {
            //             Connection duplicatedConnection = p0.ConnectTo(p1, connectionToDuplicate);
            //         }
            //     }
            // }
            // if (nodeTemplate == null)
            // {
            //     nodeTemplate.ID = "NewNode";
            //     nodeTemplate.enableSelfConnection = false;
            //     nodeTemplate.EnableDrag = true;
            //     nodeTemplate.EnableHover = true;
            //     nodeTemplate.EnableSelect = true;
            //     nodeTemplate.defaultColor = Color.cyan;
            //     nodeTemplate.outlineSelectedColor = Color.red;
            //     nodeTemplate.outlineHoverColor = Color.yellow;
            // }

            // if 후라이팬
            // {
            //     nodeTemplate.ID = "후라이팬";
            //     Port fryingPort = new Port();
            //     fryingPort.
            //     nodeTemplate.ports.Add()
            // }

            Node nodeToInstantiate = ContextMenu.GraphManager.InstantiateNode(nodeTemplate, nodeTemplate.transform.position + new Vector3(10, 10, 0));
            nodeToInstantiate.gameObject.SetActive(true);

            ContextMenu.UpdateContextMenu();
        }

        // public override void OnChangeSelection()
        // {
        //     int nodeCount = 0;
        //     for (int i = UICSystemManager.selectedElements.Count - 1; i >= 0; i--)
        //     {
        //         Node node = UICSystemManager.selectedElements[i] as Node;
        //         if (node)
        //         {
        //             nodeCount++;    
        //         }
        //     }
        //     gameObject.SetActive(nodeCount > 0);
        // }

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
