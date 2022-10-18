var ProductComments = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
};
ProductComments.prototype = {
    Initialize: function (options) {
        this.RegisterEvent();
    },
    RegisterEvent: function () {
        $("#btnSave").unbind("click").click(function () {
            Common.ProductComments.validatorForm("#form-ProductComments");
            if ($("#form-ProductComments").find(".field-validation-error").length === 0) {
                //if (!$('input[name=RattingValues]:checked').val()) {
                //    //toastr.error(
                //    //           "Vui lòng đánh giá trước khi gửi bình luận.!!!",
                //    //           "Thông báo",
                //    //           {
                //    //               timeOut: 2000,
                //    //               progressBar: true,
                //    //               closeButton: true
                //    //           }
                //    //       );
                //    //return; 
                //}
                var data = $("#form-ProductComments").serialize();
                Common.ShowLoading(true);
                Common.Ajax({
                    url: Common.ProductComments.Url.SendComment,
                    type: "POST",
                    ContentType: 'application/json; charset=utf-8',
                    async: false,
                    cache: false,
                    dataType: 'html',
                    data: data
                }, function (result) {
                    Common.ShowLoading(false);
                    if (!Common.Empty(result)) {
                        toastr.success(
                              "Gửi bình luận thành công.!!!",
                              "Thông báo",
                              {
                                  timeOut: 2000,
                                  progressBar: true,
                                  closeButton: true
                              }
                          );
                        $("#form-ProductComments").find("input[type=text], textarea").val("");
                        $("#form-ProductComments").find("input[type=radio]").removeAttr("checked");
                        $('#comment-product').html(result);
                    }
                    else
                    {
                            toastr.error(
                                "Gửi bình luận thất bại.!!!",
                                "Thông báo",
                                {
                                    timeOut: 2000,
                                    progressBar: true,
                                    closeButton: true
                                }
                            );
                    }
                }
                );
            }
            return false;
        }); 
    },
    Send: function (e, _id = "") {
        if (_id.trim() !== "") {
            $('#' + _id.trim()).removeClass('hidden');
        }
        Common.ProductComments.validatorForm("form[name=form-SendComments]");
        if ($("form[name=form-SendComments]").find(".field-validation-error").length === 0) {
            var data = $("form[name=form-SendComments]").serialize();
            Common.ShowLoading(true);
            Common.Ajax({
                url: Common.ProductComments.Url.SendComment,
                type: "POST",
                ContentType: 'application/json; charset=utf-8',
                async: false,
                cache: false,
                dataType: 'html',
                data: data
            }, function (result) {
                Common.ShowLoading(false);
                if (!Common.Empty(result)) {
                    toastr.success(
                        "Gửi bình luận thành công.!!!",
                        "Thông báo",
                        {
                            timeOut: 2000,
                            progressBar: true,
                            closeButton: true
                        }
                    );
                    $("form[name=form-SendComments]").find("input[type=text], textarea").val("");
                    $("form[name=form-SendComments]").find("input[type=radio]").removeAttr("checked");
                    $('#comment-product').html(result);
                }
                else {
                    toastr.error(
                        "Gửi bình luận thất bại.!!!",
                        "Thông báo",
                        {
                            timeOut: 2000,
                            progressBar: true,
                            closeButton: true
                        }
                    );
                }
            });
        }
        return false;
    },
    Reply: function (e, _productCommentId, _productId) {
        $('a[name=replyComment]').removeClass('hidden');
        $('form[name=form-SendComments]').remove();
        $('#' + $(e).attr('id')).addClass('hidden');
        let _html = '<form role="form" name="form-SendComments">';
        _html += '<section class="top-review">';
        _html += '        <div class="col-md-3"></div>';
        _html += '        <div class="col-md-9">';
        _html += '           <div>Để lại bình luận của bạn</div>';
        _html += '  <input id="CommentReplyId" name="CommentReplyId" type="hidden" value="' + _productCommentId + '">';
        _html += '  <input id="ProductId" name="ProductId" type="hidden" value="' + _productId + '">';
        _html += '           <section class="row">';
        _html += '                <div class="col-md-12">';
        _html += '                    <textarea style="width:100% !important" cols="20" data-msg-required="Vui lòng nhập nội dung!!!" data-rule-required="true" id="ContentComment" maxlength="4000" name="ContentComment" placeholder="Nội dung" rows="2"></textarea>';
        _html += '                </div>';
        _html += '                <div class="col-md-6">';
        _html += '                   <input style="width:100% !important"  data-msg-required="Vui lòng nhập họ tên!!!" data-rule-required="true" id="FullName" maxlength="250" name="FullName" placeholder="Họ tên" type="text" value="">';
        _html += '           </div>';
        _html += '                   <div class="col-md-6">';
        _html += '                       <input style="width:100% !important"  data-msg-email="Vui lòng nhập email đúng định dạng!!!" data-msg-required="Vui lòng nhập email!!!" data-rule-email="true" data-rule-required="true" id="Email" maxlength="250" name="Email" placeholder="Email" type="text" value="">';
        _html += '            </div>';
        _html += '        </section>';
        _html += '                   <section class="clearfix"></section>';
        _html += '                   <button type="button" id="btnSave" onclick="Common.ProductComments.Send(this,\'' + $(e).attr('id')+'\')">Gửi</button>';
        _html += '               </div>';
        _html += '            </section>';
        _html += '</form>';
        $(e).closest('.comment').append(_html);
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
                newChild.innerHTML = '<section class="clearfix"></section><div class="field-validation-error alert alert-danger fade show" role="alert" style="display: block;">' + item.getAttribute('data-msg-required') + '</div>';
                const parentNode = item.parentNode;
                parentNode.insertBefore(newChild, item.nextSibling);
            }
        });
        document.querySelectorAll(form + ' input[type="file"][data-rule-required]').forEach(function (item, index) {
            if (Common.Empty(item.value)) {
                let newChild = document.createElement('section');
                newChild.innerHTML = '<section class="clearfix"></section><div class="field-validation-error alert alert-danger fade show" role="alert" style="display: block;">' + item.getAttribute('data-msg-required') + '</div>';
                const parentNode = item.parentNode;
                parentNode.insertBefore(newChild, item.nextSibling);
            }
        });
    }
}