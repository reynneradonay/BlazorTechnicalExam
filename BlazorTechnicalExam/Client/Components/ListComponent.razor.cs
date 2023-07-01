using BlazorTechnicalExam.Shared.Data;
using Microsoft.AspNetCore.Components;
using System.Linq.Dynamic.Core;
using System.Reflection.Metadata;

namespace BlazorTechnicalExam.Client.Components
{
    public partial class ListComponent<TItem> : ComponentBase
    {
        public ListComponent() { }

        public DataSourceResult<TItem> DataSource { get; set; }

        [Parameter]
        public IEnumerable<TItem> Items { get; set; }

        [Parameter]
        public RenderFragment<TItem> ItemList { get; set; }

        [Parameter]
        public Func<TItem, string> GetFilterableText { get; set; }

        [Parameter]
        public string SortFilter { get; set; }

        [Parameter]
        public string SortAction { get; set; } = "ASC";

        [Parameter]
        public string Filter { get; set; }

        [Parameter]
        public string GroupBy { get; set; }

        public IEnumerable<TItem> FilteredItems { get; set; }

        private static readonly Func<TItem, string> DefaultGetFilterableText =
            item => (item?.ToString() ?? "");

        public void Refresh()
        {
            StateHasChanged();
        }

        public void SetFilter(string _filter)
        {
            Filter = _filter;
        }

        public IEnumerable<TItem> GetFilteredItems()
        {
            Func<TItem, string> filterFunc = GetFilterableText ?? DefaultGetFilterableText;
            IEnumerable<TItem> result = (Items ?? Array.Empty<TItem>());
            if (!string.IsNullOrEmpty(Filter))
            {
                result = result
                    .Where(x =>
                        (GetFilterableText(x) ?? "")
                        .Contains(Filter, StringComparison.InvariantCultureIgnoreCase));
            }
            if (!string.IsNullOrEmpty(SortFilter))
            {
                result = result
                    .AsQueryable()
                    .OrderBy($"{SortFilter} {SortAction}");
            }
            FilteredItems = result;
            return result;
        }
    }
}
