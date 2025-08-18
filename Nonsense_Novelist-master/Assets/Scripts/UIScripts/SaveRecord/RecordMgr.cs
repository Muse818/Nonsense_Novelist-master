using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using LitJson;
using System.IO;
public class RecordMgr : MonoSingleton<RecordMgr>
{
    public List<TextRecord> recordList =null;

    private string res = "Assets/Record/";
    
    private void Awake()
    {
        base.Awake();
        //if (recordList == null)
        //{
        //    recordList = ResMgr.GetInstance().Load<FinalText>("TextRecord").textList;
        //}
    } 

    //public void AddRecord(string _name,List<string> _content,int _RAND)
    //{
    //    int id = recordList2.Count;
    //    TextRecord _record = ScriptableObject.CreateInstance<TextRecord>(); 
    //    _record.id = id;
    //    _record.title = _name;
    //    _record.content= _content;
    //    _record.rand = _RAND;
    //    _record.reply = "无";
    //    _record.hasRead = false;
    //    AssetDatabase.CreateAsset(_record, res+id+".asset");
    //    AssetDatabase.SaveAssets();
    //    AssetDatabase.Refresh();
    //    recordList.Add(_record);
    //    AssetDatabase.SaveAssets();
    //    AssetDatabase.Refresh();
    //}

    public void SaveByJson(string _name, List<string> _content, int _RAND)
    {
        //序列化过程（将Save对象转换为字节流） 
        //创建Save对象并保存当前游戏状态
      
        SaveArticle save = new SaveArticle();
        DirectoryInfo di = new DirectoryInfo(Application.dataPath + "/StreamingAssets");
        var _files = di.GetFiles("*");
        save.id = _files.Length ;
        save.title = _name;
        save.content = _content;
        save.rand = _RAND;
        save.reply = "无";
        save.hasRead = false;
        //定义字符串filePath保存文件路径信息（就是在Assets中创建的一个文件夹名称为StreamFile,然后系统会给我创建一个byJson.json用于保存游戏信息）
        string filePath = Application.dataPath + "/StreamingAssets/" + save.id.ToString() +".json";
        //利用JsonMapper将save对象转换为Json格式的字符串(这里要引入命名空间using LitJson)
        string saveJsonStr = JsonMapper.ToJson(save);
        //将这个字符串写入到文件中
        //创建一个StreamWriter,并将字符串写入到文件中
        StreamWriter sw = new StreamWriter(filePath);
        sw.Write(saveJsonStr);
        //关闭StreamWriter
        sw.Close();
    }

    public void ChangeReadJson(string path)
    {
        //序列化过程（将Save对象转换为字节流） 
        //创建Save对象并保存当前游戏状态

        SaveArticle save = LoadByJson(path);

        save.hasRead = true;
        //定义字符串filePath保存文件路径信息（就是在Assets中创建的一个文件夹名称为StreamFile,然后系统会给我创建一个byJson.json用于保存游戏信息）
        string filePath = path;
        //利用JsonMapper将save对象转换为Json格式的字符串(这里要引入命名空间using LitJson)
        string saveJsonStr = JsonMapper.ToJson(save);
        //将这个字符串写入到文件中
        //创建一个StreamWriter,并将字符串写入到文件中
        StreamWriter sw = new StreamWriter(filePath);
        sw.Write(saveJsonStr);
        //关闭StreamWriter
        sw.Close();
    }

    public SaveArticle LoadByJson(string filepath)
    {
        if (File.Exists(filepath))
        {
            //创建一个StreamReader,用来读取流
            StreamReader sr = new StreamReader(filepath);
            //将读取到的流赋值给jsonStr
            string jsonStr = sr.ReadToEnd();
            //关闭
            sr.Close();

            //将字符串jsonStr转换为Save对象
            SaveArticle save = JsonMapper.ToObject<SaveArticle>(jsonStr);
            return save;
        }
        return null;
    }
}
