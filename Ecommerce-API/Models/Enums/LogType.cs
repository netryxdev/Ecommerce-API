namespace Ecommerce_API.Models.Enums
{
    public enum LogType // Valores negativos = erro; Valores positivos = log normal
    {
        User = 100,
        Product = 200,
        UserError = -100,
        ProductError = -200
    }
}
