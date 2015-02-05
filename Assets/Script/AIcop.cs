using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIcop : MonoBehaviour {
    public float moveSpeed;
    public float turnSpeed;
    public bool tourneEnRond;
    public bool abandonneRecherche;
    public float distanceAbandon;
    public float distancePoursuiteJoueur;

    private Vector3 moveDirection;
    private Vector3 currentPosition;
    private int currentWaypoint;
    private bool retour;

    public List<GameObject> waypoints;
    private List<GameObject> playerWaypoints;
    private Cops scriptCops;

	// Use this for initialization
	void Start () {
        if (waypoints.Count == 0)
            return;

        retour = false;
        currentWaypoint = 0;
        currentPosition = transform.position;
        scriptCops = gameObject.GetComponent<Cops>();

        moveToward(waypoints[currentWaypoint].transform.position);
	}
	
	// Update is called once per frame
	void Update () {
        if (waypoints.Count == 0)
            return;

        currentPosition = transform.position;

        //si on a détecter le joueur avec le cadavre
        //if (scriptCops.CadavreVu()) 
        //{
        //}
        //else
        //{
            //Si on est arrivé au waypoint
            if (Vector3.Distance(waypoints[currentWaypoint].transform.position, currentPosition) < 1)
            {
                //Si le PNJ va toujours dans le même sens
                if (tourneEnRond)
                {
                    if (currentWaypoint == waypoints.Count - 1)
                    {
                        currentWaypoint = 0;
                    }
                    else
                    {
                        currentWaypoint++;
                    }
                }
                else //gestion des allers retours
                {
                    if (!retour)
                    {
                        if (currentWaypoint == waypoints.Count - 1)
                        {
                            retour = true;
                            currentWaypoint--;
                        }
                        else
                        {
                            currentWaypoint++;
                        }
                    }
                    else
                    {
                        if (currentWaypoint == 0)
                        {
                            retour = false;
                            currentWaypoint++;
                        }
                        else
                        {
                            currentWaypoint--;
                        }
                    }
                }
            }

            moveToward(waypoints[currentWaypoint].transform.position);
        //}
    }

    void huntingPlayer()
    {
        Vector3 berniePosition = GameObject.Find("Bernie").transform.position;
        Vector3 claudePosition = GameObject.Find("Claude").transform.position;
        Vector3 berniePositionPrecedente = berniePosition;

        //Si le flic est suffisament proche d'un joueur, il le suit directement
        if (Vector3.Distance(berniePosition, transform.position) <= distancePoursuiteJoueur)
        {
            moveToward(berniePosition);
        }
        else if (Vector3.Distance(claudePosition, transform.position) <= distancePoursuiteJoueur)
        {
            moveToward(berniePosition);
        }
        else//aucun joueur assez proche
        {
            //si le flic abandonne les recherches
            //if (abandonneRecherche && playerWaypoints.Count >= distanceAbandon)
            //{
            //        if (currentWaypoint == playerWaypoints.Count - 1)
            //        {
            //            retour = true;
            //            currentWaypoint--;
            //        }
            //        else
            //        {
            //            currentWaypoint++;
            //        }
            //        if (currentWaypoint == 0)
            //        {
            //            retour = false;
            //            currentWaypoint++;
            //        }
            //        else
            //        {
            //            currentWaypoint--;
            //        }
            //}
            //else
            //{

            //}
            if (Vector3.Distance(waypoints[currentWaypoint].transform.position, currentPosition) < 1)
                currentWaypoint++;

            moveToward(playerWaypoints[currentWaypoint].transform.position);
        }
        
        //Si le joueur s'est déplacé suffisament on cré un nouveau waypoint
        if (Vector3.Distance(berniePosition, berniePositionPrecedente) > distancePoursuiteJoueur)
        {
            GameObject tempWaypoint = new GameObject("tempWaypoint");
            tempWaypoint.transform.position = berniePosition;
            playerWaypoints.Add(tempWaypoint);
        }
    }

    void killPlayer()
    {
    }

    void moveToward(Vector3 destination)
    {
        moveDirection = destination - transform.position;
        moveDirection.z = 0;
        moveDirection.Normalize();

        Vector3 target = moveDirection * moveSpeed + currentPosition;
        transform.position = Vector3.Lerp(currentPosition, target, Time.deltaTime);

        //rotation
        float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg + 90f;
        transform.rotation =
            Quaternion.Slerp(transform.rotation,
                             Quaternion.Euler(0, 0, targetAngle),
                             turnSpeed * Time.deltaTime);
    }
}
