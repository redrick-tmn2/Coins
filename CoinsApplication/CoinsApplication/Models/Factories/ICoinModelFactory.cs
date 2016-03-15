using CoinsApplication.DAL.Entities;

namespace CoinsApplication.Models.Factories
{
    public interface ICoinModelFactory
    {
        CoinModel Create(Coin coin);
        CoinModel Create();
    }
}