using AutoMapper;
using InventoryIT.DTOs;
using InventoryIT.Models;
namespace InventoryIT.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<FinancialYear, FinancialYearDTO>()
                .ForMember(dest => dest.CompId, opt => opt.MapFrom(src => src.CompId)); // Map CompId from Model to DTO

            // Mapping from FinancialYearDTO (DTO) to FinancialYear (Model)
            CreateMap<FinancialYearDTO, FinancialYear>()
                .ForMember(dest => dest.Comp, opt => opt.MapFrom(src => new MastComp { CompId = src.CompId })); // Map Comp from DTO to Model (using CompId)
            CreateMap<ItemCatagoryDTO, ItemCatagory>();
            CreateMap<ItemCatagory, ItemCatagoryDTO>();
            CreateMap<ItemTypeDTO, ItemType>();
            CreateMap<ItemType, ItemTypeDTO>();
            CreateMap<ItemUnit, ItemUnitDTO>();
            CreateMap<ItemUnitDTO, ItemUnit>();
            CreateMap<ItemCompany, ItemCompanyDTO>();
            CreateMap<ItemCompanyDTO, ItemCompany>();
            CreateMap<ItemMaster, ItemMasterDTO>();
            CreateMap<ItemMasterDTO, ItemMaster>();
            CreateMap<ItemSubCatagory, ItemSubCatagoryDTO>();
            CreateMap<ItemSubCatagoryDTO, ItemSubCatagory>();
            CreateMap<SupplierMaster, SupplierMasterDTO>();
            CreateMap<SupplierMasterDTO, SupplierMaster>();
            CreateMap<MastComp, MastCompDTO>();
            CreateMap<MastCompDTO, MastComp>();
            CreateMap<MastBranch, MastBranchDTO>();
            CreateMap<MastBranchDTO, MastBranch>();
            CreateMap<MastCity, MastCityDTO>();
            CreateMap<MastCityDTO, MastCity>();
            CreateMap<MastCountry, MastCountryDTO>();
            CreateMap<MastCountryDTO, MastCountry>();
            CreateMap<MastState, MastStateDTO>();
            CreateMap<MastStateDTO, MastState>();
            CreateMap<WarehouseAreaMaster, WarehouseAreaMasterDTO>();
            CreateMap<WarehouseAreaMasterDTO, WarehouseAreaMaster>();
            CreateMap<WarehouseLocationMaster, WarehouseLocationMasterDTO>();
            CreateMap<WarehouseLocationMasterDTO, WarehouseLocationMaster>();
            CreateMap<WarehouseRackMaster, WarehouseRackMasterDTO>();
            CreateMap<WarehouseRackMasterDTO, WarehouseRackMaster>();
            CreateMap<WarehouseShelfMaster, WarehouseShelfMasterDTO>();
            CreateMap<WarehouseShelfMasterDTO, WarehouseShelfMaster>();
            CreateMap<MastItemStk, MastItemStkDTO>();
            CreateMap<MastItemStkDTO, MastItemStk>();
            CreateMap<MastItemSupplierRate, MastItemSupplierRateDTO>()
            .ForMember(dest => dest.Item, opt => opt.MapFrom(src => src.Item)); // Map Item navigation property
            CreateMap<MastItemSupplierRateDTO, MastItemSupplierRate>()
            .ForMember(dest => dest.Item, opt => opt.MapFrom(src => src.Item)); // Map Item navigation property
            CreateMap<UserMaster, UserMasterDTO>();
            CreateMap<UserMasterDTO, UserMaster>();
        }
    }
}
