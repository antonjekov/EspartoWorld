namespace EspartoWorld.Web.ViewModels.ExpositionItems
{
    using System.Collections.Generic;

    using EspartoWorld.Web.ViewModels.ExposicionItems;
    using EspartoWorld.Web.ViewModels.Shared;

    public class ExpositionItemViewModelPagination : PagingViewModel
    {
        public IEnumerable<ExpositionItemViewModel> ExpositionItems { get; set; }

        public int ItemCategory { get; set; }
    }
}
