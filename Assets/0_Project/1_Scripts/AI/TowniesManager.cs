using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowniesManager : MonoBehaviour
{
    CoreManager cm;
    [SerializeField] int townfolkCount = 30;
    [SerializeField] int policeCount = 10;

    List<Townfolk> townfolks = new List<Townfolk>();

    private void Awake()
    {
       
    }

    private void Start()
    {
        for (int i = 0; i < townfolkCount; i++)
        {

        }
    }
}
