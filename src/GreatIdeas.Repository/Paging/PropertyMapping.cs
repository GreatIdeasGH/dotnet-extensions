namespace GreatIdeas.Repository.Paging;

public class PropertyMapping<TSource, TDestination> : IPropertyMapping
{
    public Dictionary<string, PropertyMappingValue> MappingDictionary { get; private set; }

    public PropertyMapping(
        Dictionary<string, PropertyMappingValue> mappingDictionary)
    {
        this.MappingDictionary = mappingDictionary ?? throw new ArgumentNullException(nameof (mappingDictionary));
    }
}