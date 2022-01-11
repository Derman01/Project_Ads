namespace Project_Ads.Model
{
    public class Rights
    {
        public bool CanViewBoard { get; private set; }
        public bool CanAdd { get; private set; }
        public bool CanEdit { get; private set; }
        public bool CanDelete { get; private set; }
        public bool IsAdmin { get; set; }
        
        public Rights(
            bool canAdd, 
            bool canDelete,
            bool canEdit,
            bool canViewBoard,
            bool isAdmin)
        {
            CanAdd = canAdd;
            CanDelete = canDelete;
            CanEdit = canEdit;
            CanViewBoard = canViewBoard;
            IsAdmin = isAdmin;
        }
    }
}