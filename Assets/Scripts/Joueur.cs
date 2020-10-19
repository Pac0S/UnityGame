using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joueur : MonoBehaviour
{

    private int points = 0;
    private int remainingLives = 3;
    public GameObject[] projectiles;
    private GameObject[] targets;
    // Start is called before the first frame update
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            System.Random rnd = new System.Random();
            int projIndex = rnd.Next(2);
            GameObject.Instantiate(projectiles[projIndex], new Vector3(-6, 0f, 0.0f), Quaternion.identity);
        }
       
    }
}
