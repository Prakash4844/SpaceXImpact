using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool IsCoopMode = false;
    public bool gameOver = true;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject coopPlayers;
    [SerializeField]
    private GameObject pauseMenuPanel;

    private UIManager _uiManager;

    private Animator _pauseAnimator;

    /* private SpawnManager _spawnManager; 
      (don't know what it does) */

    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _pauseAnimator = GameObject.Find("Pause_menu_panel").GetComponent<Animator>();
        /* _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>(); 
         * (don't know what it does) */
        _pauseAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    //if game over is true
    //if space key pressed
    //spawn the player
    //gameOver is false
    //hide title screen

    void Update()
    {
        if (gameOver == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (IsCoopMode == false)
                {
                    Instantiate(player, Vector3.zero, Quaternion.identity);

                }
                else
                {
                    Instantiate(coopPlayers, Vector3.zero, Quaternion.identity);
                }

                gameOver = false;
                _uiManager.HideTitleScreen();

            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("Main_Menu");

            }

        }

        //if P key is pressed Pause the Game
        //enable Pause menu panel
        if (Input.GetKeyDown(KeyCode.P))
        {   

            pauseMenuPanel.SetActive(true);
           _pauseAnimator.SetBool("IsPaused", true);
            Time.timeScale = 0;

        }

    }

    public void ResumeGame()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1;
    }

}
