using UnityEngine;
using System.Collections;
using System.Linq;

public class Cadavre : MonoBehaviour {
	public Vector2[] Coins = new Vector2[4];
	// Use this for initialization
	void CalculCoordonees()
	{
		Vector2 p = transform.position;
		BoxCollider2D box = gameObject.GetComponent<BoxCollider2D> ();
		Coins [0] = new Vector2 (p.x-(box.size.x), p.y-(box.size.y));
		Coins [1] = new Vector2 (p.x+(box.size.x), p.y-(box.size.y));
		Coins [2] = new Vector2 (p.x+(box.size.x), p.y+(box.size.y));
		Coins [3] = new Vector2 (p.x-(box.size.x), p.y+(box.size.y));

		/*Coins[0] = transform.rotation*(Coins[0]-p);
		Coins[1] = transform.rotation*(Coins[1]-p);
		Coins[2] = transform.rotation*(Coins[2]-p);
		Coins[3] = transform.rotation*(Coins[3]-p);*/


//		GameObject[] points = new GameObject[4];
//		points [0] = GameObject.Find ("Point1");
//		points [1] = GameObject.Find ("Point2");
//		points [2] = GameObject.Find ("Point3");
//		points [3] = GameObject.Find ("Point4");
//		for(int i = 0; i < 4; i++)
//		{
//			points[i].transform.position = Coins[i];
//		}
	}

	void Start () {
		CalculCoordonees ();
		grabs[0] = grabs[1] = false;
	}
	
	// Update is called once per frame
	void Update () {
		CalculCoordonees ();
	}
	private bool[] grabs = new bool[2];
	public void Grab(int player){
		grabs[player-1] = true;
		UpdateSprite();
	}
	public void Ungrab(int player){
		grabs[player-1] = false;
		UpdateSprite();
	}
	private void UpdateSprite(){
		gameObject.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Pjs")
			.Where(s => s.name == ((grabs[0] || grabs[1]) ? "Body_carried" : "Body_down")).First();
	}
}
