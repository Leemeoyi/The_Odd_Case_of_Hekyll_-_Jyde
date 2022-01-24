using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowniesManager : MonoBehaviour
{
    CoreManager cm;
    [SerializeField] int townfolkCount = 30;
    [SerializeField] int policeCount = 10;

    [SerializeField] GameObject townfolkPrefab;
    [SerializeField] GameObject policePrefab;
    
    List<Townfolk> townfolks = new List<Townfolk>();
    //List<Townfolk> polices = new List<Townfolk>();

    NodeManager nm;


    private void Awake()
    {
       
    }

    private void Start()
    {
        nm = NodeManager.instance;

        for (int i = 0; i < townfolkCount; i++)
        {
            GameObject temp = Instantiate(townfolkPrefab, nm.Nodes[Random.Range(0, nm.Nodes.Count)].transform.position, Quaternion.identity);
        }
    }

    public void KillTownfolk()
    {

    }
}
