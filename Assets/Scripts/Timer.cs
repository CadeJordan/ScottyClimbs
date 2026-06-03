using UnityEngine;
using Time = UnityEngine.Time;
using TMPro;
using System;
using System.IO;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[System.Serializable]
public class BestTimeData
{
    public int minutes;
    public int seconds;
    public int milliseconds;
    public float totalTime;
}



public class Timer : MonoBehaviour
{
    public float time = 0;
    public int minutes = 0;
    public int seconds = 0;
    public int milliseconds = 0;
    public TextMeshProUGUI timer;
    public Collider targetTrigger;
    private bool timerRunning = true;
    public InputActionReference buttonPressReference;
    
    private string bestTimeFilePath;
    private BestTimeData bestTimeData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bestTimeFilePath = Application.persistentDataPath + "/bestTime.json";
        LoadBestTime();
    }

    private void OnEnable()
    {
        if (buttonPressReference != null && buttonPressReference.action != null)
        {
            buttonPressReference.action.performed += OnButtonPress;
        }
    }

    private void OnDisable()
    {
        if (buttonPressReference != null && buttonPressReference.action != null)
        {
            buttonPressReference.action.performed -= OnButtonPress;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timerRunning)
        {
            time += Time.deltaTime;
            minutes = (int)(time/60);
            seconds = (int)(time%60);
            milliseconds = (int)((time - Math.Floor(time))*1000);
            timer.text = $"{minutes:00}:{seconds:00}:{milliseconds:000}";
        }
    }

    // Stop timer when entering the target trigger volume
    void OnTriggerEnter(Collider collision)
    {
        if (collision == targetTrigger)
        {
            timerRunning = false;
            SaveBestTime();
        }
    }

    void LoadBestTime()
    {
        if (File.Exists(bestTimeFilePath))
        {
            string json = File.ReadAllText(bestTimeFilePath);
            bestTimeData = JsonUtility.FromJson<BestTimeData>(json);
            Debug.Log($"Best time loaded: {bestTimeData.minutes:00}:{bestTimeData.seconds:00}:{bestTimeData.milliseconds:000}");
        }
        else
        {
            bestTimeData = new BestTimeData { minutes = 0, seconds = 0, milliseconds = 0, totalTime = float.MaxValue };
            Debug.Log("No best time file found. Starting fresh.");
        }
    }

    void SaveBestTime()
    {
        if (bestTimeData == null || time < bestTimeData.totalTime)
        {
            bestTimeData = new BestTimeData
            {
                minutes = minutes,
                seconds = seconds,
                milliseconds = milliseconds,
                totalTime = time
            };

            string json = JsonUtility.ToJson(bestTimeData, true);
            File.WriteAllText(bestTimeFilePath, json);
            Debug.Log($"New best time saved: {bestTimeData.minutes:00}:{bestTimeData.seconds:00}:{bestTimeData.milliseconds:000}");
        }
    }

    private void OnButtonPress(InputAction.CallbackContext callback)
    {
        SceneManager.LoadScene("SampleScene");
        this.enabled = false;
    }
}
