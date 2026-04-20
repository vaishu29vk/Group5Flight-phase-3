// Phase 2 — Bootstrap Date Range Picker initialization.
// Required by Phase 2 appendix: "The initialization JavaScript functions
// can be placed in site.js."
// Targets the #datePicker input on the SearchFlights filter panel.

$(function () {
    $('#datePicker').daterangepicker({
        singleDatePicker: true,
        showDropdowns: true,
        autoApply: true,
        timePicker: false,
        minDate: moment().add(1, 'days'), // default to tomorrow, no past dates
        locale: {
            format: 'MM/DD/YYYY'
        }
    });
});
