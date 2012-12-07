// Type: Spark.Web.Mvc.SparkViewFactory
// Assembly: Spark.Web.Mvc, Version=1.1.0.0, Culture=neutral, PublicKeyToken=7f8549eed921a12c
// Assembly location: C:\business websites\www.blackpoolgigs.co.uk\lib\spark\Spark.Web.Mvc.dll

using Spark;
using Spark.FileSystem;
using Spark.Web.Mvc.Wrappers;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;

namespace Spark.Web.Mvc
{
    public class SparkViewFactory : IViewEngine, IViewFolderContainer, ISparkServiceInitialize
    {
        public SparkViewFactory();
        public SparkViewFactory(ISparkSettings settings);
        public ISparkSettings Settings { get; set; }
        public ISparkViewEngine Engine { get; set; }
        public IViewActivatorFactory ViewActivatorFactory { get; set; }
        public IViewFolder ViewFolder { get; set; }
        public IDescriptorBuilder DescriptorBuilder { get; set; }
        public ICacheServiceProvider CacheServiceProvider { get; set; }

        #region ISparkServiceInitialize Members

        void ISparkServiceInitialize.Initialize(ISparkServiceContainer container);

        #endregion

        #region IViewEngine Members

        ViewEngineResult IViewEngine.FindPartialView(ControllerContext controllerContext, string partialViewName,
                                                     bool useCache);

        ViewEngineResult IViewEngine.FindView(ControllerContext controllerContext, string viewName, string masterName,
                                              bool useCache);

        void IViewEngine.ReleaseView(ControllerContext controllerContext, IView view);

        #endregion

        #region IViewFolderContainer Members

        IViewFolder IViewFolderContainer.ViewFolder { get; set; }

        #endregion

        public virtual void Initialize(ISparkServiceContainer container);
        public void SetEngine(ISparkViewEngine engine);
        public virtual ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName);

        public virtual ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName,
                                                 bool useCache);

        public virtual ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName);

        public virtual ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName,
                                                        bool useCache);

        public virtual void ReleaseView(ControllerContext controllerContext, IView view);

        public SparkViewDescriptor CreateDescriptor(ControllerContext controllerContext, string viewName,
                                                    string masterName, bool findDefaultMaster,
                                                    ICollection<string> searchedLocations);

        public SparkViewDescriptor CreateDescriptor(string targetNamespace, string controllerName, string viewName,
                                                    string masterName, bool findDefaultMaster);

        public Assembly Precompile(SparkBatchDescriptor batch);
        public List<SparkViewDescriptor> CreateDescriptors(SparkBatchDescriptor batch);
        public IList<SparkViewDescriptor> CreateDescriptors(SparkBatchEntry entry);
    }
}
