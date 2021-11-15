using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusManager.Presentation.Services
{
    public interface IJSConsole
    {
        Task LogAsync(string message);
    }

    public class JSConsole : IJSConsole
    {
        private readonly IJSRuntime _JsRuntime;

        public JSConsole(IJSRuntime jSRuntime)
        {
            _JsRuntime = jSRuntime;
        }

        public async Task LogAsync(string message)
        {
            Console.WriteLine(message);
            await _JsRuntime.InvokeVoidAsync("console.log", message);
        }
    }
}
