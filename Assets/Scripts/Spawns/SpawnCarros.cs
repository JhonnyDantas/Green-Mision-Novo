using System.Collections.Generic;
using UnityEngine;

public class SpawnCarro : MonoBehaviour
{
    [Header("Configurações")]
    public float spawnInterval = 2f; // Tempo entre os spawns em segundos
    public List<GameObject> CarroList; // Lista de prefabs de carros
    public List<Transform> CarroSpawn; // Lista de pontos de spawn (Transform)

    private float timer; // Temporizador para controlar o spawn

    void Update()
    {
        HandleSpawnTimer();
    }

    void HandleSpawnTimer()
    {
        // Incrementa o temporizador com base no tempo
        timer += Time.deltaTime;

        // Verifica se o temporizador atingiu o intervalo definido
        if (timer >= spawnInterval)
        {
            Spawn(); // Instancia um carro
            timer = 0f; // Reseta o temporizador
        }
    }

    void Spawn()
    {
        if (CarroList.Count > 0 && CarroSpawn.Count > 0)
        {
            // Seleciona aleatoriamente um prefab de carro da lista
            GameObject carroPrefab = CarroList[Random.Range(0, CarroList.Count)];

            // Seleciona aleatoriamente um ponto de spawn da lista
            Transform spawnPoint = CarroSpawn[Random.Range(0, CarroSpawn.Count)];

            // Instancia o carro no ponto de spawn com rotação padrão
            GameObject carro = Instantiate(carroPrefab, spawnPoint.position, spawnPoint.rotation);

            // Adiciona o script Carro e passa o TaskManager para ele
            CarrosController carroScript = carro.AddComponent<CarrosController>();
    
        }
    }
}