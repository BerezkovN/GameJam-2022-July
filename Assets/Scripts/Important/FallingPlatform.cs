using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FallingPlatform : MonoBehaviour
{
    public bool IsFallen = false;
    public float SecondsToDie = 1;

    [SerializeField]
    private float secondsToRestore = 3;
    [SerializeField]
    private float secondsToFall;

	private Rigidbody _rigidBody;
    private Animator _animator;

    private Transform _childTransform;

    private SavedTransform _savedTransform;

    struct SavedTransform
    {
        public Quaternion rotation;
        public Vector3 position;
    }

    public void InitStep()
    {
        if (IsFallen)
        {
            return;
        }

        if (_animator != null)
        {
            _animator.enabled = true;
        }

        StartCoroutine(DelayedFall());
    }

    IEnumerator DelayedFall()
    {
        yield return new WaitForSeconds(secondsToFall);

        if(_animator != null)
        {
            _animator.enabled = false;
        }

        IsFallen = true;

        _rigidBody.isKinematic = false;

        StartCoroutine(Restore());
    }

    IEnumerator Restore()
    {
        yield return new WaitForSeconds(secondsToRestore);

        IsFallen = false;

        _rigidBody.isKinematic = true;
        _childTransform.position = _savedTransform.position;
        _childTransform.rotation = _savedTransform.rotation;
    }

    void Start()
    {
		_rigidBody = gameObject.GetComponentInChildren<Rigidbody>();
        _animator = gameObject.GetComponentInChildren<Animator>();

        _childTransform = transform.GetChild(0);

        _savedTransform = new SavedTransform();
        _savedTransform.position = _childTransform.position;
        _savedTransform.rotation = _childTransform.rotation;
    }
    
}
