using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AINodeTool;

public class Enemy : AgentBehaviour
{
    public override void OnTargetSpotted(GameObject other)
    {
        WaypointPathfinding.IsPaused = true;
        UnityAgent.SetDestination(other.transform.position);
        other.GetComponent<HealthManager>().TakeDamage(10.0f);
    }

    public override void OnTargetLost()
    {
        WaypointPathfinding.IsPaused = false;
    }

    public override void OnTakeDamage()
    {
        GetComponent<Renderer>().material.color = Color.red;
        StartCoroutine(Delay(2.0f));
        GetComponent<Renderer>().material.color = Color.white;
    }

    public override void OnAudioHeard(Sound sound)
    {
        UnityAgent.SetDestination(sound.position);
        WaypointPathfinding.IsPaused = true;

        StartCoroutine(Delay(3.0f));
    }

    private IEnumerator Delay(float time)
    {
        yield return new WaitForSeconds(time);

        if(Vector3.Distance(transform.position, UnityAgent.destination) < 3.0f)
        {
            WaypointPathfinding.IsPaused = false;
        }
    }
}
