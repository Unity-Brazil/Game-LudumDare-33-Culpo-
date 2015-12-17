using UnityEngine;
using System.Collections;

public class DistanceTest : MonoBehaviour
{

    private float velocity =  3f;

    void Update()
    {
        transform.Translate(new Vector3(this.velocity * Time.deltaTime, 0, 0));
    }

    void OnEnterAllowableDistance(Transform other)
    {
        print("Este objeto entrou no meu raio" + " :: O nome é " + other.name);
        SpriteRenderer _sprite = other.GetComponent<SpriteRenderer>();
        _sprite.color = _sprite.color == Color.green ? Color.blue : Color.green;
        this.velocity *= -1;
    }
    void OnExitAllowableDistance(Transform other)
    {
        print("Este objeto saiu do meu raio" + " :: O nome é " + other.name);
    }
    void OnStayAllowableDistance(Transform other)
    {
        print("Este objeto está dentro do meu raio" + " :: O nome é " + other.name);
    }
}

