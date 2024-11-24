using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private Transform _player;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        // Optionnel : essayer de trouver le joueur au démarrage
        _player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        // Mettre à jour la référence du joueur si elle est nulle
        if (_player == null)
        {
            _player = GameObject.FindGameObjectWithTag("Player")?.transform;
        }

        if (_player != null)
        {
            navMeshAgent.SetDestination(_player.position);
        }
    }
}

