#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System;

[CustomEditor(typeof(AudioManager))]
public class DropdownEditor : Editor
{
    public override void OnInspectorGUI()
    {
        AudioManager script = (AudioManager)target;
        EditorUtility.SetDirty(script);
        string[] bGMNames = new string[script.BGMSounds.Length + 1];

        bGMNames[0] = "NONE";
        for (int i = 1; i < script.BGMSounds.Length + 1; i++)
        {
            bGMNames[i] = script.BGMSounds[i - 1].name;
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("BGM Lists", EditorStyles.boldLabel);

        if (script.bGMIndex.Length != SceneManager.sceneCountInBuildSettings)
        {
            Array.Resize(ref script.bGMIndex, SceneManager.sceneCountInBuildSettings);
        }

        for (int i = 0; i < script.bGMIndex.Length; i++)
        {
            script.bGMIndex[i] = EditorGUILayout.Popup(new GUIContent("Scene " + i), script.bGMIndex[i], bGMNames);
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        base.OnInspectorGUI();
    }
}

#endif