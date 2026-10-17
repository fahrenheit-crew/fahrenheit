// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit;

/// <summary>
///     Represents the method that runs in response to an <see cref="FhEvent{TArgs}"/>.
/// </summary>
/// <param name="args">The arguments provided by the event.</param>
/// <typeparam name="TArgs">The arguments provided by the event.</typeparam>
public delegate void FhEventHandler<in TArgs>(TArgs args)
    where TArgs : allows ref struct;

/// <summary>
///     Enables you to respond to in-game occurrences.
/// </summary>
/// <remarks>
///     You can only subscribe to Fahrenheit events in your
///     module's <c>init</c> method, and cannot unsubscribe from them.
/// </remarks>
/// <typeparam name="TArgs">The arguments provided by the event.</typeparam>
public class FhEvent<TArgs> where TArgs : allows ref struct {
    private readonly HashSet<FhEventHandler<TArgs>> _handlers = [];

    internal FhEvent() { }

    /// <summary>
    ///     Subscribe to the event.
    /// </summary>
    /// <param name="handler">The handler to invoke when the event is raised.</param>
    public bool subscribe(FhEventHandler<TArgs> handler) {
        if (FhEnvironment.get_execution_state() != FhExecState.INIT) {
            throw new Exception("You cannot subscribe to events outside of a module's init() method.");
        }

        return _handlers.Add(handler);
    }

    /// <summary>
    ///     Raises the event.
    /// </summary>
    /// <remarks>The event's handlers are invoked in subscription order.</remarks>
    /// <param name="args">The arguments to pass to the handlers.</param>
    /// <returns>Whether any handlers were invoked.</returns>
    internal bool invoke(TArgs args) {
        foreach (FhEventHandler<TArgs> handler in _handlers) {
            handler.Invoke(args);
        }

        return _handlers.Count == 0;
    }
}
