using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    public Animator animator;

    private Rigidbody2D rig;
    private Vector2 lastDirection;

    public int currentWater; // Quantidade de água atual
    public int maxWater; // Limite de água do regador


    private bool isRegando = false; // Verifica se o jogador está regando
    private bool isBatendo = false; // Verifica se o jogador está batendo

    public static PlayerMove instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    void FixedUpdate()
    {
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * speed;
        rig.velocity = movement;

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", rig.velocity.sqrMagnitude);

        if (movement.magnitude > 0.1f)
        {
            lastDirection = movement;
            animator.SetFloat("LestHorizontal", lastDirection.x);
            animator.SetFloat("LestVertical", lastDirection.y);
        }

       Vector3 position = transform.position;

        if (position.x < -27f)
        {
            position.x = -27f; // Mantém o jogador dentro do limite esquerdo
        }

        if (position.x > 45f)
        {
            position.x = 45f; // Mantém o jogador dentro do limite direito
        }

        if (position.y < -8f)
        {
            position.y = -8f; // Mantém o jogador dentro do limite direito
        }

        transform.position = position;
    }

    public void StartReguar()
    {
        if (currentWater > 0 && !isRegando) // Só começa se tiver água e não estiver regando
        {
            animator.SetBool("Reguando", true);
            speed = 0;
            isRegando = true;

            // Inicia o consumo de água
            StartCoroutine(ConsumeWater());
        }

    }

    public void StopReguar()
    {
        animator.SetBool("Reguando", false);
        speed = 4f;
        isRegando = false; // Para o estado de regar
    }

    public void StartBatendo()
    {
        if (!isBatendo) // Só começa se tiver água e não estiver regando
        {
            animator.SetBool("Batendo", true);
            speed = 0;
            isBatendo = true;
        }
        
    }

    public void StopBatendo()
    {
        animator.SetBool("Batendo", false);
        speed = 4f;
        isBatendo = false; // Para o estado de bater
    }

    private IEnumerator ConsumeWater()
    {
        while (isRegando && currentWater > 0) // Consome água enquanto o botão estiver pressionado
        {
            currentWater--;
            yield return new WaitForSeconds(1f); // Consome água a cada segundo
        }

        if (currentWater <= 0)
        {
            StopReguar(); // Para de regar quando a água acabar
        }
    }

    // Método para adicionar água
    public void AddWater(int amount)
    {
        currentWater = Mathf.Clamp(currentWater + amount, 0, maxWater); // Garante que não ultrapasse o limite
    }

    public void ResetSpeed()
    {
        speed = 0;
    }
    public void ReturnSeed()
    {
        speed = 4f;
    }

}
