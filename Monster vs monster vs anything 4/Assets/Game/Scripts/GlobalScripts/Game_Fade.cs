using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Game_Fade : MonoBehaviour 
{
    
    private static Game_Fade instace;
    private Animator fadeAnim;
    private GAMESCENE scene;

    void Awake()
    {
        instace = this;
		this.fadeAnim = GetComponent<Animator>();
    }

    public static Game_Fade Instance
    {
        get 
        {
            if (instace == null)
                instace = new Game_Fade();
            return instace;
        }
    }

    public void playFadeIn(GAMESCENE _scene)
    {
        if (Game_Actions.Instance.event_fadeIn != null) Game_Actions.Instance.event_fadeIn();
        this.fadeAnim.SetTrigger("FadeIn");
        this.scene = _scene;
    }

	public void playFadeOut()
	{
		this.fadeAnim.SetTrigger("FadeOut");
	}

    public void loadingScene()
    {
        Game_Director.Instance.loadScene(this.scene);
		this.scene = GAMESCENE.NULL;
    }

    public void eventFadeOut()
    {
        if ( Game_Actions.Instance.event_fadeOutOver != null ) Game_Actions.Instance.event_fadeOutOver();
    }
}
