using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityPsychBasics
{
    public class WebcamUI : MonoBehaviour {

	    public RawImage display;
	    public Dropdown webcamSelector;

	    private WebCamTexture webcamTexture;

	    // Use this for initialization
	    void Start () {
		    webcamSelector.value = PlayerPrefs.GetInt ("selectedCamera");
		    SetWebCamOptions ();
		    SelectWebcam ();
	    }

	    public void SetWebCamOptions(){
		    webcamSelector.options.Clear ();

		    List<string> devices = new List<string> ();

		    foreach (var item in WebCamTexture.devices) {
			    devices.Add(item.name);
		    }

		    webcamSelector.AddOptions (devices);
	    }

	    public void SelectWebcam () {

		    if(webcamTexture != null) webcamTexture.Stop();

		    WebCamDevice[] devices = WebCamTexture.devices;
		    string deviceName = devices[webcamSelector.value].name;
		    webcamTexture = new WebCamTexture(deviceName);

		    display.texture = webcamTexture;

		    webcamTexture.Play();
	    }


	    void OnDestroy(){
		    PlayerPrefs.SetInt ("selectedCamera", webcamSelector.value);
		    if(webcamTexture != null) webcamTexture.Stop();
	    }
    }
}