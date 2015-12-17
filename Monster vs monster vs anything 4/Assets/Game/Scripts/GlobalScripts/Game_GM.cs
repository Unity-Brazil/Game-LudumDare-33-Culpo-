using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//Global Methods
public static class Game_GM {



//----------------------------------------[ ORDEM ALEATORIA ]-------------------------------------------------------------------------

	//Embaralhar ordem da lista;
	//--> finalList = shuffleList(
	public static List<T> shuffleList<T>( this List<T> originalList ){
		List<T> copyList = new List<T>(originalList);
		originalList.Clear();
		int totalCount = copyList.Count;
		for( int i = 0; i < totalCount; i++ ){
			int indice = UnityEngine.Random.Range(0, copyList.Count);
			originalList.Add( copyList[indice] );
			copyList.RemoveAt( indice );
		}
		return originalList;
	}
	public static T [] shuffleList<T>( this T [] originalList ){
		List<T> copyList = new List<T>();
		foreach (T _obj in originalList) { copyList.Add( _obj ); }
		int totalCount = copyList.Count;
		for( int i = 0; i < totalCount; i++ ){
			int indice = UnityEngine.Random.Range(0, copyList.Count);
			originalList[i] = copyList[indice];
			copyList.RemoveAt( indice );
		}
		return originalList;
	}
}
