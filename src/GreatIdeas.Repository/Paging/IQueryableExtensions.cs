using System.Linq.Dynamic.Core;

namespace GreatIdeas.Repository.Paging;

public static class IQueryableExtensions
{
    public static IQueryable<T> ApplySort<T>(
        this IQueryable<T> source,
        string orderBy,
        Dictionary<string, PropertyMappingValue> mappingDictionary)
    {
        if (source == null)
            throw new ArgumentNullException(nameof (source));
        
        if (mappingDictionary == null)
            throw new ArgumentNullException(nameof (mappingDictionary));
        
        if (string.IsNullOrWhiteSpace(orderBy))
            return source;
        
        string ordering = string.Empty;
        foreach (string str1 in orderBy.Split(','))
        {
            string str2 = str1.Trim();
            bool flag = str2.EndsWith(" desc");
            int startIndex = str2.AsSpan().IndexOf(" ");
            string key = startIndex == -1 ? str2 : str2.Remove(startIndex);
            PropertyMappingValue propertyMappingValue = mappingDictionary.TryGetValue(key, out var value) 
                ? value 
                : throw new ArgumentException("Key mapping for " + key + " is missing");
            
            if (propertyMappingValue == null)
                throw new ArgumentNullException(nameof(propertyMappingValue), "Cannot find mapping for " + key);
            
            foreach (var destinationProperty in propertyMappingValue.DestinationProperties)
            {
                if (propertyMappingValue.Revert)
                    flag = !flag;
                ordering = ordering + (string.IsNullOrWhiteSpace(ordering) ? string.Empty : ", ") + destinationProperty + (flag ? " descending" : " ascending");
            }
        }
        return source.OrderBy(ordering);
    } 
}