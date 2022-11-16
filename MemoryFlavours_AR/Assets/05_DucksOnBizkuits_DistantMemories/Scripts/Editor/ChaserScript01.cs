using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ChaserScript01 : MonoBehaviour
{
    public GameObject aiAttractor;
    private NavMeshAgent _agent;

    public bool onPath;
    [SerializeField]
    private string currentState;
    public string nextState;
    
    private void Start()
    {
        _agent = gameObject.GetComponent<NavMeshAgent>();
        if (nextState == "")
        {
            nextState = "Run";
            StartCoroutine(nameof(Controller));
        }
    }

    private IEnumerator Controller()
    {
        currentState = nextState;
        yield return StartCoroutine(currentState);

        Debug.Log("ended");
    }
    
    private IEnumerator Run()
    {
        onPath = true;
        while (onPath)
        {
            _agent.SetDestination(aiAttractor.transform.position);
            yield return new WaitUntil(CheckArrived);
            Debug.Log("loop");
        }
    }

    /*private bool CheckArrived()
    {
        return _agent.remainingDistance <= _agent.stoppingDistance;
    }

    private bool IsPathPending()
    {
        return !_agent.pathPending;
    }*/

    private bool CheckArrived()
    {
        return !_agent.pathPending && (_agent.remainingDistance <= _agent.stoppingDistance) &&
                         (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject != aiAttractor) return;
        onPath = false;
    }
}
