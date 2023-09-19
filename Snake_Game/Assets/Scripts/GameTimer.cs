using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float startTime;
    private bool isRunning = true;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        if (isRunning)
        {
            float t = Time.time - startTime;
            string minutes = ((int)t / 60).ToString();
            string seconds = (t % 60).ToString("f2");
            timerText.text = minutes + ":" + seconds;
        }
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void StartTimer()
    {
        isRunning = true;
        startTime = Time.time;
    }
    public void ResetAndStartTimer()
    {
        startTime = Time.time;
        isRunning = true;
    }
}
