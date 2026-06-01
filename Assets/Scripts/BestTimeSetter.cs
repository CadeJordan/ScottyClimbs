using UnityEngine;
using TMPro;
using System.IO;

[System.Serializable]
public class BestTimeData
{
    public int minutes;
    public int seconds;
    public int milliseconds;
    public float totalTime;
}

public class BestTimeSetter : MonoBehaviour
{
    public TextMeshProUGUI bestTimeDisplay;
    private string bestTimeFilePath;
    private BestTimeData bestTimeData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bestTimeFilePath = Application.persistentDataPath + "/bestTime.json";
        LoadAndDisplayBestTime();
    }

    void LoadAndDisplayBestTime()
    {
        if (File.Exists(bestTimeFilePath))
        {
            string json = File.ReadAllText(bestTimeFilePath);
            bestTimeData = JsonUtility.FromJson<BestTimeData>(json);
            if (bestTimeDisplay != null)
            {
                bestTimeDisplay.text = $"{bestTimeData.minutes:00}:{bestTimeData.seconds:00}:{bestTimeData.milliseconds:000}";
                Debug.Log($"Best time displayed: {bestTimeDisplay.text}");
            }
        }
        else
        {
            if (bestTimeDisplay != null)
            {
                bestTimeDisplay.text = "No times yet!";
            }
            Debug.Log("No best time file found.");
        }
    }
}
