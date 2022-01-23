using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreStateManager : MonoBehaviour {   
    [Min(0)] [Tooltip("start from 0")]
    public int police = 10, folk = 30;

    [Header("Skill")]
    public int skillCount = 3;
    [Tooltip("in sec")]
    public float skillTime = 100f;
}
