using Model;

namespace DataLayer.DataMappers
{
    public interface IEquipaMapper : IMapper<Equipa, int>
    {
        Equipa GetEquipaLivre(string competencia);
    }
}
