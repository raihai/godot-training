using Godot;
using System;
using System.ComponentModel;

public partial class BasicMovement : CharacterBody3D
{
	[Export] public int Speed { get; set; } = 14;
	[Export] public float FallAcceleration { get; set; } = 75;
    private Vector3 _targetVelocity = Vector3.Zero;
    public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        Vector3 direction = Vector3.Zero;

        if (Input.IsActionPressed("move_right")) direction.X += 1.0f;
        if (Input.IsActionPressed("move_left")) direction.X -= 1.0f;
        if (Input.IsActionPressed("move_back")) direction.Z += 1.0f;
        if (Input.IsActionPressed("move_forward")) direction.Z -= 1.0f;

        if (direction != Vector3.Zero) {
            direction = direction.Normalized();
            // Optional: Rotate player to face direction
            // GetNode<Node3D>("Pivot").Basis = Basis.LookingAt(direction);
        }

        // Ground velocity
        _targetVelocity.X = direction.X * Speed;
        _targetVelocity.Z = direction.Z * Speed;

        // Gravity
        if (!IsOnFloor()) {
            _targetVelocity.Y -= FallAcceleration * (float)delta;
        }

        Velocity = _targetVelocity;
        MoveAndSlide();
    }
}
