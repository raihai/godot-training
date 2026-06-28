using Godot;
using System;
using System.Data.SqlTypes;

public partial class RunState : BaseState
{
    public Player player;
    private Vector3 _currPlayerVelocity;
    private float _currentHorizontalSpeed = 0;
    private Vector3 _prevDirection;

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
        if (player.GetFrontValue() == -1 && player.GetBackValue() == 1) { direction.Z = player.GetBackValue();} // 
        else { direction.Z = player.GetFrontValue() + player.GetBackValue();}

        // get direction
        if (player.GetRightValue() == 1 && player.GetLeftValue() == -1) { direction.X = player.GetRightValue();} 
        else { direction.X = player.GetLeftValue() + player.GetRightValue();}
        if (direction != Vector3.Zero) {
            direction = direction.Normalized();
        }
        // velocity movement
        float acceleration = player.GetAcceleration ();
        _currentHorizontalSpeed = Mathf.MoveToward(_currentHorizontalSpeed, player.GetPlayerMoveForce(), acceleration *(float)delta); // speedup
        _currPlayerVelocity.X = direction.X * _currentHorizontalSpeed; 
        _currPlayerVelocity.Z = direction.Z * _currentHorizontalSpeed;

        GD.Print(_currPlayerVelocity);

        if (!player.PlayerBody3D.IsOnFloor()) {
            player.GetStateMachine().ChangeState("Airborne");
        } else {
            _currPlayerVelocity.Y = 0;
        }
        player.PlayerBody3D.FloorSnapLength = player.PlayerBody3D.IsOnFloor() ? 0.3f : 0f;
        player.PlayerBody3D.Velocity = _currPlayerVelocity;
        player.PlayerBody3D.MoveAndSlide();

        if (player.PlayerBody3D.Velocity == Vector3.Zero) {
            player.GetStateMachine().ChangeState("Idle");
        }
    }
    public override void OnExitState() {
        GD.Print("Leaving the Run state");
        //player.PlayerBody3D.Velocity = Vector3.Zero;
        _currentHorizontalSpeed = 0f; // reset the accumalated velocity
    }

    public override void HandleInput(InputEvent input) {
        if (input.IsActionPressed("move_jump") && player.PlayerBody3D.IsOnFloor()) {
            GD.Print("Jumping input from running state");
            player.GetStateMachine().ChangeState("Jump");
        }
    }
    
}
