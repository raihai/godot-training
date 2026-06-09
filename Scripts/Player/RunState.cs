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
    }
    public override void Update(double delta) {
    
    }
    public override void PhysicsUpdate(double delta) {
        Vector3 direction = Vector3.Zero;

        direction.X = player.GetFrontBackValue();
        direction.Y = player.GetSideValue();
        if (direction != Vector3.Zero) {
            direction.Normalized();
        }

        _currPlayerVelocity.X = direction.X * player.GetPlayerSpeed();
        _currPlayerVelocity.Z = direction.Z * player.GetPlayerSpeed();

        if (!player.PlayerBody3D.IsOnFloor()) {
            //_playerVelocity.Y -= _acceleration * (float)delta;
        }

        player.PlayerBody3D.Velocity = _currPlayerVelocity;
        player.PlayerBody3D.MoveAndSlide();

        if (player != null) {
            if (player.GetFrontBackValue() == 0 && player.GetSideValue() == 0) {
                player.GetStateMachine().ChangeState("Idle");
            }
        }
    }
    public override void OnExitState() { 
        
    }
}
