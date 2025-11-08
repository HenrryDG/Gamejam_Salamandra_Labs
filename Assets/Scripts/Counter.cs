using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Counter : MonoBehaviour
{
    public TMP_Text timerText;
    public float timeRemaining = 90f; // 1 minuto y 30 segundos
    private bool isRunning = true;

    void Update()
    {
        if (isRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                isRunning = false;
                TimerFinished();
            }
        }
    }

    void UpdateTimerDisplay(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void TimerFinished()
    {
        
        Debug.Log("Tiempo agotado!");
        // Si TrapCounter no es igual a 5 reiniciar el nivel, recargar la escena
        if (TrapCounter.instance.trampasRecogidas < TrapCounter.instance.totalTrampas)
        {
            SceneManager.LoadScene("Nivel_1");
        }

    }
}
