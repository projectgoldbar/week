using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[DefaultExecutionOrder(-400)]
public static class SaveSystem
{
    public static void SavePlayer(Inventory inventory)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/inventor.dd";
        FileStream stream = new FileStream(path, FileMode.Create);
        InventoryData data = new InventoryData(inventory);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static InventoryData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/inventor.dd";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            InventoryData data = formatter.Deserialize(stream) as InventoryData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in" + path);
            return null;
        }
    }
}