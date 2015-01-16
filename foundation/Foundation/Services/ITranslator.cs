namespace Foundation.Services
{
    public interface ITranslator<T1, T2>
    {
        T2 Translate(T1 instance);

        T1 Translate(T2 instance);
    }
}