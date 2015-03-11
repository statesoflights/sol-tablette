using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using System.Runtime.Serialization;

public static class saveLoad {

	public static gameData game = new gameData();

	public static void Save() {
		saveLoad.SetEnvironmentVariables();
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/savedGames.gd"); 
		bf.Serialize(file, saveLoad.game);
		file.Close();
	}   
	
	public static void Load() {
		if(File.Exists(Application.persistentDataPath + "/savedGames.gd")) {
			saveLoad.SetEnvironmentVariables();
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
			saveLoad.game = (gameData)bf.Deserialize(file);
			file.Close();
		}
	}

	public static bool isExists(){
		if (File.Exists (Application.persistentDataPath + "/savedGames.gd")) {
			return true;
		}
		return false;
	}

	private static void SetEnvironmentVariables() {
		Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
	}
}
