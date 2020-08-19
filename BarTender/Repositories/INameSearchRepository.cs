using BarTender.DataModels;
using BarTender.Dtos;

namespace BarTender.Repositories {
    public interface INameSearchRepository {
        NameSearchDefaultsDto GetDefaults(PoleDB poleDb,ShwaDB shwaDb);
    }
}