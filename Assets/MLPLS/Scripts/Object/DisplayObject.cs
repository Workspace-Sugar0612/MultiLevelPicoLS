using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayObject : ManagedObject
{
    [Range(0.0f, 1.0f)]
    [SerializeField]
    private float alpha;

    public float Alpha
    {
        get => alpha;
        set 
        {
            alpha = value;
            renderer.material.SetFloat("_AlphaScale", alpha);
        }
    }

    private new Renderer renderer;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }

    public void SetAlphaTransition(float targetAlpha, float duration)
    {
        StartCoroutine(AlphaTransitionCoroutine(targetAlpha, duration));
    }

    private IEnumerator AlphaTransitionCoroutine(float targetAlpha, float duration)
    {
        yield return new WaitForSeconds(0.1f);
        float startAlpha = alpha;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration);
            renderer.material.SetFloat("_AlphaScale", alpha);
            yield return null;
        }
        alpha = targetAlpha;
        renderer.material.SetFloat("_AlphaScale", alpha);
    }
}
