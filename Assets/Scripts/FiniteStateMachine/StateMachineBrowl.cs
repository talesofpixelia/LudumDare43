﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Reflection;

public class StateMachine : MonoBehaviour
{

    class State
    {
        public Enum name = null;

        public Action updateMethod = () => { };
        public Action<Enum> enterMethod = (state) => { };
        public Action<Enum> exitMethod = (state) => { };
        public bool forceTransition = false;
        public List<Enum> transitions = null;

        public State(Enum name)
        {
            this.name = name;
            this.transitions = new List<Enum>();
        }
    }

    #region Variables

    private Dictionary<Enum, State> states;

    private State currentState = null;
    private bool inTransition = false;
    private bool initialized = false;
    private bool debugMode = false;

    private Action OnUpdate = null;

    public Enum CurrentState { get { return this.currentState.name; } }

    #endregion

    #region Unity lifecycle

    protected virtual void Update()
    {
        this.OnUpdate();
    }

    #endregion

    private bool Initialized()
    {
        if (!initialized)
        {
            Debug.LogError(this.GetType().ToString() + ": StateMachine is not initialized. You need to call InitializeStateMachine( bool debug, bool allowMultiTransition = false )");
            return false;
        }
        return true;
    }

    private static T GetMethodInfo<T>(object obj, Type type, string method, T Default) where T : class
    {
        Type baseType = type;
        MethodInfo methodInfo = baseType.GetMethod(method, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        if (methodInfo != null)
        {
            return Delegate.CreateDelegate(typeof(T), obj, methodInfo) as T;
        }
        return Default;
    }

    protected void InitializeStateMachine<T>(Enum initialState, bool debug)
    {
        if (this.initialized == true)
        {
            Debug.LogError("The StateMachine component on " + this.GetType().ToString() + " is already initialized.");
            return;
        }
        this.initialized = true;

        var values = Enum.GetValues(typeof(T));
        this.states = new Dictionary<Enum, State>();
        for (int i = 0; i < values.Length; i++)
        {
            this.initialized = this.CreateNewState((Enum)values.GetValue(i));
        }
        this.currentState = this.states[initialState];
        this.inTransition = false;
        this.debugMode = debug;

        this.currentState.enterMethod(currentState.name);
        this.OnUpdate = this.currentState.updateMethod;
        if (this.debugMode == true)
        {
            Debug.Log("StateMachine : " + this.GetType().ToString() + " initialized with " + this.currentState.name + " state.");
        }
    }

    private bool CreateNewState(Enum newstate)
    {
        if (this.Initialized() == false) { return false; }
        if (this.states.ContainsKey(newstate) == true)
        {
            Debug.Log("State " + newstate + " is already registered in " + this.GetType().ToString());
            return false;
        }
        State s = new State(newstate);
        Type type = this.GetType();
        s.enterMethod = StateMachine.GetMethodInfo<Action<Enum>>(this, type, "Enter" + newstate, DoNothingEnterExit);
        s.updateMethod = StateMachine.GetMethodInfo<Action>(this, type, "Update" + newstate, DoNothingUpdate);
        s.exitMethod = StateMachine.GetMethodInfo<Action<Enum>>(this, type, "Exit" + newstate, DoNothingEnterExit);
        this.states.Add(newstate, s);
        return true;
    }

    protected bool AddTransitionsToState(Enum sourceState, Enum[] transitions, bool forceTransition = false)
    {
        if (this.Initialized() == false) { return false; }
        if (this.states.ContainsKey(sourceState) == false) { return false; }
        State s = states[sourceState];
        s.forceTransition = forceTransition;
        foreach (Enum t in transitions)
        {
            if (s.transitions.Contains(t) == true)
            {
                Debug.LogError("State: " + sourceState + " already contains a transition for " + t + " in " + this.GetType().ToString());
                continue;
            }
            s.transitions.Add(t);
        }
        return true;
    }
    protected bool IsLegalTransition(Enum fromstate, Enum tostate)
    {
        if (this.Initialized() == false) { return false; }

        if (this.states.ContainsKey(fromstate) && this.states.ContainsKey(tostate))
        {
            if (this.states[fromstate].forceTransition == true || this.states[fromstate].transitions.Contains(tostate) == true)
            {
                return true;
            }
        }
        return false;
    }

    protected bool ChangeCurrentState(Enum newstate, bool forceTransition = false)
    {
        if (this.Initialized() == false) { return false; }

        if (this.inTransition)
        {
            if (this.debugMode == true)
            {
                Debug.LogWarning(this.GetType().ToString() + " requests transition to state " + newstate +
                        " when still transitioning");
            }
            return false;
        }

        if (forceTransition || this.IsLegalTransition(this.currentState.name, newstate))
        {
            if (this.debugMode == true)
            {
                Debug.Log(this.GetType().ToString() + " transition: " + this.currentState.name + " => " + newstate);
            }

            State transitionSource = this.currentState;
            State transitionTarget = this.states[newstate];
            this.inTransition = true;
            this.currentState.exitMethod(transitionTarget.name);
            transitionTarget.enterMethod(transitionSource.name);
            this.currentState = transitionTarget;

            if (transitionTarget == null || transitionSource == null)
            {
                Debug.LogError(this.GetType().ToString() + " cannot finalize transition; source or target state is null!");
            }
            else
            {
                this.inTransition = false;
            }
        }
        else
        {
            Debug.LogError(this.GetType().ToString() + " requests transition: " + this.currentState.name + " => " + newstate + " is not a defined transition!");
            return false;
        }

        this.OnUpdate = this.currentState.updateMethod;
        return true;
    }
    private static void DoNothingUpdate() { }
    private static void DoNothingEnterExit(Enum state) { }
}
