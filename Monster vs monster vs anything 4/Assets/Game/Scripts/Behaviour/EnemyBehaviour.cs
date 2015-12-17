using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyBehaviour : MonoBehaviour
{
	public Transform targetHit;
	public float distanceAttack;
	public float velocity;
	public int life;
	public ENEMY typeEnemy;

	private LifeBehaviour lifeBehaviour;
	private AnimatorBehaviour animatorBehaviour;

	bool isRunning = true;
	bool isAttack = false;
	bool isTimeAttack = true;
	bool isTarget = false;
	bool isEnter = false;

	float currentDistance;

	void Start()
	{
		this.animatorBehaviour = GetComponent<AnimatorBehaviour>();
		this.lifeBehaviour = GetComponent<LifeBehaviour>();

		//add content
		this.lifeBehaviour.life = life; 
		this.lifeBehaviour.currentLife = this.lifeBehaviour.life;
		this.velocity = Random.Range(1.8f, 2.2f);
	}

	/// <summary>
	/// On Enable
	/// </summary>
	void OnEnable()
	{
		AllowableDistanceManager.instance.removeAllowable += remove;
	}
	
	/// <summary>
	/// On Disable
	/// </summary>
	void OnDestroy()
	{
		AllowableDistanceManager.instance.removeAllowable -= remove;
	}

	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update () 
	{
		if(this.targetHit == null && !this.isTarget)
		{
			this.getTarget();
		}
		if(this.targetHit != null)
		{

			this.currentDistance = Vector3.Distance(targetHit.position, this.transform.position);

			if (!isEnter)
			{
				if (this.currentDistance <= distanceAttack)
				{
					this.isRunning = false;
					this.isAttack = true;
					
					isEnter = true;
				}
			}
			
			if (isEnter)
			{
				if (this.currentDistance > distanceAttack)
				{
					this.isRunning = true;
					this.isAttack = false;
					
					isEnter = false;
				}
			}

			if(this.isRunning)
			{
				transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), 
				                                         new Vector2(targetHit.position.x, targetHit.position.y), 
				                                         this.velocity * Time.deltaTime);

				if(transform.position.x <= targetHit.position.x)
					Flip(SIDE.RIGHT);
				else
					Flip(SIDE.LEFT);

			}

			if(!this.isRunning && this.isAttack & this.isTimeAttack)
			{
				this.animatorBehaviour.setAnimation(ANIMATOR.HIT);
				StartCoroutine(timetoAttack());
			}
		}
	}

	
	/// <summary>
	/// Remove the specified _obj.
	/// </summary>
	/// <param name="_obj">_obj.</param>
	void remove(GameObject _obj)
	{
		this.isRunning = true;
		this.isAttack = false;
	}


    void Flip(SIDE side)
    {
        if (side.Equals(SIDE.LEFT))
            this.transform.localScale = new Vector3(this.transform.localScale.x > 0 ? this.transform.localScale.x : -this.transform.localScale.x,
                                                    this.transform.localScale.y,
                                                    this.transform.localScale.z);
        else
            this.transform.localScale = new Vector3(this.transform.localScale.x < 0 ? this.transform.localScale.x : -this.transform.localScale.x,
                                                    this.transform.localScale.y,
                                                    this.transform.localScale.z);
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.transform.parent != null)
		{
			if(other.transform.parent.tag.Equals("Enemy") || other.transform.parent.tag.Equals("OtherEnemy"))
			{
				ENEMY _enemy = other.transform.parent.GetComponent<EnemyBehaviour>().typeEnemy;
				if(_enemy != typeEnemy)
				{
					this.lifeBehaviour.removeLife(other.GetComponent<DamageBehaviour>().currentDamage);
				}
			}

			if(other.transform.parent.tag.Equals("Player"))
			{
				this.lifeBehaviour.removeLife(other.GetComponent<DamageBehaviour>().currentDamage);
			}
		}
	}

	void getTarget()
	{
		GameObject[] _enemy;
		int number = Random.Range(1, 100);
		
		if(number < 80)
		{
			if(typeEnemy.Equals(ENEMY.OTHERS))
			{
				_enemy = GameObject.FindGameObjectsWithTag("Enemy");
				if(_enemy.Length == 0 || _enemy == null)
				{
					GameObject _obj = GameObject.FindGameObjectWithTag("Player");
					if(_obj != null)
						targetHit = _obj.transform;
				}
			}
			else
			{
				_enemy = GameObject.FindGameObjectsWithTag("OtherEnemy");
				if(_enemy.Length == 0 || _enemy == null)
				{
					GameObject _obj = GameObject.FindGameObjectWithTag("Player");
					if(_obj != null)
						targetHit = _obj.transform;
				}
			}
			
			List<GameObject> __enemy = new List<GameObject>(_enemy);
			
			//__enemy.shuffleList();
			if(__enemy.Count > 0)
				targetHit = __enemy[Random.Range(0, __enemy.Count)].transform;
		}
		else
		{
			GameObject _obj = GameObject.FindGameObjectWithTag("Player");
			if(_obj != null)
				targetHit = _obj.transform;
		}
	}

	IEnumerator timetoAttack()
	{
		this.isTimeAttack = false;
		yield return new WaitForSeconds(Random.Range(1, 3));
		this.isTimeAttack = true;
	}
}
