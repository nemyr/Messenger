using DAL.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace MessengerAPI.ModelBinders
{
    public class UserModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if (context is null) 
                throw new ArgumentNullException(nameof(context));
            var modelType = context.Metadata.ModelType;
            if (modelType == typeof(User) || modelType == typeof(Account))
            {
                try
                {
                    ILoggerFactory loggerFactory = context.Services.GetRequiredService<ILoggerFactory>();
                    IModelBinder binder = new UserModelBinder(new SimpleTypeModelBinder(modelType, loggerFactory));
                    return context.Metadata.ModelType == modelType ? binder : null;
                }
                catch (Exception)
                {

                }
            }
            return null;
        }
    }
}
