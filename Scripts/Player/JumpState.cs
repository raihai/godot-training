using Godot;
using System;
using System.Security.Cryptography.X509Certificates;
using static Godot.TextServer;

public partial class JumpState : BaseState
{
    private Player player;
    private float _jumpForce;
    private float _goingUpGravity = 100f;
    private float _goingDownGravity = 45f;
    private float _airmovementSpeed = 15f;

    private float _desiredJumpHeight = 5f;

    public JumpState(Player player) {
        this.player = player;
        _jumpForce = MathF.Sqrt(2 * _goingUpGravity * _desiredJumpHeight); // force need to get desired height 

    }
    public override void OnEnterState() {

        GD.Print("Hello, you entered the jump state");

        GD.Print($"Jump Force: {_jumpForce}, Height: {_desiredJumpHeight}");
        Vector3 currVelocity = player.PlayerBody3D.Velocity;
        currVelocity.Y = _jumpForce;
        player.PlayerBody3D.Velocity = currVelocity; // setting initial jump force 

       
    }
    public override void Update(double delta) {
        
    }
    public override void PhysicsUpdate(double delta) {

        Vector2 input = Input.GetVector("move_left", "move_right", "move_forward", "move_back");
        Vector3 direction = new Vector3(input.X, 0, input.Y).Normalized();

        if (direction != Vector3.Zero) {
            direction = direction.Normalized();
        }

        //position move
        Vector3 targetVelocity = direction * _airmovementSpeed;

        player.PlayerBody3D.Velocity = new Vector3(Mathf.MoveToward(player.PlayerBody3D.Velocity.X, targetVelocity.X, player.GetAcceleration() * (float)delta),
            player.PlayerBody3D.Velocity.Y,
            Mathf.MoveToward(player.PlayerBody3D.Velocity.Z, targetVelocity.Z, player.GetAcceleration() * (float)delta));

        //velocity
        float currGravity = player.GetPlayerVeloctiy().Y > 0 ? _goingUpGravity : _goingDownGravity;
        player.PlayerBody3D.Velocity += Vector3.Down * currGravity * (float)delta;
   
        player.PlayerBody3D.FloorSnapLength = player.PlayerBody3D.IsOnFloor() ? 0.3f : 0f;
        player.PlayerBody3D.MoveAndSlide();

        if (player.PlayerBody3D.IsOnFloor() && player.PlayerBody3D.Velocity.Y >= 0) {
            player.GetStateMachine().ChangeState("Idle");
        }

    }
    public override void OnExitState() {
        
        GD.Print("Leaving the jump state"); 
    }
    public override void HandleInput(InputEvent input) {
       

    }

    
}
