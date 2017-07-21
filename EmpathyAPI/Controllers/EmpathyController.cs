using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmpathyAPI.Core.DataLayer;
using EmpathyAPI.Core.EntityLayer;
using Microsoft.Extensions.Options;

namespace EmpathyAPI.Controllers
{
    [Produces("application/json")]
    public class EmpathyController : Controller
    {
        private IEmpathyRepository _IEmpathyRepository;

        public EmpathyController(IEmpathyRepository IEmpathyRepository)
        {
            _IEmpathyRepository = IEmpathyRepository;
        }

        public Task<Profile> GetUserProfile(string userId)
        {
            return _IEmpathyRepository.GetUserProfile(userId);
        }

        public Tuple<Task<Profile>, Task<TimeSpan>> GetUserProfileTimeSpan(string userId)
        {
            return _IEmpathyRepository.GetUserProfileTimeSpan(userId);
        }

        public Task<Profile> GetUserProfileImageString(string userId)
        {
            return _IEmpathyRepository.GetUserProfileImageString(userId);
        }
    }
}