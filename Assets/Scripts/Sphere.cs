using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour, Interactable
{
    [SerializeField]
    private Material unhighlightedMaterial;

    [SerializeField]
    private Material highlightedMaterial;

    public string interactMessage => throw new System.NotImplementedException();

    public bool isInteractable => throw new System.NotImplementedException();

    public void highlight()
    {
        this.GetComponent<Renderer>().material = highlightedMaterial;
        Console.WriteLine("highlighted");
    }

    public void interact()
    {
        throw new System.NotImplementedException();
    }

    public void unhighlight()
    {
        this.GetComponent<Renderer>().material = unhighlightedMaterial;
        Console.WriteLine("unhighlighted");
    }
}
