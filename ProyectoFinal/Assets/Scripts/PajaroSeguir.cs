using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //NavMesh

public class PajaroSeguir : MonoBehaviour
{
    [SerializeField] private Transform pajaroJefe;

    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
            Debug.Log("PajaroSeguir no encuentra NavMeshAgent asociado al Pajaro.");
    }

    private void Update()
    {
        navMeshAgent.destination = pajaroJefe.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.layer == LayerMask.NameToLayer("Target"))
        //{
        //    Debug.Log("Colision: Bird x Target");

        //    RandomMoveTarget target = other.gameObject.GetComponent<RandomMoveTarget>();
        //    target.SetRandomPosition();
        //}
    }
}
