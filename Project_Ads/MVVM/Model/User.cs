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
        
        private int _userId { get; set; }
        public int UserId
        {
            get => _userId;
            set
            {
                _userId = value;
                // тут мб нужен OnPropertyChanged, но я хз
            }
        }
        
        private string _userName { get; set; }
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                // тут мб нужен OnPropertyChanged, но я хз
            }
        }
        private string _userPhone { get; set; }
        public string UserPhone
        {
            get => _userPhone;
            set
            {
                _userPhone = value;
                // тут мб нужен OnPropertyChanged, но я хз
            }
        }

        public Role UserRole { get; set; }

        private Rights _userRights;
        public Rights UserRights
        {
            get => _userRights;
            set
            {
                _userRights = value;
                // тут мб нужен OnPropertyChanged, но я хз
            }
        }
        
        
    }
}