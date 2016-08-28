using System.Web.Optimization;

namespace MVCSchooldbDemo
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.easyui.min.js",
                "~/Scripts/easyui-lang-zh_CN.js",
                "~/Scripts/jquery.uploadify.min.js",
                "~/Scripts/common.js"
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/EasyUI/themes/metro-blue/easyui.css",
                "~/Content/EasyUI/themes/icon.css",
                "~/Content/EasyUI/themes/color.css",
                "~/Content/Site.css",
                "~/Content/Login.css",
                "~/Content/font-awesome.min.css",
                "~/Content/Uploadify/uploadify.css",
                "~/Content/material-design-iconic-font.css"));
        }
    }
}