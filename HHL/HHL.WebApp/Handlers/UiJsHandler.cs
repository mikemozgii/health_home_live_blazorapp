using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HHL.WebApp.Handlers
{
    public class UiJsHandler
    {
        public IJSRuntime JsRuntime { get; set; }
        public UiJsHandler(IJSRuntime _jsRuntime)
        {
            JsRuntime = _jsRuntime;
        }

        public async Task TriggerClientClick(string elementId)
        {
            await JsRuntime.InvokeAsync<string>("TriggerClientClick", elementId);
        }

        public async Task RemoveElementClass(string elementId, string className)
        {
            await JsRuntime.InvokeAsync<string>("RemoveElementClass", elementId, className);
        }

        public async Task AddElementClass(string elementId, string className, int? timeout = null)
        {
            await JsRuntime.InvokeAsync<string>("AddElementClass", elementId, className, timeout);
        }

        public async Task<bool> ShowLoadingPanel()
        {
            await RemoveElementClass("loadingPanel", "display_none");
            return true;
        }

        public async Task<bool> HideLoadingPanel()
        {
            await AddElementClass("loadingPanel", "display_none");
            return true;
        }

        public async Task SetCookie(string name, string value, int days)
        {
            await JsRuntime.InvokeAsync<string>("SetCookie", name, value, days);
        }
        public async Task DeleteCookie(string name)
        {
            await JsRuntime.InvokeAsync<string>("DeleteCookie", name);
        }
        
    }
}
