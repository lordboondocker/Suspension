using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowAnimationBttn_Script : MonoBehaviour
{
    [SerializeField] Texture2D wheelImage;
    // Start is called before the first frame update

    private void Awake()
    {
        this.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonClick() 
    {
        TextMeshProUGUI bttnText = this.gameObject.GetComponentInChildren<TextMeshProUGUI>();
        if (bttnText.text == "показать анимацию")
        {
            bttnText.text = "показать график";
        }
        else { bttnText.text = "показать анимацию"; }

    }

    void StartAnimation() 
    {
    
    }
    void HideGraph() 
    {
        
    }
}
