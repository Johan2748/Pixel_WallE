using UnityEngine;

public class ReadInputText : MonoBehaviour
{
    public static string input { get; private set; }

    public void ReadInput(string text)
    {
        input = text;
        Debug.Log(input);        
    }


}
