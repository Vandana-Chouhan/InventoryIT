using InventoryIT.Models;

namespace InventoryIT.Repository

{
    public interface IFinancialYearRepository
    {
        public IEnumerable<FinancialYear> GetAllFinancialYear();
        public FinancialYear GetbyId(int finanYearId);
        public int AddFinancialYear(FinancialYear financialYear);

        public int Update(FinancialYear financialYear);

        public void Delete(int compid);
    }
}