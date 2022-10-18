Common = function () {
    // this.AddCustomValidation();
    return this.Init();
};
/* Get the element you want displayed in fullscreen */
var elem = document.documentElement;
Common.prototype = {
    Init: function () {
        $(document).ready(function () {
            //of string concatenation
            String.prototype.format = function () {
                var st = this;
                for (i = 0; i < arguments.length; i++) {
                    var value = arguments[i];
                    var re = new RegExp("\{[" + i + "]\}", "g");
                    st = st.replace(re, value);
                }
                return st;
            };

        });
    },
    Empty: function (target) {
        if (target == null || target == undefined || target == "" || target.length == 0 || jQuery.trim(target).length == 0)
            return true;
        return false;
    },
    Serialize: function (el) {
        var serialized = $(el).serialize();
        if (!serialized) // not a form
            serialized = $(el).
                find('input[name],select[name],textarea[name]').serialize();
        return serialized;
    },
    ShowLoading: function (isShow) {
        if (isShow) {
            $(".loadingCommon").show();
        } else {
            $(".loadingCommon").hide();
        }
    },
    GetDateFormat: function (date) {
        return $.datepicker.formatDate($.datepicker._defaults.dateFormat, date);
    },
    //format number(money)
    RedirectLoginUrl: function () {
        window.location.href = '/Error/Permission';

    },
    convertStringToDateTime: function (_val) {
        var today = new Date();
        var dd = today.getDate();
        var mm = today.getMonth() + 1; //January is 0! 
        var yyyy = today.getFullYear();
        if (dd < 10) { dd = '0' + dd }
        if (mm < 10) { mm = '0' + mm }
        var today = dd + '/' + mm + '/' + yyyy;
        _val = (!!_val) ? _val.trim() : today;
        var dateForm = _val.split('/');
        dateTime = dateForm[2] + '-' + dateForm[1] + '-' + dateForm[0];

        var dateResult = new Date(dateTime);
        return dateResult;
    },
    DownloadFile: function (_url) {
        window.open(_url, '_blank');
    },
    setDataToObj: (obj, name, value) => {
        var names = name.split('_');
        if (names.length > 1) {
            if (Common.Empty(obj[names[0]])) {
                obj[names[0]] = {};
            }
            var o = this.setDataToObj(obj[names[0]], names[1], value);
            obj[names[0]] = o;
        }
        else {
            obj[name] = value;
        }
        return obj;
    },
    LoadingCount: 0, //Count loading request
    Ajax: function (setting, done) {
        Common.ShowLoading(true);
        //There is one more loading request
        Common.LoadingCount++;
        $('.loading').stop().fadeToggle(Common.LoadingCount > 0);
        //Add default setting
        setting = $.extend({
            type: "POST",
            async: true,
            cache: false,
            dataType: 'json'
        }, setting);

        return $.ajax(setting).done(function (data, textStatus, jqXHR) {
            Common.ShowLoading(false);
            //Call done callback 
            done(data);
        }).fail(function (jqXHR, textStatus, errorThrown) {
            Common.ShowLoading(false);
            //Server not respond
            if (!jqXHR) return;

            switch (jqXHR.status) {

                case 403:
                    //Not login error
                    alert(jqXHR.statusText);
                    //Redirect to login page
                    if (window && window.location) {
                        window.location.href = "/";
                    }
                    break;
                case 500:
                    alert(jqXHR);
                    alert(textStatus);
                    alert(errorThrown);
                    break;
                case 401:

                    //Redirect to login page
                    if (window && window.location) {
                        //window.location.href = "/Error/Permission";
                        //window.location.href = jqXHR.Data.LogOnUrl;
                        var d = $.parseJSON(jqXHR.responseText);
                        //alert("login failed");  // Let's redirect
                        window.location.href = d.LogOnUrl;
                    }
                    else {
                        //Not login error
                        $.notify({
                            // options
                            message: 'Bạn không có quyền thực hiện chức năng này.!!!'
                        }, {
                                // settings
                                delay: 1000,
                                timer: 1000,
                                type: 'danger'
                            });
                    }
                    break;
                //default:
                //    //Other error
                //    alert(jqXHR.statusText);
                //    break;
            }
        }).always(function () {
            // There is one less loading request
            Common.LoadingCount--;
            $('.loading').stop().fadeToggle(Common.LoadingCount > 0);
            Common.ShowLoading(false);
        });
    },
    ShowLoading: function (isShow) {
        if (isShow) {
            $(".loadingCommon").show();
        } else {
            $(".loadingCommon").hide();
        }
    },
    createCookie: function (cookieName, cookieValue, daysToExpire) {
        var date = new Date();
        var oldCookie = Common.accessCookie(cookieName);
        if (oldCookie != "") {
            document.cookie = cookieName + "=; expires=" + date.toGMTString();
        }      
        date.setTime(date.getTime() + (daysToExpire * 24 * 60 * 60 * 1000));
        document.cookie = cookieName + "=" + cookieValue + "; expires=" + date.toGMTString();
    },
    accessCookie: function (cookieName) {
        var name = cookieName + "=";
        var allCookieArray = document.cookie.split(';');
        for (var i = 0; i < allCookieArray.length; i++) {
            var temp = allCookieArray[i].trim();
            if (temp.indexOf(name) == 0)
                return temp.substring(name.length, temp.length);
        }
        return "";
    },
    RemoveScript: function (c) {
        r = c.replace(/(<script(.|\n)*<\/script>)|(&lt;script(.|\n)*&lt;\/script&gt;)/gi, "");
        return r;
    },
    GetDistrictByProvinceID: function (e) {
        Common.ShowLoading(true);
        var _key = $(e).attr('data-name-select');
        Common.Ajax({
            url: '/Common/GetDistrictByProvinceID',
            type: "POST",
            ContentType: 'application/json; charset=utf-8',
            async: false,
            cache: false,
            dataType: 'JSON',
            data: { ProvinceID : $(e).val()},
        }, function (result) {
            Common.ShowLoading(false);
            if (result.status == true) {
                var _html = '';
                var _data = result.data;
                for (var i = 0; i < _data.length; i++)
                { 
                    var item = _data[i];
                    _html += '<option value="' + item.DISTRICTID + '">' + item.DISTRICTNAME + '</option>';
                }
                console.log(_html);
                $('#' + _key).empty().append(_html);
            }
            else {
                 toastr.error("Đã xảy ra lỗi!", "Thông báo", { timeOut: 5000, progressBar: true, closeButton: true });

            }                   
        });
    },
    F_ConvertToNumericVN: function (value, n, x, s, c) {
        var re = '\\d(?=(\\d{' + (x || 3) + '})+' + (n > 0 ? '\\D' : '$') + ')',
            num = value.toFixed(Math.max(0, ~~n));
        return (c ? num.replace('.', c) : num).replace(new RegExp(re, 'g'), '$&' + (s || ','));
    },
    SumTotal: function (array, key) {
        return array.reduce(function (r, a) {
            return r + a[key];
        }, 0);
    },
    ShowMessage: (mesage, status) => {
        if (status == '1')
            toastr.success(
                         mesage,
                         "Thông báo",
                         {
                             timeOut: 2000,
                             progressBar: true,
                             closeButton: true
                         }
                     );
        else
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
};
this.Common = window.Common = new Common();

