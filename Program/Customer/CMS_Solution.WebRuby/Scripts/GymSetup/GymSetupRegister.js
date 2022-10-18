var GymSetupRegister = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
};
GymSetupRegister.prototype = {
    Initialize: function (options) {
        this.RegisterEvent();
    },
    RegisterEvent: function () {
        this.IsPaging = false;
        var that = this;
        $("#btnSave").unbind("click").click(function () {
            Common.GymSetupRegister.validatorForm("#form-GymTrialAdd");
            if ($("#form-GymTrialAdd").find(".field-validation-error").length == 0) {
                var data = $("#form-GymTrialAdd").serialize();
                console.log(data);
                Common.ShowLoading(true);
                Common.Ajax({
                    url: Common.GymSetupRegister.Url.Register,
                    type: "POST",
                    ContentType: 'application/json; charset=utf-8',
                    async: false,
                    cache: false,
                    dataType: 'JSON',
                    data: data
                }, function (result) {
                    Common.ShowLoading(false);
                    if (!Common.Empty(result)) {
                        if (result.status == true) {
                            toastr.success(
                                "Đăng ký thành công.!!!",
                                "Thông báo",
                                {
                                    timeOut: 2000,
                                    progressBar: true,
                                    closeButton: true
                                }
                            );
                            $("#form-GymTrialAdd").find("input[type=text], textarea").val("");
                            $("#form-GymTrialAdd").find("input[type=file]").val(null)
                        }
                        else {
                            toastr.error(
                                "Đăng ký thất bại.!!!",
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
    },
    validatorForm: (form) => {
        document.querySelectorAll(form + ' .field-validation-error').forEach(function (item, index) {
            item.remove(item);
        });
        //input required
        document.querySelectorAll(form + ' input[type="text"][data-rule-required],' + form + ' textarea[data-rule-required="true"],' + form + ' select[data-rule-required="true"]').forEach(function (item, index) {
            if (Common.Empty(item.value) && Common.Empty(item.getAttribute('disabled'))) {
                var newChild = '<div class="field-validation-error alert alert-danger fade show" role="alert" style="display: block;;width:40%;text-align: left;">' + item.getAttribute('data-msg-required') + '</div>';
                item.insertAdjacentHTML("afterend", newChild)
            }
        });
        document.querySelectorAll(form + ' input[type="text"][data-rule-required],' + form + ' textarea[data-rule-required="true"],' + form + ' select[data-rule-required="true"]').forEach(function (item, index) {
            if (!Common.Empty(item.value) && (/<[a-z][\s\S]*>/i.test(item.value) || (/<[/s/S][a-z][\s\S]*>/i.test(item.value)))) {
                var newChild = '<div class="field-validation-error alert alert-danger fade show" role="alert" style="display: block; width:40%;text-align: left;">Không được chứa mã HTML!!!</div>';
                item.insertAdjacentHTML("afterend", newChild)
            }
        });
        document.querySelectorAll(form + ' input[type="text"][data-rule-required],' + form + ' textarea[data-rule-required="true"],' + form + ' select[data-rule-required="true"]').forEach(function (item, index) {
            if (!Common.Empty(item.value) && Common.Empty(Common.RemoveScript(item.value))) {
                var newChild = '<div class="field-validation-error alert alert-danger fade show" role="alert" style="display: block; width:40%;text-align: left;">Không có đoạn Javascript không hợp lệ !!!</div>';
                item.insertAdjacentHTML("afterend", newChild)
            }
        });
        document.querySelectorAll(form + ' input[type="text"][data-rule-email]').forEach(function (item, index) {
            if (!Common.Empty(item.value) && !(/^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/.test(item.value)) && Common.Empty(item.getAttribute('disabled'))) {
                var newChild = '<div class="field-validation-error alert alert-danger fade show" role="alert" style="display: block; width:40%;text-align: left;">' + item.getAttribute('data-msg-email') + '</div>';
                item.insertAdjacentHTML("afterend", newChild)
            }
        });
        document.querySelectorAll(form + ' input[type="file"][data-rule-required]').forEach(function (item, index) {
            if (Common.Empty(item.value)) {
                var newChild = '<div class="field-validation-error alert alert-danger fade show" role="alert" style="display: block; width:40%;text-align: left;">' + item.getAttribute('data-msg-required') + '</div>';
                item.insertAdjacentHTML("afterend", newChild)
            }
        });
    },
}