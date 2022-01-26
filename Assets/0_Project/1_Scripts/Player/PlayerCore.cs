using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCore : MonoBehaviour
{
    public int potionNum = 3;
    bool justUsedPotion = false;
    public bool JustUsedPotion
    {
        get => justUsedPotion;
        set => justUsedPotion = value;
    }

    [SerializeField] float potionTime;
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
        get => timer;
        set => timer = value;
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

    private void Update()
    {
        if (isHeckyll && Input.GetKeyDown(KeyCode.E))
        {
            Destroy(CollidedObj);
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
    
}
