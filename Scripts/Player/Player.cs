using Godot;
using System;
using static Godot.TextServer;

public partial class Player : Node
{
	[Export] public CharacterBody3D PlayerBody3D { set; get; }
	private Vector3 _playerVelocity {  get; set; }
	private float _moveFrontBackValue { get; set; }
	private float _moveSideValue { get; set; }



	private StateMachine stateMachine { get; set; }
	private IdleState _idleState;
	private RunState _runState;

	private float _moveSpeed { get; set; }

	public override void _Ready()
	{
		_moveFrontBackValue = 0f;
		_moveSideValue = 0f;
		_moveSpeed = 15f;

		_playerVelocity = Vector3.Zero;
		stateMachine = new StateMachine();

		_idleState = new IdleState(this);
		_runState = new RunState(this);

        stateMachine.AddState("Idle", _idleState); // add state 
        stateMachine.AddState("Run", _runState); // add state
		stateMachine.ChangeState("Idle");

	}
	
	public override void _Process(double delta)
	{
        if (Input.IsActionPressed("move_right")) _moveFrontBackValue += 1.0f;
        if (Input.IsActionPressed("move_left")) _moveFrontBackValue -= 1.0f;
        if (Input.IsActionPressed("move_back")) _moveSideValue += 1.0f;
        if (Input.IsActionPressed("move_forward")) _moveSideValue -= 1.0f;

		stateMachine.Update(delta);
	}

    public override void _PhysicsProcess(double delta) {
        stateMachine.PhysicsUpdate(delta);
    }

	public float GetFrontBackValue() {
		return _moveFrontBackValue;
	}

	public float GetSideValue() {
		return _moveSideValue;
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
}
                          