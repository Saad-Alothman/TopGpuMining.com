var editButtonSelector = "button[data-button-type='Edit']";
var saveButtonSelector = "button[data-button-type='Save']";
var cancelButtonSelector = "button[data-button-type='Cancel']";

$(function () {

    supportIE11();
    
    initInlineForms();
    setActiveLinks();
    setupConfirm();
    setupResetForms();

   
});

//========= Start Ajax Setting =========

function block(element) {


    if (KTApp) {

        KTApp.block(element, {
            overlayColor: '#000000',
            size: 'lg',
            message: 'Please wait...'

        });
    }
    else {


        var html = '<div class="loading-message loading-message-boxed"><img src="/images/loading-spinner-grey.gif" align=""><span>&nbsp;&nbsp; </span></div>';
        var options = {};


        var el = $(element);

        if (el.height() <= ($(window).height())) {
            options.cenrerY = true;
        }

        el.block({
            message: html,
            baseZ: 1000,
            centerY: options.cenrerY !== undefined ? options.cenrerY : false,
            css: {
                top: '10%',
                border: '0',
                padding: '0',
                backgroundColor: 'none'
            },
            overlayCSS: {
                backgroundColor: '#555',
                opacity: 0.5,
                cursor: 'wait'
            }
        });
    }
}

function unblock(element) {

    $(element).unblock();
}

function onAjaxBegin(blockDiv) {

    block(blockDiv);
}

function onAjaxFailed(xhr, status, error, alertDiv, formId) {

    var data = xhr.responseJSON;

    if (!data) {
        data = xhr;
    }

    var isInline = $(formId).data("inline-edit");

    if (isInline) {
        $(formId).data("form-status", "Edit");

    }

    if (alertDiv)
        scrollToEle(alertDiv, 500);

}

function onAjaxSuccess(xhr, status, modalToHide) {
    if (modalToHide) {
        $(modalToHide).modal('hide');
    }
}

function onAjaxComplete(xhr, status, blockDiv, alertDiv, divToReplace, formId) {
    
    var data = xhr.responseJSON;

    if (!data) {
        data = xhr;
    }

    if (data) {

        if (data.isRedirect)
            window.location.href = data.redirectUrl;
        
        if (data.success || status === "success") {

            if (data.partialViewHtml)
                $(divToReplace).html(data.partialViewHtml);
            else
                $(divToReplace).html(data.responseText);

        }

        showAlert(data.alert, alertDiv);

        setFormStatus(formId);
    }

    if (formId)
        refreshUnobtrusiveValidation(formId);

    setupDate();
    //initICheck();
    //FormRepeater.init();
    //initAspSelect2(formId);
    //initSelect2Multiple();

    unblock(blockDiv);


}

//========= End Ajax Setting =========

//========= Start Inline Form =========

function enableEdit(form) {

    var formId = "#" + $(form).attr("id");

    enableInputs(formId);

    $(form).find(editButtonSelector).hide();
    $(form).find(saveButtonSelector).show();
    $(form).find(cancelButtonSelector).show();

    //updateICheck();
}

function disableEdit(form) {

    if (form) {

        $(form).find(editButtonSelector).show();
        $(form).find(saveButtonSelector).hide();
        $(form).find(cancelButtonSelector).hide();

        var alert = $(form).find('[data-alert]');

        if (alert)
            $(alert).html('');

        var formId = "#" + $(form).attr("id");

        disableInputs(formId);

        //updateICheck();
    }

}

function disableInputs(form) {

    $('input,select,textarea', form).each(
        function (i) {

            if (jQuery().bootstrapSwitch && $(this).hasClass('make-switch')) {

                if (!$(this).bootstrapSwitch('disabled')) {
                    $(this).bootstrapSwitch('toggleDisabled');
                }
            }
            else {

                $(this).prop('disabled', true);
                $(this).attr('disabled', 'disabled');
            }
        });

}

function enableInputs(form) {

    $('input,select,textarea', form).each(
        function (i) {
            if (jQuery().bootstrapSwitch && $(this).hasClass('make-switch')) {

                if ($(this).bootstrapSwitch('disabled')) {
                    $(this).bootstrapSwitch('toggleDisabled');
                }
            }
            else {
                $(this).prop('disabled', false);
                $(this).removeAttr('disabled');
                $(this).prop('readonly', false);

            }
        });

}

function setFormStatus(formId) {

    var isInline = $(formId).data("inline-edit");

    if (!isInline)
        return;

    if ($(formId).data("form-status") === "Edit") {
        enableEdit(formId);
    }
    else {
        disableEdit(formId);
    }
}

function changeFormActionUrlAndStatus(button, status, isPost) {

    var url = $(button).attr('data-action-url');

    var form = $(button).closest('form');

    form.data("form-status", status);


    if (isPost)
        form.attr('method', 'post');
    else
        form.attr('method', 'get');

    form.attr('action', url);

}

