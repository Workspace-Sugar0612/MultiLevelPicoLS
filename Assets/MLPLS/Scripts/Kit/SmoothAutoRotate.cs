using UnityEngine;
using System.Collections;
using Mirror;

public class SmoothAutoRotate : NetworkBehaviour
{
    [Header("旋转设置")]
    [SerializeField] private float rotationSpeed = 30f;
    [SerializeField] private Vector3 rotationAxis = Vector3.up;
    [SerializeField] private bool smoothTransition = true;
    [SerializeField] private float accelerationTime = 1f;

    private float currentSpeed = 0f;
    private Coroutine rotationCoroutine;

    void Start()
    {
        StartRotation();
    }

    [ServerCallback]
    public void StartRotation()
    {
        if (rotationCoroutine != null)
            StopCoroutine(rotationCoroutine);

        rotationCoroutine = StartCoroutine(RotateObject());
    }

    [ServerCallback]
    public void StopRotation()
    {
        if (rotationCoroutine != null)
        {
            StopCoroutine(rotationCoroutine);
            rotationCoroutine = null;
        }
    }

    private IEnumerator RotateObject()
    {
        if (smoothTransition)
        {
            // 平滑加速到目标速度
            float timer = 0f;
            while (timer < accelerationTime)
            {
                currentSpeed = Mathf.Lerp(0, rotationSpeed, timer / accelerationTime);
                timer += Time.deltaTime;
                yield return null;
            }
        }
        else
        {
            currentSpeed = rotationSpeed;
        }

        // 持续旋转
        while (true)
        {
            transform.Rotate(rotationAxis * currentSpeed * Time.deltaTime);
            yield return null;
        }
    }

    void OnDestroy()
    {
        StopRotation();
    }
}