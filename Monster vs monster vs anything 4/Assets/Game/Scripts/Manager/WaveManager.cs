using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour 
{
	public const int STARTNUMBERENEMYWAVE = 1;

	public int amountWavePoints;

	public int numberWave {set; get;}
	public int difficultyWave {set; get;}

	public int numberEnemys{set; get;}
	public int numberEnemysForWaveBeheaviour {set; get;}
	public int numberEnemysAlive {set; get;}

	private List<GameObject> Enemys = new List<GameObject>();
  	private bool isWave = true;

	void Start()
	{
		this.numberWave = 0;
		this.numberEnemysAlive = 0;
	}

	void Update()
	{
        if(this.isWave)
		{
			if(this.numberEnemysAlive <= 0)
			{
				this.Enemys.Clear();
				this.nextWave();
			}
		}
	}

	public void nextWave()
	{
		this.numberWave++;
		this.numberEnemys = STARTNUMBERENEMYWAVE + (this.numberWave - 1 * 2);
		this.numberEnemysForWaveBeheaviour = this.numberEnemys;
		if(Game_Actions.Instance.startWave != null) Game_Actions.Instance.startWave(this.numberEnemysForWaveBeheaviour);
	}

	public void addEnemy(GameObject enemy)
	{
		this.numberEnemysAlive++;
		this.Enemys.Add(enemy);
	}

	public void removeEnemy(GameObject enemy)
	{
		this.numberEnemysAlive--;
		this.Enemys.Remove(enemy);
	}

}
