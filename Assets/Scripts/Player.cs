using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player {

	List<int> unlocked_skins_IDs;
	public int budget;

	/* 
	 * The skin with the 0-index is going to be the always unlocked- default- skin 
	*/
	public Player () {
		unlocked_skins_IDs = new List<int> ();
		budget = 0;
		unlocked_skins_IDs.Add (0);
	}

	public bool HasSkin (int ID){
		foreach (int _id in unlocked_skins_IDs) {
			if (ID == _id)
				return true;
		}
		return false;
	}

	public void UnlockSkin (int ID) {
		if (HasSkin (ID))
			return;
		unlocked_skins_IDs.Add (ID);
	}
}
