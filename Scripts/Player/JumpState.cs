using Godot;
using System;
using System.Security.Cryptography.X509Certificates;
using static Godot.TextServer;

public partial class JumpState : BaseState
{
    private Player player;
    private float _jumpForce = 30f;
    private float _goingUpGravity = 400f;
    private float _goingDownGravity = 50f;
    private float _airmovementSpeed = 20f;



    public JumpState(Player player) {
        this.player = player;
        
    }
    public override void OnEnterState() {
        GD.Print("Hello, you entered the jump state");
        Vector3 test = player.PlayerBody3D.Velocity;
        test.Y = _jumpForce;
        player.PlayerBody3D.Velocity = test; // cannot modify the struct value directly
        
    }
    public override void Update(double delta) {
    
    }
    public override void PhysicsUpdate(double delta) {

        Vector3 direction = Vector3.Zero;
        if (player.GetFrontValue() == -1 && player.GetBackValue() == 1) { direction.Z = player.GetBackValue(); } // 
        else { direction.Z = player.GetFrontValue() + player.GetBackValue(); }
        if (player.GetRightValue() == 1 && player.GetLeftValue() == -1) { direction.X = player.GetRightValue(); } 
        else { direction.X = player.GetLeftValue() + player.GetRightValue(); }

        if (direction != Vector3.Zero) {
        direction = direction.Normalized();
        }
        Vector3 targetVelocity = direction * _airmovementSpeed;

        //direction
        player.PlayerBody3D.Velocity = new Vector3(
            Mathf.MoveToward(player.PlayerBody3D.Velocity.X, targetVelocity.X, player.GetPlayerAccelerationValue() * (float)delta),
            player.PlayerBody3D.Velocity.Y,
            Mathf.MoveToward(player.PlayerBody3D.Velocity.Z, targetVelocity.Z, player.GetPlayerAccelerationValue() * (float)delta)
        );

        //jump and gravity
        if (!player.PlayerBody3D.IsOnFloor()) {
            float currGravity = player.GetPlayerVeloctiy().Y > 0 ? _goingUpGravity : _goingDownGravity;
            player.PlayerBody3D.Velocity += Vector3.Down * currGravity * (float)delta;
            GD.Print(player.PlayerBody3D.Velocity);
        }

        player.PlayerBody3D.FloorSnapLength = player.PlayerBody3D.IsOnFloor() ? 0.3f : 0f;
        player.PlayerBody3D.MoveAndSlide();

        if (player.PlayerBody3D.IsOnFloor()) {
            player.GetStateMachine().ChangeState("Idle");
        }

    }
    public override void OnExitState() { GD.Print("Leaving the jump state"); }
    public override void HandleInput(InputEvent input) {
       

    }

    
}
