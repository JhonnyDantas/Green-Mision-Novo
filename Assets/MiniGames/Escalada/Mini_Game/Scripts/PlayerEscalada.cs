using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEscalada : MonoBehaviour
{
    public TimeControllerEscalada timeController;
    Rigidbody2D rig;
    public float Speed;
    public float pular;
    private bool isHanging = false;
    public bool podePular = true;
    [SerializeField]public Animator anim;
    public SpriteRenderer sprite;
    public ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>(); 

    }

    // Update is called once per frame
    void Update()
    {
        if (!isHanging)
        {
            Anda();

        }
            Pular();
            
    }

    void Anda()
    {
        if(isHanging)
        {
            rig.velocity = new Vector2(0f* Speed,rig.velocity.y);
        }
        else
        {
            rig.velocity = new Vector2(Input.GetAxis("Horizontal")* Speed,rig.velocity.y);
        }

        if(Input.GetAxis("Horizontal")>0)
        {
            anim.SetBool("Andando", true);
            sprite.flipX = false;
        }
         
        if(Input.GetAxis("Horizontal")<0)
        {
            anim.SetBool("Andando", true);
            sprite.flipX = true;
        }
         
        if(Input.GetAxis("Horizontal") == 0)
        {
            anim.SetBool("Andando",false);
        }

    }
    
   void Pular()
   {
        if(podePular && Input.GetKeyDown(KeyCode.Space))
        {
            rig.velocity = Vector2.up * pular;
            anim.SetBool("Escalada",true);
        }else if(!podePular)
        {
            anim.SetBool("Escalada",false);
        }

   }
    
   void OnTriggerEnter2D(Collider2D other) 
   {
        if (other.gameObject.CompareTag("Pedra") && !Input.GetKeyDown(KeyCode.Space))
        {
            rig.velocity = new Vector2(0f, 0f);
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0f;
            isHanging = true;  
            podePular = true; 
            transform.position = other.transform.position;

            anim.SetBool("Pendurado", true);
        }
   
        if(other.gameObject.CompareTag("PowerUp"))
        {
            StartCoroutine(WaitAndPrint());
            other.gameObject.GetComponentInChildren<ParticleSystem>().Play();
            other.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
   
   
   
   
   
   }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pedra"))
        {
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 1.3f;
            isHanging = false;
            podePular = false;
            
            anim.SetBool("Escalar", true);
            anim.SetBool("Pendurado", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Plata"))
        {
            anim.SetBool("Escalar", false);
            anim.SetBool("Pendurado", false);
            anim.SetBool("Parado", true);
            timeController.final = true;
        }
        if(other.gameObject.CompareTag("Chao"))
        {
            podePular = true;
            anim.SetBool("Parado", true);
        }
    }

    private void OnCollisionExit2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Chao"))
        {
            podePular = false;
            anim.SetBool("Parado", false);
            timeController.final = false;
        }
    }
    IEnumerator WaitAndPrint()
    {
        pular = 15f;
        yield return new WaitForSeconds(3);
        pular = 9f; 
    }

}


