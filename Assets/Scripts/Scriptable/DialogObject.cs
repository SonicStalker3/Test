using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialog", order=-1)]
public class DialogObject : ScriptableObject
{
    public DialogText[] dialog;
    public int iteration;

    public void Reset()
    {
        iteration = 0;
    }

    public DialogText Set(int x)
    {
        if (x<dialog.Length) 
        {
            iteration = x;
        }
        return dialog[iteration];
    }

    public DialogText Next()
    {
        DialogText temp = null;
        if (iteration < dialog.Length)
        {
            temp = dialog[iteration];
            iteration += 1;
            if (temp is null)
            {
                return CreateInstance<DialogText>();
            }
        }

        return temp;
    }
}
