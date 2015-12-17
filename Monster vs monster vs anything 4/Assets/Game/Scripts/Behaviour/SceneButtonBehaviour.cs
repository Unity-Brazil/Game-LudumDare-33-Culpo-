using UnityEngine;
using System.Collections;

public class SceneButtonBehaviour : MonoBehaviour 
{
	public GAMESCENE scene;

	public void LoadScene()
	{
		Game_Director.Instance.loadScene(scene);
	}

	public void LoadSceneWithFade()
	{
		Game_Director.Instance.loadSceneWithFade(scene);
	}

}
