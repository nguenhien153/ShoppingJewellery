

function show_hide(classs) {
    var ancestor = $(classs).closest("div");
    if ($(classs).attr("src") === "/Content/images/Icon/Admin/Show_Password.png") {
        $(classs).attr("src", "/Content/images/Icon/Admin/Hide_Password.png");
        $(ancestor).children("input").attr("type", "password");
        check = true;
    } else {
        $(classs).attr("src", "/Content/images/Icon/Admin/Show_Password.png");
        $(ancestor).children("input").attr("type", "text");
        check = false;
    }
}

function show_hide2(id) {
    var ancestor = $("#" + id).closest("div");
    if ($("#" + id).attr("src") === "/Content/images/Icon/Admin/Show_Password.png") {
        $("#" + id).attr("src", "/Content/images/Icon/Admin/Hide_Password.png");
        $(ancestor).children("input").attr("type", "password");
        check = true;
    } else {
        $("#" + id).attr("src", "/Content/images/Icon/Admin/Show_Password.png");
        $(ancestor).children("input").attr("type", "text");
        check = false;
    }
}



function validateInput(re, email) {
    return re.test(email);
}
function validate(inputt, resultt, re) {
    var $result = $("#" + resultt);
    var input = $("#" + inputt).val();
    var inputte = $("#" + inputt);
    var ancestor = $("#" + inputt).closest("div");
    var ancestor2 = $(ancestor).parent();
    var text = $(ancestor2).children("label").text();
    $result.text("");
    if (validateInput(re, input)) {
        inputte.css('border', '1px solid #ced4da');
        varr = true;
    } else {
        $result.text(text + " is not valid ");
        $result.css("color", "red");
        inputte.css('border', '1px solid red');
        varr = false
    }
    return varr;
}

function validateMinMax(inputt, resultt, re, min, max) {
    var $result = $("#" + resultt);
    var input = $("#" + inputt).val();
    var inputte = $("#" + inputt);
    var ancestor = $("#" + inputt).closest("div");
    var ancestor2 = $(ancestor).parent();
    var text = $(ancestor2).children("label").text();
    $result.text("");
    if (validateInput(re, input) == true && input.replace(/\s/g, '').length >= min && input.replace(/\s/g, '').length <= max) {
        inputte.css('border', '1px solid #ced4da');
        varr = true;
    } else {
        $result.text(text + " is not valid ");
        $result.css("color", "red");
        inputte.css('border', '1px solid red');
        varr = false
    }
    return varr;
}

function validateRange(inputt, resultt, min, max) {
    var $result = $("#" + resultt);
    var input = parseInt($("#" + inputt).val());
    var inputte = $("#" + inputt);
    var ancestor = $("#" + inputt).closest("div");
    var ancestor2 = $(ancestor).parent();
    var text = $(ancestor2).children("label").text();
    $result.text("");
    if (input >= min && input <= max) {
        inputte.css('border', '1px solid #ced4da');
        varr = true;
    } else {
        $result.text(text + " is not valid ");
        $result.css("color", "red");
        inputte.css('border', '1px solid red');
        varr = false
    }
    return varr;
}

function validatelessthan0(inputt, resultt) {
    var $result = $("#" + resultt);
    var input = parseFloat($("#" + inputt).val());
    var inputte = $("#" + inputt);
    var ancestor = $("#" + inputt).closest("div");
    var ancestor2 = $(ancestor).parent();
    var text = $(ancestor2).children("label").text();
    $result.text("");
    if (input > 0) {
        inputte.css('border', '1px solid #ced4da');
        varr = true;
    } else {
        $result.text(text + " is not valid ");
        $result.css("color", "red");
        inputte.css('border', '1px solid red');
        varr = false
    }
    return varr;
}


function validatelessthan0Int(inputt, resultt) {
    var $result = $("#" + resultt);
    var input = parseInt($("#" + inputt).val());
    var inputte = $("#" + inputt);
    var ancestor = $("#" + inputt).closest("div");
    var ancestor2 = $(ancestor).parent();
    var text = $(ancestor2).children("label").text();
    $result.text("");
    if (input > 0) {
        inputte.css('border', '1px solid #ced4da');
        varr = true;
    } else {
        $result.text(text + " is not valid ");
        $result.css("color", "red");
        inputte.css('border', '1px solid red');
        varr = false
    }
    return varr;
}

function validatecanbe0(inputt, resultt) {
    var $result = $("#" + resultt);
    var input = parseFloat($("#" + inputt).val());
    var inputte = $("#" + inputt);
    var ancestor = $("#" + inputt).closest("div");
    var ancestor2 = $(ancestor).parent();
    var text = $(ancestor2).children("label").text();
    $result.text("");
    if (input >= 0) {
        inputte.css('border', '1px solid #ced4da');
        varr = true;
    } else {
        $result.text(text + " is not valid ");
        $result.css("color", "red");
        inputte.css('border', '1px solid red');
        varr = false
    }
    return varr;
}

function validatecanbe0Int(inputt, resultt) {
    var $result = $("#" + resultt);
    var input = parseInt($("#" + inputt).val());
    var inputte = $("#" + inputt);
    var ancestor = $("#" + inputt).closest("div");
    var ancestor2 = $(ancestor).parent();
    var text = $(ancestor2).children("label").text();
    $result.text("");
    if (input >= 0) {
        inputte.css('border', '1px solid #ced4da');
        varr = true;
    } else {
        $result.text(text + " is not valid ");
        $result.css("color", "red");
        inputte.css('border', '1px solid red');
        varr = false
    }
    return varr;
}

function buttonsubmit(id) {
    $("#" + id).addClass("form-group col-12 d-flex");
    $("#" + id).children("input").addClass("btn btn-success ml-auto p-2 col-2");
}

function phonemask(id) {
    if ($("#" + id).text().replace(/\s/g, '').length == 11) {
        $("#" + id).mask('(+00) 0000 000 00');
    }
    if ($("#" + id).text().replace(/\s/g, '').length == 10) {
        $("#" + id).mask('(+0) 0000 000 00');
    }
    if ($("#" + id).text().replace(/\s/g, '').length == 12) {
        $("#" + id).mask('(+000) 0000 000 00');
    }
    $("#" + id).css("white-space", "nowrap");
}

function AddButton(table_name, icon1, icon2, icon3, title_Create, title_Edit, title_Delete, link_Create) {
    $("#" + table_name + "_length label").remove();

    $("#" + table_name + "_length").append("<a href='" + link_Create + "' class='btn createe marginleft5px' style='margin-bottom:10px' data-toggle='tooltip' title='" + title_Create + "' data-placement='bottom'><img src='" + icon1 + "' style='width:35px'/></a> " +
        '<button class="btn editt marginleft5px" style="margin-bottom: 10px;background-color:#ffffff" data-toggle="tooltip" title="' + title_Edit + '" data-placement="bottom"><img src="' + icon2 + '" style="width:39px "/></button>' +
        '<button class="btn deletee marginleft5px" style="margin-bottom: 10px;background-color:#ffffff" data-toggle="tooltip" title="' + title_Delete + '" data-placement="bottom"><img src="' + icon3 + '" style="width:39px "/></button>'
    );
    $("#" + table_name + "_filter").addClass("form-inline");
    $("#" + table_name + "_filter").css("margin-top", "10px");
    $("#" + table_name + "_filter label input").addClass("form-control");
}