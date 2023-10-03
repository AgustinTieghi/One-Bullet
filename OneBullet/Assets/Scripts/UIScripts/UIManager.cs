using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public PlayerStatsManager playerScript;
    public Image healthbar;
    public SpawnerManager spawner;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI difficultyText;
    private void Update()
    {
        if (spawner != null && spawner.gameObject.activeInHierarchy)
        {
            timerText.gameObject.SetActive(true);
            UpdateTimer(spawner.currentTime);
            UpdateDifficulty();
        }       

        UpdateHealthbar(100, playerScript.playerHP);
    }
    public void UpdateHealthbar(float maxHealth, float currentHealth)
    {
        healthbar.fillAmount = currentHealth / maxHealth;
    }

    public void UpdateTimer(float currentTime)
    {

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = string.Format("Time Left: " + "{0:00} : {1:00}", minutes, seconds);
    }
    
    public void UpdateDifficulty()
    {
        switch (spawner.index)
        {
            case 1:
                difficultyText.text = "Difficulty: Easy";
                break;
            case 2:
                difficultyText.text = "Difficulty: Hard";
                break;
            case 3:
                difficultyText.text = "Difficulty: ???";
                break;
            default
                : difficultyText.text = "";
                break;
        }
    }
}
