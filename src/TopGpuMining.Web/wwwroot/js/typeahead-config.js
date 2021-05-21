
$(function () {
    initTypeAhead();
})

function initTypeAhead() {

    $(".typeahead-auto").each(function (i) {

        var searchUrl = $(this).data("search-url");
        var resultTemplateSelector = $(this).data("template-id");
        var emptyTemplateSelector = $(this).data("empty-template-id");
        var emptyTemplate;

        if (!searchUrl) {
            console.warn("typeahead taghelper missing search url");
            return;
        }

        if (!resultTemplateSelector) {
            console.warn("typeahead taghelper missing template id");
            return;
        }

        if (emptyTemplateSelector) {
            emptyTemplate = Handlebars.compile($(emptyTemplateSelector).html());
        }

        var resultTemplate = Handlebars.compile($(resultTemplateSelector).html());

        var searchEngine = new Bloodhound({

            datumTokenizer: Bloodhound.tokenizers.whitespace,
            queryTokenizer: Bloodhound.tokenizers.whitespace,
            remote: {
                url: searchUrl,
                wildcard: '%QUERY'
            }
        });

        $(this).on("input", function () {

            var idSelector = $(this).data("id");
            $('#' + idSelector).val('');

        });


        if (getTypeAheadLang() === "ar")
            $(this).attr("dir", "rtl");
        else
            $(this).attr("dir", "lrt")

        $(this).typeahead('destroy');


        $(this).typeahead({
            minLength: 1,
            hint: false,
            classNames: {
                open: 'is-open',
                empty: 'is-empty',
                cursor: 'is-active',
                suggestion: 'typeahead-suggestion',
                selectable: 'typeahead-selectable',
                menu: "typeahead-menu",
                dataset: "typeahead-list-group"
            }
        }, {

            name: 'posList',
            displayKey: 'name',
            limit: 5,
            source: searchEngine.ttAdapter(),
            templates: {
                notFound: emptyTemplate ? emptyTemplate : "",
                header: '',
                suggestion: resultTemplate,
                footer: ''
            },
        }).on('typeahead:asyncrequest', function () {
            $('.typeahead-spinner').show();
        }).on('typeahead:asynccancel typeahead:asyncreceive', function () {
            $('.typeahead-spinner').hide();
        }).on('typeahead:selected', function (e, datum) {


            var hiddenFieldId = "#" + $(this).data("id");

            $(hiddenFieldId).val(datum.id);

            var isAutoSubmit = $(this).data('auto-submit');

            if (isAutoSubmit && isAutoSubmit === "True") {

                $(this).closest('form').submit();
            }


        }).on("typeahead:select", function (ev, suggestion) {

        });


    });
}


function getTypeAheadLang() {

    if (isEnglish())
        return 'en';
    else
        return 'ar';
}