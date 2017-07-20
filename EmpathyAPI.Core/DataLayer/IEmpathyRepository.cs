using EmpathyAPI.Core.EntityLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmpathyAPI.Core.DataLayer
{
    public interface IEmpathyRepository : IDisposable
    {

        Task<Profile> GetUserProfile(string userId);
        Tuple<Task<Profile>, Task<TimeSpan>> GetUserProfileTimeSpan(string userId);

        Task<Profile> GetUserProfileImageString(string userId);
    }
}
