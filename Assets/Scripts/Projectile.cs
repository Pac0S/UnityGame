using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

public class Projectile : MonoBehaviour
{
    public bool state; //Sur le tapis ou attrappé/envoyé
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //Détruire l'objet si il dépasse une certaine position
        if(transform.position.x >= 6)
        {
            Object.Destroy(this.gameObject);
        }

        Vector3 direction = -Vector3.Normalize(transform.position - new Vector3(6.0f, 0.0f, 0.0f));
        direction.y = 0.0f;
        direction.z = 0.0f;
        transform.Translate(direction*5 * Time.deltaTime);
        Debug.Log(transform.position.x);

    }
}
