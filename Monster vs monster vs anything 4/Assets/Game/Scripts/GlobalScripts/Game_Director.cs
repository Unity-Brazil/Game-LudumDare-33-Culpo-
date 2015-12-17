using UnityEngine;
using System.Collections;

public class Game_Director : MonoBehaviour
{
    private static Game_Director instance;

    public GAMESCENE scene { set; get; }
    public GAMESTATE state { set; get; }

	void Awake()
	{
		instance = this;
	}

    /// <summary>
    /// Rigdols
    /// </summary>
    public static Game_Director Instance
    {
        get
        {
            if (instance == null)
                instance = new Game_Director();
            return instance;
        }
    }

    /// <summary>
    /// Load scene in Game
    /// </summary>
    /// <param name="_scene"></param>
    public void loadScene( GAMESCENE _scene )
    {
        switch(_scene)
        {
                //Implementation
            case GAMESCENE.LOGO :
                Application.LoadLevel("Logo");
                break;
            case GAMESCENE.GAME :
				Application.LoadLevel("Nivel");
				break;
            case GAMESCENE.MENU :
                Application.LoadLevel("Menu");
                break;
            default:
                Application.LoadLevel("Logo");
                break;
        }
    }

    public void loadSceneWithFade(GAMESCENE _scene)
    {
        Game_Fade.Instance.playFadeIn(_scene);
    }

    public void StartGame()
    {
        this.state = GAMESTATE.START;
    }

    public void QUIT()
    {
        Application.Quit();
    }

	public void startWaitTime(float time, int idObject)
	{
		StartCoroutine(waitTime(time, idObject));
	}

    IEnumerator waitTime( float time, int idObject )
    {
		yield return new WaitForSeconds(time);
		print(Game_Actions.Instance.event_waitTime);
        if(Game_Actions.Instance.event_waitTime != null)  Game_Actions.Instance.event_waitTime(idObject);
    }
}
