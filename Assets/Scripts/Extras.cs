using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UniversalEventArgs<T> : EventArgs where T : class
{
	public string Name { get; set; }
	public T CustomVariable { get; set; }
	public string VarTypeName => CustomVariable == null ? "" : CustomVariable.GetType().Name;
}
