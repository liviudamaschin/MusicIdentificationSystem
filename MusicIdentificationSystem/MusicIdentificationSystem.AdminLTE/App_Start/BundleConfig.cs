using System.Web.Optimization;
using WebHelpers.Mvc5;

namespace MusicIdentificationSystem.AdminLTE.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Bundles/css")
                .Include("~/Content/css/bootstrap.min.css", new CssRewriteUrlTransformAbsolute())
                .Include("~/Content/css/bootstrap-select.css")
                .Include("~/Content/css/bootstrap-datepicker3.min.css")
                .Include("~/Content/css/bootstrap-datepicker3.min.css")
                .Include("~/Content/css/font-awesome.min.css", new CssRewriteUrlTransformAbsolute())
                .Include("~/Content/css/icheck/blue.min.css", new CssRewriteUrlTransformAbsolute())
                .Include("~/Content/css/AdminLTE.css", new CssRewriteUrlTransformAbsolute())
                .Include("~/Content/css/Gridmvc.css", new CssRewriteUrlTransformAbsolute())
                .Include("~/Content/css/select2/select2.css")
                .Include("~/Content/css/bootstrap-datepicker.css")
                .Include("~/Content/css/daterangepicker.css")
                .Include("~/Content/css/timepicker/bootstrap-timepicker.css")
                .Include("~/Content/css/skins/skin-blue.css"));

            bundles.Add(new ScriptBundle("~/Bundles/js")
                .Include("~/Content/js/plugins/jquery/jquery-2.2.4.js")
                .Include("~/Content/js/plugins/bootstrap/bootstrap.js")
                .Include("~/Content/js/plugins/fastclick/fastclick.js")
                .Include("~/Content/js/plugins/slimscroll/jquery.slimscroll.js")
                .Include("~/Content/js/plugins/bootstrap-select/bootstrap-select.js")
                .Include("~/Content/js/plugins/moment/moment.js")
                .Include("~/Content/js/plugins/datepicker/bootstrap-datepicker.js")
                .Include("~/Content/js/timepicker/bootstrap-timepicker.js")
                .Include("~/Content/js/plugins/icheck/icheck.js")
                .Include("~/Content/js/plugins/validator.js")
                .Include("~/Content/js/plugins/inputmask/jquery.inputmask.bundle.js")
                .Include("~/Content/js/adminlte.js")
                .Include("~/Content/js/daterangepicker.js")
                .Include("~/Content/js/moment.min.js")
                .Include("~/Content/js/Flot/jquery.flot.js")
                .Include("~/Content/js/Flot/jquery.flot.resize.js")
                .Include("~/Content/js/Flot/jquery.flot.pie.js")
                .Include("~/Content/js/Flot/jquery.flot.categories.js")
                .Include("~/Content/js/Scripts/gridmvc.js")
                .Include("~/Content/js/select2/select2.full.js")
                .Include("~/Content/js/init.js"));

#if DEBUG
            BundleTable.EnableOptimizations = false;
#else
            BundleTable.EnableOptimizations = true;
#endif
        }
    }
}
