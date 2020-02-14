using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    [SerializeField] private AnimationCurve moveCurve = AnimationCurve.EaseInOut(0.0f, 0.0f, 1.0f, 1.0f);

    public void DoMovement(Transform target, bool toPosition = true, bool toRotation = true)
    {
        if (_activeRoutine != null)
            StopCoroutine(_activeRoutine);
        _activeRoutine = StartCoroutine(MoveToAnimation(target, toPosition, toRotation));
    }

    private Coroutine _activeRoutine;

    private IEnumerator MoveToAnimation(Transform target, bool toPosition, bool toRotation)
    {
        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;
        float animLength = moveCurve.keys[moveCurve.length - 1].time;

        for (float timer = 0.0f; timer <= animLength; timer += Time.deltaTime)
        {
            yield return null;
            float tValue = moveCurve.Evaluate(timer);

            if(toPosition)
                transform.position = Vector3.Lerp(startPos, target.position, tValue);
            if(toRotation)
                transform.rotation = Quaternion.Slerp(startRot, target.rotation, tValue);
        }

        if(toPosition)
            transform.position = target.position;
        if(toRotation)
            transform.rotation = target.rotation;

        _activeRoutine = null;
    }
}
