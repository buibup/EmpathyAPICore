using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmpathyAPI.Core.DataLayer;
using EmpathyAPI.Core.EntityLayer;

namespace EmpathyAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Empathy")]
    public class EmpathyController : Controller
    {
        private IEmpathyRepository _IEmpathyRepository;
        public EmpathyController()
        {
            _IEmpathyRepository = new EmpathyRepository();
        }
        [Route("GetUserProfile")]
        public Task<Profile> GetUserProfile(string userId, string AccessToken)
        {
            return _IEmpathyRepository.GetUserProfile(userId, AccessToken);
        }
    }
}