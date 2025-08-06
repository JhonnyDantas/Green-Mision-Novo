using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerEscalada : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        

    }
    void Update()
    {

    }
    // Update is called once per frame
   void OnCollisionEnter2D(Collision2D Pedra) 
   {
        if (Pedra.gameObject.CompareTag("Player"))
        {
            Pedra.gameObject.GetComponent<Rigidbody2D>().gravityScale = -1;
        }
   }
    void OnCollisionExit2D(Collision2D Pedra)
    {
        if (Pedra.gameObject.CompareTag("Player"))
        {
            Pedra.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }
    
}
