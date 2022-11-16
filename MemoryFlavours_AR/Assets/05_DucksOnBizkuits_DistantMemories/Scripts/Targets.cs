using UnityEngine;

public class Targets : MonoBehaviour
{
    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        var toggle=true;
        Gizmos.color = Color.red;
        foreach (Transform target in transform)
        {
            Gizmos.DrawWireSphere(target.position, 0.05f);
            if (!toggle) continue;
            Gizmos.color = Color.blue;
            toggle = false;
        }

        Gizmos.color = Color.red;
        for (var i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position);
            if (toggle) continue;
            Gizmos.color = Color.yellow;
            toggle = true;
        }
        
        Gizmos.DrawLine(transform.GetChild(transform.childCount - 1).position, transform.GetChild(0).position);
    }
    #endif

    /// <summary>
    /// Call this to choose the next child in hierarchy order
    ///  For code re-usability (checks based on next target than index)
    /// </summary>
    public Transform GetNextTarget(Transform currentTarget)
    {
        // if the target is null, go to the first waypoint
        // get the sibling of the inputted transform and if its out of range, set it back to 0
        if (currentTarget == null)
        {
            return transform.GetChild(UnityEngine.Random.Range(0, transform.childCount));
        }

        if (currentTarget.GetSiblingIndex() >= transform.childCount - 1)
        {
            return transform.GetChild(0);
        }
        else if (currentTarget.GetSiblingIndex() == 3)
        {
            return UnityEngine.Random.Range(0, 2) == 1 ? transform.GetChild(1) : transform.GetChild(currentTarget.GetSiblingIndex() + 1);
        }
        else
        {
            return transform.GetChild(currentTarget.GetSiblingIndex() + 1);
        }
    }
}
