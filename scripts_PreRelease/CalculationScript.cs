using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CalculationScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject input_springStiffness;
    [SerializeField] GameObject input_mediumViscosity;
    [SerializeField] GameObject input_objectMass;
    [SerializeField] GameObject input_springStartDeformation;
    [SerializeField] GameObject input_startVelocity;
    [SerializeField] GameObject input_time;

    private float c;    // input_springStiffness
    private float u;    // input_mediumViscosity
    private float M;    // input_objectMass
    private float l0;   // input_springStartDeformation
    private float v0;   // input_startVelocity
    private float timeSpan;    // input_time

    private float k;
    private float n;

    static private ulong iterCount = 2000;
    private float tStep;


    private Vector2[] results = new Vector2[iterCount];
    [SerializeField] DrawGraph drawGraphScript;
    [SerializeField] TextMeshProUGUI textMeshProUGUI;

    public void GetInput() 
    {
        Debug.Log("Getting Input");
        try
        {
            c = (float)Convert.ToDouble(input_springStiffness.GetComponent<TMP_InputField>().text);
            u = (float)Convert.ToDouble(input_mediumViscosity.GetComponent<TMP_InputField>().text);
            M = (float)Convert.ToDouble(input_objectMass.GetComponent<TMP_InputField>().text);
            l0 = (float)Convert.ToDouble(input_springStartDeformation.GetComponent<TMP_InputField>().text);
            v0 = (float)Convert.ToDouble(input_startVelocity.GetComponent<TMP_InputField>().text);
            timeSpan = (float)Convert.ToDouble(input_time.GetComponent<TMP_InputField>().text);
        }
        catch (Exception e){ Debug.Log(e.Message); }
        MakeCalculations();
        drawGraphScript.graphData = results;
        drawGraphScript.UpdateGraph();
    }

    public Vector2 CalculatePoint(float t)
    {
        Debug.Log("Calculating point");


        //3.1
        k = (float)Math.Sqrt(c / M);
        n = u / (2 * M);

        //3.2
        float k1 = (float)Math.Sqrt(k * k - n * n);
        float lmbd = M / c * Physics.gravity.y;
        float x0 = -(lmbd - l0);
        float _x0 = v0;
        float C1 = x0;
        float C2 = (_x0 + n * x0) / k1;
        float A = (float)Math.Sqrt(C1 * C1 + C2 * C2);

        Debug.Log(k1 + " " + lmbd + " " + C1 + " " + C2 + " " + A);

        float alpha = 0;
        if (C1 != 0)
        {
            alpha = MathF.Asin(C1 / A);
        }
        if (C2 != 0)
        {
            alpha = MathF.Acos(C2 / A);
        }
        return new Vector2(t, (float)(A * Math.Exp(-n * (t)) * MathF.Sin(k1 * (t) + alpha)));
    }

    private void MakeCalculations()
    {
        Debug.Log("Calculating");

        tStep = timeSpan / iterCount;

        //3.1
        k = (float)Math.Sqrt(c / M);
        n = u / (2 * M);

        if (n >= k) { Debug.Log("High medium viscosity"); textMeshProUGUI.text = "Среда с большой вязкостью. Возникает апериодическое движение"; return; }

        Debug.Log(n + " " + k);

        //3.2
        float k1 = (float)Math.Sqrt(k * k - n * n);
        float lmbd = M / c * Physics.gravity.y;
        float x0 = -(lmbd - l0);
        float _x0 = v0;
        float C1 = x0;
        float C2 = (_x0 + n * x0) / k1;
        float A = (float)Math.Sqrt(C1*C1+C2*C2);

        Debug.Log(k1 + " " + lmbd + " " + C1 + " " + C2 + " " + A);

        float alpha = 0;
        if(C1 != 0) 
        {
            alpha = MathF.Asin(C1 / A);
        }
        if (C2 != 0) 
        {
            alpha = MathF.Acos(C2 / A);
        }

        for (ulong i = 0; i < iterCount; i++)
        {
            var result = new Vector2(i * tStep, (float)(A * Math.Exp(-n * (i*tStep)) * MathF.Sin(k1 * (i * tStep) + alpha)));
            results[i] = result;
        }

        //Вывод точек перегиба
        Debug.Log("Inflection points");
        for(ulong i = 1;i < iterCount-1; i++)
        {
            if (Math.Abs(results[i].y) > Math.Abs(results[i-1].y) && Math.Abs(results[i].y) > Math.Abs(results[i + 1].y)) {
                Debug.Log(results[i].y);
            }
        }
    }
}
