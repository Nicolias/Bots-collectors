using System.Collections;
using UnityEngine;

public class CoroutineServise : MonoBehaviour, ICoroutineServise
{
    public void StartRoutine(IEnumerator routine)
    {
        StartCoroutine(routine);
    }

    public void StopRoutine(IEnumerator routine)
    {
        StopCoroutine(routine);
    }
}
