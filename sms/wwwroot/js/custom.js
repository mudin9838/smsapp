$(document).ready(function () {
//    //$("#CategoryId").change(function () {
//    //    $.get("/StockItems/GetSubCategoryList", { CategoryId: $("#CategoryId").val() }, function (data) {
//    //        $("#SubCategoryId").empty();
//    //        $("#MeasurementUnitId").empty();
//    //        $("#SubCategoryId").append($('<option></option>').val('').text('--ይምረጡ--'));
//    //        $.each(data, function (index, item) {
//    //            $("#SubCategoryId").append($('<option></option>').val(item.Value).text(item.Text));
//    //        });
//    //    });
//    //});

//    //$("#SubCategoryId").change(function () {
//    //    $.get("/StockItems/GetMeasurementUnitList", { SubCategoryId: $("#SubCategoryId").val() }, function (data) {
//    //        $("#MeasurementUnitId").empty();
//    //        //  $("#MeasurementUnitId").append($('<option></option>').val('').text('--ይምረጡ--'));
//    //        $.each(data, function (index, item) {
//    //            $("#MeasurementUnitId").append($('<option></option>').val(item.Value).text(item.Text));
//    //        });
//    //    });
//    //});

    $("#EachPrice").keyup(function () {
        var vatValue = $("#EachPrice").val() * $("#Quantity").val() * 0.15;
        var totalPriceWithVat = $("#Quantity").val() * $("#EachPrice").val() + vatValue;
        $("#Vat").val(vatValue);
        $("#TotalPrice").val(totalPriceWithVat);
        
    })

  
});