
using System;
using System.ComponentModel.DataAnnotations;

namespace Shared
{

    public class NotEmptyGuidAttribute:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            Guid guidValue;
            if (value != null && Guid.TryParse(value.ToString(),out guidValue))
            {
                return guidValue != Guid.Empty;
            }
            return false;
        }
    }

}
