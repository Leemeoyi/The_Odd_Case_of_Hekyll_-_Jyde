using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TNRD.Utilities;
using UnityEngine;

public class Node : MonoBehaviour
{
    NodeManager nodeManager;
    
    [SerializeField] List<Node> connectedNode;

    public List<Node> ConnectedNode
    {
        get => connectedNode;
    }


    public void OnCreate(GameObject go)
    {
        this.gameObject.SetIcon(ShapeIcon.CircleBlue);
        nodeManager = GetComponentInParent<NodeManager>();
        connectedNode = new List<Node>();
        connectedNode.Add(go.GetComponent<Node>());
    }

    private void OnValidate()
    {
        nodeManager = GetComponentInParent<NodeManager>();
    }

    public void OnCreate()
    {
        this.gameObject.SetIcon(ShapeIcon.CircleBlue);
        nodeManager = GetComponentInParent<NodeManager>();
    }
    

    [Button("Add Node", enabledMode: EButtonEnableMode.Editor)]
    void AddNode()
    {
        if (connectedNode == null)
        {
            connectedNode = new List<Node>();
        }
        GameObject temp = nodeManager.AddNode(this.gameObject);

        connectedNode.Add(temp.GetComponent<Node>());
    }

    void OnDrawGizmosSelected()
    {
        if (connectedNode != null)
        {
            foreach (var go in connectedNode)
            {
                Debug.DrawLine(this.transform.position, go.transform.position, Color.blue);
            }
        }
    }

    public void Draw()
    {
        foreach (var go in connectedNode)
        {
            Debug.DrawLine(this.transform.position, go.transform.position, Color.blue);
        }
    }
}
