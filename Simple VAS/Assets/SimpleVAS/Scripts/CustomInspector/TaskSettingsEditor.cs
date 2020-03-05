using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace UnityPsychBasics {

    [CustomEditor(typeof(TaskSettings))]
    public class TaskSettingsEditor : Editor {

        private int previousSize;
        
        override public void OnInspectorGUI() {

           var myScript = target as TaskSettings;

            myScript.sceneBeforeLastCondition = EditorGUILayout.TextField("Scene Before Last", myScript.sceneBeforeLastCondition);
            myScript.sceneAfterLastCondition = EditorGUILayout.TextField("Scene Before Last", myScript.sceneAfterLastCondition);
            EditorGUILayout.Space();

            myScript.withinScene = EditorGUILayout.Toggle("Tasks within Scene", myScript.withinScene);
            EditorGUILayout.Space();

            using (var group = new EditorGUILayout.FadeGroupScope(Convert.ToSingle(myScript.withinScene))) {
                if (group.visible == true)
                    TasksWithinSceneInspectorOptions(myScript);
                else
                    TasksInDifferentScenesOptions(myScript);
            }
        }

        private void TasksWithinSceneInspectorOptions(TaskSettings myScript) {
            EditorGUI.indentLevel++;
            myScript.numberOfConditions = EditorGUILayout.IntField("Number of Conditions", myScript.numberOfConditions);
            EditorGUILayout.Space();

            if (myScript.numberOfConditions != previousSize) {
                CleanLists(myScript);
                PopulateLists(myScript);
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("shuffle"), new GUIContent("Shuffle"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("useImage"), new GUIContent("Use Image"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("analogueScale"), new GUIContent("Use VAS"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("useMouseClickSelector"), new GUIContent("Use Mouse Selector"), true);
            serializedObject.ApplyModifiedProperties();

            EditorGUI.indentLevel--;
        }

        private void TasksInDifferentScenesOptions(TaskSettings myScript){
            EditorGUI.indentLevel++;

            myScript.shuffleBool = EditorGUILayout.Toggle("Shuffle", myScript.shuffleBool);
            myScript.useImageBool = EditorGUILayout.Toggle("Use Image", myScript.useImageBool);
            myScript.useAnalogueScaleBool = EditorGUILayout.Toggle("Use VAS", myScript.useAnalogueScaleBool);
            myScript.useMouseBool = EditorGUILayout.Toggle("Use Mouse Selector", myScript.useMouseBool);

            EditorGUI.indentLevel--;
        }

        private void CleanLists(TaskSettings myScript) {
            myScript.shuffle.Clear();
            myScript.useImage.Clear();
            myScript.analogueScale.Clear();
            myScript.useMouseClickSelector.Clear();
        }

         private void PopulateLists(TaskSettings myScript) {
             for (int i = 0; i < myScript.numberOfConditions; i++) {
                   myScript.shuffle.Add(false);
                   myScript.useImage.Add(false);
                   myScript.analogueScale.Add(false);
                   myScript.useMouseClickSelector.Add(false);
               }

               previousSize = myScript.numberOfConditions;
           }

        }
}