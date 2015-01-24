using UnityEngine;
using System.Collections;

public class Cadavre : MonoBehaviour {
	public Vector2[] Coins = new Vector2[4];
	// Use this for initialization
	void CalculCoordonees()
	{
		Vector2 p = transform.position;
		
		Coins [0] = new Vector2 (p.x-2, p.y-0.5f);
		Coins [1] = new Vector2 (p.x+2, p.y-0.5f);
		Coins [2] = new Vector2 (p.x+2, p.y+0.5f);
		Coins [3] = new Vector2 (p.x-2, p.y+0.5f);
		
		Coins[0] = transform.rotation*(Coins[0]-p);
		Coins[1] = transform.rotation*(Coins[1]-p);
		Coins[2] = transform.rotation*(Coins[2]-p);
		Coins[3] = transform.rotation*(Coins[3]-p);
	}

	void Start () {
		CalculCoordonees ();
	}
	
	// Update is called once per frame
	void Update () {
		CalculCoordonees ();
	}
}
