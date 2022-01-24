using System;
using System.Collections.Generic;

public class StateMachine
{
    private class Transition
    {
        public Func<bool> condition { get; }

        public IState to { get; }

        public Transition(IState to, Func<Boolean> condition)
        {
            this.to = to;
            this.condition = condition;
        }
    }

    IState currentState;
    Dictionary<Type, List<Transition>> transitions = new Dictionary<Type, List<Transition>>();
    List<Transition> currentTransitions = new List<Transition>();
    List<Transition> anyTransitions = new List<Transition>();
    static List<Transition> emptyTransitions = new List<Transition>();

    public void Tick()
    {
        Transition transition = GetTransition();

        if (transition != null) SetState(transition.to);

        currentState?.Tick();
    }

    public void SetState(IState state)
    {
        if (state == currentState) return;

        currentState?.OnExit();
        currentState = state;

        transitions.TryGetValue(currentState.GetType(), out currentTransitions);

        if (currentTransitions == null) currentTransitions = emptyTransitions;

        currentState.OnEnter();
    }

    public void AddTransition(IState from, IState to, Func<bool> predicate)
    {
        if (transitions.TryGetValue(from.GetType(), out var _transitions) == false)
        {
            _transitions = new List<Transition>();
            transitions[from.GetType()] = _transitions;

            _transitions.Add(new Transition(to, predicate));
        }
    }

    public void AddAnyTransition(IState state, Func<bool> predicate)
    {
        anyTransitions.Add(new Transition(state, predicate));
    }

    Transition GetTransition()
    {
        foreach (var item in anyTransitions)
        {
            if (item.condition()) return item; 
        }

        foreach (var item in currentTransitions)
        {
            if (item.condition()) return item;
        }

        return null;
    }
}

