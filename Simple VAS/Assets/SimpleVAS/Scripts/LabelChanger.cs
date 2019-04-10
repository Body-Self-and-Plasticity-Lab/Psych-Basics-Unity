using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabelChanger : MonoBehaviour {

    public List<string> leftLabelStrings = new List<string>();
    public List<string> rightLabelStrings = new List<string>();

    public Text leftLabel, rightLabel;

    public static LabelChanger instance;


    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void NextLabel(int item) {

        if (item < leftLabelStrings.Count) {
            leftLabel.text = leftLabelStrings[item];
            rightLabel.text = rightLabelStrings[item];
        }
        else
            Debug.Log("index is out of range, you entered a number larger than the label strings in your dictionary");
    }
}
