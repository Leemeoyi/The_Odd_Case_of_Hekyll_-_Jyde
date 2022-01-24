using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Timer : MonoBehaviour
{
    Text timer;
    bool isStop = false;

    private void Awake()
    {
        timer = GetComponent<Text>();
    }

    void Update()
    {
        if (!isStop)
        {
            CoreManager.playTime += Time.deltaTime;
            timer.text = timeFormater(CoreManager.playTime);
        }
    }

    string timeFormater(float currentTime)
    {
        int hours = TimeSpan.FromSeconds(currentTime).Hours;
        int minutes = TimeSpan.FromSeconds(currentTime).Minutes;
        int secs = TimeSpan.FromSeconds(currentTime).Seconds;
        string format = hours > 0
            ? "{0:00}:{1:00}:{2:00}"
            : "{1:00}:{2:00}";

        return string.Format(format, hours, minutes, secs);
    }

    public void stop()
    {
        isStop = true;
    }
}