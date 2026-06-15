using Godot;
using System;

public partial class AirborneState : BaseState
{
    public Player player;
    private Vector3 _currPlayerVelocity;
    //private float _airborneSpeed = 30;
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

        //direction
        Vector3 direction = Vector3.Zero;
        if (player.GetFrontValue() == -1 && player.GetBackValue() == 1) { direction.Z = player.GetBackValue(); } // 
        else { direction.Z = player.GetFrontValue() + player.GetBackValue(); }

        if (player.GetRightValue() == 1 && player.GetLeftValue() == -1) { direction.X = player.GetRightValue(); } else { direction.X = player.GetLeftValue() + player.GetRightValue(); }
        if (direction != Vector3.Zero) {
            direction = direction.Normalized();
        }

        //velocity
        //_currPlayerVelocity.X = direction.X * _airborneSpeed;
        //_currPlayerVelocity.Z = direction.Z * _airborneSpeed;
        _currPlayerVelocity.Y -= player.GetPlayerAccelerationValue() * (float)delta;

        player.PlayerBody3D.Velocity = _currPlayerVelocity;
       
        GD.Print(_currPlayerVelocity);
        player.PlayerBody3D.MoveAndSlide();
        
        if (player.PlayerBody3D.IsOnFloor()) {
            player.GetStateMachine().ChangeState("Idle");
        }
    }

    public override void OnExitState() {
        GD.Print("Leaving airborne state");
        _currPlayerVelocity  = Vector3.Zero;
    }
}
