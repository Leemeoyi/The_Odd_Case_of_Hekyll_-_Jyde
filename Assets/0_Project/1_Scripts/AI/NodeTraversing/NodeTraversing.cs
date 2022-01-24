using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NodeTraversing : MonoBehaviour
{
    [SerializeField] List<GameObject> nodes;
    public List<GameObject> Nodes { get => nodes; }

    [Button("Generate Node", enabledMode: EButtonEnableMode.Editor)]
    void GenerateNode()
    {
        if (nodes != null)
        {
            foreach (GameObject gameObject in nodes)
            {
                DestroyImmediate(gameObject);
            }
            nodes.Clear();
        }
        
        nodes = new List<GameObject>();
        
        GameObject temp = new GameObject("Node" + nodes.Count, typeof(Node));
        temp.transform.SetParent(this.transform);
        temp.GetComponent<Node>().OnCreate();
        nodes.Add(temp);
    }
    
    public GameObject AddNode(GameObject go)
    {
        GameObject temp = new GameObject("Node" + nodes.Count, typeof(Node));
        temp.transform.SetParent(this.transform);
        temp.GetComponent<Node>().OnCreate(temp);
        temp.GetComponent<Node>().ConnectedNode.Add(go);
        
        nodes.Add(temp);
        
        return temp;
    }
    
    void UpdateNodes()
    {
        
    }

}
