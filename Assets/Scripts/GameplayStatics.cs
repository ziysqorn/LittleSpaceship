using Game.Interfaces;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class GameplayStatics
{
	public static void ApplyDamage(in GameObject damagedObj, int damageAmount, in GameObject instigator, in GameObject damageCauser)
	{
		if (damagedObj)
		{
			IDamageable damageable = damagedObj.GetComponent<IDamageable>();
			if (damageable != null)
			{
				damageable.TakeDamage(damageAmount, instigator, damageCauser);
			}
		}
	}

	public static void SaveGame(in SaveObject saveObj, in string saveLoc)
	{
		string path = Application.persistentDataPath + saveLoc;
		FileStream fileStream = null;
		if (File.Exists(path)) 
			fileStream = new FileStream(path, FileMode.Open);
		else 
			fileStream = new FileStream(path, FileMode.Create);
		BinaryFormatter formatter = new BinaryFormatter();
		formatter.Serialize(fileStream, saveObj);
		fileStream.Close();
	}

	public static void DeleteSaveGame(in string saveLoc)
	{
		string path = Application.persistentDataPath + saveLoc;
		if (File.Exists(path))
		{
			File.Delete(path);
		}
	}

	public static T LoadGame<T>(in string saveLoc) where T : SaveObject
	{
		string path = Application.persistentDataPath + saveLoc;
		T result = null;
		if (File.Exists(path))
		{
			FileStream fileStream = new FileStream(path, FileMode.Open);
			BinaryFormatter formatter = new BinaryFormatter();
			result = formatter.Deserialize(fileStream) as T;
			fileStream.Close();
		}
		return result;
	} 
}
