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
    [Route("api/Empathy")]
    public class EmpathyController : Controller
    {
        private IEmpathyRepository _IEmpathyRepository;

        public EmpathyController(IEmpathyRepository IEmpathyRepository)
        {
            _IEmpathyRepository = IEmpathyRepository;
        }
        [Route("GetUserProfile/{userId}")]
        public Task<Profile> GetUserProfile(string userId)
        {
            return _IEmpathyRepository.GetUserProfile(userId);
        }
        [Route("GetUserProfileTimeSpan/{userId}")]
        public Tuple<Task<Profile>, Task<TimeSpan>> GetUserProfileTimeSpan(string userId)
        {
            return _IEmpathyRepository.GetUserProfileTimeSpan(userId);
        }

        [Route("GetUserProfileImageString/{userId}")]
        public Task<Profile> GetUserProfileImageString(string userId)
        {
            return _IEmpathyRepository.GetUserProfileImageString(userId);
        }
    }
}