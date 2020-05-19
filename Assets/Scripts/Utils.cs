using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;

public static class Utils {

	public static T[] Scramble<T> (T[] array) {
		 
		// shuffle chars
		for (int t = 0; t < array.Length; t++ )
		{
			T tmp = array[t];
			int r = UnityEngine.Random.Range(t, array.Length);
			array[t] = array[r];
			array[r] = tmp;
		}
		return array;
	}

	public static List<T> Scramble<T> (List<T> list) {

		// shuffle chars
		for (int t = 0; t < list.Count; t++ )
		{
			T tmp = list[t];
			int r = UnityEngine.Random.Range(t, list.Count);
			list[t] = list[r];
			list[r] = tmp;
		}
		return list;
	}


	public static void DelayAndCall (MonoBehaviour caller, float delay, Action callBack) {
		caller.StartCoroutine (DelayAndCallRoutine (delay, callBack));
	}

	public static void StaggerAndCall<T> (MonoBehaviour caller, float delay, Action<T> callBack, List<T> items) {
		caller.StartCoroutine (StaggerAndCallRoutine<T> (delay, callBack, items));
	}

	static IEnumerator StaggerAndCallRoutine<T>  (float delay, Action<T> callBack, List<T> items) {
		var i = 0;
		while (i < items.Count) {
			callBack (items[i]);
			yield return new WaitForSeconds (delay);
			i++;
		}
	}

	static IEnumerator DelayAndCallRoutine  (float delay, Action callBack) {
		yield return new WaitForSeconds (delay);
		callBack ();
	}

	////https://answers.unity.com/questions/578393/clear-console-through-code-in-development-build.html
	static MethodInfo _clearConsoleMethod;
	static MethodInfo clearConsoleMethod
	{
		get
		{
			if (_clearConsoleMethod == null)
			{
				////Assembly assembly = Assembly.GetAssembly(typeof(SceneView));
				////Type logEntries = assembly.GetType("UnityEditor.LogEntries");
				////_clearConsoleMethod = logEntries.GetMethod("Clear");
			}
			return _clearConsoleMethod;
		}
	}

	public static void ClearLogConsole()
	{
		////clearConsoleMethod.Invoke(new object(), null);
	}
	public static void MyLog(string message)
	{
		////Debug.Log(message + "\n" + DateTime.Now.ToString());
	}
}
