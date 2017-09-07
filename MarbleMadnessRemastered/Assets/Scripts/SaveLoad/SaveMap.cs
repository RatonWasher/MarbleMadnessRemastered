using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class SaveMap : MonoBehaviour
{
    public string slotName;

    public void SaveEditorMap()
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.dataPath + "/StreamingAssets/" + slotName + ".dat";
        FileStream file = File.Create(path);

        MapData data = new MapData();
        data.name = "slotObjects";


        ArrayList edit_objects = new ArrayList();

        GameObject[] AllgOs = FindObjectsOfType<GameObject>() as GameObject[];
        foreach (GameObject gO in AllgOs)
        {
            //Only store player's block
            if (gO.name.Length >= 5)
            {
                if (gO.name.Substring(0, 5) == "1x1x1" ||
                    gO.name.Substring(0, 5) == "RampL" ||
                    gO.name.Substring(0, 5) == "RampH" ||
                    gO.name.Substring(0, 5) == "EndBl")
                {
                    edit_objects.Add(gO);

                }
            }
        }

        foreach (GameObject edit_object in edit_objects)
        {
            //Getting path of the prefab
            string PrefabName = edit_object.name; PrefabName.Remove(PrefabName.Length - 7);

            //Saving all the informations about the bloc
            EditorObject EObject = new EditorObject(
                edit_object.transform.position, edit_object.transform.rotation.y, PrefabName);
            data.EObjectsList.Add(EObject);
        }


        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadEditorMap(string name)
    {
        slotName = name;
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.dataPath + "/StreamingAssets/"+ slotName + ".dat";
        Stream stream = File.Open(path, FileMode.Open);
        MapData data = new MapData();
        data = (MapData)bf.Deserialize(stream);
        stream.Close();

        GameObject newBlock;

        foreach (EditorObject Block in data.EObjectsList)
        {
            Vector3 position = new Vector3(Block.PosX, Block.PosY, Block.PosZ);

            //Removing (Clone) of the name
            string PrefabPath = Block.PrefabName; PrefabPath = PrefabPath.Remove(PrefabPath.Length - 7);

            //Getting the prefab
            GameObject Prefab = (GameObject)Resources.Load(PrefabPath, typeof(GameObject));

            //Instantiating the block
            newBlock = Instantiate(Prefab, position, Quaternion.Euler(0, Block.Rotation, 0)) as GameObject;
        }
    }
}




[Serializable]
class MapData
{
    public string name;
    public ArrayList EObjectsList = new ArrayList();
}