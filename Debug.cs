using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
public class Debug
{
    [Conditional("DEBUG")]
    public static void Log(object message, Object context)
	{
        UnityEngine.Debug.Log(message, context);
	}

    [Conditional("DEBUG")]
    public static void Log(object message)
	{
        UnityEngine.Debug.Log(message);
    }

    [Conditional("UNITY_ASSERTIONS")]
    public static void LogAssertion(object message, Object context)
	{
        UnityEngine.Debug.LogAssertion(message, context)
    }

    [Conditional("UNITY_ASSERTIONS")]
    public static void LogAssertion(object message)
	{
        UnityEngine.Debug.LogAssertion(message);
    }

    [Conditional("UNITY_ASSERTIONS")]
    public static void LogAssertionFormat(Object context, string format, params object[] args)
	{
        UnityEngine.Debug.LogAssertionFormat(context, format, args);
	}

    [Conditional("UNITY_ASSERTIONS")]
    public static void LogAssertionFormat(string format, params object[] args)
	{
        UnityEngine.Debug.LogAssertionFormat(format, args);
    }

    [Conditional("DEBUG")]
    public static void LogError(object message, Object context)
	{
        UnityEngine.Debug.LogError(message, context);
    }

    [Conditional("DEBUG")]
    public static void LogError(object message)
	{
        UnityEngine.Debug.LogError(message);
    }

    [Conditional("DEBUG")]
    public static void LogErrorFormat(string format, params object[] args)
	{
        UnityEngine.Debug.LogErrorFormat(format, args);
    }

    [Conditional("DEBUG")]
    public static void LogErrorFormat(Object context, string format, params object[] args)
	{
        UnityEngine.Debug.LogErrorFormat(context, format, args);
    }

    [Conditional("DEBUG")]
    public static void LogException(System.Exception exception)
	{
        UnityEngine.Debug.LogException(exception)
    }

    [Conditional("DEBUG")]
    public static void LogException(System.Exception exception, Object context)
	{
        UnityEngine.Debug.LogException(exception, context);
    }

    [Conditional("DEBUG")]
    public static void LogFormat(LogType logType, LogOption logOptions, Object context, string format, params object[] args)
	{
        UnityEngine.Debug.LogFormat(logType, logOptions, context, format, args);
    }

    [Conditional("DEBUG")]
    public static void LogFormat(Object context, string format, params object[] args)
	{
        UnityEngine.Debug.LogFormat(context, format, args);
    }

    [Conditional("DEBUG")]
    public static void LogFormat(string format, params object[] args)
	{
        UnityEngine.Debug.LogFormat(format, args)
    }

    [Conditional("DEBUG")]
    public static void LogWarning(object message)
	{
        UnityEngine.Debug.LogWarning(message);
    }

    [Conditional("DEBUG")]
    public static void LogWarning(object message, Object context)
	{
        UnityEngine.Debug.LogWarning(message, context);
    }

    [Conditional("DEBUG")]
    public static void LogWarningFormat(string format, params object[] args)
	{
        UnityEngine.Debug.LogWarningFormat(format, args);
    }

    [Conditional("DEBUG")]
    public static void LogWarningFormat(Object context, string format, params object[] args)
	{
        UnityEngine.Debug.LogWarningFormat(context, format, args);
    }
}
