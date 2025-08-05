using System.Collections;
using UnityEngine;

public class SpawnInimigo : MonoBehaviour
{
    public Transform[] pontos;
    public GameObject inimigoPrefab;
    public float timerSpawn;


    void Start()
    {
        StartCoroutine(SpawnInimigos());
    }

    IEnumerator SpawnInimigos()
    {
        while (true)
        {
            int pontoAleatorio = Random.Range(0, pontos.Length);

            GameObject inimigo = Instantiate(inimigoPrefab, pontos[pontoAleatorio].position, Quaternion.identity);

            yield return new WaitForSeconds(timerSpawn);
        }
    }
}