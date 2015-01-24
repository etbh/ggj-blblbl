using UnityEngine;
using System.Collections;

public class Cops : PNJ {
	// Use this for initialization
	void Start () {
		cops = new ArrayList ();
		GameObject[] copstemp = GameObject.FindGameObjectsWithTag ("cop");
		foreach(GameObject cop in copstemp)
		{
			Cops c = Instantiate(cop) as Cops;
			cops.Add(c);
		}
		Radius = 5.0;
		AngleB = 0.0;
		AngleT = 60.0*(Mathf.PI/180);
		p.x = 50.0f;
		p.y = 50.0f;
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
