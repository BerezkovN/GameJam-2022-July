using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject _camera;
    [SerializeField] private float _rollSpeed = 5;

    private float _multipler;

    private bool _isMoving;

    private bool _cancel = false;

    public event System.Action WinEvent;


    private void Start()
    {
        _multipler = GetComponent<Collider>().bounds.size.x / 2;

        WinEvent += () => Debug.Log("Win!!!");
    }

    private void Update()
    {
        if (_isMoving) return;

        Vector3[] states = new Vector3[4]
        {
            Vector3.forward,
            Vector3.right,
            Vector3.back,
            Vector3.left
        };

        int state = -1;

        if (Input.GetKeyDown(KeyCode.W)) state = 0;
        else if (Input.GetKeyDown(KeyCode.D)) state = 1;
        else if (Input.GetKeyDown(KeyCode.S)) state = 2;
        else if (Input.GetKeyDown(KeyCode.A)) state = 3;
        if (state != -1)
        {
            state += Mathf.RoundToInt(0.5f + (_camera.transform.rotation.eulerAngles.y - 45.0f) / 90);
            Assemble(states[state % 4]);
        }
        

        void Assemble(Vector3 dir)
        {
            print(GetComponent<Collider>().bounds.size);
            //print(GetComponent<meshr>().bounds.size);
            var anchor = transform.position + (Vector3.down + dir) * _multipler;
            var axis = Vector3.Cross(Vector3.up, dir);
            StartCoroutine(Roll(anchor, axis, 90));
        }
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


    // Cancels current movement
    public void CancelMovement()
    {
        _cancel = true;
    }
}