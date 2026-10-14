// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit;

/// <summary>
///     The method that runs in response to a Fahrenheit event.
/// </summary>
/// <param name="args">An object that contains the event data.</param>
/// <typeparam name="TArgs">The type of supplementary data associated with the event.</typeparam>
public delegate void FhEventHandler<in TArgs>(TArgs args)
    where TArgs : allows ref struct;

/// <summary>
///     An occurrence of interest which can be responded to.
/// </summary>
/// <typeparam name="TArgs">The type of supplementary data associated with the event.</typeparam>
public class FhEvent<TArgs> where TArgs : allows ref struct {
    private readonly HashSet<FhEventHandler<TArgs>> _handlers;

    internal FhEvent() {
        _handlers = new();
    }

    /// <summary>
    ///     Subscribe to the event.
    /// </summary>
    /// <param name="handler">The handler to subscribe to the event with.</param>
    public bool subscribe(FhEventHandler<TArgs> handler) {
        if (FhEnvironment.get_execution_state() != FhExecState.INIT) {
            throw new Exception("You cannot subscribe to events outside of a module's init() method.");
        }

        return _handlers.Add(handler);
    }

    /// <summary>
    ///     Invokes the event's handlers.
    /// </summary>
    /// <param name="args">The arguments to invoke the event's handlers with.</param>
    /// <returns>Whether any handlers exist.</returns>
    internal bool invoke(TArgs args) {
        foreach (FhEventHandler<TArgs> handler in _handlers) {
            handler.Invoke(args);
        }

        return _handlers.Count == 0;
    }
}
