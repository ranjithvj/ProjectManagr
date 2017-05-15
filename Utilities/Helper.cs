namespace Utilities
{
    public class Helper
    {
        public static void TransferData<T>(T source, T destination)
        {
            foreach (var property in typeof(T).GetProperties())
            {
                property.SetValue(destination, property.GetValue(source));
            }
        }

        public void Donothing()
        {
        }
    }
}