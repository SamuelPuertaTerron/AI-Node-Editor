using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeTool
{
    public class RootNode : Node
    {
        [HideInInspector] public Node child;

        public override object OnGetValue()
        {
	    if(child)	
            	return child.OnGetValue();
	    else
	    {
		Debug.LogError("Child is null: A Root Node needs to have a child node connected");
		return null;
	    }
        }
    }
}


