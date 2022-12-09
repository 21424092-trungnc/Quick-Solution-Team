var ThemContactUs = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
};
ThemContactUs.prototype = {
    Initialize: function (options) {
        this.RegisterEvent();
    },
    RegisterEvent: function () {
        this.IsPaging = false;
        var that = this;
        $("button[name='btn_Send']").unbind("click").click(function () {
            Common.ThemContactUs.Send();
        });  
    },
    Send: function () {
        Common.ThemContactUs.validatorForm("#form-AddContactUs");
        if ($("#form-AddContactUs").find(".field-validation-error").length == 0) {
            var _model = {
                TopicId: $("#txt_toppic option:selected").val(),
                FullName: $("input[name='txt_name']").val(),
                PhoneNumber: $.trim($("input[name='txt_phone']").val()),
                Email: $.trim($("input[name='txt_mail']").val()),
                ContentSupport: $.trim($("textarea[name='txt_content']").val()),
                IsActive: true,
                IsDeleted: false
            };
            var formdata = new FormData();
            formdata.append('modelThongTin', JSON.stringify(_model));
            Common.Ajax({
                type: "POST",
                url: Common.ThemContactUs.Url.Send,
                async: false,
                cache: false,
                datatype: "json",
                contentType: false,
                processData: false,
                data: formdata
            }, function (result) {
                if (!Common.Empty(result)) {
                    if (result.status == true) {
                        toastr.success(
                            "Gửi liên hệ thành công!",
                            "Thông báo",
                            {
                                timeOut: 1000,
                                progressBar: true,
                                closeButton: true
                            }
                        );
                        let url = $("button[name='btn_Send']").data('url');
                        setTimeout(e => {
                            window.location = window.location.origin + url;
                        }, 1500);
                    }
                    else {
                        toastr.error(
                            "Gửi liên hệ không thành công!",
                            "Thông báo",
                            {
                                timeOut: 2000,
                                progressBar: true,
                                closeButton: true
                            }
                        );
                    }
                }
            });
        }
    },
    validatorForm: (form) => {
        document.querySelectorAll(form + ' .field-validation-error').forEach(function (item, index) {
            item.remove(item);
        });
        //input required
        document.querySelectorAll(form + ' input[type="text"][data-rule-required],' + form + ' textarea[data-rule-required="true"],' + form + ' select[data-rule-required="true"]').forEach(function (item, index) {
            if (Common.Empty(item.value) && Common.Empty(item.getAttribute('disabled'))) {
                var newChild = '<div class="field-validation-error alert alert-danger fade show" role="alert" style="display: block;;width:100%;text-align: left;">' + item.getAttribute('data-msg-required') + '</div>';
                item.insertAdjacentHTML("afterend", newChild)
            }
        });
        document.querySelectorAll(form + ' input[type="text"][data-rule-required],' + form + ' textarea[data-rule-required="true"],' + form + ' select[data-rule-required="true"]').forEach(function (item, index) {
            if (!Common.Empty(item.value) && (/<[a-z][\s\S]*>/i.test(item.value) || (/<[/s/S][a-z][\s\S]*>/i.test(item.value)))) {
                var newChild = '<div class="field-validation-error alert alert-danger fade show" role="alert" style="display: block; width:100%;text-align: left;">Không được chứa mã HTML!!!</div>';
                item.insertAdjacentHTML("afterend", newChild)
            }
        });
        document.querySelectorAll(form + ' input[type="text"][data-rule-required],' + form + ' textarea[data-rule-required="true"],' + form + ' select[data-rule-required="true"]').forEach(function (item, index) {
            if (!Common.Empty(item.value) && Common.Empty(Common.RemoveScript(item.value))) {
                var newChild = '<div class="field-validation-error alert alert-danger fade show" role="alert" style="display: block; width:100%;text-align: left;">Không có đoạn Javascript không hợp lệ !!!</div>';
                item.insertAdjacentHTML("afterend", newChild)
            }
        });
        document.querySelectorAll(form + ' input[type="text"][data-rule-email]').forEach(function (item, index) {
            if (!Common.Empty(item.value) && !(/^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/.test(item.value)) && Common.Empty(item.getAttribute('disabled'))) {
                var newChild = '<div class="field-validation-error alert alert-danger fade show" role="alert" style="display: block; width:100%;text-align: left;">' + item.getAttribute('data-msg-email') + '</div>';
                item.insertAdjacentHTML("afterend", newChild)
            }
        });
        document.querySelectorAll(form + ' input[type="file"][data-rule-required]').forEach(function (item, index) {
            if (Common.Empty(item.value)) {
                var newChild = '<div class="field-validation-error alert alert-danger fade show" role="alert" style="display: block; width:100%;text-align: left;">' + item.getAttribute('data-msg-required') + '</div>';
                item.insertAdjacentHTML("afterend", newChild)
            }
        });
    },
}