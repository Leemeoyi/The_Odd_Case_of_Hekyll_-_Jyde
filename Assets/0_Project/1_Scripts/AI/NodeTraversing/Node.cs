using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
#if UNITY_EDITOR
using TNRD.Utilities;
#endif
using UnityEngine;

public class Node : MonoBehaviour
{
    NodeManager nodeManager;

    [SerializeField] List<Node> connectedNode;

    [SerializeField] float nodeRadius = 1f;
    public float NodeRadius { get => nodeRadius; }

    public List<Node> ConnectedNode
    {
        get => connectedNode;
    }


    public void OnCreate(GameObject go)
    {
#if UNITY_EDITOR
        this.gameObject.SetIcon(ShapeIcon.CircleBlue);
#endif
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
#if UNITY_EDITOR
        this.gameObject.SetIcon(ShapeIcon.CircleBlue);
#endif
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

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, nodeRadius);
    }

    public void Draw()
    {
        foreach (var go in connectedNode)
        {
            Debug.DrawLine(this.transform.position, go.transform.position, Color.blue);
        }
    }
}
