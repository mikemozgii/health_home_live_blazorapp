using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.RenderTree;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace HHL.WebApp.Components.Shared
{
    public class InputCustomNumber<T> : InputBase<T>
    {
        delegate bool Parser(string value, out T result);
        private static Parser _parser;
        private static string _stepAttributeValue; // Null by default, so only allows whole numbers as per HTML spec

        // Determine the parsing logic once per T and cache it, so we don't have to consider all the possible types on each parse
        public InputCustomNumber()
        {
            // Unwrap Nullable<T>, because InputBase already deals with the Nullable aspect
            // of it for us. We will only get asked to parse the T for nonempty inputs.
            var targetType = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);

            if (targetType == typeof(int))
            {
                _parser = TryParseInt;
            }
            else if (targetType == typeof(long))
            {
                _parser = TryParseLong;
            }
            else if (targetType == typeof(float))
            {
                _parser = TryParseFloat;
                _stepAttributeValue = "any";
            }
            else if (targetType == typeof(double))
            {
                _parser = TryParseDouble;
                _stepAttributeValue = "any";
            }
            else if (targetType == typeof(decimal))
            {
                _parser = TryParseDecimal;
                _stepAttributeValue = "any";
            }
            else
            {
                throw new InvalidOperationException($"The type '{targetType}' is not a supported numeric type.");
            }
        }

        /// <summary>
        /// Gets or sets the error message used when displaying an a parsing error.
        /// </summary>
        [Parameter] public string ParsingErrorMessage { get; private set; } = "The {0} field must be a number.";
        [Parameter] protected string Placeholder { get; private set; }
        [Parameter] protected string Min { get; private set; }
        [Parameter] protected string Max { get; private set; }
        [Parameter] protected string Step { get; private set; }
        [Parameter] protected bool IsDisabled { get; private set; }
        [Parameter] protected EventCallback<UIChangeEventArgs> OnCustomChange { get; private set; }
        /// <inheritdoc />
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "input");
            builder.AddAttribute(1, "type", "number");

            if (!string.IsNullOrWhiteSpace(Step))
            {
                builder.AddAttribute(2, "step", Step);
            }
            else
            {
                builder.AddAttribute(2, "step", _stepAttributeValue);
            }

            builder.AddAttribute(3, "id", Id);
            builder.AddAttribute(4, "class", CssClass);
            builder.AddAttribute(5, "value", BindMethods.GetValue(CurrentValueAsString));
        
            if (OnCustomChange.HasDelegate)
            {
                var val = OnCustomChange;
                builder.AddAttribute(6, "onchange", val);
            }
            else
            {
                builder.AddAttribute(6, "onchange", BindMethods.SetValueHandler(__value => CurrentValueAsString = __value, CurrentValueAsString));
            }


            if (!string.IsNullOrWhiteSpace(Placeholder))
            {
                builder.AddAttribute(7, "placeholder", Placeholder);
            }

            if (!string.IsNullOrWhiteSpace(Min))
            {
                builder.AddAttribute(8, "min", Min);
            }

            if (!string.IsNullOrWhiteSpace(Max))
            {
                builder.AddAttribute(9, "max", Max);
            }

        if (IsDisabled)
        {
            builder.AddAttribute(10, "disabled", "disabled");
        }


        builder.CloseElement();
        }

        /// <inheritdoc />
        protected override bool TryParseValueFromString(string value, out T result, out string validationErrorMessage)
        {
            if (_parser(value, out result))
            {
                validationErrorMessage = null;
                return true;
            }
            else
            {
                validationErrorMessage = string.Format(ParsingErrorMessage, FieldIdentifier.FieldName);
                return false;
            }
        }

        static bool TryParseInt(string value, out T result)
        {
            var success = int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var parsedValue);
            if (success)
            {
                result = (T)(object)parsedValue;
                return true;
            }
            else
            {
                result = default;
                return false;
            }
        }

        static bool TryParseLong(string value, out T result)
        {
            var success = long.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var parsedValue);
            if (success)
            {
                result = (T)(object)parsedValue;
                return true;
            }
            else
            {
                result = default;
                return false;
            }
        }

        static bool TryParseFloat(string value, out T result)
        {
            var success = float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var parsedValue);
            if (success)
            {
                result = (T)(object)parsedValue;
                return true;
            }
            else
            {
                result = default;
                return false;
            }
        }

        static bool TryParseDouble(string value, out T result)
        {
            var success = double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var parsedValue);
            if (success)
            {
                result = (T)(object)parsedValue;
                return true;
            }
            else
            {
                result = default;
                return false;
            }
        }

        static bool TryParseDecimal(string value, out T result)
        {
            var success = decimal.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var parsedValue);
            if (success)
            {
                result = (T)(object)parsedValue;
                return true;
            }
            else
            {
                result = default;
                return false;
            }
        }
    }
}
