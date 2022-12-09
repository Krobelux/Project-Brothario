#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;


public class MovingPlatformControllerEditor : Editor
{
#if UNITY_EDITOR
[CustomEditor(typeof(MovingPlatformController))]
    public override void OnInspectorGUI()
    {
        #if UNITY_EDITOR
        //base.OnInspectorGUI();
        MovingPlatformController controller = (MovingPlatformController)target;
        controller.moveSpeed = EditorGUILayout.FloatField("Speed: ", controller.moveSpeed);     //Adds a Float Field in the editor that modifies MovingPlatformController.moveSpeed value
        
        controller.waypointobj = (GameObject)EditorGUILayout.ObjectField("Waypoint Object", controller.waypointobj, typeof(GameObject), false);
        if (GUILayout.Button("Add Waypoint"))
        {
            controller.AddNewWayPoint();
            //   Debug.Log("Way point added");
        }
        if (GUILayout.Button("Clear Waypoint"))
        {
            controller.clearwaypoints();
            //   Debug.Log("Way point added");
        }

        if(controller.waypoint != null && controller.waypoint.Count != 0)
        {
            #if UNITY_EDITOR
            for (int i = 0; i < controller.waypoint.Count; i++)
            {
                #if UNITY_EDITOR
                EditorGUILayout.BeginHorizontal();
                controller.waypoint[i].gameObject.name = EditorGUILayout.TextField(controller.waypoint[i].gameObject.name);     //Adds a Text Field in the editor that changes the waypoint Object name
                controller.waypoint[i].position = EditorGUILayout.Vector2Field("" + i, controller.waypoint[i].position);

                

                if(GUILayout.Button("Delete"))
                {
                    //remove waypoint
                    controller.RemoveWayPoints(i);
                }

                EditorGUILayout.EndHorizontal();
                #endif
            }
            #endif
        }
        #endif


    }
#endif
}
