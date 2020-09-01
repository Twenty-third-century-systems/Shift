using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BarTender.Dtos;
using BarTender.Models;
using Cooler.DataModels;

namespace BarTender.Repositories {
    public class NameSearchRepository : INameSearchRepository {
        public NameSearchDefaultsDto GetDefaults(PoleDB poleDb, ShwaDB shwaDb)
        {
            var reasons = this.GetReasons(poleDb);
            var types = this.GetTypes(poleDb);
            var designations = this.GetDesignations(poleDb);


            var offices = this.GetOffices(shwaDb);
            return new NameSearchDefaultsDto
            {
                Reasons = reasons,
                Types = types,
                Designations = designations,
                Sorters = offices
            };
        }


        private List<Val> GetOffices(ShwaDB shwaDb)
        {
            List<Val> offices = new List<Val>();
            var officesFromDb = (
                from a in shwaDb.Cities
                where a.CanSort != null
                select a
            ).ToList();

            foreach (var city in officesFromDb)
            {
                offices.Add(new Val
                {
                    id = city.ID,
                    value = city.Name.ToUpper()
                });
            }

            return offices;
        }


        private List<Val> GetDesignations(PoleDB poleDb)
        {
            List<Val> designations = new List<Val>();
            var designationsFromDb = (
                from a in poleDb.Designations
                select a
            ).ToList();

            foreach (var designation in designationsFromDb)
            {
                designations.Add(new Val
                {
                    id = designation.Id,
                    value = designation.Description.ToUpper()
                });
            }

            return designations;
        }


        private List<Val> GetTypes(PoleDB poleDb)
        {
            List<Val> types = new List<Val>();
            var typesFromDb = (
                from a in poleDb.Services
                where a.DepartmentId == 1 &&
                      a.CanBeApplied > 0 &&
                      a.IsAnEntity > 0
                select a
            ).ToList();

            foreach (var type in typesFromDb)
            {
                types.Add(new Val
                {
                    id = type.Id,
                    value = type.Description.ToUpper()
                });
            }

            return types;
        }


        private List<Val> GetReasons(PoleDB poleDb)
        {
            List<Val> reasons = new List<Val>();
            var reasonsFromDb = (
                from a in poleDb.ReasonForSearches
                select a
            ).ToList();

            foreach (var reson in reasonsFromDb)
            {
                reasons.Add(new Val
                {
                    id = reson.Id,
                    value = reson.Description.ToUpper()
                });
            }

            return reasons;
        }
    }
}