using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTransition : MonoBehaviour
{
    public Animator transition; // O Animator responsável pelas animações de transição

    private static GameTransition instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }



    public void Transition(string nomeCena)
    {
        StartCoroutine(TransicaoCena(nomeCena));
    }

    private IEnumerator TransicaoCena(string nomeCena)
    {
        // Dispara a animação de escurecimento
        transition.SetTrigger("Start");

        // Aguarda a duração da animação "Fade_Start"
        yield return new WaitForSeconds(1f); // Ajuste para o tempo exato da sua animação de escurecimento

        // Carrega a nova cena
        SceneManager.LoadScene(nomeCena);

        // Pequeno delay para garantir que a nova cena esteja carregada
        yield return new WaitForSeconds(0.1f);

        // Dispara a animação de clareamento
        transition.SetTrigger("End");

        // Aguarda a duração da animação "Fade_End"
        yield return new WaitForSeconds(1f); // Ajuste para o tempo exato da sua animação de clareamento
    }
}
