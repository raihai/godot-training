using Godot;
using System;

public partial class RunState : BaseState
{
    public Player player;
    private Vector3 _currPlayerVelocity; 

    public RunState(Player player) {
        this.player = player;
    }
    public override void OnEnterState() {
        this._currPlayerVelocity = player.GetPlayerVeloctiy();

        GD.Print("Hello, you entered the Run state");
    }
    public override void Update(double delta) {
    
    }
    public override void PhysicsUpdate(double delta) {
        Vector3 direction = Vector3.Zero;

        if (player.GetFrontValue() == -1 && player.GetBackValue() == 1) {
            direction.Z = player.GetBackValue();
        } else {
            direction.Z = player.GetFrontValue() + player.GetBackValue();
        }

        if (player.GetRightValue() == 1 && player.GetLeftValue() == -1) {
            direction.X = player.GetRightValue();
        } else {
            direction.X = player.GetLeftValue() + player.GetRightValue(); 
        }

        if (direction != Vector3.Zero) {
            direction = direction.Normalized();
            GD.Print(direction);
        }

            _currPlayerVelocity.X = direction.X * player.GetPlayerSpeed();
        _currPlayerVelocity.Z = direction.Z * player.GetPlayerSpeed();

        player.PlayerBody3D.Velocity = _currPlayerVelocity;
        player.PlayerBody3D.MoveAndSlide();

        if (!player.PlayerBody3D.IsOnFloor()) {
            _currPlayerVelocity.Y -= player.GetAccelerationValue() * (float)delta;
        } else {
            _currPlayerVelocity.Y = 0; 
        }

        if (player != null) {
            if (player.GetFrontValue() == 0 && player.GetBackValue() == 0 && player.GetLeftValue() == 0 && player.GetRightValue() == 0) {
                player.GetStateMachine().ChangeState("Idle");
            }
        }
    }
    public override void OnExitState() {
        GD.Print("Leaving the Run state");
    }
}
