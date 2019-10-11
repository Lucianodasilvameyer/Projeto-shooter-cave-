using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public Transform target;
    public Transform posicaoFlecha;

    [SerializeField]
    Rigidbody Flecha;

    public LayerMask clickable;

    private NavMeshAgent navMeshAgent;

    Animator animator;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movement();
        animations();

        if (Input.GetKeyDown(KeyCode.T))
        {
            AtirarFhecha();
        }
    }

    void movement()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 300, clickable))
            {
                navMeshAgent.SetDestination(hit.point);
            }
        }
    }

    void animations()
    {
        if(navMeshAgent.remainingDistance > 0)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }
    public void AtirarFhecha()
    {
        Rigidbody JogarFlecha = Instantiate(Flecha, posicaoFlecha.position, posicaoFlecha.rotation) as Rigidbody;
    }
}
