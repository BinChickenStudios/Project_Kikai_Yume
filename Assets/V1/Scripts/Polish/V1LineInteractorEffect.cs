using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class V1LineInteractorEffect : XRInteractorLineVisual
{
    [SerializeField] private Material LineMat;
    [SerializeField] private Color col = Color.green;

    private void Start()
    {
        UpdateMatColour();
    }
    public void UpdateMatColour() 
    {
        UpdateMatColour(col);
    }
    public void UpdateMatColour(Color color) 
    { 
        LineMat.SetColor("_EmissionColor", color * 3f);
    }

    
    
}
