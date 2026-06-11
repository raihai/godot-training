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

        //direction
        Vector3 direction = Vector3.Zero;
        if (player.GetFrontValue() == -1 && player.GetBackValue() == 1) { direction.Z = player.GetBackValue();} // 
        else { direction.Z = player.GetFrontValue() + player.GetBackValue();}

        if (player.GetRightValue() == 1 && player.GetLeftValue() == -1) { direction.X = player.GetRightValue();} 
        else { direction.X = player.GetLeftValue() + player.GetRightValue();}
        if (direction != Vector3.Zero) {
            direction = direction.Normalized();

        }
        // velocity movement
        /// float targetSpeed = direction.Length() * player.GetPlayerMoveForce();
        float acceleration = player.GetPlayerAccelerationValue(); // get which friction to use 

        //if (player.GetFrontValue() == -1 && player.GetBackValue() == 1) { 
           // _currentHorizontalSpeed = Mathf.MoveToward(_currentHorizontalSpeed, 0, 15 *(float)delta); // slow down 
          //  GD.Print()
       // } else { 
        _currentHorizontalSpeed = Mathf.MoveToward(_currentHorizontalSpeed, player.GetPlayerMoveForce(), acceleration *(float)delta); // speedup to max

        // if player is already moving and the the opposited direction is hit we decelrate until the direction vector is same as the input vector and speed up
       // GD.Print(acceleration);


        _currPlayerVelocity.X = direction.X * _currentHorizontalSpeed; 
        _currPlayerVelocity.Z = direction.Z * _currentHorizontalSpeed;
        GD.Print(_currPlayerVelocity.X);
        GD.Print(_currPlayerVelocity.Z);

        player.PlayerBody3D.Velocity = _currPlayerVelocity;
        player.PlayerBody3D.MoveAndSlide();

        if (!player.PlayerBody3D.IsOnFloor()) {
            _currPlayerVelocity.Y -= player.GetPlayerAccelerationValue() * (float)delta;
        } else {
            _currPlayerVelocity.Y = 0; 
        }

        _prevDirection = direction;

        if (player != null) {
            if (player.GetFrontValue() == 0 && player.GetBackValue() == 0 && player.GetLeftValue() == 0 && player.GetRightValue() == 0) {
                player.GetStateMachine().ChangeState("Idle");
            }
        }
    }
    public override void OnExitState() {
        GD.Print("Leaving the Run state");
        _currentHorizontalSpeed = 0f; // reset the accumalated velocity
    }
}
