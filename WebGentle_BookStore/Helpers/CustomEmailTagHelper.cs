using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebGentle_BookStore.Helpers
{
    public class CustomEmailTagHelper : TagHelper
    {
        //To dynamically pass value
        public string MyEmail { get; set; } //this will be treated as attribute in the tag.
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a"; //for anchor tag <a>
            //Now to define attributes to tag helper
            output.Attributes.SetAttribute("href", "mailto:sungitmca@gmail.com");
            //OR
            output.Attributes.SetAttribute("href", $"mailto:{MyEmail}");
            //OR
            output.Attributes.Add("id", "my-email-id");
            //To Set content
            output.Content.SetContent("my-email");
            //base.Process(context, output);
        }
    }
}
