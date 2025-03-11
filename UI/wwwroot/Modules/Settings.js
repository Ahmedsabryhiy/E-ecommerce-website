var ClsSettings = {
    GetAll: function () {
        Helper.AjaxCallGet("https://localhost:7226/api/Settings", {}, "json",
            function (data) {
                console.log(data);
                $("#LnkFacebook").attr("href", data.data.faseBookLink);
                $("#Description").text(data.data.webSiteDescription);
                $("#ContactNumber").text(data.data.contactNumber); // Corrected line
            },
            function (error) {
                console.error("Error fetching settings:", error);
            }
        );
    }
};

ClsSettings.GetAll();
