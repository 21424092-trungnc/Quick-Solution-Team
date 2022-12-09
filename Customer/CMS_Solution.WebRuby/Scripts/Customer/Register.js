var Register = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
};
Register.prototype = {
    Initialize: function (options) {
        this.RegisterEvent();
    },
    RegisterEvent: function () {
        this.IsPaging = false;
        var that = this;
        $("#btnSave").unbind("click").click(function () {
            Common.Register.validatorForm("#form-InsUpd");
            if ($("#form-InsUpd").find(".field-validation-error").length === 0) {
                var data = $("#form-InsUpd").serialize();
                Common.ShowLoading(true);
                Common.Ajax({
                    url: Common.Register.Url.Create,
                    type: "POST",
                    ContentType: 'application/json; charset=utf-8',
                    async: false,
                    cache: false,
                    dataType: 'JSON',
                    data: data
                }, function (result) {
                   
                   
                    if (result.status === true) {
                        toastr.success("Đăng ký thành công!",
                                 "Thông báo",
                                 {
                                     timeOut: 1000,
                                     progressBar: true,
                                     closeButton: true
                                 }
                             );
                        window.location = '/dang-nhap'
                        Common.ShowLoading(false); 
                    }
                    else {
                        switch(result.type) {
                            case 'email':
                                toastr.error("Email đã tồn tại!", "Thông báo", { timeOut: 5000, progressBar: true, closeButton: true });
                                break;
                            case 'phone':
                                toastr.error("Số điện thoại đã tồn tại!", "Thông báo", { timeOut: 5000, progressBar: true, closeButton: true });
                                break;
                            default:
                                toastr.error("Đăng ký không thành công!", "Thông báo", { timeOut: 5000, progressBar: true, closeButton: true });
                            }
                            Common.ShowLoading(false);
                        }
                   }
                );
            }
        });
        document.querySelectorAll("input[data-rule-phone]")
     .forEach(function (item, index) {
         let elemEventHandler = function (event) {
             this.value = this.value.replace(/[^+\d]/g, '');
         };
         item.removeEventListener('input', elemEventHandler);
         item.addEventListener('input', elemEventHandler);
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
                parentNode.appendChild(newChild);
            }
        });
        document.querySelectorAll(form + ' input[type="text"][data-rule-email]').forEach(function (item, index) {
            if (!Common.Empty(item.value) && !(/^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/.test(item.value)) && Common.Empty(item.getAttribute('disabled'))) {
                let newChild = document.createElement('section');
                newChild.innerHTML = '<section class="clearfix"></section><div class="field-validation-error alert alert-danger fade show" role="alert" style="display: block;">' + item.getAttribute('data-msg-email') + '</div>';
                const parentNode = item.parentNode;
                parentNode.appendChild(newChild);
            }
        });
        document.querySelectorAll(form + ' input[type="text"][data-rule-min-length],' + form + ' textarea[data-rule-min-length]').forEach(function (item, index) {
            if (!Common.Empty(item.value) && item.value.length < item.getAttribute('data-rule-min-length')) {
                let newChild = document.createElement('section');
                newChild.innerHTML = '<section class="clearfix"></section><div class="field-validation-error alert alert-danger fade show" role="alert" style="display: block;">' + item.getAttribute('data-msg-min-length') + '</div>';
                const parentNode = item.parentNode;
                parentNode.appendChild(newChild);
            }
        });
        document.querySelectorAll(form + ' input[type="password"][data-rule-required],' + form + ' textarea[data-rule-required="true"]').forEach(function (item, index) {
            if (Common.Empty(item.value) && Common.Empty(item.getAttribute('disabled'))) {
                let newChild = document.createElement('section');
                newChild.innerHTML = '<section class="clearfix"></section><div class="field-validation-error alert alert-danger fade show" role="alert" style="display: block;">' + item.getAttribute('data-msg-required') + '</div>';
                const parentNode = item.parentNode;
                parentNode.appendChild(newChild);
            }
        });
        //Check pass
        let rep = document.getElementById("RePassword");
        let p = document.getElementById("Password");
        if (!Common.Empty(p.value) && Common.Empty(p.getAttribute('disabled')) && !Common.Empty(rep.value) && Common.Empty(rep.getAttribute('disabled'))) {
            if (p.value !== rep.value) {
                let newChild = document.createElement('section');
                newChild.innerHTML = '<section class="clearfix"></section><div class="field-validation-error alert alert-danger fade show" role="alert" style="display: block;">Mật khẩu xác nhận không chính xác!!!</div>';
                const parentNode = rep.parentNode;
                parentNode.appendChild(newChild);
            }
        }
    },
}