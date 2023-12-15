using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventsMediator 
{
    static List<Player> healthChangedInvokers = new List<Player>();
    static List<Action<int>> healthChangedListeners = new List<Action<int>>();

    static List<Player> gameOverInvokers = new List<Player>();
    static List<Action> gameOverListeners = new List<Action>();

    public static void AddHealthChangedInvoker(Player invoker)
    {
        // add listeners to new invoker and add new invoker to list
        foreach (Action<int> listener in healthChangedListeners)
        {
            invoker.AddPlayerHPChangedListener(listener);
        }
        healthChangedInvokers.Add(invoker);
    }

    
    public static void AddHealthChangedListener(Action<int> listener)
    {
        // add as listener to all invokers and add new listener to list
        foreach (Player invoker in healthChangedInvokers)
        {
            invoker.AddPlayerHPChangedListener(listener);
        }
        healthChangedListeners.Add(listener);
    }

    
    public static void AddPlayerDiedInvoker(Player invoker)
    {
        // add listeners to new invoker and add new invoker to list
        foreach (Action listener in gameOverListeners)
        {
            invoker.AddPlayerDiedListener(listener);
        }
        gameOverInvokers.Add(invoker);
    }

    
    public static void AddPlayerDiedListener(Action listener)
    {
        // add as listener to all invokers and add new listener to list
        foreach (Player invoker in gameOverInvokers)
        {
            invoker.AddPlayerDiedListener(listener);
        }
        gameOverListeners.Add(listener);
    }
}
