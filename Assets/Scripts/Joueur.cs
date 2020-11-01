using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Joueur : MonoBehaviour
{

    //private int points = 0;
    private int remainingLives = 25;

    public Text score;
    public Text finalScore;

    public GameObject[] projectiles;
    public GameObject[] targets;
    private float alarm;
    //public Target targetPrefab;
    //public List<Target> targets; //Liste des targets instanciées
    private float distanceJeu = 20;
    //private int nbTargets = 3;
    private float interval;
    public float spawnSpeedFactor = 1.0f;
    private bool isDead;
    public GameObject LoseMenuUI;


    // Start is called before the first frame update
    void Start()
    {
        interval = (UnityEngine.Random.Range(Projectile.speedTapis * 0.06f, Projectile.speedTapis * 0.25f)) * spawnSpeedFactor;
        //alarm = Time.time + 1.0f;
        alarm = Time.time + interval;
        InstanciateTargets();
        isDead = false;
        //LoseMenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //On instancie un nouveau minion de façon aléatoire toutes les 5 secondes
        InstanciateMinion();
        SetCountText();
        if (isDead == true)
        {
            finalScore.text = "Vous avez marqué " + Projectile.points.ToString() + " points !";
            LoseMenuUI.SetActive(true);
        }
    }

    private void InstanciateMinion()
    {
        //On instancie un nouveau minion de façon aléatoire toutes les 5 secondes
        if (Time.time > alarm)
        {
            System.Random rnd = new System.Random();
            int projIndex = rnd.Next(projectiles.Length);
            GameObject.Instantiate(projectiles[projIndex], new Vector3(-6, 0.5f, -3.5f), Quaternion.identity);
            interval = UnityEngine.Random.Range(Projectile.speedTapis * 0.07f, Projectile.speedTapis * 0.2f) * spawnSpeedFactor;
            alarm = Time.time + interval;
        }
    }

    public void InstanciateTargets()
    {
        float xOffset = -20 + 12f / (targets.Length + 1);
        for (int i = 0; i < targets.Length; i++)
        {
            xOffset += 5f + 12f / (targets.Length + 1);
            GameObject.Instantiate(targets[i], new Vector3(xOffset, 1f, distanceJeu), Quaternion.identity);
        }

    }
    //Fonction qui permet de suivre la progression des scores
    public void SetCountText()
    {
        score.text = "Nombre de points : " + Projectile.points.ToString() + "\n" + "Nombres de vies restantes : " + (remainingLives-Projectile.errors).ToString();
        if ((remainingLives - Projectile.errors) == 0.0f)
        {
            Debug.Log("No more life!");
            isDead = true;
        }
        
    }

    public void SetPoints(int pts)
    {
        Projectile.points = pts;
        Projectile.errors = 0;
        Projectile.speedTapis = 5.0f;
        remainingLives = 25;
        spawnSpeedFactor = 1.0f;
        interval = (UnityEngine.Random.Range(Projectile.speedTapis * 0.06f, Projectile.speedTapis * 0.25f)) * spawnSpeedFactor;
        //alarm = Time.time + 1.0f;
        alarm = Time.time + interval;
    }

    /* public void setPoints(int b) { points = b; }
     public int getPoints() { return points; }*/
}
