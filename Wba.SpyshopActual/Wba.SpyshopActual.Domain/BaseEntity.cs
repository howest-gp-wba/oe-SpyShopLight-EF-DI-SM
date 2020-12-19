using System;
using System.Collections.Generic;
using System.Text;

namespace Wba.SpyshopActual.Domain
{
    public abstract class BaseEntity<TKey>
    {
        public TKey Id { get; set; }
    }
}
