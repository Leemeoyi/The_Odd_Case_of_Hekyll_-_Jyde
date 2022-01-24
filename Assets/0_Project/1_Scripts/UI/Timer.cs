using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Timer : MonoBehaviour
{
    Text timer;

    private void Awake()
    {
        timer = GetComponent<Text>();
    }

    void Update()
    {
        timer.text = TimeFormater(CoreManager.playTime);
    }

    string TimeFormater(float currentTime)
    {
        int hours = TimeSpan.FromSeconds(currentTime).Hours;
        int minutes = TimeSpan.FromSeconds(currentTime).Minutes;
        int secs = TimeSpan.FromSeconds(currentTime).Seconds;
        string format = hours > 0
            ? "{0:00}:{1:00}:{2:00}"
            : "{1:00}:{2:00}";

        return string.Format(format, hours, minutes, secs);
    }
}