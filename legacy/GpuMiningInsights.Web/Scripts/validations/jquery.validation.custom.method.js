
//$.validator.addMethod('date', function (value, element) {

//    var isValid = moment(value, 'iYYYY-iMM-iDD').isValid();

//    if (!isValid)
//        return false;
//    else
//        return true;
//});

//$(function () {

//    if ($.validator && $.validator.unobtrusive) {

//        $.validator.unobtrusive.adapters.addSingleVal("greaterthanvld", "propertytocompare");

//        $.validator.addMethod("greaterthanvld", function (value, element, propertyToCompare) {

//            var toCompareInput = $("input[name$='" + propertyToCompare + "'").closest(".date-picker");


//            var datePicker = $(element).closest('.date-picker').data("DateTimePicker");
//            var toCompareDatePicker = $("input[name$='" + propertyToCompare + "'").closest('.date-picker').data("DateTimePicker");

//            var toCompareValue = $("input[name$='" + propertyToCompare + "'").val();
            

//            initPropertyToCompareValidation(toCompareInput, element);
//            var selectedDate;
//            var toCompareDate;


//            if (datePicker.hijri()) {
//                selectedDate = moment(value, 'iYYYY-iMM-iDD');
//                toCompareDate = moment(toCompareValue, 'iYYYY-iMM-iDD')
//                return selectedDate > toCompareDate;

//            }
//            else {
//                selectedDate = moment(value, 'iDD-iMM-iYYYY');
//                toCompareDate = moment(toCompareValue, 'iDD-iMM-iYYYY');
//                return selectedDate > toCompareDate;
//            }

//        });

//    }

//    refreshUnobtrusiveValidation('form');

//})


//function initPropertyToCompareValidation(input,element) {

//    $(input).off('dp.change');
//    $(input).on('dp.change', function (e) {
//        $(element).valid();
//    });
//}