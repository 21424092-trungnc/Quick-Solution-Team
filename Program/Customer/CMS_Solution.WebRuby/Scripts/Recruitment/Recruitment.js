var Recruitment = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
};
Recruitment.prototype = {
    Initialize: function (options) {
        this.RegisterEvent();
    },
    RegisterEvent: function () {
        this.IsPaging = false;
        var that = this;
        $("#btnFormSearch").unbind("click").click(function () {
            Common.ShowLoading(true);
            var data = $("#form-SearchRecruitment").serialize();
            Common.Ajax({
                url: Common.Recruitment.Url.Search,
                type: "POST",
                ContentType: 'application/json; charset=utf-8',
                async: false,
                cache: false,
                dataType: 'html',
                data: data
            }, function (result) {
                Common.ShowLoading(false);
                if (!Common.Empty(result)) {
                    $('#result-FormSearch').empty().append(result);
                };
            });
        });
    },
}