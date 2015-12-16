using UnityEngine;

public static class AudioUtility
{
    /// <summary>
    ///     Converts a linear volume into a logarithmic decibel volume.
    /// </summary>
    public static float LinearToLog(float input)
    {
        return Mathf.Clamp(10.0f * (Mathf.Log(input, 2.0f)), float.MinValue, float.MaxValue);
    }

    /// <summary>
    ///     Converts a logarithmic decibel volume into a linear volume.
    /// </summary>
    public static float LogToLinear(float input)
    {
        return Mathf.Clamp(Mathf.Pow(2.0f, input / 10.0f), float.MinValue, float.MaxValue);
    }
}