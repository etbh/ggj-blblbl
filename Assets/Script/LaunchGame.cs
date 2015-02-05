using UnityEngine;
using System.Collections;

public class LaunchGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey("joystick button 0")){
			Application.LoadLevel("Level1");
		}
	}
}