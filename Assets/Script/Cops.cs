using UnityEngine;
using System.Collections;

public class Cops : PNJ {
	// Use this for initialization
	void Start () {
		Radius = 3.0;
		AngleB = 0.0;
		AngleT = 60.0*(Mathf.PI/180);
		p = transform.position;
		GameObject[] points = new GameObject[4];
		points [0] = GameObject.Find ("Point5");
		points [1] = GameObject.Find ("Point6");
		points [2] = GameObject.Find ("Point7");
		points [3] = GameObject.Find ("Point8");
		
		points [0].transform.position = p;
		points [1].transform.position = new Vector2 (Mathf.Cos(0f)*(float)Radius, Mathf.Sin(0f)*(float)Radius) + p;
		points [3].transform.position = new Vector2 ((float)(Radius* Mathf.Cos((float)AngleT)), (float)(Radius*Mathf.Sin((float)AngleT))) + p;
		points [2].transform.position = new Vector2 ((float)(Radius*Mathf.Cos(30.0f*(Mathf.PI/180))), (float)(Radius*Mathf.Sin(30.0f*(Mathf.PI/180)))) + p;
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
		//Go to v
	}
	
	// Update is called once per frame
	void Update () {
		CadavreVu ();
	}
}
