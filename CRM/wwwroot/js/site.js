$(document).ready(function () {
    
});

var site = {
    apis: {
        crm: "/",
        leadtypes: {
            get: function () {
                return site.apis.crm.concat("leadtypes/get");
            },
            uploadImage: function () {
                return site.apis.crm.concat("leadtypes/uploadImage/");
            },
        },
        partners: {
            uploadLogo: function () {
                return site.apis.crm.concat("partners/uploadLogo/");
            },
        },
        customers: {
            getForLookup: function () {
                return site.apis.crm.concat("customers/getForLookup/");
            },
        },
        leads: {
            getGroupActions: function () {
                return site.apis.crm.concat("leads/getGroupActions/");
            },
        },
    },

    methods: {
        isDefined: function (object) {
            if (typeof (object) === "undefined")
                return false;
            else
                return true;
        },
        isFunction: function (object) {
            if (typeof (object) === "function")
                return true;
            else
                return false;
        },
        getDistinctArrayFromObject: function (object, propName) {
            var arrayValue = object.map(function (obj) { return obj[propName]; });
            return site.methods.getDistinctArray(arrayValue);
        },
        getDistinctArray: function (arrayValue) {
            return arrayValue.filter(function (currentValue, index) {
                // indexOf() return the first index found of the current value
                // e.g. array = [17, 17, 35]
                // current value: 17 => Current index: 0 == Found index: 0
                // current value: 17 => Current index: 1 == Found index: 0
                // current value: 35 => Current index: 2 == Found index: 2
                return arrayValue.indexOf(currentValue) == index;
            });
        },
    },
}