using UnityEngine;
using System.Collections;

public class DamageBehaviour : MonoBehaviour 
{
	public int damage;
	public int currentDamage {set; get;}

	void Start()
	{
		this.currentDamage = damage;
	}

	public void addDamage(int Damage)
	{
		this.currentDamage += Damage;
	}

	public void removeDamage(int Damage)
	{
		this.currentDamage -= Damage;
	}

	public void addDamage(int Damage, float _time)
	{
		this.currentDamage += Damage;
		StartCoroutine(time(_time));
	}
	
	public void removeDamage(int Damage, float _time)
	{
		this.currentDamage -= Damage;
		StartCoroutine(time(_time));
	}

	void restartDamage()
	{
		this.currentDamage = this.damage;
	}

	IEnumerator time(float time)
	{
		yield return new WaitForSeconds(time);
		this.restartDamage();
	}
}
