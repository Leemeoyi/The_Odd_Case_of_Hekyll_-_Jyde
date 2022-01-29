using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputEventManager : MonoBehaviour
{
    public KeyCode pauseKey;
    public UnityEvent resumeEvents, pauseEvents;

    public KeyCode continueKey;
    public UnityEvent continueEvents;

    void Update()
    {
        pauseCase();
        continueCase();
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

    void continueCase()
    {
        if (Input.GetKeyDown(continueKey))
        {
            continueEvents.Invoke();
        }
    }
}
