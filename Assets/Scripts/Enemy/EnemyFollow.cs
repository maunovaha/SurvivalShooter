using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Tutorial
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyFollow : MonoBehaviour
    {
        private Transform player;
        private NavMeshAgent agent;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            agent.SetDestination(player.position);
        }
    }
}

