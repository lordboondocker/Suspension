using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Input Field Validator", menuName = "Input Field Validator")]
public class InputValidator : TMP_InputValidator
{
    // Start is called before the first frame update
    [SerializeField] Color goodColor = Color.green;
    [SerializeField] Color badColor = Color.red; 
    [SerializeField] GameObject owner;

    public override char Validate(ref string text, ref int pos, char ch)
    {
        Debug.Log("Validating: " + text);
        if (text == null) { this.GetComponent<Image>().color = badColor; return '1'; }
        else if (text.Length == 0) { this.GetComponent<Image>().color = badColor; return '1'; }
        else if (text.Length > 20) { this.GetComponent<Image>().color = badColor; return '1'; }

        bool dotFound = false;
        for (int i = 0; i < text.Length; i++)
        {
            char c = text[i];
            if (c == ',')
            {
                if (i == 0) { this.GetComponent<Image>().color = badColor; return '1'; }   // первый символ '.' -> не валидно
                if (dotFound) { this.GetComponent<Image>().color = badColor; return ','; }  // точку уже находили -> не валидно
                dotFound = true;
            }
            else if (!System.Char.IsDigit(c)) { this.GetComponent<Image>().color = badColor; return '2'; } //если хоть 1 символ не число -> не валидно
            if (i == 20) { this.GetComponent<Image>().color = badColor; return '1'; }  //вышло за границу -> не валидно
        }
        this.GetComponent<Image>().color = goodColor;
        return '\0';
    }
}
