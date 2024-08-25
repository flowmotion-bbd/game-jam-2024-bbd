using UnityEngine;
using UnityEngine.UI;

public class ValidCharAndLengthValidator : MonoBehaviour
{
    [SerializeField] private int maxLength = 10;
    [SerializeField] private string allowedCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    void Start()
    {
        GetComponent<InputField>().onValidateInput = Validate;
    }

    public char Validate(string text, int pos, char ch)
    {
        if (validText(text) && validChar(ch))
        {
            return ch;
        }

        return '\0';
    }

    bool validChar(char c)
    {
        return allowedCharacters.Contains(c);
    }

    bool validText(string text)
    {
        return text.Length < maxLength;
    }
}
