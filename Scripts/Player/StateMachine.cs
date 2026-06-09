using Godot;
using System;
using System.Collections.Generic;

public class StateMachine 
{
    private Dictionary<string, BaseState> _playerStates = new Dictionary<string, BaseState>();
    private BaseState _currentState; 
    public StateMachine() { }

    public void AddState(string StateName, BaseState NewState) {
        _playerStates[StateName] = NewState;
    }

    public void ChangeState(string state) {
        if (_currentState != null) {
            _currentState.OnExitState();
        }
        _currentState = _playerStates.GetValueOrDefault(state);
        if (_currentState != null) {
            _currentState.OnEnterState();
        }
    }

    public void Update(double delta) {
        _currentState.Update(delta);
    }

    public void PhysicsUpdate(double delta) {
        _currentState.PhysicsUpdate(delta);
    }

   
}
  