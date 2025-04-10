using UnityEngine;

public class Testing : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Token token = new Token(TokenType.STAR_STAR, "**", 0);

        Debug.Log(token.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
