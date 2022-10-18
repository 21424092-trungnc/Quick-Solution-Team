var CandidateApply = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
};
CandidateApply.prototype = {
    Initialize: function (options) {
        this.RegisterEvent();
    },
    RegisterEvent: function () {
        this.IsPaging = false;
        var that = this;
        $("#PhoneNumber").val("");
        document.querySelectorAll("input[data-rule-phone]")
      .forEach(function (item, index) {
          let elemEventHandler = function (event) {
              this.value = this.value.replace(/[^+\d]/g, '');
          };
          item.removeEventListener('input', elemEventHandler);
          item.addEventListener('input', elemEventHandler);
      });
        $("#btnSave").unbind("click").click(function () {
            Common.CandidateApply.validatorForm("#form-CandidateApply");
            if ($("#form-CandidateApply").find(".field-validation-error").length == 0) {
                var data = $("#form-CandidateApply").serialize();
                Common.ShowLoading(true);
                Common.Ajax({
                    url: Common.CandidateApply.Url.Apply,
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
                                "Gửi thông tin ứng tuyển thành công.!!!",
                                "Thông báo",
                                {
                                    timeOut: 2000,
                                    progressBar: true,
                                    closeButton: true
                                }
                            );
                            $("#form-CandidateApply").find("input[type=text], textarea").val("");
                            $("#form-CandidateApply").find("input[type=file]").val(null)
                            $("#form-CandidateApply").find("input[type=radio]").val(0)
                        }
                        else {
                            toastr.error(
                                "Gửi thông tin ứng tuyển thất bại.!!!",
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
            item.parentNode.removeChild(item);
        });
        //input required
        document.querySelectorAll(form + ' input[type="text"][data-rule-required],' + form + ' textarea[data-rule-required="true"]').forEach(function (item, index) {
            if (Common.Empty(item.value) && Common.Empty(item.getAttribute('disabled'))) {
                var newChild = '<div class="field-validation-error alert alert-danger fade show" role="alert" style="display: block; width:50%">' + item.getAttribute('data-msg-required') + '</div>';
                item.parentNode.insertAdjacentHTML('beforeend', newChild);
            }
        });
        document.querySelectorAll(form + ' input[type="text"][data-rule-required],' + form + ' textarea[data-rule-required="true"]').forEach(function (item, index) {
            if (!Common.Empty(item.value) && (/<[a-z][\s\S]*>/i.test(item.value) || (/<[/s/S][a-z][\s\S]*>/i.test(item.value)))) {
                var newChild = '<div class="field-validation-error alert alert-danger fade show" role="alert" style="display: block; width:50%">Không được chứa mã HTML!!!</div>';
                item.parentNode.insertAdjacentHTML('beforeend', newChild);
            }
        });
        document.querySelectorAll(form + ' input[type="text"][data-rule-required],' + form + ' textarea[data-rule-required="true"]').forEach(function (item, index) {
            if (!Common.Empty(item.value) && Common.Empty(Common.RemoveScript(item.value))) {
                var newChild = '<div class="field-validation-error alert alert-danger fade show" role="alert" style="display: block; width:50%">Không có đoạn Javascript không hợp lệ !!!</div>';
                item.parentNode.insertAdjacentHTML('beforeend', newChild);
            }
        });
        document.querySelectorAll(form + ' input[type="text"][data-rule-email]').forEach(function (item, index) {
            if (!Common.Empty(item.value) && !(/^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/.test(item.value)) && Common.Empty(item.getAttribute('disabled'))) {
                var newChild = '<div class="field-validation-error alert alert-danger fade show" role="alert" style="display: block; width:50%">' + item.getAttribute('data-msg-email') + '</div>';
                item.parentNode.insertAdjacentHTML('beforeend', newChild);
            }
        });
        document.querySelectorAll(form + ' input[type="text"][data-rule-min-length],' + form + ' textarea[data-rule-min-length]').forEach(function (item, index) {
            if (!Common.Empty(item.value) && item.value.length < item.getAttribute('data-rule-min-length')) {
                var newChild = '<div class="field-validation-error alert alert-danger fade show" role="alert" style="display: block; width:50%">' + item.getAttribute('data-msg-min-length') + '</div>';
                item.parentNode.insertAdjacentHTML('beforeend', newChild);
            }
        });
        document.querySelectorAll(form + ' input[type="file"][data-rule-required]').forEach(function (item, index) {
            if (Common.Empty(item.value)) {
                var newChild = '<div class="field-validation-error alert alert-danger fade show" role="alert" style="display: block; width:50%">' + item.getAttribute('data-msg-required') + '</div>';
                item.parentNode.insertAdjacentHTML('beforeend', newChild);
            }
        });
    },
    UploadFile: function () {
        $("#AttachmentPath").val("");
        $("#AttachmentName").val("");
        let fileUploads = $('#fileCV')[0].files;
        let fileSize = fileUploads[0].size / 1024 / 1024;
        if (!Common.Empty(fileUploads)) {
            let regex = new RegExp("(doc|DOC|docx|DOCX|pdf|PDF|png|jpg|jpeg|PNG|JPG)$");
            let fileName = fileUploads[0].name;
            if (!regex.test(Common.CandidateApply.getTypeFile(fileName))) {
                toastr.warning(
                    "Vui lòng chọn tệp tin đúng định dạng hình!!!",
                    "Thông báo",
                    {
                        timeOut: 2000,
                        progressBar: true,
                        closeButton: true
                    }
                );
                $('#fileCV').val(null);
                return false;
            }
            else {
                var data = new FormData();
                if (fileUploads.length > 0) {
                    data.append('UploadedFiles', fileUploads[0], fileUploads[0].name);
                }
                Common.ShowLoading(true);
                Common.Ajax({
                    type: "POST",
                    url: Common.CandidateApply.Url.UploadFile,
                    dataType: 'json',
                    data: data,
                    contentType: false, // Not to set any content header
                    processData: false // Not to process data
                }, function (data) {
                    Common.ShowLoading(false);
                    if (data) {
                        if (data.status) {
                            toastr.success(
                                "Đính kèm CV thành công.!!!",
                                "Thông báo",
                                {
                                    timeOut: 2000,
                                    progressBar: true,
                                    closeButton: true
                                }
                            );
                            $("#AttachmentPath").val(data.filePath);
                            $("#AttachmentName").val(data.fileName);
                        }
                        else {
                            toastr.error(
                                "Tải hình lỗi.!!!",
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
        }
    },
    getTypeFile: (_filename) => {
        var _name = '';
        var _f = (_filename != '') ? _filename.lastIndexOf('.') : -1;
        var _t = (_filename != '') ? _filename.length : -1;
        if (_filename != '' && _f > -1) {
            _name = _filename.substring((_f + 1), _t);
        } else {
            _name = '';
        }
        _name = (_name != '') ? _name.toLowerCase() : _name;
        return _name;
    }
}