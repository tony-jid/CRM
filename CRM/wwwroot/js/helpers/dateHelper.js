var dateHelper = {
    formats: {
        SHORT_MONTH_STR: "D MMM YYYY",
    },

    items: {
        getDateRange: function (todayIncluded) {
            var dateRange = [
                {
                    text: "This Week", value: {
                        start: moment().startOf("isoWeek").format(dateHelper.formats.SHORT_MONTH_STR),
                        end: moment().endOf("isoWeek").format(dateHelper.formats.SHORT_MONTH_STR),
                    }
                },
                {
                    text: "Last Week", value: {
                        start: moment().subtract(1, "weeks").startOf("isoWeek").format(dateHelper.formats.SHORT_MONTH_STR),
                        end: moment().subtract(1, "weeks").endOf("isoWeek").format(dateHelper.formats.SHORT_MONTH_STR),
                    }
                },
                {
                    text: "This Month", value: {
                        start: moment().startOf("month").format(dateHelper.formats.SHORT_MONTH_STR),
                        end: moment().endOf("month").format(dateHelper.formats.SHORT_MONTH_STR),
                    }
                },
                {
                    text: "Last Month", value: {
                        start: moment().subtract(1, "months").startOf("month").format(dateHelper.formats.SHORT_MONTH_STR),
                        end: moment().subtract(1, "months").endOf("month").format(dateHelper.formats.SHORT_MONTH_STR),
                    }
                },
                {
                    text: "This Year", value: {
                        start: moment().startOf("year").format(dateHelper.formats.SHORT_MONTH_STR),
                        end: moment().endOf("year").format(dateHelper.formats.SHORT_MONTH_STR),
                    }
                },
                {
                    text: "Last Year", value: {
                        start: moment().subtract(1, "years").startOf("year").format(dateHelper.formats.SHORT_MONTH_STR),
                        end: moment().subtract(1, "years").endOf("year").format(dateHelper.formats.SHORT_MONTH_STR),
                    }
                },
            ];

            if (!site.methods.isDefined(todayIncluded))
                todayIncluded = true;

            if (todayIncluded)
                dateRange.unshift({
                    text: "Today", value: {
                        start: moment().format(dateHelper.formats.SHORT_MONTH_STR),
                        end: moment().format(dateHelper.formats.SHORT_MONTH_STR),
                    }
                });

            return dateRange;
        },
    },
}