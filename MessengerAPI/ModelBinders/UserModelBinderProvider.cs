using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MessengerAPI.ModelBinders
{
    public class UserModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            throw new NotImplementedException();
        }
    }
}
