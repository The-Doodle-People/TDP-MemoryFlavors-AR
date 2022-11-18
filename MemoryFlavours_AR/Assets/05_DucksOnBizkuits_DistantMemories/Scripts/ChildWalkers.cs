using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChildWalkers : MonoBehaviour
{
    private NavMeshAgent agent;

    [SerializeField] private string aiState = "start";
    
    [Range(0.1f, 1f)] public float range = 0.45f; //radius of sphere

    public Transform centrePoint; //centre of the area the agent wants to move around in
    //instead of centrePoint you can set it as the transform of the agent if you don't care about a specific area
    
    private readonly string[] allStates = {"Move", "Idle"};

    #if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(centrePoint.position, range);
    }

    #endif

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        StartCoroutine(AiManager());
    }

    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        var randomPoint = center + UnityEngine.Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    private IEnumerator AiManager()
    {
        var repeats = 0;
        var oldState = "start";
        while (true)
        { 
            // if repeated twice, force other state
            if (repeats > 2)
            {
                //Notes:               // Move       //Idle         //Move
                aiState = oldState == allStates[0] ? allStates[1] : allStates[0];
            }
            else
            {
                const int minRange = 0;
                int maxRange = allStates.Length;
                aiState = UnityEngine.Random.Range(minRange, maxRange)%2 == 0 ? allStates[0] : allStates[1];
            }
            
            // resets state tracking if state has changed
            if (oldState != aiState)
            {
                oldState = aiState;
                repeats = 0;
            }
            
            yield return StartCoroutine(aiState);
            repeats++;
        }
    }
    
    private IEnumerator Move()
    {
        agent.SetDestination(transform.position);
        Vector3 point;
        if (RandomPoint(centrePoint.position, range, out point)) //pass in our centre point and radius of area
        {
            Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
            agent.SetDestination(point);
        }
        while (true)
        {
            yield return new WaitForSeconds(0.7f);
            if ( !agent.pathPending && (agent.remainingDistance <= agent.stoppingDistance) && 
                 (!agent.hasPath || agent.velocity.sqrMagnitude == 0f))
            {
                break;
            }
        }    
    }
    
    private IEnumerator Idle()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(2f, 5f));
    }
}