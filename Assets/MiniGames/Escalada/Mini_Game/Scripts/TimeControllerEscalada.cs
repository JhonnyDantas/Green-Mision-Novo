using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControllerEscalada: MonoBehaviour
{
    public TMPro.TextMeshProUGUI Tempo;
    private float tempoEmSegundos;
    private string tempoFormatado;
    public bool final;
    public string FormatTempo(float tempo)
    {
        int minutos = Mathf.FloorToInt(tempo / 60);
        int segundos = Mathf.FloorToInt(tempo % 60);

        return string.Format("{0:00}:{1:00}", minutos, segundos);
    }

    IEnumerator ContarTempo()
    {
        if(!final)
        {
            while(final == false)
            {
                Tempo.text = tempoFormatado;
                tempoFormatado = FormatTempo(tempoEmSegundos);
                yield return new WaitForSeconds(1f);
                tempoEmSegundos += 1;
            }
        }
    }

    void Start() 
    {
         StartCoroutine(ContarTempo()); 
    }

}
