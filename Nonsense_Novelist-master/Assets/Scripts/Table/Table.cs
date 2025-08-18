using UnityEngine;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Build.Content;

public enum TableErrorType : byte
{
    None = 0,
    NotExist,
    ParseLine,
    Duplicate
}

public class TableErrorMeta
{
    public TableErrorType mErrorType = TableErrorType.None;
    public int row = 0;
    public int col = 0;
}

public abstract class TableBase
{
    public abstract string AssetName { get; }

    public virtual TableErrorMeta Load()
    {
        var textAsset = AssetManager.Load<TextAsset>("Tables", AssetName);

        if (textAsset == null)
            return new TableErrorMeta { mErrorType = TableErrorType.NotExist };

        return Parse(textAsset.text);
    }
    
    public virtual void PreLoad() { }
    
    public virtual void PostLoad() { }

    public abstract TableErrorMeta Parse(string text);
}

public abstract class VecTable<TV> : TableBase
{
    public static List<TV> DataList = new();
    
    protected abstract TV ParseVecEntry(TokenReader reader);
    
    public static TV Get(int index)
    {
        if (index >= 0 && index < DataList.Count)
        {
            return DataList[index];
        }

        return default;
    }
    
    public static int GetIndex(TV val)
    {
        return DataList.IndexOf(val);
    }

    public override void PreLoad()
    {
        DataList.Clear();
    }

    public override TableErrorMeta Parse(string text)
    {
        TableErrorMeta errorMeta = new TableErrorMeta();
        StringReader stringReader = new StringReader(text);

        // ignore first line

        bool bAdvance = false;
        {
            string headLine = stringReader.ReadLine();
            TokenReader tokenReader = new TokenReader(headLine);
            
            if (tokenReader.Read<string>().Contains("//"))
            {
                bAdvance = true;
            }
        }

        int lineCnt = 1;

        while (true)
        {
            string line = stringReader.ReadLine();
            if (line == null)
                break;
            else
            {
                ++lineCnt;

                TokenReader tokenReader = new TokenReader(line);

                if (bAdvance)
                {
                    tokenReader.Advance();
                }
                
                TV data = ParseVecEntry(tokenReader);
                if (!tokenReader.IsReadValid())
                {
                    errorMeta.mErrorType = TableErrorType.ParseLine;
                    errorMeta.row = lineCnt;
                    errorMeta.col = tokenReader.ReadPos;
                    break;
                }
                
                DataList.Add(data);
            }
        }

        return errorMeta;
    }
}

public abstract class MapTable<TK, TV> : TableBase
{
    public static Dictionary<TK, TV> DataList = new();
    
    public static TV Find(TK key)
    {
        if (DataList.TryGetValue(key, out var rst)) 
            return rst;

        return default;
    }

    protected abstract KeyValuePair<TK, TV> ParseMapEntry(TokenReader reader);
    
    public override void PreLoad()
    {
        DataList.Clear();
    }

    public override TableErrorMeta Parse(string text)
    {
        TableErrorMeta errorMeta = new TableErrorMeta();
        StringReader stringReader = new StringReader(text);

        // ignore first line
        bool bAdvance = false;
        {
            string headLine = stringReader.ReadLine();
            TokenReader tokenReader = new TokenReader(headLine);
            
            if (tokenReader.Read<string>().Contains("//"))
            {
                bAdvance = true;
            }
        }

        int lineCnt = 1;

        while (true)
        {
            string line = stringReader.ReadLine();
            if (line == null)
                break;
            else
            {
                ++lineCnt;

                TokenReader tokenReader = new TokenReader(line);
                if (bAdvance)
                {
                    tokenReader.Advance();
                }

                var data = ParseMapEntry(tokenReader);
                if (!tokenReader.IsReadValid())
                {
                    errorMeta.mErrorType = TableErrorType.ParseLine;
                    errorMeta.row = lineCnt;
                    errorMeta.col = tokenReader.ReadPos;
                    break;
                }
                
                if (DataList.ContainsKey(data.Key))
                {
                    errorMeta.mErrorType = TableErrorType.Duplicate;
                    errorMeta.row = lineCnt;
                    errorMeta.col = 0;
                    break;
                }
                
                DataList.Add(data.Key, data.Value);
            }
        }

        return errorMeta;
    }
}

public abstract class JsonTable<TV> : TableBase
{
    public static TV mData;
    
    public override TableErrorMeta Parse(string text)
    {
        TableErrorMeta errorMeta = new TableErrorMeta();

        mData = JsonUtility.FromJson<TV>(RemoveJsonComments(text));
        if (mData == null)
        {
            errorMeta.mErrorType = TableErrorType.ParseLine;
        }
        
        return errorMeta;
    }
    
    public string RemoveJsonComments(string json)
    {
        // ÒÆ³ýµ¥ÐÐ×¢ÊÍ
        json = System.Text.RegularExpressions.Regex.Replace(json, @"//.*", "");
        // ÒÆ³ý¶àÐÐ×¢ÊÍ
        json = System.Text.RegularExpressions.Regex.Replace(json, @"/\*.*?\*/", "", System.Text.RegularExpressions.RegexOptions.Singleline);
        return json;
    }    
}
