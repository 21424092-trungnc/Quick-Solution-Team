using Core.Common.Localization;
using Core.Common.Mvc.Routes;
using System.Web.Routing;
namespace CMS_Solution.WebRuby.Infrastructure
{
    public partial class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            //We reordered our routes so the most used ones are on top. It can improve performance.

            //home page
            routes.MapLocalizedRoute("HomePage",
                            "",
                            new { controller = "Home", action = "Index" },
                            new[] { "CMS_Solution.WebRuby.Controllers" });
            //san pham
            //home page
            routes.MapLocalizedRoute("ProductCate",
                            "san-pham",
                            new { controller = "Catalog", action = "Index" },
                            new[] { "CMS_Solution.WebRuby.Controllers" });
            //about us
            routes.MapLocalizedRoute("About",
                            "gioi-thieu",
                            new { controller = "Common", action = "About" },
                            new[] { "CMS_Solution.WebRuby.Controllers" });
            //contact us
            routes.MapLocalizedRoute("ContactUs",
                            "lien-he",
                            new { controller = "Common", action = "ContactUs" },
                            new[] { "CMS_Solution.WebRuby.Controllers" });
            //Service
            routes.MapLocalizedRoute("Service",
                            "dich-vu",
                            new { controller = "Service", action = "Index" },
                            new[] { "CMS_Solution.WebRuby.Controllers" });
            //Human resource
            routes.MapLocalizedRoute("HumanResource",
                            "dich-vu/dao-tao-nhan-su",
                            new { controller = "Service", action = "HumanResource" },
                            new[] { "CMS_Solution.WebRuby.Controllers" });
            //Setup Gym
            routes.MapLocalizedRoute("SetupGym",
                            "dich-vu/set-up-phong-tap",
                            new { controller = "Service", action = "SetupGym" },
                            new[] { "CMS_Solution.WebRuby.Controllers" });
            //Gym Design
            routes.MapLocalizedRoute("GymDesign",
                            "dich-vu/thiet-ke-phong-tap",
                            new { controller = "Service", action = "GymDesign" },
                            new[] { "CMS_Solution.WebRuby.Controllers" });
            //Providing Teachers
            routes.MapLocalizedRoute("ProvidingTeachers",
                            "dich-vu/cung-cap-giao-vien",
                            new { controller = "Service", action = "ProvidingTeachers" },
                            new[] { "CMS_Solution.WebRuby.Controllers" });
            //tuyen dung
            routes.MapLocalizedRoute("Recruitment",
                            "tuyen-dung",
                            new { controller = "Common", action = "Recruitment" },
                            new[] { "CMS_Solution.WebRuby.Controllers" });
            //gio hang
            routes.MapLocalizedRoute("Cart",
                            "gio-hang",
                            new { controller = "Cart", action = "Index" },
                            new[] { "CMS_Solution.WebRuby.Controllers" });
            //thanh toán
            routes.MapLocalizedRoute("CheckOut",
                            "thanh-toan",
                            new { controller = "Cart", action = "CheckOutCart" },
                            new[] { "CMS_Solution.WebRuby.Controllers" });
            //thông tin thanh toán
            routes.MapLocalizedRoute("FromCheckOut",
                            "thong-tin-thanh-toan",
                            new { controller = "Cart", action = "FormCheckOut" },
                            new[] { "CMS_Solution.WebRuby.Controllers" });
            //page not found
            routes.MapLocalizedRoute("PageNotFound",
                            "khong-tim-thay-trang",
                            new { controller = "Common", action = "PageNotFound" },
                             new[] { "CMS_Solution.WebRuby.Controllers" });
            ////login
            routes.MapLocalizedRoute("Login",
                            "dang-nhap/",
                            new { controller = "Customer", action = "Login" },
                            new[] { "CMS_Solution.WebRuby.Controllers" });
            //register
            routes.MapLocalizedRoute("Register",
                            "dang-ky/",
                            new { controller = "Customer", action = "Register" },
                            new[] { "CMS_Solution.WebRuby.Controllers" });
            //active account (ActiveUserRegister)
            routes.MapLocalizedRoute("ActiveUserRegister",
                          "kich-hoat/",
                          new { controller = "Customer", action = "ActiveUserRegister" },
                          new[] { "CMS_Solution.WebRuby.Controllers" });

