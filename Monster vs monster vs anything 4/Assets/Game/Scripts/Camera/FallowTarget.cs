using UnityEngine;
using System.Collections;

public class FallowTarget : MonoBehaviour {

	[SerializeField] private GameObject target;
	[SerializeField] private Vector2 min, max;

    public float y_size;


	void FixedUpdate () {

        if (target != null)
        {
            if (transform.position.x <= min.x && target.transform.position.x <= transform.position.x)

                transform.position = new Vector3(min.x, target.transform.position.y + y_size, transform.position.z);

            else if (transform.position.x >= max.x && target.transform.position.x >= transform.position.x)

                transform.position = new Vector3(max.x, target.transform.position.y + y_size, transform.position.z);
            else

                transform.position = new Vector3(target.transform.position.x, target.transform.position.y + y_size, transform.position.z);
        }
	}
}
