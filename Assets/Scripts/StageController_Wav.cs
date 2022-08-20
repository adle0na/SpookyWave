using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StageController_Wav : MonoBehaviour
{
    [SerializeField]
    private CameraController_Wav _cameraController;
    [SerializeField]
    private TextMeshProUGUI      textCurrentScore;
    [SerializeField]
    private TextMeshProUGUI      textBestScore;

    [Header("OnOffObejcts")]
    [SerializeField]
    private GameObject pauseButton;
    [SerializeField]
    private GameObject gameOverView;
    [SerializeField]
    private GameObject startViewOff;
    [SerializeField]
    private GameObject startViewOn;
    [SerializeField]
    private GameObject player;

    private int           currentScore = 0;
    private int           bestScore    = 0;
    
    public bool        IsGameOver { private set; get; } = false;
    
    private IEnumerator Start()
    {
        bestScore = PlayerPrefs.GetInt("BestScore");
        textBestScore.text = $"<size=50>BEST</size>\n<size=100>{bestScore}</size>";
        
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameStart();
                
                yield break;
            }
            yield return null;
        }
    }

    private void GameStart()
    {
        player.SetActive(true);
        startViewOn.SetActive(true);
        startViewOff.SetActive(false);
    }
    
    public void GameOver()
    {
        ShakeCamera_Wav.Instance.OnShakeCamera(0.5f, 0.2f);
        
        IsGameOver = true;

        gameOverView.SetActive(true);
        pauseButton.SetActive(false);
        
        if (currentScore == bestScore)
            PlayerPrefs.SetInt("BestScore", currentScore);
        
        if (currentScore == 0)
            textCurrentScore.gameObject.SetActive(true);
            
    }

    public void IncreaseScore(int score)
    {
        textCurrentScore.gameObject.SetActive(true);
        
        currentScore += score;

        textCurrentScore.text = currentScore.ToString();

        _cameraController.ChangeBackgroundColor();
        
        if (bestScore < currentScore)
        {
            bestScore = currentScore;
            textBestScore.text = $"<size=50>BEST</size>\n<size=100>{currentScore}</size>";
        }
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    

}
