using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joueur : MonoBehaviour
{

    private int points = 0;
    private int remainingLives = 3;
    public GameObject[] projectiles;
    //private GameObject[] targets;
    private float alarm;
    public Target targetPrefab;
    //public List<Target> targets; //Liste des targets instanciées
    private float distanceJeu = 20;
    private int nbTargets = 3;


    // Start is called before the first frame update
    void Start()
    {
        alarm = Time.time + 1.0f;
        InstanciateTargets();
    }

    // Update is called once per frame
    void Update()
    {
        
        //On instancie un nouveau minion de façon aléatoire toutes les 5 secondes
        InstanciateMinion();

    }

    private void InstanciateMinion()
    {
        //On instancie un nouveau minion de façon aléatoire toutes les 5 secondes
        if (Time.time > alarm)
        {
            System.Random rnd = new System.Random();
            int projIndex = rnd.Next(projectiles.Length);
            GameObject.Instantiate(projectiles[projIndex], new Vector3(-6, 0.5f, 0.0f), Quaternion.identity);
            alarm = Time.time + 1.0f;
        }
    }

    public void InstanciateTargets()
    {
        float xOffset = -6 + 12f / (nbTargets + 1);
        for (int i = 0; i < nbTargets; i++)
        {
            //Instantiate(targetPrefab, new Vector3(xOffset, 0f, distanceJeu), Quaternion.identity);
            xOffset += 12f / (nbTargets + 1);
            GameObject.Instantiate(targetPrefab, new Vector3(xOffset, 0f, distanceJeu), Quaternion.identity);
            /*Target target = */
            //targets.Add(target);
        }

    }
}
