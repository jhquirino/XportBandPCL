using System;
using System.IO;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;

namespace Serilog.Sinks.Debug
{

    internal class DebugSink : ILogEventSink
    {

        private readonly ITextFormatter _textFormatter;

        public DebugSink(ITextFormatter textFormatter)
        {
            if (textFormatter == null)
                throw new ArgumentNullException(nameof(textFormatter));
            _textFormatter = textFormatter;
        }

        public void Emit(LogEvent logEvent)
        {
            if (logEvent == null)
                throw new ArgumentNullException(nameof(logEvent));
            var renderSpace = new StringWriter();
            _textFormatter.Format(logEvent, renderSpace);
            System.Diagnostics.Debug.WriteLine(renderSpace.ToString());
        }

    }

}
