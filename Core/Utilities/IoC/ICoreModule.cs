using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.IoC
{
    // Bütün projelerde kullanacağımız IoC injectionları burada bulunur.
    // Genel bağımlılıkları yükler.
    public interface ICoreModule
    {
        void Load(IServiceCollection serviceCollection);
    }
}
