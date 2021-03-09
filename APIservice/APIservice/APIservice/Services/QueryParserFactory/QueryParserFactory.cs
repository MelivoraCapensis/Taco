namespace APIservice.Services.QueryParserFactory
{
    public abstract class QueryParserCreator
    {
        public abstract IQuerryParser FactoryMethod(string _searchField, string[] _assumptionalItems, string[] _assumptionalAreas);

        public string[] DoParse(string _searchField, string[] _assumptionalItems, string[] _assumptionalAreas)
        {
            var queryParser = FactoryMethod(_searchField, _assumptionalItems, _assumptionalAreas);
            var result = queryParser.Parse();

            return result;
        }
    }
    public interface IQuerryParser
    {
        string[] Parse();
    }
}