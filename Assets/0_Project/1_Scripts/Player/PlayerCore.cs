using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCore : MonoBehaviour
{
    public int potionNum = 3;

    bool justUsedPotion = false;

    [SerializeField] float potionTimer;
    
    float timer = 0f;
    public float Timer => timer;

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
            timer -= Time.deltaTime;
        }
        
    }
    
}
