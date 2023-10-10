// Copyright (c) 2023 Adrián Xuíz García.
// Licensed under the MIT License.

using System;
using UnityEngine;

public class ErrorManager
{
    public static ErrorManager Instance;
    public static event Action<string> OnNewError;

    void Awake(){
        Instance = this;
    }

    public static void NewErrorMessage(string s)
	{	
		Debug.Log("<color=red>VMDC ERROR: </color>"+s);
        OnNewError?.Invoke(s);
    }

    public static void NewErrorMessageThrowException(string s)
	{	
		Debug.Log("<color=red>VMDC ERROR: </color>"+s);
        OnNewError?.Invoke(s);
		throw new Exception();
    }
}
