using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MessengerAPI.ModelBinders
{
    public class UserModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            throw new NotImplementedException();
        }
    }
}
