using UnityEngine;
using System.Collections;

//Implementation types delegates

public class Game_Actions : MonoBehaviour
{

    public delegate T GameActions<T>();
    public delegate T GameActionsParameters<T, U>(U u);
	public delegate void GameActionsVoid();
    public delegate void GameActionsVoidParameters<U>(U u);

    private static Game_Actions instance;

    #region Events 

    public GameActions<bool> event_fadeIn;
    public GameActions<bool> event_fadeOutOver;

	public GameActionsVoidParameters<int> startWave;
	public GameActionsVoid stopWave;

    public GameActionsVoid over;

    public GameActionsVoidParameters<int> event_waitTime;

    #endregion

	void Awake()
	{
		instance = this;
	}

    /// <summary>
    /// Rigdols
    /// </summary>
    public static Game_Actions Instance
    {
        get 
        {
            return instance;
        }
    }
}
