var AddProductToCart = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
};
AddProductToCart.prototype = {
    Initialize: function (options) {
        this.RegisterEvent();
    },
    RegisterEvent: function () {
        $('td[name="td-quantity"] .quantity-up').unbind("click").click(function () {
            let productId = $(this).closest("tr").data("id");
            Common.ShowLoading(true);
            Common.AddProductToCart.UpdateOrderCart(productId, "T", this)
        })
        $('td[name="td-quantity"] .quantity-down').unbind("click").click(function () {
            let productId = $(this).closest("tr").data("id");
            let input = $('input[name="Quantity-' + productId + '"]');
            if (input && input.val() === 1) {
                toastr.error(
                           "Số lượng sản phẩm phải lớn hơn 0.!!!",
                           "Thông báo",
                           {
                               timeOut: 2000,
                               progressBar: true,
                               closeButton: true
                           }
                       );
                return false;
            }
            Common.ShowLoading(true);
            Common.AddProductToCart.UpdateOrderCart(productId, "G", this)
        })
        $('.quantity-txt').unbind("change").change(function () {
            let input = $(this);
            let productId = $(this).closest("tr").data("id");
            if (input && input.val() <=0) {
                toastr.error(
                           "Số lượng sản phẩm phải lớn hơn 0.!!!",
                           "Thông báo",
                           {
                               timeOut: 2000,
                               progressBar: true,
                               closeButton: true
                           }
                       );
                return false;
            }
            Common.ShowLoading(true);
            var val = $(this).data("value");
            Common.AddProductToCart.UpdateOrderCart(productId, "C", this, input.val() - val);
            $(this).data("value", input.val());
        }),
        document.querySelectorAll(".quantity-txt")
            .forEach(function (item, index) {
                let elemEventHandler = function (event) {
                    this.value = this.value.replace(/[^+\d]/g, '');
                };
                item.removeEventListener('input', elemEventHandler);
                item.addEventListener('input', elemEventHandler); 
            });
        $(".buyAdd").unbind("click").click(function () {
            let productId = $(this).data("id");
            Common.ShowLoading(true);
            Common.Ajax({
                url: Common.AddProductToCart.Url.AddItemToCartByProductID,
                type: "POST",
                ContentType: 'application/json; charset=utf-8',
                async: false,
                cache: false,
                dataType: 'JSON',
                data: { productId: productId }
            }, function (result) {
                Common.ShowLoading(false);
                if (!Common.Empty(result)) {
                    if (result.status === true) {
                        var cookieName = result.cookieid;
                        Common.createCookie("orderCart", result.cookieid, 1);
                        var total = 0;
                        for (var i = 0, _len = result.data.length; i < _len; i++) {
                            total += result.data[i]["Quantity"] || 0;
                        }
                        Common.createCookie(cookieName, result.data ? total : "0", 1);
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
                        window.location.href = Common.AddProductToCart.Url.CartIndex;
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
        });
        $(".deleteOrder").unbind("click").click(function () {
            let productId = $(this).attr("data-id");
            let cartId = $(this).attr("data-cartId");
            Common.ShowLoading(true);
            Common.Ajax({
                url: Common.AddProductToCart.Url.DeleteItemToCartByProductID,
                type: "POST",
                ContentType: 'application/json; charset=utf-8',
                async: false,
                cache: false,
                dataType: 'JSON',
                data: { productId: productId, cartId: cartId }
            }, function (result) {
                Common.ShowLoading(false);
                if (!Common.Empty(result)) {
                    if (result.status === true) {
                        var cookieName = result.cookieid;
                        Common.createCookie("orderCart", result.cookieid, 1);
                        var total = 0
                        for (var i = 0, _len = result.data.length; i < _len; i++) {
                            total += result.data[i]["Quantity"] || 0;
                        }
                        Common.createCookie(cookieName, result.data ? total : "0", 1);
                        toastr.success(
                            "Cập nhật giỏ hàng thành công!!!",
                            "Thông báo",
                            {
                                timeOut: 2000,
                                progressBar: true,
                                closeButton: true
                            }
                        );
                        setTimeout(() => {
                            window.location.reload();
                        }, 2000);
                        //Gọi hàm render số lượng giỏ hàng.
                        //window.location.href = Common.AddProductToCart.Url.CartIndex;
                    }
                    else {
                        toastr.error(
                            "Cập nhật giỏ hàng thất bại.!!!",
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
        })
    },
    AddToCart: function (e) {
        let productId = $(e).data("id");
        Common.ShowLoading(true);
        Common.Ajax({
            url: Common.AddProductToCart.Url.AddItemToCartByProductID,
            type: "POST",
            ContentType: 'application/json; charset=utf-8',
            async: false,
            cache: false,
            dataType: 'JSON',
            data: { productId: productId }
        }, function (result) {
            Common.ShowLoading(false);
            if (!Common.Empty(result)) {
                if (result.status === true) {
                    var cookieName = result.cookieid;
                    Common.createCookie("orderCart", result.cookieid, 1);
                    var total = 0
                    for (var i = 0, _len = result.data.length; i < _len; i++) {
                        total += result.data[i]["Quantity"] || 0;
                    }
                    Common.createCookie(cookieName, result.data ? total : "0", 1);
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
                    window.location.href = Common.AddProductToCart.Url.CartIndex;
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
        });
    },
    UpdateOrderCart: function (productid, flag, e,val) {
        Common.Ajax({
            url: Common.AddProductToCart.Url.AddItemToCartByProductID,
            type: "POST",
            ContentType: 'application/json; charset=utf-8',
            async: false,
            cache: false,
            dataType: 'JSON',
            data: { productId: productid, flag: flag, val: val }
        }, function (result) {
            Common.ShowLoading(false);
            if (!Common.Empty(result)) {
                if (result.status === true) {
                    var cookieName = result.cookieid;
                    Common.createCookie("orderCart", result.cookieid, 1);
                    var total = 0
                    for (var i = 0, _len = result.data.length; i < _len; i++) {
                        total += result.data[i]["Quantity"] || 0;
                    }
                    Common.createCookie(cookieName, result.data ? total : "0", 1);
                    var item = result.data.filter(item => item.ProductId === productid);
                    if (item[0].Price === 0) {
                        $(e).closest("tr").find(".priceTotalItem").text('Liên hệ');
                    }
                    else {
                        $(e).closest("tr").find(".priceTotalItem").text(item[0].TotalPriceItemStr);
                    }
                   
                    $(e).closest("tr").find('input[name="Quantity-' + productid + '"]').val(item[0].Quantity);
                    var _totalPrice = Common.SumTotal(result.data, "TotalPriceItem");
                    (_totalPrice > 0) ? $('.priceTotal').text(_totalPrice.toLocaleString("en-US").replace(/,/g, '.') + 'đ') : $('.priceTotal').text('Liên hệ');
                    toastr.success(
                        "Cập nhật giỏ hàng thành công!!!",
                        "Thông báo",
                        {
                            timeOut: 2000,
                            progressBar: true,
                            closeButton: true
                        }
                    );
                    Common.ShowLoading(false);
                    countOrder("orderCart");
                }
                else {
                    toastr.error(
                        "Cập nhật giỏ hàng thất bại.!!!",
                        "Thông báo",
                        {
                            timeOut: 2000,
                            progressBar: true,
                            closeButton: true
                        }
                    );
                    Common.ShowLoading(false);
                }
            }
        }
        );
    }, 
}