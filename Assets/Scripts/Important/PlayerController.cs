using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject _camera;
    [SerializeField] private float _rollSpeed = 5;
    [SerializeField] private AudioSource _wallHit;
    [SerializeField] private AudioSource _diceRoll;

    private float _multipler;

    private bool _isMoving;

    private bool _cancel = false;

    public event System.Action WinEvent;

    struct SavedTransform {
        public Quaternion rotation;
        public Vector3 position;
    }

    private SavedTransform _saved;
    private bool _terminateCoroutine = false;

    public void ResetDice()
    {
        transform.rotation = _saved.rotation;
        transform.position = _saved.position;
        _terminateCoroutine = true;
        _isMoving = false;
    }


    private void Start()
    {
        _saved.rotation = transform.rotation;
        _saved.position = transform.position;

        _multipler = GetComponent<Collider>().bounds.size.x / 2;

        if (_camera == null)
            _camera = Camera.main.gameObject;

        WinEvent += () => Debug.Log("Win!!!");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            ResetDice();
            return;
        }
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
            var anchor = transform.position + (Vector3.down + dir) * _multipler;
            var axis = Vector3.Cross(Vector3.up, dir);
            _terminateCoroutine = false;
            _diceRoll.pitch = Random.Range(0.8f, 1.2f);
            _diceRoll.Play();
            StartCoroutine(Roll(anchor, axis, 1.0f / _rollSpeed));
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

        while (startRotation <= endRotation && !_terminateCoroutine)
        {
            if (_cancel)
            {
                _cancel = false;

                var cameraShake = Camera.current.GetComponent<CameraShake>();
                if (cameraShake != null)
                {
                    cameraShake.start = true;
                }
                _wallHit.pitch = Random.RandomRange(0.5f, 1.5f);

                _wallHit.Play();
                StartCoroutine(Roll(anchor, -axis, startRotation));
                yield break;
            }

            transform.RotateAround(anchor, axis, 90 * _rollSpeed * Mathf.Min(Time.deltaTime, endRotation - startRotation));
            startRotation += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        _isMoving = false;
    }

    public void CancelMovement()
    {
        _cancel = true;
    }
}