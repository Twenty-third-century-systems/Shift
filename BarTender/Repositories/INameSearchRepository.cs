using BarTender.Dtos;
using Cooler.DataModels;

namespace BarTender.Repositories {
    public interface INameSearchRepository {
        NameSearchDefaultsDto GetDefaults(PoleDB poleDb,ShwaDB shwaDb);
    }
}