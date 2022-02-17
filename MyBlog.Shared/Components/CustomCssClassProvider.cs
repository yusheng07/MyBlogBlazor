using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace MyBlog.Shared.Components /*namespace MyBlogServerSide.Components*/
{
    public class CustomCssClassProvider<ProviderType>: ComponentBase 
                    where ProviderType : FieldCssClassProvider , new()
    {
        //Cascading Parameter that will populated from EditForm
        [CascadingParameter]
        EditContext? CurrentEditContext { get; set; } 
        public ProviderType Provier { get; set; } = new ProviderType();

        protected override void OnInitialized()
        {
            if (CurrentEditContext == null)
            {
                throw new InvalidOperationException(
                    $"{nameof(DataAnnotationsValidator)} requires a cascading parameter of type {nameof(EditContext)}." +
                    $" For example, you can use {nameof(DataAnnotationsValidator)} inside an EditForm.");
            }
            //set FieldCssClassProvider on EditContext
            CurrentEditContext.SetFieldCssClassProvider(Provier);
        }
    }
}
