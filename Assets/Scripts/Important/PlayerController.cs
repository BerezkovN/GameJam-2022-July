using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //======== Public stuff ========//
    public event System.Action WinEvent;

    // Cancels current movement
    public void CancelMovement()
    {
        _cancel = true;
    }

    //======== Private stuff ========//
    [SerializeField]
    private float _rollSpeed = 5;

    private bool _isMoving;

    private bool _cancel = false;

    private void Start()
    {
        WinEvent += () => Debug.Log("Win!!!");
    }

    private void Update()
    {
        if (_isMoving) return;

        if (Input.GetKeyDown(KeyCode.A)) Assemble(Vector3.left);
        else if (Input.GetKeyDown(KeyCode.D)) Assemble(Vector3.right);
        else if (Input.GetKeyDown(KeyCode.W)) Assemble(Vector3.forward);
        else if (Input.GetKeyDown(KeyCode.S)) Assemble(Vector3.back);

        void Assemble(Vector3 dir)
        {
            var anchor = transform.position + (Vector3.down + dir) * 0.5f;
            var axis = Vector3.Cross(Vector3.up, dir);
            StartCoroutine(Roll(anchor, axis, 90));
        }
    }

    private IEnumerator Roll(Vector3 anchor, Vector3 axis, float endRotation)
    {
        _isMoving = true;

        float startRotation = 0;

        while (startRotation != endRotation)
        {
            if (_cancel)
            {
                _cancel = false;

                var cameraShake = Camera.current.GetComponent<CameraShake>();
                if (cameraShake != null)
                {
                    cameraShake.start = true;
                }

                StartCoroutine(Roll(anchor, -axis, startRotation));
                yield break;
            }

            startRotation += _rollSpeed;
            transform.RotateAround(anchor, axis, _rollSpeed);
            yield return new WaitForEndOfFrame();
        }

        _isMoving = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            CancelMovement();
            return;
        }

        if (collision.gameObject.CompareTag("WinPod"))
        {
            int dice = DiceUtility.WhichIsUp(transform);
            int pod = DiceUtility.WhichIsUp(collision.transform);

            if (dice == pod) 
            { 
                WinEvent?.Invoke();
            }
        }
    }
}