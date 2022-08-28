using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMP_Text CreditText;
    public TMP_Text HealthText;
    public TMP_Text WaveText;

    public GameObject EndGamePanel;
    public GameObject WinPanel;
    public GameObject LosePanel;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.Events.OnWaveStarted += WaveUpdate;
        GameManager.Instance.Events.OnCreditChanged += CreditUpdate;
        GameManager.Instance.Events.OnHealthChanged += HealthUpdate;
        GameManager.Instance.Events.OnGameOver += GameOver;

        CreditText.text = "$" + GameManager.Instance.Credits;
        WaveText.text = "Wave: " + (GameManager.Instance.CurrentLevel.WaveCount + 1).ToString();
        HealthText.text = "Health: " + GameManager.Instance.CurrentHealth; 

    }

    private void OnDestroy()
    {
        GameManager.Instance.Events.OnWaveStarted -= WaveUpdate;
        GameManager.Instance.Events.OnCreditChanged -= CreditUpdate;
        GameManager.Instance.Events.OnHealthChanged -= HealthUpdate;
        GameManager.Instance.Events.OnGameOver -= GameOver;
    }

    private void GameOver(bool whoWon)
    {
        EndGamePanel.SetActive(true);
        if (whoWon)
            WinPanel.SetActive(true);
        else
            LosePanel.SetActive(true);

    }

    private void HealthUpdate()
    {
        HealthText.text = "Health: " + GameManager.Instance.CurrentHealth;
    }

    private void CreditUpdate()
    {
        CreditText.text = "$" + GameManager.Instance.Credits;
    }

    private void WaveUpdate()
    {
        WaveText.text = "Wave: " + (GameManager.Instance.CurrentLevel.WaveCount + 1).ToString();
    }
}
