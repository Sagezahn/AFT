using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject endUI;
    public Text endMessage;

    public static GameManager Instance;
    private EnemySpawner enemySpawner;
    void Awake()
    {
        Instance = this;
        enemySpawner = GetComponent<EnemySpawner>();
    }

    public void Win()
    {
        endUI.SetActive(true);
        endMessage.text = "Vectory";
    }
    public void Failed()
    {
        enemySpawner.Stop();
        endUI.SetActive(true);
        endMessage.text = "Defalt";
    }

    public void OnButtonNext()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public void OnButtonRetry()
    {
        SceneManager.LoadScene(1);
    }
    public void OnButtonMenu()
    {
        SceneManager.LoadScene(0);
    }
}
