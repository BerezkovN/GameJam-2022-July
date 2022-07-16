using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable enable

public class Arrows : MonoBehaviour
{
    [SerializeField] private GameObject _arrow;
    private Dispenser? _dispenser = null;
    private float _speed;
    

    public bool Moving { get; private set; } = true;
    public float CollisionTime { get; private set; } = float.MaxValue;


    public void SetDispenserReference(Dispenser dispenser)
    {
        _dispenser = dispenser;
        transform.rotation = _dispenser.transform.rotation;
        transform.position = _dispenser.transform.position;
        _speed = _dispenser.ArrowSpeed;
    }

    void Start()
    {
        for (int i = 0; i < 100; i++)
        {
            GameObject go = GameObject.Instantiate(_arrow);
            go.transform.parent = transform;
            go.transform.localPosition = new Vector3(Random.value / 1.5f - 1.0f/3, Random.value / 1.5f - 1.0f / 3, Random.value / 4 - 0.125f);
            go.transform.rotation *= transform.rotation;
        }
    }

    
    void Update()
    {
        if (Time.time - CollisionTime < 1.0f / 8.0f / _speed )
        {
            transform.position += (transform.forward * _speed * Time.deltaTime);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Moving = false;
        CollisionTime = Time.time;
    }
}
