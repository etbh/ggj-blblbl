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
		Coins [0] = new Vector2 (p.x-(box.size.x * transform.localScale.x/2), p.y-(box.size.y * transform.localScale.y/2));
		Coins [1] = new Vector2 (p.x+(box.size.x * transform.localScale.x/2), p.y-(box.size.y * transform.localScale.y/2));
		Coins [2] = new Vector2 (p.x+(box.size.x * transform.localScale.x/2), p.y+(box.size.y * transform.localScale.y/2));
		Coins [3] = new Vector2 (p.x-(box.size.x * transform.localScale.x/2), p.y+(box.size.y * transform.localScale.y/2));

		GameObject[] points = new GameObject[4];
		points [0] = GameObject.Find ("Point1");
		points [1] = GameObject.Find ("Point2");
		points [2] = GameObject.Find ("Point3");
		points [3] = GameObject.Find ("Point4");

		for(int i = 0; i < 4; i++)
		{
			Coins[i] = p+(Vector2)(transform.rotation*(Coins[i]-p));
			points[i].transform.position = Coins[i];
		}
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
	public int howGrabbed(){
		return (grabs[0]?1:0) + (grabs[1]?1:0);
	}
}
