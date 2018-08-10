using Models;
using RepositoryInterfaces;
using ServiceInterfaces;
using System.Collections.Generic;

namespace Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public void Delete(int id)
        {
            _projectRepository.Delete(id);
        }

        public Project Get(int id)
        {
            return _projectRepository.Get(id);
        }

        public List<Project> GetAll()
        {
            return _projectRepository.GetAll();
        }

        public void Insert(Project item)
        {
            _projectRepository.Insert(item);
        }

        public void Update(Project item)
        {
            _projectRepository.Update(item);
        }
    }
}