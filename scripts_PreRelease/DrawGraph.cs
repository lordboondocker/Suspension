using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DrawGraph : MonoBehaviour
{
    [SerializeField] RectTransform graphContainer;

    [SerializeField] float sizeCoefficientY = 1;
    [SerializeField] float sizeCoefficientX = 1;
    [SerializeField] TextMeshProUGUI textMeshPro;
    [SerializeField] GraphPoint graphPoint;
    [SerializeField] Button animationButton;
    public int xStart = 0;
    public int xSize = 100;
    // Start is called before the first frame update
    public Vector2[] graphData;

    public void Awake()
    {
    }
    public void UpdateGraph()
    {
        //Calculating sizeCoefficientY
        float maxHeight = 0;
        foreach (var point in graphData)
        {
            if (Math.Abs(point.y) > maxHeight) { maxHeight = Math.Abs(point.y); }
        }
        Debug.Log(maxHeight);
        Debug.Log(graphContainer.sizeDelta.y);
        sizeCoefficientY = (graphContainer.sizeDelta.y) / maxHeight;

        if(sizeCoefficientY == float.PositiveInfinity) { Debug.Log("sizeCoefficientY is Infinity"); textMeshPro.text="Показатели графика стремятся к бесконечности. Уменьшите время или измените другие входные данные."; return; }

        //Calcultaing sizeCoefficientX
        sizeCoefficientX = (graphContainer.sizeDelta.x) / xSize;
        if (sizeCoefficientX == float.PositiveInfinity) { Debug.Log("sizeCoefficientX is Infinity"); return; }

        //Drawing the graph
        Debug.Log("Updating Graph");
        ClearGraph();
        graphPoint.CreatePoints(ref graphData,ref graphContainer,ref sizeCoefficientX,ref sizeCoefficientY, ref xSize, ref xStart);
        animationButton.gameObject.SetActive(true);
    }

    public void ClearGraph()
    {
        foreach (Transform child in graphContainer.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
