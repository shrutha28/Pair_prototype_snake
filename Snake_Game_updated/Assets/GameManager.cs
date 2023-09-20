using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text timerText;
    public Text foodCountText;
    public float timer = 0f;
    public int foodCount = 0;
    public bool isGameRunning = false;

    void Update()
    {
        if (isGameRunning)
        {
            timer += Time.deltaTime;
            timerText.text = "Time: " + timer.ToString("F2");
        }
    }

    public void IncrementFoodCount()
    {
        foodCount++;
        foodCountText.text = "Food Count: " + foodCount;
    }

    public void StartGame()
    {
        isGameRunning = true;
        timer = 0f;
        foodCount = 0;
    }

    public void PauseGame()
    {
        isGameRunning = false;
    }
}
