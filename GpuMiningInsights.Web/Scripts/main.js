

var editButtonSelector = "button[data-button-type='Edit']";
var saveButtonSelector = "button[data-button-type='Save']";
var cancelButtonSelector = "button[data-button-type='Cancel']";

var isEnglish = false;
var isGregorianCalendar = true;

$(function () {

    isEnglish = getLanguage() == "en";

    setupDeleteDialogs();
    //setupDate();
    setAutoSearch();
    equalHeightColumns();
    ////initICheck();
    //setupTime();
    //setupMenuSearch();
    setupConfirm();
    setupConfirmationWithUrl();

    //if (initTranslation != undefined) 
    //    initTranslation();

});

function block(element,timeout) {

    $(element).block(
        {
            message: "LOADING ..."
        }
    ); 
    
}

function unblock(element) {
    $(element).unblock(); 
}

function setupDeleteDialogs() {

    $('#confirm-delete').on('show.bs.modal', function (e) {
        //deleteText is Optional
        var deleteText = $(e.relatedTarget).data('delete-text');
        if (deleteText) {
            $(this).find('.modal-body p').text($(e.relatedTarget).data('delete-text'));
        }
        $(this).find('#btnConfirmDelete').attr('href', $(e.relatedTarget).data('url'));

    });

}

function setMetronicActiveLink() {

    var currentPath = location.pathname.toLocaleLowerCase();

    var selectedTag = '<span class="selected"></span>';

    $('.nav-item a').each(function (i, n) {

        var href = $(n).attr("href");
        href = href.toLowerCase();

        if (href.endsWith(currentPath)) {

            var anchor = $(n);

            var navToggle = anchor.parents('.nav-item');


            navToggle.addClass('active');
            navToggle.addClass('open');
            navToggle.find("a").append(selectedTag);

            $(n).closest("li").addClass('active');
            $(n).append(selectedTag);
        }
    });
}


//=============== BEGIN ALERTS ==================
function showAlert(alert, container) {
    debugger;
    var css = alert.alertCSS;
    var message = alert.message;
    var type = alert.alertTypeMetronic;
    var dismissable = alert.isAutoHide;

    console.log(container);
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "positionClass": "toast-top-full-width",
        "onclick": null,
        "showDuration": "1000",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }


    switch (type) {
    case "warning": //Warning
        toastr.warning(alert.Title, alert.message);
        break;
    case "info": //Info
        toastr.info(alert.Title, alert.message);
        break;
    case "success": //Success
        toastr.success(alert.Title, alert.message);
        break;
    case "danger": //Error
        toastr.error(alert.Title, alert.message);
        break;
    default:
        toastr.info(alert.Title, alert.message);
        break;
    }
  
}

function showAutoHideAlert(alert) {

    toastr.options = {
        "closeButton": true,
        "debug": false,
        "positionClass": "toast-top-full-width",
        "onclick": null,
        "showDuration": "1000",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }


    switch (alert.alertTypeMetronic) {
        case "warning": //Warning
            toastr.warning(alert.Title, alert.message);
            break;
        case "info": //Info
            toastr.info(alert.Title, alert.message);
            break;
        case "success": //Success
            toastr.success(alert.Title, alert.message);
            break;
        case "danger": //Error
            toastr.error(alert.Title, alert.message);
            break;
        default:
            toastr.info(alert.Title, alert.message);
            break;
    }

}
//=============== END ALERTS ==================



function changeFormActionUrl(button) {
    var url = $(button).attr('data-action-url');

    var form = $(button).closest('form');

    form.attr('action', url);

    return true;
}

function changeFormActionUrlAndStatus(button, status) {
    var url = $(button).attr('data-action-url');

    var form = $(button).closest('form');

    form.data("form-status", status);

    form.attr('action', url);

    return true;
}

//=========== Begin AJAX Helpers ================


function onAjaxBegin(blockDiv) {

    block(blockDiv);
}

function onAjaxFailed(xhr, status, error, alertDiv, formId) {

    var data = xhr.responseJSON;
    debugger;
    if (data) {

        if (data.alertMessage != null) {

            if (data.alertMessage.isAutoHide)
                showAutoHideAlert(data.alertMessage);
            else
                showAlert(data.alertMessage, alertDiv);
        }

    }

    $(formId).data("form-status", 'Edit');
}

function onAjaxSuccess(xhr, status, modalToHide) {
    if (modalToHide) {
        $(modalToHide).modal('hide');
    }
}

