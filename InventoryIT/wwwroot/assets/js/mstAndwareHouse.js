/*$(document).on("click", "#warehouseLocationLink, #masterDataLink, #openFormCard, #financialSetupLink, #companySetupLink, #branchSetupLink, #houseArea, #houseRack, #houseSelf, #loadDashboard", function (e) {
    e.preventDefault();

    var urls = {
        "warehouseLocationLink": "/Warehouse/OpenWareHouseLocation",
        "masterDataLink": "/MasterData/Index",
        "openFormCard": "/MasterData/LoadFormPartial",
        "financialSetupLink": "/FinancialYear/AddFinancial",
        "companySetupLink": "/MastComp/AddCompany",
        "branchSetupLink": "/MastBranch/AddFBranchMaster",
        "houseArea": "/Warehouse/ShowWhereHouseArea",
        "houseRack": "/Warehouse/ShowWarehouseRack",
        "houseSelf": "/Warehouse/ShowWarehouseShelf",
        "loadDashboard":"/Das/LoadDashboard"
    };

    var id = $(this).attr("id");
    var url = urls[id];

    if (url) {
        if (id === "warehouseLocationLink" || id === "masterDataLink") {
            $("#partialContent").show().load(url);
            $("#partialContentForm").html("");
        } else {
            $("#partialContent").hide();
            $("#partialContentForm").show().load(url);
        }
    }
});
*/
$(document).on("click", "#warehouseLocationLink, #masterDataLink, #openFormCard, #financialSetupLink, #companySetupLink, #branchSetupLink, #houseArea, #houseRack, #houseSelf, #loadDashboard", function (e) {
    e.preventDefault();

    var urls = {
        "warehouseLocationLink": "/Warehouse/OpenWareHouseLocation",
        "masterDataLink": "/MasterData/Index",
        "openFormCard": "/MasterData/LoadFormPartial",
        "financialSetupLink": "/FinancialYear/AddFinancial",
        "companySetupLink": "/MastComp/AddCompany",
        "branchSetupLink": "/MastBranch/AddFBranchMaster",
        "houseArea": "/Warehouse/ShowWhereHouseArea",
        "houseRack": "/Warehouse/ShowWarehouseRack",
        "houseSelf": "/Warehouse/ShowWarehouseShelf",
        "loadDashboard": "/Das/LoadDashboard"
    };

    var id = $(this).attr("id");
    var url = urls[id];

    if (url) {
        localStorage.setItem("reloadUrl", url); // URL स्टोर करें
        location.reload(); // पेज को रिफ्रेश करें
    }
});

// जब पेज लोड हो, तो स्टोर किया हुआ URL चेक करें
$(document).ready(function () {
    var reloadUrl = localStorage.getItem("reloadUrl");

    if (reloadUrl) {
        localStorage.removeItem("reloadUrl"); // URL हटा दें ताकि बार-बार लोड न हो
        $("#partialContent").load(reloadUrl); // व्यू लोड करें
    }
});
