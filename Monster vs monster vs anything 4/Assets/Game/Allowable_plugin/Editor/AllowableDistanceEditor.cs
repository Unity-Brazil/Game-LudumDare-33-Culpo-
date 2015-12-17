using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(AllowableDistance))]
public class AllowableDistanceEditor: Editor
{
    AllowableDistance myDistance;

	public void OnEnable()
	{
		myDistance = (AllowableDistance)target;
	}

	public override void OnInspectorGUI ()
	{
        myDistance.allowableDistance = EditorGUILayout.FloatField("Allowable Distance", myDistance.allowableDistance);

		Resize();

		Repaint();
	}

	public void OnSceneGUI()
	{
		myDistance.allowableDistance = Handles.RadiusHandle( Quaternion.identity, myDistance.transform.position, myDistance.allowableDistance);

		Handles.color = Color.blue;
		Handles.CircleCap( 0, myDistance.transform.position, myDistance.transform.rotation, myDistance.allowableDistance);

		Resize();

		Repaint();
	}

	void Resize()
	{
		if(myDistance.allowableDistance <= 0.1f)
			myDistance.allowableDistance = 0.1f;
	}

	void RePaint()
	{
		if (GUI.changed)
			EditorUtility.SetDirty (target);
	}
}
