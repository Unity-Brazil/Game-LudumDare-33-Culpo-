using UnityEngine;
using System.Collections;

public class AllowableDistanceBehaviour : MonoBehaviour 
{
    private GameObject myGameObject;

    void Awake()
    {
        this.myGameObject = GetComponent<Transform>().gameObject;
    }

    void OnEnable()
    {
        AllowableDistanceManager.instance.addAllowableDistanceObjects(this.myGameObject);
    }

    void OnDestroy()
    {
        AllowableDistanceManager.instance.removeAllowableDistanceObjects(this.myGameObject);
    }
}
