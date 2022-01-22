using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TNRD.Utilities;
using UnityEngine;

public class Node : MonoBehaviour
{
    NodeTraversing nodeManager;
    
    [SerializeField] List<GameObject> connectedNode;

    public List<GameObject> ConnectedNode
    {
        get { return connectedNode; }
        set { connectedNode = value; }
    }


    public void OnCreate(GameObject go)
    {
        this.gameObject.SetIcon(ShapeIcon.CircleBlue);
        nodeManager = GetComponentInParent<NodeTraversing>();
        connectedNode = new List<GameObject>();
        connectedNode.Add(go);
    }

    public void OnCreate()
    {
        this.gameObject.SetIcon(ShapeIcon.CircleBlue);
        nodeManager = GetComponentInParent<NodeTraversing>();
    }
    

    [Button("Add Node", enabledMode: EButtonEnableMode.Editor)]
    void AddNode()
    {
        if (connectedNode == null)
        {
            connectedNode = new List<GameObject>();
        }
        
        GameObject temp = nodeManager.AddNode(this.gameObject);
        
        connectedNode.Add(temp);
    }

    void OnDrawGizmosSelected()
    {
        if (connectedNode != null)
        {
            foreach (GameObject go in connectedNode)
            {
                Debug.DrawLine(this.transform.position, go.transform.position, Color.blue);
            }
        }
    }

    public void Draw()
    {
        foreach (GameObject go in connectedNode)
        {
            Debug.DrawLine(this.transform.position, go.transform.position, Color.blue);
        }
    }
}