function onAjaxComplete(xhr, status, blockDiv, alertDiv, divToReplace, formId,callBack) {

    var data = xhr.responseJSON;
    //this is due to when JSonResultObject is coming back as XHR
    if (!data) {
        data = xhr;
    }
    if (data) {

        if (data.isRedirect)
            window.location.href = data.redirectUrl;


        if (data.success) {

            if (data.alertMessage != null) {

                if (data.alertMessage.isAutoHide)
                    showAutoHideAlert(data.alertMessage);
                else
                    showAlert(data.alertMessage, alertDiv);
            }

            if (data.partialViewHtml)
                $(divToReplace).html(data.partialViewHtml);
            else
                $(divToReplace).html(data.responseText);
        }

        setFormStatus(formId);


    }

    //setupDate();

    //setupTime();

    //initICheck();

    //initSelect2();

    setupConfirm();
    //setupSlimScroll();
    //if (status == 'success')
    //    $(formId).data("form-status", 'Read');

    unblock(blockDiv);
    if (callBack)
    callBack(data.data);




}

function setupSlimScroll() {
    App.initSlimScroll('.scroller');
}
function refreshList(url, divToUpdate) {

    $.ajax(url,
        {
            method: "GET",
            beforeSend: function () {

                block(divToUpdate);
            },
            complete: function (xhr, status) {

                unblock(divToUpdate);
            },

            success: function (data, status, xhr) {

                $(divToUpdate).html(data);

            },
            error: function (xhr, status, error) {

            }

        });

}

function refreshList(url, divToUpdate, formData) {

    $.ajax(url,
        {
            method: "GET",
            data: formData,
            beforeSend: function () {

                block(divToUpdate);
            },
            complete: function (xhr, status) {

                unblock(divToUpdate);
            },

            success: function (data, status, xhr) {

                $(divToUpdate).html(data);

            },
            error: function (xhr, status, error) {

            }

        });

}
//=========== END AJAX Helpers ================


function enableEdit(form) {

    var formId = "#" + $(form).attr("id");
    enableInputs(formId);

    $(form).find(editButtonSelector).hide();
    $(form).find(saveButtonSelector).show();
    $(form).find(cancelButtonSelector).show();



}

function disableEdit(form) {

    if (form) {

        $(form).find(editButtonSelector).show();
        $(form).find(saveButtonSelector).hide();
        $(form).find(cancelButtonSelector).hide();

        var formId = "#" + $(form).attr("id");
        disableInputs(formId);
    }
}

function disableInputs(form) {

    $(form + ' input, ' + form + ' select, ' + form + ' textarea ').each(
        function (i) {
            $(this).prop('disabled', true);
        });
}

function enableInputs(form) {
    //$(form + " :input[type='text']").prop('disabled', false);

    $(form + ' input, ' + form + ' select, ' + form + ' textarea ').each(
        function (i) {
            $(this).prop('disabled', false);
        });
    //" + form + ":input[type='radio']," + form + " textarea," + form + " select"
}

function setFormStatus(formId) {

    if ($(formId).data("form-status") == "Edit") {
        enableEdit(formId);
    }
    else {
        disableEdit(formId);
    }

    refreshUnobtrusiveValidation(formId);
}


function getCulture() {

    if (isEnglish)
        return "en-us";
    else
        return "ar-sa";
}

function refreshUnobtrusiveValidation(formSelector) {
    var form = $(formSelector);
    form.removeData('validator');
    form.removeData('unobtrusiveValidation');
    $.validator.unobtrusive.parse(form);

    $(formSelector).validateBootstrap(true);
}




//=========== Confirm Delete  ================
function confirmDelete(element, url, divToBlock) {
    var item = $(this);
    var errorMsg = "خطأ";
    var listToBlock;

    $('#confirm-delete').find('#btnConfirmDelete').on('click', function (e) {
        e.preventDefault();
        $.ajax({
            method: "POST",
            url: url,
            beforeSend: function () {
                block(divToBlock);
            },
            complete: function (jqXHR, textStatus) {
                onAjaxComplete(jqXHR, textStatus, divToBlock, '#alert', divToBlock)
            },
            success: function (data, textStatus, jqXHR) {
                onAjaxSuccess(jqXHR, textStatus, '#confirm-delete')
            }
        }).fail(function (jqXHR, textStatus, errorThrown) {
            onAjaxFailed(jqXHR, textStatus, errorThrown, "#alert")
        });
    });
}


