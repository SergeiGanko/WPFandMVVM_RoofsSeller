using RoofsSeller.Model.Entities;
using System;
using System.Collections.Generic;

namespace RoofsSeller.UI.Wrapper
{

    public class CustomerWrapper : ModelWrapper<Customer>
    {
        public CustomerWrapper(Customer model) : base(model)
        {
        }

        public int Id { get { return Model.Id; } }

        public string Name
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string Address
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string Info
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string Phone
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string Email
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        protected override IEnumerable<string> ValidateProperty(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(Name):
                    if (string.Equals(Name, "Robot", StringComparison.OrdinalIgnoreCase))
                    {
                        yield return "Robots are not valid customers";
                    }
                    break;
            }
        }
    }
}
