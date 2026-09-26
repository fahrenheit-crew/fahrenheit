// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit;

/* [fkelava 11/06/26 23:02]
 * .NET has two 'unhandled exception handlers' - AppDomain.UnhandledException
 * and ExceptionHandling.SetUnhandledExceptionHandler (.NET 10+). They have different semantics.
 *
 * Furthermore, some of these are only raised for some classes of exceptions.
 * Here's the table I've been able to piece together:
 *                                           AD.FC | AD.UE | SUEH
 * ---
 * Exception on user-spawned thread             X      X      X
 * Exception on application main thread         X      X      -
 * Exception on application non-main thread     X      -      -
 * Task exception (unobserved)                  X      -      -
 * ---
 * We install the 'newer' ExceptionHandling API.
 *
 * Stage 0 acts as an underlying crash handler, and it will process
 * any unhandled exceptions or fatal errors (AV/EEE). This can be simplified
 * down the line with https://github.com/dotnet/runtime/issues/101560.
 */

/// <summary>
///     Provides exception handling methods.
/// </summary>
internal static class FhExceptionHandler {

    private static readonly FhLogger _eh_log = new("error.log");

    /* [fkelava 14/06/26 14:23]
     * There is one edge case in which thIS will throw; a failure in initializing `FhEnvironment.Finder`,
     * because `FhLogger` relies on it. But initializing it is the first act Fahrenheit does _after_ installing EH,
     * so the window in which that can happen is vanishingly brief (and Finder itself will only fail if we can't
     * R/W to our own directory...)
     */

    /// <summary>
    ///     Installed through <see cref="ExceptionHandling.SetUnhandledExceptionHandler(Func{Exception, bool})"/>.
    /// </summary>
    internal static bool eh_unhandled(Exception ex) {
        try {
            _eh_log.LogDirect($"Unhandled exception at: {TimeProvider.System.GetUtcNow():u}");
            _eh_log.LogDirect(ex.ToString());
            _eh_log.LogDirect("================================");
        }
        catch { }

        /* [fkelava 13/06/26 12:38]
         * See https://source.dot.net/#System.Private.CoreLib/System/Threading/Thread.NativeAot.cs,445,
         * https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/when#when-in-a-catch-clause
         *
         * We are very explicitly not _handling_ it. We just want to know it happened.
         */
        return false;
    }
}
