using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MovementBehaviour))]
public class PlayerBehaviour : MonoBehaviour 
{
    public InputController inputController { set; get; }
    public MovementBehaviour movementBehaviour { set; get; }
	public AnimatorBehaviour animatorBehaviour {set; get;}
	public LifeBehaviour lifeBehaviour {set; get;}
	public Energy energyBehaviour;

	public ENEMY typeEnemy;
    public float speedMove;
    public float speedImpulse;
	public float timeImpulseMove;
	public int life;

	private SIDE playerSide;
    private Transform myTransform;

	private bool isRunning = true;

	void Awake () 
    {
        this.inputController = GameObject.FindGameObjectWithTag("Input").GetComponent<InputController>();
        this.myTransform = GetComponent<Transform>();
		this.lifeBehaviour = GetComponent<LifeBehaviour>();
        this.movementBehaviour = GetComponent<MovementBehaviour>();
		this.animatorBehaviour = GetComponent<AnimatorBehaviour>();


		this.energyBehaviour = new Energy();
		this.lifeBehaviour.life = life; this.lifeBehaviour.currentLife = life;
	}
	

    void Start()
    {
        this.movementBehaviour.maxSpeedMove = new Vector2(100f, 100f);
    }

    /// <summary>
    /// 
    /// </summary>
	void OnEnable()
    {
        inputController.ev_leftControl_left += arrowLeft;
        inputController.ev_leftControl_right += arrowRight;
        inputController.ev_leftControl_up += arrowUp;
        inputController.ev_leftControl_down += arrowDown;
        inputController.ev_last_frame += stopMove;

        inputController.ev_rightControl_left += impulse;
		inputController.ev_rightControl_right += HandleAttack;
    }

    /// <summary>
    /// 
    /// </summary>
    void OnDisable()
    {
        inputController.ev_leftControl_left -= arrowLeft;
        inputController.ev_leftControl_right -= arrowRight;
        inputController.ev_leftControl_up -= arrowUp;
        inputController.ev_leftControl_down -= arrowDown;
        inputController.ev_last_frame -= stopMove;

        inputController.ev_rightControl_left -= impulse;
		inputController.ev_rightControl_right -= HandleAttack;
    }

	void Update()
	{
		this.energyBehaviour.addEnergy(12 * Time.deltaTime);
        HUDPlayerManager.Instance.addEnergy();
	}

	void HandleAttack()
	{
        if (this.isRunning && this.energyBehaviour.currentEnergy >= 50)
        {
            MusicManager.Instance.playOnShot(1);
            this.animatorBehaviour.setAnimation(ANIMATOR.HIT);
        }
	}

	/// <summary>
	/// Impulse this instance.
	/// </summary>
    void impulse()
    {
		if(this.isRunning && this.energyBehaviour.currentEnergy >= 30)
		{
	        this.movementBehaviour.stop();
			if(this.playerSide == SIDE.LEFT)
				this.movementBehaviour.moveImpulse(new Vector2(-(this.speedImpulse), this.movementBehaviour.rigidbody2d.velocity.y));
			else if(this.playerSide == SIDE.RIGHT)
				this.movementBehaviour.moveImpulse(new Vector2((this.speedImpulse), this.movementBehaviour.rigidbody2d.velocity.y));
			else if(this.playerSide == SIDE.DOWN)
				this.movementBehaviour.moveImpulse(new Vector2(this.movementBehaviour.rigidbody2d.velocity.x, -(this.speedImpulse)));
			else if(this.playerSide == SIDE.UP)
				this.movementBehaviour.moveImpulse(new Vector2(this.movementBehaviour.rigidbody2d.velocity.x, (this.speedImpulse)));
			/*
			else if(this.playerSide == SIDE.LEFTUP)
				this.movementBehaviour.moveImpulse(new Vector2((this.speedImpulse), (this.speedImpulse)));
			else if(this.playerSide == SIDE.LEFTDOWN)
				this.movementBehaviour.moveImpulse(new Vector2((this.speedImpulse), (-this.speedImpulse)));
			else if(this.playerSide == SIDE.RIGHTUP)
				this.movementBehaviour.moveImpulse(new Vector2((this.speedImpulse), (this.speedImpulse)));
			else if(this.playerSide == SIDE.RIGHTDOWN)
				this.movementBehaviour.moveImpulse(new Vector2((this.speedImpulse), (-this.speedImpulse)));*/


            MusicManager.Instance.playOnShot(2);
			this.energyBehaviour.removeEnergy(30);
            HUDPlayerManager.Instance.removeEnergy();
			StartCoroutine(waitTimeRunning());

		}

    }
    void stopMove()
    {
		if(this.isRunning)
        	this.movementBehaviour.stop();
    }
    void arrowLeft() 
    {
		if(this.isRunning)
		{
			Flip(SIDE.LEFT);
			this.playerSide = SIDE.LEFT;
	        this.movementBehaviour.move(new Vector2(-this.speedMove, this.movementBehaviour.rigidbody2d.velocity.y));
		}
    }
    void arrowRight() 
    {
		if(this.isRunning)
		{
			Flip(SIDE.RIGHT);
			this.playerSide = SIDE.RIGHT;
	        this.movementBehaviour.move(new Vector2(this.speedMove, this.movementBehaviour.rigidbody2d.velocity.y));
		}
    }

    void arrowDown()
    {
		if(this.isRunning)
		{
			this.playerSide = SIDE.DOWN;
	        this.movementBehaviour.move(new Vector2(this.movementBehaviour.rigidbody2d.velocity.x, -this.speedMove));
		}
    }

    void arrowUp() 
    {
		if(this.isRunning)
		{
			this.playerSide = SIDE.UP;
	        this.movementBehaviour.move(new Vector2(this.movementBehaviour.rigidbody2d.velocity.x, this.speedMove));
		}
        //this.myTransform.Translate(Vector3.right * this.speedMove * Time.deltaTime);
    }

	void Flip(SIDE side)
	{
		if(side.Equals(SIDE.LEFT))
            this.transform.localScale = new Vector3(this.transform.localScale.x < 0 ? this.transform.localScale.x : -this.transform.localScale.x, 
                                                    this.transform.localScale.y, 
                                                    this.transform.localScale.z);
		else
            this.transform.localScale = new Vector3(this.transform.localScale.x > 0 ? this.transform.localScale.x : -this.transform.localScale.x, 
                                                    this.transform.localScale.y, 
                                                    this.transform.localScale.z);
	}

	IEnumerator waitTimeRunning()
	{
		this.isRunning = false;
		yield return new WaitForSeconds(this.timeImpulseMove);
		this.movementBehaviour.stop();
		this.isRunning = true;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.transform.parent != null)
		{
			if(other.transform.parent.tag.Equals("Enemy") || other.transform.parent.tag.Equals("OtherEnemy"))
			{
				this.lifeBehaviour.removeLife(other.GetComponent<DamageBehaviour>().currentDamage);
                MusicManager.Instance.playOnShot(0);
                HUDPlayerManager.Instance.removeLife();
			}
		}
	}
}
