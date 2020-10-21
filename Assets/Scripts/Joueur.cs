using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joueur : MonoBehaviour
{

    private int points = 0;
    private int remainingLives = 3;
    public GameObject[] projectiles;
    private GameObject[] targets;
    private float alarm;


    // Start is called before the first frame update
    void Start()
    {
        alarm = Time.time + 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
        //On instancie un nouveau minion de façon aléatoire toutes les 5 secondes
        if (Time.time > alarm)
        {
            System.Random rnd = new System.Random();
            int projIndex = rnd.Next(3);
            GameObject.Instantiate(projectiles[projIndex], new Vector3(-6, 0f, 0.0f), Quaternion.identity);
           alarm = Time.time + 1.0f;
        }
       
    }
}