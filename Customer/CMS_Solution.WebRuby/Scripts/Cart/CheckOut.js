var CheckOut = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
};
CheckOut.prototype = {
    Initialize: function (options) {
        this.RegisterEvent();
    },
    RegisterEvent: function () {
        $("#btn-login").unbind("click").click(function () {
            Common.CheckOut.validatorForm("#form-Login");
            if ($("#form-Login").find(".field-validation-error").length === 0) {
                $('#form-Login').unbind("submit").submit();
            }

            return false;
        });
        document.querySelectorAll("input[data-rule-phone]")
        .forEach(function (item, index) {
            let elemEventHandler = function (event) {
                this.value = this.value.replace(/[^+\d]/g, '');
            };
            item.removeEventListener('input', elemEventHandler);
            item.addEventListener('input', elemEventHandler);
        });
        $("#btn-Save").unbind("click").click(function () {
            Common.CheckOut.validatorForm("#form-InsUpd");
            if ($("#form-InsUpd").find(".field-validation-error").length === 0) {
                Common.ShowLoading(true);
                var data = $("#form-InsUpd").serialize(); 
                Common.Ajax({
                    url: Common.CheckOut.Url.Create,
                    type: "POST",
                    ContentType: 'application/json; charset=utf-8',
                    async: false,
                    cache: false,
                    dataType: 'JSON',
                    data: data
                }, function (result) {
                    Common.ShowLoading(false);
                    if (result.status === true) {
                        var cookieName = result.cookieid;
                        Common.createCookie("orderCart", result.cookieid, 1);
                        Common.createCookie(cookieName, 0, 1);
                        toastr.success("Bạn đã mua hàng thành công!",
                                 "Thông báo",
                                 {
                                     timeOut: 1000,
                                     progressBar: true,
                                     closeButton: true
                                 }
                             );
                        window.location = '/';
                    }
                    else {
                        switch (result.type) {
                            case 'email':
                                toastr.error("Email đã tồn tại!", "Thông báo", { timeOut: 5000, progressBar: true, closeButton: true });
                                break;
                            case 'phone':
                                toastr.error("Số điện thoại đã tồn tại!", "Thông báo", { timeOut: 5000, progressBar: true, closeButton: true });
                                break;
                            default:
                                toastr.error("Đã xảy ra lỗi!", "Thông báo", { timeOut: 5000, progressBar: true, closeButton: true });
                        }
                    }                   
                }
                );
            }
            return false;
        });
        $("#btnCartOffer").unbind("click").click(function () {
            Common.CheckOut.validatorForm("#form-InsUpd");
            if ($("#form-InsUpd").find(".field-validation-error").length === 0) {
                $('#modalPromotion').modal('Show');
            }
            return false;
        });
        $('input[data-key="inp-Offer"]').unbind("click").change(function () {
            var _valchk = $(this).val();
            var _dataKey = $(this).closest('tr').attr('data-key');
            var _dataIsApply = $(this).closest('tr').attr('data-isapply');
            //disable in group
            if ($(this).is(":checked")) {
                $('input[inp-key="' + _dataKey + '"]').not(":checked").prop('disabled', true);
                //disable out group 
                $('tr[data-isapply="false"]').each(function () {
                    $(this).find('input').prop('disabled', true);
                });
            }
            else {
                $('input[inp-key="' + _dataKey + '"]').not(":checked").prop('disabled', false);
            }

        });
        $('#SaveCartPromotion').unbind("click").click(function () {
            var _CartPromotion = [];
            $('#modalPromotion tr[name="item"]').each(function () {
                let _inp = $(this).find('input');
                if (_inp.is(':checked')) { 
                    _CartPromotion.push({ "PromotionId": _inp.attr('data-promotionid'), "promotionofferid": _inp.attr('data-promotionofferid'), "ProductGiftsId": _inp.attr('data-productgiftsid'), "ProductId": _inp.attr('data-productid'), "DisCountValue": _inp.attr('data-discountvalue') });
                }
            });
            Common.CheckOut.validatorForm("#form-InsUpd");
            if ($("#form-InsUpd").find(".field-validation-error").length === 0) {
                var _data = $("#form-InsUpd").serializeArray();
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
                    url: Common.CheckOut.Url.CreatePromotion,
                    data: fd,
                    dataType: 'json',
                    success: function (result) {
                        Common.ShowLoading(false);
                        Common.ShowLoading(false);
                        if (result.status === true) {
                            var cookieName = result.cookieid;
                            Common.createCookie("orderCart", result.cookieid, 1);
                            Common.createCookie(cookieName, 0, 1);
                            toastr.success("Bạn đã mua hàng thành công!",
                                "Thông báo",
                                {
                                    timeOut: 1000,
                                    progressBar: true,
                                    closeButton: true
                                }
                            );
                            window.location = '/';
                        }
                        else {
                            switch (result.type) {
                                case 'email':
                                    toastr.error("Email đã tồn tại!", "Thông báo", { timeOut: 5000, progressBar: true, closeButton: true });
                                    break;
                                case 'phone':
                                    toastr.error("Số điện thoại đã tồn tại!", "Thông báo", { timeOut: 5000, progressBar: true, closeButton: true });
                                    break;
                                default:
                                    toastr.error("Đã xảy ra lỗi!", "Thông báo", { timeOut: 5000, progressBar: true, closeButton: true });
                            }
                        }     
                    },
                    error: function (xhr, desc, err) {

                    }
                });
            }
            return false;
        });
    },
    validatorForm: (form) => {
        document.querySelectorAll(form + ' .field-validation-error').forEach(function (item, index) {
            let parent = item.parentNode;
            parent.parentNode.removeChild(parent);
        });
        //input required
        document.querySelectorAll(form + ' input[type="text"][data-rule-required],' + form + ' textarea[data-rule-required="true"]').forEach(function (item, index) {
            if (Common.Empty(item.value) && Common.Empty(item.getAttribute('disabled'))) {
                let newChild = document.createElement('section');
                newChild.innerHTML = '<section class="clearfix"></section><div class="field-validation-error alert alert-danger fade show" role="alert" style="display: block;">' + item.getAttribute('data-msg-required') + '</div>';
                const parentNode = item.parentNode;
                parentNode.insertBefore(newChild, item.nextSibling);
            }
        });
        document.querySelectorAll(form + ' input[type="text"][data-rule-email]').forEach(function (item, index) {
            if (!Common.Empty(item.value) && !(/^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/.test(item.value)) && Common.Empty(item.getAttribute('disabled'))) {
                let newChild = document.createElement('section');
                newChild.innerHTML = '<section class="clearfix"></section><div class="field-validation-error alert alert-danger fade show" role="alert" style="display: block;">' + item.getAttribute('data-msg-email') + '</div>';
                const parentNode = item.parentNode;
                parentNode.insertBefore(newChild, item.nextSibling);
            }
        });
        //input required
        document.querySelectorAll(form + ' input[type="text"][data-rule-min-length],' + form + ' textarea[data-rule-min-length]').forEach(function (item, index) {
            if (!Common.Empty(item.value) && item.value.length < item.getAttribute('data-rule-min-length')) {
                let newChild = document.createElement('section');
                newChild.innerHTML = '<section class="clearfix"></section><div class="field-validation-error alert alert-danger fade show" role="alert" style="display: block;">' + item.getAttribute('data-msg-min-length') + '</div>';
                const parentNode = item.parentNode;
                parentNode.insertBefore(newChild, item.nextSibling);
            }
        });
        //select required
        document.querySelectorAll(form + ' select[data-rule-required]:not([multiple])').forEach(function (item, index) {
            if (Common.Empty(item.getAttribute('disabled'))) {
                var selected = item.querySelectorAll('option:checked');
                if (selected.length == 0 || Common.Empty(selected[0].value)) {
                    let newChild = document.createElement('section');
                    newChild.innerHTML = '<section class="clearfix"></section><div class="field-validation-error alert alert-danger fade show" role="alert" style="display: block;">' + item.getAttribute('data-msg-required') + '</div>';
                    const parentNode = item.parentNode;
                    parentNode.insertBefore(newChild, item.nextSibling);
                }
            }
        });
    }
}