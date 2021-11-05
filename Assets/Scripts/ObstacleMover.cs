using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObstacleMover : MonoBehaviour
{
    [SerializeField] private Transform _endPosition;
    [SerializeField] private float _speed = 2f;

    private void Start()
    {
        transform.DOMove(_endPosition.position, _speed).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }
}
