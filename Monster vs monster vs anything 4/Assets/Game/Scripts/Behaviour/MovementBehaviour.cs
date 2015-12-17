using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody2D))]
public class MovementBehaviour : MonoBehaviour 
{
    public Rigidbody2D rigidbody2d { set; get; }
    public Vector2 maxSpeedMove { set; get; }
    public float angleSpeed { set; get; }

    private Transform myTransform;

    void Start()
    {
        this.myTransform = GetComponent<Transform>();
        this.rigidbody2d = GetComponent<Rigidbody2D>();
    }

    public void move( Vector2 _speed)
	{
		/*
        if (_speed.x > this.maxSpeedMove.x)
            _speed.x = this.maxSpeedMove.x;
        if (_speed.y > this.maxSpeedMove.y)
            _speed.y = this.maxSpeedMove.y;
        if (_speed.x < -this.maxSpeedMove.x)
            _speed.x = -this.maxSpeedMove.x;
        if (_speed.y < -this.maxSpeedMove.y)
            _speed.y = -this.maxSpeedMove.y;
*/
        this.rigidbody2d.velocity = new Vector2(_speed.x, _speed.y);
    }

    public void rotate(float _rotate)
    {
        this.rigidbody2d.MoveRotation( this.rigidbody2d.rotation * _rotate * Time.deltaTime);
    }

    public void moveImpulse( Vector2 _speed)
    {
        //Debug.Log(_speed);
        this.rigidbody2d.AddForce(_speed, ForceMode2D.Impulse);
    }

    public void stop()
    {
        this.rigidbody2d.velocity = Vector2.zero;
    }
}
