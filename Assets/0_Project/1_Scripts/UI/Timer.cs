using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Timer : MonoBehaviour
{
    Text timer;
    float time = 0f;

    private void Awake() {
        timer = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timer.text = timeFormater(time);
    }

    string timeFormater(float currentTime) {
        int hours = TimeSpan.FromSeconds(currentTime).Hours;
        int minutes = TimeSpan.FromSeconds(currentTime).Minutes;
        int secs = TimeSpan.FromSeconds(currentTime).Seconds;

        string format = hours > 0
            ? "{0:00}:{1:00}:{2:00}"
            : "{1:00}:{2:00}";

        return string.Format(format, hours, minutes, secs);
    }
}
