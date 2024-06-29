using UnityEngine;

public static class TxtFiledManager
{
    public static string[] GetTextFromTxt(TextAsset file)
    {
        return file.text.Split('\n');
    }
}
