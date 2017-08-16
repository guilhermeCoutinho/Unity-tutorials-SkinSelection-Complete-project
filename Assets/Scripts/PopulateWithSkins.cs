using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PopulateWithSkins : MonoBehaviour {
    public SkinDB skins;
    public GameObject adapter;

	void OnEnable () {
		if ( adapter == null || skins == null) {
			Debug.LogError ("Missing references in a execute in edit mode script");
			return;
		}
		for (int i = transform.childCount - 1; i >= 0; i--) {
			DestroyImmediate (transform.GetChild (i).gameObject);
		}
		foreach (Skin skin in skins.mySkins){
			var instance = Instantiate(adapter, transform);
			instance.transform.localScale = Vector3.one;
			instance.name = skin.description;
			// instance.GetComponent<Adapter>().bindData();
		}
		bindAdapters ();
	}

	public void bindAdapters(){
		for (int i = 0; i < transform.childCount; i++){
			Adapter adapter = transform.GetChild(i).GetComponent<Adapter>();
			adapter.bindData(skins.mySkins[i]);
		}
	}
	void bindAdapter(int i){
		Adapter adapter = transform.GetChild(i).GetComponent<Adapter>();
		adapter.bindData(skins.mySkins[i]);
	}


}
