var CartAdd = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
};
CartAdd.prototype = {
    Initialize: function (options) {
        this.RegisterEvent();
    },
    RegisterEvent: function () {
        $("#btnSaveCart").unbind("click").click(function () {
            Common.CartAdd.validatorForm("#form-CartAdd");
            if ($("#form-CartAdd").find(".field-validation-error").length === 0) {
                var data = $("#form-CartAdd").serialize();
                Common.ShowLoading(true);
                Common.Ajax({
                    url: Common.CartAdd.Url.AddItemToCart,
                    type: "POST",
                    ContentType: 'application/json; charset=utf-8',
                    async: false,
                    cache: false,
                    dataType: 'JSON',
                    data: data
                }, function (result) {
                    Common.ShowLoading(false);
                    if (!Common.Empty(result)) {
                        if (result.status === true) { 
                            var cookieName = result.data[0].CookieId;
                            Common.createCookie("orderCart", result.data[0].CookieId, 1);
                            Common.createCookie(cookieName, JSON.stringify(result.data), 1);
                            toastr.success(
                                "Thêm vào giỏ hàng thành công.!!!",
                                "Thông báo",
                                {
                                    timeOut: 2000,
                                    progressBar: true,
                                    closeButton: true
                                }
                            );
                            //Gọi hàm render số lượng giỏ hàng.
                            window.location.href = Common.CartAdd.Url.CartIndex;
                           // $('#CookieId').val(result.data.CookieId);
                        }
                        else {
                            toastr.error(
                                "Thêm vào giỏ hàng thất bại.!!!",
                                "Thông báo",
                                {
                                    timeOut: 2000,
                                    progressBar: true,
                                    closeButton: true
                                }
                            );
                        }
                    }
                }
                );
            }
            return false;
        });
        //Show modal promotion

        $('#btnOfferCart').unbind("click").click(function () {
            $('#modalPromotion').modal('show');
        });
        $('#SaveCartPromotion').unbind("click").click(function () {
            var _CartPromotion = [];
            $('#modalPromotion tr[name="item"]').each(function () {
                let _inp = $(this).find('input');
                if (_inp.is(':checked')) {
                    console.log(_inp.val());
                    _CartPromotion.push({ "PromotionId": _inp.attr('data-promotionid'), "promotionofferid": _inp.attr('data-promotionofferid'), "ProductGiftsId": _inp.attr('data-productgiftsid'), "ProductId": _inp.attr('data-productid'), "DisCountValue": _inp.attr('data-discountvalue') });
                }
            });
            Common.CartAdd.validatorForm("#form-CartAdd");
            if ($("#form-CartAdd").find(".field-validation-error").length === 0) {
                var _data = $("#form-CartAdd").serializeArray();
                var data = {};
                _data.forEach(function (item, index) {
                    if (item.name.indexOf('.') > -1) {
                        data[item.name.split('.')[1]] = item.value;
                    }
                    else {
                        data[item.name] = item.value;
                    }
                }); 
                var fd = new FormData();
                fd.append("dataModel", JSON.stringify(data));
                fd.append("CartPromotion", JSON.stringify(_CartPromotion));
        
                Common.ShowLoading(true);
                $.ajax({
                    type: 'POST',
                    contentType: false,
                    processData: false,
                    url: Common.CartAdd.Url.AddItemToCartPromotion, 
                    data: fd,
                    dataType: 'json',
                    success: function (result) {
                        Common.ShowLoading(false);
                        if (!Common.Empty(result)) {
                            if (result.status === true) {
                                var cookieName = result.data[0].CookieId;
                                Common.createCookie("orderCart", result.data[0].CookieId, 1);
                                Common.createCookie(cookieName, JSON.stringify(result.data), 1);
                                toastr.success(
                                    "Thêm vào giỏ hàng thành công.!!!",
                                    "Thông báo",
                                    {
                                        timeOut: 2000,
                                        progressBar: true,
                                        closeButton: true
                                    }
                                );
                                //Gọi hàm render số lượng giỏ hàng.
                                window.location.href = Common.CartAdd.Url.CartIndex;
                                // $('#CookieId').val(result.data.CookieId);
                            }
                            else {
                                toastr.error(
                                    "Thêm vào giỏ hàng thất bại.!!!",
                                    "Thông báo",
                                    {
                                        timeOut: 2000,
                                        progressBar: true,
                                        closeButton: true
                                    }
                                );
                            }
                        }
                    },
                    error: function (xhr, desc, err) {
                        
                    }
                });
            }
            return false;
        });
        // check item offer
        $('input[data-key="inp-Offer"]').unbind("click").change(function () {
            if ($('input[data-key="inp-Offer"]:checked').length === 2) {
                $('input[data-key="inp-Offer"]').not(":checked").prop('disabled', true);
                return false;
            }
            else {
                $('input[data-key="inp-Offer"]').not(":checked").prop('disabled', false);
            }
            var _valchk = $(this).val();
            var _dataKey = $(this).closest('tr').attr('data-key');
           
            let _promotionid = $(this).attr('data-promotionid');
           
             //disable in group
            if ($(this).is(":checked")) {
                $('input[inp-key="' + _dataKey + '"]').not(":checked").prop('disabled', true);
                //disable out group 
                $('tr[data-isapply="false"]').each(function () {
                    $(this).find('input').prop('disabled', true);
                });

                //disable prmotion
                $('input[data-promotionid="' + _promotionid + '"]').not(":checked").prop('disabled', true);
                var _dataIsApply = $(this).closest('tr').attr('data-isapply');
                
                if (_dataIsApply === 'False') {
                    $('input[data-key="inp-Offer"]').not(":checked").prop('disabled', true);
                }
            }
            else {
                $('input[inp-key="' + _dataKey + '"]').not(":checked").prop('disabled', false);
                $('input[data-promotionid="' + _promotionid + '"]').not(":checked").prop('disabled', false);
                $('input[data-key="inp-Offer"]').not(":checked").prop('disabled', false);
            }
           
        });

        //$(".buy").unbind("click").click(function(){
        //    let productId = $(this).data("id");
            
        //    Common.ShowLoading(true);
        //    Common.Ajax({
        //        url: Common.CartAdd.Url.AddItemToCartByProductID,
        //        type: "POST",
        //        ContentType: 'application/json; charset=utf-8',
        //        async: false,
        //        cache: false,
        //        dataType: 'JSON',
        //        data: JSON.stringify(productId)
        //    }, function (result) {
        //        console.log(result);
        //        Common.ShowLoading(false);
        //        if (!Common.Empty(result)) {
        //            //if (result.status == true) {
        //            //    var cookieName = result.data.CookieId;
        //            //    Common.createCookie("orderCart", result.data.CookieId, 1);
        //            //    Common.createCookie(cookieName, JSON.stringify(result.data), 1);
        //            //    toastr.success(
        //            //        "Thêm vào giỏ hàng thành công.!!!",
        //            //        "Thông báo",
        //            //        {
        //            //            timeOut: 2000,
        //            //            progressBar: true,
        //            //            closeButton: true
        //            //        }
        //            //    );
        //            //    //Gọi hàm render số lượng giỏ hàng.
        //            //    window.location.href = Common.CartAdd.Url.CartIndex;
        //            //    $('#CookieId').val(result.data.CookieId);
        //            //}
        //            //else {
        //            //    toastr.error(
        //            //        "Thêm vào giỏ hàng thất bại.!!!",
        //            //        "Thông báo",
        //            //        {
        //            //            timeOut: 2000,
        //            //            progressBar: true,
        //            //            closeButton: true
        //            //        }
        //            //    );
        //            //}
        //        }
        //    }
        //    );
        //})
        //$('#myModal').modal('show');
       // $('#myModal').modal('hide');
    }, 
    validatorForm:(form) => {
        document.querySelectorAll(form + ' .field-validation-error').forEach(function (item, index) {
            item.parentNode.removeChild(item);
        });
        //input required
        document.querySelectorAll(form + ' input[type="text"][data-rule-required],' + form + ' textarea[data-rule-required="true"]').forEach(function (item, index) {
            if (Common.Empty(item.value) && Common.Empty(item.getAttribute('disabled'))) {
                var newChild = '<div class="field-validation-error alert alert-danger fade show" role="alert" style="display: block;">' + item.getAttribute('data-msg-required') + '</div>';
                item.parentNode.insertAdjacentHTML('beforeend', newChild);
            }
        });
        document.querySelectorAll(form + ' input[type="text"][data-rule-email]').forEach(function (item, index) {
            if (!Common.Empty(item.value) && !(/^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/.test(item.value)) && Common.Empty(item.getAttribute('disabled'))) {
                var newChild = '<div class="field-validation-error alert alert-danger fade show" role="alert" style="display: block;">' + item.getAttribute('data-msg-email') + '</div>';
                item.parentNode.insertAdjacentHTML('beforeend', newChild);
            }
        });
        document.querySelectorAll(form + ' input[type="file"][data-rule-required]').forEach(function (item, index) {
            if (Common.Empty(item.value)) {
                var newChild = '<div class="field-validation-error alert alert-danger fade show" role="alert" style="display: block;">' + item.getAttribute('data-msg-required') + '</div>';
                item.parentNode.insertAdjacentHTML('beforeend', newChild);
            }
        });
    }
}