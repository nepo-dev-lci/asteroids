using UnityEngine;
using UnityEngine.UI;

class RadialProgressBar
{
    public Shader progressBarShader;
    private Material material;

    public RadialProgressBar(GameObject gameObject)
    {
        material = new Material(gameObject.GetComponent<Image>().material);
        gameObject.GetComponent<Image>().material = material;
    }

    public void SetProgress(float progress)
    {
        material.SetFloat("_Fillpercentage", progress);
    }

    public float GetProgress()
    {
        return material.GetFloat("_Fillpercentage");
    }

}