using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class LevelLoader : MonoBehaviour
{
    public Animator Anim;
    public float transitionTime = 1f;
    public Image img;
    public Joueur joueur;
    //private GameObject projectile;
    
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    static string previous_level = null;

    public void PlayGame() //ou loadnextlevel
    {
        //projectile = joueur.projectiles[0];
        StartCoroutine(LoadLevel()); 
    }

    public void NewGame()
    {
        Resume(); //****
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        joueur.SetPoints(0);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("QUIT GAME!");
    }

    void Update()
    {

    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

   public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    IEnumerator LoadLevel()
    {
        Anim.SetBool("Fade", true);

        yield return new WaitUntil(()=>img.color.a==1);

        SceneManager.LoadScene("Minions_Scene");
    }
}
