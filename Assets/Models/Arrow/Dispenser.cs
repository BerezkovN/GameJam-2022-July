using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispenser : MonoBehaviour
{
    [SerializeField] private GameObject _arrowsModel;
    [SerializeField] private float _arrowSpeed = 2.5f;
    [SerializeField] private float _timeToReset = 10.0f;
    [SerializeField] private float _timeToResetOnCollision = 3.0f;
    private Arrows? _arrows = null;
    private float _arrowInitializationTime = 0.0f;
    
    public float ArrowSpeed { get { return _arrowSpeed; } }


    void ResetArrows()
    {
        if(_arrows != null)
        {
            Destroy(_arrows.gameObject);
        }

        _arrows = GameObject.Instantiate(_arrowsModel).GetComponent<Arrows>();
        _arrows.SetDispenserReference(this);
        _arrowInitializationTime = Time.time;
    }

    void Start()
    {
        ResetArrows();
    }

    void Update()
    {
        if((!_arrows.Moving && Time.time - _arrows.CollisionTime > _timeToResetOnCollision) || (Time.time - _arrowInitializationTime > _timeToReset))
        {
            ResetArrows();
        }
    }
}
