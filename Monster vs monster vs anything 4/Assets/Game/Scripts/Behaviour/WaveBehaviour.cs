using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveBehaviour : MonoBehaviour 
{
	private WaveManager waveManager;
	private int currentNumberEnemyWave;


    public SpriteRenderer portal;
	public GameObject[] Enemys;

	public int numberEnemyWave{set; get;}
	
	void Start () 
	{
		this.waveManager = GameObject.FindGameObjectWithTag("WaveManager").GetComponent<WaveManager>();
	}

	void OnEnable()
	{
		Game_Actions.Instance.startWave += goToWave;
	}

	void OnDisable()
	{
		Game_Actions.Instance.startWave -= goToWave;
	}

	void goToWave(int numberEnemys)
	{
		this.numberEnemyWave = numberEnemys;
		StartCoroutine(startWave());
	}
	/*
	void targetHit(EnemyBehaviour enemyBehaviour)
	{
		GameObject[] _enemy;
		int number = Random.Range(1, 100);

		if(number < 75)
		{
			if(enemyBehaviour.typeEnemy.Equals(ENEMY.OTHERS))
			{
				_enemy = GameObject.FindGameObjectsWithTag("Enemy");
				if(_enemy.Length == 0 || _enemy == null)
					enemyBehaviour.targetHit = GameObject.FindGameObjectWithTag("Player").transform;
			}
			else
			{
				_enemy = GameObject.FindGameObjectsWithTag("OtherEnemy");
				if(_enemy.Length == 0 || _enemy == null)
				{
					enemyBehaviour.targetHit = GameObject.FindGameObjectWithTag("Player").transform;
				}
			}

			List<GameObject> __enemy = new List<GameObject>(_enemy);

			//__enemy.shuffleList();
			enemyBehaviour.targetHit = __enemy[Random.Range(0, __enemy.Count)].transform;
		}
		else
			enemyBehaviour.targetHit = GameObject.FindGameObjectWithTag("Player").transform;
	}
*/
	IEnumerator startWave()
	{
        this.portal.enabled = true;
		while(this.numberEnemyWave >= 0)
		{
			GameObject _enemy = Instantiate(Enemys[Random.Range(0, Enemys.Length)], transform.position, Quaternion.identity) as GameObject;
			EnemyBehaviour enemyBehaviour = _enemy.GetComponent<EnemyBehaviour>();
			//targetHit(enemyBehaviour);
			this.waveManager.addEnemy(_enemy);
			this.numberEnemyWave--;
			yield return new WaitForSeconds(0.2f);
		}
        yield return new WaitForSeconds(2f);
        this.portal.enabled = false;
	}
}
