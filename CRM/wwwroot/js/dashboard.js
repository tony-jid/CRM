var dashboard = {
    ids: {
        selectBoxLeadOverview: "#selectBoxLeadOverview",
        chartLeadOverview: "#chartLeadOverview"
    },

    instances: {
        selectBoxLeadOverview: function () {
            return $(dashboard.ids.selectBoxLeadOverview).dxSelectBox("instance");
        },
        chartLeadOverview: function () {
            return $(dashboard.ids.chartLeadOverview).dxChart("instance");
        },
    },

    handlers: {
        onSelectBoxLeadOverviewContentReady: function (e) {
            var firstDateRangeItem = e.component.getDataSource().items()[0];
            e.component.option("value", { start: firstDateRangeItem.value.start, end: firstDateRangeItem.value.end });
        },
        onSelectBoxLeadOverviewChanged: function (e) {
            var dateStart = e.value.start;
            var dateEnd = e.value.end;

            ajax.callers.crm(
                ajax.controllers.home.name
                , ajax.controllers.home.actions.getChartLeadOverview
                , ajax.controllers.home.actions.getChartLeadOverview.params(dateStart, dateEnd)
                , function (response) {
                    //console.log(response);
                    var dsChart = dashboard.methods.convertLeadOverviewForChart(dateStart, dateEnd, response);

                    dashboard.methods.setLeadOverviewDataSource(dsChart);
                    dashboard.methods.setLeadOverviewTitle(dateStart, dateEnd);
                    dashboard.methods.setLeadOverviewSeries();
                }
            );
        },
    },

    callbacks: {

    },

    methods: {
        convertLeadOverviewForChart: function (dateStart, dateEnd, dataItems) {
            var dsChart = [];

            dateStart = moment(dateStart, dateHelper.formats.SHORT_MONTH_STR);
            dateEnd = moment(dateEnd, dateHelper.formats.SHORT_MONTH_STR);
            var diffDays = dateEnd.diff(dateStart, "days");

            //console.log("diff days => " + diffDays);

            // Days amount > 30 then grouped by months
            // Days amount > 6 then grouped by weeks
            // other wise grouped by days
            //
            if (diffDays > 30) {
                for (var i = 0; i < chartFields.arguments.months.length; i++) {
                    var dsChartItem = { argument: chartFields.arguments.months[i] };

                    for (var j = 0; j < chartFields.values.status.keys.length; j++) {
                        dsChartItem[chartFields.values.status.keys[j]] = 0;

                        $.each(dataItems, function (index, value) {
                            if (value.ArgumentField == chartFields.arguments.months[i]
                                && value.ValueFieldName == chartFields.values.status.keys[j])
                                dsChartItem[chartFields.values.status.keys[j]] = value.ValueField;
                        });
                    }

                    dsChart.push(dsChartItem);
                }
            }
            else if (diffDays > 6) {
                for (var i = 0; i <= diffDays; i++) {
                    var dayNumber = i + 1;
                    dayNumber = dayNumber < 10 ? "0" + dayNumber : "" + dayNumber;
                    var dsChartItem = { argument: dayNumber };

                    for (var j = 0; j < chartFields.values.status.keys.length; j++) {
                        dsChartItem[chartFields.values.status.keys[j]] = 0;

                        $.each(dataItems, function (index, value) {
                            if (value.ArgumentField == dayNumber
                                && value.ValueFieldName == chartFields.values.status.keys[j])
                                dsChartItem[chartFields.values.status.keys[j]] = value.ValueField;
                        });
                    }

                    dsChart.push(dsChartItem);
                }
            }
            else {
                for (var i = 0; i < chartFields.arguments.days.length; i++) {
                    var dsChartItem = { argument: chartFields.arguments.days[i] };

                    for (var j = 0; j < chartFields.values.status.keys.length; j++) {
                        dsChartItem[chartFields.values.status.keys[j]] = 0;

                        $.each(dataItems, function (index, value) {
                            if (value.ArgumentField == chartFields.arguments.days[i]
                                && value.ValueFieldName == chartFields.values.status.keys[j])
                                dsChartItem[chartFields.values.status.keys[j]] = value.ValueField;
                        });
                    }

                    dsChart.push(dsChartItem);
                }
            }

            //console.log(dsChart);

            return dsChart;
        },
        setLeadOverviewDataSource: function (dsChart) {
            dashboard.instances.chartLeadOverview().option("dataSource", dsChart);
        },
        setLeadOverviewTitle: function (dateStart, dateEnd) {
            dashboard.instances.chartLeadOverview().option("title", "From <b>" + dateStart + "</b> to <b>" + dateEnd + "</b>");
        },
        setLeadOverviewSeries: function () {
            var valueFields = [];

            for (var i = 0; i < chartFields.values.status.keys.length; i++) {
                var valueFieldItem = {
                    valueField: chartFields.values.status.keys[i]
                    , name: chartFields.values.status.names[chartFields.values.status.keys[i]]
                }

                valueFields.push(valueFieldItem);
            }

            dashboard.instances.chartLeadOverview().option("series", valueFields);
        },
    },
}