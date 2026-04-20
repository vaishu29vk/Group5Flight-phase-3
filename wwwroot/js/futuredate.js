jQuery.validator.addMethod("futuredate",
    function (value, element, param) {

        // get date value from user, ensure it's not empty
        if (value === '') return false;

        // string → Date using moment.js (YYYY-MM-DD from date input)
        var dateToCheck = moment(value, "YYYY-MM-DD", true);
        if (!dateToCheck.isValid()) return false;

        // get number of years from param
        var years = Number(param);

        // get current date
        var now = moment().startOf("day");

        // calculate max allowed date (today + years)
        var maxDate = moment().add(years, "years").startOf("day");

        // must be strictly after today AND on or before maxDate
        if (dateToCheck.isAfter(now) && dateToCheck.isSameOrBefore(maxDate))
            return true;

        return false;
    });

jQuery.validator.unobtrusive.adapters.addSingleVal("futuredate", "years");
