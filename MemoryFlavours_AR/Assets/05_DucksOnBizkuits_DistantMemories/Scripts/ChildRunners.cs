using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ChildRunners : MonoBehaviour
{
    // reference to target system
    private Targets target;

    private Transform currentTarget;

    private NavMeshAgent agent;

    public string aiState = "Run";

    private bool onTarget;

    public Transform childTarget;

    private Collider triggerCollider;

    [SerializeField] private float chaserWait = 3f;

    [SerializeField] private float aiSpeed = 0.3f;
    
    // Start is called before the first frame update
    private void Start()
    {
        target = FindObjectOfType<Targets>();
        agent = GetComponent<NavMeshAgent>();
        triggerCollider = GetComponent<Collider>();
        StartCoroutine(AiManager());
    }

    private IEnumerator AiManager()
    {
        while (true)
        {
            onTarget = true;

            switch (aiState)
            {
                case "Chase":
                    // set aiSpeed, chaser starts slow
                    agent.speed = aiSpeed * 0.25f;
                    currentTarget = childTarget;
                    break;
                case "Run":
                    // set aiSpeed, running starts normal
                    agent.speed = aiSpeed;
                    currentTarget = target.GetNextTarget(currentTarget);
                    break;
            }
            
            StartCoroutine(RestoreDetectors());
            yield return StartCoroutine(aiState);
        }
    }

    private IEnumerator Run()
    {
        // stores last target data
        var oldTarget = currentTarget;
        var oldTotalDist = Vector3.Distance(transform.position, oldTarget.position);

        // edit/randomize variable to control when does AI change target
        var turningThreshold = 0.2f;

        // start first path
        agent.SetDestination(currentTarget.position);
        transform.LookAt(currentTarget);

        while (onTarget)
        {
            // if the % of dist travelled is < turningThreshold, recalculate Destination, change target
            if (Vector3.Distance(transform.position, oldTarget.position) / oldTotalDist < turningThreshold ||
                agent.remainingDistance <= agent.stoppingDistance)
            {
                currentTarget = target.GetNextTarget(currentTarget);
                oldTarget = currentTarget;
                oldTotalDist = Vector3.Distance(transform.position, oldTarget.position);
                agent.SetDestination(currentTarget.position);
                transform.LookAt(currentTarget);
            }
            // for debuging/testing catches + safety net in case of issue
            // keeps same target, but updates new distance
            else if (currentTarget.position != oldTarget.position)
            {
                oldTotalDist = Vector3.Distance(transform.position, oldTarget.position);
                agent.SetDestination(currentTarget.position);
                transform.LookAt(currentTarget);
            }

            yield return new WaitForSeconds(0.7f);
        }

        // run a bit more, and stop
        agent.speed = aiSpeed;
        yield return new WaitForSeconds(0.8f);
        agent.SetDestination(transform.position);
    }

    private IEnumerator Chase()
    {
        // yield return new WaitForSeconds(chaserWait);
        while (onTarget)
        {
            agent.SetDestination(currentTarget.position);
            transform.LookAt(currentTarget);
            yield return new WaitForSeconds(0.7f);
        }
        // stop chasing almost immediately and change course!
        agent.speed = aiSpeed;
        yield return new WaitForSeconds(0.4f);
        agent.SetDestination(transform.position); 
    }

    private IEnumerator RestoreDetectors()
    {
        var timeToWait = chaserWait * 3;
        // number of times to check the code
        const float checkTimes = 12;
            
        // Release Chaser Method: CheckingDist
        for (var i = 0; i < checkTimes; i++)
        {
            yield return new WaitForSeconds(timeToWait / checkTimes);
            if (Vector3.Distance(transform.position, currentTarget.position) > 0.5f)
            {
                if (aiState == "Chase") StartCoroutine(RestoreSpeed());
                break;
            }
        }
        triggerCollider.enabled = true;
    }

    private IEnumerator RestoreSpeed()
    {
        // takes 3s to get back to max speed
        var restoreRate = 3f;
        var deBuffedSpeed = aiSpeed * 0.25f;
        while (agent.speed < aiSpeed)
        {
            agent.speed += (restoreRate * Time.deltaTime) * aiSpeed * 0.75f;
            yield return new WaitForSeconds(restoreRate * Time.deltaTime);
        }
        agent.speed = aiSpeed;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform != childTarget) return;
        // Debug.Log("Contact!");
        StopCoroutine(RestoreDetectors());
        StopCoroutine(RestoreSpeed());
        triggerCollider.enabled = false;
        onTarget = false;

        switch (aiState)
        {
            case "Run":
                aiState = "Chase";
                break;
            
            case "Chase":
                aiState = "Run";
                break;
        }
        // ensures the calling of on trigger enter once
        currentTarget = null;
    }
}
