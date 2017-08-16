using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Adapter : MonoBehaviour {
    public Image thumb;
    public Text description;

    public Skin skin;

    public void bindData (Skin skin) {
        this.thumb.sprite = skin.thumb;
		this.skin = skin;
		enableSkin (true);
    }

	public void enableSkin (bool enable) {
		this.enabled = enable;
		if (enabled) {
			thumb.color = Vector4.one;
			this.description.text = skin.description;
		} else {
			thumb.color = new Vector4 (.2f, .2f, .2f, .2f);
			description.text = skin.cost.ToString();
		}		
	}

    public  void buttonClicked (){
		if (SkinUnlockSystem.Instance.unlockSkin (skin)) {
			enableSkin (true);
		}
    }

}
