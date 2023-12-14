using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AINodeTool;

public class Player : AgentBehaviour
{
    public override void OnTakeDamage()
    {
        GetComponent<NodeTool.GraphRunner>().enabled = true;
        GetComponent<NodeTool.GraphRunner>().RunOnce();
    }

    public override void OnDeath()
    {
        Destroy(gameObject);
    }
}
