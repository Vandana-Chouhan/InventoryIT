using InventoryIT.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryIT.Repository
{
    public class UserMasterRepository : IUserMasterRepository
    {
        private readonly InventoryContext _inventoryContext;
        public UserMasterRepository(InventoryContext inventoryContext)
        {
            _inventoryContext = inventoryContext;
        }
        public IEnumerable<UserMaster> GetAllUserMaster()
        {
            return _inventoryContext.UserMasters.ToList();
        }
        public UserMaster? GetbyId(int userid)
        {
            return _inventoryContext.UserMasters.Find(userid);
        }
        public int AddUserMaster(UserMaster userMaster)
        {
            int result = 0;
            if (userMaster != null)
            {
                try
                {
                    _inventoryContext.UserMasters.Add(userMaster);
                    _inventoryContext.SaveChanges();
                    result = userMaster.UserId;  // Assuming CompId is the primary key
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding user master details.", ex);
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(userMaster), "User master cannot be null");
            }
            return result;
        }
        public int Update(UserMaster userMaster)
        {
            int result = -1;
            if (userMaster != null)
            {
                try
                {
                    _inventoryContext.Entry(userMaster).State = EntityState.Modified;
                    _inventoryContext.SaveChanges();
                    result = userMaster.UserId;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error updating User Master", ex);
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(userMaster), " User Master cannot be null");
            }
            return result;
        }
        public void Delete(int userid)
        {
            var year = _inventoryContext.UserMasters.Find(userid);
            if (year != null)
            {
                try
                {
                    _inventoryContext.UserMasters.Remove(year);
                    _inventoryContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error deleting user master details", ex);
                }
            }
            else
            {
                throw new KeyNotFoundException($"user master  with ID {userid} not found.");
            }
        }
    }
}