function clearForm(form) {

    // iterate over all of the inputs for the form

    // element that was passed in



    $(':input', form).each(function () {

        var type = this.type;

        var tag = this.tagName.toLowerCase(); // normalize case

        // it's ok to reset the value attr of text inputs,

        // password inputs, and textareas

        if (type == 'text' || type == 'password' || tag == 'textarea') {
            this.value = "";

            // checkboxes and radios need to have their checked state cleared

            // but should *not* have their 'value' changed
        }

        else if (type == 'checkbox' || type == 'radio')

            this.checked = false;

        // select elements need to have their 'selectedIndex' property set to -1

        // (this works for both single and multiple select elements)

        else if (tag == 'select')

            this.selectedIndex = 0;

    });


    ////clear Icheck
    //$('.icheck', form).each(function () {
    //    $(this).iCheck('uncheck');
    //});

};

function DoPrevent(e) {
    e.preventDefault();
    e.stopPropagation();
}


function creaDevAjax(blockDiv, alertDiv, divToReplace, dataToSend, method, url,callback) {

    onAjaxBegin(blockDiv);
    $.ajax({
        url: url,
        method: method,
        data: dataToSend,

    })
        .done(function (data) {
            onAjaxSuccess(data);
        })
        .fail(function (xhr) {
            onAjaxFailed(xhr);
        })
        .always(function (xhr, status) {
            onAjaxComplete(xhr, status, blockDiv, alertDiv, divToReplace,'', callback);
        });
}

//============ Mini Tabs =====================================
function initMiniTabs() {
    $('.mini-tab-navigation').each(function () {
        var targetNavSelector = '#' + $(this).data('target-tabs');
        var listItems = '';
        $(targetNavSelector + ' li a').each(function () {
            listItems += '<option value="' + $(this).attr('href') + '">' + $(this).html() + '</options>';
        });
        $(this).html(listItems);
    });

}

function onMiniTabChange(miniTabSelector, tabSelector) {
    var targetTab = $(miniTabSelector + ' option:selected').val();
    //$(tabSelector+' a[href="' + targetTab  + '"]').tab('show');
    $(tabSelector + ' a[href="' + targetTab + '"]').click();
    //var onclick =$(tabSelector + ' a[href="' + targetTab + '"]').attr('onclick');


}
//============ Localization =====================================

function getLocalizedValue(localizableObject) {
    if (isEnglish)
        return localizableObject.english;

    localizableObject.arabic;
}
function getLanguage() {
    return window.location.pathname.slice(1, 3).toLowerCase();
}

function getCalendarText(isHijri) {
    var hijrString = "هجري";
    var hijrStringEn = "Hijri";
    var gregString = "ميلادي";
    var gregStringEn = "Gregorian";

    if (isEnglish) {

        if (isHijri)
            return hijrStringEn;
        else
            return gregStringEn;
    }
    else {

        if (isHijri)
            return hijrString;
        else
            return gregString;
    }
}

function equalHeightColumns(isTimeout) {
    var timeout = 1;
    if (isTimeout)
        timeout = 200;
    setTimeout(function () {
        $(".equal-height-columns").each(function () {
            heights = [];
            $(".equal-height", this).each(function () {
                $(this).removeAttr("style");
                heights.push($(this).height()); // write column's heights to the array
            });
            $(".equal-height", this).height(Math.max.apply(Math, heights)); //find and set max
        });

    }, timeout);

}

function setAutoSearch() {
    $('form.auto-search').each(function () {
        var theForm = $(this);
        var inputs = $(theForm).find('input');
        var minCharacters = 3;
        for (var i = 0; i < inputs.length; i++) {
            var input = inputs[i];
            var tagName = $(input).prop('tagName');
            var isHidden = $(input).attr('type') == 'hidden';
            if (tagName == 'INPUT' && !isHidden) {
                if ($(input).attr('type') == 'text') {
                    $(input).keyup(function () {
                        if ($(this).val().length == 0 || $(this).val().length >= minCharacters) {
                            $(theForm).submit();

                        }
                    });

                } else {
                    $(input).change(function () {
                        $(theForm).submit();
                    });
                }
            }

        }
    });
}

function forceClearModal(selector) {

    $(selector).on('hidden.bs.modal', function () {
        $(this).data('bs.modal', null);
    });

    //$(selector).hide();
    $('.modal-backdrop').remove();
    $("body").removeClass("modal-open");

}


function setActiveLink() {

    var pathName = window.location.pathname;
    var anchor = $(".nav-link[href='" + pathName + "']")
    var li = anchor.closest(".chrs-main-link");
    li.addClass("active");

}


