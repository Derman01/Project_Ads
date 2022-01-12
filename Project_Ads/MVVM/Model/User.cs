using System.Collections.Generic;

namespace Project_Ads.MVVM.Model
{
    public class User
    {
        public enum Role
        { 
            Admin,
            User,
            NotAuthorizedUser
        }
        
        public int UserId { get; set; }

        public string UserName { get; set; }
        
        public string UserPhone { get; set; }

        public Role UserRole { get; set; }
    }
}