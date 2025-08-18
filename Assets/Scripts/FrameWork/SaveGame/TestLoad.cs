using System;
using UnityEngine;

public class TestLoad :MonoBehaviour
{
    private Test mTest = new Test();
    
    private void Start()
    {
        Load();
        Save();
    }

    public void Save()
    {
        mTest.mName = "TestSave" + DateTime.Now;
        mTest.mId = DateTime.Now.ToFileTimeUtc();
        Debug.Log($"{mTest.mName} + {mTest.mId}");
        mTest.SaveData();
    }
    
    public void Load()
    {
        if (mTest.LoadData())
            Debug.Log($"{mTest.mName} + {mTest.mId}");
    }
}

public class Test : Save
{
    public override string mFileName => "TestSave";
    public string mName = "None Name";
    public long mId = 1;
    public int mId1 = 2;

    public override bool WriteA(SaveHandler writer)
    {
        writer.Write(mName);
        writer.Write(mId);
        writer.Write(mId1);
        return true;
    }
    
    public override bool ReadA(SaveHandler reader)
    {
        mName = reader.Read<string>();
        mId = reader.Read<long>();
        mId1 = reader.Read<int>();
        return true;
    }
}
