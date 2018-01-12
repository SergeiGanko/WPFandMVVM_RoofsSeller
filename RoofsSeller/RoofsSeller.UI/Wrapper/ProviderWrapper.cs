using RoofsSeller.Model.Entities;
using System;

namespace RoofsSeller.UI.Wrapper
{
    [Serializable]
    public class ProviderWrapper : ModelWrapper<Provider>
    {
        public ProviderWrapper(Provider model) : base(model)
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

        public string Info
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
    }
}
