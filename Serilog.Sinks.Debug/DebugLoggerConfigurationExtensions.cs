//-----------------------------------------------------------------------
// <copyright file="DebugLoggerConfigurationExtensions.cs" company="Jorge Alberto Hernández Quirino">
// Copyright (c) Jorge Alberto Hernández Quirino 2015-2016. All rights reserved.
// </copyright>
// <author>Jorge Alberto Hernández Quirino</author>
//-----------------------------------------------------------------------
using System;
using Serilog.Configuration;
using Serilog.Events;
using Serilog.Formatting.Display;
using Serilog.Sinks.Debug;

namespace Serilog
{

    /// <summary>
    /// Extends <see cref="LoggerConfiguration" /> with methods to add debug sinks.
    /// </summary>
    public static class DebugLoggerConfigurationExtensions
    {

        #region Constants

        /// <summary>
        /// Default template for log output: "{Timestamp} [{Level}] {Message}{NewLine}{Exception}".
        /// </summary>
        private const string DefaultOutputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}";

        #endregion

        #region Methods

        /// <summary>
        /// Writes log events to <see cref="System.Diagnostics.Debug"/>.
        /// </summary>
        /// <param name="sinkConfiguration">Logger sink configuration.</param>
        /// <param name="restrictedToMinimumLevel">The minimum level for events passed through the sink.</param>
        /// <param name="outputTemplate">A message template describing the format used to write to the sink.the default is "{Timestamp} [{Level}] {Message}{NewLine}{Exception}".</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public static LoggerConfiguration Debug(this LoggerSinkConfiguration sinkConfiguration,
                                                LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
                                                string outputTemplate = DefaultOutputTemplate,
                                                IFormatProvider formatProvider = null)
        {
            if (sinkConfiguration == null)
                throw new ArgumentNullException(nameof(sinkConfiguration));
            if (outputTemplate == null)
                throw new ArgumentNullException(nameof(outputTemplate));
            var formatter = new MessageTemplateTextFormatter(outputTemplate, formatProvider);
            return sinkConfiguration.Sink(new DebugSink(formatter), restrictedToMinimumLevel);
        }

        #endregion

    }

}
