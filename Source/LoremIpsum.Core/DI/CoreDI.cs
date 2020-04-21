using Dna;

namespace LoremIpsum.Core
{
    public class CoreDI
    {
        /// <summary>
        /// A shortcut to access the <see cref="ITaskManager"/>
        /// </summary>
        public static ITaskManager TaskManager => Framework.Service<ITaskManager>();
    }
}
