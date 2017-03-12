using System.Threading.Tasks;
using Microsoft.AspNetCore.NodeServices;
using Microsoft.AspNetCore.Razor.TagHelpers;
using server_render_elm.Models;

namespace server_render_elm.TagHelpers
{
    public class ElmRenderTagHelper : TagHelper
    {
        private INodeServices nodeServices { get; set; }
        public string Container { get; set; }
        public string ContainerId { get; set; }
        public string Flags { get; set; }

        public ElmRenderTagHelper(INodeServices _nodeServices)
        {
            nodeServices = _nodeServices;
        }
        
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var elmResult = await nodeServices.InvokeExportAsync<RenderedElmResult>("./NodeCode/render-elm.js", "renderElmFromJS", Flags);
            output.TagName = Container ?? "div";
            output.Attributes.SetAttribute("id", ContainerId);
            output.Content.SetHtmlContent(elmResult.Html);
        }
    }
}