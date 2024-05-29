using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using log4net;
using TAINATechTest.Data.Data;
using TAINATechTest.Data.Models;


namespace TAINATechTest.Data.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly PersonContext _personContext;

        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);

        public PersonRepository(PersonContext personContext)
        {
            _personContext = personContext;
        }

        public List<Person> GetAll()
        {
            try
            {
                List<Person> people = _personContext.People?.ToList();
                
                _log.Debug($"Found {people?.Count} people");

                return people;
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }

            return null;
        }

        public Person GetById(long id)
        {
            try
            {
                Person person = _personContext.People?.FirstOrDefault(x => x.Id == id);
                _log.Debug($"Found {person?.FirstName}  {person?.LastName} for id: {id}");
                return person;
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }

            return null;
        }

        public long AddOrUpdate(Person person)
        {
            try
            {
                if (person.Id == 0)
                {
                    _personContext.Add(person);
                    _log.Debug($"Added {person?.FirstName}  {person?.LastName} with id: {person.Id}");
                }
                else
                {
                    _personContext.Update(person);
                }

                return _personContext.SaveChanges() > 0 ? person.Id : 0;
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }

            return 0;
        }

        public bool Delete(long id)
        {
            try
            {
                var person = _personContext.People.FirstOrDefault(x => x.Id == id);
                if (person == null)
                {
                    _log.Debug($"Person with id: {id} not deleted as does not exist in DB");
                }
                else
                {
                    _personContext.Remove(person);
                    _personContext.SaveChanges();
                    _log.Debug($"Deleted person with id: {id}");
                    return true;
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }

            return false;
        }
    }
}
