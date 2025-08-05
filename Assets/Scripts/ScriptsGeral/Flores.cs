using System.Collections;
using UnityEngine;

public class Flores : MonoBehaviour
{
    private Animator anim;
    private bool isRegando = false;
    private bool isMorta = false;

    public float TimeMinMorta = 3f;
    public float TimeMaxMorta = 5f;
    public float reguarTime = 5f;
    private float currentReguarTime;

    private Coroutine regarCoroutine;
    private ScoreManager scoreManager;

    public int totalPontos = 100;
    public GameObject balao;

    void Start()
    {
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogError("Animator não encontrado no GameObject " + gameObject.name);
            return;
        }

        scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager não encontrado!");
        }

        anim.Play("FlorAmarela_Animi");
        balao.SetActive(false);

        StartCoroutine(StartLifeCycle());
    }

    private IEnumerator StartLifeCycle()
    {
        while (true)
        {
            float timeToMorta = Random.Range(TimeMinMorta, TimeMaxMorta);
            yield return new WaitForSeconds(timeToMorta);
            SetMorta(true);
            yield return new WaitUntil(() => !isMorta);
        }
    }

    private void SetMorta(bool morta)
    {
        isMorta = morta;
        balao.SetActive(morta);
        anim.SetBool("Morta", isMorta);

        if (isMorta)
        {
            currentReguarTime = reguarTime;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Reguar") && isMorta && !isRegando)
        {
            regarCoroutine = StartCoroutine(RegarCoroutine());
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Reguar") && isRegando)
        {
            StopCoroutine(regarCoroutine);
            isRegando = false;
        }
    }

    private IEnumerator RegarCoroutine()
    {
        isRegando = true;

        while (currentReguarTime > 0f)
        {
            currentReguarTime -= Time.deltaTime;
            yield return null;
        }

        if (scoreManager != null)
        {
            scoreManager.AddScore(totalPontos);
            Debug.Log($"Flor {gameObject.name} somou {totalPontos} pontos.");
        }

        SetMorta(false);
        balao.SetActive(false);
        isRegando = false;
    }
}
