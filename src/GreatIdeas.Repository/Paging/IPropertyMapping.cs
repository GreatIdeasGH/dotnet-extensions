namespace GreatIdeas.Repository.Paging;

public interface IPropertyMapping
{
    Dictionary<string, PropertyMappingValue> MappingDictionary { get; }
}