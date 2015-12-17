using UnityEngine;
using System.Collections;

public class LifeBehaviour : MonoBehaviour 
{
    public int currentLife { set; get; }
	public int life {set; get;}

	public void addLife(int addLife)
	{
		this.currentLife += addLife;
		if(this.currentLife > this.life)
			this.currentLife = this.life;
	}

	public void removeLife(int removeLife)
	{
		this.currentLife -= removeLife;
		if(this.currentLife < 0)
			this.currentLife = 0;
		this.checkLife();
	}

	void checkLife()
	{
		if(this.currentLife <= 0)
		{
			if(gameObject.tag.Equals("Enemy") || gameObject.tag.Equals("OtherEnemy"))
			{
				GameObject.FindGameObjectWithTag("WaveManager").GetComponent<WaveManager>().removeEnemy(this.gameObject);
			}

            if (gameObject.tag.Equals("Player"))
            {

                if (Game_Actions.Instance.over != null) Game_Actions.Instance.over();
                Game_Director.Instance.state = GAMESTATE.OVER;
            }
            MusicManager.Instance.playOnShot(3);
			Destroy(this.gameObject);
		}
	}
}
