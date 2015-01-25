using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIcop : MonoBehaviour {
    public float moveSpeed;
    public float turnSpeed;
    public bool tourneEnRond;

    private Vector3 moveDirection;
    private int currentWaypoint;
    private bool retour;

    public List<GameObject> waypoints;

	// Use this for initialization
	void Start () {
        retour = false;
        currentWaypoint = 0;

        Vector3 moveToward = waypoints[currentWaypoint].transform.position;

        moveDirection = moveToward - transform.position;
        moveDirection.z = 0;
        moveDirection.Normalize();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 currentPosition = transform.position;
        Vector3 moveToward;

        //Si on est arrivé au waypoint
        if (Vector3.Distance(waypoints[currentWaypoint].transform.position, currentPosition) < 1)
        {
            //Si le PNJ va toujours dans le même sens
            if (tourneEnRond)
            {
                if(currentWaypoint == waypoints.Count - 1) {
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

            Debug.Log(currentWaypoint + " " + retour);

            moveToward = waypoints[currentWaypoint].transform.position;

            moveDirection = moveToward - transform.position;
            moveDirection.z = 0;
            moveDirection.Normalize();
        }

        //déplacement
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
