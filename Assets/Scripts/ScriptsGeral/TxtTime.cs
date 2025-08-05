using UnityEngine;
using UnityEngine.UI;

public class TxtTime : MonoBehaviour
{
    public Text timeDisplay; // Arraste e solte um Text UI do Unity para exibir o tempo

    private int hours = 0; // Representa as horas
    private int minutes = 0; // Representa os minutos
    private float timer = 0f; // Um timer para contar os segundos

    void Update()
    {
        timer += Time.deltaTime; // Incrementa o timer com base no tempo real

        if (timer >= 1f) // Quando 1 segundo passar
        {
            timer = 0f; // Reseta o timer
            IncrementTime(); // Atualiza o tempo
        }

        UpdateDisplay(); // Atualiza a exibição
    }

    private void IncrementTime()
    {
        minutes++; // Incrementa os minutos

        if (minutes >= 60) // Se os minutos ultrapassarem 59
        {
            minutes = 0; // Reseta os minutos
            hours++; // Incrementa as horas

            if (hours >= 24) // Se as horas ultrapassarem 23
            {
                hours = 0; // Reseta as horas
            }
        }
    }

    private void UpdateDisplay()
    {
        // Formata o tempo no formato HH:MM
        string formattedTime = string.Format("{0:00}:{1:00}", hours, minutes);

        // Atualiza o texto na UI
        if (timeDisplay != null)
        {
            timeDisplay.text = formattedTime;
        }
        else
        {
            Debug.Log(formattedTime); // Caso não tenha UI, mostra no console
        }
    }
}
