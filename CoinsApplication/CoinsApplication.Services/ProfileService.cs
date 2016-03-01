using System.Collections.Generic;
using CoinsApplication.FakeServices;
using CoinsApplication.Models;
using CoinsApplication.Services.Interfaces;

namespace CoinsApplication.Services
{
    public class ProfileService : IProfileService
    {
        public IEnumerable<ProfileModel> GetAllProfiles()
        {
            return new List<ProfileModel>
            {
                new ProfileModel("Профиль 1", new List<CoinModel>
                {
                    new CoinModel
                    {
                        Title = "Один рубль",
                        Image = Properties.Resources.one_rouble.ToByteArray()
                    },
                    new CoinModel
                    {
                        Title = "Два рубля",
                        Image = Properties.Resources.one_rouble.ToByteArray()
                    },
                })
            };
        }
    }
}
