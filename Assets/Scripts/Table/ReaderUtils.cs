
using System.Collections.Generic;

public class CustomParam
{
    public string mKey;
    public List<float> mValues;
}
    
public static class ReaderUtils
{
    public static bool ParseCustomParams(TokenReader reader, Dictionary<string, CustomParam> paramMap)
    {
        int paramNum = reader.Read<int>();
        
        for (int i = 0; i < paramNum * 2; i += 2)
        {
            CustomParam param = new CustomParam
            {
                mKey = reader.Read<string>(),
                mValues = reader.ReadVec<float>()
            };

            if (param.mValues is { Count: > 0 })
            {
                paramMap.Add(param.mKey, param);
            }
            else
            {
                reader.MarkReadInvalid();
                return false;
            }
        }

        return true;
    }
}