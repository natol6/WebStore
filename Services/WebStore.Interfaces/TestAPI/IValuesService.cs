namespace WebStore.Interfaces.TestAPI
{
    internal interface IValuesService
    {
        IEnumerable<string> GetValues();

        int Count();

        string? GetById(int Id);

        void Add(string Value);

        void Edit(int Id, string Value);

        bool Delete(int Id);
    }
}
