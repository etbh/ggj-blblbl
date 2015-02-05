using UnityEngine;
using System.Collections;

public class NextLevel : MonoBehaviour {
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.name == "Cadavre"){
			if (Application.loadedLevelName == "Level1"){
				Application.LoadLevel("Level2");
			}
			if (Application.loadedLevelName == "Level2"){
				Application.LoadLevel("Level3");
			}
			if (Application.loadedLevelName == "Level3"){
				//
			}

		}
	}
}