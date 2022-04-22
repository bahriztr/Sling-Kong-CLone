using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] public static UIManager Instance;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text coinText;
    public int score;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        scoreText.text = score.ToString();
    }

    public void UpdateCoin(int coin)
    {
        coinText.text = coin.ToString();
    }

    public void PlayAgain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
