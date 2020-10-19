using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

public class Projectile : MonoBehaviour
{
    #region Attributs
    //Est attrapé par le joueur
    private bool isCaught;

    //Est lancé par le joueur
    private bool isLaunched;

    //vitesse de translation sur le tapis roulant
    private float speed;

    //Offset pour corriger la position de la souris sur l'écran
    private Vector3 mOffset;

    //Coordonnée z du gameobject au moment de sa saisie
    private float mZCoord;

    //Sur le tapis ou attrappé/envoyé
    public bool state; 
    #endregion



    // Start is called before the first frame update
    void Start()
    {
        isCaught = false;
        isLaunched = false;
        speed = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //Détruire l'objet si il dépasse une certaine position
        if(transform.position.x >= 6)
        {
            Object.Destroy(this.gameObject);
        }

        
        //Les minions défilent sur le tapis s'ils ne sont pas attrapés et / ou lancés
        if (!isCaught && !isLaunched)
        {
            Vector3 direction = -Vector3.Normalize(transform.position - new Vector3(speed, 0.0f, 0.0f));
            direction.y = 0.0f;
            direction.z = 0.0f;
            transform.Translate(direction * 5 * Time.deltaTime);
            Debug.Log(transform.position.x);
        }

    }

    void OnMouseDown()
    {
        //Le minion est attrapé
        isCaught = true;


        //Coordonnée z du minion
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        //offset = gameobject world pos - mouse world pos
        mOffset = gameObject.transform.position - GetMouseAsWorldPoint();

    }

    private Vector3 GetMouseAsWorldPoint()
    {
        //Coordonnées de la souris
        Vector3 mousePoint = Input.mousePosition;

        //Coordonnée z de la souris
        mousePoint.z = mZCoord;

        //Conversion avec l'échelle du world
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }



    void OnMouseDrag()
    {
        //Drag le minion avec la souris
        transform.position = GetMouseAsWorldPoint() + mOffset;
    }

    private void OnMouseUp()
    {
        //Le minion est laché et lancé...
        isCaught = false;
        isLaunched = true;

        //...dans la direction de la souris
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        transform.GetComponent<Rigidbody>().AddForce(ray.direction * 1000.0f);
    }
}
