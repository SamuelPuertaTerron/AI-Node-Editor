using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeTool;
using NodeToolEditor;

public class DialogueNode : PureNode
{
    [SerializeField, TextArea(3, 10)] private string text;

    // This is called on the first frame when in play mode
    public override void OnNodeStart()
    {
	    ParentObject.GetComponent<TMPro.TMP_Text>().text = text;	
    }
    
    // This is called every frame when in play mode
    public override void OnNodeUpdate()
    {
       
    } 
}