using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

[CustomEditor(typeof(ButtonExtention), true)]
public class ButtonExtentionEditor : ButtonEditor
{

    //public ButtonExtentionEditor() : base("ButtonEditor") { }
    public override void OnInspectorGUI()
    {
        //ButtonExtention targetMenuButton = (ButtonExtention)target;

        // targetMenuButton.dd = EditorGUILayout.Toggle("ButtonExtention", targetMenuButton.dd);

        // Show default inspector property editor
        base.DrawDefaultInspector();
    }
}

