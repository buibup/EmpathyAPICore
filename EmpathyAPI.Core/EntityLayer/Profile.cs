using System;
using System.Collections.Generic;
using System.Text;

namespace EmpathyAPI.Core.EntityLayer
{
    public class Profile
    {
        public string DisplayName { get; set; }
        public string UserId { get; set; }
        public string PictureUrl { get; set; }
        public string StatusMessage { get; set; }
    }
}
