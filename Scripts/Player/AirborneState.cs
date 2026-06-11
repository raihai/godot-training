using Godot;
using System;

public partial class AirborneState : BaseState
{
    public Player player;
    private Vector3 _currPlayerVelocity;
	public AirborneState(Player player) {
        this.player = player;
	}

    public override void OnEnterState() {
        GD.Print("Entering Airborne State");

        _currPlayerVelocity = player.PlayerBody3D.Velocity;
    }

    public override void Update(double delta) {

    }

    public override void PhysicsUpdate(double delta) {
        if (!player.PlayerBody3D.IsOnFloor()) {
            _currPlayerVelocity.Y -= player.GetPlayerAccelerationValue() * (float)delta;
            GD.Print("Not on floor");
            GD.Print(_currPlayerVelocity);
            GD.Print(_currPlayerVelocity.Y);
            player.PlayerBody3D.Velocity = _currPlayerVelocity;
            player.PlayerBody3D.MoveAndSlide();
        } else {
            player.GetStateMachine().ChangeState("Idle");
        }
    }

    public override void OnExitState() {
        GD.Print("Leaving the Run state");
        _currPlayerVelocity  = Vector3.Zero;
    }
}
