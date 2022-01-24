using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCore : MonoBehaviour
{
    public int potionNum = 3;
    bool isHeckyll = false;

    GameObject collidedObj;
    public bool IsHeckyll
    {
        get => isHeckyll;
        set => isHeckyll = value;
    }

    public GameObject CollidedObj
    {
        get => collidedObj;
    }

    private void Update()
    {
        if (isHeckyll && Input.GetKeyDown(KeyCode.E))
        {
            Destroy(CollidedObj);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("folk"))
        {
            collidedObj = col.gameObject;
        }

        if (col.gameObject.CompareTag("police"))
        {
            gameObject.SetActive(false);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("folk"))
        {
            collidedObj = null;
        }
    }

}
