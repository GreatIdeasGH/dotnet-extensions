using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Globalization;
using System.Reflection;
using System;

namespace GreatIdeas.Blazor.MudComponents
{
    public partial class DateTimePicker<T>
    {
        [Parameter]
        public T Value { get; set; }

        [Parameter]
        public EventCallback<T> ValueChanged { get; set; }

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

        public DateTime? DateTime_ { get; set; }

        Type type;
        protected override async Task OnParametersSetAsync()
        {
            type = Value.GetType();
            if (type == typeof(DateTimeOffset? ) || type == typeof(DateTimeOffset))
            {
                PropertyInfo pi = type.GetProperty("LocalDateTime");
                DateTime_ = (DateTime? )pi.GetValue(Value);
            }
            else if (type == typeof(DateTime? ) || type == typeof(DateTime))
            {
                object dtobj = (object)Value;
                DateTime_ = (DateTime? )dtobj;
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
                await ValueChanged.InvokeAsync(default(T));
            }
        }

        async Task TimeChanged(TimeSpan? ts)
        {
            if (ts.HasValue)
            {
                DateTime_ = new DateTime(DateTime_.HasValue ? DateTime_.Value.Year : System.DateTime.Now.Year, DateTime_.HasValue ? DateTime_.Value.Month : System.DateTime.Now.Month, DateTime_.HasValue ? DateTime_.Value.Day : System.DateTime.Now.Day, ts.Value.Hours, ts.Value.Minutes, 0);
                await DateTimeChanged();
            }
            else
            {
                DateTime_ = null;
                await ValueChanged.InvokeAsync(default(T));
            }
        }

        async Task DateTimeChanged()
        {
            if (type == typeof(DateTimeOffset) || type == typeof(DateTimeOffset? ))
            {
                TimeZoneInfo tzi = TimeZoneInfo.Local;
                DateTimeOffset dateTimeOffset = new DateTimeOffset(DateTime_.Value.Year, DateTime_.Value.Month, DateTime_.Value.Day, DateTime_.Value.Hour, DateTime_.Value.Minute, 0, tzi.GetUtcOffset(DateTime_.Value));
                object dtobj = (object)dateTimeOffset;
                await ValueChanged.InvokeAsync((T)dtobj);
            }
            else if (type == typeof(DateTime) || type == typeof(DateTime? ))
            {
                object dtobj = (object)DateTime_;
                await ValueChanged.InvokeAsync((T)dtobj);
            }
        }
    }
}