using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public int playerid;
	private bool grabbing = false;
	private GameObject grabbed;
	
	void Start () {

		
	}

	void Update () {
		float horizontal = Input.GetAxis("Horizontal - J" + playerid);
		float vertical = Input.GetAxis("Vertical - J" + playerid);
		Vector3 translation = new Vector3(horizontal, vertical, 0);
		if (translation.magnitude > 0){
			Vector3 rotation = transform.rotation.eulerAngles;
			rotation.z = Mathf.Atan2(translation.x, -translation.y) * Mathf.Rad2Deg;
			Quaternion qrot = Quaternion.Euler(rotation);
			//transform.rotation = new Quaternion(qrot.x, qrot.y, qrot.z, qrot.w);
			transform.Rotate(new Vector3(0, 0, Quaternion.Angle(transform.rotation, qrot)));
		}
		transform.Translate(new Vector3(0, -translation.magnitude, 0) / 100);
		if (grabbing){
			Vector3 distance  =(transform.position - grabbed.transform.position);
			if (distance.magnitude > .3)
				grabbed.transform.Translate(distance / 50);
		}
		bool hold = Input.GetAxis("Hold - J" + playerid) > 0.5;
		//Debug.Log(hold);
		if (!grabbing && hold)
			grab();
		if (grabbing && !hold)
			release();

	}

	void grab() {
		Vector3 target = transform.position - transform.rotation * new Vector3(0, 1, 0) / 3;
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
