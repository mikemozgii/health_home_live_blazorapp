using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HHL.Core.Handlers;

namespace HHL.WebApp.Models
{
    public class WizardStepModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string ContentClass { get; set; }
        public string Icon { get; set; }
        public string PassIcon { get; set; } = "check";
        public string CurrentIcon { get; set; }
        public string HeaderItemClass { get; set; }
        public bool IsPass { get; set; }

        public string TabId { get{ return $"{Name.ToUrl()}-tab-button"; } }
        public string ContentId { get { return $"{Name.ToUrl()}-tab-content"; } }


        public WizardStepModel(string name, string icon = null, bool isActive = false)
        {
            Name = name;
            IsActive = isActive;
            Icon = icon;
            if (string.IsNullOrWhiteSpace(Icon))
            {
                Icon = "user";
                CurrentIcon = Icon;
            }
            ProcessTab();
        }

        public void ProcessTab()
        {
            if (!IsActive)
            {
                ContentClass = "display_none";
                HeaderItemClass = null;
            }
            else
            {
                ContentClass = null;
                HeaderItemClass = "wizard_header_item_active";
            }

            if (IsPass)
            {
                CurrentIcon = PassIcon;
                HeaderItemClass = "wizard_header_item_pass";
            }
            else
            {
                CurrentIcon = Icon;
            }
        }
    }
}
