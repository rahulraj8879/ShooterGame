using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WavesGameMode : MonoBehaviour
{
    [SerializeField] Life PlayerLife;
    [SerializeField] Life PlayerBaseLife;
    // Start is called before the first frame update
    void Start()
    {
        PlayerLife.onDeath.AddListener(OnLifeChanged);
        PlayerBaseLife.onDeath.AddListener(OnBaseLifeChanged);
        EnemyManager.instance.onChanged.AddListener(CheckWinCondition);
        WaveManager.instance.onChanged.AddListener(CheckWinCondition);
    }

    // Update is called once per frame
    void CheckWinCondition()
    {
        if(EnemyManager.instance.enemies.Count <= 0 && WaveManager.instance.waves.Count <= 0)
        {
            SceneManager.LoadScene("WinScreen");
        }

       
    }
    void OnLifeChanged()
    {
        if (PlayerLife.amount <= 0)
        {
            SceneManager.LoadScene("loseScreen");
        }
    }  void OnBaseLifeChanged()
    {
        if (PlayerBaseLife.amount <= 0)
        {
            SceneManager.LoadScene("loseScreen");
        }
    }
    
}
