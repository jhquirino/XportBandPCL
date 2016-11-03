//-----------------------------------------------------------------------
// <copyright file="DebugLoggerConfigurationExtensions.cs" company="Jorge Alberto Hernández Quirino">
// Copyright (c) Jorge Alberto Hernández Quirino 2015-2016. All rights reserved.
// </copyright>
// <author>Jorge Alberto Hernández Quirino</author>
//-----------------------------------------------------------------------
using System;
using System.IO;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;

namespace Serilog.Sinks.Debug
{

    /// <summary>
    /// Write log events to a <see cref="System.Diagnostics.Debug"/> output.
    /// </summary>
    internal class DebugSink : ILogEventSink
    {

        #region Inner members

        /// <summary>
        /// Formatter used to convert log events to text.
        /// </summary>
        private readonly ITextFormatter _textFormatter;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DebugSink"/> class.
        /// </summary>
        /// <param name="textFormatter">Formatter used to convert log events to text.</param>
        public DebugSink(ITextFormatter textFormatter)
        {
            if (textFormatter == null)
                throw new ArgumentNullException(nameof(textFormatter));
            _textFormatter = textFormatter;
        }

        #endregion

        #region ILogEventSink implementation

        /// <summary>
        /// Emit the provided log event to the sink.
        /// </summary>
        /// <param name="logEvent">The log event to write.</param>
        public void Emit(LogEvent logEvent)
        {
            if (logEvent == null)
                throw new ArgumentNullException(nameof(logEvent));
            var renderSpace = new StringWriter();
            _textFormatter.Format(logEvent, renderSpace);
            System.Diagnostics.Debug.WriteLine(renderSpace.ToString());
        }

        #endregion

    }

}
