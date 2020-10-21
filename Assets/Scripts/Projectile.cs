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

    //vitesse de l'objet au lancer
    private float launchSpeed = 0;

    //Offset pour corriger la position de la souris sur l'écran
    private Vector3 mOffset;

    //Coordonnée z du gameobject au moment de sa saisie
    private float mZCoord;
    
    //Base temporelle pour calculer la force de lancer
    private float t0;

    private Animator animator;

    public SpriteRenderer fleche;

    public Material material;

    #endregion



    // Start is called before the first frame update
    void Start()
    {
        isCaught = false;
        isLaunched = false;
        speed = 5.0f;
        animator = GetComponent<Animator>();
        transform.Rotate(new Vector3(0f, 180f, 0f));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Détruire l'objet si il dépasse une certaine position
        if(transform.position.x >= 6.0f && !isLaunched && !isCaught || transform.position.y <= -1.0f && !isCaught)
        {
            Object.Destroy(this.gameObject);
        }

        
        //Les minions défilent sur le tapis s'ils ne sont pas attrapés et / ou lancés
        if (!isCaught && !isLaunched)
        {
            Vector3 direction = Vector3.Normalize(transform.position - new Vector3(10.0f, 0.0f, 0.0f));
            direction.y = 0.0f;
            direction.z = 0.0f;
            transform.Translate(direction * speed * Time.deltaTime);
           
        }
        if (isCaught)
        {
            animator?.SetBool("Walk", true);
        }
        Debug.Log(launchSpeed);
    }

    void OnMouseDown()
    {
        //Le minion est attrapé
        isCaught = true;

        //On initialise un temps qui donnera la force de lancer
        t0 = Time.time;

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
        material.SetFloat("_Yoffset", Mathf.Abs(Mathf.Sin(Time.time - t0)));
    }

    private void OnMouseUp()
    {
        //Le minion est laché et lancé...
        isCaught = false;
        isLaunched = true;

        //Variation sinusoidale de la force de lancer
        launchSpeed = Mathf.Abs(Mathf.Sin(Time.time-t0) * 3000);

        //...dans la direction de la souris
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        transform.GetComponent<Rigidbody>().AddForce(ray.direction * launchSpeed);
        Debug.Log(launchSpeed);
        material.SetFloat("_Yoffset", 0.0f);
    }


    private void OnCollisionEnter(Collision collision)
    {
        //foreach (GameObject target in joueur.targets)
         if (collision.gameObject.GetComponent("Target") as Target != null )
         {
                Object.Destroy(this.gameObject);
         }
    }
}
