using DAL.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MessengerAPI.ModelBinders
{
    public class UserModelBinder : IModelBinder
    {
        private readonly IModelBinder defaultBinder;

        public UserModelBinder(IModelBinder defaultBinder)
        {
            this.defaultBinder = defaultBinder;
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var modelTypeValue = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).FirstValue;

            if (bindingContext.ModelType == typeof(Account))
            {
                bindingContext.Result = ModelBindingResult.Success(bindingContext.HttpContext.Items["Account"]);
            }
            else if (bindingContext.ModelType == typeof(User))
            {
                bindingContext.Result = ModelBindingResult.Success(bindingContext.HttpContext.Items["User"]);
            }
            return Task.CompletedTask;
        }
    }
}
