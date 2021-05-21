var datePickerSelector = ".hijri-date-picker";
var hijriFormat = 'iYYYY-iMM-iDD';
var gregFormat = 'DD-MM-YYYY';
var dateLang = 'ar-sa';

$(function () {

    setupDate();

});

function setupDate() {

    setupDateConfig();

    $(datePickerSelector).each(function () {

        var dateInput = this;

        initHijriDatePicker(dateInput);


    });


}

function setupDateConfig() {

    if (isEnglish()) {

        dateLang = "en-us";
    }
    else {

        dateLang = "ar-sa";
        
    }

}

function initHijriDatePicker(item) {

    $(item).hijriDatePicker(
        {
            locale: dateLang,
            format: getDateGregFromat(item),
            hijriFormat: getDateHijriFormat(item),
            dayViewHeaderFormat: 'MMMM YYYY',
            hijriDayViewHeaderFormat: 'iMMMM iYYYY',
            viewMode: getDateAttribute(item,"view-mode"),
            hijri: getDateAttribute(item,'default-hijri'),
            showClear: getDateAttribute(item, 'show-clear'),
            showClose: getDateAttribute(item,'show-close'),
            showTodayButton: getDateAttribute(item, 'show-today'),
            showSwitcher: getDateAttribute(item, 'show-switch'),
            viewDate: getDateAttribute(item, 'view-date'),
            minDate: getDateMinDate(item),
            maxDate: getDateMaxDate(item),
            useCurrent: false,
            collapse: false,
            
            //debug: true,
            //defaultDate: '2010-01-01',
            //viewDate: '1990-01-01',
            //maxDate: "2010-01-01",
            icons: {
                time: 'fa fa-clock text-primary',
                date: 'glyphicon glyphicon-calendar',
                up: 'fa fa-chevron-up text-primary',
                down: 'fa fa-chevron-down text-primary',
                previous: '<i class="fa fa-chevron-left text-primary"></i>',
                next: '<i class="fa fa-chevron-right text-primary"></i>',
                today: '<i class="fa fa-calendar-alt text-primary"></i>',
                clear: '<i class="fa fa-trash-alt text-warning"></i>',
                close: '<i class="fa fa-times text-danger"></i>',
                switch: '<i class="fa fa-sync-alt text-primary"></i>'
            },
        });
}

function getDateAttribute(item,attr) {

    return $(item).data(attr);

}

function getDateHijriFormat(item) {

    var selectType = $(item).attr("data-select-mode");

    var format = hijriFormat;

    if (selectType === 'month') {
        format = "iYYYY-iMM";
    }

    if (selectType === 'year') {
        format = "iYYYY";
    }

    if (selectType === 'date_time') {
        format = hijriFormat + " " + timeFormat;
    }

    return format;

}

function getDateGregFromat(item) {

    var selectType = $(item).attr("data-select-mode");

    var format = gregFormat;

    if (selectType === 'month') {
            format = "MM-YYYY";
    }

    if (selectType === 'year') {
        format = "YYYY";
    }

    if (selectType === 'date_time') {
        format = gregFormat + " " + timeFormat;
    }

    return format;

}

function getDateMinDate(item) {

    var defaultDate = '1950-01-01';
    var minDate = $(item).data('min-date');
    
    var isValid = moment(minDate, 'YYYY-MM-DD').isValid();

    if (!isValid)
        return defaultDate;

    var actualMinDate = moment(minDate, 'YYYY-MM-DD');
    var actualMaxMinDate = moment(defaultDate, 'YYYY-MM-DD');

    if (actualMaxMinDate <= actualMinDate)
        return minDate;
    else
        return defaultDate;
}

function getDateMaxDate(item) {

    var defaultDate = '2070-01-01';
    var maxDate = $(item).data('max-date');

    var isValid = moment(maxDate, 'YYYY-MM-DD').isValid();

    if (!isValid)
        return defaultDate;

    var actualMaxDate = moment(maxDate, 'YYYY-MM-DD');
    var actualMaxMaxDate = moment(defaultDate, 'YYYY-MM-DD');

    if (actualMaxMaxDate >= actualMaxDate)
        return maxDate;
    else
        return defaultDate;
}