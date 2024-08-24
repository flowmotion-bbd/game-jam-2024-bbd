using UnityEngine;
using TMPro;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "MaxLengthValidator", menuName = "TMP Input Validators/MaxLengthValidator")]
public class ValidCharAndLengthValidator : TMP_InputValidator
{
    public int maxLength = 10;
    public string allowedCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    public override char Validate(ref string text, ref int pos, char ch)
    {
        if (validText(text) && validChar(ch))
        {
            text = text.Insert(pos, ch.ToString());
            pos++;
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
