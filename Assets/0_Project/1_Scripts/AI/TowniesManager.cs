using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Events;

public class TowniesManager : MonoBehaviour
{
    public static TowniesManager instance;

    CoreManager cm;

    [SerializeField] int townfolksCount;
    public int TownfolksCount => townfolksCount;

    [SerializeField] int policesCount;
    public int PolicesCount => policesCount;


    [SerializeField] GameObject townfolkPrefab;
    [SerializeField] GameObject policePrefab;

    List<GameObject> townfolks = new List<GameObject>();

    public List<GameObject> Townfolks { get => townfolks; }
    [HideInInspector] public int currentFolkCount = 0;

    List<GameObject> polices = new List<GameObject>();
    public List<GameObject> Polices { get => polices; }

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
        currentFolkCount = townfolksCount;

        int rand = 0;
        int prevRand = 0;

        for (int i = 0; i < townfolksCount; i++)
        {
            bool pass = false;
            do
            {
                rand = Random.Range(0, nm.Nodes.Count);

                if (i == 0 || prevRand != rand)
                {
                    GameObject temp = Instantiate(townfolkPrefab, nm.Nodes[rand].transform.position, Quaternion.identity);
                    temp.name = "Townfolk " + i;
                    prevRand = rand;
                    townfolks.Add(temp);
                    pass = true;
                }
            } while (pass != true);
        }

        for (int i = 0; i < policesCount; i++)
        {
            bool pass = false;
            do
            {
                rand = Random.Range(0, nm.Nodes.Count);

                if (i == 0 || prevRand != rand)
                {
                    GameObject temp = Instantiate(policePrefab, nm.Nodes[rand].transform.position, Quaternion.identity);
                    temp.name = "Police " + i;
                    prevRand = rand;
                    polices.Add(temp);
                    pass = true;
                }
            } while (pass != true);
        }
    }

    //! add police and remove townfolk
    public void KillTownfolk(GameObject deadFolk)
    {
        townfolks.Remove(deadFolk);
        currentFolkCount--;

        int rand = 0;

        rand = Random.Range(0, nm.Nodes.Count);
        GameObject temp = Instantiate(policePrefab, nm.Nodes[rand].transform.position, Quaternion.identity);

        polices.Add(temp);
        Destroy(deadFolk);
    }
}
