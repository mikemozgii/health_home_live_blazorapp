using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.RenderTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HHL.WebApp.Components.Shared
{
    public class InputCustomTextArea : InputBase<string>
    {
        [Parameter] protected string Placeholder { get; private set; }
        [Parameter] protected int Row { get; private set; } = 1;
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);
            builder.OpenElement(0, "textarea");
            builder.AddAttribute(1, "id", Id);
            builder.AddAttribute(2, "class", CssClass);
            builder.AddAttribute(3, "value", BindMethods.GetValue(CurrentValue));
            builder.AddAttribute(4, "onchange", BindMethods.SetValueHandler(__value => CurrentValue = __value, CurrentValue));

            if (!string.IsNullOrWhiteSpace(Placeholder))
            {
                builder.AddAttribute(5, "placeholder", Placeholder);
            }

            builder.AddAttribute(6, "row", Row);

            builder.CloseElement();
        }

     
        protected override bool TryParseValueFromString(string value, out string result, out string validationErrorMessage)
        {
            result = value;
            validationErrorMessage = null;
            return true;
        }
    }
}
