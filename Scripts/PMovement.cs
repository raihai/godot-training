using Godot;
using System;

public partial class PMovement : Node
{
	[Export] float speed = 15f;
	private Vector3 _velocity = Vector3.Zero;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{


	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        Vector3 direction = Vector3.Zero; // direction vector

        if (Input.IsActionPressed("move_right")) direction.X += 1.0f;
        if (Input.IsActionPressed("move_left")) direction.X -= 1.0f;
        if (Input.IsActionPressed("move_back")) direction.Z += 1.0f;
        if (Input.IsActionPressed("move_forward")) direction.Z -= 1.0f;

		if(direction != Vector3.Zero) {
			direction = direction.Normalized();
		}

		_velocity = direction * speed * (float)delta;
		
		
		

    }
}
