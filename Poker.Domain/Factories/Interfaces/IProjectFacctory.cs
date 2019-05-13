using Poker.Domain.Entities.Interfaces;

namespace Poker.Domain.Factories.Interfaces
{
    public interface IProjectFactory
    {
        IProject Get(int id);

        IProject Get(Transportation.Entities.Project project);

        IProject New();
    }
}