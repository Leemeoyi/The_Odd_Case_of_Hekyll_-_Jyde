using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Events;

public class TowniesManager : MonoBehaviour
{
    public static TowniesManager instance;

    CoreManager cm;

    [SerializeField] AudioData audioData;

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

    List<Police> pursuiter = new List<Police>();


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
        AudioManager.instance.PlayBGM(audioData, "Calm");

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
                    Vector2 spawnPos = nm.Nodes[rand].transform.position;
                    spawnPos += Random.insideUnitCircle * nm.Nodes[rand].NodeRadius;
                    
                    GameObject temp = Instantiate(townfolkPrefab, spawnPos, Quaternion.identity);
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
                    Vector2 spawnPos = nm.Nodes[rand].transform.position;
                    spawnPos += Random.insideUnitCircle * nm.Nodes[rand].NodeRadius;
                    
                    GameObject temp = Instantiate(policePrefab, spawnPos, Quaternion.identity);
                    temp.name = "Police " + i;
                    prevRand = rand;
                    polices.Add(temp);
                    pass = true;
                }
            } while (pass != true);
        }
    }

    public void AddPursuiter(Police popo)
    {
        if (pursuiter.Contains(popo))
            return;
        
        pursuiter.Add(popo);
        CheckPoliceChase();
    }

    public void RemovePursuiter(Police popo)
    {
        pursuiter.Remove(popo);
        CheckPoliceChase();
    }
    
    void CheckPoliceChase()
    {
        if (pursuiter.Any())
        {
            if (AudioManager.instance.BGM_Source.clip.name != "HeckyllandJyde_ChaseLoop")
            {
                AudioManager.instance.BGM_Source.Stop();
                AudioManager.instance.PlayBGM(audioData, "Chase");
            }
        }
        else
        {
            if (AudioManager.instance.BGM_Source.clip.name != "HeckyllandJyde_Loop")
            {
                AudioManager.instance.BGM_Source.Stop();
                AudioManager.instance.PlayBGM(audioData, "Calm");
            }
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
