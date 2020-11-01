using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class LevelLoader : MonoBehaviour
{
    #region Attributes

    public Animator Anim;

    public float transitionTime = 1f;

    public Image img;

    public Joueur joueur;
    
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject LoseMenuUI;

    static string previous_level = null;

    #endregion

    #region Methods

    public void PlayGame() //ou loadnextlevel --pas encore d'autre level
    {
        StartCoroutine(LoadLevel()); 
    }

    public void NewGame()
    {
        Resume();
        //LoseMenuUI.SetActive(false); //activé via le script joueur
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        joueur.SetPoints(0); //réinitialiser les score et vie
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("QUIT GAME!");
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        LoseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

   public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Lose()
    {
        LoseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    IEnumerator LoadLevel()
    {
        Anim.SetBool("Fade", true);

        yield return new WaitUntil(()=>img.color.a==1);

        SceneManager.LoadScene("Minions_Scene");
    }
    #endregion
}
