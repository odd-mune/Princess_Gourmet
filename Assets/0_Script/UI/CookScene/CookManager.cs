using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MeadowGames.UINodeConnect4;
using TMPro;
using System.Linq;
using MeadowGames.UINodeConnect4.UICContextMenu;
using UnityEditor.MemoryProfiler;

public class CookManager : MonoBehaviour
{
    public GraphManager graphManager;
    public PlayerInventory playerInventory;
    public Node toolTemplate;
    public Node ingredientTemplate;
    public PauseInvetoryManager PauseInventoryManager;
    public Port toolPortInTemplate;
    // public Node pointNode;
    public ContextMenuManager ContextMenu;
    public PauseCookManager PauseCookManager;

    // [SerializeField] Toggle toggleMovePoints;
    // [SerializeField] Toggle toggleAddPoints;
    // [SerializeField] Toggle toggleHelp;

    private List<InventoryItem> mInventoryItems;
    private List<Node> mNodes;

    private void Start()
    {
        mInventoryItems = new List<InventoryItem>();
        mNodes = new List<Node>();
    }

    void OnEnable()
    {
        UICSystemManager.AddToUpdate(OnUpdate);
        if (ContextMenu == null)
        {
            ContextMenu = GetComponentInParent<ContextMenuManager>();
        }
    }

    void OnDisable()
    {
        foreach (InventoryItem item in mInventoryItems)
        {
            if (item.unique == false)
            {
                item.numberHeld += 1;
            }

            if (item.numberHeld == 1)
            {
                playerInventory.myInventory.Add(item);
            }
        }
        mInventoryItems.Clear();

        foreach (Node node in mNodes)
        {
            node.Remove();
        }
        mNodes.Clear();

        while (graphManager.localConnections.Count > 0)
        {
            graphManager.localConnections[0].Remove();
            graphManager.localConnections.RemoveAt(0);
        }

        UICSystemManager.RemoveFromUpdate(OnUpdate);
    }

    public void Solve()
    {
        //     for (int i = 0; i < graphManager.localNodes.Count; i++)
        //     {
        //         SolveRecursive(graphManager.localNodes[i]);
        //     }

        foreach (InventoryItem item in mInventoryItems)
        {
            if (item.itemType == ItemType.Ingredient)
            {
                continue;
            }

            if (item.unique == false)
            {
                item.numberHeld += 1;
            }

            if (item.numberHeld == 1)
            {
                playerInventory.myInventory.Add(item);
            }
        }
        mInventoryItems.Clear();

        foreach (Node node in mNodes)
        {
            node.Remove();
        }
        mNodes.Clear();

        while (graphManager.localConnections.Count > 0)
        {
            graphManager.localConnections[0].Remove();
        }
    }

    // // recursive solve allows gate loops and locks 
    // void SolveRecursive(Node node)
    // {
    //     Gate gate = node.GetComponent<Gate>();
    //     foreach (Node otherNode in node.GetNodesConnectedToPolarity(Port.PolarityType._in))
    //     {
    //         if (!gate.solved.Contains(otherNode))
    //         {
    //             gate.solved.Add(otherNode);
    //             SolveRecursive(otherNode);
    //         }
    //     }

    //     gate.Solve();
    // }

    void UpdateConnectionsState()
    {
        // foreach (Connection connection in graphManager.localConnections)
        // {
        //     if (connection.port0.node.GetComponent<Gate>().Output)
        //     {
        //         connection.line.animation.isActive = true;
        //     }
        //     else
        //     {
        //         connection.line.animation.isActive = false;
        //     }
        // }
    }

    void OnUpdate()
    {
        // Solve();

    //     HandleMovePoints();

    //     HandleCreateAndDestroyPoints();
    }

    public void CreateNode(InventoryItem item)
    {
        PauseInventoryManager.ChangePause(true);

        mInventoryItems.Add(item);

        Node nodeTemplate = null;
        switch (item.itemType)
        {
            case ItemType.Ingredient:
            {
                nodeTemplate = ingredientTemplate;
            }
            break;
            case ItemType.Tool:
            {
                nodeTemplate = toolTemplate;
            }
            break;
            default:
            break;
        }

        Node nodeToInstantiate = ContextMenu.GraphManager.InstantiateNode(nodeTemplate, nodeTemplate.transform.position + new Vector3(10, 10, 0));
        Image image = nodeToInstantiate.GetComponent<Image>();
        image.sprite = item.itemImage;

        TextMeshProUGUI text = nodeToInstantiate.GetComponentInChildren<TextMeshProUGUI>();
        // text.text = item.itemName;
        text.text = item.itemName;

        if (item.itemType == ItemType.Tool)
        {
            Port[] ports = nodeToInstantiate.GetComponentsInChildren<Port>();
            uint portIndex = 0;
            foreach (CookType cookType in item.cookTypes)
            {
                Port port = null;
                do
                {
                    port = ports[portIndex++];
                } while (port.Polarity == Port.PolarityType._out && (portIndex < ports.Length));
                TextMeshProUGUI cookTypeText = port.GetComponentInChildren<TextMeshProUGUI>();
                switch (cookType)
                {
                    case CookType.StirFry:
                        {
                            cookTypeText.text = "볶기";
                        }
                        break;
                    case CookType.Roast:
                        {
                            cookTypeText.text = "굽기";
                        }
                        break;
                    case CookType.Boil:
                        {
                            cookTypeText.text = "삶기";
                        }
                        break;
                    default:
                        break;
                }
            }

            for (; portIndex < ports.Length; ++portIndex)
            {
                Port port = ports[portIndex++];
                port.gameObject.SetActive(false);
            }
        }

        nodeToInstantiate.gameObject.SetActive(true);

        mNodes.Add(nodeToInstantiate);
        ContextMenu.UpdateContextMenu();
    }
}