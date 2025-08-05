using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class cameraEscalada : MonoBehaviour
{
    public Transform posicaoPlayer;
    public float tamanho;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector2.Lerp(transform.position,posicaoPlayer.position, 0.03f);
    }

     void Update()  
    {
        PlayerMorrer();
    }

    void PlayerMorrer()
    {
        if(posicaoPlayer.position.y <= transform.position.y - tamanho)
        {
            SceneManager.LoadScene("GameOverEScalada");
        }
    }

    private void OnDrawGizmos() 
    {
        Gizmos.DrawRay(transform.position - Vector3.up * tamanho, transform.right);
    }




}
