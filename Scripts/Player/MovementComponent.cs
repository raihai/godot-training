using Godot;
using System;

public partial class MovementComponent : Node
{
    //** Handle movement physics ***
    [Export] public CharacterBody3D player { set; get; } // set player
    
    
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        Vector3 direction = Vector3.Zero;
        //// Ground velocity
        //_playerVelocity.X = direction.X * _speed; // 1
        //_playerVelocity.Z = direction.Z * _speed; // 2

        //// Gravity
        //if (!player.IsOnFloor()) {
        //    _playerVelocity.Y -= _acceleration * (float)delta;
        //}

        //player.Velocity = _playerVelocity;
        //player.MoveAndSlide();
    }
}
