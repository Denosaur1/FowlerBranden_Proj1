using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    [SerializeField] float _maxSpeed = .25f;
    [SerializeField] float _curSpeed = .25f;
    [SerializeField] float _turnSpeed = 2f;
    public float timer = 0;

    public float MaxSpeed
    {
        get => _maxSpeed; 
        set => _maxSpeed = value; 
    } public float CurSpeed
    {
        get => _curSpeed; 
        set => _curSpeed = value; 
    }
    Rigidbody _rb = null;
    private void Start()
    {
        _curSpeed = _maxSpeed;
    }
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if(timer > 0) { timer--; }
        else { _curSpeed = _maxSpeed; }
        MoveTank();
        TurnTank();
    }

    public void MoveTank()
    {
        // calculate the move amount
        float moveAmountThisFrame = Input.GetAxis("Vertical") * _curSpeed;
        // create a vector from amount and direction
        Vector3 moveOffset = transform.forward * moveAmountThisFrame;
        // apply vector to the rigidbody
        _rb.MovePosition(_rb.position + moveOffset);
        // technically adjusting vector is more accurate! (but more complex)
    }

    public void TurnTank()
    {
        // calculate the turn amount
        float turnAmountThisFrame = Input.GetAxis("Horizontal") * _turnSpeed;
        // create a Quaternion from amount and direction (x,y,z)
        Quaternion turnOffset = Quaternion.Euler(0, turnAmountThisFrame, 0);
        // apply quaternion to the rigidbody
        _rb.MoveRotation(_rb.rotation * turnOffset);
    }
}
