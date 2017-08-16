using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public static class Save
{
    
	const string FILE_NAME = "/skin_selection_tutorial.gd";
    public static Player playerData = new Player();


    public static void save()
    {
        BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + FILE_NAME);
        bf.Serialize(file, playerData);
        file.Close();
    }

    public static void Load()
    {
//		Debug.Log (Application.persistentDataPath + "/savedGames.gd");
		if (File.Exists(Application.persistentDataPath + FILE_NAME))
        {
            BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + FILE_NAME, FileMode.Open);
            playerData = (Player)bf.Deserialize(file);
            file.Close();
        }
    }

    public static void ResetProgress()
    {
		if (File.Exists(Application.persistentDataPath + FILE_NAME))
        {
			File.Delete(Application.persistentDataPath + FILE_NAME);
            playerData = new Player();
        }
    }

}
