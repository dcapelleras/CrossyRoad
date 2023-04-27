using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    //puntuació?
    //menu?
    //pausa?
    //mort?
    //vides?
    //enemics?
    //mecaniques extra en plan atac?
    //infinit o no infinit?
    public static GameManager Instance;

    public bool gamePaused = true;
    [SerializeField] GameObject pausePanel;
    [SerializeField] TMP_Text pointsText;

    int points;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        Time.timeScale = 0;
    }


    private void Update()
    {
        if (gamePaused)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Escape))
            {
                gamePaused = false;
                pausePanel.SetActive(false);
                Time.timeScale = 1;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                gamePaused = true;
                pausePanel.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }


    public void ScorePoint()
    {
        points ++;
        pointsText.text = points.ToString();
    }

    public void ResetPoints()
    {
        points = 0;
        pointsText.text = points.ToString();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
