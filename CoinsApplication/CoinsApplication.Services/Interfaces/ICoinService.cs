using System.Collections.Generic;
using CoinsApplication.Models;

namespace CoinsApplication.Services.Interfaces
{
    public interface ICoinService
    {
        IEnumerable<CoinModel> GetAllCoins();
        CoinModel CreateNewCoin();
    }
}
