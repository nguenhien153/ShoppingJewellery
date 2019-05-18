
function table_inital(table_name) {
    $("#" + table_name + "_filter").addClass("form-inline");
    $("#" + table_name + "_filter").css("margin-top", "14px");
    $("#" + table_name + "_filter").css("margin-bottom", "10px");
    $("#" + table_name + "_filter label input").addClass("form-control");

    $("#" + table_name + "_length").css("margin-top", "14px");
    $("#" + table_name + "_length").css("margin-bottom", "10px");
    $("#" + table_name + "_length").addClass("form-inline");
    $("#" + table_name + "_length label select").addClass("form-control");
}


function remove_ClassButton() {
    $("button.dt-button").css({
        "background-color": "rgba(0,0,0,0)"
    });
    $("button.dt-button").attr("data-toggle", "tooltip");
    $("button.dt-button").removeClass("dt-button");
    $("div.dt-buttons").css({
        "margin-right": "20px"
    });
    $('[data-toggle="tooltip"]').tooltip();
}

function Currentcc(id) {
    var num = $(id).text();
    var commaNum = numberWithCommas(num);
    $(id).text(commaNum);
};

function default_currentInput(id) {
    $(id).val($(id).val().replace(/,/g, ""));
}
function CurrentccforValue(id) {
    var num = parseFloat($(id).val().replace(/,/g, "")).toFixed(2);
    var commaNum = numberWithCommas(num);
    $(id).val(commaNum);
};

function CurrentccforPassValue(id, value) {
    var num = parseFloat(value.toString().replace(/,/g, "")).toFixed(2);
    var commaNum = numberWithCommas(num);
    $(id).val(commaNum);
};
function Currentcc(add, add2) {
    var num = $(add).text();
    var commaNum = numberWithCommas(num);
    $(add).text(commaNum);
    var num2 = $(add2).text();
    var commaNum2 = numberWithCommas(num2);
    $(add2).text(commaNum2);
};

function numberWithCommas(number) {
    var parts = number.toString().split(".");
    parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    return parts.join(".");
};


function refreshpage(id) {
    $(id).on('hidden.bs.modal', function () {
        location.reload();
    });
}

function readonlyInput() {
    $('input[readonly]').each(function () {
        if ($(this).prop('disabled') == false) {
            $(this).css("background", "#fff");
        }
    });
    $('input[readonly]').parent(".form-inline").each(function () {
        $(this).children("input").eq(1).css("background", "#e9ecef");
    });
}