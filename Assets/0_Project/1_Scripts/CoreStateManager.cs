using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreStateManager : MonoBehaviour {   
    [Range(0,  100)] public int police = 10, folk = 30;

    [Header("Skill")]
    public int skillCount = 3;
    public float skillTime = 100f;
}
