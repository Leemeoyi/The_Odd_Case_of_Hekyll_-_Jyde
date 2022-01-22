using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoreManager : MonoBehaviour
{
    static CoreManager instance {
        get {
            if (!instance) {
                instance = new CoreManager();
            }
            return instance;
        }
        set {}
    }

    public static float playTime = 0f;
    
    [Header("UI")]
    public Text policeText;
    public Text folkText;

    [Header("Stat")]
    [SerializeField, Range(0,  100)] int police;
    int policeNum  {
        get { return police; }
        set {
            policeText.text = value.ToString();
            police = value;
        }
    }

    [SerializeField, Range(0,  100)] int folk;
    int folkNum {
        get { return folk; }
        set {
            folkText.text = value.ToString();
            folk = value;
        }
    }

    void Start() {
        policeText.text = police.ToString();
        folkText.text = folk.ToString();
    }
}
