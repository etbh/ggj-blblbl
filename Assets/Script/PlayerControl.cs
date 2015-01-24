using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public int playerid;
	private bool grabbing = false;
	private GameObject grabbed;
	private Sprite[] sprites;
	private int animpos = 0;
	
	void Start () {
		sprites = Resources.LoadAll<Sprite>("Pjs");
	}

	void Update () {
		float horizontal = Input.GetAxis("Horizontal - J" + playerid);
		float vertical = Input.GetAxis("Vertical - J" + playerid);
		Vector3 translation = new Vector3(horizontal, vertical, 0);

		bool hold = Input.GetAxis("Hold - J" + playerid) > 0.5;
		//Debug.Log(hold);
		if (!grabbing && hold)
			grab();
		if (grabbing && !hold)
			release();

		if (translation.magnitude > 0){
			Vector3 rotation = transform.rotation.eulerAngles;

			transform.Translate(translation/100);

			if (grabbing){
				Vector3 toCadaver = GameObject.Find ("Cadavre").transform.position - transform.position;
				rotation.z = Mathf.Atan2(toCadaver.x, -toCadaver.y) * Mathf.Rad2Deg;
//				transform.rotation = Quaternion.Euler(rotation);
//				transform.Translate(transform.rotation * translation  / 100);
//				Debug.Log (transform.rotation.eulerAngles);
//				//transform.Rotate(new Vector3(0, 0, Quaternion.Angle(transform.rotation, Quaternion.Euler(rotation))));
//				string spritename = ("" + (char)('A' + playerid) + "_grab"  + 
//				                     ( (animpos/10)% 4) +1)  ;
//				foreach(Sprite sprite in sprites)
//					if (sprite.name == spritename)
//						GetComponent<SpriteRenderer>().sprite = sprite;

				GetComponentsInChildren<Transform>()[1].rotation = Quaternion.Euler( rotation);


			}
			else{
//				rotation.z = Mathf.Atan2(translation.x, -translation.y) * Mathf.Rad2Deg;
//				Quaternion qrot = Quaternion.Euler(rotation);
//				// transform.rotation = new Quaternion(qrot.x, qrot.y, qrot.z, qrot.w);
//				// ceci est un commentaire
//				// these comments are in multiple languages
//				// si si si, por favor !
//				// std::cout << "Hello World" << std::endl;
//				float angle = Quaternion.Angle(transform.rotation, qrot);
//				if (angle != 0)
//					Debug.Log(angle);
//				transform.Rotate(new Vector3(0, 0, angle));
//				string spritename = ("" + (char)('A' + playerid) + "_walk" + 
//				                     ( (animpos/10)% 8) +1)  ;
//				foreach(Sprite sprite in sprites)
//					if (sprite.name == spritename)
//						GetComponent<SpriteRenderer>().sprite = sprite;
//				transform.Translate(new Vector3(0, -translation.magnitude, 0) / 100);


				//Debug.Log(GetComponentsInChildren<Transform>().Length);
				GetComponentsInChildren<Transform>()[1].rotation =
					Quaternion.Euler(new Vector3(0,0, Mathf.Atan2(translation.x, -translation.y) * Mathf.Rad2Deg));
			}
			animpos ++;

		}
		else{
//			foreach(Sprite sprite in sprites)
//				if (sprite.name == ("" + (char)('A' + playerid) + "_walk1"))
//					GetComponentsInChildren<SpriteRenderer>().sprite = sprite;
		}
		if (grabbing){
			Vector3 distance  =(transform.position - grabbed.transform.position);
			if (distance.magnitude > .3)
				grabbed.transform.Translate(distance / 50);
		}

	}

	void grab() {
		Vector3 target = transform.position - GetComponentsInChildren<Transform>()[1].rotation * new Vector3(0, 1, 0) / 3;
		//transform.position = target; // (was a test)

		Collider2D[] colliders = Physics2D.OverlapPointAll(target);

		foreach (Collider2D collider in colliders){
			if (collider.gameObject.CompareTag("Grabbable")){
				grabbed = collider.gameObject;
				grabbing = true;
			}
		}




	}

	void release(){

		grabbing = false;
	}
}
