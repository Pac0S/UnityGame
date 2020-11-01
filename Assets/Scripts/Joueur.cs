using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class Joueur : MonoBehaviour
{
    #region Attributes

    private int remainingLives = 25;

    public Text score;
    public Text finalScore;

    public GameObject[] projectiles;
    public GameObject[] targets;

    private float alarm;
    
    private float distanceJeu = 20;
    private float interval;
    public float spawnSpeedFactor = 1.0f;
    
    private bool isDead;
    
    public GameObject LoseMenuUI;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        interval = (UnityEngine.Random.Range(Projectile.speedTapis * 0.06f, Projectile.speedTapis * 0.25f)) * spawnSpeedFactor;
        alarm = Time.time + interval;

        InstanciateTargets();
        
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        //On instancie un nouveau minion
        InstanciateMinion();
        //on affiche score et vie
        SetCountText();

        //Lorsque le joueur n'a plus de vie
        if (isDead == true)
        {
            finalScore.text = "You got " + Projectile.points.ToString() + " points!";
            LoseMenuUI.SetActive(true);
        }
    }

    #region Methods
    private void InstanciateMinion()
    {
        //On instancie un nouveau minion de façon aléatoire, avec un interval de temps variable
        if (Time.time > alarm)
        {
            System.Random rnd = new System.Random();
            int projIndex = rnd.Next(projectiles.Length);
            GameObject.Instantiate(projectiles[projIndex], new Vector3(-35, 0.5f, -9f), Quaternion.identity);

            interval = UnityEngine.Random.Range(0.40f, 1.6f) * spawnSpeedFactor;
            //interval = UnityEngine.Random.Range(Projectile.speedTapis * 0.07f, Projectile.speedTapis * 0.2f) * spawnSpeedFactor + 0.15f;
            alarm = Time.time + interval;
        }
    }

    public void InstanciateTargets() //pour générer les différentes cibles espacées les unes des autres
    {

        float[] xs = { -26, 0, 0, 26 };
        float[] ys = { distanceJeu+20, distanceJeu +25 , distanceJeu, distanceJeu+20 };
        for (int i = 0; i < targets.Length; i++)
        {
            GameObject.Instantiate(targets[i], new Vector3(xs[i], 1f, ys[i]), Quaternion.identity);
        }

        /*float xOffset = -20 + 12f / (targets.Length + 1);

        for (int i = 0; i < targets.Length; i++)
        {
            xOffset += 5f + 12f / (targets.Length + 1);
            GameObject.Instantiate(targets[i], new Vector3(xOffset, 1f, distanceJeu), Quaternion.identity);
        }*/

    }
    
    public void SetCountText() //Fonction qui permet de suivre la progression des scores
    {
        score.text = "Safe puppies : " + Projectile.points.ToString() + "\t\t\t\t\t\t" + "Puppies in some other dimension : " + (remainingLives-Projectile.errors).ToString();
        
        if ((remainingLives - Projectile.errors) == 0.0f) //Lorsque le joueur n'a plus de vie
        {
            Debug.Log("No more life!");
            isDead = true;
        }
        
    }

    public void SetPoints(int pts) //réinitialisationb des variables pour une nouvelle partie
    {
        Projectile.points = pts;
        Projectile.errors = 0;
        Projectile.speedTapis = 5.0f;
        remainingLives = 25;
        spawnSpeedFactor = 1.0f;
        interval = (UnityEngine.Random.Range(Projectile.speedTapis * 0.06f, Projectile.speedTapis * 0.25f)) * spawnSpeedFactor;
        alarm = Time.time + interval;
    }
    #endregion
}
