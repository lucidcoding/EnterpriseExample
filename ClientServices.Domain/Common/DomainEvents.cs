﻿using System;
using System.Collections.Generic;

namespace ClientServices.Domain.Common
{
    /// <summary>
    /// Provides logic for raising and handling domain events.
    /// </summary>
    public static class DomainEvents
    {
        [ThreadStatic] //so that each thread has its own callbacks    
        private static List<Delegate> _actions;    
        //public static IContainer Container { get; set; } //as before    
        
        //Registers a callback for the given domain event 
        public static void Register<T>(Action<T> callback) 
        {
            if (_actions == null)  
                _actions = new List<Delegate>();   
            
            _actions.Add(callback);  
        }  
        
        //Clears callbacks passed to Register on the current thread 
        public static void ClearCallbacks ()
        {
            _actions = null;   
        }  
        
        //Raises the given domain event  
        public static void Raise<T>(T args) 
        {   
            //if (Container != null)  
            //  foreach(var handler in Container.ResolveAll<Handles<T>>()) 
            //      handler.Handle(args);  
            
            if (_actions != null)   
                foreach (var action in _actions)  
                    if (action is Action<T>)  
                        ((Action<T>)action)(args); 
        }
    }
}
