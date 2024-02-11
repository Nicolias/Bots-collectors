using System.Collections;

public interface ICoroutineServise
{
    public void StartRoutine(IEnumerator routine);

    public void StopRoutine(IEnumerator routine);
}