function updateNotification() {

    var url = getBaseLanguageUrl() + "/Home/GetNotification";

    $.ajax(url,
        {
            method: "GET",
            beforeSend: function () {

            },
            complete: function (xhr, status) {

            },

            success: function (data, status, xhr) {

                showManagerLeavePendingNotification(data.managerPendingLeaves);
                showLeavePendingNotification(data.pendingLeaves);

            },
            error: function (xhr, status, error) {

            }

        });
}

function showManagerLeavePendingNotification(count) {

    var selector = ".manager-pending-notification";

    if (count > 0) {
        $(selector).html(count);
        $(selector).removeClass('hidden');
    }
    else {
        $(selector).addClass('hidden');
        $(selector).html('');
    }

}

function showLeavePendingNotification(count) {

    var selector = ".leave-pending-notification";


    if (count > 0) {
        $(selector).html(count);
        $(selector).removeClass('hidden');
    }
    else {
        $(selector).addClass('hidden');
        $(selector).html('');
    }

}
function getUserNotifications() {

    var url = getBaseLanguageUrl() + "/Home/GetUserNotifications";

    $.ajax(url,
        {
            method: "GET",
            beforeSend: function () {

            },
            complete: function (xhr, status) {

            },

            success: function (data, status, xhr) {

                userNotificationsLoaded(data);

            },
            error: function (xhr, status, error) {

            }

        });
}

function UserNotificationsAreRead() {
    var url = getBaseLanguageUrl() + "/Home/UserNotificationsAreRead";

    $.ajax(url,
        {
            method: "GET",
            beforeSend: function () {

            },
            complete: function (xhr, status) {

            },

            success: function (data, status, xhr) {


            },
            error: function (xhr, status, error) {

            }

        });
}
function userNotificationsLoaded(notifications) {
    var notificationslistContainerSelector = '#notifications-list-container';
    var totalNew = 0;
    var headerNotificationBarheaderNotificationBarAnchorSelector = '#header_notification_bar > a';
    var notificationsHtml = '';
    var numberOfNotificationsBadgeId = 'notifications-badge';
    var newNotificationCountSelector = '#new-notifcations-count';
    for (var i = 0; i < notifications.length; i++) {
        var notification = notifications[i];
        var notificationTime = notification.timeAsText;
        var notificationClass = 'label-' + notification.type;
        var notificationText = notification.body.arabic;
        if (isEnglish)
            notificationText = notification.body.english;
        var notificationUrl = 'javascript:;';
        var notificationAnchorClass = 'hover-no-pointer';
        if (notification.url && notification.url.length>1) {
            notificationUrl = notification.url;
            notificationAnchorClass = '';
        }
        var fontClass = '';
        if (!notification.isRead) {
            totalNew++;
            fontClass = 'font-blue';
        }
        var notificationHtml = '<li>'
            + '<a href= "' + notificationUrl + '" class=' + notificationAnchorClass+'>'
            + '<span class="time">' + notificationTime + '</span>'
            + '<span class="details ' + fontClass+'">'
            + '<span class="label label-sm label-icon ' + notificationClass + '">'
            + '<i class="' + fontClass+' fa fa-info-circle"></i>'
            + '</span> ' + notificationText
            + '</span>'
            + '</a>'
            + '</li>';

        
        notificationsHtml += notificationHtml;

    }
    $(newNotificationCountSelector).html(totalNew);

        $(numberOfNotificationsBadgeId).remove();
    if (totalNew > 0) {
        var numberOfNotificationsBadge = '<span id=' +
            numberOfNotificationsBadgeId +
            ' class="badge badge-default">' +
            totalNew +
            '</span>';
        $(headerNotificationBarheaderNotificationBarAnchorSelector).append(numberOfNotificationsBadge);
    }
    $(notificationslistContainerSelector).html(notificationsHtml);
   
}
function checkUserNotificationsPeriodically(timeInMilliSeconds) {
        getUserNotifications();
        setTimeout(function () {
            checkUserNotificationsPeriodically(timeInMilliSeconds);
        }, timeInMilliSeconds);

}
function onNotificationsBellClicked() {
    UserNotificationsAreRead();

}
function updateContainer(url, divToUpdate, formData) {
    var result = $.Deferred();

    $.ajax(url,
        {
            method: "GET",
            data: formData,
            beforeSend: function () {
                block(divToUpdate);
            },
            complete: function (xhr, status) {
                unblock(divToUpdate);
            },
            success: function (data, status, xhr) {
                if (typeof data === 'object') {
                    $(divToUpdate).html(data.partialViewHtml);
                } else {
                    $(divToUpdate).html(data);
                }

                result.resolve(data);
            },
            error: function (xhr, status, error) {
                result.reject;
            }
        });

    return result.promise();
}

