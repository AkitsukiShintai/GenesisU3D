using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
public class  LoadJson 
{
    public static string defaultPath = null;

  

    /// <summary>
    /// 加载本地文件
    /// </summary>
    public static T LoadJsonFromFile<T>(string path)
    {
        
        if (!File.Exists(path))
        {
            Debug.Log("nothing");
            return default(T);
        }

        StreamReader sr = new StreamReader(path);

        if (sr == null)
        {
            return default(T);
        }
        string json = sr.ReadToEnd();

        if (json.Length > 0)
        {
            //Debug.Log(json.Length);
            //Debug.Log(json);
            
            JsonSerializerSettings setting = new JsonSerializerSettings();
           // setting.
            return JsonConvert.DeserializeObject<T>(json);
        }

        return default(T);
    }


    /// <summary>
    /// 加载网络数据
    /// </summary>
    public static T LoadJsonFromNet<T>(string result)
    {
        Debug.Log(result.Length);
        return JsonUtility.FromJson<T>(result);
    }
}