function initInlineForms() {

    $("form").each(function () {

        var isInlineForm = $(this).attr("data-inline-edit");

        if (isInlineForm) {

            var id = $(this).attr("id");

            setFormStatus("#" + id);
        }

    });

}

//========= End Inline Form =========

function isEnglish() {

    var url = window.location.href;

    var index = url.indexOf('/en/');

    var isEnglish = index > -1;

    if (isEnglish)
        return true;
    else
        return false;
}

function setupConfirm() {

    $('#confirm-modal').on('show.bs.modal', function (event) {

        var button = $(event.relatedTarget);

        var action = button.data('action');

        $("#confirm-form").attr("action", action);
    });

    $('#confirm-ajax-modal').on('show.bs.modal', function (event) {

        var formSelector = "#confirm-ajax-form";

        $("#confirm-ajax-alert").html("");

        var button = $(event.relatedTarget);

        var action = button.data('action');
        var complete = button.data('complete');
        var success = button.data('success');

        if (complete)
            $(formSelector).attr("data-ajax-complete", complete);

        if (success)
            $(formSelector).attr("data-ajax-success", success);


        $(formSelector).attr("action", action);
    });
}

function updateListFromDiv(div) {

    var url = getRouteUrl(div);

    updateContainer(url, div);
}

function updateContainer(url, divToUpdate) {

    var result = $.Deferred();
    
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

function ajaxRequest(url, data, divToBlock, httpMethod) {

    if (!httpMethod) {
        httpMethod = 'GET';
    }
    if (!data) {
        data = {};
    }
    var result = $.Deferred();

    $.ajax(url,
        {

            data: data,
            method: httpMethod,
            beforeSend: function () {
                block(divToBlock);
            },
            complete: function (xhr, status) {
                unblock(divToBlock);
            },
            success: function (data, status, xhr) {
                result.resolve(data);
            },
            error: function (xhr, status, error) {
                result.reject;
            }
        });
    return result.promise();
}
function setupResetForms() {

    var selector = '[data-reset-validation="true"]';

    $(selector).each(function (i) {

        $(this).on('hidden.bs.modal', function (event) {

            var form = $(this).find('form');
            var alert = $(this).find('[data-alert]');

            if (alert) {
                $(alert).html('');
            }

            if (form) {
                clearFromData(form);
                clearFormValidation(form);
                refreshUnobtrusiveValidation(form);
            }
        });
    });

}

function clearFromData(formId) {

    $(formId).trigger('reset');

    $(':input', formId)
        .not(':button, :submit,:checkbox, :radio, :reset, :input[type=hidden][data-reset!="True"]')
        .removeAttr('checked')
        .removeAttr('selected')
        .val('');
}

function clearFormValidation(formId) {
    
    var form = $(formId);

    $('label, input,select, .invalid-feedback', form).each(function (i) {

        $(this)
            .removeClass('is-valid')
            .removeClass('text-danger')
            .removeClass('text-success')
            .removeClass('is-invalid');
        
    });

    //Clear validation summary
    form.find("[data-valmsg-summary=true]")
        .removeClass("validation-summary-errors")
        .addClass("validation-summary-valid")
        .find("ul").empty();

    //reset unobtrusive field level, if it exists
    form.find("[data-valmsg-replace]")
        .removeClass("field-validation-error")
        .addClass("field-validation-valid")
        .empty();

    form.find("[data-val=true]")
        .removeClass("text-danger");
    
    form.find('[data-alert]').empty();
}

function setActiveLinks() {

    var currentPath = location.pathname.toLocaleLowerCase();

    $('.kt-menu__item a').each(function (i, n) {

        var href = $(n).attr("href");

        if (href) {
            href = href.toLowerCase();

            if (href.endsWith(currentPath)) {

                var anchor = $(n);

                var navToggle = anchor.parents('.kt-menu__item');
                
                navToggle.addClass('kt-menu__item--active');
                navToggle.addClass("kt-menu__item--open");
            }
        }
    });
}

function showModal(modal) {

    $(modal).modal('show');
}

function refreshUnobtrusiveValidation(formSelector) {
    var form = $(formSelector);
    form.removeData('validator');
    form.removeData('unobtrusiveValidation');
    $.validator.unobtrusive.parse(form);
    
}

function scrollToEle(ele, speed) {

    if (!speed)
        speed = 800;

    $('html, body').animate({
        scrollTop: $(ele).offset().top - 70
    }, speed, function () {

        //window.location.hash = ele;
    });

}

function getUrl(url) {

    if (isEnglish())
        return "/en" + url;
    else
        return "/ar" + url;

}

function getRouteUrl(div) {

    return $("input[data-route]", div).val();

}

function supportIE11() {

    String.prototype.endsWith = function (s) {
        return this.length >= s.length && this.substr(this.length - s.length) == s;
    }

}
