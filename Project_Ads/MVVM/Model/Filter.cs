﻿using Project_Ads.Core;

namespace Project_Ads.Model
{
    public class Filter: ObservableObject
    {
        private bool _isCat = true;
        private bool _isDog = true;
        private bool _isFind = true;
        private bool _isLost = true;
        public bool IsCat
        {
            get => _isCat;
            set
            {
                _isCat = value;
                OnPropertyChanged("IsCat");
            }
        }

        public bool IsDog
        {
            get => _isDog;
            set
            {
                _isDog = value;
                OnPropertyChanged("IsDog");
            }
        }

        public bool IsFind
        {
            get => _isFind;
            set
            {
                _isFind = value;
                OnPropertyChanged("IsFind");
            }
        }

        public bool IsLost
        {
            get => _isLost;
            set
            {
                _isLost = value;
                OnPropertyChanged("IsLost");
            }
        }

        public bool IsItemFilter(Advertisement item)
        {
            return (item.TypeAdvertisement == Advertisement.Type.Find && IsFind
                    || item.TypeAdvertisement == Advertisement.Type.Lose && IsLost)
                   && ( item.TypesAnimal == Animal.Type.Cat && IsCat
                        || item.TypesAnimal == Animal.Type.Dog && IsDog);
        }

        public void CopyTo(Filter f)
        {
            IsCat = f.IsCat;
            IsDog = f.IsDog;
            IsFind = f.IsFind;
            IsLost = f.IsLost;
        }
    }
}