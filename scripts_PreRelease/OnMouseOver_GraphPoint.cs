using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnMouseOver_GraphPoint : MonoBehaviour
{
    public float t = 0;
    public float value = 0;
    public Image ownerImage;

    private void OnMouseEnter()
    {
        Debug.Log(t + ":" + value);
        ownerImage.color = Color.red;
    }

    private void OnMouseExit()
    {
        ownerImage.color = Color.white;
    }

    public void SetOwner(Image ownerImage)
    {
        this.ownerImage = ownerImage;
    }
}
