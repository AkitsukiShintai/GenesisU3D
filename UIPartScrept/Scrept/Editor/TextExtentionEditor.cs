using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TextEditor = UnityEditor.UI.TextEditor;

[CustomEditor(typeof(TextExtention), true)]
public class TextExtentionEditor : TextEditor
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
