using System.Collections;
using UnityEngine;

public class Objetos : MonoBehaviour
{
    private Animator anim;
    private bool isQuebrado = false;
    private int hitsParaConsertar = 5;
    private int hitsRecebidos = 0;

    public float TimeMinNormal = 3f;
    public float TimeMaxNormal = 5f;
    public GameObject balao;

    void Start()
    {
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogError("Animator não encontrado no GameObject " + gameObject.name);
            return;
        }
        StartCoroutine(StartLifeCycle());
    }

    private IEnumerator StartLifeCycle()
    {
        while (true)
        {
            float timeToBreak = Random.Range(TimeMinNormal, TimeMaxNormal);
            yield return new WaitForSeconds(timeToBreak);
            SetQuebrado(true);
            yield return new WaitUntil(() => !isQuebrado);
        }
    }

    private void SetQuebrado(bool quebrado)
    {
        isQuebrado = quebrado;
        balao.SetActive(quebrado);
        anim.SetBool("Quebrado", isQuebrado);
       

        if (!isQuebrado)
        {
            hitsRecebidos = 0;
            StartCoroutine(StartLifeCycle());
            
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("C") && isQuebrado)
        {
            hitsRecebidos++;
            if (hitsRecebidos >= hitsParaConsertar)
            {
                 GameObject.Find("CaderninhoController").GetComponent<Caderninho>().MissaoBanco();
                SetQuebrado(false);
            }
        }
    }
}
