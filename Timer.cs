using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;
    public static Timer Instance;
    public bool timeout = false;
    private float accumulatedTime = 0f;
    #region Singleton
    private void Awake()
    {
        Instance = this;
    }
    #endregion





    private void Update()
    {
        if (remainingTime > 0)
        {
            accumulatedTime += Time.deltaTime;
            if (accumulatedTime >= 1f)
            {
                remainingTime -= 1f;
                accumulatedTime = 0f;

                int minutes = Mathf.FloorToInt(remainingTime / 60);
                int seconds = Mathf.FloorToInt(remainingTime % 60);
                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

                if (remainingTime == 0)
                {
                    timeout = true;
                }
            }
        }
        else
        {
            remainingTime = 0;
            timerText.color = Color.red;
        }
    }
}