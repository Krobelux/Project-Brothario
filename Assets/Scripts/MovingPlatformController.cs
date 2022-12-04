using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public List<Transform> waypoint;
    private int currentTargetIndex;
    public GameObject waypointobj;
    public GameObject platform;
    private PlayerController pc;
    
    


    private void Awake()
    {
        waypoint = new List<Transform>();
        foreach(Transform t in transform.parent.GetChild(0))
        {
            waypoint.Add(t);
        }
        if (waypoint.Count > 0)
        {
            transform.position = waypoint[0].position;
        }

        
    }

    void Start() 
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (waypoint.Count > 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, waypoint[currentTargetIndex].position , Time.deltaTime * moveSpeed);
            if (Vector2.Distance(transform.position, waypoint[currentTargetIndex].position) < 0.01f)
            {
                // if your close to target1 then move towards target2
                // modulus --> Gets the remainder of the division
                // Count waypoint
                // currentTargetIndex
                // currentTargetIndex++
                currentTargetIndex = (currentTargetIndex + 1) % waypoint.Count;
         
            }
        }

    }
    
    public void AddNewWayPoint()
    {
        GameObject gObj = Instantiate(waypointobj, Vector2.zero, Quaternion.identity);
        gObj.transform.SetParent(transform.parent.GetChild(0));
        gObj.name = "WayPoint" + waypoint.Count;
        waypoint.Add(gObj.transform);
    }

    public void RemoveWayPoints(int index)
    {
        waypoint.RemoveAt(index);
        DestroyImmediate(transform.parent.GetChild(0).GetChild(index).gameObject);
    }
    public void clearwaypoints()
    {
        for (int i = 0; i < waypoint.Count; i++){
            DestroyImmediate(waypoint[i].gameObject);
        }
        waypoint.Clear();
    }


    public void OnCollisionEnter2D(Collision2D collision) 
    {
        collision.transform.SetParent(transform);

    }


    private void OnCollisionExit2D(Collision2D collision) 
    {
        collision.transform.SetParent(null);
        
    }

}
