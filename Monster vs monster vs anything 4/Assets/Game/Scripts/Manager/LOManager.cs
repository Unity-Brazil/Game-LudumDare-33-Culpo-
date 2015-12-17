using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LOManager : MonoBehaviour {

    private Dictionary<string, List<GameObject>> LOManagerDictionary;   // Dicionario onde contem as lista que for preciso para criar os objetos
    public static LOManager Instance;                                   // Referencia do Objeto
	
    /// <summary>
    /// Inicializa o dicionario
    /// </summary>
    void Awake()
    {
		Instance = this;
        this.LOManagerDictionary = new Dictionary<string, List<GameObject>>();
    }

    /// <summary>
    /// Criar uma lista de GameObjects
    /// </summary>
    /// <param name="_key"> Nome da chave que vai servir de referencia para utilizar a lista </param>
    public void LO_create( string _key )
    {
        if (LOManagerDictionary.ContainsKey(_key))
            Debug.Log("Já existe está key salva dentro do dicionario.  " + _key);
        else
            LOManagerDictionary.Add(_key, new List<GameObject>());
    }

    /// <summary>
    /// Metodo responsavel por deletar uma lista de objetos dentro do dicionario
    /// </summary>
    /// <param name="_key"> Referencia da lista a ser deletada </param>
    public void LO_delete(string _key)
    {
        LOManagerDictionary.Remove(_key);
    }

    /// <summary>
    /// Metodo responsavel por limpar todo dicionario
    /// Usado quando uma missão chega ao final
    /// </summary>
    public void LO_clear()
    {
        LOManagerDictionary.Clear();
    }

    /// <summary>
    /// Metodo responsavel para retornar uma lista de GameObjects
    /// </summary>
    /// <param name="key"> Nome da chave para buscar a lista  </param>
    /// <returns></returns>
    public List<GameObject> LO_load (string _key)
    {
        if (!LOManagerDictionary.ContainsKey(_key))
        {
            Debug.Log("Não existe Esta chave :" + _key + " No gerenciador de Objetos");
            return null;
        }
        else
        {
            List<GameObject> _newList;
            LOManagerDictionary.TryGetValue(_key, out _newList);
            return _newList;
        }
    }

    /// <summary>
    /// Metodo responsavel para criar e adicionar mais um objeto a uma das lista disponivel
    /// </summary>
    /// <param name="_object"> Objeto a ser adicionado dentro da lista </param>
    /// <param name="key"> Referencia onde vai ser adicionar este objeto </param>
    public void LO_add(string _key, GameObject _object)
    {
        if (!LOManagerDictionary.ContainsKey(_key))
        {
            Debug.Log("Não existe Esta chave :" + _key + " No gerenciador de Objetos, ele vai ser criado para que os objetos seja adicionado");
            LO_create(_key);
            LO_add(_key, _object);
        }
        else
        {
            GameObject __object = Instantiate(_object) as GameObject;
            __object.SetActive(false);
            LOManagerDictionary[_key].Add(__object);
        }
    }

    /// <summary>
    /// Metodo responsavel para adicionar uma lista de objetos já existentes dentro da lista
    /// </summary>
    /// <param name="_object"> Objeto a ser adicionado dentro da lista </param>
    /// <param name="key"> Referencia onde vai ser adicionar este objeto </param>
    public void LO_addObjectCreated(string _key, GameObject _object)
    {
        if (!LOManagerDictionary.ContainsKey(_key))
        {
            Debug.Log("Não existe Esta chave :" + _key + " No gerenciador de Objetos, ele vai ser criado para que os objetos seja adicionado");
            LO_create(_key);
            LO_addObjectCreated(_key, _object);
        }
        else
        {
            _object.SetActive(false);
            LOManagerDictionary[_key].Add(_object);
        }
    }

    /// <summary>
    /// Metodo responsavel para adicionar uma lista de objetos já existentes dentro da lista
    /// </summary>
    /// <param name="_object"> lista de objetos a ser adicionado dentro da lista </param>
    /// <param name="key"> Referencia onde vai ser adicionar este objeto </param>
    public void LO_addObjectCreated(string _key, List<GameObject> _object)
    {
        if (!LOManagerDictionary.ContainsKey(_key))
        {
            Debug.Log("Não existe Esta chave :" + _key + " No gerenciador de Objetos, ele vai ser criado para que os objetos seja adicionado");
            LO_create(_key);
            LO_addObjectCreated(_key, _object);
        }
        else
        {
            for (int i = 0; i < _object.Count; i++)
            {
                _object[i].SetActive(false);
                LOManagerDictionary[_key].Add(_object[i]);
            }
        }
    }

    /// <summary>
    /// Metodo responsavel para inserir uma lista de GameObject dentro do dicionario
    /// </summary>
    /// <param name="_object"> O prefab do objeto que vai ser clonado para adicionar na lista  </param>
    /// <param name="_amount"> Quantidade de objetos daquele tipo que vai ser criado</param>
    /// <param name="_key"> Referencia onde vai ser criado a lista dentro do dicionario </param>

    public void LO_createList(string _key, GameObject _object, int _amount)
    {
        if (!LOManagerDictionary.ContainsKey(_key))
        {
            Debug.Log("Não existe Esta chave :" + _key + " No gerenciador de Objetos, ele vai ser criado para que os objetos seja adicionado");
            LO_create(_key);
            LO_createList(_key, _object, _amount);
        }
        else
        {
            for (int i = 0; i < _amount; i++)
            {
                GameObject __object = Instantiate(_object) as GameObject;
                __object.SetActive(false);
                LOManagerDictionary[_key].Add(__object);
            }
        }
    }

    /// <summary>
    /// Responsável por retornar um objeto que está disponivel para ser usado dentro da lista de objeto
    /// </summary>
    /// <param name="_key"> Referencia em qual lista está armazenado os objetos que podem estar disponiveis </param>
    /// <returns></returns>
    public GameObject LO_GetObjectDictionary(string _key)
    {

        if (!LOManagerDictionary.ContainsKey(_key))
        {
            Debug.Log("Não existe Esta chave :" + _key + " No gerenciador de Objetos");
            return null;
        }
        else
        {
            List<GameObject> _newList;
            LOManagerDictionary.TryGetValue(_key, out _newList);
            //_newList.shuffleList();
            for (int i = 0; i < _newList.Count; i++ )
            {
                if(!_newList[i].activeSelf)
                {
                    return _newList[i];
                }
            }
            Debug.Log("Nenhum Objeto encontrado dentro da lista da key: " + _key);
            return null;
        }
    }

    /// <summary>
    /// Responsavel por disabilitar todos os objetos da lista que é comparado a chave
    /// </summary>
    /// <param name="_key"></param>
    public void LO_DisableList(string _key)
    {
        if (!LOManagerDictionary.ContainsKey(_key))
        {
            Debug.Log("Não existe Esta chave :" + _key + " No gerenciador de Objetos");
        }
        else
        {
            List<GameObject> _newList;
            LOManagerDictionary.TryGetValue(_key, out _newList);
            for (int i = 0; i < _newList.Count; i++)
            {
                _newList[i].SetActive(false);
            }
        }
    }
}