            routes.MapLocalizedRoute("FilterAttributeProductSearch",
                          "sanpham/filter",
                          new { controller = "Catalog", action = "FilterAttributeProductSearch" },
                          new[] { "Nop.Web.Controllers" });
            routes.MapLocalizedRoute("ProductSearch",
                        "tim-kiem",
                        new { controller = "Product", action = "ProductSearch" },
                        new[] { "Nop.Web.Controllers" });
            ////logout
            //routes.MapLocalizedRoute("Logout",
            //                "logout/",
            //                new { controller = "Customer", action = "Logout" },
            //                new[] { "CMS_Solution.WebRuby.Controllers" });

            ////shopping cart
            //routes.MapLocalizedRoute("ShoppingCart",
            //                "cart/",
            //                new { controller = "ShoppingCart", action = "Cart" },
            //                new[] { "CMS_Solution.WebRuby.Controllers" });


            ////sitemap
            //routes.MapLocalizedRoute("Sitemap",
            //                "sitemap",
            //                new { controller = "Common", action = "Sitemap" },
            //                new[] { "CMS_Solution.WebRuby.Controllers" });
            ////recently viewed products
            //routes.MapLocalizedRoute("RecentlyViewedProducts",
            //                "recentlyviewedproducts/",
            //                new { controller = "Product", action = "RecentlyViewedProducts" },
            //                new[] { "CMS_Solution.WebRuby.Controllers" });
            ////new products
            //routes.MapLocalizedRoute("NewProducts",
            //                "san-pham-moi/",
            //                new { controller = "Product", action = "NewProducts" },
            //                new[] { "CMS_Solution.WebRuby.Controllers" });
            ////blog
            //routes.MapLocalizedRoute("Blog",
            //                "blog",
            //                new { controller = "Blog", action = "List" },
            //                new[] { "CMS_Solution.WebRuby.Controllers" });
            ////news
            routes.MapLocalizedRoute("NewsArchive",
                            "tin-tuc",
                            new { controller = "News", action = "List" },
                            new[] { "CMS_Solution.WebRuby.Controllers" });

            ////manufacturers
            //routes.MapLocalizedRoute("ManufacturerList",
            //                "manufacturer/all/",
            //                new { controller = "Catalog", action = "ManufacturerAll" },
            //                new[] { "CMS_Solution.WebRuby.Controllers" });
            ////vendors
            //routes.MapLocalizedRoute("VendorList",
            //                "vendor/all/",
            //                new { controller = "Catalog", action = "VendorAll" },
            //                new[] { "CMS_Solution.WebRuby.Controllers" });


            ////add product to cart (without any attributes and options). used on catalog pages.
            //routes.MapLocalizedRoute("AddProductToCart-Catalog",
            //                "addproducttocart/catalog/{productId}/{shoppingCartTypeId}/{quantity}",
            //                new { controller = "ShoppingCart", action = "AddProductToCart_Catalog" },
            //                new { productId = @"\d+", shoppingCartTypeId = @"\d+", quantity = @"\d+" },
            //                new[] { "CMS_Solution.WebRuby.Controllers" });
            ////add product to cart (with attributes and options). used on the product details pages.
            //routes.MapLocalizedRoute("AddProductToCart-Details",
            //                "addproducttocart/details/{productId}/{shoppingCartTypeId}",
            //                new { controller = "ShoppingCart", action = "AddProductToCart_Details" },
            //                new { productId = @"\d+", shoppingCartTypeId = @"\d+" },
        }
        public int Priority
        {
            get
            {
                return 0;
            }
        }
    }
}