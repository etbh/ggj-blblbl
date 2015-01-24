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
	
	double max(double a, double b, double c, double d)
	{
		if (a > b) {
			if(a>c)
			{
				if(a > d)
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
				if(c > d)
				{
					return c;
				}
				else
				{
					return c;
				}
			}
		} else {
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
	}

	public void CadavreVu()
	{
		//Déterminer l'angle min qui voit tout le cadavre
		double a, b, c, d;
		float x, y;
		cad.Coins[0].x = 0.0f;
		p.x = 0.0f;
		x = cad.Coins[0].x - p.x;
		y = cad.Coins[0].y - p.y;
		a = Mathf.Atan2 (y, x);
		x = cad.Coins[1].x - p.x;
		y = cad.Coins[1].x - p.x;
		b = Mathf.Atan2 (y, x);
		x = cad.Coins[2].x - p.x;
		y = cad.Coins[2].x - p.x;
		c = Mathf.Atan2 (y, x);
		x = cad.Coins[3].x - p.x;
		y = cad.Coins[3].x - p.x;
		d = Mathf.Atan2 (y, x);
		a = min (a, b, c, d);
		b = max (a, b, c, d);

		//Distance assez courte?
		Vector2 centre;
		centre.x = (cad.Coins[2].x - cad.Coins[0].x)/2; 
		centre.y = (cad.Coins[2].y - cad.Coins[0].y)/2;
		float dist = Mathf.Sqrt(Mathf.Pow((float)(centre.x-p.x), 2) + Mathf.Pow((float)(centre.x-p.x), 2));
		if(dist<=Radius)
		{
			bool continuer = false;
			//Est on dans la meme direction?
			if((a>AngleB && a<(AngleB+AngleT)))
			{
				continuer = true;
				b = AngleB+AngleT;
			}
			else if(b>AngleB && b<(AngleB+AngleT))
			{
				continuer = true;
				a = AngleB;
			}
			else if(a<AngleB && b>(AngleB+AngleT))
			{
				continuer = true;
			}
			if(continuer)
			{
				bool vu = false;
				//Visible ou obstacle?
				for(double i = a; i < b; i+=0.1)
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