using CrudUsingdapper.Model;

namespace CrudUsingdapper.IService
{
    public interface ICrudService
    {
        ModelCrud Post(ModelCrud crud, String connectionString);

        List<ModelCrud> Gets();

        ModelCrud Get(int Idd);

        ModelCrud Update(ModelCrud crud);
        String Delete(int Idd);
    }
}
