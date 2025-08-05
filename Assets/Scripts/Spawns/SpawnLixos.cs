using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLixos : MonoBehaviour
{
    [Header ("Pontos de Spawns")]
    public Transform[] pontos; // Pontos de spawn pré-definidos


    [Header ("Obejtos de Spawn")]
    public GameObject[] lixosPrefab; // Lista de prefabs que podem ser instanciados
    public GameObject objetoEspecialPrefab; // Prefab do objeto especial


    [Header ("Tempo para Spawnar")]
    public float timerSpawn; // Tempo entre os spawns
    public float delayObjetoEspecial; // Tempo antes de instanciar o objeto especial


    [Header ("Boleanos")]   
    private bool[] pontosOcupados; // Controle dos pontos ocupados
    private bool objetoEspecialInstanciado = false; // Controle para garantir que o objeto especial só seja instanciado uma vez


    void Start()
    {
        pontosOcupados = new bool[pontos.Length]; // Inicializa o array como vazio (false)
        StartCoroutine(SpawnInimigos());
        StartCoroutine(SpawnObjetoEspecial());
    }

    IEnumerator SpawnInimigos()
    {
        while (true)
        {
            // Verifica se há pelo menos um ponto livre
            if (ExistemPontosLivres())
            {
                // Escolhe um ponto livre aleatoriamente
                int pontoAleatorio = EscolherPontoLivre();

                // Escolhe aleatoriamente um prefab de lixo
                int lixoAleatorio = Random.Range(0, lixosPrefab.Length);

                // Pega a posição do ponto selecionado
                Vector3 spawnPosition = pontos[pontoAleatorio].position;

                // Instancia o objeto no ponto selecionado
                GameObject lixo = Instantiate(lixosPrefab[lixoAleatorio], spawnPosition, Quaternion.identity);

                // Marca o ponto como ocupado
                pontosOcupados[pontoAleatorio] = true;

                // Libera o ponto quando o objeto for destruído
                StartCoroutine(LiberarPontoQuandoDestruído(lixo, pontoAleatorio));
            }

            // Aguarda antes de tentar instanciar novamente
            yield return new WaitForSeconds(timerSpawn);
        }
    }

    IEnumerator SpawnObjetoEspecial()
    {
        // Aguarda o tempo definido antes de instanciar o objeto especial
        yield return new WaitForSeconds(delayObjetoEspecial);

        // Garante que o objeto especial só será instanciado uma vez
        if (!objetoEspecialInstanciado && ExistemPontosLivres())
        {
            // Escolhe um ponto livre aleatoriamente
            int pontoAleatorio = EscolherPontoLivre();

            // Pega a posição do ponto selecionado
            Vector3 spawnPosition = pontos[pontoAleatorio].position;

            // Instancia o objeto especial no ponto selecionado
            Instantiate(objetoEspecialPrefab, spawnPosition, Quaternion.identity);

            // Marca o ponto como ocupado
            pontosOcupados[pontoAleatorio] = true;

            // Define que o objeto especial já foi instanciado
            objetoEspecialInstanciado = true;
        }
    }

    private bool ExistemPontosLivres()
    {
        foreach (bool ocupado in pontosOcupados)
        {
            if (!ocupado) return true;
        }
        return false;
    }

    private int EscolherPontoLivre()
    {
        List<int> pontosLivres = new List<int>();

        for (int i = 0; i < pontosOcupados.Length; i++)
        {
            if (!pontosOcupados[i])
                pontosLivres.Add(i);
        }

        return pontosLivres[Random.Range(0, pontosLivres.Count)];
    }

    IEnumerator LiberarPontoQuandoDestruído(GameObject lixo, int pontoIndex)
    {
        while (lixo != null)
        {
            yield return null; // Aguarda o próximo quadro enquanto o objeto existe
        }

        // Quando o objeto for destruído, libera o ponto
        pontosOcupados[pontoIndex] = false;
    }
}
