
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