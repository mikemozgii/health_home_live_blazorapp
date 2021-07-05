using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HHL.WebApp.Handlers
{
    public class LoadingScreen : IDisposable
    {

        public UiJsHandler UiJsHandler { get; set; }
        public LoadingScreen(UiJsHandler _uiJsHandler)
        {
            UiJsHandler = _uiJsHandler;
            //invoke();

            //var r = this.UiJsHandler.ShowLoadingPanel().Result;
        }


        //public LoadingScreen Load()
        //{
        //    var r = UiJsHandler.ShowLoadingPanel().Result;
        //    return this;
        //}

        public async Task<LoadingScreen> Load()
        {
            var r = await UiJsHandler.ShowLoadingPanel();
            return this;
        }

        public async void Dispose()
        {
            var r = await UiJsHandler.HideLoadingPanel();
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        //public void Dispose()
        //{

        //    var  r =  UiJsHandler.HideLoadingPanel().GetAwaiter().GetResult();
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}

        // NOTE: Leave out the finalizer altogether if this class doesn't
        // own unmanaged resources, but leave the other methods
        // exactly as they are.
        ~LoadingScreen()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }


        protected virtual void Dispose(bool disposing)
        {
            //UiJsHandler.HideLoadingPanel().GetAwaiter().GetResult();
        }

        //public void Dispose()
        //{
        //    var r = UiJsHandler.HideLoadingPanel().Result;
        //}
    }
}
