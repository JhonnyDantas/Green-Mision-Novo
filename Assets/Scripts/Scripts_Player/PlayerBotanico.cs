using UnityEngine;

public class PlayerBotanico : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    public float speed;


    private float moveX = 0;
    private float moveY = 0;

    // Variáveis para armazenar a última direção de movimento
    private float lastMoveX;
    private float lastMoveY;

    public static PlayerBotanico instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        // Vetor de movimento com base nos inputs atuais
        Vector2 move = new Vector2(moveX, moveY);

        // Atualiza os parâmetros da animação
        anim.SetFloat("Horizontal", move.x);
        anim.SetFloat("Vertical", move.y);
        anim.SetFloat("Speed", move.magnitude);

        // Atualiza a última direção de movimento
        if (move.magnitude > 0)
        {
            lastMoveX = move.x;
            lastMoveY = move.y;
        }

        // Define os valores para Idle
        anim.SetFloat("LestHorizontal", lastMoveX);
        anim.SetFloat("LestVertical", lastMoveY);

        
        
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, Input.GetAxisRaw("Vertical") * speed);
        
        
    }



    // Métodos para os botões: pressionar e soltar
    public void MoveUpStart() { moveX = 0; moveY = 1; }
    public void MoveDownStart() { moveX = 0; moveY = -1; }
    public void MoveLeftStart() { moveX = -1; moveY = 0; }
    public void MoveRightStart() { moveX = 1; moveY = 0; }
    public void StopMovement() { moveX = 0; moveY = 0; }
}
