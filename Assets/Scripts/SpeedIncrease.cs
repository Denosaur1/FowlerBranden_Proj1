using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedIncrease : Collectible
{
    [SerializeField] float speedAmount;
    protected override void Collect(Player player)
    {
        TankController controller = player.GetComponent<TankController>();
        if (controller != null) {
            controller.MaxSpeed += speedAmount;
            controller.CurSpeed = controller.MaxSpeed;
        }

    }
    protected override void Movement(Rigidbody rb)
    {
        Quaternion turnOffset = Quaternion.Euler(MovementSpeed, MovementSpeed, MovementSpeed);
        rb.MoveRotation(rb.rotation * turnOffset);
    }
}
