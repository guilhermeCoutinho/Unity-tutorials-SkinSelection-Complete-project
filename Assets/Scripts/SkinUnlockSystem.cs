using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[ExecuteInEditMode]
public class SkinUnlockSystem : MonoBehaviour {

    #region Singleton
    static SkinUnlockSystem instance;
    public static SkinUnlockSystem Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<SkinUnlockSystem>();
            if (instance == null)
                Debug.LogError("You need a skin unlocking system in your scene");
            return instance;
        }
    }
    #endregion

    public Transform skinsHolder;
	public Text budgetText;

	void Start () {
		updateBudgetUI ();
        
		foreach (Adapter adpt in skinsHolder.GetComponentsInChildren<Adapter>()) {
			adpt.enableSkin (Save.playerData.HasSkin (adpt.skin.ID));
		}
	}

	public bool unlockSkin (Skin skin){
		if (!Save.playerData.HasSkin (skin.ID))
			if (try2Buy (skin.cost)) {
				Save.playerData.UnlockSkin (skin.ID);
				Save.save ();
				return true;
			}

		return false;
	}

	void updateBudgetUI () {
		budgetText.text = Save.playerData.budget.ToString ();
	}

	public bool try2Buy( int cost ){
		if ( Save.playerData.budget - cost >= 0){
			Save.playerData.budget -= cost;
			updateBudgetUI ();
			return true;
		}
		return false;
	}

	public void getMoney ( int money) {
		Save.playerData.budget += money;
		Save.save ();
		updateBudgetUI ();
	}

}
