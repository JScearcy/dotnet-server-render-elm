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
        public string ModulePath { get; set; }
        public string FunctionName { get; set; }
        public string ScriptLocation { get; set; }
        public bool AttachStartScript { get; set; }

        public ElmRenderTagHelper(INodeServices _nodeServices)
        {
            nodeServices = _nodeServices;
        }
        
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            RenderedElmResult elmResult;
            if (!string.IsNullOrWhiteSpace(FunctionName)) 
            {
                elmResult = await nodeServices.InvokeExportAsync<RenderedElmResult>(ModulePath, FunctionName, Flags);
            }
            else
            {
                elmResult = await nodeServices.InvokeAsync<RenderedElmResult>(ModulePath, Flags);
            }
            if (AttachStartScript)
            {
                if (Flags != null) 
                {
                    output.PostContent.SetHtmlContent($"<script src='{ScriptLocation}'></script><script>Elm.Main.embed(document.getElementById('{ContainerId}'), {Flags})</script>");
                } 
                else
                {
                    output.PostContent.SetHtmlContent($"<script src='{ScriptLocation}'></script><script>Elm.Main.embed(document.getElementById('{ContainerId}'))</script>");
                }
            }
            output.TagName = Container ?? "div";
            output.Attributes.SetAttribute("id", ContainerId);
            output.Content.SetHtmlContent(elmResult.Html);
        }
    }
}