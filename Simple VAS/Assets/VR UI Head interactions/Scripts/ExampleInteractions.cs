using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExampleInteractions : MonoBehaviour
{

    public Scrollbar m_scrollBar;

    public void OnButtonSelect()
    {
        Debug.Log("the button was pressed, things are not as they used to...");
    }

    public void OnSliderChange()
    {
        if (m_scrollBar != null)
            Debug.Log("the selected value is " + m_scrollBar.value + ", this will always be less than infinite just as your own life");
    }

    public void OnToggle()
    {
        Debug.Log("a toggle has changed, you have changed...");
    }
}