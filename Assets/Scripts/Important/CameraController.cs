using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float _diceOffset = 7.0f;
    [SerializeField] GameObject _dice;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = _dice.transform.position - transform.forward * _diceOffset;
        transform.LookAt(_dice.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = _dice.transform.position - transform.forward * _diceOffset;
        transform.position = _dice.transform.position - transform.forward * _diceOffset;
        if (Input.GetAxis("RotateCamera") != 0)
        {
            Vector3 rotation = new Vector3(Input.GetAxis("RotateCamera") > 0 ? 1 : -1, 0 ,0);
            transform.Translate(rotation * Time.deltaTime * 10.0f);
        }
        transform.LookAt(_dice.transform.position);
    }
}
