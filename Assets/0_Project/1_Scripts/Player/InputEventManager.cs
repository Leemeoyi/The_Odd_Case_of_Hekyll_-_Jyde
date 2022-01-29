using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputEventManager : MonoBehaviour
{
    public KeyCode pauseKey;
    public UnityEvent resumeEvents, pauseEvents;

    void Update()
    {
        pauseCase();
    }

    void pauseCase()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            if (Time.timeScale == 0f)
            {
                resumeEvents.Invoke();
            }
            else
            {
                pauseEvents.Invoke();
            }
        }
    }
}
