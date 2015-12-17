using UnityEngine;
using System.Collections;

public class CFallow2D : MonoBehaviour {

	[SerializeField] private Transform target;
	[SerializeField] private float delay;
	[SerializeField] private float cameraHigh;

	void Start()
	{
		this.init();
		this.target = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void FixedUpdate()
	{
        if(target != null)
		    transform.position = Vector3.Slerp (new Vector3(transform.position.x, transform.position.y, -10), 
		                                        new Vector3(target.position.x, target.position.y + this.cameraHigh, -10), this.delay * Time.deltaTime);
	}

	#region set, Get

	private void init ()
	{
		//this.delay = 4f;
		//this.cameraHigh = 1.2f;
	}

	public CFallow2D (float delay, float cameraHigh)
	{
		this.Delay = delay;
		this.CameraHigh = cameraHigh;
	}
	

	public Transform Target {
		get {
			return this.target;
		}
		set {
			target = value;
		}
	}

	public float Delay {
		get {
			return this.delay;
		}
		set {
			delay = value;
		}
	}

	public float CameraHigh {
		get {
			return this.cameraHigh;
		}
		set {
			cameraHigh = value;
		}
	}
	#endregion
}
