using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InputCheckScript : MonoBehaviour
{
    [SerializeField] GameObject input_springStiffness;
    [SerializeField] GameObject input_mediumViscosity;
    [SerializeField] GameObject input_objectMass;
    [SerializeField] GameObject input_springStartDeformation;
    [SerializeField] GameObject input_startVelocity;
    [SerializeField] GameObject input_time;
    [SerializeField] CalculationScript caclculator;
    [SerializeField] Color goodColor = Color.green;
    [SerializeField] Color badColor = Color.red;
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    [SerializeField] DrawGraph drawGraph;
    [SerializeField] Button animationButton;
    // Start is called before the first frame update

    public void CheckInput()
    {
        animationButton.gameObject.SetActive(false);
        Debug.Log("Checking Input");

        Debug.Log(input_springStiffness.GetComponent<TMP_InputField>().text);
        bool inputError = false;
        if (!IsInputValid(input_springStiffness.GetComponent<TMP_InputField>().text))
        { 
            inputError = true;
            input_springStiffness.GetComponent<Image>().color = badColor;
            Debug.Log("input_springStiffness is invalid");
            textMeshProUGUI.text = "¬веденна€ жесткость пружины не валидна";
        }
        else { input_springStiffness.GetComponent<Image>().color = goodColor; }

        if (!IsInputValid(input_mediumViscosity.GetComponent<TMP_InputField>().text))
        {
            inputError = true;
            input_mediumViscosity.GetComponent<Image>().color = badColor;
            Debug.Log("input_mediumViscosity is invalid");
            textMeshProUGUI.text = "¬веденный коэф. в€зкости среды не валиден";
        }
        else { input_mediumViscosity.GetComponent<Image>().color = goodColor; }

        if (!IsInputValid(input_objectMass.GetComponent<TMP_InputField>().text))
        {
            inputError = true;
            input_objectMass.GetComponent<Image>().color = badColor;
            Debug.Log("input_objectMass is invalid");
            textMeshProUGUI.text = "¬веденна€ масса не валидна";
        }
        else { input_objectMass.GetComponent<Image>().color = goodColor; }

        if (!IsInputValid(input_springStartDeformation.GetComponent<TMP_InputField>().text))
        {
            inputError = true;
            input_springStartDeformation.GetComponent<Image>().color = badColor;
            Debug.Log("input_springStartDeformation is invalid");
            textMeshProUGUI.text = "¬веденна€ начальна€ деформаци€ пружины не валидна";
        }
        else { input_springStartDeformation.GetComponent<Image>().color = goodColor; }

        if (!IsInputValid(input_startVelocity.GetComponent<TMP_InputField>().text))
        {
            inputError = true;
            input_startVelocity.GetComponent<Image>().color = badColor;
            Debug.Log("input_startVelocity is invalid");
            textMeshProUGUI.text = "¬веденна€ начальна€ скорость не валидна";
        }
        else { input_startVelocity.GetComponent<Image>().color = goodColor; }

        if (!IsInputValid(input_time.GetComponent<TMP_InputField>().text))
        {
            inputError = true;
            input_time.GetComponent<Image>().color = badColor;
            Debug.Log("input_time is invalid");
            textMeshProUGUI.text = "¬веденное врем€ не валидно";
        }
        else { input_time.GetComponent<Image>().color = goodColor; }


        drawGraph.ClearGraph();
        if (!inputError)
        {
            textMeshProUGUI.text = "";
           caclculator.GetInput();
        }
    }

    private bool IsInputValid(string text)
    {
        Debug.Log("Validating: " + text);
        if(text==null) { return false; }
        else if (text.Length == 0) { return false; }
        else if (text.Length > 20) { return false; }

        bool dotFound = false;
        for(int i =0;i<text.Length;i++)
        {
            char c = text[i];
            if (c == ',') 
            {
                if (i == 0) { return false; }   // первый символ '.' -> не валидно
                if(dotFound) { return false; }  // точку уже находили -> не валидно
                dotFound = true;
            }
            else if (!System.Char.IsDigit(c)) { return false; } //если хоть 1 символ не число -> не валидно
            if (i == 20) { return false; }  //вышло за границу -> не валидно
        }
        return true;
    }
}
