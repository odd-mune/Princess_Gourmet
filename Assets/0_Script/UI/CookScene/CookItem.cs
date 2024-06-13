using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeadowGames.UINodeConnect4;

public abstract class CookItem : Node
{
    // public Node node;

    // public virtual bool Output { get; set; }

    protected virtual void OnEnable()
    {
        // node = GetComponent<Node>();
    }

    // public List<bool> inputs = new List<bool>();

    // public List<Node> solved = new List<Node>();

    // public virtual void GetInputs()
    // {
    //     inputs = new List<bool>();

    //     foreach (Port port in node.ports)
    //     {
    //         if (port.Polarity == Port.PolarityType._in)
    //         {
    //             bool result = false;
    //             foreach (Port otherPort in port.GetConnectedPorts())
    //             {
    //                 if (otherPort.node.GetComponent<CookItem>().Output == true)
    //                 {
    //                     result = true;
    //                     break;
    //                 }
    //             }
    //             inputs.Add(result);
    //         }
    //     }
    // }

    // public virtual void Solve() { }
}