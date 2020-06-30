using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using System.IO.Ports;


namespace UnityPsychBasics
{
    public class SetSerialDropdown : MonoBehaviour {
	    public Dropdown serialDropdown;

	    // Use this for initialization
	    void Start () {
		    SetSerialDropDownOptions ();
	    }
	
	    //added 
	    private void SetSerialDropDownOptions () {

		    serialDropdown.options.Clear ();

		    List<string> ports = new List<string> ();
		    foreach (string c in SerialPort.GetPortNames()){
			    ports.Add (c);
		    }

		    serialDropdown.AddOptions (ports);
		    serialDropdown.value = PlayerPrefs.GetInt ("serial_port");
	    }

        public void OnSelection() {
            PlayerPrefs.SetInt("serial_port", serialDropdown.value);
            SerialControl.instance.SetSerial(serialDropdown.options[serialDropdown.value].text);        
        }
    }
}
