using UnityEngine;
using System.Collections;

public class AnimatorBehaviour : MonoBehaviour 
{
	private Animator animation;
	// Use this for initialization
	void Start () 
	{
		this.animation = GetComponent<Animator>();
	}

	public void setAnimation(ANIMATOR animator)
	{
		if(animator.Equals(ANIMATOR.HIT))
		{
			this.animation.SetTrigger("Hit");
		}
	}
}
