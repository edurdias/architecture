function autoCompleteFor(inputName, selectedInputName, url) {

    $("#" + inputName).autocomplete({
        source: function (request, response) {
            $.getJSON(url, { keyword: request.term }, function (data) {
                response($.map(data, function (item) {
                    return {
                        label: item.Name,
                        value: item.Id
                    };
                }));
            });
        },
        minLength: 0,
        select: function (event, ui) {
            $("#" + selectedInputName).val(ui.item.value);
            $("#" + inputName).val(ui.item.label);
            return false;
        }
    });
}