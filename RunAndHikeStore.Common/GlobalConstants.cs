namespace RunAndHikeStore.Common
{
    public static class GlobalConstants
    {
        public const string SystemName = "RunAndHikeStore";

        public const string AdministratorRoleName = "Administrator";
        public const string NormalizedAdministratorRoleName = "ADMINISTRATOR";

        public const string UserRoleName = "User";
        public const string NormalizedUserRoleName = "USER";

        public const string DecimalMaxValue = "79228162514264337593543950335";

        public static class ApplicationUser
        {
            public const int EmailMinLength = 5;
            public const int EmailMaxLength = 60;

            public const int PasswordMaxLength = 20;
            public const int PasswordMinLength = 5;

            public const int FirstNameMinLength = 2;
            public const int FirstNameMaxLength = 50;

            public const int LastNameMinLength = 2;
            public const int LastNameMaxLength = 50;

            public const int PhoneNumberMinLength = 7;
            public const int PhoneNumberMaxLength = 15;
        }

        public static class Address
        {
            public const int StreetAddressMinLength = 10;
            public const int StreetAddressMaxLength = 150;

            public const int CityMinLength = 2;
            public const int CityMaxLength = 150;

            public const int CountryMinLength = 2;
            public const int CountryMaxLength = 150;

            public const int PostalCodeMinLength = 3;
            public const int PostalCodeMaxLength = 20;

            public const int AddressTypeMaxLength = 20;
        }

        public static class Brand
        {
            public const int BrandNameMinLength = 2;
            public const int BrandNameMaxLength = 50;
        }

        public static class Product
        {
            public const int ProductNameMinLength = 2;
            public const int ProductNameMaxLength = 50;

            public const int ProductDescriptionMinLength = 5;
            public const int ProductDescriptionMaxLength = 5000;

            public const int ProductImageUrlMinLength = 5;
            public const int ProductImageUrlMaxLength = 3000;

            public const int GenderMaxLength = 10;
        }

        public static class ProductType
        {
            public const int ProductTypeNameMinLength = 2;
            public const int ProductTypeNameMaxLength = 50;
        }

        public static class Size
        {
            public const int ProductSizeNameMinLength = 1;
            public const int ProductSizeNameMaxLength = 10;

            public const int GenderMaxLength = 10;
        }

        public static class Category
        {
            public const int CategoryNameMinLength = 2;
            public const int CategoryNameMaxLength = 50;
        }

        public static class Order
        {
            public const int OrderStatusMaxLength = 20;
            public const int PaymentStatusMaxLength = 20;
        }

        public static class MessageConstant
        {
            public const string ErrorMessage = "ErrorMessage";
            public const string SuccessMessage = "SuccessMessage";
            public const string WarningMessage = "WarningMessage";
        }
    }
}
