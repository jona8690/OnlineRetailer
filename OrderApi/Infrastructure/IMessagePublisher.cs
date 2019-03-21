namespace OrderApi.Infrastructure
{
    public interface IMessagePublisher
    {
        bool CustomerExists(int customerNo);
        bool CustomerGoodStanding(int customerNo);
    }
}
