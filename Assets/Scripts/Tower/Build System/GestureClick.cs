using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerBuildSystem
{
    public class GestureClick : MonoBehaviour, IPointerClickHandler
    {
        public event Action<Vector3> Click;

        public void OnPointerClick(PointerEventData eventData)
        {
            Click?.Invoke(eventData.pointerCurrentRaycast.worldPosition);
        }
    }
}