using System.Collections.Generic;
using CoinsApplication.Models;

namespace CoinsApplication.Services.Interfaces
{
    public interface IProfileService
    {
        IEnumerable<ProfileModel> GetAllProfiles();
    }
}
