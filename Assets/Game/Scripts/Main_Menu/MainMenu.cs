using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    //Load Single player Scene when Single player Button is clicked
    public void LoadSinglePlayerGame()
    {
        Debug.Log("Single pLayer Game Loading...");
        SceneManager.LoadScene("Single_Player");

    }

    public void LoadCoOpMode()
    {
        Debug.Log("Co-Op Mode Loading...");
        SceneManager.LoadScene("Co-Op_Mode");

    }

    public void Quit()
    {
        Debug.Log("Game quit");
        Application.Quit();
    }


}
