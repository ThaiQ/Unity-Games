//Created by ThaiQ
//Based by Brayden Cloud (Youtube Channel)'s movement script
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;

public class RandomGhost : MonoBehaviour {

    public GameObject GroupOfWaypoints;
    private wpt[] waypoints;
    public wpt CurrentWaypoint;
    public wpt.direction direction;
    public float speed = 2f;
    public float TurnForgiveness = 0.1f;
    public wpt Rt;
    public wpt Lt;
    public wpt exits;
    public wpt spawn;
    private int randomdirection;
    // Use this for initialization
    void Start()
    {
        waypoints = new wpt[GroupOfWaypoints.transform.childCount];
        int i = 0;
        foreach (Transform transf in GroupOfWaypoints.transform)
        {
            waypoints[i] = transf.gameObject.GetComponent<wpt>();
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Doing the change of direction according to the pressed key
        if (randomdirection == 0)
        {
            this.SetDirection(wpt.direction.U);
            if (direction == wpt.direction.U)
            {
                transform.localScale = new Vector2(0.01f, 0.01f);
                transform.localRotation = Quaternion.Euler(0, 0, 90);
            }
        }
        if (randomdirection == 1)
        {
            this.SetDirection(wpt.direction.D);
            if (direction == wpt.direction.D)
            {
                transform.localScale = new Vector2(0.01f, 0.01f);
                transform.localRotation = Quaternion.Euler(0, 0, 270);
            }
        }
        if (randomdirection == 2)
        {
            this.SetDirection(wpt.direction.L);
            if (direction == wpt.direction.L)
            {
                transform.localScale = new Vector2(-0.01f, 0.01f);
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }
        if (randomdirection == 3)
        {
            this.SetDirection(wpt.direction.R);
            if (direction == wpt.direction.R)
            {
                transform.localScale = new Vector2(0.01f, 0.01f);
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }
        //Doing the movement
        if (!CheckForDestination())
        {
            switch (direction)
            {
                case wpt.direction.L: transform.position = new Vector3(transform.position.x - (Time.deltaTime * speed), transform.position.y, transform.position.z); break;
                case wpt.direction.R: transform.position = new Vector3(transform.position.x + (Time.deltaTime * speed), transform.position.y, transform.position.z); break;
                case wpt.direction.D: transform.position = new Vector3(transform.position.x, transform.position.y - (Time.deltaTime * speed), transform.position.z); break;
                case wpt.direction.U: transform.position = new Vector3(transform.position.x, transform.position.y + (Time.deltaTime * speed), transform.position.z); break;
            }
        }
        //Doing the changing of next waypoints
        else
        {
            var nextwaypoint = NextWaypoint(CurrentWaypoint, direction);

            // Teleportation if at the portal
            if (CurrentWaypoint.transform.position == Rt.transform.position && direction == wpt.direction.R)
            {
                this.transform.position = Lt.transform.position;
                CurrentWaypoint = Lt;
                return;
            }
            else if (CurrentWaypoint.transform.position == Lt.transform.position && direction == wpt.direction.L)
            {
                this.transform.position = Rt.transform.position;
                CurrentWaypoint = Lt;
                return;
            }

            //becoming next waypoint
            if (nextwaypoint != null)
            {
                CurrentWaypoint = nextwaypoint;
            }
            else
            {
                transform.position = CurrentWaypoint.transform.position;
            }
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////Changing direction
    void SetDirection(wpt.direction dir)
    {
        bool CanChange = false;
        foreach (var directions in CurrentWaypoint.ValidDiriections)
        {
            if (NearNextWpt())
            {
                randomdirection = random();
                if (directions == dir) { CanChange = true;}
            }
            else
            {
                //if (Mathf.Abs((int)direction) == Mathf.Abs((int)dir)) { CanChange = true; }
            }
        }

        if (CanChange)
        {
            this.direction = dir;
        }

    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////Deciding the next waypoint associate to the possible direction
    public wpt NextWaypoint(wpt CurrentPoint, wpt.direction dir)
    {
        var CurrentPosition = CurrentPoint.transform.position;
        wpt Target = null;

        if (atspawn() == true)
        {
            Target = exits;
        }

        foreach (var NextWaypoint in waypoints)
        {
            var NextPosition = NextWaypoint.transform.position;
            switch (dir)
            {
                case wpt.direction.L: //if the next waypoint is too the left, current x > next x, current y = next y.
                    if (NextPosition.x < CurrentPosition.x && NextPosition.y == CurrentPosition.y && NextWaypoint != CurrentPoint)
                    {
                        if (Target != null)
                        {
                            if (distance(CurrentPosition, NextPosition, wpt.direction.L) < distance(CurrentPosition, Target.transform.position, wpt.direction.L))
                            {
                                if (CanGo(CurrentPoint, wpt.direction.L) && CanGo(NextWaypoint, wpt.direction.R))
                                {
                                    Target = NextWaypoint;
                                }
                            }
                        }
                        else
                        {
                            if (CanGo(CurrentPoint, wpt.direction.L) && CanGo(NextWaypoint, wpt.direction.R))
                            {
                                Target = NextWaypoint;
                            }
                        }
                    }
                    break;
                case wpt.direction.R: //if the next waypoint is to the right, current x < next x, current y = next y.
                    if (NextPosition.x > CurrentPosition.x && NextPosition.y == CurrentPosition.y && NextWaypoint != CurrentPoint)
                    {
                        if (Target != null)
                        {
                            if (distance(CurrentPosition, NextPosition, wpt.direction.R) < distance(CurrentPosition, Target.transform.position, wpt.direction.R))
                            {
                                if (CanGo(CurrentPoint, wpt.direction.R) && CanGo(NextWaypoint, wpt.direction.L))
                                {
                                    Target = NextWaypoint;
                                }
                            }
                        }
                        else
                        {
                            if (CanGo(CurrentPoint, wpt.direction.R) && CanGo(NextWaypoint, wpt.direction.L))
                            {
                                Target = NextWaypoint;
                            }
                        }
                    }
                    break;
                case wpt.direction.U: //if the next waypoint is up, current y < next y, current x = next x.
                    if (NextPosition.y > CurrentPosition.y && NextPosition.x == CurrentPosition.x && NextWaypoint != CurrentPoint)
                    {
                        if (Target != null)
                        {
                            if (distance(CurrentPosition, NextPosition, wpt.direction.U) < distance(CurrentPosition, Target.transform.position, wpt.direction.U))
                            {
                                if (CanGo(CurrentPoint, wpt.direction.U) && CanGo(NextWaypoint, wpt.direction.D))
                                {
                                    Target = NextWaypoint;
                                }
                            }
                        }
                        else
                        {
                            if (CanGo(CurrentPoint, wpt.direction.U) && CanGo(NextWaypoint, wpt.direction.D))
                            {
                                Target = NextWaypoint;
                            }
                        }
                    }
                    break;
                case wpt.direction.D: //if the next waypoint is down, current y > next y, current x = next x.
                    if (NextPosition.y < CurrentPosition.y && NextPosition.x == CurrentPosition.x && NextWaypoint != CurrentPoint)
                    {
                        if (Target != null)
                        {
                            if (distance(CurrentPosition, NextPosition, wpt.direction.D) < distance(CurrentPosition, Target.transform.position, wpt.direction.D))
                            {
                                if (CanGo(CurrentPoint, wpt.direction.D) && CanGo(NextWaypoint, wpt.direction.U))
                                {
                                    Target = NextWaypoint;
                                }
                            }
                        }
                        else
                        {
                            if (CanGo(CurrentPoint, wpt.direction.D) && CanGo(NextWaypoint, wpt.direction.U))
                            {
                                Target = NextWaypoint;
                            }
                        }
                    }
                    break;
            }
        }

        return Target;
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////Check for valid direction
    bool CanGo(wpt waypoint, wpt.direction direction)
    {
        foreach (var dir in waypoint.ValidDiriections)
        { if (dir == direction) return true; }
        return false;
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////If player reaches the destination or not
    bool CheckForDestination()
    {
        switch (direction)
        {
            case wpt.direction.R: if (transform.position.x >= CurrentWaypoint.transform.position.x) { return true; } else return false; //>=
            case wpt.direction.L: if (transform.position.x <= CurrentWaypoint.transform.position.x) { return true; } else return false; //<=
            case wpt.direction.U: if (transform.position.y >= CurrentWaypoint.transform.position.y) { return true; } else return false; //>=
            case wpt.direction.D: if (transform.position.y <= CurrentWaypoint.transform.position.y) { return true; } else return false; //<=
        }
        return false;
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////Check if player is near the waypoint for turning
    bool NearNextWpt()
    {
        var CurrentPosition = CurrentWaypoint.transform.position;
        var PlayerPosition = transform.position;
        switch (direction)
        {
            case wpt.direction.L:
            case wpt.direction.R:
                return (Mathf.Abs(CurrentPosition.x - PlayerPosition.x) <= TurnForgiveness);
            case wpt.direction.U:
            case wpt.direction.D:
                return (Mathf.Abs(CurrentPosition.y - PlayerPosition.y) <= TurnForgiveness);
        }
        return false;
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////Finding the Distance from one vector to another
    public static double distance(Vector3 CurrentVector, Vector3 FarestVector, wpt.direction dir)
    {
        switch (dir)
        {
            case wpt.direction.L: return Mathf.Abs(FarestVector.x - CurrentVector.x);
            case wpt.direction.R: return Mathf.Abs(FarestVector.x - CurrentVector.x);
            case wpt.direction.U: return Mathf.Abs(FarestVector.y - CurrentVector.y);
            case wpt.direction.D: return Mathf.Abs(FarestVector.y - CurrentVector.y);
        }
        return 0.0;
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////Randomize directions
    int random()
    {
        int r = Random.Range(0,4);
        int pre = r;
        while (r == pre)
        {
            r = Random.Range(0, 4);
            break;
        }
        return r;
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////check spawn
    bool atspawn()
    {
        if (this.transform.position == spawn.transform.position)
        {
            return true;
        }
        else { return false; }
    }
}
