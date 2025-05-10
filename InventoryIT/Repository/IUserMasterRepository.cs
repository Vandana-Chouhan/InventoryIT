using InventoryIT.Models;

namespace InventoryIT.Repository
{
    public interface IUserMasterRepository
    {
        public IEnumerable<UserMaster> GetAllUserMaster();
        public UserMaster? GetbyId(int userid);
        public int AddUserMaster(UserMaster userMaster);
        public int Update(UserMaster userMaster);
        public void Delete(int userid);
    }
}