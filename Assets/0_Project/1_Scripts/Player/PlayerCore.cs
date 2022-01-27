using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCore : MonoBehaviour
{
    public static PlayerCore instance;
    public int potionNum = 3;
    public float radiusSize = 1.5f;
    
    bool justUsedPotion = false;
    public bool JustUsedPotion
    {
        get => justUsedPotion;
        set => justUsedPotion = value;
    }

    [SerializeField] float potionTime;
    public float PotionTimer
    {
        get => potionTime;
    }
    [SerializeField] float transformationTime;

    float timer = 0f;
    public float Timer
    {
        get => timer;
        set => timer = value;
    }

    float transformationTimer = 0f;
    public float TransformationTimer
    {
        get => transformationTimer;
        set => transformationTimer = value;
    }

    bool isHeckyll = true;

    GameObject collidedObj;
    public bool IsHeckyll
    {
        get => isHeckyll;
        set => isHeckyll = value;
    }

    public GameObject CollidedObj
    {
        get => collidedObj;
        set => collidedObj = value;
    }

    [Header("Event")]
    public UnityEvent killEvents;

    // method
    void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else
            instance = this;
    }

    private void Update()
    {
        if (isHeckyll && Input.GetKeyDown(KeyCode.E) && collidedObj != null)
        {
            TowniesManager.instance.KillTownfolk(collidedObj);
            killEvents.Invoke();
        }

        if (justUsedPotion)
        {
            if (timer >= 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                justUsedPotion = false;
            }
        }
    }

    public void TransformingToJyde()
    {
        timer = potionTime;
        transformationTimer = transformationTime;
        justUsedPotion = true;
        isHeckyll = false;
        potionNum--;
    }

    public void TransformingToHeckyll()
    {
        isHeckyll = true;
        justUsedPotion = false;
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusSize);


    }
}
