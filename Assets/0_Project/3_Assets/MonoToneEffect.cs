using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Mono Tone")]
public class MonoToneEffect : ImageEffectBase
{
    [Range(-10, 10)] public float Brightness = 3f;

    public Color HighlightColor;
    [Range(0, 1)] public float HighlightThreshold = 0.66f;

    public Color MidColor;


    [Range(0, 1)] public float ShadowThreshold = 0.33f;
    public Color ShadowColor;

    [Range(0, 1)] public float EffectAlpha = 1f;

    protected virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
        material.SetColor("_HighlightColor", HighlightColor);
        material.SetFloat("_HighlightThreshold", HighlightThreshold);

        material.SetColor("_MidColor", MidColor);

        material.SetFloat("_ShadowThreshold", ShadowThreshold);
        material.SetColor("_ShadowColor", ShadowColor);

        material.SetFloat("_Brightness", Brightness);
        material.SetFloat("_Alpha", EffectAlpha);

        Graphics.Blit(source, destination, material);
	}

}