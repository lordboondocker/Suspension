using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class GraphPoint : MonoBehaviour
{
    [SerializeField] Color circleColor = Color.white;
    [SerializeField] Sprite circleSprite;
    [SerializeField] float width = 4f;
    // Start is called before the first frame update   
    public void CreatePoints(ref Vector2[] points, ref RectTransform graphContainer, ref float sizeCoefficientX, ref float sizeCoefficientY, ref int xSize, ref int xStart)
    {
        // Create the line object and set it up
        GameObject lineObj = new GameObject("line", typeof(LineRenderer));
        lineObj.transform.SetParent(graphContainer, false);
        lineObj.layer = 5;

        LineRenderer lR = lineObj.GetComponent<LineRenderer>();
        lR.useWorldSpace = false; // Use local space to align with UI elements
        lR.material = new Material(Shader.Find("Sprites/Default"));
        lR.startColor = Color.white;
        lR.endColor = circleColor;
        lR.startWidth = width;
        lR.endWidth = width;
        lR.positionCount = xSize;

        // Set sorting order to ensure the line renderer is on top
        lR.sortingOrder = 1;  // Adjust this value if needed

        Vector3[] linePoints = new Vector3[xSize];

        for (int index = xStart; index < points.Length; index++)
        {
            if (index >= xSize + xStart) { break; }

            GameObject circle  = new GameObject("circle",typeof(Empty<GameObject>)); circle.transform.SetParent(graphContainer, false);
            // Drawing of a circle
            /*
            GameObject circle = new GameObject("circle", typeof(Image));
            circle.transform.SetParent(graphContainer, false);
            
            
            circle.GetComponent<Image>().sprite = circleSprite;
            circle.GetComponent<Image>().color = Color.white;
            var rndrr = circle.AddComponent<SpriteRenderer>();
            circle.gameObject.layer = 5;
            rndrr.sortingOrder = 2;
            CircleCollider2D col = circle.AddComponent<CircleCollider2D>();
            col.radius = 2f;
            OnMouseOver_GraphPoint oMO_gP = circle.AddComponent<OnMouseOver_GraphPoint>();
            oMO_gP.enabled = true;
            oMO_gP.t = points[index - xStart].x;
            oMO_gP.value = points[index - xStart].y;
            oMO_gP.SetOwner(circle.GetComponent<Image>());
            */

            RectTransform rectTransform = circle.AddComponent<RectTransform>();
            points[index-xStart] = new Vector2((index-xStart) * sizeCoefficientX, points[index].y * sizeCoefficientY * 0.95f);
            rectTransform.anchoredPosition = points[index - xStart];
            rectTransform.sizeDelta = new Vector2(5, 5);
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(0, 0);

            // Calculate line points relative to the graph container
            linePoints[index-xStart] = new Vector3(rectTransform.localPosition.x, rectTransform.localPosition.y, 0);
        }

        lR.SetPositions(linePoints);

        // Ensure the line renderer is enabled
        lR.enabled = true;
    }

}
