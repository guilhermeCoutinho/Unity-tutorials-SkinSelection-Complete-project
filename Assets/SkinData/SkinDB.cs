using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skin", menuName = "Skins/DB", order = 1)]
public class SkinDB : ScriptableObject {
    public Skin[] mySkins;
}

[System.Serializable]
[CreateAssetMenu(fileName = "Skin", menuName = "Skins/Skin", order = 0)]
public class Skin : ScriptableObject{
	static int _ID;
    public Sprite thumb;
    public string description;
    public int cost;
	public int ID;

	public Skin () {
		ID = _ID;
		_ID++;
	}
}
