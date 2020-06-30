using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;


namespace UnityPsychBasics
{
    public class SerialControl : MonoBehaviour {
	
	    //[HideInInspector]
	    public string portName;//the COM Port

	    public int baudRate = 9600;//Fixed to 9600 for the trigger box.
	    SerialPort serialDevice;

        public static SerialControl instance;

        private void Awake()
        {
            if (instance == null) instance = this;    
        }

        public void SetSerial(string port) {
            portName = port;
            Debug.Log(serialDevice);

            if(serialDevice != null && serialDevice.IsOpen) serialDevice.Close(); //close device when setting up if open

            serialDevice = new SerialPort(portName, baudRate); //initializes a serial port
            if (serialDevice != null) serialDevice.Close(); //makes sure the device is closed before openning
            serialDevice.Open(); //opens serial device
        }

	    public void WriteToPort(string message){
		    if (serialDevice.IsOpen) {
			    serialDevice.Write (message); //if the device is open, send string message when function is called.
			    Debug.Log("sent message " + message); //writes message to console (this does not confirm that it was received)
		    }
	    }

    }
}
