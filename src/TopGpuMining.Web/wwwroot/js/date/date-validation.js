
var dateRuleName = 'greaterthanvld';

$(function () {


    $.validator.addMethod('date', function (value, element) {

        var isValid = moment(value, 'YYYY-MM-DD').isValid();

        if (!isValid) {
            isValid = /\d{1,2}-\d{4}$/.test(value);
        }

        return isValid;
    });

    initDateValidation();

});


function initDateValidation() {

    if ($.validator && $.validator.unobtrusive) {

        addDateAdapters();

        addDateValidations();

        refreshUnobtrusiveValidation('form');
    }
}

function addDateAdapters() {

    $.validator.unobtrusive.adapters.add(
        dateRuleName,
        ["propertyToCompare"],
        function (options) {

            options.rules[dateRuleName] = options.params;
            options.messages[dateRuleName] = options.message;
        }
    );

}

function addDateValidations() {

    $.validator.addMethod(dateRuleName, function (value, element, params) {

        var propertyToCompareName = params.propertyToCompare;

        if (!propertyToCompareName)
            return;

        var toCompareElement = getFormElementByName(propertyToCompareName, element.form);

        if (!toCompareElement)
            return;

        initPropertyToCompareValidation(toCompareElement, element);

        var toCompareDatePicker = $(toCompareElement).data("HijriDatePicker");
        var selectedDatePicker = $(element).data("HijriDatePicker");

        var selectedDate = selectedDatePicker.date();
        var toCompareDate = toCompareDatePicker.date();

        var result = selectedDate >= toCompareDate;

        return result;

    });


}

function initPropertyToCompareValidation(input, element) {

    $(input).off('dp.change');
    $(input).on('dp.change', function (e) {
        $(element).valid();
    });
}


function getFormElementByName(name, form) {

    for (var i = 0; i < form.length; i++) {
        if (form[i].name === name)
            return form[i];
    }
}

function getDatePickerValue(datepicker) {

    return datepicker.date();
}