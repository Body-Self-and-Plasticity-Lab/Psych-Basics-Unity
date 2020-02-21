using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneAfterTask : MonoBehaviour {

    public string beforeLastCondition, afterLastCondition;

    public static LoadSceneAfterTask instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

}
