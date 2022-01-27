using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseHandler : MonoBehaviour
{
    SpriteRenderer sr;
    bool isBehind;
    float timeSinceStarted;
    float timestartLerping;
    [SerializeField] float lerpTime;
    
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isBehind)
        {
            sr.color = new Color(1f, 1f, 1f, Lerp(sr.color.a, 0.5f, timestartLerping, lerpTime));
        }
        else
        {
            sr.color = new Color(1f, 1f, 1f, Lerp(sr.color.a, 1f, timestartLerping, lerpTime));
        }
        

    }

    float Lerp(float start, float end, float timeStartedLerping, float lerpTime)
    {
        float timeSinceStarted = Time.time - timestartLerping;

        float percentageComplete = timeSinceStarted / lerpTime;

        float result = Mathf.Lerp(start, end, percentageComplete);

        return result;
    }
    

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            isBehind = true;
            timestartLerping = Time.time;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isBehind = false;
            timestartLerping = Time.time;
        }
    }
}
