using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspetorController : MonoBehaviour
{
    public float speed = 2f;
    public float waitTime = 1f;
    public List<Transform> paths = new List<Transform>();
    
    private int index = 0;
    private bool isMoving = false;

    void Start()
    {
        StartCoroutine(MoveBetweenPoints());
    }

    IEnumerator MoveBetweenPoints()
    {
        while (true)
        {
            Vector2 targetPosition = paths[index].position;

            // Move até o ponto de destino
            while (Vector2.Distance(transform.position, targetPosition) > 0.05f)
            {
                if (!DialogueController.instance.isShowing)
                {
                    Vector2 position = transform.position;
                    Vector2 direction = (targetPosition - position).normalized;

                    // Move apenas horizontal ou vertical por vez
                    if (Mathf.Abs(targetPosition.x - position.x) > 0.05f)
                    {
                        position.x = Mathf.MoveTowards(position.x, targetPosition.x, speed * Time.deltaTime);
                    }
                    else if (Mathf.Abs(targetPosition.y - position.y) > 0.05f)
                    {
                        position.y = Mathf.MoveTowards(position.y, targetPosition.y, speed * Time.deltaTime);
                    }

                    transform.position = position;

                    // Inverter direção do sprite
                    if (direction.x > 0.1f)
                        transform.eulerAngles = new Vector2(0, 0);
                    else if (direction.x < -0.1f)
                        transform.eulerAngles = new Vector2(0, 180);
                }

                yield return null;
            }

            // Garante que o personagem pare exatamente no ponto
            transform.position = targetPosition;

            // Espera antes de ir ao próximo ponto (só conta se não estiver em diálogo)
            float timer = 0f;
            while (timer < waitTime)
            {
                if (!DialogueController.instance.isShowing)
                {
                    timer += Time.deltaTime;
                }
                yield return null;
            }

            // Próximo ponto (loop infinito)
            index = (index + 1) % paths.Count;
        }
    }
}
