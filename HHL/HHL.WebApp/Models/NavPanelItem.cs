
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HHL.WebApp.Models
{
    public class NavPanelItem
    {
        public string Name { get; set; }
        public string Href { get; set; }
        public bool IsActive { get {

                return UriHelper.GetAbsoluteUri().Contains(Href);
            } }

        public Microsoft.AspNetCore.Components.IUriHelper UriHelper { get; set; }

        public NavPanelItem(Microsoft.AspNetCore.Components.IUriHelper _uriHelper, string _name, string _href)
        {
            UriHelper = _uriHelper;
            Name = _name;
            Href = _href;
        }
    }
}
