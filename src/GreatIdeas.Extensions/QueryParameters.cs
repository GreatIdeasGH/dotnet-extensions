using System.Text;
using System.Text.Encodings.Web;

namespace GreatIdeas.Extensions;

public static class QueryParameters
{
    //
    // Summary:
    //     Append the given query key and value to the URI.
    //
    // Parameters:
    //   uri:
    //     The base URI.
    //
    //   name:
    //     The name of the query key.
    //
    //   value:
    //     The query value.
    //
    // Returns:
    //     The combined result.
    public static string AddQueryString(string uri, string name, string value)
    {
        if (uri == null)
        {
            throw new ArgumentNullException(nameof(uri));
        }

        if (name == null)
        {
            throw new ArgumentNullException(nameof(name));
        }

        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        return AddQueryString(uri, new KeyValuePair<string, string>[1]
        {
                new KeyValuePair<string, string>(name, value)
        });
    }

    //
    // Summary:
    //     Append the given query keys and values to the uri.
    //
    // Parameters:
    //   uri:
    //     The base uri.
    //
    //   queryString:
    //     A collection of name value query pairs to append.
    //
    // Returns:
    //     The combined result.
    public static string AddQueryString(string uri, IDictionary<string, string> queryString)
    {
        if (uri == null)
        {
            throw new ArgumentNullException(nameof(uri));
        }

        if (queryString == null)
        {
            throw new ArgumentNullException(nameof(queryString));
        }

        return AddQueryString(uri, (IEnumerable<KeyValuePair<string, string>>)queryString);
    }

    private static string AddQueryString(string uri, IEnumerable<KeyValuePair<string, string>> queryString)
    {
        if (uri == null)
        {
            throw new ArgumentNullException(nameof(uri));
        }

        if (queryString == null)
        {
            throw new ArgumentNullException(nameof(queryString));
        }

        int num = uri.IndexOf('#');
        string text = uri;
        string value = "";
        if (num != -1)
        {
            value = uri.Substring(num);
            text = uri.Substring(0, num);
        }

        bool flag = text.IndexOf('?') != -1;
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(text);
        foreach (KeyValuePair<string, string> item in queryString)
        {
            stringBuilder.Append(flag ? '&' : '?');
            stringBuilder.Append(UrlEncoder.Default.Encode(item.Key));
            stringBuilder.Append('=');
            stringBuilder.Append(UrlEncoder.Default.Encode(item.Value));
            flag = true;
        }

        stringBuilder.Append(value);
        return stringBuilder.ToString();
    }
}
