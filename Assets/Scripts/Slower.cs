using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slower : Enemy
{
    [SerializeField] float speedDown = .5f;
    [SerializeField] float time = 10;
    protected override void PlayerImpact(Player player)
    {
        TankController controller = player.GetComponent<TankController>();
        if (controller != null)
        {
            controller.CurSpeed = controller.CurSpeed * speedDown;
            controller.CurSpeed = Mathf.Clamp(controller.CurSpeed, .1f, controller.MaxSpeed);
            controller.timer = time;
        }
    }
}
