var Login = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
};
Login.prototype = {
    Initialize: function (options) {
        this.LoginEvent();
    },
    LoginEvent: function () {
        this.IsPaging = false;
        var that = this;
        $("#btn-login").unbind("click").click(function () {
            Common.Login.validatorForm("#form-Login");
            if ($("#form-Login").find(".field-validation-error").length == 0) {
                $('#form-Login').unbind("submit").submit();
            }
                
            return false;
        });
        //$("#fb-login").unbind("click").click(function () {
        //    FB.login(function (resLogin) {
        //        console.log(resLogin);
        //        if (resLogin.status === 'connected') {
        //            Common.Login.getUserInfoFB(resLogin.authResponse.accessToken);
        //        } else {
        //            toastr.error(
        //                   "Login facebook không thành công",
        //                   "Thông báo",
        //                   {
        //                       timeOut: 2000,
        //                       progressBar: true,
        //                       closeButton: true
        //                   }
        //               );
        //        }
        //    }, { scope: 'public_profile,email,user_gender' });
        //    return false;
        //});
        //gapi.load('auth2', function () {
        //    auth2 = gapi.auth2.init({
        //        client_id: '133460432243-rmrjsl4vuiflk3k1uk58uqme4mttpvdd.apps.googleusercontent.com',
        //        cookiepolicy: 'single_host_origin',
        //    });
        //    attachSignin(document.getElementById('gg-login'));
        //});
        //function attachSignin(element) {
        //    auth2.attachClickHandler(element, {},
        //        function (googleUser) {
        //            Common.Login.getUserInfoGG(googleUser);
        //        }, function (error) {
        //            toastr.error(
        //                  "Login google không thành công",
        //                  "Thông báo",
        //                  {
        //                      timeOut: 2000,
        //                      progressBar: true,
        //                      closeButton: true
        //                  }
        //              );
        //        });
        //}
    },
    getUserInfoFB: (accessToken) => {
        FB.api('/me', { fields: 'id,email,name,gender', access_token: accessToken }, function (response) {
            const obj = {
                FacebookID: response.id,
                Email: response.email,
                FullName: response.name,
                Gender: response.gender
            };
            Common.Login.ssoLogin(obj);
        });
    },
    getUserInfoGG: (googleUser) => {
        const profile = googleUser.getBasicProfile();
        const obj = {
            GoogleID: profile.getId(),
            FullName: profile.getFamilyName() + (profile.getGivenName() ? (' ' + profile.getGivenName()) : ''),
            Email: profile.getEmail()
        };
        Common.Login.ssoLogin(obj);
    },
    ssoLogin: (user) =>{
        Common.ShowLoading(true);
        Common.Ajax({
            url: Common.Login.Url.LoginAccountSSO,
            type: "POST",
            ContentType: 'application/json; charset=utf-8',
            async: false,
            cache: false,
            dataType: 'JSON',
            data: user
        }, function (result) {
            Common.ShowLoading(false);
            if (!Common.Empty(result)) {
                window.location = result.url;
            }
            else {
                toastr.error(
                    "Đăng nhập thất bại.!!!",
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
        document.querySelectorAll(form + ' input[type="password"][data-rule-required],' + form + ' textarea[data-rule-required="true"]').forEach(function (item, index) {
            if (Common.Empty(item.value) && Common.Empty(item.getAttribute('disabled'))) {
                let newChild = document.createElement('section');
                newChild.innerHTML = '<section class="clearfix"></section><div class="field-validation-error alert alert-danger fade show" role="alert" style="display: block;">' + item.getAttribute('data-msg-required') + '</div>';
                const parentNode = item.parentNode;
                parentNode.insertBefore(newChild, item.nextSibling);
            }
        });
    },
    ShowMessage: (mesage) => {
        toastr.error(
                         mesage,
                         "Thông báo",
                         {
                             timeOut: 2000,
                             progressBar: true,
                             closeButton: true
                         }
                     );
        return false;
    }
}