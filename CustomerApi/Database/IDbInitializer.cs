namespace CustomerApi.Database
{
    public interface IDbInitializer
    {
        void Initialize(CustomerApiContext context);
    }
}
