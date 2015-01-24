using UnityEngine;
using System.Collections;

public class Cops : PNJ {
	// Use this for initialization
	void Start () {
		Radius = 5.0;
		AngleB = 0.0;
		AngleT = 60.0*(Mathf.PI/180);
		p = transform.position;
	}

	public override IEnumerator CallCops(Vector2 pos)
	{
		yield return new WaitForSeconds(5);
		foreach (Cops c in cops)
		{
			c.ChangePath(pos);
		}
	}
	
	public override void ChangePath(Vector2 pos)
	{
		Vector2 v = pos;
	}
	
	// Update is called once per frame
	void Update () {
		CadavreVu ();
	}
}
