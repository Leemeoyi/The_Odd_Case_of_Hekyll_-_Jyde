using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    public static NodeManager instance;

    [SerializeField] List<Node> nodes;
    public List<Node> Nodes { get => nodes; }

    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else
            instance = this;
    }

    [Button("Generate Node", enabledMode: EButtonEnableMode.Editor)]
    void GenerateNode()
    {
        if (nodes != null)
        {
            foreach (Node node in nodes)
            {
                DestroyImmediate(node.gameObject);
            }
            nodes.Clear();
        }
        
        nodes = new List<Node>();
        
        GameObject temp = new GameObject("Node" + nodes.Count, typeof(Node));
        temp.transform.SetParent(this.transform);
        temp.GetComponent<Node>().OnCreate();
        nodes.Add(temp.GetComponent<Node>());
    }
    
    public GameObject AddNode(GameObject node)
    {
        GameObject temp = new GameObject("Node" + nodes.Count, typeof(Node));
        temp.transform.SetParent(this.transform);
        temp.GetComponent<Node>().OnCreate(node);

        nodes.Add(temp.GetComponent<Node>());
        
        return temp;
    }
    
    void UpdateNodes()
    {
        
    }

}
