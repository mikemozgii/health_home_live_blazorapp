using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.RenderTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HHL.WebApp.Components.Shared
{
    public class InputCustomText : InputBase<string>
    {
        [Parameter] protected string Placeholder { get; private set; }
        [Parameter] protected bool IsDisabled { get; private set; }

        
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {




            base.BuildRenderTree(builder);
            builder.OpenElement(0, "input");
            builder.AddAttribute(1, "id", Id);
            builder.AddAttribute(2, "type", "text");           
            builder.AddAttribute(3, "class", CssClass);
            builder.AddAttribute(4, "value", BindMethods.GetValue(CurrentValue));
            builder.AddAttribute(5, "onchange", BindMethods.SetValueHandler(__value => CurrentValue = __value, CurrentValue));
            if (!string.IsNullOrWhiteSpace(Placeholder))
            {
                builder.AddAttribute(6, "placeholder", Placeholder);
            }

            if (IsDisabled)
            {
                builder.AddAttribute(7, "disabled", "disabled");
            }

            builder.CloseElement();
        }

        /// <inheritdoc />
        protected override bool TryParseValueFromString(string value, out string result, out string validationErrorMessage)
        {
            result = value;
            validationErrorMessage = null;
            return true;
        }
    }
}
