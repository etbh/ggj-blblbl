using UnityEngine;
using System.Collections;


public class Civil : PNJ {
	// Use this for initialization
	void Start () {
		System.Console.Write ("InitCivil");
		Radius = 5.0;
		AngleB = 0.0;
		AngleT = 60.0*(Mathf.PI/180);
		p = transform.position;
	}

	public override IEnumerator CallCops(Vector2 pos)
	{
		yield return new WaitForSeconds(2);
		foreach (Cops c in cops)
		{
			c.ChangePath(pos);
		}
	}

	public override void ChangePath(Vector2 pos)
	{
		Vector2 v = new Vector2 (p.x - pos.x, p.y - pos.y);
		//Get away from p/Go to v direction
	}
	
	// Update is called once per frame
	void Update () {
		CadavreVu ();
	}
}
