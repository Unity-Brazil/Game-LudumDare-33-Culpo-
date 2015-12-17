using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDPlayerManager : MonoBehaviour 
{
    public RectTransform life;
    public RectTransform energy;

    public float initialEnergy;
    public float initialLife;

    public PlayerBehaviour player;

    public static HUDPlayerManager Instance;


    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        this.initialEnergy = this.energy.sizeDelta.x;
        this.initialLife = this.life.sizeDelta.x;
    }

    public void addLife()
    {
        float sizeX = (this.initialLife * this.player.lifeBehaviour.currentLife) /this.player.lifeBehaviour.life; 
        this.life.sizeDelta = new Vector2( sizeX, this.life.sizeDelta.y );
    }

    public void removeLife()
    {
        float sizeX = (this.initialLife * this.player.lifeBehaviour.currentLife) / this.player.lifeBehaviour.life;
        this.life.sizeDelta = new Vector2(sizeX, this.life.sizeDelta.y);
    }

    public void addEnergy()
    {
        float sizeX = (this.initialLife * this.player.energyBehaviour.currentEnergy) / this.player.energyBehaviour.energy;
        this.energy.sizeDelta = new Vector2(sizeX, this.energy.sizeDelta.y);
    }

    public void removeEnergy()
    {
        float sizeX = (this.initialLife * this.player.energyBehaviour.currentEnergy) / this.player.energyBehaviour.energy;
        this.energy.sizeDelta = new Vector2(sizeX, this.energy.sizeDelta.y);
    }

}
