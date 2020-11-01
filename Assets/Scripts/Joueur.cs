using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Joueur : MonoBehaviour
{

    //private int points = 0;
    private int remainingLives = 3;

    public Text score;

    public GameObject[] projectiles;
    //private GameObject[] targets;
    private float alarm;
    public Target targetPrefab;
    //public List<Target> targets; //Liste des targets instanciées
    private float distanceJeu = 20;
    private int nbTargets = 3;
    private float interval;
    public float spawnSpeedFactor = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        interval = (UnityEngine.Random.Range(Projectile.speedTapis * 0.06f, Projectile.speedTapis * 0.25f)) * spawnSpeedFactor;
        //alarm = Time.time + 1.0f;
        alarm = Time.time + interval;
        InstanciateTargets();
    }

    // Update is called once per frame
    void Update()
    {
        //On instancie un nouveau minion de façon aléatoire toutes les 5 secondes
        InstanciateMinion();
        SetCountText();
    }

    private void InstanciateMinion()
    {
        //On instancie un nouveau minion de façon aléatoire toutes les 5 secondes
        if (Time.time > alarm)
        {
            System.Random rnd = new System.Random();
            int projIndex = rnd.Next(projectiles.Length);
            GameObject.Instantiate(projectiles[projIndex], new Vector3(-6, 0.5f, 0.0f), Quaternion.identity);
            interval = UnityEngine.Random.Range(Projectile.speedTapis * 0.07f, Projectile.speedTapis * 0.2f) * spawnSpeedFactor;
            alarm = Time.time + interval;
        }
    }

    public void InstanciateTargets()
    {
        float xOffset = -6 + 12f / (nbTargets + 1);
        for (int i = 0; i < nbTargets; i++)
        {
            xOffset += 12f / (nbTargets + 1);
            GameObject.Instantiate(targetPrefab, new Vector3(xOffset, 0f, distanceJeu), Quaternion.identity);
        }

    }
    //Fonction qui permet de suivre la progression des scores
    public void SetCountText()
    {
        score.text = "Nombre de points : " + Projectile.points.ToString();
        
    }

    public void SetPoints(int pts)
    {
        Projectile.points = pts;
    }

    /* public void setPoints(int b) { points = b; }
     public int getPoints() { return points; }*/
}
