using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Interactable
{
    string interactMessage
    {
        get;
    }
    bool isInteractable
    {
        get;
    }

    public void interact();
    public void highlight();
    public void unhighlight();

}
