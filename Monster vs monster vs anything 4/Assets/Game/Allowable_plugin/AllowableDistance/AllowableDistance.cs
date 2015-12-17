using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class AllowableDistanceStruct
{
    public GameObject obj { set; get; }
    public bool isEnter { set; get; }
    
    public AllowableDistanceStruct( GameObject obj, bool isEnter )
    {
        this.obj = obj;
        this.isEnter = isEnter;
    }
}

public class AllowableDistance: MonoBehaviour
{
	[HideInInspector]
	public float allowableDistance = 1.0f;

	private float currentDistance;
	
	private Transform myTransform;

	private AllowableDistance Distance;
	
    public List<AllowableDistanceStruct> objectStruct = new List<AllowableDistanceStruct>();

	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake()
	{
		Distance = GetComponent<AllowableDistance>();
	}

    /// <summary>
    /// Start this instance.
    /// </summary>
    void Start()
    {
        this.myTransform = this.transform;

		List<GameObject> _objects = AllowableDistanceManager.instance.allowableDistanceObjects;
		int count = _objects.Count;

		for(int i = 0; i < count; i++ )
		{
			AllowableDistanceStruct _struct = new AllowableDistanceStruct( _objects[i], false );

			if(!reserchObject(_objects[i]))
				this.objectStruct.Add(_struct);
		}
    }

    /// <summary>
    /// On Enable
    /// </summary>
    void OnEnable()
    {
        AllowableDistanceManager.instance.addAllowable += add;
        AllowableDistanceManager.instance.removeAllowable += remove;
    }

    /// <summary>
    /// On Disable
    /// </summary>
    void OnDestroy()
    {
        AllowableDistanceManager.instance.addAllowable -= add;
        AllowableDistanceManager.instance.removeAllowable -= remove;
    }

    /// <summary>
    /// 
    /// </summary>
    void add(GameObject _obj)
    {
        AllowableDistanceStruct _struct = new AllowableDistanceStruct( _obj, false );
		if(!reserchObject(_obj))
        	this.objectStruct.Add(_struct);
    }

	bool reserchObject(GameObject obj)
	{
		for(int i = 0 ; i < objectStruct.Count; i++)
		{
			if(objectStruct[i].obj.GetInstanceID() == obj.GetInstanceID())
				return true;
		}
		return false;
	}

    /// <summary>
    /// 
    /// </summary>
    void remove(GameObject _obj)
    {
        foreach( AllowableDistanceStruct _struct in objectStruct )
        {
            if(_struct.obj.GetInstanceID() == _obj.GetInstanceID() )
            {
                objectStruct.Remove(_struct);
                break;
            }
        }
    }
	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update()
	{
        if (this.objectStruct != null) 
		{
            foreach (AllowableDistanceStruct target in objectStruct)
			{
                if (target.obj.GetInstanceID() != myTransform.gameObject.GetInstanceID())
                {
	                    this.currentDistance = Vector3.Distance(target.obj.transform.position, this.myTransform.position);

	                    if (!target.isEnter)
	                    {
	                        if (this.currentDistance <= Distance.allowableDistance)
	                        {
	                            SendMessage("OnEnterAllowableDistance", target.obj.transform);

	                            target.isEnter = true;
	                        }
	                    }

	                    if (target.isEnter)
	                    {
	                        if (this.currentDistance <= Distance.allowableDistance)

	                            SendMessage("OnStayAllowableDistance", target.obj.transform);

	                        if (this.currentDistance > Distance.allowableDistance)
	                        {
	                            SendMessage("OnExitAllowableDistance", target.obj.transform);

	                            target.isEnter = false;
	                        }
	                    }
                }
			}
		}
	}

	/// <summary>
	/// Raises the enter allowable distance event.
	public virtual void OnEnterAllowableDistance(Transform other){
        // No implementation
    }
	/// <summary>
	/// Raises the exit allowable distance event.
    public virtual void OnExitAllowableDistance(Transform other)
    {
		// No implementation
	}

	/// <summary>
	/// Raises the stay allowable distance event.
    public virtual void OnStayAllowableDistance(Transform other)
    {
		// No implementation
	}
}
