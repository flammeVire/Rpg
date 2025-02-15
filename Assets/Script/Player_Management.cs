using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed = 1f;
    [SerializeField] private Transform actualTransform;

    #region UnityBase Function
    private void Start()
    {
        GameManager.instance.InitPlayer(this);
    }
    private void Reset()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        InputManagement();
    }
    #endregion
    public void Init(Vector3 initialPosition)
    {
        rb.position = initialPosition;
    }

    #region Input
    
    private void InputManagement()
    {
        Movement();
        Interract();
    } 
    private void Movement()
    {
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * speed;
        rb.velocity = moveDirection;

        if (moveDirection != Vector3.zero)
        {
            transform.forward = moveDirection; // Tourne le joueur dans la direction du mouvement
        }
    } 

    private void Interract()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Interract");
            float sphereRadius = 0.5f; // Rayon du SphereCast
            float castDistance = 2f; // Distance du cast
            RaycastHit hit;
            Vector3 origin = transform.position; // Point de départ du cast
            Vector3 direction = transform.forward; // Direction du cast

            if (Physics.SphereCast(origin, sphereRadius, direction, out hit, castDistance))
            {
                if (hit.collider.CompareTag("interractable"))
                {
                    Debug.Log("Objet interactif détecté : " + hit.collider.name);


                    //si on interragit avec un PNJ de quete
                    if (hit.collider.GetComponent<PnjQuest>() != null)
                    {
                        hit.collider.GetComponent<PnjQuest>().ManageQuest();
                    }

                    else if(hit.collider.GetComponent<StartBattle>() != null)
                    {
                        actualTransform = transform;
                        Debug.Log("actual transform == " + actualTransform.position);
                        hit.collider.GetComponent<StartBattle>().GoToFight(actualTransform);
                    }
                }
            }
        }
    }
    #endregion
}