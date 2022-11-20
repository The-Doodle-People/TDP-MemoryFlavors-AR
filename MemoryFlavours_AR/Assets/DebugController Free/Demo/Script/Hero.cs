using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Hero : MonoBehaviour {

    public Camera mainCamera;
    public Transform canvas;
    public Slider helth;
    public Slider power;
    public NavMeshAgent agent;
    public GameObject clickPoint;

    public int helthValue = 100;
    public int powerValue = 100;
    public int maxhelthValue = 100;
    public int maxpowerValue = 100;

    public HeroState state = HeroState.Alive;

    private void Update()
    {
        canvas.forward = mainCamera.transform.forward;

        if (state == HeroState.Alive)
        {
            if (Input.GetMouseButtonDown(0) && GUIUtility.hotControl == 0)
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.name == "Ground")
                    {
                        agent.SetDestination(hit.point);
                        clickPoint.SetActive(true);
                        clickPoint.transform.position = hit.point;
                    }
                }
            }

            helth.value = (float)helthValue / maxhelthValue;
            power.value = (float)powerValue / maxpowerValue;

            if (helthValue <= 0)
            {
                Death();
            }
        }
    }

    public void Death()
    {
        helthValue = 0;
        powerValue = 0;
        state = HeroState.Dead;
        agent.isStopped = true;
        canvas.gameObject.SetActive(false);
        clickPoint.SetActive(false);
        GetComponent<MeshRenderer>().enabled = false;
        Debug.LogWarning("Hero is dead!");
    }

    public void Rebirth()
    {
        helthValue = maxhelthValue;
        powerValue = maxpowerValue;
        state = HeroState.Alive;
        agent.isStopped = false;
        canvas.gameObject.SetActive(true);
        clickPoint.SetActive(false);
        GetComponent<MeshRenderer>().enabled = true;
        Debug.LogWarning("Hero is resurgence!");
    }
}

public enum HeroState
{
    Alive,
    Dead
}
