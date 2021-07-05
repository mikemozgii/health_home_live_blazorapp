using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.RenderTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HHL.WebApp.Components.Shared
{
    public class InputCustomSelect<T> : InputBase<T>
    {
        [Parameter] RenderFragment ChildContent { get; set; }
        [Parameter] public string Placeholder { get; private set; }
        [Parameter] public EventCallback<UIChangeEventArgs> OnCustomChange { get; set; }
        [Parameter] protected bool IsDisabled { get; private set; }
        /// <inheritdoc />
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);
            builder.OpenElement(0, "select");
            builder.AddAttribute(1, "id", Id);
            builder.AddAttribute(2, "class", CssClass);
            builder.AddAttribute(3, "value", BindMethods.GetValue(CurrentValueAsString));
           

            if (OnCustomChange.HasDelegate)
            {
                var val = OnCustomChange;
                builder.AddAttribute(4, "onchange", val);
            }
            else
            {
                builder.AddAttribute(4, "onchange", BindMethods.SetValueHandler(__value => CurrentValueAsString = __value, CurrentValueAsString));
            }


            if (!string.IsNullOrWhiteSpace(Placeholder))
            {
                builder.AddAttribute(5, "placeholder", Placeholder);
            }

            if (IsDisabled)
            {
                builder.AddAttribute(6, "disabled", "disabled");
            }

            builder.AddContent(7, ChildContent);


            builder.CloseElement();
        }

        /// <inheritdoc />
        protected override bool TryParseValueFromString(string value, out T result, out string validationErrorMessage)
        {
            if (typeof(T) == typeof(string))
            {
                result = (T)(object)value;
                validationErrorMessage = null;
                return true;
            }
            else if (typeof(T).IsEnum)
            {
                // There's no non-generic Enum.TryParse (https://github.com/dotnet/corefx/issues/692)
                try
                {
                    result = (T)Enum.Parse(typeof(T), value);
                    validationErrorMessage = null;
                    return true;
                }
                catch (ArgumentException)
                {
                    result = default;
                    validationErrorMessage = $"The {FieldIdentifier.FieldName} field is not valid.";
                    return false;
                }
            }
            else if (typeof(T) == typeof(Guid?))
            {

                if (value != null)
                {
                    Type t = typeof(T);
                    Type u = Nullable.GetUnderlyingType(t);
                    //result = (T)Convert.ChangeType(value, typeof(T));
                    var r = new Guid(value.ToString());
                    result = (T)Convert.ChangeType(r, u);

                }
                else
                {
                    result = default(T);
                }

                validationErrorMessage = null;
                return true;

            }
            else if (typeof(T) == typeof(Guid))
            {

                if (value != null)
                {
                    Type u = typeof(T);
                    var r = new Guid(value.ToString());
                    result = (T)Convert.ChangeType(r, u);

                }
                else
                {
                    result = default(T);
                }

                validationErrorMessage = null;
                return true;

            }
            else if (typeof(T) == typeof(int?))
            {

                if (value != null)
                {
                    Type t = typeof(T);
                    Type u = Nullable.GetUnderlyingType(t);
                    var r = Convert.ToInt32(value.ToString());
                    result = (T)Convert.ChangeType(r, u);

                }
                else
                {
                    result = default(T);
                }

                validationErrorMessage = null;
                return true;

            }
            else if (typeof(T) == typeof(int))
            {

                if (value != null)
                {
                    Type u = typeof(T);
                    var r = Convert.ToInt32(value.ToString());
                    result = (T)Convert.ChangeType(r, u);

                }
                else
                {
                    result = default(T);
                }

                validationErrorMessage = null;
                return true;

            }
            throw new InvalidOperationException($"{GetType()} does not support the type '{typeof(T)}'.");
        }
    }
}
