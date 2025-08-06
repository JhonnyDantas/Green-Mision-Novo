using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    [SerializeField] private Ball ball;
    [SerializeField] private Paddle playerPaddle;
    [SerializeField] private Paddle computerPaddle;
    [SerializeField] private Text playerScoreText;
    [SerializeField] private Text computerScoreText;
    public GameObject PainelgameOver;
    public GameObject PainelMudarNivel;
    private int playerScore;
    private int computerScore;
    private  int diferenca;
    public int maximoPontos;
    private void Start()
    {
        NewGame();
         Time.timeScale = 1f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) {
            NewGame();
        }
        EncerrarPartida();
        diferenca = playerScore - computerScore;
    }

    public void NewGame()
    {
        SetPlayerScore(0);
        SetComputerScore(0);
        NewRound();
    }

    public void NewRound()
    {
        playerPaddle.ResetPosition();
        computerPaddle.ResetPosition();
        ball.ResetPosition();

        CancelInvoke();
        Invoke(nameof(StartRound), 1f);
    }

    private void StartRound()
    {
        ball.AddStartingForce();
    }

    public void OnPlayerScored()
    {
        SetPlayerScore(playerScore + 1);
        NewRound();
    }

    public void OnComputerScored()
    {
        SetComputerScore(computerScore + 1);
        NewRound();
    }

    private void SetPlayerScore(int score)
    {
        playerScore = score;
        playerScoreText.text = score.ToString();
    }

    private void SetComputerScore(int score)
    {
        computerScore = score;
        computerScoreText.text = score.ToString();
    }

    private void EncerrarPartida()
    {
            if(diferenca >= 2  )
            {
                if(playerScore >= maximoPontos)
                {
                    PainelMudarNivel.SetActive(true);
                    PlayerPrefs.SetString("fase", SceneManager.GetActiveScene().name);
                    Time.timeScale = 0f; 
                }
               
            }
            else if(diferenca <= -2  )
            {
                if( computerScore >= maximoPontos)
                {
                    PainelgameOver.SetActive(true);  
                    Time.timeScale = 0;
                }
              
            }
    }

    //    if(playerScore >= 6 && computerScore <= 4)
    //       {
    //     PainelMudarNivel.SetActive(true);
    //    }
    //    else if(computerScore >= 6 && playerScore <=4 )
    //    {
    //     PainelgameOver.SetActive(true);
    //    }
    }
