using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class xStartScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI inputField_xStart;
    [SerializeField] Color goodColor;
    [SerializeField] Color badColor;
    [SerializeField] DrawGraph dGScript;
    // Start is called before the first frame update

    public void OnValueChange()
    {
        if (inputField_xStart.text!="") 
        { 
            for(int i = 0;i<inputField_xStart.text.Length;i++)
            {
                char c = inputField_xStart.text[i];
                if (!System.Char.IsDigit(c)) { return; inputField_xStart.color = badColor; }
            }
        }
        dGScript.xStart =0 ;
        dGScript.UpdateGraph();
    }
}
