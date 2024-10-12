using Microsoft.AspNetCore.Components;
using System.Globalization;
using System.Reflection;

namespace GreatIdeas.Blazor.MudComponents;

public partial class DateTimePicker<T>
{
    [Parameter]
    public T Value { get; set; } = default!;

    [Parameter]
    public EventCallback<T> ValueChanged { get; set; }

    public DateTime? DateTime_ { get; set; }

    Type? type;

    string datePattern
    {
        get
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
        }
    }

    bool timeAmPm
    {
        get
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern.EndsWith("tt");
        }
    }



    protected override async Task OnParametersSetAsync()
    {
        type = Value!.GetType();
        if (type == typeof(DateTimeOffset?) || type == typeof(DateTimeOffset))
        {
            PropertyInfo pi = type.GetProperty("LocalDateTime")!;
            DateTime_ = (DateTime?)pi.GetValue(Value);
        }
        else if (type == typeof(DateTime?) || type == typeof(DateTime))
        {
            object dtobj = (object)Value;
            DateTime_ = (DateTime?)dtobj;
        }
        else
        {
            throw new InvalidOperationException("Only DateTime and DateTimeOffset types are supported");
        }

        await base.OnParametersSetAsync();
    }

    async Task DateChanged(DateTime? dt)
    {
        if (dt.HasValue)
        {
            DateTime_ = new DateTime(dt.Value.Year, dt.Value.Month, dt.Value.Day, DateTime_.HasValue ? DateTime_.Value.Hour : 0, DateTime_.HasValue ? DateTime_.Value.Minute : 0, 0);
            await DateTimeChanged();
        }
        else
        {
            DateTime_ = null;
            await ValueChanged.InvokeAsync(default);
        }
    }

    async Task TimeChanged(TimeSpan? ts)
    {
        if (ts.HasValue)
        {
            DateTime_ = new DateTime(DateTime_.HasValue ? DateTime_.Value.Year : DateTime.Now.Year, DateTime_.HasValue ? DateTime_.Value.Month : DateTime.Now.Month, DateTime_.HasValue ? DateTime_.Value.Day : DateTime.Now.Day, ts.Value.Hours, ts.Value.Minutes, 0);
            await DateTimeChanged();
        }
        else
        {
            DateTime_ = null;
            await ValueChanged.InvokeAsync(default);
        }
    }

    async Task DateTimeChanged()
    {
        if (type == typeof(DateTimeOffset) || type == typeof(DateTimeOffset?))
        {
            TimeZoneInfo tzi = TimeZoneInfo.Local;
            DateTimeOffset dateTimeOffset = new(DateTime_!.Value.Year, DateTime_.Value.Month, DateTime_.Value.Day, DateTime_.Value.Hour, DateTime_.Value.Minute, 0, tzi.GetUtcOffset(DateTime_.Value));
            object dtobj = (object)dateTimeOffset;
            await ValueChanged.InvokeAsync((T)dtobj);
        }
        else if (type == typeof(DateTime) || type == typeof(DateTime?))
        {
            object dtobj = DateTime_!;
            await ValueChanged.InvokeAsync((T)dtobj);
        }
    }
}