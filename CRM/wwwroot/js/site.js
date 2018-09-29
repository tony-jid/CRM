$(document).ready(function () {
    
});

var site = {
    apis: {
        crm: "https://localhost:44394/",
        leadtypes: {
            get: function () {
                return site.apis.crm.concat("leadtypes/get");
            },
        },
    },
}