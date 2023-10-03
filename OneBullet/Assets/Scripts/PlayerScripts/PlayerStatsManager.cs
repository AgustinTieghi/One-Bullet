
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStatsManager : MonoBehaviour
{
    public static PlayerStatsManager instance;
    public string gameOverScene = "GameOverScene";
    private void Awake()
    {
        instance = this;
    }
    [Header("Player Stats")]
    [SerializeField] private GameObject player;
    [SerializeField] public float playerHP = 100;
    public GameObject redHUD;
    [HideInInspector]
    public bool dead;
    private void Update()
    {
        if (playerHP <= 0)
        {
            Die();
            SceneManager.LoadScene(gameOverScene);        
        }
        if (redHUD != null)
        {
            if (redHUD.GetComponent<Image>().color.a > 0)
            {
                var color = redHUD.GetComponent<Image>().color;
                color.a -= 0.02f;

                redHUD.GetComponent<Image>().color = color;
            }
        }
    }

    public void GetHit(int damageValue)
    {
        playerHP -= damageValue;    
        var color = redHUD.GetComponent<Image>().color;
        color.a = 0.8f;
        redHUD.GetComponent<Image>().color = color;
    }
    private void Die()
    {
        dead = true;
        player.GetComponent<FirstPersonController>().enabled = false;
    }
}
