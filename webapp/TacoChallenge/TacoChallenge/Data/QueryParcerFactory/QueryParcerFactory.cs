namespace TacoChallenge.Data.QueryParcerFactory
{
    abstract class QueryParcerCreator
    {
        public abstract IQuerryParcer FactoryMethod(string _searchField, string[] _assumptionalItems, string[] _assumptionalAreas);

        public string[] DoParce(string _searchField, string[] _assumptionalItems, string[] _assumptionalAreas)
        {
            var queryParcer = FactoryMethod(_searchField, _assumptionalItems, _assumptionalAreas);
            var result = queryParcer.Parce();

            return result;
        }
    }
    public interface IQuerryParcer
    {
        string[] Parce();
    }
}