using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Components;
namespace DemoBlazorServer.Components;

public partial class GenericTable<ItemType>
{
	[Parameter]
	[EditorRequired]
	public RenderFragment? HeaderContent { get; set; }

    [Parameter]
    [EditorRequired]
    public RenderFragment<ItemType>? RowContent { get; set; }

    [Parameter]
    [AllowNull]
    public IReadOnlyList<ItemType> Items { get; set; }
}

