using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public int playerid;
	public float speed = 1;
	public float anim_speed = 1;
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
		horizontal = Mathf.Abs(horizontal)<.7 ? 0 : horizontal;
		vertical = Mathf.Abs(vertical)<.7 ?0 : vertical;
		Vector3 translation = new Vector3(horizontal, vertical, 0);

		//Debug.Log(horizontal);

		bool hold = Input.GetAxis("Hold - J" + playerid) > 0.5;
		Debug.Log(hold);

		if (!grabbing && hold)
			grab();
		if (grabbing && !hold)
			release();
		//Debug.Log(grabbing);
		//Debug.Log (translation.magnitude);
		if (translation.magnitude > 0){
			Vector3 rotation = transform.rotation.eulerAngles;


			if (grabbing){
				Vector3 toCadaver = GameObject.Find ("Cadavre").transform.position - transform.position;
				rotation.z = Mathf.Atan2(toCadaver.x, -toCadaver.y) * Mathf.Rad2Deg;
//				transform.rotation = Quaternion.Euler(rotation);
//				transform.Translate(transform.rotation * translation  / 100);
//				Debug.Log (transform.rotation.eulerAngles);
//				//transform.Rotate(new Vector3(0, 0, Quaternion.Angle(transform.rotation, Quaternion.Euler(rotation))));
				string spritename = ("" + (char)('A' + playerid) + "_grab"  + 
				                     (1 + (anim_speed * animpos/10)% 4));
				foreach(Sprite sprite in sprites)
					if (sprite.name == spritename)
						GetComponentsInChildren<SpriteRenderer>()[0].sprite = sprite;

				GetComponentsInChildren<Transform>()[1].rotation = Quaternion.Euler( rotation);
				Cadavre corpse = grabbed.GetComponent<Cadavre>();
				int mult = corpse == null ? 1 : corpse.howGrabbed();
				transform.Translate(mult * speed * translation/100);

				if ((GameObject.Find ("Cadavre").transform.position - transform.position).magnitude > 4){
					Debug.Log ("Too bad");
					release();
				}


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
				string spritename = ("" + (char)('A' + playerid) + "_walk" + 
				                     ( 1+ (anim_speed * animpos/10)% 8));
				foreach(Sprite sprite in sprites)
					if (sprite.name == spritename)
						GetComponentsInChildren<SpriteRenderer>()[0].sprite = sprite;


				GetComponentsInChildren<Transform>()[1].rotation =
					Quaternion.Euler(new Vector3(0,0, Mathf.Atan2(translation.x, -translation.y) * Mathf.Rad2Deg));

				transform.Translate(speed * translation/50);
			}
			animpos ++;

		}
		else {
			foreach(Sprite sprite in sprites)
				if (sprite.name == ("" + (char)('A' + playerid) + "_walk1"))
					GetComponentsInChildren<SpriteRenderer>()[0].sprite = sprite;
		}
		if (grabbing){
			Vector3 distance  =(transform.position - grabbed.transform.position);
			if (distance.magnitude > .3){
				Cadavre corpse = grabbed.GetComponent<Cadavre>();
				int mult = corpse == null ? 1 : corpse.howGrabbed();
				grabbed.transform.rotation = Quaternion.Euler( new Vector3(0,0,0));
				grabbed.transform.Translate(mult * speed * distance / 50);
			}
		}
	}

	void grab() {
		Vector3 target = transform.position - GetComponentsInChildren<Transform>()[1].rotation * new Vector3(0, 1, 0) / 3;
		//transform.position = target; // (was a test)

		Collider2D[] colliders = Physics2D.OverlapAreaAll(
			new Vector2(target.x - 1, target.y -1),
			new Vector2(target.x + 1, target.y +1)
		);

		foreach (Collider2D collider in colliders){
			if (collider.gameObject.CompareTag("Grabbable")){
				Debug.Log ("Grabbed");
				grabbed = collider.gameObject;
				grabbing = true;
				Cadavre corpse = grabbed.GetComponent<Cadavre>();
				if (corpse != null)
					corpse.Grab(playerid);
			}
		}




	}

	void release(){

		grabbing = false;
		Cadavre corpse = grabbed.GetComponent<Cadavre>();
		if (corpse != null)
			corpse.Ungrab(playerid);
	}
}
