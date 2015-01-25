using UnityEngine;
using System.Collections;

abstract public class PNJ : MonoBehaviour {
	public double Radius;
	public double AngleB;
	public double AngleT;
	public Vector2 p;
	public Cadavre cad;
	public ArrayList cops;
	// Use this for initialization
	void Start () {
		Radius = 5.0;
		AngleB = 0.0;
		AngleT = 60.0*(Mathf.PI/180);
		p = transform.position;

		GameObject[] points = new GameObject[4];
		points [0] = GameObject.Find ("Point5");
		points [1] = GameObject.Find ("Point6");
		points [2] = GameObject.Find ("Point7");
		points [3] = GameObject.Find ("Point8");

		points [0].transform.position = p;
		points [1].transform.position = new Vector2 (Mathf.Cos(0f), Mathf.Sin(0f));
		points [2].transform.position = new Vector2 ((float)(Radius)*Mathf.Cos((float)AngleT), (float)(Radius)*Mathf.Sin((float)AngleT));
		points [3].transform.position = new Vector2 ((float)(Radius)*Mathf.Cos(30.0f*(Mathf.PI/180)), (float)(Radius)*Mathf.Sin(30.0f*(Mathf.PI/180)));
	}

	double min(double a, double b, double c, double d)
	{
		if (a < b) {
			if(a<c)
			{
				if(a < d)
				{
					return a;
				}
				else
				{
					return d;
				}
			}
			else
			{
				if(c < d)
				{
					return c;
				}
				else
				{
					return c;
				}
			}
		} else {
			if(b<c)
			{
				if(b < d)
				{
					return b;
				}
				else
				{
					return d;
				}
			}
			else
			{
				if(c < d)
				{
					return c;
				}
				else
				{
					return c;
				}
			}
		}
	}
	
	double max(double b, double c, double d)
	{
			if(b>c)
			{
				if(b > d)
				{
					return b;
				}
				else
				{
					return d;
				}
			}
			else
			{
				if(c > d)
				{
					return c;
				}
				else
				{
					return c;
				}
			}
	}

	public void CadavreVu()
	{
		//Déterminer l'angle min qui voit tout le cadavre
		double a, b, c, d;
		float x, y;
		x = cad.Coins[0].x - p.x;
		y = cad.Coins[0].y - p.y;
		a = Mathf.Atan2 (y, x);
		//Debug.Log ("a=" + a);
		x = cad.Coins[1].x - p.x;
		y = cad.Coins[1].y - p.y;
		b = Mathf.Atan2 (y, x);
		//Debug.Log ("b=" + b);
		x = cad.Coins[2].x - p.x;
		y = cad.Coins[2].y - p.y;
		c = Mathf.Atan2 (y, x);
		//Debug.Log ("c=" + c);
		x = cad.Coins[3].x - p.x;
		y = cad.Coins[3].y - p.y;
		d = Mathf.Atan2 (y, x);
		//Debug.Log ("d=" + d);
		double temp;
		temp = min (a, b, c, d);
		if (temp != a)
		{
			if(temp==b)
			{
				temp = a;
				a = b;
				b = temp;
			} else if(temp==c)
			{
				temp = a;
				a = c;
				c = temp;
			} else{
				temp = a;
				a = d;
				d = temp;
			}
		}
		temp = max (b, c, d);
		if (temp != b) {
			if(temp == c)
			{
				temp = b;
				b = c;
				c = temp;
			} else
			{
				temp = b;
				b = d;
				d = temp;
			}
		}
		//Distance assez courte?
		Vector2 centre = cad.transform.position;
		//centre.x = (cad.Coins[2].x - cad.Coins[0].x)/2; 
		//centre.y = (cad.Coins[2].y - cad.Coins[0].y)/2;
		float distC = Mathf.Sqrt(Mathf.Pow((float)(centre.x-p.x), 2) + Mathf.Pow((float)(centre.y-p.y), 2));
		float dist1, dist2, dist3, dist4;
		dist1 = Mathf.Sqrt(Mathf.Pow((float)(cad.Coins[0].x-p.x), 2) + Mathf.Pow((float)(cad.Coins[0].y-p.y), 2));
		dist2 = Mathf.Sqrt(Mathf.Pow((float)(cad.Coins[1].x-p.x), 2) + Mathf.Pow((float)(cad.Coins[1].y-p.y), 2));
		dist3 = Mathf.Sqrt(Mathf.Pow((float)(cad.Coins[2].x-p.x), 2) + Mathf.Pow((float)(cad.Coins[2].y-p.y), 2));
		dist4 = Mathf.Sqrt(Mathf.Pow((float)(cad.Coins[3].x-p.x), 2) + Mathf.Pow((float)(cad.Coins[3].y-p.y), 2));
		if(distC<=Radius || dist1<=Radius || dist2<=Radius || dist3<=Radius || dist4<=Radius)
		{
			//Debug.Log("Cadavre a distance");
			bool continuer = false;
			//Est on dans la meme direction?
			if((a>AngleB && a<(AngleB+AngleT)))
			{
				continuer = true;
				b = AngleB+AngleT;
			} else if(b>AngleB && b<(AngleB+AngleT)) {
				continuer = true;
				a = AngleB;
			} else if(a<AngleB && b>(AngleB+AngleT)) {
				continuer = true;
			}
			if(continuer)
			{
				//Debug.Log("Cadavre dans ma direction");
				bool vu = false;
				//Visible ou obstacle?
				for(double i = a; i < b; i+=0.01)
				{
					RaycastHit2D r = Physics2D.Raycast(p, new Vector2(Mathf.Cos((float)i), Mathf.Sin((float)i)), (float)Radius);
					if(r.collider != null)
					{
						GameObject o = r.collider.gameObject;
						if(o.layer == 8)
						{
							vu = true;
							break;
						}					
					}
				}
				if(vu)
				{
					Debug.Log("Cadavre trouvé! Alerte général!");
					CallCops(centre);
					ChangePath(centre);
				}
			}
		}
	}

	abstract public IEnumerator CallCops(Vector2 pos);
	abstract public void ChangePath (Vector2 pos);
	// Update is called once per frame
	void Update () {


	}
}