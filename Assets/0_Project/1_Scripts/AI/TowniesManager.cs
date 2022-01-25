using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TowniesManager : MonoBehaviour
{
    public static TowniesManager instance;
    
    CoreManager cm;
    [SerializeField] int townfolkCount = 30;
    [SerializeField] int policeCount = 10;

    [SerializeField] GameObject townfolkPrefab;
    [SerializeField] GameObject policePrefab;
    
    List<GameObject> townfolks = new List<GameObject>();
    List<GameObject> polices = new List<GameObject>();

    NodeManager nm;


    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        nm = NodeManager.instance;

        int rand = 0;
        int prevRand = 0;
        
        for (int i = 0; i < townfolkCount; i++)
        {
            bool pass = false;
            do
            {
                rand = Random.Range(0, nm.Nodes.Count);

                if (i == 0 || prevRand != rand)
                {
                    GameObject temp = Instantiate(townfolkPrefab, nm.Nodes[rand].transform.position, Quaternion.identity);
                    prevRand = rand;
                    pass = true;
                }
                
            } while (pass != true);
        }
        
        for (int i = 0; i < policeCount; i++)
        {
            bool pass = false;
            do
            {
                rand = Random.Range(0, nm.Nodes.Count);

                if (i == 0 || prevRand != rand)
                {
                    GameObject temp = Instantiate(policePrefab, nm.Nodes[rand].transform.position, Quaternion.identity);
                    prevRand = rand;
                    pass = true;
                }
                
            } while (pass != true);
        }
    }

    //! add police and remove townfolk
    public void KillTownfolk(GameObject deadFolk)
    {
        townfolks.Contains(deadFolk);
        
        int rand = 0;
        
        rand = Random.Range(0, nm.Nodes.Count);
        GameObject temp = Instantiate(policePrefab, nm.Nodes[rand].transform.position, Quaternion.identity);
        
        polices.Add(temp);
    }
    
    
}
