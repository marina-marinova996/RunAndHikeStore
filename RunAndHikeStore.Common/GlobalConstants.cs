namespace RunAndHikeStore.Common
{
    public static class GlobalConstants
    {
        public const string SystemName = "RunAndHikeStore";

        public const string AdministratorRoleName = "Administrator";

        public const string UserRoleName = "User";

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
        }

        public static class Address
        {
            public const int MinLength = 2;
            public const int MaxLength = 50;
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
            public const int ProductSizeNameMinLength = 2;
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
    }
}
