﻿using System.Web;
using System.Web.Optimization;

namespace WildCampingWithMvc
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryajax").Include(
                       "~/Scripts/jquery.unobtrusive*"));

            // ***BEGIN*** JQGRID Files
            bundles.Add(new StyleBundle("~/Content/jqgrid").Include(
                      "~/Content/jquery.jqGrid/ui.jqgrid.css",
                      "~/Content/themes/jq-ui-themes/sunny/theme.css"));

            bundles.Add(new ScriptBundle("~/bundles/jqgrid").Include(
                       "~/Scripts/jquery.jqGrid.min.js",
                       "~/Scripts/i18n/grid.locale-en.js",
                       "~/Scripts/Custom/custom-admin-user-grid.js"
                       ));
            // ***END*** JQGRID Files

            // ***BEGIN*** CUSTOM JS FILES !!!!!!!!!!!!!!!!!!!!
            bundles.Add(new ScriptBundle("~/bundles/custom").Include(
                        "~/Scripts/Custom/custom-js-*"));

            bundles.Add(new ScriptBundle("~/bundles/category").Include(
                        "~/Scripts/Custom/category-js-*"));
            // ***END*** CUSTOM JS FILES

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap-united.css",
                      "~/Content/Site.css"));
        }
    }
}