//TimePicker

function setupTime() {
    $(".timepicker-default").timepicker({
        autoclose: !0,
        showSeconds: false,
        minuteStep: 1,
        showMeridian: false,
        defaultTime: false
    });
}

function setupMenuSearch() {
    var results = '';
    $('.hor-menu .nav.navbar-nav > li').each(function () {
        var isOptGroup = false;
        var subMenu = $(this).find('> ul > li');
        var hasChilds = subMenu.length > 0;
        var mainitem = '';
        if (hasChilds) {

            mainitem = '<optgroup label="' + $(this).find('> a').text() + '">';
            var mainChilds = '';
            $(this).find('> ul > li').each(function () {
                var isOptGroup = false;
                var subMenu = $(this).find('> ul > li');
                var hasChilds = subMenu.length > 0;
                var text = $(this).find('> a').text();
                var link = $(this).find('> a').attr('href');
                if (text != '')
                    mainChilds += '<option value="' + link + '">' + text + '</option>';
                if (hasChilds) {
                    subMenu.each(function () {
                        var text = $(this).find('> a').text();
                        var link = $(this).find('> a').attr('href');
                        if (text != '')
                            mainChilds += '<option value="' + link + '">' + text + '</option>';
                    });
                }

            });

            mainitem += mainChilds + '</optgroup>';

        } else {
            mainitem = '<option value="' + $(this).find('> a').attr('href') + '">' + $(this).find('> a').text() + '</option>';

        }
        results += mainitem;
        $('#select2-button-addons-multiple-input-group-sm').html(results);

        $('#select2-button-addons-multiple-input-group-sm').change(function () {
            location.href = $(this).val();
        });

    });
}
function onSearchMenuClick() {

    location.href = $('#select2-button-addons-multiple-input-group-sm').val();

}
function setupConfirmationWithUrl() {
    $('button[data-toggle="confirmation"]').each(function () {
        var url = $(this).data('confirm-url');
        if (url) {
            $(this).on("confirmed.bs.confirmation", function () {
                location.href = url;
            });
        }

    });
}


function refreshCookies() {

    var url = "/Home/RefreshCookies"

    $.ajax(url,
        {
            method: "GET",

            beforeSend: function () {

            },
            complete: function (xhr, status) {
                console.log(xhr.status);

            },

            success: function (data, status, xhr) {
                console.log("");
            },
            error: function (xhr, status, error) {
                alert('error');
            }

        });
}



function showModal(item) {

    $(item).modal('show');
}

function hideModal(item) {

    $(item).modal('hide');
}

function setupConfirm() {

    $('#confirm-modal').on('show.bs.modal', function (event) {

        var button = $(event.relatedTarget);

        var action = button.data('action');

        $("#confirm-form").attr("action", action);
    });

    $('#confirm-ajax-modal').on('show.bs.modal', function (event) {

        var button = $(event.relatedTarget);

        var action = button.data('action');
        var completeMethod = button.data('complete');

        $("#confirm-ajax-form").attr("action", action);

        var completeAttr = $("#confirm-ajax-form").attr("data-ajax-complete");

        completeAttr = completeAttr.replace('CompleteMethoToReplace', completeMethod);

        $("#confirm-ajax-form").attr("data-ajax-complete",completeAttr);

    });
    
    $('#confirm-ajax-modal').on('hidden.bs.modal', function (event) {

        debugger;
        var defaultMethod = "onAjaxComplete(xhr, status, '#confirm-ajax-content', '#alert', '', '');CompleteMethoToReplace";

        $("#confirm-ajax-form").attr("action", '');
        $("#confirm-ajax-form").attr("data-ajax-complete", defaultMethod);

    });
}

function executeFunctionByName(functionName, context,args) {

    var args = Array.prototype.slice.call(arguments, 2);

    var namespaces = functionName.split(".");

    var func = namespaces.pop();

    for (var i = 0; i < namespaces.length; i++) {
        context = context[namespaces[i]];
    }

    return context[func].apply(context, args);
}

function addQueryToUrl(url, query) {

    url += (url.split('?')[1] ? '&' : '?') + query;

    return url;
}
