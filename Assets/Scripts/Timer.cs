using UnityEngine;
using Time = UnityEngine.Time;
using TMPro;
using System;
public class Timer : MonoBehaviour
{
    public float time = 0;
    public int minutes = 0;
    public int seconds = 0;
    public int milliseconds = 0;
    public TextMeshProUGUI timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        minutes = (int)(time/60);
        seconds = (int)(time%60);
        milliseconds = (int)((time - Math.Floor(time))*1000);
        timer.text = $"{minutes:00}:{seconds:00}:{milliseconds:000}";
    }
}
