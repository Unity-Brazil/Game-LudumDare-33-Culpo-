using UnityEngine;
using System.Collections;

public class LogoController : MonoBehaviour 
{
	public float time;
	public GAMESCENE scene;

    void Start()
    {
		Game_Director.Instance.startWaitTime(this.time, this.gameObject.GetInstanceID());
    }

	void OnEnable()
	{
		print(Game_Actions.Instance);
		Game_Actions.Instance.event_waitTime += HandleEventTime;
		print(Game_Actions.Instance.event_waitTime);
	}

	void OnDisable()
	{
		Game_Actions.Instance.event_waitTime -= HandleEventTime;
	}

	void HandleEventTime(int IdObject)
	{
		if(this.gameObject.GetInstanceID().Equals(IdObject))
			Game_Fade.Instance.playFadeIn(scene);
	}
}
