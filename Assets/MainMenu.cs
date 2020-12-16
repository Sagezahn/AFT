using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public GameObject Mainmenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void NewGame()
    {
        SceneManager.LoadScene(0);
    }
    public void Stop()
    {   
        Time.timeScale = 0f;
        Mainmenu.SetActive(true);
    }
    public void Continue()
    {
        Time.timeScale = 1;
        Mainmenu.SetActive(false);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
