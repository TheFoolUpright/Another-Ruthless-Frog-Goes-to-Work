using UnityEngine;


public class Randomness
{
    public static float NextGaussian()
    {
        float v1, v2, s;

        do
        {
            v1 = 2.0f * Random.Range(0.0f, 1.0f) - 1.0f;
            v2 = 2.0f * Random.Range(0.0f, 1.0f) - 1.0f;
            s = v1 * v1 + v2 * v2;
        }
        while (s > 1.0f || s == 0f);

        s = Mathf.Sqrt((-2.0f * Mathf.Log(s)) / s);
        return v1 * s;
    }

    public static float NextGaussian(float mean, float std_dev)
    {
        return mean + NextGaussian() * std_dev;
    }

    public static float NextGaussian(float mean, float std_dev, float min, float max)
    {
        float v;        do
        {
            v = NextGaussian(mean, std_dev);
        }
        while (v < min || v > max);
        return v;
    }

}
