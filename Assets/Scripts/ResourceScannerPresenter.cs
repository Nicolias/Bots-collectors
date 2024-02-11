using System;
using System.Collections;
using UnityEngine;

public class ResourceScannerPresenter
{
    private readonly ICoroutineServise _coroutineServise;

    private readonly ResourceScannerView _view;
    private readonly float _delay;

    private bool _isEnable;

    public ResourceScannerPresenter(ICoroutineServise coroutineServise, ResourceScannerView view, float delay)
    {
        if(coroutineServise == null)
            throw new ArgumentNullException();

        if (view == null)
            throw new ArgumentNullException();

        if (delay <= 0)
            throw new ArgumentOutOfRangeException();

        _view = view;
        _delay = delay;
    }

    public void Enable()
    {
        _isEnable = true;

        _coroutineServise.StartRoutine(ScaneArea());
    }

    public void Disable()
    {
        _coroutineServise.StopRoutine(ScaneArea());
        _isEnable = false;
    }

    public IEnumerator ScaneArea()
    {
        WaitForSeconds wait = new WaitForSeconds(_delay);

        while (_isEnable)
        {
            _view.ScaneArea();
            yield return wait;
        }
    }
}