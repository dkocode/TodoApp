using System.Text;
using BtaApplication.Todo.Dtos;
using BtaDomain.ToDo;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BtaPresentationWeb.TagHelpers
{
    [HtmlTargetElement("todo-table", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class TodoTableTagHelper : TagHelper
    {
        private readonly IUrlHelperFactory _urlHelperFactory;
        
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContextData { get; set; }
        
        public ICollection<TodoItemDto> Items { get; set; } = new List<TodoItemDto>(0);

        public TodoTableTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            _urlHelperFactory = urlHelperFactory;
        }
        
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var urlHelper = _urlHelperFactory.GetUrlHelper(ViewContextData);
            
            output.TagName = "table";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Add("class", "table");

            var precontent = new StringBuilder(@"
                <tr>
                    <th scope=""col"">Id</th>
                    <th scope=""col"">Name</th>
                    <th scope=""col"">Priority</th>
                    <th scope=""col"">Status</th>
                    <th scope=""col"">&nbsp;</th>
                </tr>");
            
            var content = new StringBuilder();

            if (!Items.Any())
            {
                content.AppendLine(@"
                <tr>
                    <td colspan=""5"">There are no todos! Let's add a new one to see some interesting stuff here ...</td>
                </tr>");
            }
            else
            {
                foreach (TodoItemDto item in Items)
                {
                    content.AppendLine(@$"
                    <tr>
                        <td>{item.Id}</td>
                        <td>{item.Name}</td>
                        <td>{item.Priority.Name}</td>
                        <td>{item.Status.Name}</td>
                        <td><a href=""{urlHelper.Action("Detail", "Todo", new { item.Id })}"">Detail</a></td>
                    </tr>");
                }
            }

            output.PreContent.SetHtmlContent(precontent.ToString());
            output.Content.SetHtmlContent(content.ToString());
        }
    }
}