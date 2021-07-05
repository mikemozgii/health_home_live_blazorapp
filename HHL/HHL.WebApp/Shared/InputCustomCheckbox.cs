using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.RenderTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HHL.WebApp.Components.Shared
{
    public class InputCustomCheckbox : InputBase<bool>
    {
        [Parameter] protected EventCallback<UIChangeEventArgs> OnCustomChange { get; private set; }
        [Parameter] public bool IsCustomChecked { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "input");
            builder.AddAttribute(1, "type", "checkbox");
            builder.AddAttribute(2, "id", Id);
            builder.AddAttribute(3, "class", CssClass);
            builder.AddAttribute(4, "value", BindMethods.GetValue(CurrentValue));
            if (OnCustomChange.HasDelegate)
            {
                var val = OnCustomChange;
                builder.AddAttribute(5, "onchange", val);
            }
            else
            {
                builder.AddAttribute(5, "onchange", BindMethods.SetValueHandler(__value => CurrentValueAsString = __value, CurrentValueAsString));
            }

            if (IsCustomChecked)
            {
                builder.AddAttribute(6, "checked", "true");
            }

            builder.CloseElement();
        }

  
        protected override bool TryParseValueFromString(string value, out bool result, out string validationErrorMessage)
            => throw new NotImplementedException($"This component does not parse string inputs. Bind to the '{nameof(CurrentValue)}' property, not '{nameof(CurrentValueAsString)}'.");
    }
}
