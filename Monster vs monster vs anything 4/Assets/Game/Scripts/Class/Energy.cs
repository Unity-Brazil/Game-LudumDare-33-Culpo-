using UnityEngine;
using System.Collections;

[System.Serializable]
public class Energy
{
	public float energy {set; get;}
	public float currentEnergy;

	public Energy()
	{
		this.energy = 100;
		this.currentEnergy = 100;
	}

	public void addEnergy(float _energy)
	{
		this.currentEnergy += _energy;
		if(this.currentEnergy >= this.energy)
			this.currentEnergy = this.energy;
	}

	public void removeEnergy(float _energy)
	{
		this.currentEnergy -= _energy;
		if(this.currentEnergy <= 0)
			this.currentEnergy = 0;
	}
}
