using Godot;
using System;
using static Godot.TextServer;

public partial class Player : Node
{
	[Export] public CharacterBody3D PlayerBody3D { set; get; } 
	private Vector3 _playerVelocity {  get; set; } = Vector3.Zero;
	private float _moveFrontValue { get; set; }
	private float _moveBackValue {  get; set; }
	private float _moveLeftValue {  get; set; }
	private float _moveRightValue { get; set; }
    private float _moveSpeed { get; set; } = 15f;
    private float _acceleration { get; set; } = 75f;

    private StateMachine stateMachine { get; set; } = new StateMachine();

	private IdleState _idleState;
	private RunState _runState;


	public override void _Ready()
	{
		_moveFrontValue = 0f;
		_moveBackValue = 0f;
		_moveLeftValue = 0f;
        _moveRightValue = 0f;

		_idleState = new IdleState(this);
		_runState = new RunState(this);

        stateMachine.AddState("Idle", _idleState);
        stateMachine.AddState("Run", _runState); 
		stateMachine.ChangeState("Idle");
	}
	
	public override void _Process(double delta)
	{
        _moveFrontValue = Input.IsActionPressed("move_forward") ? -1.0f : 0.0f;
        _moveBackValue = Input.IsActionPressed("move_back") ? 1.0f : 0.0f;
        _moveLeftValue = Input.IsActionPressed("move_left")? -1.0f : 0.0f;
        _moveRightValue = Input.IsActionPressed("move_right") ? 1.0f : 0.0f;

		stateMachine.Update(delta);
	}

    public override void _PhysicsProcess(double delta) {
        stateMachine.PhysicsUpdate(delta);
    }

	public float GetFrontValue() {
		return _moveFrontValue;
	}

	public float GetBackValue() {
		return _moveBackValue;
	}

	public float GetLeftValue() {
		return _moveLeftValue;
	}
	public float GetRightValue() {
	return _moveRightValue;
	}
	public Vector3 GetPlayerVeloctiy() {
		return _playerVelocity;
	}

	public float GetPlayerSpeed() { 
		return _moveSpeed;
	}

	public StateMachine GetStateMachine() {
		return stateMachine;
	}

	public float GetAccelerationValue() {
		return _acceleration;
	}

}
                          