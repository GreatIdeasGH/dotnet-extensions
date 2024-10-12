﻿using Microsoft.AspNetCore.Components.Forms;
using System.Diagnostics.CodeAnalysis;

namespace GreatIdeas.Blazor.Input;

/// <summary>
/// Custom InputSelect Component to support Guid, int, int? and string data types.
/// </summary>
/// <typeparam name="TValue"></typeparam>
public class CustomInputSelect<TValue> : InputSelect<TValue>
{
    protected override bool TryParseValueFromString(string? value, [MaybeNullWhen(false)] out TValue result, [NotNullWhen(false)] out string? validationErrorMessage)
    {
        if (typeof(TValue) == typeof(int))
        {
            if (int.TryParse(value, out var resultInt))
            {
                result = (TValue)(object)resultInt;
                validationErrorMessage = string.Empty;
                return true;
            }
            else
            {
                result = default;
                validationErrorMessage = $"The selected value {value} is not a valid number.";
                return false;
            }
        }
        else if (typeof(TValue) == typeof(Guid))
        {
            if (Guid.TryParse(value, out var resultGuid))
            {
                result = (TValue)(object)resultGuid;
                validationErrorMessage = string.Empty;
                return true;
            }
            else
            {
                result = default;
                validationErrorMessage = $"The selected value {value} is not a valid number.";
                return false;
            }
        }
        else if (typeof(TValue) == typeof(int?))
        {
            if (int.TryParse(value, out var resultGuid))
            {
                result = (TValue)(object)resultGuid;
                validationErrorMessage = string.Empty;
                return true;
            }
            else
            {
                result = default;
                validationErrorMessage = $"The selected value {value} is not a valid number.";
                return false;
            }
        }
        else
        {
            return base.TryParseValueFromString(value, out result, out validationErrorMessage);
        }
    }
}